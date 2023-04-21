using System;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CorServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using System.Collections.Generic;

namespace Programax.Easy.View.Telas.Cadastros.Cores
{
    public partial class FormCadastroDeCores : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroDeCores()
        {
            InitializeComponent();

            PreenchaOStatus();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.NomeDaTela = "Cadastro de Cor";

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
                Cor cor = new Cor();

                cor.Id = txtId.Text.ToInt();
                cor.Descricao = txtDescricao.Text;
                cor.DataCadastro = txtDataCadastro.Text.ToDate();
                cor.Status = cboStatus.EditValue.ToString();

                ServicoCor servicoCor = new ServicoCor();

                if (cor.Id == 0)
                {
                    servicoCor.Cadastre(cor);
                }
                else
                {
                    servicoCor.Atualize(cor);
                }

                txtId.Text = cor.Id.ToString();
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
            FormPesquisaCores formPesquisaDeCores = new FormPesquisaCores();

            var cor = formPesquisaDeCores.PesquiseUmaCor();

            if (cor != null)
            {
                EditeCor(cor);
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

        private void EditeCor(Cor cor)
        {
            if (cor != null)
            {
                txtId.Text = cor.Id.ToString();
                txtDescricao.Text = cor.Descricao;

                txtDataCadastro.Text = cor.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = cor.Status;

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

                MessageBox.Show("Cor não encontrada", "Cor não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            ServicoCor servicoCor = new ServicoCor();
            var cor = servicoCor.Consulte(txtId.Text.ToInt());

            EditeCor(cor);
        }

        #endregion
    }
}
