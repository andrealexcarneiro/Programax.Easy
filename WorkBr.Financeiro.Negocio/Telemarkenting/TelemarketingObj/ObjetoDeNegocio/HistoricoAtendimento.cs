using System;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.TeleMarketing.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio
{
    [Serializable]
    public class HistoricoAtendimento : ObjetoDeNegocioBase
    {
        public virtual PedidoDeVenda Pedido { get; set; }

        public virtual PedidoDeVenda NovoPedido { get; set; }

        public virtual Pessoa Usuario { get; set; }
        
        public virtual string DescricaoHistorico { get; set; }      

        public virtual DateTime DataHistorico { get; set; }

        public virtual string TempoDuracao { get; set; }
        public virtual int contador { get; set; }

        public virtual EnumStatusAtendimento Status { get; set; }  
    }
    
}
