﻿using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Report.RelatoriosDevExpress.Vendas
{
    public partial class RelatorioPedidoVenda : RelatorioBaseDev
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _numeroPedido;
        private Parametros _parametros;
        private string strEmpresa;
        private EnumTipoEndereco tipo;
        #endregion

        #region " CONSTRUTOR "

        public RelatorioPedidoVenda(int numeroPedido, EnumTipoEndereco Endereco )
        {
            InitializeComponent();

            CarregueParametros();
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            tipo = Endereco;
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            if (empresa.DadosEmpresa.NomeFantasia.Length < 8)
            {
                strEmpresa = empresa.DadosEmpresa.NomeFantasia.Substring(0, empresa.DadosEmpresa.NomeFantasia.Length);
            }
            else
            {
                strEmpresa = empresa.DadosEmpresa.NomeFantasia.Substring(0, 8);
            }


            _numeroPedido = numeroPedido;

            _tituloRelatorio = "ORÇAMENTO / PEDIDO DE VENDA";
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
            var pedido = servicoPedidoDeVenda.Consulte(_numeroPedido);

            PedidoDeVendaRelatorio pedidoDeVendaRelatorio = new PedidoDeVendaRelatorio();

            pedidoDeVendaRelatorio.NumeroDocumento = pedido.Id;
            pedidoDeVendaRelatorio.TipoDocumento = pedido.TipoPedidoVenda.Descricao();

            if (pedido.Cliente != null)
            {
                pedidoDeVendaRelatorio.CpfCnpjCliente = pedido.Cliente.DadosGerais.CpfCnpj;
                pedidoDeVendaRelatorio.NomeCliente = pedido.Cliente.Id + " - " + pedido.Cliente.DadosGerais.NomeFantasia;
                pedidoDeVendaRelatorio.Contato = pedido.Cliente.EmpresaPessoa != null ? pedido.Cliente.EmpresaPessoa.NomeContato1 : string.Empty;
                pedidoDeVendaRelatorio.Email = pedido.Cliente.EmpresaPessoa != null ? pedido.Cliente.EmpresaPessoa.EmailPrincipal : string.Empty;

                var telefone = pedido.Cliente.ListaDeTelefones.FirstOrDefault();
                if (_parametros.ParametrosVenda.ExibirTelefonePedido == false)
                {

                    if (telefone != null)
                    {
                        pedidoDeVendaRelatorio.Telefone = "(" + telefone.Ddd + ") " + telefone.Numero;
                    }
                }
               
            }

            if (pedido.EnderecoPedidoDeVenda != null)
            {
                //ServicoPessoa servicoPessoa = new ServicoPessoa();
                //var cliente = new ServicoPessoa().ConsulteClienteAtivo(pedido.Cliente.Id);

                //if (cliente != null && cliente.ListaDeEnderecos != null && cliente.ListaDeEnderecos.Count > 0)
                //{
                    var endereco = pedido.Cliente.ListaDeEnderecos.FirstOrDefault(end => end.TipoEndereco == tipo);

                    if (endereco == null)
                    {
                        endereco = pedido.Cliente.ListaDeEnderecos.First();
                    }

                    pedidoDeVendaRelatorio.Endereco = endereco.Rua;
                    pedidoDeVendaRelatorio.Numero = endereco.Numero;
                    pedidoDeVendaRelatorio.Complemento = endereco.Complemento;
                    pedidoDeVendaRelatorio.Bairro = endereco.Bairro;
                    pedidoDeVendaRelatorio.Cidade = endereco.Cidade.Descricao;
                    pedidoDeVendaRelatorio.Estado =  endereco.Cidade.Estado != null ? endereco.Cidade.Estado.UF : string.Empty;
                    pedidoDeVendaRelatorio.Cep =  endereco.CEP;
                //}

                //pedidoDeVendaRelatorio.Endereco = pedido.EnderecoPedidoDeVenda.Rua;
                //pedidoDeVendaRelatorio.Numero = pedido.EnderecoPedidoDeVenda.Numero;
                //pedidoDeVendaRelatorio.Complemento = pedido.EnderecoPedidoDeVenda.Complemento;
                //pedidoDeVendaRelatorio.Bairro = pedido.EnderecoPedidoDeVenda.Bairro;
                //pedidoDeVendaRelatorio.Cidade = pedido.EnderecoPedidoDeVenda.Cidade.Descricao;
                //pedidoDeVendaRelatorio.Estado = pedido.EnderecoPedidoDeVenda.Cidade.Estado != null ? pedido.EnderecoPedidoDeVenda.Cidade.Estado.UF : string.Empty;
                //pedidoDeVendaRelatorio.Cep = pedido.EnderecoPedidoDeVenda.CEP;
               
            }

            pedidoDeVendaRelatorio.Atendente = pedido.Atendente != null ? pedido.Atendente.Id + " - " + pedido.Atendente.DadosGerais.NomeFantasia : string.Empty;
            pedidoDeVendaRelatorio.Vendedor = pedido.Vendedor != null ? pedido.Vendedor.Id + " - " + pedido.Vendedor.DadosGerais.NomeFantasia : string.Empty;
            pedidoDeVendaRelatorio.Transportadora = pedido.Transportadora != null ? pedido.Transportadora.Id + " - " + pedido.Transportadora.DadosGerais.NomeFantasia : string.Empty;

            pedidoDeVendaRelatorio.TipoFrete = pedido.TipoFrete.Descricao();

            pedidoDeVendaRelatorio.DataElaboracao = pedido.DataElaboracao.ToString("dd/MM/yyyy");
            pedidoDeVendaRelatorio.DataFechamento = pedido.DataFechamento != null ? pedido.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;

            pedidoDeVendaRelatorio.CondicaoPagamento = pedido.CondicaoPagamento != null ? pedido.CondicaoPagamento.Descricao : string.Empty;

            pedidoDeVendaRelatorio.FormaPagamento = pedido.FormaPagamento != null ? pedido.FormaPagamento.Descricao : string.Empty;
            
            if(_parametros.ParametrosVenda.LimiteDiarioManha == 0)
            {
                pedidoDeVendaRelatorio.Situacao = pedido.StatusPedidoVenda == EnumStatusPedidoDeVenda.FATURADO ? "FATURADO" : pedido.StatusPedidoVenda.Descricao();
            }
            else
            {
                pedidoDeVendaRelatorio.Situacao = pedido.StatusPedidoVenda.Descricao();
            }            
            
            pedidoDeVendaRelatorio.Observacoes = pedido.ObservacoesGeraisVenda;

           

            if (strEmpresa == "SHOPPING")
            {
                txtPagoNaoPago.Visible = true;
                txtPagoNaoPago.Text = pedido.EstahPago == true ? "PAGO" : "NÃO PAGO";
            }
            else
            {
                txtPagoNaoPago.Visible = false;

            }


            if (!string.IsNullOrEmpty(pedido.DataPrevisaoEntrega.ToString()))
            {
                lblDataPrevisaoEntrega.Visible = true;
                txtDataPrevisaoEntrega.Visible = true;
                pedidoDeVendaRelatorio.DataPrevisaoEntrega = pedido.DataPrevisaoEntrega.Value.ToString("dd/MM/yyyy");
            }
           
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
