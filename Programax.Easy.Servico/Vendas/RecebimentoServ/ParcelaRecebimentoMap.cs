using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Vendas.RecebimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;

namespace Programax.Easy.Servico.Vendas.RecebimentoServ
{
    public class ParcelaRecebimentoMap : MapeamentoBase<ParcelaRecebimento>
    {
        public ParcelaRecebimentoMap()
        {
            Table("VW_PARCELAS_RECEBIMENTO");

            CompositeId()
                        .KeyProperty(x => x.Id, "PARCELA_ID")
                        .KeyProperty(x => x.TipoDocumentoRecebimento, "PARCELA_TIPO_DOCUMENTO").CustomType<EnumTipoDocumentoRecebimento>();

            Map(item => item.DataVencimento).Column("PARCELA_DATA_VENCIMENTO");
            Map(item => item.NumeroDocumento).Column("PARCELA_NUMERO_DOCUMENTO");
            Map(item => item.NumeroParcela).Column("PARCELA_NUMERO_PARCELA");
            Map(item => item.Valor).Column("PARCELA_VALOR");
            Map(item => item.CondicaoPagamentoId).Column("PARCELA_CONDICAO_PAGAMENTO_ID");
            Map(item => item.FormaPagamentoId).Column("PARCELA_FORMA_PAGAMENTO_ID");
            Map(item => item.TipoFormaPagamento).Column("PARCELA_TIPO_FORMA_PAGAMENTO").CustomType<EnumTipoFormaPagamento>();

            References(item => item.OperadorasCartao).Column("PARCELA_OPERADORA_ID");

            References(item => item.Recebimento).Columns("PARCELA_RECEBIMENTO_ID", "PARCELA_TIPO_DOCUMENTO");
        }
    }
}
