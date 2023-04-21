using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using Programax.Easy.Servico;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.PessoaObj.Repositorio;
using Programax.Easy.Servico.Cadastros.EnderecoServ;

namespace Programax.Easy.ConversorEasy
{
    public partial class Form1 : Form
    {
        static FbConnection _conexaoFirebird;
        static FbCommandBuilder _comandoFirebird = new FbCommandBuilder();

        public Form1()
        {
            InitializeComponent();

            ConecteAoBancoAkil();

            ConecteAoFirebird();

            MigreClientes();

            DesconecteDoFirebird();
        }

        private void ConecteAoFirebird()
        {
            string strDeConexao = @"User=SYSDBA;Password=masterkey;Database=" + @"C:\EAS.fdb;" + @"DataSource=localhost;
                                                  Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;
                                                  MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;";

            _conexaoFirebird = new FbConnection(strDeConexao);
            _conexaoFirebird.Open();
        }

        private void DesconecteDoFirebird()
        {
            _conexaoFirebird.Close();
        }

        private void ConecteAoBancoAkil()
        {
            StructureMap.ObjectFactory.Initialize(
                   x =>
                   {
                       x.Scan(scan =>
                       {
                           scan.Assembly(typeof(RegistroDeMapeamentos).Assembly);
                           scan.WithDefaultConventions();
                       }
                           );
                       x.AddRegistry<RegistroDeMapeamentos>();
                   });
        }

        private void MigreClientes()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            ServicoEndereco servicoEndereco = new ServicoEndereco();

            FbCommand comando = new FbCommand();
            comando.Connection = _conexaoFirebird;
            comando.CommandText = "SELECT * FROM CLIENTES";

            var dados = comando.ExecuteReader();

            DataTable clientes = new DataTable();

            clientes.Load(dados);

            List<Pessoa> listaClientesAkil = new List<Pessoa>();

            foreach (DataRow clienteEasy in clientes.Rows)
            {
                Pessoa clienteAkil = new Pessoa();

                #region " DADOS GERAIS "

                clienteAkil.DadosGerais.DataCadastro = DateTime.Now;
                clienteAkil.DadosGerais.EhCliente = true;
                clienteAkil.DadosGerais.NomeFantasia = clienteEasy["FANTASIA"].ToString();
                clienteAkil.DadosGerais.Razao = clienteEasy["RAZAOSOCIAL"].ToString();
                clienteAkil.DadosGerais.Status = clienteEasy["STATUS"].ToString();
                clienteAkil.DadosGerais.TipoPessoa = (EnumTipoPessoa)clienteEasy["TIPO"].ToInt();
                clienteAkil.DadosGerais.CpfCnpj = clienteEasy["INSCRICAO"].ToString();

                if (clienteAkil.DadosGerais.TipoPessoa == EnumTipoPessoa.PESSOAFISICA)
                {
                    clienteAkil.DadosGerais.CpfCnpj = clienteAkil.DadosGerais.CpfCnpj.Remove(11, 1);
                    clienteAkil.DadosGerais.CpfCnpj = clienteAkil.DadosGerais.CpfCnpj.Insert(11, "-");
                }

                #endregion

                #region " ENDEREÇO "

                string cepEasy = clienteEasy["CEP"].ToString();

                if (!string.IsNullOrEmpty(cepEasy))
                {
                    cepEasy = cepEasy.Replace(".", "");
                }

                var cep = servicoEndereco.ConsulteAtivo(cepEasy);

                if (cep != null)
                {
                    EnderecoPessoa enderecoPessoa = new EnderecoPessoa();

                    enderecoPessoa.TipoEndereco = EnumTipoEndereco.PRINCIPAL;
                    //enderecoPessoa.DadosBasicos = cep;
                    enderecoPessoa.Complemento = clienteEasy["COMPLEMENTO"].ToString();
                    enderecoPessoa.Numero = clienteEasy["NUMERO"].ToString();

                    enderecoPessoa.Pessoa = clienteAkil;

                    clienteAkil.ListaDeEnderecos.Add(enderecoPessoa);
                }

                #endregion

                #region " TELEFONES "

                FbCommand comandoTelefones = new FbCommand();
                comandoTelefones.Connection = _conexaoFirebird;
                comandoTelefones.CommandText = "SELECT * FROM CLIENTES WHERE CODENTIDADE =" + clienteEasy["CODIGO"] + "  AND TIPOENTIDADE = 'C'";

                var dadosTelefones = comando.ExecuteReader();

                DataTable clientesTelefones = new DataTable();

                clientesTelefones.Load(dadosTelefones);

                //foreach (DataRow telefone in clientesTelefones)
                //{
                //    var numeroTelefone = telefone["FONE"];


                //}

                #endregion

                listaClientesAkil.Add(clienteAkil);
            }

            var repositorioCliente = FabricaDeRepositorios.Crie<IRepositorioPessoa>();

            repositorioCliente.CadastreLista(listaClientesAkil);
        }
    }
}
