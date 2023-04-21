using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.PessoaServ
{
    public class EmpresaPessoaMap : MapeamentoBase<EmpresaPessoa>
    {
        public EmpresaPessoaMap()
        {
            Table("PESSOASEMPRESA");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PESEMPR_ID");

            Map(x => x.EmailPrincipal).Column("PESEMPR_EMAIL_PRINCIPAL");
            Map(x => x.EmailCobranca).Column("PESEMPR_EMAIL_COBRANCA");

            Map(x => x.InscricaoEstadual).Column("PESEMPR_INSC_ESTADUAL");
            Map(x => x.TipoInscricaoICMS).Column("PESEMPR_TIPO_INSCRICAO_ICMS").CustomType<EnumTipoInscricaoICMS>();
            Map(x => x.InscricaoMunicipal).Column("PESEMPR_INSC_MUNICIPAL");

            Map(x => x.NomeContato1).Column("PESEMPR_NOME_CONTATO1");
            Map(x => x.RamalContato1).Column("PESEMPR_RAMAL1");
            Map(x => x.DepartamentoContato1).Column("PESEMPR_DEPARTAMENTO1");
            Map(x => x.FuncaoContato1).Column("PESEMPR_FUNCAO1");

            Map(x => x.NomeContato2).Column("PESEMPR_NOME_CONTATO2");
            Map(x => x.RamalContato2).Column("PESEMPR_RAMAL2");
            Map(x => x.DepartamentoContato2).Column("PESEMPR_DEPARTAMENTO2");
            Map(x => x.FuncaoContato2).Column("PESEMPR_FUNCAO2");

            Map(x => x.NomeContato3).Column("PESEMPR_NOME_CONTATO3");
            Map(x => x.RamalContato3).Column("PESEMPR_RAMAL3");
            Map(x => x.DepartamentoContato3).Column("PESEMPR_DEPARTAMENTO3");
            Map(x => x.FuncaoContato3).Column("PESEMPR_FUNCAO3");

            References(x => x.RamoDeAtividade).Column("PESEMPR_RAMO_ID").Not.LazyLoad().Fetch.Join();
        }
    }
}
