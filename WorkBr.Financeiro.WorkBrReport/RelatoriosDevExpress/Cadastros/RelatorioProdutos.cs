using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using System.Windows.Documents;
using System.Collections.Generic;
using Programax.Easy.Servico.Estoque.EntradaMercadoriaServ;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Servico.Fiscal.NcmServ;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.TransferenciaServ;

namespace Programax.Easy.Report.RelatoriosDevExpress.Cadastros
{
    public partial class RelatorioProdutos : RelatorioBasePaisagem
    {
        #region " VARIÁVEIS PRIVADAS "

        private Categoria _categoria;
        private Grupo _grupo;
        private SubGrupo _subGrupo;
        private Marca _marca;
        private Fabricante _fabricante;
        private Tamanho _tamanho;
        private bool _itensComEstoqueMinimo;
        private int? _itemComDDVAbaixoDe;
        private EnumOrdenacaoPesquisaProduto _ordenacaoPesquisaProduto;
        private string _status;
        private double totalVenda;
        private double totalcusto;
        private double totalSaldo;
        private int? _estoqueMairQue;
        private bool _mostrarNcms;
        private List<Ncm> listaNcms;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioProdutos(Categoria categoria,
                                            Grupo grupo,
                                            SubGrupo subGrupo,
                                            Marca marca,
                                            Fabricante fabricante,
                                            Tamanho tamanho,
                                            bool itensComEstoqueMinimo,
                                            bool mostrarNcms,
                                            int? itemComDDVAbaixoDe,
                                            EnumOrdenacaoPesquisaProduto ordenacaoPesquisaProduto,
                                            string status,
                                            int? estoqueMaiorQue = null)
        {
            InitializeComponent();

            _categoria = categoria;
            _grupo = grupo;
            _subGrupo = subGrupo;
            _marca = marca;
            _fabricante = fabricante;
            _tamanho = tamanho;
            _itensComEstoqueMinimo = itensComEstoqueMinimo;
            _itemComDDVAbaixoDe = itemComDDVAbaixoDe;
            _ordenacaoPesquisaProduto = ordenacaoPesquisaProduto;
            _status = status;
            _estoqueMairQue = estoqueMaiorQue;
            _mostrarNcms = mostrarNcms;

            _tituloRelatorio = "RELATÓRIO DE ITENS";
        }

        #endregion

        #region " MÉTODOS SOBRECARREGADOS "

        protected override void CarregueDadosRelatorio()
        {
            if(_mostrarNcms)
            {
                //lblNcm.Visible = true;
                //txtNcm.Visible = true;
                listaNcms = new ServicoNcm().ConsulteLista();
            }
            
            ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();
            ServicoProduto servicoProduto = new ServicoProduto();
            
            List<VWProduto> listaProdutos = servicoProduto.ConsulteLista(_categoria,
                                                                                                  _grupo,
                                                                                                  _subGrupo,
                                                                                                  _marca,
                                                                                                  _fabricante,
                                                                                                  _tamanho,
                                                                                                  _itensComEstoqueMinimo,
                                                                                                  _itemComDDVAbaixoDe,
                                                                                                  _ordenacaoPesquisaProduto,
                                                                                                  _estoqueMairQue);

            List<ProdutoRelatorio> listaProdutosRelatorio = new List<ProdutoRelatorio>();
            double quantidadesubestoque = 0;

            foreach (var produto in listaProdutos)
            {
                Produto produtoAtivo = new Produto();

                if (_status != "T")
                {
                    if (_status == "A")
                    {
                        produtoAtivo = servicoProduto.ConsulteProdutoAtivo(produto.Id);
                        if (produtoAtivo == null)
                            continue;
                    }
                    //status = "I"
                    else
                    {
                        produtoAtivo = servicoProduto.ConsulteProdutoAtivo(produto.Id);
                        if (produtoAtivo != null)
                            continue;
                    }
                }
                    
                    ProdutoRelatorio produtoRelatorio = new ProdutoRelatorio();
                    double estoquereservado = 0;

                    produtoRelatorio.Categoria = produto.CategoriaDescricao;
                    produtoRelatorio.Grupo = produto.GrupoDescricao;
                    produtoRelatorio.SubGrupo = produto.SubGrupoDescricao;
                    produtoRelatorio.Tamanho = produto.TamanhoDescricao;
                    produtoRelatorio.Unidade = produto.UnidadeDescricao;
                    produtoRelatorio.Marca = produto.MarcaDescricao;

                    produtoRelatorio.Id = produto.Id;
                    produtoRelatorio.CodigoBarras = produto.CodigoBarras;
                    produtoRelatorio.Descricao = produto.Descricao;

                    produtoRelatorio.NCM = _mostrarNcms && produto.CodigoFiscal !=0? listaNcms.Find(x=>x.Id ==produto.CodigoFiscal).CodigoNcm : string.Empty;

                    produtoRelatorio.ValorVenda = produto.ValorVenda.ToString("#,###,##0.00");

                    produtoRelatorio.ValorCompra = produto.PrecoCompra.ToString("R$ #,###,##0.00");

                    produtoRelatorio.Saldo = produto.Estoque.ToString("#,###,##0");

                    produtoRelatorio.EstoqueMinimo = produto.QtdMinima.ToString("#,###,##0");
                    estoquereservado = produto.EstoqueReservado;
                    if (estoquereservado < 0)
                    {
                        estoquereservado = 0;
                    }
                    produtoRelatorio.Reserva = estoquereservado.ToString("#,###,##0");
                    produtoRelatorio.Disponivel = (produto.Estoque - estoquereservado).ToString("#,###,##0");

                ServicoItemTransferencia servicoItemTransferencia = new ServicoItemTransferencia();
                    var ItemTransferencia = servicoItemTransferencia.ConsulteProduto(produto.Id);
                    quantidadesubestoque = 0;

                    if (ItemTransferencia != null)
                    {
                        foreach (var itemproduto in ItemTransferencia)
                        {
                            quantidadesubestoque += itemproduto.QuantidadeEstoque;
                        }

                        produtoRelatorio.QtdSub = quantidadesubestoque.ToString("#0.00");
                    }
                    else
                    {
                        produtoRelatorio.QtdSub = "0.00";
                    }



                    produtoRelatorio.DDV = produto.DDV.ToString("#,###,##0");

                    totalVenda += (produto.ValorVenda * produto.Estoque);
                    totalcusto += (produto.PrecoCompra * produto.Estoque);
                    totalSaldo += produto.Estoque;

                listaProdutosRelatorio.Add(produtoRelatorio);                
            }

            ConteudoRelatorio.DataSource = listaProdutosRelatorio;

            txtTotalRegistros.Text = listaProdutosRelatorio.Count.ToString();
            txtTotalSaldos.Text = totalSaldo.ToString("#,###,##0");            
            txtTotalVendas.Text = totalVenda.ToString("#,###,##0.00");
            txtTotalCustos.Text = totalcusto.ToString("#,###,##0.00");
        }

        #endregion

        #region " CLASSES AUXILIARES "

        public class ProdutoRelatorio
        {
            public int Id { get; set; }

            public string CodigoBarras { get; set; }

            public string Descricao { get; set; }

            public string NCM { get; set; }

            public string Unidade { get; set; }

            public string Categoria { get; set; }

            public string Grupo { get; set; }

            public string SubGrupo { get; set; }

            public string Marca { get; set; }

            public string Tamanho { get; set; }

            public string ValorCompra { get; set; }

            public string ValorVenda { get; set; }

            public string EstoqueMinimo { get; set; }
            public string Reserva { get; set; }
            public string Disponivel { get; set; }

            public string Saldo { get; set; }
            public string QtdSub { get; set; }

            public string DDV { get; set; }
        }

        #endregion
    }
}
