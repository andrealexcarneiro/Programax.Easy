using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Cadastros.Marcas
{
    public partial class FormCadastroMarca : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroMarca()
        {
            InitializeComponent();
            
            this.ActiveControl = txtId;

            this.NomeDaTela = "Cadastro de Marca";
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
                Marca marca = new Marca();

                marca.Id = txtId.Text.ToInt();
                marca.Descricao = txtDescricao.Text;
                marca.DataCadastro = txtDataCadastro.Text.ToDate();
                marca.Ativo = cboStatus.EditValue.ToString();

                ServicoMarca servicoMarca = new ServicoMarca();

                if (marca.Id == 0)
                {
                    servicoMarca.Cadastre(marca);
                }
                else
                {
                    servicoMarca.Atualize(marca);
                }

                txtId.Text = marca.Id.ToString();
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
            FormMarcasPesquisa formMarcasPesquisa = new FormMarcasPesquisa();

            var marca = formMarcasPesquisa.ExibaPesquisaDeMarca();

            if (marca != null)
            {
                EditeMarca(marca);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePeloId();
            }
        }

        private void FormCadastroMarca_Load(object sender, EventArgs e)
        {
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

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
            txtId.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            cboStatus.EditValue = "A";

            txtId.Enabled = true;
            txtId.Focus();
        }

        private void EditeMarca(Marca marca)
        {
            if (marca != null)
            {
                txtId.Text = marca.Id.ToString();
                txtDescricao.Text = marca.Descricao;
                txtDataCadastro.Text = marca.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = marca.Ativo;

                txtId.Enabled = false;

                txtDescricao.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;

                txtId.Focus();

                MessageBox.Show("Marca não encontrada", "Marca não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PesquisePeloId()
        {
            ServicoMarca servicoMarca = new ServicoMarca();
            var marca = servicoMarca.Consulte(txtId.Text.ToInt());

            EditeMarca(marca);
        }

        #endregion
    }
}
