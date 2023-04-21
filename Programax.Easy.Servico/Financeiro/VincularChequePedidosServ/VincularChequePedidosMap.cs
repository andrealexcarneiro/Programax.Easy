using Programax.Easy.Negocio.Financeiro.VincularChequePedidosObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Financeiro.VincularChequePedidosServ
{
    public class VincularChequePedidosMap : MapeamentoBase<VincularChequePedidos>
    {
        public VincularChequePedidosMap()
        {
            Table("VINCULARCHEQUEPEDIDOS");
            
            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("VINC_ID");
            
            Map(vinculo => vinculo.NumeroPedidos).Column("VINC_PEDIDO_ID");
            
            References(vinculo => vinculo.Cheque).Column("VINC_CHEQUE_ID");
            References(vinculo => vinculo.Pessoa).Column("VINC_PESSOA_ID");
        }
    }
}
