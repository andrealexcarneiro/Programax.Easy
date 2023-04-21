using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Financeiro.CondicoesPagamento
{
    public partial class FormCondicaoPagamentoPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<CondicaoPagamento> _listaDeCondicoesPagamento;
        private CondicaoPagamento _CondicoesPagamentoSelecionada;

        #endregion

        #region " CONSTRUTOR "

        public FormCondicaoPagamentoPesquisa()
        {
            InitializeComponent();

            PreenchaOStatus();

            this.ActiveControl = txtDescricao;
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

        public CondicaoPagamento ExibaPesquisaDeCondicoesPagamento()
        {
            this.ShowDialog();

            return _CondicoesPagamentoSelecionada;
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
            ServicoCondicaoPagamento servicoCondicaoPagamento = new ServicoCondicaoPagamento();

            _listaDeCondicoesPagamento = servicoCondicaoPagamento.ConsulteLista(txtDescricao.Text, cboStatus.EditValue.ToStringEmpty());

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<CondicoesPagamentoAuxiliar> listaDeCondicoesPagamentoAuxiliares = new List<CondicoesPagamentoAuxiliar>();

            foreach (var condicaoPagamento in _listaDeCondicoesPagamento)
            {
                CondicoesPagamentoAuxiliar condicaoPagamentosAuxiliar = new CondicoesPagamentoAuxiliar();

                condicaoPagamentosAuxiliar.Descricao = condicaoPagamento.Descricao;
                condicaoPagamentosAuxiliar.Id = condicaoPagamento.Id;
                condicaoPagamentosAuxiliar.Status = condicaoPagamento.Status == "A" ? "ATIVO" : "INATIVO";

                listaDeCondicoesPagamentoAuxiliares.Add(condicaoPagamentosAuxiliar);
            }

            gcCondicoesPagamento.DataSource = listaDeCondicoesPagamentoAuxiliares;
            gcCondicoesPagamento.RefreshDataSource();
        }

        private void Selecione()
        {
            _CondicoesPagamentoSelecionada = null;

            if (_listaDeCondicoesPagamento != null && _listaDeCondicoesPagamento.Count > 0)
            {
                ServicoCondicaoPagamento servicoCondicoesPagamento = new ServicoCondicaoPagamento();

                _CondicoesPagamentoSelecionada = servicoCondicoesPagamento.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        #endregion

        #region " CLASSE AUXILIAR "

        private class CondicoesPagamentoAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
