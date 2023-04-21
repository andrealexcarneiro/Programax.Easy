using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Financeiro.FormasPagamento
{
    public partial class FormFormaPagamentoPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<FormaPagamento> _listaDeFormasPagamento;
        private FormaPagamento _formaPagamentoSelecionada;

        #endregion

        #region " CONSTRUTOR "

        public FormFormaPagamentoPesquisa()
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

        public FormaPagamento ExibaPesquisaDeFormaPagamento()
        {
            this.ShowDialog();

            return _formaPagamentoSelecionada;
        }

        private void Pesquise()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

            _listaDeFormasPagamento = servicoFormaPagamento.ConsulteLista(txtDescricao.Text, cboStatus.EditValue.ToString());

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<FormaPagamentoAuxiliar> listaDeFormasPagamentos = new List<FormaPagamentoAuxiliar>();

            foreach (var formaPagamento in _listaDeFormasPagamento)
            {
                FormaPagamentoAuxiliar formaPagamentoAuxiliar = new FormaPagamentoAuxiliar();

                formaPagamentoAuxiliar.Id = formaPagamento.Id;
                formaPagamentoAuxiliar.Descricao = formaPagamento.Descricao;
                formaPagamentoAuxiliar.Status = formaPagamento.Status == "A" ? "ATIVO" : "INATIVO";
                formaPagamentoAuxiliar.DisponivelParaPdv = formaPagamento.DisponivelParaPdv ? "SIM" : "NÃO";
                formaPagamentoAuxiliar.DisponivelParaContasPagar = formaPagamento.DisponivelParaContasPagar ? "SIM" : "NÃO";
                formaPagamentoAuxiliar.DisponivelParaContasReceber = formaPagamento.DisponivelParaContasReceber ? "SIM" : "NÃO";
                formaPagamentoAuxiliar.DisponivelParaPedidoVenda = formaPagamento.DisponivelParaPedidoVenda ? "SIM" : "NÃO";

                listaDeFormasPagamentos.Add(formaPagamentoAuxiliar);
            }

            gcFormasPagamento.DataSource = listaDeFormasPagamentos;
            gcFormasPagamento.RefreshDataSource();
        }

        private void Selecione()
        {
            _formaPagamentoSelecionada = null;

            if (_listaDeFormasPagamento != null && _listaDeFormasPagamento.Count > 0)
            {
                ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

                _formaPagamentoSelecionada = servicoFormaPagamento.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
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

        private class FormaPagamentoAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }

            public string DisponivelParaPdv { get; set; }

            public string DisponivelParaContasPagar { get; set; }

            public string DisponivelParaContasReceber { get; set; }

            public string DisponivelParaPedidoVenda { get; set; }
        }

        #endregion
    }
}
