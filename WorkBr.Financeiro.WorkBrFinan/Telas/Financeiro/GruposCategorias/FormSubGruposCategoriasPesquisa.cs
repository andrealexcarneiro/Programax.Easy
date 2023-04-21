using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.GruposCategoriasServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Cadastros.GruposCategorias
{
    public partial class FormSubGruposCategoriasPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<SubGrupoCategoria> _listaDeGrupos;
        private SubGrupoCategoria _GrupoSelecionado;

        #endregion

        #region " CONSTRUTOR "

        public FormSubGruposCategoriasPesquisa()
        {
            InitializeComponent();

            PreenchaOStatus();

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcNcms_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcNcms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void btnPesquisaNcm_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void cboStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public SubGrupoCategoria ExibaPesquisaDeGrupo()
        {
            this.ShowDialog();

            return _GrupoSelecionado;
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

        private void Pesquise()
        {
            ServicoSubGrupoCategoria servicoGrupo = new ServicoSubGrupoCategoria();

            string status = cboStatus.EditValue != null ? cboStatus.EditValue.ToString() : string.Empty;

            _listaDeGrupos = servicoGrupo.ConsulteLista(txtId.Text.ToIntNullabel(), txtDescricao.Text, status);

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<GrupoAuxiliar> listaDeGruposAuxiliares = new List<GrupoAuxiliar>();

            foreach (var grupo in _listaDeGrupos)
            {
                GrupoAuxiliar grupoAuxiliar = new GrupoAuxiliar();

                grupoAuxiliar.Descricao = grupo.Descricao;
                grupoAuxiliar.Id = grupo.Id;
                grupoAuxiliar.Status = grupo.Ativo == "A" ? "ATIVO" : "INATIVO";

                listaDeGruposAuxiliares.Add(grupoAuxiliar);
            }

            gcNcms.DataSource = listaDeGruposAuxiliares;
            gcNcms.RefreshDataSource();
        }

        private void Selecione()
        {
            _GrupoSelecionado = null;

            if (_listaDeGrupos != null && _listaDeGrupos.Count > 0)
            {
                ServicoSubGrupoCategoria servicoGrupo = new ServicoSubGrupoCategoria();

                _GrupoSelecionado = servicoGrupo.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        #endregion

        #region " CLASSE AUXILIAR "

        private class GrupoAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
