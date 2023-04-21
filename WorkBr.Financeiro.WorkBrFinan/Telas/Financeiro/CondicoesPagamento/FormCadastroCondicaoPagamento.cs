using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using System.Windows.Forms;
using System.Data;
using System.Text.RegularExpressions;

namespace Programax.Easy.View.Telas.Financeiro.CondicoesPagamento
{
    public partial class FormCadastroCondicaoPagamento : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroCondicaoPagamento()
        {
            InitializeComponent();

            this.NomeDaTela = "Condição de Pagamento";

            chkDisponivelAcimaDoValor.Checked = false;

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            PreenchaOStatus();

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS DOS CONTROLES "

        private void chkDisponivelAcimaDoValor_CheckedChanged(object sender, System.EventArgs e)
        {
            if (chkDisponivelAcimaDoValor.Checked)
            {
                txtValorDisponivelCondicaoPagamento.Enabled = true;
            }
            else
            {
                txtValorDisponivelCondicaoPagamento.Enabled = false;
                txtValorDisponivelCondicaoPagamento.Text = string.Empty;
            }
        }

        private void btnSair_Click(object sender, System.EventArgs e)
        {
            this.FecharFormulario();
        }

        private void gcParcelas_RowsAdded(object sender, System.Windows.Forms.DataGridViewRowsAddedEventArgs e)
        {
            if (gcParcelas.Rows.Count > 1)
            {
                gcParcelas.Rows[gcParcelas.Rows.Count - 2].Cells[0].Value = gcParcelas.Rows.Count - 1;
            }
        }

        private void gcParcelas_RowsRemoved(object sender, System.Windows.Forms.DataGridViewRowsRemovedEventArgs e)
        {
            ReorganizarNumeroParcelas();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Salve();
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            PesquiseCondicaoPeloId();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void gcParcelas_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(CelulaGrid_KeyPress);
            e.Control.KeyDown += new KeyEventHandler(CelulaGrid_KeyDown);
        }

        private void CelulaGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            var celula = (DataGridViewTextBoxEditingControl)sender;

            if (gcParcelas.CurrentCellAddress.X == 1)
            {
                e.Handled = true;

                if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;

                if (char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 44 || e.KeyChar == 46)
                {
                    if ((e.KeyChar != 44 && e.KeyChar != 46) || ((e.KeyChar == 44 || e.KeyChar == 46) && !celula.Text.ToStringEmpty().Contains(",")))
                    {
                        e.Handled = false;
                    }
                }
            }
        }

        private void CelulaGrid_KeyDown(object sender, KeyEventArgs e)
        {
            var celula = (DataGridViewTextBoxEditingControl)sender;

            if (gcParcelas.CurrentCellAddress.X != 1)
            {
                celula.Text = celula.Text.Replace(".", ",");
            }
        }

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            FormCondicaoPagamentoPesquisa formCondicaoPagamentoPesquisa = new FormCondicaoPagamentoPesquisa();
            var condicaoPagamento = formCondicaoPagamentoPesquisa.ExibaPesquisaDeCondicoesPagamento();

            if (condicaoPagamento != null)
            {
                EditeCondicaoPagamento(condicaoPagamento);
            }
        }

        private void btnGerarParcelas_Click(object sender, EventArgs e)
        {
            GereParcelas();
        }

        private void txtQuantidadeParcelas_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtSomenteNumeros_KeyPress(sender, e);
        }

        private void txtDiaPrimeiraParcela_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtSomenteNumeros_KeyPress(sender, e);
        }

        private void txtDiasEntreParcelas_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtSomenteNumeros_KeyPress(sender, e);
        }

        private void txtDiasEntreParcelas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GereParcelas();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void Salve()
        {
            Action actionSalvar = () =>
            {
                var condicaoPagamento = RetorneCondicaoPagamentoEmEdicao();

                ServicoCondicaoPagamento servicoCondicaoPagamento = new ServicoCondicaoPagamento();

                if (string.IsNullOrEmpty(txtId.Text))
                {
                    servicoCondicaoPagamento.Cadastre(condicaoPagamento);
                }
                else
                {
                    servicoCondicaoPagamento.Atualize(condicaoPagamento);
                }

                txtId.Text = condicaoPagamento.Id.ToString();

                PesquiseCondicaoPeloId();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private CondicaoPagamento RetorneCondicaoPagamentoEmEdicao()
        {
            CondicaoPagamento condicaoPagamento = new CondicaoPagamento();

            condicaoPagamento.Id = txtId.Text.ToInt();
            condicaoPagamento.DataCadastro = txtDataCadastro.Text.ToDate();
            condicaoPagamento.Descricao = txtDescricao.Text;
            condicaoPagamento.Status = cboStatus.EditValue.ToString();

            condicaoPagamento.EstahDisponivelParaContasAPagar = chkDisponivelParaContasAPagar.Checked;
            condicaoPagamento.EstahDisponivelParaContasAReceber = chkDisponivelParaContasAReceber.Checked;
            condicaoPagamento.EstahDisponivelParaPdv = chkDisponivelParaPdv.Checked;
            condicaoPagamento.PrecisaDaLiberacaoDoGerente = chkPrecisaLiberacaoDoGerente.Checked;
            condicaoPagamento.EstahDisponivelAcimaDeDeterminadoValor = chkDisponivelAcimaDoValor.Checked;
            condicaoPagamento.CondicaoPadraoAVista = chkPadraoAVista.Checked;

            condicaoPagamento.ValorQueEstaraDisponivel = txtValorDisponivelCondicaoPagamento.Text.ToDoubleNullabel();

            condicaoPagamento.ListaDeParcelas = RetorneListaParcelasCondicaoPagamento();

            return condicaoPagamento;
        }

        private List<ParcelaCondicaoPagamento> RetorneListaParcelasCondicaoPagamento()
        {
            List<ParcelaCondicaoPagamento> listaParcelas = new List<ParcelaCondicaoPagamento>();

            for (int i = 0; i < gcParcelas.Rows.Count - 1; i++)
            {
                ParcelaCondicaoPagamento parcela = new ParcelaCondicaoPagamento();

                parcela.NumeroParcela = gcParcelas.Rows[i].Cells[0].Value.ToInt();
                parcela.Dias = gcParcelas.Rows[i].Cells[1].Value.ToInt();
                parcela.PercentualRateio = gcParcelas.Rows[i].Cells[2].Value.ToDouble();
                parcela.PercentualAcrescimo = gcParcelas.Rows[i].Cells[3].Value.ToDouble();

                listaParcelas.Add(parcela);
            }

            return listaParcelas;
        }

        private void PreenchaOStatus()
        {
            ObjetoParaComboBox objetoComboBoxAtivo = new ObjetoParaComboBox();
            objetoComboBoxAtivo.Valor = "A";
            objetoComboBoxAtivo.Descricao = "Ativo";

            ObjetoParaComboBox objetoComboBoxInativo = new ObjetoParaComboBox();
            objetoComboBoxInativo.Valor = "I";
            objetoComboBoxInativo.Descricao = "Inativo";

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivo);
            listaDeItensParaOComboBox.Add(objetoComboBoxInativo);

            cboStatus.Properties.DataSource = listaDeItensParaOComboBox;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";

            cboStatus.EditValue = "A";
        }

        private void ReorganizarNumeroParcelas()
        {
            for (int i = 0; i < gcParcelas.Rows.Count - 1; i++)
            {
                var linha = gcParcelas.Rows[i];

                linha.Cells[0].Value = (i + 1);
            }
        }

        private void LimpeFormulario()
        {
            EditeCondicaoPagamento(null);
        }

        private void EditeCondicaoPagamento(CondicaoPagamento condicaoPagamento)
        {
            txtQuantidadeParcelas.Text = string.Empty;
            txtDiaPrimeiraParcela.Text = string.Empty;
            txtDiasEntreParcelas.Text = string.Empty;

            if (condicaoPagamento != null)
            {
                txtId.Text = condicaoPagamento.Id.ToString();
                txtDescricao.Text = condicaoPagamento.Descricao;
                txtDataCadastro.Text = condicaoPagamento.DataCadastro.ToString("dd/MM/yyyy");
                txtValorDisponivelCondicaoPagamento.Text = condicaoPagamento.ValorQueEstaraDisponivel.ToStringEmpty();

                chkDisponivelParaContasAPagar.Checked = condicaoPagamento.EstahDisponivelParaContasAPagar;
                chkDisponivelParaContasAReceber.Checked = condicaoPagamento.EstahDisponivelParaContasAReceber;
                chkDisponivelParaPdv.Checked = condicaoPagamento.EstahDisponivelParaPdv;
                chkPrecisaLiberacaoDoGerente.Checked = condicaoPagamento.PrecisaDaLiberacaoDoGerente;
                chkDisponivelAcimaDoValor.Checked = condicaoPagamento.EstahDisponivelAcimaDeDeterminadoValor;
                chkPadraoAVista.Checked = condicaoPagamento.CondicaoPadraoAVista;

                gcParcelas.Rows.Clear();

                foreach (var parcela in condicaoPagamento.ListaDeParcelas)
                {
                    gcParcelas.Rows.Add(parcela.NumeroParcela, parcela.Dias, parcela.PercentualRateio, parcela.PercentualAcrescimo);
                }

                gcParcelas.Refresh();

                txtId.Enabled = false;
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtValorDisponivelCondicaoPagamento.Text = string.Empty;

                chkDisponivelParaContasAPagar.Checked = false;
                chkDisponivelParaContasAReceber.Checked = false;
                chkDisponivelParaPdv.Checked = false;
                chkPrecisaLiberacaoDoGerente.Checked = false;
                chkDisponivelAcimaDoValor.Checked = false;
                chkPadraoAVista.Checked = false;

                gcParcelas.Rows.Clear();
                gcParcelas.Refresh();

                txtId.Enabled = true;

                txtId.Focus();
            }
        }

        private void GereParcelas()
        {
            int quantidadeParcelas = txtQuantidadeParcelas.Text.ToInt();
            int diaPrimeiraParcela = txtDiaPrimeiraParcela.Text.ToInt();
            int diasEntreParcelas = txtDiasEntreParcelas.Text.ToInt();

            if (!CamposGeracaoParcelasInformados(quantidadeParcelas, diasEntreParcelas))
            {
                return;
            }

            double percentualRateio = Math.Round(100 / (double)quantidadeParcelas, 2);

            var diferencaUltimaParcela = percentualRateio * quantidadeParcelas == 100 ? 0 : 100 - percentualRateio * quantidadeParcelas;

            gcParcelas.Rows.Clear();

            for (int i = 0; i < quantidadeParcelas; i++)
            {
                int numeroParcela = quantidadeParcelas + 1;
                int dias = diaPrimeiraParcela + (diasEntreParcelas * i);

                if (i == quantidadeParcelas - 1)
                {
                    percentualRateio += diferencaUltimaParcela;
                }

                gcParcelas.Rows.Add(numeroParcela, dias, percentualRateio, 0);
            }

            gcParcelas.Refresh();
        }

        private bool CamposGeracaoParcelasInformados(int quantidadeParcelas, int diasEntreParcelas)
        {
            string mensagemDeErro = string.Empty;

            if (quantidadeParcelas == 0)
            {
                mensagemDeErro += "\n\nQuantidade de parcelas não informada!";
            }

            if (quantidadeParcelas > 1 && diasEntreParcelas == 0)
            {
                mensagemDeErro += "\n\nDias entre Parcelas não informada!";
            }

            if (!string.IsNullOrEmpty(mensagemDeErro))
            {
                mensagemDeErro = "Erro ao Gerar Parcelas:" + mensagemDeErro;

                MessageBox.Show(mensagemDeErro, "Erro ao Gerar Parcelas", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        private void PesquiseCondicaoPeloId()
        {
            if (!string.IsNullOrEmpty(txtId.Text) && txtId.Enabled)
            {
                ServicoCondicaoPagamento servicoCondicaoPagamento = new ServicoCondicaoPagamento();
                var condicaoPagamento = servicoCondicaoPagamento.Consulte(txtId.Text.ToInt());

                if (condicaoPagamento == null)
                {
                    MessageBox.Show("Condição de Pagamento não encontrada.", "Condição de Pagamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                EditeCondicaoPagamento(condicaoPagamento);
            }
        }

        #endregion
    }
}
