using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.TeleMarketing.Enumeradores;

namespace Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio
{
    [Serializable]
    public class PedidoDeVenda : ObjetoDeNegocioBase
    {
        public PedidoDeVenda()
        {
            EnderecoPedidoDeVenda = new EnderecoPedidoDeVenda();
            ListaItens = new List<ItemPedidoDeVenda>();
            ListaParcelasPedidoDeVenda = new List<ParcelaPedidoDeVenda>();
        }

        public virtual Pessoa Cliente { get; set; }

        public virtual Pessoa Usuario { get; set; }

        public virtual DateTime DataElaboracao { get; set; }
        public virtual string DataElaboracaoII { get; set; }

        public virtual EnumTipoPedidoDeVenda? TipoPedidoVenda { get; set; }

        public virtual EnumStatusPedidoDeVenda StatusPedidoVenda { get; set; }

        public virtual EnderecoPedidoDeVenda EnderecoPedidoDeVenda { get; set; }

        public virtual Pessoa Indicador { get; set; }

        public virtual Pessoa Atendente { get; set; }

        public virtual Pessoa Vendedor { get; set; }

        public virtual Pessoa Supervisor { get; set; }

        public virtual NaturezaOperacao NaturezaOperacao { get; set; }

        public virtual TabelaPreco TabelaPreco { get; set; }

        public virtual FormaPagamento FormaPagamento { get; set; }

        public virtual CondicaoPagamento CondicaoPagamento { get; set; }

        public virtual Pessoa Transportadora { get; set; }

        public virtual DateTime? DataPrevisaoEntrega { get; set; }

        public virtual DateTime? DataMontagem { get; set; }

        public virtual DateTime? DataDesmontagem { get; set; }

        public virtual string ObservacoesNotaFiscal { get; set; }

        public virtual string ObservacoesGeraisVenda { get; set; }
        public virtual string VendedorNome { get; set; }

        public virtual double ValorFrete { get; set; }

        public virtual int Volume { get; set; }

        public virtual double Desconto { get; set; }

        public virtual double ValorIcmsST { get; set; }

        public virtual double ValorTotal { get; set; }
        public virtual string ValorTotalII { get; set; }

        public virtual bool DescontoEhPercentual { get; set; }

        public virtual double LimiteDeCredito { get; set; }

        public virtual double SaldoDisponivel { get; set; }

        public virtual double AReceberAberto { get; set; }

        public virtual double MaiorCompra { get; set; }

        public virtual EnumTipoFrete TipoFrete { get; set; }

        public virtual MotivoBloqueio MotivoBloqueio { get; set; }

        public virtual IList<ItemPedidoDeVenda> ListaItens { get; set; }

        public virtual IList<ParcelaPedidoDeVenda> ListaParcelasPedidoDeVenda { get; set; }

        public virtual DateTime? DataFechamento { get; set; }

        public virtual bool PedidoDoPdv { get; set; }

        public virtual bool PedidoExportadoParaPdvEcf { get; set; }

        public virtual EnumTipoCliente TipoCliente { get; set; }

        public virtual NotaFiscal NotaFiscal { get; set; }

        public virtual EnumStatusRoteiro? StatusRoteiro { get; set; }

        public virtual bool EstahPago { get; set; }
        public EnumStatusAtendimento StatusAtendimento { get; set; }
        public virtual int Carteira { get; set; }
    }
}
