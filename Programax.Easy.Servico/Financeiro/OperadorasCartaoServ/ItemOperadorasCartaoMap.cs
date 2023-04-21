using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Financeiro.OperadorasCartaoServ
{
    public class ItemOperadorasCartaoMap : MapeamentoBase<ItemOperadorasCartao>
    {
        public ItemOperadorasCartaoMap()
        {
            Table("ITEMOPERADORASCARTAO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ITEM_OPER_ID");

            Map(itemOperadora => itemOperadora.CobrarApartirDaParcela).Column("ITEM_OPER_COBRAR_APARTIR_DA_PARCELA");
            Map(itemOperadora => itemOperadora.Taxa).Column("ITEM_OPER_TAXA");
            Map(itemOperadora => itemOperadora.OperadorasId).Column("ITEM_OPERADORA_ID");
            
            References(itemOperadora => itemOperadora.CondicaoPagamento).Column("ITEM_OPER_CONDICAO_PGTO_ID").Not.LazyLoad();
        }
    }
}
