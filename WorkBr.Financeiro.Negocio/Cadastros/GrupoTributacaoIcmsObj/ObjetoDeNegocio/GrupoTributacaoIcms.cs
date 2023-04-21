using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;

namespace Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio
{
    [Serializable]
    public class GrupoTributacaoIcms : ObjetoDeNegocioBase
    {
        public GrupoTributacaoIcms()
        {
            ListaTributacoesIcms = new List<TributacaoIcms>();
        }

        public virtual string Descricao { get; set; }

        public virtual EnumCodigoRegimeTributario RegimeTributario { get; set; }

        public virtual EnumNaturezaProduto NaturezaProduto { get; set; }

        public virtual IList<TributacaoIcms> ListaTributacoesIcms { get; set; }
    }
}
