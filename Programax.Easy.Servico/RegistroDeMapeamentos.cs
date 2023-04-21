using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using StructureMap.Configuration.DSL;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Easy.Servico.Movimentacao;
using Programax.Easy.Servico.Cadastros.TabelaPrecoServ;
using Programax.Infraestrutura.Servico;
using System.Reflection;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Configuration;
using Programax.Infraestrutura.Negocio.Utils;
using System.IO;
using Newtonsoft.Json;
using NHibernate.Cfg;
using NHibernate.Event;

namespace Programax.Easy.Servico
{
    public class RegistroDeMapeamentos : RegistroDeSessao
    {
        public static string StringConexao { get; set; }
        public static string StringConexaoII { get; set; }

        public static int IndiceBancoDados { get; set; }

        public RegistroDeMapeamentos()
        {
            For<ISessionFactory>().Singleton().Use(ObterConfiguracaoDoBanco());
            For<ISession>().HybridHttpOrThreadLocalScoped().Use(
                x =>
                {
                    var sessao = x.GetInstance<ISessionFactory>().OpenSession();

                    sessao.FlushMode = FlushMode.Unspecified;

                    //sessao.CacheMode = CacheMode.Refresh;

                    Sessao = sessao;

                    return Sessao;
                });
        }

        public static ISessionFactory ObterConfiguracaoDoBanco()
        {
            ObtenhaStringDeConexao();

            var cfg = new NHibernate.Cfg.Configuration();

            cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.GenerateStatistics, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.Hbm2ddlKeyWords, Hbm2DDLKeyWords.None.ToString())
            .SetProperty(NHibernate.Cfg.Environment.PrepareSql, Boolean.TrueString)
            .SetProperty(NHibernate.Cfg.Environment.PropertyBytecodeProvider, "lcg")
            .SetProperty(NHibernate.Cfg.Environment.PropertyUseReflectionOptimizer, Boolean.TrueString)
            .SetProperty(NHibernate.Cfg.Environment.QueryStartupChecking, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.ShowSql, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.StatementFetchSize, "100")
            .SetProperty(NHibernate.Cfg.Environment.UseProxyValidator, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.UseSecondLevelCache, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.UseSqlComments, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.UseQueryCache, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.WrapResultSets, Boolean.TrueString);

            cfg.EventListeners.PostLoadEventListeners = new IPostLoadEventListener[0];
            cfg.EventListeners.PreLoadEventListeners = new IPreLoadEventListener[0];


            var configuracaoDoBanco = Fluently.Configure(cfg)
                .Database(MySQLConfiguration.Standard.ConnectionString(StringConexao))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();

            cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.GenerateStatistics, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.Hbm2ddlKeyWords, Hbm2DDLKeyWords.None.ToString())
            .SetProperty(NHibernate.Cfg.Environment.PrepareSql, Boolean.TrueString)
            .SetProperty(NHibernate.Cfg.Environment.PropertyBytecodeProvider, "lcg")
            .SetProperty(NHibernate.Cfg.Environment.PropertyUseReflectionOptimizer, Boolean.TrueString)
            .SetProperty(NHibernate.Cfg.Environment.QueryStartupChecking, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.ShowSql, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.StatementFetchSize, "100")
            .SetProperty(NHibernate.Cfg.Environment.UseProxyValidator, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.UseSecondLevelCache, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.UseSqlComments, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.UseQueryCache, Boolean.FalseString)
            .SetProperty(NHibernate.Cfg.Environment.WrapResultSets, Boolean.TrueString);

            cfg.EventListeners.PostLoadEventListeners = new IPostLoadEventListener[0];
            cfg.EventListeners.PreLoadEventListeners = new IPreLoadEventListener[0];

            FabricaSessao = configuracaoDoBanco.CloneCompleto();

            return configuracaoDoBanco;
        }

        private static void ObtenhaStringDeConexao()
        {
            try
            {

                string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

                ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

                var item = conexoes.Conexoes[IndiceBancoDados];

                string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
                string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
                string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
                string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
                int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverPrincipalOnline)
                {
                    StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
                }
                else
                {
                    ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                    database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                    userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                    senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                    porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                    var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                    if (serverSecundarioOnline)
                    {
                        StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                    }
                    else
                    {   
                        //throw new Exception();
                        //throw new Exception("Servidor de banco de dados não encontrado");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao conectar.", ex);
            }
        }

        public class ConexoesJson
        {
            public ConexoesJson()
            {
                Conexoes = new List<Conexao>();
            }

            public List<Conexao> Conexoes { get; set; }
        }

        public class ConexoesJsonII
        {
            public ConexoesJsonII()
            {
                ConexoesII = new List<Conexao>();
            }

            public List<Conexao> ConexoesII { get; set; }
        }

        public class Conexao
        {
            public string NomeConexao { get; set; }

            public string IpPrincipal { get; set; }

            public int PortaPrincipal { get; set; }

            public string BancoDadosPrincipal { get; set; }

            public string UsuarioPrincipal { get; set; }

            public string SenhaPrincipal { get; set; }

            public string IpSecundario { get; set; }

            public int PortaSecundaria { get; set; }

            public string BancoDadosSecundario { get; set; }

            public string UsuarioSecundario { get; set; }

            public string SenhaSecundaria { get; set; }
        }
    }
}
