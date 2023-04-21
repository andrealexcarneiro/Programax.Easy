using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.MotivoTrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.MotivoTrocaPedidoDeVendaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Cadastros.MotivosTrocaPedidoDeVenda
{
    public partial class FormCadastroDeMotivoTrocaPedidoDeVenda: FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroDeMotivoTrocaPedidoDeVenda()
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
                MotivoTrocaPedidoDeVenda motivoTrocaPedidoDeVenda = new MotivoTrocaPedidoDeVenda();

                motivoTrocaPedidoDeVenda.Id = txtId.Text.ToInt();
                motivoTrocaPedidoDeVenda.Descricao = txtDescricao.Text;
                motivoTrocaPedidoDeVenda.Status = cboStatus.EditValue.ToString();
                motivoTrocaPedidoDeVenda.DataCadastro = txtDataCadastro.Text.ToDate();

                ServicoMotivoTrocaPedidoDeVenda servicoMotivoTrocaPedidoDeVenda = new ServicoMotivoTrocaPedidoDeVenda();

                if (motivoTrocaPedidoDeVenda.Id == 0)
                {
                    servicoMotivoTrocaPedidoDeVenda.Cadastre(motivoTrocaPedidoDeVenda);
                }
                else
                {
                    servicoMotivoTrocaPedidoDeVenda.Atualize(motivoTrocaPedidoDeVenda);
                }

                txtId.Text = motivoTrocaPedidoDeVenda.Id.ToString();
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
            FormPesquisaDeMotivoTrocaPedidoDeVenda formPesquisaDeMotivoTrocaPedidoDeVenda = new FormPesquisaDeMotivoTrocaPedidoDeVenda();

            var motivoTrocaPedidoDeVenda = formPesquisaDeMotivoTrocaPedidoDeVenda.PesquiseUmaMotivoTrocaPedidoDeVenda();

            if (motivoTrocaPedidoDeVenda != null)
            {
                EditeMotivoTrocaPedidoDeVenda(motivoTrocaPedidoDeVenda);
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

        private void EditeMotivoTrocaPedidoDeVenda(MotivoTrocaPedidoDeVenda motivoTrocaPedidoDeVenda)
        {
            if (motivoTrocaPedidoDeVenda != null)
            {
                txtId.Text = motivoTrocaPedidoDeVenda.Id.ToString();
                txtDescricao.Text = motivoTrocaPedidoDeVenda.Descricao;

                txtDataCadastro.Text = motivoTrocaPedidoDeVenda.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = motivoTrocaPedidoDeVenda.Status;

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
            ServicoMotivoTrocaPedidoDeVenda servicoMotivoTrocaPedidoDeVenda = new ServicoMotivoTrocaPedidoDeVenda();
            var motivoTrocaPedidoDeVenda = servicoMotivoTrocaPedidoDeVenda.Consulte(txtId.Text.ToInt());

            EditeMotivoTrocaPedidoDeVenda(motivoTrocaPedidoDeVenda);
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
