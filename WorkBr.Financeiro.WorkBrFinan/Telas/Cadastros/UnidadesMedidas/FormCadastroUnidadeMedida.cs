using System;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.UnidadeMedidaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using System.Collections.Generic;

namespace Programax.Easy.View.Telas.Cadastros.UnidadesMedidas
{
    public partial class FormCadastroUnidadeMedida : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroUnidadeMedida()
        {
            InitializeComponent();

            PreenchaOStatus();
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.NomeDaTela = "Cadastro de Unidade de Medida";

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
            UnidadeMedida unidadeMedida = new UnidadeMedida();

            unidadeMedida.Id = txtId.Text.ToInt();
            unidadeMedida.Descricao = txtDescricao.Text;
            unidadeMedida.Abreviacao = txtAbreviacao.Text;
            unidadeMedida.Status = cboStatus.EditValue.ToString();
            unidadeMedida.DataCadastro = txtDataCadastro.Text.ToDate();

            Action actionSalvar = () =>
            {
                ServicoUnidadeMedida servicoUnidadeMedida = new ServicoUnidadeMedida();

                if (unidadeMedida.Id == 0)
                {
                    servicoUnidadeMedida.Cadastre(unidadeMedida);
                }
                else
                {
                    servicoUnidadeMedida.Atualize(unidadeMedida);
                }

                txtId.Text = unidadeMedida.Id.ToString();
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
            FormPesquisaUnidadeMedida formPesquisaDeUnidadeMedidaes = new FormPesquisaUnidadeMedida();

            var unidadeMedida = formPesquisaDeUnidadeMedidaes.PesquiseUmaUnidadeMedida();

            if (unidadeMedida != null)
            {
                EditeUnidadeMedida(unidadeMedida);
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
            txtAbreviacao.Text = string.Empty;

            cboStatus.EditValue = "A";
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            txtId.Enabled = true;
            txtId.Focus();
        }

        private void EditeUnidadeMedida(UnidadeMedida unidadeMedida)
        {
            if (unidadeMedida != null)
            {
                txtId.Text = unidadeMedida.Id.ToString();
                txtDescricao.Text = unidadeMedida.Descricao;
                txtAbreviacao.Text = unidadeMedida.Abreviacao;

                cboStatus.EditValue = unidadeMedida.Status;
                txtDataCadastro.Text = unidadeMedida.DataCadastro.ToString("dd/MM/yyyy");

                txtId.Enabled = false;

                txtDescricao.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;

                cboStatus.EditValue = "A";
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                txtId.Focus();

                MessageBox.Show("Unidade de medida não encontrada", "UnidadeMedida não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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

        private void PesquisePeloId()
        {
            ServicoUnidadeMedida servicoUnidadeMedida = new ServicoUnidadeMedida();
            var unidadeMedida = servicoUnidadeMedida.Consulte(txtId.Text.ToInt());

            EditeUnidadeMedida(unidadeMedida);
        }

        #endregion
    }
}
