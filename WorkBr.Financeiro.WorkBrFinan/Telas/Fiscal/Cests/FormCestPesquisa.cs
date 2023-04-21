using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Fiscal.CestObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Fiscal.CestServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Fiscal.Cests
{
    public partial class FormCestPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Cest _cest;
        private List<Cest> _listaDeCests;

        #endregion

        #region " CONSTRUTOR "

        public FormCestPesquisa()
        {
            InitializeComponent();

            _listaDeCests = new List<Cest>();

            PreenchaOStatus();

            this.ActiveControl = txtCodigoCest;
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

        public Cest ExibaPesquisaDeCest()
        {
            this.ShowDialog();

            return _cest;
        }

        private void Pesquise()
        {
            ServicoCest servicoCest = new ServicoCest();

            _listaDeCests = servicoCest.ConsulteLista(txtCodigoCest.Text, txtDescricaoCest.Text, cboStatus.EditValue.ToStringEmpty());

            preencherGrid();
        }

        private void Selecione()
        {
            _cest = null;

            if (_listaDeCests != null && _listaDeCests.Count > 0)
            {
                ServicoCest servicoCest = new ServicoCest();

                _cest = servicoCest.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void preencherGrid()
        {
            List<GridCest> listaDeCestsGrid = new List<GridCest>();

            foreach (var cest in _listaDeCests)
            {
                GridCest gridCest = new GridCest();

                gridCest.Id = cest.Id;
                gridCest.CodigoCest = cest.CodigoCest;
                gridCest.Status = cest.Status == "A" ? "ATIVO" : "INATIVO";
                gridCest.Descricao = cest.DescricaoCest;

                listaDeCestsGrid.Add(gridCest);
            }

            gcCests.DataSource = listaDeCestsGrid;
            gcCests.RefreshDataSource();
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

        private class GridCest
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string CodigoCest { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
