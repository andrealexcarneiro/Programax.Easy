
using System.Linq;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Report.RelatoriosDevExpress.Vendas
{
    public partial class RelatorioPedidoVendaReduzido : RelatorioBaseDev
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _numeroPedido;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioPedidoVendaReduzido(int numeroPedido)
        {
            InitializeComponent();

            _numeroPedido = numeroPedido;

            _tituloRelatorio = "ORÇAMENTO / PEDIDO DE VENDA";
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
            var pedido = servicoPedidoDeVenda.Consulte(_numeroPedido);

            PedidoDeVendaRelatorioReduzido pedidoDeVendaRelatorio = new PedidoDeVendaRelatorioReduzido();

            pedidoDeVendaRelatorio.NumeroDocumento = pedido.Id;
            pedidoDeVendaRelatorio.TipoDocumento = pedido.TipoPedidoVenda.Descricao();

            if (pedido.Cliente != null)
            {
                pedidoDeVendaRelatorio.CpfCnpjCliente = pedido.Cliente.DadosGerais.CpfCnpj;
                pedidoDeVendaRelatorio.NomeCliente = pedido.Cliente.Id + " - " + pedido.Cliente.DadosGerais.NomeFantasia;
                pedidoDeVendaRelatorio.Contato = pedido.Cliente.EmpresaPessoa != null ? pedido.Cliente.EmpresaPessoa.NomeContato1 : string.Empty;
                pedidoDeVendaRelatorio.Email = pedido.Cliente.EmpresaPessoa != null ? pedido.Cliente.EmpresaPessoa.EmailPrincipal : string.Empty;

                var telefone = pedido.Cliente.ListaDeTelefones.FirstOrDefault();

                if (telefone != null)
                {
                    pedidoDeVendaRelatorio.Telefone = "(" + telefone.Ddd + ") " + telefone.Numero;
                }
            }

            if (pedido.EnderecoPedidoDeVenda != null)
            {
                pedidoDeVendaRelatorio.Endereco = pedido.EnderecoPedidoDeVenda.Rua;
                pedidoDeVendaRelatorio.Numero = pedido.EnderecoPedidoDeVenda.Numero;
                pedidoDeVendaRelatorio.Complemento = pedido.EnderecoPedidoDeVenda.Complemento;
                pedidoDeVendaRelatorio.Bairro = pedido.EnderecoPedidoDeVenda.Bairro;
                pedidoDeVendaRelatorio.Cidade = pedido.EnderecoPedidoDeVenda.Cidade.Descricao;
                pedidoDeVendaRelatorio.Estado = pedido.EnderecoPedidoDeVenda.Cidade.Estado != null ? pedido.EnderecoPedidoDeVenda.Cidade.Estado.UF : string.Empty;
                pedidoDeVendaRelatorio.Cep = pedido.EnderecoPedidoDeVenda.CEP;
            }

            pedidoDeVendaRelatorio.Atendente = pedido.Atendente != null ? pedido.Atendente.Id + " - " + pedido.Atendente.DadosGerais.NomeFantasia : string.Empty;
            pedidoDeVendaRelatorio.Vendedor = pedido.Vendedor != null ? pedido.Vendedor.Id + " - " + pedido.Vendedor.DadosGerais.NomeFantasia : string.Empty;
            pedidoDeVendaRelatorio.Transportadora = pedido.Transportadora != null ? pedido.Transportadora.Id + " - " + pedido.Transportadora.DadosGerais.NomeFantasia : string.Empty;

            pedidoDeVendaRelatorio.TipoFrete = pedido.TipoFrete.Descricao();

            pedidoDeVendaRelatorio.DataElaboracao = pedido.DataElaboracao.ToString("dd/MM/yyyy");
            pedidoDeVendaRelatorio.DataFechamento = pedido.DataFechamento != null ? pedido.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;

            pedidoDeVendaRelatorio.CondicaoPagamento = pedido.CondicaoPagamento != null ? pedido.CondicaoPagamento.Descricao : string.Empty;

            pedidoDeVendaRelatorio.FormaPagamento = pedido.FormaPagamento != null ? pedido.FormaPagamento.Descricao : string.Empty;

            pedidoDeVendaRelatorio.Situacao = pedido.StatusPedidoVenda.Descricao();

            pedidoDeVendaRelatorio.Observacoes = pedido.ObservacoesGeraisVenda;

            pedidoDeVendaRelatorio.Frete = pedido.ValorFrete.ToString("#,###,##0.00");

            pedidoDeVendaRelatorio.Total = pedido.ValorTotal.ToString("#,###,##0.00");

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
               

                pedidoDeVendaRelatorio.ListaParcelas.Add(itemParcela);
            }
            
            pedidoDeVendaRelatorio.SubTotal = subTotal.ToString("#,###,##0.00");

            pedidoDeVendaRelatorio.Desconto = totalDesconto.ToString("#,###,##0.00");
            pedidoDeVendaRelatorio.IcmsSTeIPI = totalIcmsSTEIPI.ToString("#,###,##0.00");
            pedidoDeVendaRelatorio.PesoBrutoTotal = pesoBrutoTotal.ToString("#,###,##0.00");
            pedidoDeVendaRelatorio.QuantidadeTotal = QuantidadeTotal.ToString();

            List<PedidoDeVendaRelatorioReduzido> listaPedidos = new List<PedidoDeVendaRelatorioReduzido>();
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

        #region " CLASSES AUXILIARES "

        public class PedidoDeVendaRelatorioReduzido
        {
            public PedidoDeVendaRelatorioReduzido()
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
        }

        public class ItemVenda
        {
            public int CodigoProduto { get; set; }

            public string DescricaoProduto { get; set; }

            public string Unidade { get; set; }

            public string Marca { get; set; }
            
            public string Quantidade { get; set; }

            public string ValorUnitario { get; set; }

            public string Desconto { get; set; }

            public string ValorTotal { get; set; }
        }

        public class ItemParcela
        {
            public string DataVencimento { get; set; }

            public string ValorParcela { get; set; }
        }

        #endregion
    }
}
