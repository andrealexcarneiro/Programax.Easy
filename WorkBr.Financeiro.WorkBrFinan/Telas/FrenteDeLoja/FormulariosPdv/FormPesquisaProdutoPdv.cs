using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Vendas.VendaRapidaServ;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.View.ClassesAuxiliares;

namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    public partial class FormPesquisaProdutoPdv : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<VWProdutoPdv> _listaProdutos;
        private Produto _produtoSelecionado;
        private TabelaPreco _tabelaPreco;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaProdutoPdv(TabelaPreco tabelaPreco, string textoParaConsulta = "")
        {
            InitializeComponent();

            _listaProdutos = new List<VWProdutoPdv>();
            _tabelaPreco = tabelaPreco;

            this.ActiveControl = txtCodigoBarras;

            if (!string.IsNullOrEmpty(textoParaConsulta))
            {
                txtCodigoBarras.Text = textoParaConsulta;
            }
        }

        #endregion

        #region " METODOS PÚBLICOS "

        public Produto PesquiseProduto()
        {
            this.AbrirTelaModal(true);

            return _produtoSelecionado;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void FormPesquisaProdutoPdv_Load(object sender, EventArgs e)
        {
            txtCodigoBarras.Select(txtCodigoBarras.Text.Length, 0);
        }

        private void txtCodigoBarras_EditValueChanged(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void FormPesquisaProdutoPdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SelecioneProduto();
            }
        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = false;
                gridView5.MovePrev();
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.Handled = false;
                gridView5.MoveNext();
            }
        }
                

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            SelecioneProduto();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gcProdutos_Click(object sender, EventArgs e)
        {
            var fotoPdv = _listaProdutos.Find(x => x.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

            if (fotoPdv.Foto == null)
            {
                picFoto.Image = Properties.Resources.produtos;
            }
            else
            {
                picFoto.Image = TratamentoDeImagens.ConvertByteToImagem(fotoPdv.Foto).Image;
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void Pesquise()
        {
            if (string.IsNullOrWhiteSpace(txtCodigoBarras.Text))
            {
                _listaProdutos.Clear();
            }
            else
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                _listaProdutos = servicoProduto.ConsulteListaVwProdutosPdvCodigoBarrasDescricao(txtCodigoBarras.Text);
            }

            PreenchaGrid();
        }

        private void PreenchaGrid()
        {
            List<ProdutoGrid> listaProdutosGrid = new List<ProdutoGrid>();

            foreach (var produto in _listaProdutos)
            {
                ProdutoGrid produtoGrid = new ProdutoGrid();
                produtoGrid.CodigoBarras = produto.CodigoDeBarras;
                produtoGrid.Descricao = produto.Descricao;
                produtoGrid.Id = produto.Id.ToString();
                produtoGrid.SaldoDisponivel = (produto.Estoque - produto.EstoqueReservado).ToString();

                Produto produtoOriginal = new Produto { Id = produto.Id };
                produtoOriginal.FormacaoPreco.EhPromocao = produto.EhPromocao;
                produtoOriginal.FormacaoPreco.ValorPromocao = produto.ValorPromocao;
                produtoOriginal.FormacaoPreco.ValorVenda = produto.ValorVenda;

                produtoGrid.ValorUnitario = ServicoVendaRapida.CalculePrecoUnitarioProduto(_tabelaPreco, produtoOriginal, pesquisarTabelaPreco: false, pesquisarProduto: false).ToString("#,##0.00");

                listaProdutosGrid.Add(produtoGrid);
            }

            gcProdutos.DataSource = listaProdutosGrid;
            gcProdutos.RefreshDataSource();
        }

        private void SelecioneProduto()
        {
            _produtoSelecionado = null;

            if (_listaProdutos != null && _listaProdutos.Count > 0)
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                _produtoSelecionado = servicoProduto.Consulte(colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

                this.Close();
            }
            else
            {
                MessageBoxAkil.Show("Selecione um produto por favor.", "Aviso");
            }
        }

        
        #endregion

        #region " CLASSES AUXILIARES "

        private class ProdutoGrid
        {
            public string Id { get; set; }

            public string CodigoBarras { get; set; }

            public string Descricao { get; set; }

            public string SaldoDisponivel { get; set; }

            public string ValorUnitario { get; set; }
        }

        #endregion
                
    }
}
