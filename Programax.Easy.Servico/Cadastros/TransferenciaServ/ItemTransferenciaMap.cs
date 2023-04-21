using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.TransferenciaServ
{
    public class ItemTransferenciaMap: MapeamentoBase<ItemTransferencia>
    {
        public ItemTransferenciaMap()
        {
            Table("SUBESTOQUEITENS");

            Id(transferencia => transferencia.Id).Column("SUBESTOQUEITENS_ID");

            Map(ItemTransferencia => ItemTransferencia.QuantidadeEstoque).Column("SUBESTOQUEITENS_QUANT");
            Map(ItemTransferencia => ItemTransferencia.DataCadastro).Column("SUBESTOQUEITENS_DATACADASTRO");
            Map(ItemTransferencia => ItemTransferencia.produto).Column("SUBESTOQUEITENS_PRODID");
            Map(ItemTransferencia => ItemTransferencia.SubEstoque).Column("SUBESTOQUEITENS_SUB");
            Map(ItemTransferencia => ItemTransferencia.Descricao).Column("SUBESTOQUEITENS_DESCRICAO");
            Map(ItemTransferencia => ItemTransferencia.Unidade).Column("SUBESTOQUEITENS_UNIDADE");

        }
    }
}
