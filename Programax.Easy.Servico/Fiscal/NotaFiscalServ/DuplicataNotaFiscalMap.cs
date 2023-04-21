using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Fiscal.NotaFiscalServ
{
    public class DuplicataNotaFiscalMap: MapeamentoBase<DuplicataNotaFiscal>
    {
        public DuplicataNotaFiscalMap()
        {
            Table("NOTAS_FISCAIS_DUPLICATAS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("DUPLICATA_ID");

            Map(duplicata => duplicata.DataVencimento).Column("DUPLICATA_DATA_VENCIMENTO");
            Map(duplicata => duplicata.NumeroDuplicata).Column("DUPLICATA_NUMERO");
            Map(duplicata => duplicata.Parcela).Column("DUPLICATA_PARCELA");
            Map(duplicata => duplicata.ValorDuplicata).Column("DUPLICATA_VALOR");
            Map(duplicata => duplicata.CondicaoPagamento).Column("DUPLICATA_CONDICAO_PAGAMENTO");
            Map(duplicata => duplicata.FormaPagamento).Column("DUPLICATA_FORMA_PAGAMENTO").CustomType<EnumTipoFormaPagamento>();

            References(duplicata => duplicata.NotaFiscal).Column("DUPLICATA_NOTA_FISCAL_ID");
        }
    }
}
