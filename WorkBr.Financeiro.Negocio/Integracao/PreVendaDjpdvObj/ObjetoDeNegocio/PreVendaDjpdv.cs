using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Integracao.Enumeradores;

namespace Programax.Easy.Negocio.Integracao.PreVendaDjpdvObj.ObjetoDeNegocio
{
    public class PreVendaDjpdv : ObjetoDeNegocioBase
    {
        public virtual int PedidoDeVendaId { get; set; }
    }
}
