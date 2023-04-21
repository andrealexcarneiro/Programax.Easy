using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;


namespace Programax.Easy.View.Telas.Cadastros.Categorias
{
    public partial class FormPesquisaCategoria : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Categoria _categoriaSelecionada;
        private List<Categoria> _listaDeCategorias;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaCategoria()
        {
            InitializeComponent();

            this.ActiveControl = txtDescricao;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void gcCategorias_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcCategorias_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void FormPesquisaCategoria_Load(object sender, EventArgs e)
        {
            PreenchaOStatus();
        }

        private void cboStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public Categoria PesquiseUmCategoria()
        {
            this.ShowDialog();

            return _categoriaSelecionada;
        }

        private void PreenchaOStatus()
        {
            ObjetoParaComboBox objetoComboBoxAtivo = new ObjetoParaComboBox();
            objetoComboBoxAtivo.Valor = "A";
            objetoComboBoxAtivo.Descricao = "Ativo";

            ObjetoParaComboBox objetoComboBoxInativo = new ObjetoParaComboBox();
            objetoComboBoxInativo.Valor = "I";
            objetoComboBoxInativo.Descricao = "Inativo";

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivo);
            listaDeItensParaOComboBox.Add(objetoComboBoxInativo);

            cboStatus.Properties.DataSource = listaDeItensParaOComboBox;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";

            cboStatus.EditValue = "A";
        }

        private void Selecione()
        {
            _categoriaSelecionada = null;

            if (_listaDeCategorias != null && _listaDeCategorias.Count > 0)
            {
                ServicoCategoria servicoCategoria = new ServicoCategoria();

                _categoriaSelecionada = servicoCategoria.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void Pesquise()
        {
            ServicoCategoria servicoCategoria = new ServicoCategoria();

            _listaDeCategorias = servicoCategoria.ConsulteLista(txtDescricao.Text, cboStatus.EditValue.ToString(),"");

            PreencherGrid();
        }

        private void PreencherGrid()
        {
            List<CategoriaAuxiliar> listaDeCategoriasParaGrid = new List<CategoriaAuxiliar>();

            foreach (var categoria in _listaDeCategorias)
            {
                CategoriaAuxiliar categoriaAuxiliar = new CategoriaAuxiliar();
                categoriaAuxiliar.Descricao = categoria.Descricao;
                categoriaAuxiliar.Id = categoria.Id;
                categoriaAuxiliar.Status = categoria.Status == "A" ? "ATIVO" : "INATIVO";

                listaDeCategoriasParaGrid.Add(categoriaAuxiliar);
            }

            gcCategorias.DataSource = listaDeCategoriasParaGrid;
            gcCategorias.RefreshDataSource();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class CategoriaAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
