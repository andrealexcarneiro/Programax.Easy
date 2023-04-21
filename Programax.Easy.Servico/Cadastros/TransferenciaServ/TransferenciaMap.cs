using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.TransferenciaServ
{
    public class TransferenciaMap : MapeamentoBase<Transferencia>
    {
        public TransferenciaMap()
        {
            Table("INVENTARIOS");

            Id(inventario => inventario.Id).Column("INV_ID");

            Map(inventario => inventario.TipoInventario).Column("INV_TIPO_INVENTARIO").CustomType<EnumTipoInventario>();
            Map(inventario => inventario.Modalidade).Column("INV_MODALIDADE").CustomType<EnumModalidadeInventario>();
            Map(inventario => inventario.FiltroSituacaoSaldo).Column("INV_FILTRO_SITUACAO_SALDO").CustomType<EnumFiltroSituacao>();

            Map(inventario => inventario.DataInicio).Column("INV_DATA_INICIO");
            Map(inventario => inventario.DataFinal).Column("INV_DATA_FINAL");
            
            Map(inventario => inventario.BloquearProdutosMovimentacao).Column("INV_BLOQUEAR_PRODUTOS_MOVIMENTACAO");
            Map(inventario => inventario.UtilizarSaldoPrimeiraContagem).Column("INV_UTILIZAR_SALDO_PRIMEIRA_CONTAGEM");

            Map(inventario => inventario.ContagemAtual).Column("INV_CONTAGEM_ATUAL");
            Map(inventario => inventario.Status).Column("INV_STATUS").CustomType<EnumStatusInventario>();

            Map(inventario => inventario.DataInicioPrimeiraContagem).Column("INV_DATA_INICIO_PRIMEIRA_CONTAGEM");
            Map(inventario => inventario.DataInicioSegundaContagem).Column("INV_DATA_INICIO_SEGUNDA_CONTAGEM");
            Map(inventario => inventario.DataInicioTerceiraContagem).Column("INV_DATA_INICIO_TERCEIRA_CONTAGEM");

            References(inventario => inventario.Marca).Column("INV_MARCA_ID");
            References(inventario => inventario.Categoria).Column("INV_CATEGORIA_ID");
            References(inventario => inventario.Grupo).Column("INV_GRUPO_ID");
            References(inventario => inventario.SubGrupo).Column("INV_SUBGRUPO_ID");

            Map(inventario => inventario.OrdenacaoContagem).Column("INV_ORDENACAO_CONTAGEM").CustomType<EnumFiltroOrdenacaoContagem>();

            //HasMany(inventario => inventario.ListaDeItens).KeyColumn("IVT_INV_ID").Cascade.All().Inverse().AsBag();
        }
    }
}
