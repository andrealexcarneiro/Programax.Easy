using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using System;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio
{
    [Serializable]
    public class ContabilFiscalProduto
    {
        public virtual Ncm Ncm { get; set; }

        public virtual EnumNaturezaProduto NaturezaProduto { get; set; }

        public virtual EnumSituacaoTributariaProduto? SituacaoTributariaProduto { get; set; }

        public virtual double? Icms { get; set; }

        public virtual string CodigoGtin { get; set; }

        public virtual EnumOrigem OrigemProduto { get; set; }

        public virtual GrupoTributacaoIcms GrupoTributacaoIcms { get; set; }

        public virtual GrupoTributacaoFederal GrupoTributacaoFederal { get; set; }
    }
}
