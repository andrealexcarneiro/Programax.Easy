using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.Servico.Fiscal.NotaFiscalServ
{
    public class FormaPagamentoNotaFiscalMap : MapeamentoBase<FormaPagamentoNotaFiscal>
    {
        public FormaPagamentoNotaFiscalMap()
        {
            Table("NOTAS_FISCAIS_FORMA_PAGAMENTO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("FP_ID");

            Map(formaPagamento => formaPagamento.FormaPagamentoNfce).Column("FP_FORMA_PAGAMENTO_NFCE").CustomType<EnumFormaPagamentoNfce>();

            Map(formaPagamento => formaPagamento.ValorPagamento).Column("FP_VALOR_PAGAMENTO");

            References(formaPagamento => formaPagamento.NotaFiscal).Column("FP_NOTA_FISCAL_ID");

            Component(formaPagamento => formaPagamento.Cartao, cartao =>
            {
                cartao.Map(cart => cart.BandeiraCartao).Column("FP_BANDEIRA_CARTAO").CustomType<EnumBandeiraCartao>();
                cartao.Map(cart => cart.CnpjCredenciadoraCartao).Column("FP_CNPJ_CREDENCIADORA");
                cartao.Map(cart => cart.CodigoAutorizacaoOperacaoCartaoCreditoDebito).Column("FP_CODIGO_AUTORIZACAO");
                cartao.Map(cart => cart.TipoIntegracaoPagamento).Column("FP_TIPO_INTEGRACAO_PAGAMENTO").CustomType<EnumTipoIntegracaoPagamento>();
            });
        }
    }
}
