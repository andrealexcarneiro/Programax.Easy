using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.PessoaServ
{
    public class FuncionarioMap : MapeamentoBase<Funcionario>
    {
        public FuncionarioMap()
        {
            Table("PESSOASFUNCIONARIO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PESFUNC_ID");

            Map(x => x.DataDeAdmissao).Column("PESFUNC_DATA_ADMISSAO");
            Map(x => x.DataDeDemissao).Column("PESFUNC_DATA_DEMISSAO");

            Map(x => x.Salario).Column("PESFUNC_SALARIO");
            Map(x => x.MotivoRescisao).Column("PESFUNC_MOTIVO_RESCISAO");

            Map(x => x.Matricula).Column("PESFUNC_MATRICULA");
            Map(x => x.Cargo).Column("PESFUNC_CARGO");
            Map(x => x.Departamento).Column("PESFUNC_DEPARTAMENTO");
            Map(x => x.Pis).Column("PESFUNC_PIS");

            Map(x => x.NumeroCtps).Column("PESFUNC_CTPS_NUMERO");
            Map(x => x.SerieCtps).Column("PESFUNC_CTPS_SERIE");
            Map(x => x.UfCtps).Column("PESFUNC_CTPS_UF");
            Map(x => x.DataEmissaoCtps).Column("PESFUNC_CTPS_DATA_EMISSAO");

            Map(x => x.NumeroCertificadoMilitar).Column("PESFUNC_CERTIFICADO_MILITAR_NUMERO");
            Map(x => x.CategoriaCertificadoMilitar).Column("PESFUNC_CERTIFICADO_MILITAR_CATEGORIA");

            Map(x => x.NumeroTituloEleitor).Column("PESFUNC_TITULO_ELEITOR_NUMERO");
            Map(x => x.SerieTiuloEleitor).Column("PESFUNC_TITULO_ELEITOR_SERIE");
            Map(x => x.SecaoTituloEleitor).Column("PESFUNC_TITULO_ELEITOR_SECAO");

            Map(x => x.Agencia).Column("PESFUNC_BANCOAGENCIA");
            Map(x => x.Conta).Column("PESFUNC_BANCOCONTA");
            Map(x => x.Operacao).Column("PESFUNC_BANCOOPERACAO");

            References(x => x.Banco).Column("PESFUNC_BANC_ID").Not.LazyLoad().Fetch.Join();

            Map(x => x.NumeroCnh).Column("PESFUNC_CNH_NUMERO");
            Map(x => x.CategoriaCnh).Column("PESFUNC_CNH_CATEGORIA");
            Map(x => x.ValidadeCnh).Column("PESFUNC_CNH_VALIDADE");

            Map(x => x.ConselhoCarteiraConselho).Column("PESFUNC_CART_CONSELHO_CONSELHO");
            Map(x => x.NumeroCarteiraConselho).Column("PESFUNC_CART_CONSELHO_NUMERO");
            Map(x => x.DataInscricaoCarteiraConselho).Column("PESFUNC_CART_CONSELHO_DATA_INSCRICAO");
        }
    }
}
