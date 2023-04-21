using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.FabricanteServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Cadastros.Fabricantes
{
    public partial class FormFabricantesPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Fabricante> _listaDeFabricantes;
        private Fabricante _fabricanteSelecionada;

        #endregion

        #region " CONSTRUTOR "

        public FormFabricantesPesquisa()
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

        public Fabricante ExibaPesquisaDeFabricante()
        {
            this.ShowDialog();

            return _fabricanteSelecionada;
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
            ServicoFabricante servicoFabricante = new ServicoFabricante();

            string status = cboStatus.EditValue != null ? cboStatus.EditValue.ToString() : string.Empty;

            _listaDeFabricantes = servicoFabricante.ConsulteLista(txtId.Text.ToIntNullabel(), txtDescricao.Text, status);

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<FabricanteAuxiliar> listaDeFabricantesAuxiliares = new List<FabricanteAuxiliar>();

            foreach (var fabricante in _listaDeFabricantes)
            {
                FabricanteAuxiliar fabricanteAuxiliar = new FabricanteAuxiliar();

                fabricanteAuxiliar.Descricao = fabricante.Descricao;
                fabricanteAuxiliar.Id = fabricante.Id;
                fabricanteAuxiliar.Status = fabricante.Ativo == "A" ? "ATIVO" : "INATIVO";

                listaDeFabricantesAuxiliares.Add(fabricanteAuxiliar);
            }

            gcFabricantes.DataSource = listaDeFabricantesAuxiliares;
            gcFabricantes.RefreshDataSource();
        }

        private void Selecione()
        {
            _fabricanteSelecionada = null;

            if (_listaDeFabricantes != null && _listaDeFabricantes.Count > 0)
            {
                ServicoFabricante servicoFabricante = new ServicoFabricante();

                _fabricanteSelecionada = servicoFabricante.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        #endregion

        #region " CLASSE AUXILIAR "

        private class FabricanteAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
