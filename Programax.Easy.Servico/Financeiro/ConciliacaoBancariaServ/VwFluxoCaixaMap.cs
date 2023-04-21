using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;


namespace Programax.Easy.Servico.Financeiro.ConciliacaoBancariaServ
{
    public class VwFluxoCaixaMap : MapeamentoBase<vw_fluxo_caixa>
    {
        public VwFluxoCaixaMap()
        {
            Table("vw_relatorio_fluxo_caixa");

            Id(x => x.Id2).GeneratedBy.Native().UnsavedValue(0).Column("ID");

           // Map(fluxo => fluxo.GRUPOID).Column("GRUPO_ID");
            //Map(fluxo => fluxo.NOMEGRUPO).Column("NOME_GRUPO");

            Map(fluxo => fluxo.CATEGORIAID).Column("CATEGORIA_ID");
            Map(fluxo => fluxo.NOMECATEGORIA).Column("NOME_CATEGORIA");
                                   
            Map(fluxo => fluxo.DATAREALIZADO).Column("DATA_REALIZADO");
            Map(fluxo => fluxo.VALOR).Column("VALOR");

            //Map(fluxo => fluxo.CAIXABANCOID).Column("CAIXA_BANCO_ID");
            //Map(fluxo => fluxo.NOMECAIXABANCO).Column("NOME_CAIXA_BANCO");

            Map(fluxo => fluxo.TIPOMOVIMENTACAO).Column("TIPO_MOVIMENTACAO");
            Map(fluxo => fluxo.ORIGEM).Column("ORIGEM");
        }
    }
}
