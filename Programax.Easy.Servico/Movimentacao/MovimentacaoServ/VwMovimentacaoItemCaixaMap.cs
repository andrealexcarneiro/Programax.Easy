using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using FluentNHibernate.Mapping;

namespace Programax.Easy.Servico.Movimentacao.MovimentacaoServ
{
    public class VwMovimentacaoItemCaixaMap : MapeamentoBase<VWMovimentacaoItemCaixa>
    {
        public VwMovimentacaoItemCaixaMap()
        {
            Table("VW_MOVIMENTACAO_ITEM_CAIXA");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("VWMOV_ID");

            Map(item => item.caixa).Column("VWMOV_CAIXA");
            Map(item => item.numeroDocumentoOrigem).Column("VWMOV_NUMERO_DOCUMENTO_ORIGEM");
            Map(item => item.dataFechamento).Column("VWMOV_DATA_FECHAMENTO_CAIXA");
            Map(item => item.id_Movimentacao).Column("VWMOV_ID_MOVIMENTACAO");
            Map(item => item.formaPagamento).Column("VWMOV_FORMA_PAGAMENTO");
            Map(item => item.valor).Column("VWMOV_VALOR");
        }
    }
}
