using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.TrocaPedidoDeVendaServ;

namespace Programax.Easy.Report.RelatoriosDevExpress.Vendas
{
    public partial class RelatorioTrocaDeVendas : RelatorioBaseDev
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _numeroTroca;
        private Parametros _parametros;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioTrocaDeVendas(int numeroTroca)
        {
            InitializeComponent();

            CarregueParametros();

            _numeroTroca = numeroTroca;

            _tituloRelatorio = "TROCA DE VENDAS";
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            ServicoTrocaPedidoDeVenda servicoTrocaPedidoDeVenda = new ServicoTrocaPedidoDeVenda();
            var pedido = servicoTrocaPedidoDeVenda.Consulte(_numeroTroca);

            PedidoDeVendaRelatorio pedidoDeVendaRelatorio = new PedidoDeVendaRelatorio();

            pedidoDeVendaRelatorio.NumeroDocumento = pedido.Id;
            pedidoDeVendaRelatorio.TipoDocumento = "TROCA DE VENDA";

            if (pedido.PedidoDeVenda.Cliente != null)
            {
                pedidoDeVendaRelatorio.CpfCnpjCliente = pedido.PedidoDeVenda.Cliente.DadosGerais.CpfCnpj;
                pedidoDeVendaRelatorio.NomeCliente = pedido.PedidoDeVenda.Cliente.Id + " - " + pedido.PedidoDeVenda.Cliente.DadosGerais.NomeFantasia;
                pedidoDeVendaRelatorio.Contato = pedido.PedidoDeVenda.Cliente.EmpresaPessoa != null ? pedido.PedidoDeVenda.Cliente.EmpresaPessoa.NomeContato1 : string.Empty;
                pedidoDeVendaRelatorio.Email = pedido.PedidoDeVenda.Cliente.EmpresaPessoa != null ? pedido.PedidoDeVenda.Cliente.EmpresaPessoa.EmailPrincipal : string.Empty;

                var telefone = pedido.PedidoDeVenda.Cliente.ListaDeTelefones.FirstOrDefault();

                if (telefone != null)
                {
                    pedidoDeVendaRelatorio.Telefone = "(" + telefone.Ddd + ") " + telefone.Numero;
                }
            }

            if (pedido.PedidoDeVenda.EnderecoPedidoDeVenda != null)
            {
                pedidoDeVendaRelatorio.Endereco = pedido.PedidoDeVenda.EnderecoPedidoDeVenda.Rua;
                pedidoDeVendaRelatorio.Numero = pedido.PedidoDeVenda.EnderecoPedidoDeVenda.Numero;
                pedidoDeVendaRelatorio.Complemento = pedido.PedidoDeVenda.EnderecoPedidoDeVenda.Complemento;
                pedidoDeVendaRelatorio.Bairro = pedido.PedidoDeVenda.EnderecoPedidoDeVenda.Bairro;
                pedidoDeVendaRelatorio.Cidade = pedido.PedidoDeVenda.EnderecoPedidoDeVenda.Cidade.Descricao;
                pedidoDeVendaRelatorio.Estado = pedido.PedidoDeVenda.EnderecoPedidoDeVenda.Cidade.Estado != null ? pedido.PedidoDeVenda.EnderecoPedidoDeVenda.Cidade.Estado.UF : string.Empty;
                pedidoDeVendaRelatorio.Cep = pedido.PedidoDeVenda.EnderecoPedidoDeVenda.CEP;
            }

            pedidoDeVendaRelatorio.Atendente = pedido.PedidoDeVenda.Atendente != null ? pedido.PedidoDeVenda.Atendente.Id + " - " + pedido.PedidoDeVenda.Atendente.DadosGerais.NomeFantasia : string.Empty;
            pedidoDeVendaRelatorio.Vendedor = pedido.PedidoDeVenda.Vendedor != null ? pedido.PedidoDeVenda.Vendedor.Id + " - " + pedido.PedidoDeVenda.Vendedor.DadosGerais.NomeFantasia : string.Empty;
            pedidoDeVendaRelatorio.Transportadora = pedido.PedidoDeVenda.Transportadora != null ? pedido.PedidoDeVenda.Transportadora.Id + " - " + pedido.PedidoDeVenda.Transportadora.DadosGerais.NomeFantasia : string.Empty;

            pedidoDeVendaRelatorio.TipoFrete = pedido.PedidoDeVenda.TipoFrete.Descricao();

            pedidoDeVendaRelatorio.DataElaboracao = pedido.DataElaboracao.ToString("dd/MM/yyyy");
            pedidoDeVendaRelatorio.DataFechamento = pedido.DataFechamento != null ? pedido.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;

            pedidoDeVendaRelatorio.CondicaoPagamento = pedido.PedidoDeVenda.CondicaoPagamento != null ? pedido.PedidoDeVenda.CondicaoPagamento.Descricao : string.Empty;

            pedidoDeVendaRelatorio.FormaPagamento = pedido.PedidoDeVenda.FormaPagamento != null ? pedido.FormaPagamento.Descricao : string.Empty;

            pedidoDeVendaRelatorio.Situacao = pedido.PedidoDeVenda.StatusPedidoVenda.Descricao();

            pedidoDeVendaRelatorio.Observacoes = pedido.PedidoDeVenda.ObservacoesGeraisVenda;

            if(!string.IsNullOrEmpty(pedido.PedidoDeVenda.DataPrevisaoEntrega.ToString()))
            {
                lblDataPrevisaoEntrega.Visible = true;
                txtDataPrevisaoEntrega.Visible = true;
                pedidoDeVendaRelatorio.DataPrevisaoEntrega = pedido.PedidoDeVenda.DataPrevisaoEntrega.Value.ToString("dd/MM/yyyy");
            }
           
            pedidoDeVendaRelatorio.Frete = pedido.PedidoDeVenda.ValorFrete.ToString("#,###,##0.00");

            pedidoDeVendaRelatorio.Total = pedido.PedidoDeVenda.ValorTotal.ToString("#,###,##0.00");

            double subTotal = 0;
            double totalDesconto = 0;
            double totalIcmsSTEIPI = 0;
            double pesoBrutoTotal = 0;
            double QuantidadeTotal = 0;

            foreach (var item in pedido.ListaItens)
            {
                ItemVenda itemVenda = new ItemVenda();

                itemVenda.CodigoProduto = item.Produto.Id;
                itemVenda.DescricaoProduto = item.Produto.DadosGerais.Descricao;
                itemVenda.Marca = item.Produto.Principal != null && item.Produto.Principal.Marca != null ? item.Produto.Principal.Marca.Descricao : string.Empty;
                itemVenda.Cor = item.Produto.Vestuario != null && item.Produto.Vestuario.Cor != null ? item.Produto.Vestuario.Cor.Descricao : string.Empty;
                itemVenda.Unidade = item.Produto.DadosGerais.Unidade != null ? item.Produto.DadosGerais.Unidade.Abreviacao : string.Empty;

                itemVenda.Quantidade = item.Quantidade.ToString();
                QuantidadeTotal += item.Quantidade;
                itemVenda.ValorUnitario = item.ValorUnitario.ToString("#,###,##0.00");
                itemVenda.Desconto = item.TotalDesconto.ToString("#,###,##0.00");
                itemVenda.ValorTotal = (item.Quantidade * item.ValorUnitario - item.TotalDesconto).ToString("#,###,##0.00");

                subTotal += item.ValorUnitario * item.Quantidade;
                totalDesconto += item.TotalDesconto;
                totalIcmsSTEIPI += item.ValorIcmsST.GetValueOrDefault();

                //Calcula o peso bruto total, caso o produto não seja nulo
                if (item.Produto.Principal.PesoBruto != null)
                    pesoBrutoTotal += (item.Produto.Principal.PesoBruto.ToDouble() * item.Quantidade.ToDouble());

                pedidoDeVendaRelatorio.ListaItensVenda.Add(itemVenda);
            }
            
            foreach (var item in pedido.ListaParcelasPedidoDeVenda)
            {
                ItemParcela itemParcela = new ItemParcela();

                itemParcela.DataVencimento = item.DataVencimento.ToString("dd/MM/yyyy");
                itemParcela.ValorParcela = item.Valor.ToString("#,###,##0.00");
                itemParcela.ItemFormaPagamento = item.FormaPagamento.Descricao;

                pedidoDeVendaRelatorio.ListaParcelas.Add(itemParcela);
            }
            
            pedidoDeVendaRelatorio.SubTotal = subTotal.ToString("#,###,##0.00");

            pedidoDeVendaRelatorio.Desconto = totalDesconto.ToString("#,###,##0.00");
            pedidoDeVendaRelatorio.IcmsSTeIPI = totalIcmsSTEIPI.ToString("#,###,##0.00");
            pedidoDeVendaRelatorio.PesoBrutoTotal = pesoBrutoTotal.ToString("#,###,##0.00");
            pedidoDeVendaRelatorio.QuantidadeTotal = QuantidadeTotal.ToString();

            List<PedidoDeVendaRelatorio> listaPedidos = new List<PedidoDeVendaRelatorio>();
            listaPedidos.Add(pedidoDeVendaRelatorio);

            switch (pedido.TipoPedidoVenda)
            {
                case Negocio.Vendas.Enumeradores.EnumTipoPedidoDeVenda.ORCAMENTO:
                    lblTituloRelatorio.Text = "ORÇAMENTO";
                    break;
                case Negocio.Vendas.Enumeradores.EnumTipoPedidoDeVenda.PEDIDOVENDA:
                    lblTituloRelatorio.Text = "PEDIDO DE VENDA";
                    break;

                case Negocio.Vendas.Enumeradores.EnumTipoPedidoDeVenda.PERMUTA:
                    lblTituloRelatorio.Text = "PERMUTA";
                    break;

                case Negocio.Vendas.Enumeradores.EnumTipoPedidoDeVenda.CONSIGNADO:
                    lblTituloRelatorio.Text = "CONSIGNADO";
                    break;
            }

           

            ConteudoRelatorio.DataSource = listaPedidos;
        }

        #endregion

        #region "Métodos Auxiliares"

        private void CarregueParametros()
        {
            ServicoParametros servicoParametros = new ServicoParametros();

            _parametros = servicoParametros.ConsulteParametros();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        public class PedidoDeVendaRelatorio
        {
            public PedidoDeVendaRelatorio()
            {
                ListaItensVenda = new List<ItemVenda>();
                ListaParcelas = new List<ItemParcela>();
            }

            public int NumeroDocumento { get; set; }

            public string Atendente { get; set; }

            public string Vendedor { get; set; }

            public string DataElaboracao { get; set; }

            public string DataFechamento { get; set; }

            public string Situacao { get; set; }

            public string FormaPagamento { get; set; }

            public string CondicaoPagamento { get; set; }

            public string TipoFrete { get; set; }

            public string Transportadora { get; set; }

            public string SubTotal { get; set; }

            public string Desconto { get; set; }

            public string Frete { get; set; }

            public string IcmsSTeIPI { get; set; }

            public string Total { get; set; }

            public string Observacoes { get; set; }

            public string CpfCnpjCliente { get; set; }

            public string NomeCliente { get; set; }

            public string Endereco { get; set; }

            public string Numero { get; set; }

            public string Complemento { get; set; }

            public string Bairro { get; set; }

            public string Cidade { get; set; }

            public string Estado { get; set; }

            public string Cep { get; set; }

            public string Telefone { get; set; }

            public string Contato { get; set; }

            public string Email { get; set; }

            public string TipoDocumento { get; set; }

            public List<ItemVenda> ListaItensVenda { get; set; }

            public List<ItemParcela> ListaParcelas { get; set; }

            public string PesoBrutoTotal { get; set; }

            public string QuantidadeTotal { get; set; }

            public string DataPrevisaoEntrega { get; set; }
        }

        public class ItemVenda
        {
            public int CodigoProduto { get; set; }

            public string DescricaoProduto { get; set; }

            public string Unidade { get; set; }

            public string Marca { get; set; }

            public string Cor { get; set; } 

            public string Quantidade { get; set; }

            public string ValorUnitario { get; set; }

            public string Desconto { get; set; }

            public string ValorTotal { get; set; }
        }

        public class ItemParcela
        {
            public string DataVencimento { get; set; }

            public string ValorParcela { get; set; }

            public string ItemFormaPagamento { get; set; }
        }

        #endregion
    }
}
