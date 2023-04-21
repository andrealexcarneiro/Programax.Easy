using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Integracao.PreVendaDjpdvObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Integracao.Enumeradores;

namespace Programax.Easy.Servico.Integracao.PreVendaDjpdvServ
{
    public class PreVendaDjpdvMap : MapeamentoBase<PreVendaDjpdv>
    {
        public PreVendaDjpdvMap()
        {
            Table("PreVendaDjpdv");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PREVENDA_ID");

            Map(prevendaDj => prevendaDj.PedidoDeVendaId).Column("PREVENDA_PEDIDO_ID");
        }
    }
}
