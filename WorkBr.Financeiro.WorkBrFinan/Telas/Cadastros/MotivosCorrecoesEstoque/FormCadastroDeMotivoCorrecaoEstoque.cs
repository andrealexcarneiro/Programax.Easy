using System;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.MotivoCorrecaoEstoqueServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using System.Collections.Generic;

namespace Programax.Easy.View.Telas.Cadastros.MotivosCorrecoesEstoque
{
    public partial class FormCadastroDeMotivoCorrecaoEstoque: FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroDeMotivoCorrecaoEstoque()
        {
            InitializeComponent();

            PreenchaOStatus();
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.NomeDaTela = "Motivo da Correção do Estoque";

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
            Action actionSalvar = () =>
            {
                MotivoCorrecaoEstoque motivoCorrecaoEstoque = new MotivoCorrecaoEstoque();

                motivoCorrecaoEstoque.Id = txtId.Text.ToInt();
                motivoCorrecaoEstoque.Descricao = txtDescricao.Text;
                motivoCorrecaoEstoque.Status = cboStatus.EditValue.ToString();
                motivoCorrecaoEstoque.DataCadastro = txtDataCadastro.Text.ToDate();

                ServicoMotivoCorrecaoEstoque servicoMotivoCorrecaoEstoque = new ServicoMotivoCorrecaoEstoque();

                if (motivoCorrecaoEstoque.Id == 0)
                {
                    servicoMotivoCorrecaoEstoque.Cadastre(motivoCorrecaoEstoque);
                }
                else
                {
                    servicoMotivoCorrecaoEstoque.Atualize(motivoCorrecaoEstoque);
                }

                txtId.Text = motivoCorrecaoEstoque.Id.ToString();
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
            FormPesquisaDeMotivoCorrecaoEstoque formPesquisaDeMotivoCorrecaoEstoque = new FormPesquisaDeMotivoCorrecaoEstoque();

            var motivoCorrecaoEstoque = formPesquisaDeMotivoCorrecaoEstoque.PesquiseUmaMotivoCorrecaoEstoque();

            if (motivoCorrecaoEstoque != null)
            {
                EditeMotivoCorrecaoEstoque(motivoCorrecaoEstoque);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePeloId();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void LimpeFormulario()
        {
            txtId.Text = string.Empty;
            txtDescricao.Text = string.Empty;

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            cboStatus.EditValue = "A";

            txtId.Enabled = true;
            txtId.Focus();
        }

        private void EditeMotivoCorrecaoEstoque(MotivoCorrecaoEstoque motivoCorrecaoEstoque)
        {
            if (motivoCorrecaoEstoque != null)
            {
                txtId.Text = motivoCorrecaoEstoque.Id.ToString();
                txtDescricao.Text = motivoCorrecaoEstoque.Descricao;

                txtDataCadastro.Text = motivoCorrecaoEstoque.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = motivoCorrecaoEstoque.Status;

                txtId.Enabled = false;

                txtDescricao.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;

                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                cboStatus.EditValue = string.Empty;

                txtId.Focus();

                MessageBox.Show("Motivo da Correcao do Estoque não encontrada", "Motivo da Correcao do Estoque não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PesquisePeloId()
        {
            ServicoMotivoCorrecaoEstoque servicoMotivoCorrecaoEstoque = new ServicoMotivoCorrecaoEstoque();
            var motivoCorrecaoEstoque = servicoMotivoCorrecaoEstoque.Consulte(txtId.Text.ToInt());

            EditeMotivoCorrecaoEstoque(motivoCorrecaoEstoque);
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

        #endregion
    }
}
