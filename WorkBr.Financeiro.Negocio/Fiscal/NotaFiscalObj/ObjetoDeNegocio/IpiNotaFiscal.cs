using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class IpiNotaFiscal : ObjetoDeNegocioBase
    {
        public virtual string EstadoDestino { get; set; }
    
        public virtual EnumTipoSaidaTributacaoIcms TipoSaida { get; set; }

        public virtual EnumTipoCliente TipoCliente { get; set; }

        public virtual double? BaseDeCalculo { get; set; }

        public virtual double? AliquotaIpi { get; set; }

        public virtual double? ValorIpi { get; set; }

        public virtual EnumCstIpi? CstIpi { get; set; }

        public virtual int CodigoEnquadramento { get; set; }

        public virtual GrupoTributacaoFederal GrupoTributacaoFederal { get; set; }
    }
}
