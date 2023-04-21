using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;
using Programax.Easy.Servico.Financeiro.MovimentacaoBancoServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Financeiro.BancosParaMovimento;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Financeiro.MovimentacoesBanco
{
    public partial class FormPesquisaMovimentacoesBanco : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<MovimentacaoBanco> _listaMovimentacoesBanco;
        private MovimentacaoBanco _movimentacaoBanco;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaMovimentacoesBanco()
        {
            InitializeComponent();

            PreenchaCboDataFiltrar();
            PreenchaCboStatus();

            if (!Sessao.GrupoAcesso.Tesoureiro)
            {
                txtIdBanco.Properties.ReadOnly = true;
                btnPesquisarBanco.Visible = false;

                this.ActiveControl = cboDataFiltrar;
            }
            else
            {
                this.ActiveControl = txtIdBanco;
            }
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnPesquisarBanco_Click(object sender, EventArgs e)
        {
            FormPesquisaBancoParaMovimento formPesquisaDeBancos = new FormPesquisaBancoParaMovimento();

            var banco = formPesquisaDeBancos.PesquiseUmBanco();

            if (banco != null)
            {
                PreenchaInformacoesBanco(banco);
            }
        }

        private void txtIdBanco_Leave(object sender, EventArgs e)
        {
            if (txtIdBanco.Enabled && !string.IsNullOrEmpty(txtIdBanco.Text))
            {
                ServicoBancoParaMovimento servicoParaMovimento = new ServicoBancoParaMovimento();
                var banco = servicoParaMovimento.Consulte(txtIdBanco.Text.ToInt());

                PreenchaInformacoesBanco(banco, exibirMensagemDeNaoEncontrado: true);
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

        public MovimentacaoBanco PesquiseBanco(BancoParaMovimento banco)
        {
            if (banco != null)
            {
                txtIdBanco.Text = banco.Id.ToString();
                txtDescricaoBanco.Text = banco.NomeBanco;
            }

            this.ShowDialog();

            return _movimentacaoBanco;
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

        private void PreenchaInformacoesBanco(BancoParaMovimento banco, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (banco != null)
            {
                txtIdBanco.Text = banco.Id.ToString();
                txtDescricaoBanco.Text = banco.NomeBanco;
            }
            else
            {
                txtIdBanco.Text = string.Empty;
                txtDescricaoBanco.Text = string.Empty;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Banco não encontrado", "Banco não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                this.ActiveControl = txtIdBanco;
                txtIdBanco.Focus();
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
            List<MovimentacoesBancoGrid> listaMovimentacoesBancoGrid = new List<MovimentacoesBancoGrid>();

            foreach (var item in _listaMovimentacoesBanco)
            {
                MovimentacoesBancoGrid movimentacoesBancoGrid = new MovimentacoesBancoGrid();

                movimentacoesBancoGrid.Id = item.Id;
                movimentacoesBancoGrid.Status = item.Status.Descricao();
                movimentacoesBancoGrid.Banco = item.Banco.Id + " - " + item.Banco.NomeBanco;
                movimentacoesBancoGrid.DataHoraAbertura = item.DataHoraAbertura != null ? item.DataHoraAbertura.GetValueOrDefault().ToString("dd/MM/yyyy HH:mm") : string.Empty;
                movimentacoesBancoGrid.DataHoraFechamento = item.DataHoraFechamento != null ? item.DataHoraFechamento.GetValueOrDefault().ToString("dd/MM/yyyy HH:mm") : string.Empty;

                listaMovimentacoesBancoGrid.Add(movimentacoesBancoGrid);
            }

            gcItens.DataSource = listaMovimentacoesBancoGrid;
            gcItens.RefreshDataSource();
        }

        private void Pesquise()
        {
            BancoParaMovimento banco = !string.IsNullOrEmpty(txtIdBanco.Text) ? new BancoParaMovimento { Id = txtIdBanco.Text.ToInt() } : null;

            EnumDataFiltrarMovimentacaoCaixa? dataFiltrarMovimentacaoBanco = (EnumDataFiltrarMovimentacaoCaixa?)cboDataFiltrar.EditValue;

            DateTime? dataInicial = txtDataInicialPeriodo.Text.ToDateNullabel();
            DateTime? dataFinal = txtDataFinalPeriodo.Text.ToDateNullabel();

            EnumStatusMovimentacaoCaixa? statusMovimentacao = (EnumStatusMovimentacaoCaixa?)cboStatus.EditValue;

            ServicoMovimentacaoBanco servicoMovimentacaoBanco = new ServicoMovimentacaoBanco();

            _listaMovimentacoesBanco = servicoMovimentacaoBanco.ConsulteLista(banco, dataFiltrarMovimentacaoBanco, dataInicial, dataFinal, statusMovimentacao);

            PreenchaGrid();
        }

        private void Selecione()
        {
            _movimentacaoBanco = null;

            if (_listaMovimentacoesBanco != null && _listaMovimentacoesBanco.Count > 0)
            {
                ServicoMovimentacaoBanco servicoBancos = new ServicoMovimentacaoBanco();

                _movimentacaoBanco = servicoBancos.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class MovimentacoesBancoGrid
        {
            public int Id { get; set; }

            public string Banco { get; set; }

            public string Status { get; set; }

            public string DataHoraAbertura { get; set; }

            public string DataHoraFechamento { get; set; }
        }

        #endregion
    }
}
