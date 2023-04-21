using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Caixas;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Financeiro.MovimentacoesCaixa
{
    public partial class FormPesquisaMovimentacoesCaixa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<MovimentacaoCaixa> _listaMovimentacoesCaixa;
        private MovimentacaoCaixa _movimentacaoCaixa;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaMovimentacoesCaixa()
        {
            InitializeComponent();

            PreenchaCboDataFiltrar();
            PreenchaCboStatus();

            if (!Sessao.GrupoAcesso.Tesoureiro)
            {
                txtIdCaixa.Properties.ReadOnly = true;
                btnPesquisarCaixa.Visible = false;

                this.ActiveControl = cboDataFiltrar;
            }
            else
            {
                this.ActiveControl = txtIdCaixa;
            }
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnPesquisarCaixa_Click(object sender, EventArgs e)
        {
            FormPesquisaCaixa formPesquisaDeCaixaes = new FormPesquisaCaixa();

            var caregoria = formPesquisaDeCaixaes.PesquiseUmCaixa();

            if (caregoria != null)
            {
                PreenchaInformacoesCaixa(caregoria);
            }
        }

        private void txtIdCaixa_Leave(object sender, EventArgs e)
        {
            if (txtIdCaixa.Enabled && !string.IsNullOrEmpty(txtIdCaixa.Text))
            {
                ServicoCaixa servicoCaixa = new ServicoCaixa();
                var caregoria = servicoCaixa.Consulte(txtIdCaixa.Text.ToInt());

                PreenchaInformacoesCaixa(caregoria, exibirMensagemDeNaoEncontrado: true);
            }
        }

        private void btnPesquiarRegistros_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void cboStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void cboDataFiltrar_EditValueChanged(object sender, EventArgs e)
        {
            if (cboDataFiltrar.EditValue != null)
            {
                txtDataInicialPeriodo.Enabled = true;
                txtDataFinalPeriodo.Enabled = true;
            }
            else
            {
                txtDataInicialPeriodo.Enabled = false;
                txtDataFinalPeriodo.Enabled = false;
            }
        }

        private void gcItens_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public MovimentacaoCaixa PesquiseCaixa(Caixa caixa)
        {
            if (caixa != null)
            {
                txtIdCaixa.Text = caixa.Id.ToString();
                txtNomeCaixa.Text = caixa.NomeCaixa;
            }

            this.ShowDialog();

            return _movimentacaoCaixa;
        }

        private void gcItens_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaInformacoesCaixa(Caixa caixa, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (caixa != null)
            {
                txtIdCaixa.Text = caixa.Id.ToString();
                txtNomeCaixa.Text = caixa.NomeCaixa;

            }
            else
            {
                txtIdCaixa.Text = string.Empty;
                txtNomeCaixa.Text = string.Empty;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Caixa não encontrado", "Caixa não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                this.ActiveControl = txtIdCaixa;
                txtIdCaixa.Focus();
            }
        }

        private void PreenchaCboDataFiltrar()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumDataFiltrarMovimentacaoCaixa>();

            lista.Insert(0, null);

            cboDataFiltrar.Properties.DisplayMember = "Descricao";
            cboDataFiltrar.Properties.ValueMember = "Valor";
            cboDataFiltrar.Properties.DataSource = lista;
        }

        private void PreenchaCboStatus()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusMovimentacaoCaixa>();

            lista.Insert(0, null);

            cboStatus.Properties.DisplayMember = "Descricao";
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DataSource = lista;
        }

        private void PreenchaGrid()
        {
            List<MovimentacoesCaixaGrid> listaMovimentacoesCaixaGrid = new List<MovimentacoesCaixaGrid>();

            foreach (var item in _listaMovimentacoesCaixa)
            {
                MovimentacoesCaixaGrid movimentacoesCaixaGrid = new MovimentacoesCaixaGrid();

                movimentacoesCaixaGrid.Id = item.Id;
                movimentacoesCaixaGrid.Status = item.Status.Descricao();
                movimentacoesCaixaGrid.Caixa = item.Caixa.Id + " - " + item.Caixa.NomeCaixa;
                movimentacoesCaixaGrid.DataHoraAbertura = item.DataHoraAbertura != null ? item.DataHoraAbertura.GetValueOrDefault().ToString("dd/MM/yyyy HH:mm") : string.Empty;
                movimentacoesCaixaGrid.DataHoraFechamento = item.DataHoraFechamento != null ? item.DataHoraFechamento.GetValueOrDefault().ToString("dd/MM/yyyy HH:mm") : string.Empty;

                listaMovimentacoesCaixaGrid.Add(movimentacoesCaixaGrid);
            }

            gcItens.DataSource = listaMovimentacoesCaixaGrid;
            gcItens.RefreshDataSource();
        }

        private void Pesquise()
        {
            Caixa caixa = !string.IsNullOrEmpty(txtIdCaixa.Text) ? new Caixa { Id = txtIdCaixa.Text.ToInt() } : null;

            EnumDataFiltrarMovimentacaoCaixa? dataFiltrarMovimentacaoCaixa = (EnumDataFiltrarMovimentacaoCaixa?)cboDataFiltrar.EditValue;

            DateTime? dataInicial = txtDataInicialPeriodo.Text.ToDateNullabel();
            DateTime? dataFinal = txtDataFinalPeriodo.Text.ToDateNullabel();

            EnumStatusMovimentacaoCaixa? statusMovimentacao = (EnumStatusMovimentacaoCaixa?)cboStatus.EditValue;

            ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();

            _listaMovimentacoesCaixa = servicoMovimentacaoCaixa.ConsulteLista(caixa, dataFiltrarMovimentacaoCaixa, dataInicial, dataFinal, statusMovimentacao);

            PreenchaGrid();
        }

        private void Selecione()
        {
            _movimentacaoCaixa = null;

            if (_listaMovimentacoesCaixa != null && _listaMovimentacoesCaixa.Count > 0)
            {
                ServicoMovimentacaoCaixa servicoBancos = new ServicoMovimentacaoCaixa();

                _movimentacaoCaixa = servicoBancos.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class MovimentacoesCaixaGrid
        {
            public int Id { get; set; }

            public string Caixa { get; set; }

            public string Status { get; set; }

            public string DataHoraAbertura { get; set; }

            public string DataHoraFechamento { get; set; }
        }

        #endregion
    }
}
