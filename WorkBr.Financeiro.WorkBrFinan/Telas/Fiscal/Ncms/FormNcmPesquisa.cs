using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Fiscal.NcmServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Fiscal.Ncms
{
    public partial class FormNcmPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Ncm _ncm;
        private List<Ncm> _listaDeNcms;

        #endregion

        #region " CONSTRUTOR "

        public FormNcmPesquisa()
        {
            InitializeComponent();

            _listaDeNcms = new List<Ncm>();

            PreenchaOStatus();

            this.ActiveControl = txtCodigoNcm;
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

        private void btnPesquisaNcm_Click(object sender, EventArgs e)
        {
            Pesquise();
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

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public Ncm ExibaPesquisaDeNcm()
        {
            this.ShowDialog();

            return _ncm;
        }

        private void Pesquise()
        {
            ServicoNcm servicoNcm = new ServicoNcm();

            _listaDeNcms = servicoNcm.ConsulteLista(txtCodigoNcm.Text, txtDescricao.Text, cboStatus.EditValue.ToStringEmpty());

            preencherGrid();
        }

        private void Selecione()
        {
            _ncm = null;

            if (_listaDeNcms != null && _listaDeNcms.Count > 0)
            {
                ServicoNcm servicoNcm = new ServicoNcm();

                _ncm = servicoNcm.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void preencherGrid()
        {
            List<NcmGrid> listaDeNcmsGrid = new List<NcmGrid>();

            foreach (var ncm in _listaDeNcms)
            {
                NcmGrid ncmGrid = new NcmGrid();

                ncmGrid.Id = ncm.Id;
                ncmGrid.CodigoNcm = ncm.CodigoNcm;
                ncmGrid.Status = ncm.Status == "A" ? "ATIVO" : "INATIVO";
                ncmGrid.Descricao = ncm.Descricao;

                listaDeNcmsGrid.Add(ncmGrid);
            }

            gcNcms.DataSource = listaDeNcmsGrid;
            gcNcms.RefreshDataSource();
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

        #endregion

        #region " CLASSE AUXILIAR "

        private class NcmGrid
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string CodigoNcm { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
