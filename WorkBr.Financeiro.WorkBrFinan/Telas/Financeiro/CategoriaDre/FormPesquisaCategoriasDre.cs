using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.CategoriaDreObj.ObjetoDeNeocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.CategoriaDreServ;
using Programax.Easy.Servico.Financeiro.CategoriaServ;
using Programax.Easy.Servico.Financeiro.GruposCategoriasServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Financeiro.Categorias
{
    public partial class FormPesquisaCategoriasDre : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private CategoriaDre _categoriaSelecionada;
        private List<CategoriaDre> _listaDeCategoria;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaCategoriasDre()
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

        private void gcGrupos_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcGrupos_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void FormCadastroCategoriasFinanceiras_Load(object sender, EventArgs e)
        {
            PreenchaCboGrupos();
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

        private void cboCategorias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public CategoriaDre PesquiseUmaCategoria()
        {
            this.ShowDialog();

            return _categoriaSelecionada;
        }

        private void PreenchaOStatus()
        {
            ObjetoParaComboBox objetoComboBoxAtivoOuInativo = new ObjetoParaComboBox();
            objetoComboBoxAtivoOuInativo.Valor = string.Empty;
            objetoComboBoxAtivoOuInativo.Descricao = "Ativo ou Inativo";

            ObjetoParaComboBox objetoComboBoxAtivo = new ObjetoParaComboBox();
            objetoComboBoxAtivo.Valor = "A";
            objetoComboBoxAtivo.Descricao = "Ativo";

            ObjetoParaComboBox objetoComboBoxInativo = new ObjetoParaComboBox();
            objetoComboBoxInativo.Valor = "I";
            objetoComboBoxInativo.Descricao = "Inativo";

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivoOuInativo);
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivo);
            listaDeItensParaOComboBox.Add(objetoComboBoxInativo);

            cboStatus.Properties.DataSource = listaDeItensParaOComboBox;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";

            cboStatus.EditValue = string.Empty;
        }

        private void PreenchaCboGrupos()
        {
            ServicoSubGrupoCategoria servicoGrupoCat = new ServicoSubGrupoCategoria();

            var grupos = servicoGrupoCat.ConsulteListaAtiva();

            grupos.Insert(0, null);

            cboGrupos.Properties.DataSource = grupos;
            cboGrupos.Properties.DisplayMember = "Descricao";
            cboGrupos.Properties.ValueMember = "Id";
        }

        private void Selecione()
        {
            _categoriaSelecionada = null;

            if (_listaDeCategoria != null && _listaDeCategoria.Count > 0)
            {
                ServicoCategoriaDre servicoGrupo = new ServicoCategoriaDre();

                _categoriaSelecionada = servicoGrupo.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void Pesquise()
        {
            SubGrupoCategoria grupoCategoria = null;

            if (cboGrupos.EditValue != null)
            {
                grupoCategoria = new SubGrupoCategoria { Id = (int)cboGrupos.EditValue };
            }

            ServicoCategoriaDre servicoGrupo = new ServicoCategoriaDre();

            _listaDeCategoria = servicoGrupo.ConsulteLista(txtDescricao.Text, grupoCategoria, cboStatus.EditValue.ToString());

            PreencherGrid();
        }

        private void PreencherGrid()
        {
            List<GrupoAuxiliar> listaDeCategoriasParaGrid = new List<GrupoAuxiliar>();

            foreach (var categoria in _listaDeCategoria)
            {
                GrupoAuxiliar grupoAuxiliar = new GrupoAuxiliar();
                grupoAuxiliar.SubGrupo = categoria.SubGrupoCategoria != null ? categoria.SubGrupoCategoria.Descricao : string.Empty;
                grupoAuxiliar.Descricao = categoria.Descricao;
                grupoAuxiliar.Id = categoria.Id;
                grupoAuxiliar.Status = categoria.Status == "A" ? "ATIVO" : "INATIVO";

                listaDeCategoriasParaGrid.Add(grupoAuxiliar);
            }

            gcGrupos.DataSource = listaDeCategoriasParaGrid;
            gcGrupos.RefreshDataSource();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class GrupoAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string SubGrupo { get; set; }

            public string Status { get; set; }
        }

        #endregion
               
    }
}
