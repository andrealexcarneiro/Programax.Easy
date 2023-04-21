using System;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.TamanhoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using System.Collections.Generic;

namespace Programax.Easy.View.Telas.Cadastros.Tamanhos
{
    public partial class FormCadastroDeTamanhos : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroDeTamanhos()
        {
            InitializeComponent();

            PreenchaOStatus();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.NomeDaTela = "Cadastro de Tamanho";

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
                Tamanho tamanho = new Tamanho();

                tamanho.Id = txtId.Text.ToInt();
                tamanho.Descricao = txtDescricao.Text;
                tamanho.Status = cboStatus.EditValue.ToString();
                tamanho.DataCadastro = txtDataCadastro.Text.ToDate();

                ServicoTamanho servicoTamanho = new ServicoTamanho();

                if (tamanho.Id == 0)
                {
                    servicoTamanho.Cadastre(tamanho);
                }
                else
                {
                    servicoTamanho.Atualize(tamanho);
                }

                txtId.Text = tamanho.Id.ToString();
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
            FormPesquisaTamanhos formPesquisaDeTamanhos = new FormPesquisaTamanhos();

            var tamanho = formPesquisaDeTamanhos.PesquiseUmaTamanho();

            if (tamanho != null)
            {
                EditeTamanho(tamanho);
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

        private void EditeTamanho(Tamanho tamanho)
        {
            if (tamanho != null)
            {
                txtId.Text = tamanho.Id.ToString();
                txtDescricao.Text = tamanho.Descricao;

                txtDataCadastro.Text = tamanho.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = tamanho.Status;

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

                MessageBox.Show("Tamanho não encontrada", "Tamanho não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            ServicoTamanho servicoTamanho = new ServicoTamanho();
            var tamanho = servicoTamanho.Consulte(txtId.Text.ToInt());

            EditeTamanho(tamanho);
        }

        #endregion
    }
}
