using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;

namespace Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio
{
    [Serializable]
    public class GrupoTributacaoFederal : ObjetoDeNegocioBase
    {
        public GrupoTributacaoFederal()
        {
            ListaCofins = new List<CofinsNotaFiscal>();
            ListaPis = new List<PisNotaFiscal>();
            ListaIpi = new List<IpiNotaFiscal>();
        }

        public virtual string Descricao { get; set; }

        public virtual EnumCodigoRegimeTributario RegimeTributario { get; set; }

        public virtual EnumNaturezaProduto NaturezaProduto { get; set; }

        public virtual IList<CofinsNotaFiscal> ListaCofins { get; set; }

        public virtual IList<PisNotaFiscal> ListaPis { get; set; }

        public virtual IList<IpiNotaFiscal> ListaIpi { get; set; }
    }
}
