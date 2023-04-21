using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.TabelaPrecoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Produtos.TabelaDePreco
{
    public partial class FormTabelaDePreco : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private ServicoTabelaPreco _servicoTabelaPreco;

        #endregion

        #region " CONSTRUTOR "

        public FormTabelaDePreco()
        {
            InitializeComponent();

            _servicoTabelaPreco = new ServicoTabelaPreco();

            PreenchaOsItensDosStatus();
            PreenchaADataCadastroComADataAtual();

            this.NomeDaTela = "Cadastro de Tabela de Preço";

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Salve();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePeloId();
            }
        }

        private void pbPesquisaCadastro_Click(object sender, EventArgs e)
        {
            FormTabelaDePrecoPesquisa formTabelaPesquisa = new FormTabelaDePrecoPesquisa();

            var tabelaPreco = formTabelaPesquisa.PesquiseTabelaDePreco();

            if (tabelaPreco != null)
            {
                EditarTabelaDePreco(tabelaPreco);
            }
        }

        private void txtAcrescimo_EditValueChanged(object sender, EventArgs e)
        {
            HabiliteCampoDecrescimo();
        }

        private void txtDecrescimo_EditValueChanged(object sender, EventArgs e)
        {
            HabiliteCampoAcrescimo();
        }

        private void rdbAcrescimoValor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAcrescimoValor.Checked)
            {
                txtAcrescimo.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,2})?";
            }
            else
            {
                txtAcrescimo.Properties.Mask.EditMask = @"[0-9]{1,2}([\.\,][0-9]{0,2})?";
            }

            txtAcrescimo.Text = txtAcrescimo.Text;
        }

        private void rdbDecrescimoValor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDecrescimoValor.Checked)
            {
                txtDecrescimo.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,2})?";
            }
            else
            {
                txtDecrescimo.Properties.Mask.EditMask = @"[0-9]{1,2}([\.\,][0-9]{0,2})?";
            }

            txtDecrescimo.Text = txtDecrescimo.Text;
        }

        private void rdbFreteValor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbFreteValor.Checked)
            {
                txtFrete.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,2})?";
            }
            else
            {
                txtFrete.Properties.Mask.EditMask = @"[0-9]{1,2}([\.\,][0-9]{0,2})?";
            }

            txtFrete.Text = txtFrete.Text;
        }

        private void rdbAcrescimoPercentual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }

        private void rdbDecrescimoPercentual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }

        private void rdbFretePercentual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFrete.Focus();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " EDIÇÃO TABELA PREÇO "

        private void EditarTabelaDePreco(TabelaPreco tabelaPrecoSelecionada, bool exibirMensagemRegistroNaoEncontrado = false)
        {
            if (tabelaPrecoSelecionada != null)
            {
                txtDescricao.Text = tabelaPrecoSelecionada.NomeTabela;
                txtId.Text = tabelaPrecoSelecionada.Id.ToString();

                txtAcrescimo.Text = tabelaPrecoSelecionada.Acrescimo.ToString("0.00");
                txtDecrescimo.Text = tabelaPrecoSelecionada.Decrescimo.ToString("0.00");
                txtFrete.Text = tabelaPrecoSelecionada.Frete.ToString("0.00");

                rdbAcrescimoValor.Checked = true;
                rdbDecrescimoValor.Checked = true;
                rdbFreteValor.Checked = true;

                rdbAcrescimoPercentual.Checked = tabelaPrecoSelecionada.AcrescimoEhPercentual;
                rdbDecrescimoPercentual.Checked = tabelaPrecoSelecionada.DecrescimoEhPercentual;
                rdbFretePercentual.Checked = tabelaPrecoSelecionada.FreteEhPercentual;

                txtDtCadastro.Text = tabelaPrecoSelecionada.DataDeCadastro.ToString("dd/MM/yyyy");
                dtValidade.DateTime = tabelaPrecoSelecionada.DataDeValidade.GetValueOrDefault();

                if (tabelaPrecoSelecionada.DataDeValidade == null)
                {
                    dtValidade.Text = string.Empty;
                }

                cboStatus.EditValue = tabelaPrecoSelecionada.Status;

                txtDescricao.Focus();

                txtId.Enabled = false;
            }
            else
            {
                txtDescricao.Text = string.Empty;
                txtId.Text = string.Empty;

                txtAcrescimo.Text = string.Empty;
                txtDecrescimo.Text = string.Empty;
                txtFrete.Text = string.Empty;

                rdbAcrescimoPercentual.Checked = true;
                rdbDecrescimoPercentual.Checked = true;
                rdbFretePercentual.Checked = true;

                txtDtCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                dtValidade.Text = string.Empty;

                cboStatus.EditValue = "A";

                txtId.Enabled = true;

                txtId.Focus();

                if (exibirMensagemRegistroNaoEncontrado)
                {
                    MessageBox.Show("Tabela de Preço não encontrada.", "Tabela de Preço não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        #endregion

        #region " LIMPAR FORMULÁRIO "

        private void LimpeFormulario()
        {
            EditarTabelaDePreco(null);
        }

        #endregion

        #region " PREENCHA COMBOS E VALORES PADRÕES "

        private void PreenchaOsItensDosStatus()
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

        private void PreenchaADataCadastroComADataAtual()
        {
            txtDtCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }

        #endregion

        #region " HABILITAÇÃO DE CAMPOS "

        private void HabiliteCampoAcrescimo()
        {
            if (string.IsNullOrEmpty(txtDecrescimo.Text) || txtDecrescimo.Text.ToDouble() == 0)
            {
                txtAcrescimo.Enabled = true;
            }
            else
            {
                txtAcrescimo.Enabled = false;
                txtAcrescimo.Text = string.Empty;
            }
        }

        private void HabiliteCampoDecrescimo()
        {
            if (string.IsNullOrEmpty(txtAcrescimo.Text) || Convert.ToDouble(txtAcrescimo.Text) == 0)
            {
                txtDecrescimo.Enabled = true;
            }
            else
            {
                txtDecrescimo.Enabled = false;
                txtDecrescimo.Text = string.Empty;
            }
        }

        #endregion

        #region " SALVAR TABELA DE PREÇO "

        private void Salve()
        {
            Action actionSalvar = () =>
            {
                TabelaPreco tabelaDePreco = RetorneATabelaDePreco();

                ServicoTabelaPreco servicoTabelaPreco = new ServicoTabelaPreco();

                if (string.IsNullOrEmpty(txtId.Text))
                {
                    servicoTabelaPreco.Cadastre(tabelaDePreco);
                }
                else
                {
                    servicoTabelaPreco.Atualize(tabelaDePreco);
                }

                txtId.Text = tabelaDePreco.Id.ToString();

                PesquisePeloId();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private TabelaPreco RetorneATabelaDePreco()
        {
            TabelaPreco tabelaDePreco = new TabelaPreco();

            tabelaDePreco.Id = txtId.Text.ToInt();

            tabelaDePreco.Acrescimo = txtAcrescimo.Text.ToDouble();
            tabelaDePreco.Decrescimo = txtDecrescimo.Text.ToDouble();
            tabelaDePreco.Frete = txtFrete.Text.ToDouble();

            tabelaDePreco.AcrescimoEhPercentual = rdbAcrescimoPercentual.Checked;
            tabelaDePreco.DecrescimoEhPercentual = rdbDecrescimoPercentual.Checked;
            tabelaDePreco.FreteEhPercentual = rdbFretePercentual.Checked;

            tabelaDePreco.DataDeCadastro = txtDtCadastro.Text.ToDate();
            tabelaDePreco.DataDeValidade = dtValidade.Text.ToDateNullabel();

            tabelaDePreco.NomeTabela = txtDescricao.Text;
            tabelaDePreco.Status = cboStatus.EditValue.ToString();

            return tabelaDePreco;
        }

        private void PesquisePeloId()
        {
            ServicoTabelaPreco servicoTabelaPreco = new ServicoTabelaPreco();

            var tabelaPreco = servicoTabelaPreco.Consulte(txtId.Text.ToInt());

            EditarTabelaDePreco(tabelaPreco, exibirMensagemRegistroNaoEncontrado: true);
        }

        #endregion        

        #endregion
    }
}
