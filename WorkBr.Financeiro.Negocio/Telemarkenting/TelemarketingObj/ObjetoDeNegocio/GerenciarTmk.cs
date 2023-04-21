using System;
using Programax.Easy.Negocio.TeleMarketing.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio
{
    [Serializable]
    public class GerenciarTmk : ObjetoDeNegocioBase
    {
        public virtual Int64 IdAtendimento { get; set; }

        public Int64 Status { get; set; }

        public virtual DateTime Data { get; set; }

        public virtual Int64 VendedorId { get; set; }

        public virtual string Duracao { get; set; }

        public virtual decimal ValorVenda { get; set; }

    }
}
