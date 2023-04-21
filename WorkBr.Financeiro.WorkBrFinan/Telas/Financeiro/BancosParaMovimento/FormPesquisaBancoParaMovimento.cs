using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;

namespace Programax.Easy.View.Telas.Financeiro.BancosParaMovimento
{
    public partial class FormPesquisaBancoParaMovimento : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private BancoParaMovimento _bancoSelecionado;
        private List<BancoParaMovimento> _listaDeBancos;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaBancoParaMovimento()
        {
            InitializeComponent();

            PreenchaOStatus();            

            this.ActiveControl = txtNomeCaixa;
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

        private void gcCaixas_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcCaixas_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void cboStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void txtNomeCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void cboFuncionarios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public BancoParaMovimento PesquiseUmBanco()
        {
            this.ShowDialog();

            return _bancoSelecionado;
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

        private void Selecione()
        {
            _bancoSelecionado = null;

            if (_listaDeBancos != null && _listaDeBancos.Count > 0)
            {
                ServicoBancoParaMovimento servicoCaixa = new ServicoBancoParaMovimento();

                _bancoSelecionado = servicoCaixa.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void Pesquise()
        {  
            ServicoBancoParaMovimento servicoCaixa = new ServicoBancoParaMovimento();

            _listaDeBancos = servicoCaixa.ConsulteLista(txtNomeCaixa.Text, cboStatus.EditValue.ToStringEmpty());

            PreencherGrid();
        }

        private void PreencherGrid()
        {
            List<BancoAuxiliar> listaDeBancosParaGrid = new List<BancoAuxiliar>();

            foreach (var banco in _listaDeBancos)
            {
                BancoAuxiliar bancoAuxiliar = new BancoAuxiliar();
                bancoAuxiliar.NomeBanco = banco.NomeBanco;
                bancoAuxiliar.Id = banco.Id;
                bancoAuxiliar.Status = banco.Status == "A" ? "ATIVO" : "INATIVO";

                bancoAuxiliar.BancoPadrao = banco.TornarPadrao == true? "SIM":"NÃO";

                listaDeBancosParaGrid.Add(bancoAuxiliar);
            }

            gcBancos.DataSource = listaDeBancosParaGrid;
            gcBancos.RefreshDataSource();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class BancoAuxiliar
        {
            public int Id { get; set; }

            public string NomeBanco { get; set; }

            public string Status { get; set; }

            public string BancoPadrao { get; set; }
        }

        #endregion
    }
}
