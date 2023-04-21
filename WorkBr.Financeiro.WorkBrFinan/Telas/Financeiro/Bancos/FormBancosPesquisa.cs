using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.BancoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Financeiro.Bancos
{
    public partial class FormBancosPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Banco> _listaDeBancos;
        private Banco _BancosSelecionada;

        #endregion

        #region " CONSTRUTOR "

        public FormBancosPesquisa()
        {
            InitializeComponent();

            PreenchaOStatus();
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

        #endregion

        #region " MÉTODOS AUXILIARES "

        public Banco ExibaPesquisaDeBancos()
        {
            this.ShowDialog();

            return _BancosSelecionada;
        }

        private void Pesquise()
        {
            ServicoBanco servicoBanco = new ServicoBanco();

            _listaDeBancos = servicoBanco.ConsulteLista(txtDescricao.Text, cboStatus.EditValue.ToString());

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<BancosAuxiliar> listaDeBancosAuxiliares = new List<BancosAuxiliar>();

            foreach (var banco in _listaDeBancos)
            {
                BancosAuxiliar bancosAuxiliar = new BancosAuxiliar();

                bancosAuxiliar.Descricao = banco.Descricao;
                bancosAuxiliar.Id = banco.Id;
                bancosAuxiliar.CodigoCompensacao = banco.Codigo;
                bancosAuxiliar.Site = banco.Site;
                bancosAuxiliar.Status = banco.Status == "A" ? "ATIVO" : "INATIVO";

                listaDeBancosAuxiliares.Add(bancosAuxiliar);
            }

            gcBancos.DataSource = listaDeBancosAuxiliares;
            gcBancos.RefreshDataSource();
        }

        private void Selecione()
        {
            _BancosSelecionada = null;

            if (_listaDeBancos != null && _listaDeBancos.Count > 0)
            {
                ServicoBanco servicoBancos = new ServicoBanco();

                _BancosSelecionada = servicoBancos.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
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

        private class BancosAuxiliar
        {
            public int Id { get; set; }

            public string CodigoCompensacao { get; set; }

            public string Descricao { get; set; }

            public string Site { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
