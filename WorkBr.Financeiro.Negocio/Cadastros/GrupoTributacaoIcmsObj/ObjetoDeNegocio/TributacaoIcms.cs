using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;

namespace Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio
{
    [Serializable]
    public class TributacaoIcms : ObjetoDeNegocioBase
    {
        public virtual EnumTipoSaidaTributacaoIcms TipoSaida { get; set; }

        public virtual EnumTipoCliente TipoCliente { get; set; }

        public virtual EnumTipoInscricaoICMS? TipoInscricaoICMS { get; set; }

        public virtual Cfop Cfop { get; set; }

        public virtual EnumCstCsosn? CstCsosn { get; set; }

        public virtual string EstadoDestino { get; set; }

        public virtual double? AliquotaCreditoST { get; set; }

        public virtual double? AliquotaDebitoST { get; set; }

        public virtual double? aliquotaSimplesNacional { get; set; }

        public virtual double? MVA { get; set; }

        public virtual double? PercentualMargemAdicST { get; set; }

        public virtual double? ReducaoBaseST { get; set; }

        public virtual double? IcmsBaseCalculo { get; set; }

        public virtual double? IcmsReducaoBaseCalculo { get; set; }

        public virtual EnumModBC? ModalidadeBaseCalculo { get; set; }

        public virtual double? AliquotaIcmsST { get; set; }

        public virtual EnumModBCST? ModalidadeIcmsST { get; set; }

        public double? baseDeCalculoIcmsST { get; set; }

        public double? baseDeCalculoIcms { get; set; }

        public virtual GrupoTributacaoIcms GrupoTributacaoIcms { get; set; }
    }
}
