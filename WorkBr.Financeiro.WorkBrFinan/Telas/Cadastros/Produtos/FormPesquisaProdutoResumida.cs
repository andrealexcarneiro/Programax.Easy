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

namespace Programax.Easy.View.Telas.Cadastros.Produtos
{
    public partial class FormPesquisaProdutoResumida : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Produto> _listaDeProdutos;
        private Produto _produtoSelecionado;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaProdutoResumida()
        {
            InitializeComponent();

            this.ActiveControl = txtDescricao;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            PesquiseProdutos();
        }

        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PesquiseProdutos();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcProdutos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void gcProdutos_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PesquiseProdutos()
        {
            ServicoProduto servicoProduto = new ServicoProduto();

            _listaDeProdutos = servicoProduto.ConsulteListaAtivaPelaDescricao(txtDescricao.Text);

            PreencherGrid();
        }

        private void PreencherGrid()
        {
            List<ProdutoAuxiliar> listaDeProdutosParaGrid = new List<ProdutoAuxiliar>();

            foreach (var produto in _listaDeProdutos)
            {
                ProdutoAuxiliar produtoAuxiliar = new ProdutoAuxiliar();

                produtoAuxiliar.Descricao = produto.DadosGerais.Descricao;
                produtoAuxiliar.Id = produto.Id;
                produtoAuxiliar.Marca = produto.Principal.Marca != null ? produto.Principal.Marca.Descricao : string.Empty;
                produtoAuxiliar.Status = produto.DadosGerais.Status == "A" ? "ATIVO" : "INATIVO";
                produtoAuxiliar.Unidade = produto.DadosGerais.Unidade != null ? produto.DadosGerais.Unidade.Abreviacao : string.Empty;

                listaDeProdutosParaGrid.Add(produtoAuxiliar);
            }

            gcProdutos.DataSource = listaDeProdutosParaGrid;
            gcProdutos.RefreshDataSource();
        }

        private void Selecione()
        {
            _produtoSelecionado = null;

            if (_listaDeProdutos != null && _listaDeProdutos.Count > 0)
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                _produtoSelecionado = servicoProduto.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public Produto PesquiseProduto()
        {
            this.ShowDialog();

            return _produtoSelecionado;
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ProdutoAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Marca { get; set; }

            public string Unidade { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
