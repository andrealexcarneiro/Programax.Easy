using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.OrigemClienteServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Cadastros.OrigemClienteObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Cadastros.OrigensClientes
{
    public partial class FormCadastroOrigemCliente : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroOrigemCliente()
        {
            InitializeComponent();

            this.ActiveControl = txtId;

            this.NomeDaTela = "Cadastro de origem do cliente";

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
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
                OrigemCliente origemCliente = new OrigemCliente();

                origemCliente.Id = txtId.Text.ToInt();
                origemCliente.Descricao = txtDescricao.Text;
                origemCliente.Status = cboStatus.EditValue.ToString();
                origemCliente.DataCadastro = txtDataCadastro.Text.ToDate();

                ServicoOrigemCliente servicoOrigemCliente = new ServicoOrigemCliente();

                if (origemCliente.Id == 0)
                {
                    servicoOrigemCliente.Cadastre(origemCliente);
                }
                else
                {
                    servicoOrigemCliente.Atualize(origemCliente);
                }

                txtId.Text = origemCliente.Id.ToString();

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
            FormPesquisaOrigemCliente formPesquisaDeOrigemClientes = new FormPesquisaOrigemCliente();

            var origemCliente = formPesquisaDeOrigemClientes.PesquiseUmOrigemCliente();

            if (origemCliente != null)
            {
                EditeOrigemCliente(origemCliente);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePeloId();
            }
        }

        private void FormCadastroOrigemCliente_Load(object sender, EventArgs e)
        {
            PreenchaOStatus();
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
            EditeOrigemCliente(null);
        }

        private void EditeOrigemCliente(OrigemCliente origemCliente, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (origemCliente != null)
            {
                txtId.Text = origemCliente.Id.ToString();
                txtDescricao.Text = origemCliente.Descricao;
                cboStatus.EditValue = origemCliente.Status;

                txtDataCadastro.Text = origemCliente.DataCadastro.ToString("dd/MM/yyyy");

                txtId.Enabled = false;

                txtDescricao.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;

                cboStatus.EditValue = "A";

                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                txtId.Enabled = true;
                txtId.Focus();

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Origem Cliente não encontrada", "Origem Cliente não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void PesquisePeloId()
        {
            ServicoOrigemCliente servicoOrigemCliente = new ServicoOrigemCliente();
            var origemCliente = servicoOrigemCliente.Consulte(txtId.Text.ToInt());

            EditeOrigemCliente(origemCliente, exibirMensagemDeNaoEncontrado : true);
        }

        #endregion
    }
}
