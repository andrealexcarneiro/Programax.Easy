using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.PessoaServ
{
    public class DadosPessoaisMap : MapeamentoBase<DadosPessoais>
    {
        public DadosPessoaisMap()
        {
            Table("PESSOASPESSOAIS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PESPES_ID");

            Map(x => x.DataDeNascimento).Column("PESPES_DATA_NASCIMENTO");

            Map(x => x.DataEmissao).Column("PESPES_DATA_IDENTIDADE");
            Map(x => x.Identidade).Column("PESPES_IDENTIDADE");
            Map(x => x.OrgaoEmissor).Column("PESPES_ORGAOEMISSOR");

            Map(x => x.EmpresaQueTrabalha).Column("PESPES_EMPRESA_TRABALHA");
            Map(x => x.EsporteFavorito).Column("PESPES_ESPORTE_FAVORITO");
            Map(x => x.EstadoCivil).Column("PESPES_ESTADO_CIVIL");
            Map(x => x.Formacao).Column("PESPES_FORMACAO");
            Map(x => x.FuncaoExercida).Column("PESPES_FUNCAO_EXERCIDA");
            Map(x => x.GrauDeInstrucao).Column("PESPES_GRAU_INSTRUCAO").CustomType<EnumGrauDeInstrucao>();
            Map(x => x.Hobby).Column("PESPES_HOBBY");
            Map(x => x.Nacionalidade).Column("PESPES_NACIONALIDADE");

            Map(x => x.NomeDaMae).Column("PESPES_NOME_MAE");
            Map(x => x.NomeDoPai).Column("PESPES_NOME_PAI");
            Map(x => x.PossuiResidenciaPropria).Column("PESPES_RESIDENCIA_PROPRIA");
            Map(x => x.TempoDeResidencia).Column("PESPES_TEMPO_RESIDENCIA");
            Map(x => x.RendaComprovada).Column("PESPES_RENDA_COMPROVADA");
            Map(x => x.RendaFamiliar).Column("PESPES_RENDA_FAMILIAR");

            Map(x => x.Sexo).Column("PESPES_SEXO");
            Map(x => x.TimeFavorito).Column("PESPES_TIME_FAVORITO");

            Map(x => x.IdEstrangeiro).Column("PESPES_ESTRANGEIRO_ID");

            References(x => x.Naturalidade).Column("PESPES_NATURALIDADE_CIDADE_ID").Not.LazyLoad().Fetch.Join();
        }
    }
}
