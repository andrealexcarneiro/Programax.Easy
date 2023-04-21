using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Vendas.PedidoDeVendaServ
{
    public class VWVendaMap: MapeamentoBase<VWVenda>
    {
        public VWVendaMap()
        {
            Table("VW_VENDAS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("VENDA_ID");

            Map(pedido => pedido.ClienteId).Column("VENDA_CLIENTE_ID");
            Map(pedido => pedido.ClienteNome).Column("VENDA_CLIENTE_NOME");
            Map(pedido => pedido.ClienteCpfCnpj).Column("VENDA_CLIENTE_CPF_CNPJ");
            Map(pedido => pedido.TipoCliente).Column("VENDA_TIPO_CLIENTE").CustomType<EnumTipoPessoa>();

            Map(pedido => pedido.Cidade).Column("VENDA_CIDADE");
            Map(pedido => pedido.Bairro).Column("PEDIDO_BAIRRO_ENDERECO");
            Map(pedido => pedido.UF).Column("VENDA_UF");

            Map(pedido => pedido.IndicadorId).Column("VENDA_INDICADOR_ID");
            Map(pedido => pedido.IndicadorNome).Column("VENDA_INDICADOR_NOME");

            Map(pedido => pedido.AtendenteId).Column("VENDA_ATENDENTE_ID");
            Map(pedido => pedido.AtendenteNome).Column("VENDA_ATENDENTE_NOME");

            Map(pedido => pedido.VendedorId).Column("VENDA_VENDEDOR_ID");
            Map(pedido => pedido.VendedorNome).Column("VENDA_VENDEDOR_NOME");

            Map(pedido => pedido.SupervisorId).Column("VENDA_SUPERVISOR_ID");
            Map(pedido => pedido.SupervisorNome).Column("VENDA_SUPERVISOR_NOME");

            Map(pedido => pedido.TipoPedidoVenda).Column("VENDA_TIPO_PEDIDO").CustomType<EnumTipoPedidoDeVenda>();
            Map(pedido => pedido.Status).Column("VENDA_STATUS").CustomType<EnumStatusPedidoDeVenda>();

            Map(pedido => pedido.DataElaboracao).Column("VENDA_DATA_ELABORACAO");
            Map(pedido => pedido.DataFechamento).Column("VENDA_DATA_FECHAMENTO");

            Map(pedido => pedido.FormaPagamentoNome).Column("VENDA_FORMA_PAGAMENTO");
            Map(pedido => pedido.CondicaoPagamentoNome).Column("VENDA_CONDICAO_PAGAMENTO");

            Map(pedido => pedido.VendaJahExportadaPdvEcf).Column("VENDA_JAH_EXPORTADA_PDV_ECF");

            Map(pedido => pedido.ValorTotal).Column("VENDA_VALOR_TOTAL");

            Map(pedido => pedido.ComissaoIndicador).Column("VENDA_COMISSAO_INDICADOR");
            Map(pedido => pedido.ComissaoAtendente).Column("VENDA_COMISSAO_ATENDENTE");
            Map(pedido => pedido.ComissaoVendedor).Column("VENDA_COMISSAO_VENDEDOR");
            Map(pedido => pedido.ComissaoSupervisor).Column("VENDA_COMISSAO_SUPERVISOR");

            Map(pedido => pedido.PedidoEstahPago).Column("VENDA_ESTAH_PAGO");
        }
    }
}
