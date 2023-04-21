using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio
{
    [Serializable]
    public class Tmk : ObjetoDeNegocioBase
    {
        public virtual DateTime DataCompra { get; set; }

        public virtual Int64 NumPedido { get; set; }

        public virtual Int64 ClienteId { get; set; }
        public string DescricaoCliente { get; set; }
        public virtual Int64 status { get; set; }
        public virtual Int64 NumPedidoNovo { get; set; }
        public virtual DateTime Agendamento { get; set; }
    }
}
