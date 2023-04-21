using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Cadastros.Grupos
{
    public partial class FormPesquisaGrupo : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Grupo _grupoSelecionada;
        private List<Grupo> _listaDeGrupos;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaGrupo()
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

        private void FormPesquisaGrupo_Load(object sender, EventArgs e)
        {
            PreenchaCboCategoria();
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

        public Grupo PesquiseUmGrupo()
        {
            this.ShowDialog();

            return _grupoSelecionada;
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

        private void PreenchaCboCategoria()
        {
            ServicoCategoria servicoLinha = new ServicoCategoria();

            var linhas = servicoLinha.ConsulteLista();

            linhas.Insert(0, null);

            cboCategorias.Properties.DataSource = linhas;
            cboCategorias.Properties.DisplayMember = "Descricao";
            cboCategorias.Properties.ValueMember = "Id";
        }

        private void Selecione()
        {
            _grupoSelecionada = null;

            if (_listaDeGrupos != null && _listaDeGrupos.Count > 0)
            {
                ServicoGrupo servicoGrupo = new ServicoGrupo();

                _grupoSelecionada = servicoGrupo.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void Pesquise()
        {
            Categoria categoria = null;

            if (cboCategorias.EditValue != null)
            {
                categoria = new Categoria { Id = (int)cboCategorias.EditValue };
            }

            ServicoGrupo servicoGrupo = new ServicoGrupo();

            _listaDeGrupos = servicoGrupo.ConsulteLista(txtDescricao.Text, categoria, cboStatus.EditValue.ToString());

            PreencherGrid();
        }

        private void PreencherGrid()
        {
            List<GrupoAuxiliar> listaDeGruposParaGrid = new List<GrupoAuxiliar>();

            foreach (var grupo in _listaDeGrupos)
            {
                GrupoAuxiliar grupoAuxiliar = new GrupoAuxiliar();
                grupoAuxiliar.Categoria = grupo.Categoria != null ? grupo.Categoria.Descricao : string.Empty;
                grupoAuxiliar.Descricao = grupo.Descricao;
                grupoAuxiliar.Id = grupo.Id;
                grupoAuxiliar.Status = grupo.Status == "A" ? "ATIVO" : "INATIVO";

                listaDeGruposParaGrid.Add(grupoAuxiliar);
            }

            gcGrupos.DataSource = listaDeGruposParaGrid;
            gcGrupos.RefreshDataSource();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class GrupoAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Categoria { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
