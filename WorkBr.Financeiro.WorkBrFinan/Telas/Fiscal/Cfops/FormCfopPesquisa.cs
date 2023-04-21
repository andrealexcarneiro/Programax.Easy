using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Servico.Fiscal.CfopServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Fiscal.Cfops
{
    public partial class FormCfopPesquisa : FormularioPadrao
    {
        #region " VARIÁVIES PRIVADAS "

        private List<Cfop> _listaDeCfops;
        private Cfop _cfopSelecionado;

        #endregion

        #region " CONSTRUTOR "

        public FormCfopPesquisa()
        {
            InitializeComponent();

            PreenchaOStatus();
            PreenchaTipoCfop();

            this.ActiveControl = txtCodigoCfop;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbPesquisaCadastro_Click(object sender, EventArgs e)
        {
            PesquiseCfops();
        }

        private void cboStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PesquiseCfops();
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void txtCodigoCfop_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PesquiseCfops();
            }
        }

        private void cboTipoCfop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PesquiseCfops();
            }
        }

        private void gcCfop_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcCfop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public Cfop PesquiseUmCfop()
        {
            this.ShowDialog();

            return _cfopSelecionado;
        }

        private void PesquiseCfops()
        {
            ServicoCfop servicoCfop = new ServicoCfop();

            _listaDeCfops = servicoCfop.ConsulteLista(txtCodigoCfop.Text, txtDescricao.Text, cboStatus.EditValue.ToString(), (EnumTipoCfop)cboTipoCfop.EditValue);

            PreencherGrid();
        }

        private void PreencherGrid()
        {
            List<CfopAuxiliar> listaDeProdutosParaGrid = new List<CfopAuxiliar>();

            foreach (var cfop in _listaDeCfops)
            {
                CfopAuxiliar cfopAuxiliar = new CfopAuxiliar();

                cfopAuxiliar.Id = cfop.Id;
                cfopAuxiliar.Descricao = cfop.Descricao;
                cfopAuxiliar.CodigoCfop = cfop.Codigo;
                cfopAuxiliar.Status = cfop.Status == "A" ? "ATIVO" : "INATIVO";

                listaDeProdutosParaGrid.Add(cfopAuxiliar);
            }

            gcCfop.DataSource = listaDeProdutosParaGrid;
            gcCfop.RefreshDataSource();
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

        private void PreenchaTipoCfop()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoCfop>();

            cboTipoCfop.Properties.DataSource = lista;
            cboTipoCfop.Properties.ValueMember = "Valor";
            cboTipoCfop.Properties.DisplayMember = "Descricao";

            cboTipoCfop.EditValue = EnumTipoCfop.ENTRADAESAIDA;
        }

        private void cboTipoCfop_EditValueChanged(object sender, EventArgs e)
        {
            if (cboTipoCfop.EditValue == null)
            {
                cboTipoCfop.Text = "ENTRADA E SAÍDA";
            }
        }

        private void Selecione()
        {
            _cfopSelecionado = null;

            if (_listaDeCfops != null && _listaDeCfops.Count > 0)
            {
                ServicoCfop servicoCfop = new ServicoCfop();

                _cfopSelecionado = servicoCfop.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        #endregion

        #region " CLASSE AUXILIAR "

        private class CfopAuxiliar
        {
            public int Id { get; set; }

            public string CodigoCfop { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
