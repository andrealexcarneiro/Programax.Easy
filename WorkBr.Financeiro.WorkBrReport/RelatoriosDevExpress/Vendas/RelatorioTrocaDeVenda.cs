using System.Linq;
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

            _tituloRelatorio = "TROCA DE VENDA";
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            ServicoTrocaPedidoDeVenda servicoTrocaPedidoDeVenda = new ServicoTrocaPedidoDeVenda();
            var pedidoTroca = servicoTrocaPedidoDeVenda.Consulte(_numeroTroca);

            PedidoDeVendaRelatorio pedidoDeVendaRelatorio = new PedidoDeVendaRelatorio();

            pedidoDeVendaRelatorio.NumeroDocumento = pedidoTroca.Id;
            pedidoDeVendaRelatorio.TipoDocumento = "TROCA DE VENDA";

            if (pedidoTroca.PedidoDeVenda.Cliente != null)
            {
                pedidoDeVendaRelatorio.CpfCnpjCliente = pedidoTroca.PedidoDeVenda.Cliente.DadosGerais.CpfCnpj;
                pedidoDeVendaRelatorio.NomeCliente = pedidoTroca.PedidoDeVenda.Cliente.Id + " - " + pedidoTroca.PedidoDeVenda.Cliente.DadosGerais.NomeFantasia;
                pedidoDeVendaRelatorio.Contato = pedidoTroca.PedidoDeVenda.Cliente.EmpresaPessoa != null ? pedidoTroca.PedidoDeVenda.Cliente.EmpresaPessoa.NomeContato1 : string.Empty;
                pedidoDeVendaRelatorio.Email = pedidoTroca.PedidoDeVenda.Cliente.EmpresaPessoa != null ? pedidoTroca.PedidoDeVenda.Cliente.EmpresaPessoa.EmailPrincipal : string.Empty;

                var telefone = pedidoTroca.PedidoDeVenda.Cliente.ListaDeTelefones.FirstOrDefault();

                if (telefone != null)
                {
                    pedidoDeVendaRelatorio.Telefone = "(" + telefone.Ddd + ") " + telefone.Numero;
                }
            }

            if (pedidoTroca.PedidoDeVenda.EnderecoPedidoDeVenda != null)
            {
                pedidoDeVendaRelatorio.Endereco = pedidoTroca.PedidoDeVenda.EnderecoPedidoDeVenda.Rua;
                pedidoDeVendaRelatorio.Numero = pedidoTroca.PedidoDeVenda.EnderecoPedidoDeVenda.Numero;
                pedidoDeVendaRelatorio.Complemento = pedidoTroca.PedidoDeVenda.EnderecoPedidoDeVenda.Complemento;
                pedidoDeVendaRelatorio.Bairro = pedidoTroca.PedidoDeVenda.EnderecoPedidoDeVenda.Bairro;
                pedidoDeVendaRelatorio.Cidade = pedidoTroca.PedidoDeVenda.EnderecoPedidoDeVenda.Cidade.Descricao;
                pedidoDeVendaRelatorio.Estado = pedidoTroca.PedidoDeVenda.EnderecoPedidoDeVenda.Cidade.Estado != null ? pedidoTroca.PedidoDeVenda.EnderecoPedidoDeVenda.Cidade.Estado.UF : string.Empty;
                pedidoDeVendaRelatorio.Cep = pedidoTroca.PedidoDeVenda.EnderecoPedidoDeVenda.CEP;
            }

            pedidoDeVendaRelatorio.Atendente = pedidoTroca.UsuarioRealizouTroca.DadosGerais.Razao != null ? pedidoTroca.UsuarioRealizouTroca.DadosGerais.Razao : string.Empty;
                                   
            pedidoDeVendaRelatorio.DataElaboracao = pedidoTroca.DataElaboracao.ToString("dd/MM/yyyy");
            pedidoDeVendaRelatorio.DataFechamento = pedidoTroca.DataFechamento != null ? pedidoTroca.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;
            
            pedidoDeVendaRelatorio.FormaPagamento = pedidoTroca.FormaPagamento != null ? pedidoTroca.FormaPagamento.Descricao : string.Empty;

            pedidoDeVendaRelatorio.NumeroPedidoVenda = pedidoTroca.PedidoDeVenda.Id;

            pedidoDeVendaRelatorio.Situacao = pedidoTroca.Status.Descricao();

            pedidoDeVendaRelatorio.Observacoes = "TROCA DE VENDA FEITA ATRAVÉS DO PEDIDO DE NÚMERO:"+ pedidoDeVendaRelatorio.NumeroPedidoVenda;

            pedidoDeVendaRelatorio.Total = pedidoTroca.ValorTotalTroca.ToString("#,###,##0.00");

            foreach (var item in pedidoTroca.ListaItensPedido)
            {
                ItemVenda itemVenda = new ItemVenda();

                itemVenda.CodigoProduto = item.Produto.Id;
                itemVenda.DescricaoProduto = item.Produto.DadosGerais.Descricao;
                itemVenda.Marca = item.Produto.Principal != null && item.Produto.Principal.Marca != null ? item.Produto.Principal.Marca.Descricao : string.Empty;
                itemVenda.Cor = item.Produto.Vestuario != null && item.Produto.Vestuario.Cor != null ? item.Produto.Vestuario.Cor.Descricao : string.Empty;
                itemVenda.Unidade = item.Produto.DadosGerais.Unidade != null ? item.Produto.DadosGerais.Unidade.Abreviacao : string.Empty;

                itemVenda.Quantidade = item.Quantidade.ToString();
                
                itemVenda.ValorUnitario = item.ValorUnitario.ToString("#,###,##0.00");               
               
                itemVenda.QuantidadeTrocada = item.QuantidadeTrocar.ToString();

                itemVenda.Desconto = item.Desconto.ToString("#,###,##0.00");

                itemVenda.ValorTotal = item.ValorTotal.ToString("#,###,##0.00");

                pedidoDeVendaRelatorio.ListaItensVenda.Add(itemVenda);
            }


            foreach (var item in pedidoTroca.ListaItens)
            {
                ItemVenda itemVenda = new ItemVenda();

                itemVenda.CodigoProduto = item.Produto.Id;
                itemVenda.DescricaoProduto = item.Produto.DadosGerais.Descricao;
                itemVenda.Marca = item.Produto.Principal != null && item.Produto.Principal.Marca != null ? item.Produto.Principal.Marca.Descricao : string.Empty;
                itemVenda.Cor = item.Produto.Vestuario != null && item.Produto.Vestuario.Cor != null ? item.Produto.Vestuario.Cor.Descricao : string.Empty;
                itemVenda.Unidade = item.Produto.DadosGerais.Unidade != null ? item.Produto.DadosGerais.Unidade.Abreviacao : string.Empty;

                itemVenda.Quantidade = item.Quantidade.ToString();
                
                itemVenda.ValorUnitario = item.ValorUnitario.ToString("#,###,##0.00");

                itemVenda.Desconto = item.Desconto.ToString("#,###,##0.00");

                itemVenda.ValorTotal = item.ValorTotal.ToString("#,###,##0.00");

                pedidoDeVendaRelatorio.ListaItensVendaTroca.Add(itemVenda);
            }

            List<PedidoDeVendaRelatorio> listaPedidos = new List<PedidoDeVendaRelatorio>();
            listaPedidos.Add(pedidoDeVendaRelatorio);

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
                ListaItensVendaTroca = new List<ItemVenda>();
            }

            public int NumeroDocumento { get; set; }

            public int NumeroPedidoVenda { get; set; }

            public string Atendente { get; set; }

            public string DataElaboracao { get; set; }

            public string DataFechamento { get; set; }

            public string Situacao { get; set; }

            public string FormaPagamento { get; set; }

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

            public List<ItemVenda> ListaItensVendaTroca { get; set; }

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

            public string QuantidadeTrocada { get; set; }
        }
        
        #endregion
    }
}
