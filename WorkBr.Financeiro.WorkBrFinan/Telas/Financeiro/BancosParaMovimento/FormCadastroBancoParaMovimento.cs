using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Financeiro.ContasBancarias;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ContaBancariaServ;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;
using Programax.Easy.Negocio.Financeiro.ContaBancariaObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Financeiro.BancosParaMovimento
{
    public partial class FormCadastroBancoParaMovimento : FormularioPadrao
    {
        #region " CONSTRUTOR "

        private int _IdContaBancaria;

        public FormCadastroBancoParaMovimento()
        {
            InitializeComponent();

            PreenchaOStatus();
            
            this.NomeDaTela = "Cadastro de Banco Para Movimento";

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            BancoParaMovimento banco = new BancoParaMovimento();

            banco.Id = txtId.Text.ToInt();
            banco.NomeBanco = txtNomeBanco.Text;
            banco.DataCadastro = txtDataCadastro.Text.ToDate();
            banco.Status = cboStatus.EditValue.ToString();

            banco.ContaBancaria = _IdContaBancaria != 0 ? new ContaBancaria { Id = _IdContaBancaria } : null;

            banco.TornarPadrao = chkTornarPadrao.Checked;

            Action actionSalvar = () =>
            {
                ServicoBancoParaMovimento servicoBanco = new ServicoBancoParaMovimento();
                
                if(chkTornarPadrao.Checked)
                {
                    var listaDeBancos = servicoBanco.ConsulteLista(null, null);

                    foreach (var item in listaDeBancos)
                    {
                        if (item.TornarPadrao==true)
                        {
                            item.TornarPadrao = false;
                            servicoBanco.Atualize(item);
                        }                        
                    }
                }

                if (banco.Id == 0)
                {
                    servicoBanco.Cadastre(banco);
                }
                else
                {
                    servicoBanco.Atualize(banco);
                }

                txtId.Text = banco.Id.ToString();

                PesquisePeloId();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPesquisaBancoParaMovimento formPesquisaDeBancos = new FormPesquisaBancoParaMovimento();

            var banco = formPesquisaDeBancos.PesquiseUmBanco();

            if (banco != null)
            {
                EditeBanco(banco);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePeloId();
            }
        }

        private void pbPesquisaContaBancaria_Click(object sender, EventArgs e)
        {
            FormContaBancariaPesquisa formContaBancariaPesquisa = new FormContaBancariaPesquisa();

            var contaBancaria = formContaBancariaPesquisa.ExibaPesquisaDeContasBancarias();

            if (contaBancaria != null)
            {
                txtContaBancaria.Text = contaBancaria.NumeroConta;
                lblNomeDoBanco.Text = contaBancaria.Agencia.Banco.Descricao;
                _IdContaBancaria = contaBancaria.Id;
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

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
                
        private void LimpeFormulario()
        {
            EditeBanco(null);
        }

        private void EditeBanco(BancoParaMovimento banco, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (banco != null)
            {
                txtId.Text = banco.Id.ToString();
                txtNomeBanco.Text = banco.NomeBanco;
                txtDataCadastro.Text = banco.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = banco.Status;

                var contaBancaria = new ServicoContaBancaria().Consulte(banco.ContaBancaria.Id.ToInt());

                txtContaBancaria.Text = contaBancaria != null ? contaBancaria.NumeroConta : string.Empty;
                lblNomeDoBanco.Text = contaBancaria != null ? contaBancaria.Agencia.Banco.Descricao : string.Empty;
                _IdContaBancaria = contaBancaria != null ? contaBancaria.Id : 0;

                chkTornarPadrao.Checked = banco.TornarPadrao;

                txtId.Enabled = false;

                txtNomeBanco.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtNomeBanco.Text = string.Empty;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                cboStatus.EditValue = "A";

                txtContaBancaria.Text = string.Empty;
                lblNomeDoBanco.Text = string.Empty;
                _IdContaBancaria = 0;

                chkTornarPadrao.Checked = false;

                txtId.Enabled = true;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Banco não encontrado", "Banco não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                txtId.Focus();
            }
        }
        
        private void PesquisePeloId()
        {
            ServicoBancoParaMovimento servicoBanco = new ServicoBancoParaMovimento();
            var banco = servicoBanco.Consulte(txtId.Text.ToInt());

            EditeBanco(banco, exibirMensagemDeNaoEncontrado: true);
        }

        #endregion
                
    }
}
