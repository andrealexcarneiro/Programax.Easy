using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.InventarioServ
{
    public class ItemInventarioMap: MapeamentoBase<ItemInventario>
    {
        public ItemInventarioMap()
        {
            Table("INVENTARIOSITENS");

            Id(inventario => inventario.Id).Column("IVT_ID");

            Map(itemInventario => itemInventario.QuantidadeEstoque).Column("IVT_QTD_ESTOQUE");
            Map(itemInventario => itemInventario.QuantidadeContagemUm).Column("IVT_QTD_CONTAGEM_UM");
            Map(itemInventario => itemInventario.QuantidadeContagemDois).Column("IVT_QTD_CONTAGEM_DOIS");
            Map(itemInventario => itemInventario.QuantidadeContagemTres).Column("IVT_QTD_CONTAGEM_TRES");

            References(itemInventario => itemInventario.Produto).Column("IVT_PROD_ID").Not.LazyLoad().Fetch.Join();
            References(itemInventario => itemInventario.Inventario).Column("IVT_INV_ID");
        }
    }
}
