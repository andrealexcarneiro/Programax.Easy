using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class PisNotaFiscal : ObjetoDeNegocioBase
    {
        public virtual string EstadoDestino { get; set; }
    
        public virtual EnumTipoSaidaTributacaoIcms TipoSaida { get; set; }

        public virtual EnumTipoCliente TipoCliente { get; set; }

        public virtual double? BaseDeCalculo { get; set; }

        public virtual double? BaseDeCalculoST { get; set; }

        public virtual double? QuantidadeVendida { get; set; }

        public virtual double? QuantidadeVendidaST { get; set; }

        public virtual double? AliquotaPercentual { get; set; }

        public virtual double? AliquotaReais { get; set; }

        public virtual double? AliquotaPercentualST { get; set; }

        public virtual double? AliquotaReaisST { get; set; }

        public virtual double? ValorPis { get; set; }

        public virtual double? ValorPisST { get; set; }

        public virtual EnumCstPis? CstPis { get; set; }

        public virtual GrupoTributacaoFederal GrupoTributacaoFederal { get; set; }
    }
}
