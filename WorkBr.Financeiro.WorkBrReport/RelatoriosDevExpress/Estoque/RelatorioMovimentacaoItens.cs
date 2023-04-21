using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;


namespace Programax.Easy.Report.RelatoriosDevExpress.Estoque
{
    public partial class RelatorioMovimentacaoItens : RelatorioBasePaisagem
    {
        #region " VARIÁVEIS PRIVADAS "

        private int? _produtoId;
        private DateTime? _dataInicialMovimentacao;
        private DateTime? _dataFinalMovimentacao;
        private EnumTipoMovimentacao? _tipoMovimentacao;
        private EnumOrigemMovimentacao? _origemMovimentacao;
        private Marca _marca;
        private Fabricante _fabricante;
        private SubGrupo _subgrupo;
        private Categoria _categoria;
        private Grupo _grupo;
        private string _descricao;
        #endregion 

        #region " CONSTRUTOR "
        
        public RelatorioMovimentacaoItens(int? produtoId, DateTime? dataInicialMovimentacao, DateTime? dataFinalMovimentacao, EnumTipoMovimentacao? tipoMovimentacao, EnumOrigemMovimentacao? origemMovimentacao,
                                          string descricao, Marca marca, Fabricante fabricante, SubGrupo subgrupo, Categoria categoria, Grupo grupo)
        {
            InitializeComponent();

            _produtoId = produtoId;
            _dataInicialMovimentacao = dataInicialMovimentacao;
            _dataFinalMovimentacao = dataFinalMovimentacao;
            _tipoMovimentacao = tipoMovimentacao;
            _origemMovimentacao = origemMovimentacao;
            _marca = marca;
            _fabricante = fabricante;
            _subgrupo = subgrupo;
            _categoria = categoria;
            _grupo = grupo;
            _descricao = descricao;

            lblDataPeriodo.Text = _dataInicialMovimentacao + "  à " + dataFinalMovimentacao;

            _tituloRelatorio = "Movimentações de Itens";
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            ServicoProduto servicoProduto = new ServicoProduto();
            var _listaProdutos = servicoProduto.ConsulteListasProdutosAtivos(_descricao, _marca, _fabricante, _categoria, _grupo, _subgrupo);

            


            //Fazer a busca por grupos
            if (_listaProdutos.Count != 0)
            {
                //Inicializar as totalizações
                double TotalSaida = 0;
                double TotalEntrada = 0;
                double TotalSaldo = 0;
                
                //Inicializa o objeto de movimentações de produtos
                VwMovimentacaoProdutoRelatorio vwMovimentacaoProdutoRelatorio = new VwMovimentacaoProdutoRelatorio();
                
                //Busca os produtos listados e carrega o objeto movimentação
                foreach (var produto in _listaProdutos)
                {
                    ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao();

                    var listaVwMovimentacaoProduto = servicoMovimentacao.ConsulteVwMovimentacoesProdutos(produto.Id, _dataInicialMovimentacao, _dataFinalMovimentacao, _tipoMovimentacao, _origemMovimentacao);

                    int quantidadeMovimentacoes = listaVwMovimentacaoProduto != null ? listaVwMovimentacaoProduto.Count : 0;
                                        
                    bool houveAlgumFiltro = //_dataInicialMovimentacao != null ||
                                                        //_dataFinalMovimentacao != null ||
                                                        _tipoMovimentacao != null ||
                                                        _origemMovimentacao != null ? true : false;                    

                    if (listaVwMovimentacaoProduto != null)
                    {
                        double saldo = 0;
                        
                        for (int i = quantidadeMovimentacoes - 1; i >= 0; i--)
                        {
                            var item = listaVwMovimentacaoProduto[i];
                            ItensRelatorio itensRelatorio = new ItensRelatorio();

                            itensRelatorio.Data = item.DataMovimentacao.ToString("dd/MM/yyyy HH:mm");
                            itensRelatorio.Usuario = item.PessoaId + " - " + item.PessoaNome;
                            itensRelatorio.Itens = item.ProdutoDescricao;
                            itensRelatorio.Tipo = item.TipoMovimentacao.Descricao();
                            itensRelatorio.Origem = item.OrigemMovimentacao.Descricao();
                            itensRelatorio.Observacoes = item.Observacoes;
                            itensRelatorio.Custo = produto.FormacaoPreco.PrecoCompra.ToString();
                            if (item.TipoMovimentacao == EnumTipoMovimentacao.ENTRADA)
                            {
                                itensRelatorio.Entrada = item.Quantidade.ToString("#0.0000");

                                saldo += item.Quantidade;
                                TotalEntrada += item.Quantidade;
                            }
                            else
                            {
                                itensRelatorio.Saida = item.Quantidade.ToString("#0.0000");

                                saldo -= item.Quantidade;
                                TotalSaida += item.Quantidade;
                            }

                            if (!houveAlgumFiltro)
                            {
                                itensRelatorio.Saldo = saldo.ToString("#0.0000");
                                TotalSaldo += saldo;
                            }
                                                        
                            vwMovimentacaoProdutoRelatorio.ListaItensRelatorio.Add(itensRelatorio);
                        }                        
                    }
                                        
                }
                vwMovimentacaoProdutoRelatorio.TotalEntranda = TotalEntrada.ToString("0.0000");
                vwMovimentacaoProdutoRelatorio.TotalSaida = TotalSaida.ToString("0.0000");
                vwMovimentacaoProdutoRelatorio.TotalSaldo = (TotalEntrada - TotalSaida).ToString("0.0000");//TotalSaldo.ToString("0.0000");

                if (_marca != null)
                    vwMovimentacaoProdutoRelatorio.Marca = _marca.Descricao;

                if (_fabricante != null)
                vwMovimentacaoProdutoRelatorio.Fabricante = _fabricante.Descricao;

                if (_categoria != null)
                    vwMovimentacaoProdutoRelatorio.Categoria = _categoria.Descricao;

                if (_grupo != null)
                    vwMovimentacaoProdutoRelatorio.Grupo = _grupo.Descricao;

                if (_subgrupo != null)
                    vwMovimentacaoProdutoRelatorio.Subgrupo = _subgrupo.Descricao;

                List<VwMovimentacaoProdutoRelatorio> lista = new List<VwMovimentacaoProdutoRelatorio>();
                lista.Add(vwMovimentacaoProdutoRelatorio);
                ConteudoRelatorio.DataSource = lista;
            }
            else if(_marca == null || _fabricante == null || _categoria == null) //Vai fazer a busca apenas por item
            {
                ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao();

                var lista = servicoMovimentacao.ConsulteVwMovimentacoesProdutos(_produtoId, _dataInicialMovimentacao, _dataFinalMovimentacao, _tipoMovimentacao, _origemMovimentacao);

                PreenchaRelatorioMovimentacoesProdutos(lista);
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaRelatorioMovimentacoesProdutos(List<VwMovimentacaoProduto> listaVwMovimentacaoProduto)
        {
            int quantidadeMovimentacoes = listaVwMovimentacaoProduto != null ? listaVwMovimentacaoProduto.Count : 0;

            //VwMovimentacaoProdutoRelatorio[] listaVwMovimentacaoprodutoRelatorio = new VwMovimentacaoProdutoRelatorio[quantidadeMovimentacoes];

            bool houveAlgumFiltro = _dataInicialMovimentacao != null ||
                                                _dataFinalMovimentacao != null ||
                                                _tipoMovimentacao != null ||
                                                _origemMovimentacao != null ? true : false;

            VwMovimentacaoProdutoRelatorio vwMovimentacaoProdutoRelatorio = new VwMovimentacaoProdutoRelatorio();

            if (listaVwMovimentacaoProduto != null)
            {
                double saldo = 0;
                double TotalSaida = 0;
                double TotalEntrada = 0;
                double TotalSaldo = 0;
                
                for (int i = quantidadeMovimentacoes - 1; i >= 0; i--)
                {
                    var item = listaVwMovimentacaoProduto[i];
                    ItensRelatorio itensRelatorio = new ItensRelatorio();

                    itensRelatorio.Data = item.DataMovimentacao.ToString("dd/MM/yyyy HH:mm");
                    itensRelatorio.Usuario = item.PessoaId + " - " + item.PessoaNome;
                    itensRelatorio.Itens = item.ProdutoDescricao;
                    itensRelatorio.Tipo = item.TipoMovimentacao.Descricao();
                    itensRelatorio.Origem = item.OrigemMovimentacao.Descricao();
                    itensRelatorio.Observacoes = item.Observacoes;

                    if (item.TipoMovimentacao == EnumTipoMovimentacao.ENTRADA)
                    {
                        itensRelatorio.Entrada = item.Quantidade.ToString("#0.0000");

                        saldo += item.Quantidade;
                        TotalEntrada += item.Quantidade;                        
                    }
                    else
                    {
                        itensRelatorio.Saida = item.Quantidade.ToString("#0.0000");

                        saldo -= item.Quantidade;
                        TotalSaida += item.Quantidade;                        
                    }

                    if (!houveAlgumFiltro)
                    {
                        itensRelatorio.Saldo = saldo.ToString("#0.0000");
                    }

                    TotalSaldo += saldo;
                    vwMovimentacaoProdutoRelatorio.ListaItensRelatorio.Add(itensRelatorio);
                }

                vwMovimentacaoProdutoRelatorio.TotalEntranda = TotalEntrada.ToString("0.0000");
                vwMovimentacaoProdutoRelatorio.TotalSaida = TotalSaida.ToString("0.0000");
                vwMovimentacaoProdutoRelatorio.TotalSaldo = (TotalEntrada - TotalSaida).ToString("0.0000");//TotalSaldo.ToString("0.0000");
                vwMovimentacaoProdutoRelatorio.Marca = "";
                vwMovimentacaoProdutoRelatorio.Fabricante = "";
                vwMovimentacaoProdutoRelatorio.Categoria = "";
                vwMovimentacaoProdutoRelatorio.Grupo = "";
                vwMovimentacaoProdutoRelatorio.Subgrupo = "";
                
            }

            List<VwMovimentacaoProdutoRelatorio> lista = new List<VwMovimentacaoProdutoRelatorio>();
            lista.Add(vwMovimentacaoProdutoRelatorio);
            ConteudoRelatorio.DataSource = lista;
        }

        #endregion

        #region " CLASSES AUXILIARES "

        public class VwMovimentacaoProdutoRelatorio
        {
            public VwMovimentacaoProdutoRelatorio()
            {
                ListaItensRelatorio = new List<ItensRelatorio>();
            }

            public string Marca { get; set; }

            public string Fabricante { get; set; }

            public string Categoria { get; set; }

            public string Grupo { get; set; }

            public string Subgrupo { get; set; }

            public string TotalEntranda { get; set; }

            public string TotalSaida { get; set; }

            public string TotalSaldo { get; set; }

            public List<ItensRelatorio> ListaItensRelatorio { get; set; }
        }

        public class ItensRelatorio
        {
            public string Data { get; set; }

            public string Itens { get; set; }

            public string Usuario { get; set; }

            public string Tipo { get; set; }

            public string Origem { get; set; }

            public string Observacoes { get; set; }

            public string Entrada { get; set; }

            public string Saida { get; set; }
            public string Custo { get; set; }

            public string Saldo { get; set; }
        }
        #endregion
    }
}
