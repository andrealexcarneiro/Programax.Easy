using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.SubGrupoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Cadastros.SubGrupos
{
    public partial class FormPesquisaSubGrupos : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private SubGrupo _subGrupoSelecionado;
        private List<SubGrupo> _listaDeSubGrupos;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaSubGrupos()
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

        #endregion

        #region " MÉTODOS AUXILIARES "

        public SubGrupo PesquiseUmSubGrupo()
        {
            this.ShowDialog();

            return _subGrupoSelecionado;
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
            ServicoGrupo servicoGrupo = new ServicoGrupo();

            var linhas = servicoGrupo.ConsulteLista();

            linhas.Insert(0, null);

            cboGrupos.Properties.DataSource = linhas;
            cboGrupos.Properties.DisplayMember = "Descricao";
            cboGrupos.Properties.ValueMember = "Id";
        }

        private void Selecione()
        {
            _subGrupoSelecionado = null;

            if (_listaDeSubGrupos != null && _listaDeSubGrupos.Count > 0)
            {
                ServicoSubGrupo servicoSubGrupo = new ServicoSubGrupo();

                _subGrupoSelecionado = servicoSubGrupo.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void Pesquise()
        {
            Grupo grupo = null;

            if (cboGrupos.EditValue != null)
            {
                grupo = new Grupo { Id = (int)cboGrupos.EditValue };
            }

            ServicoSubGrupo servicoSubGrupo = new ServicoSubGrupo();

            _listaDeSubGrupos = servicoSubGrupo.ConsulteLista(txtDescricao.Text, grupo, cboStatus.EditValue.ToString());

            PreencherGrid();
        }

        private void PreencherGrid()
        {
            List<SubGrupoAuxiliar> listaDeGruposParaGrid = new List<SubGrupoAuxiliar>();

            foreach (var grupo in _listaDeSubGrupos)
            {
                SubGrupoAuxiliar subGrupoAuxiliar = new SubGrupoAuxiliar();
                subGrupoAuxiliar.Grupo = grupo.Grupo != null ? grupo.Grupo.Descricao : string.Empty;
                subGrupoAuxiliar.Descricao = grupo.Descricao;
                subGrupoAuxiliar.Id = grupo.Id;
                subGrupoAuxiliar.Status = grupo.Status == "A" ? "ATIVO" : "INATIVO";

                listaDeGruposParaGrid.Add(subGrupoAuxiliar);
            }

            gcSubGrupos.DataSource = listaDeGruposParaGrid;
            gcSubGrupos.RefreshDataSource();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class SubGrupoAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Grupo { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
