using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class IcmsNotaFiscal
    {
        public virtual EnumOrigem Origem { get; set; }

        public virtual EnumCstCsosn CstCsosn{ get; set; }

        public virtual double? BaseCalculoIcms { get; set; }

        public virtual double? AliquotaIcms { get; set; }

        public virtual double? AliquotaReducaoIcms { get; set; }

        public virtual double? AliquotaReducaoIcmsSubstituicaoTributaria { get; set; }

        public virtual double? ValorIcms { get; set; }

        public virtual double? BaseIcmsSubstituicaoTributaria { get; set; }

        public virtual double? PercentualMargemValorAdicST { get; set; }

        public virtual double? AliquotaIva { get; set; }

        public virtual double? AliquotaSubstituicaoTributaria { get; set; }

        public virtual double? AliquotaDbSt { get; set; }

        public virtual double? AliquotaCrSt { get; set; }

        public virtual double? ValorSubstituicaoTributaria { get; set; }

        public virtual EnumModBC? ModBC { get; set; }

        public virtual EnumModBCST? ModBCST { get; set; }

        public virtual EnumMotivoDesoneracaoProduto? MotivoDesoneracaoProduto { get; set; }

        public virtual double? ValorDesoneracaoProduto { get; set; }

        public virtual double? AliquotaSimplesNacional { get; set; }

        public virtual double? ValorIcmsSimplesNacional { get; set; }
    }
}
