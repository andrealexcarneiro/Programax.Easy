using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.FabricanteServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Cadastros.Fabricantes
{
    public partial class FormCadastroFabricante : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroFabricante()
        {
            InitializeComponent();
            
            this.ActiveControl = txtId;

            this.NomeDaTela = "Cadastro de Fabricante";
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
                Fabricante fabricante = new Fabricante();

                fabricante.Id = txtId.Text.ToInt();
                fabricante.Descricao = txtDescricao.Text;
                fabricante.DataCadastro = txtDataCadastro.Text.ToDate();
                fabricante.Ativo = cboStatus.EditValue.ToString();

                ServicoFabricante servicoFabricante = new ServicoFabricante();

                if (fabricante.Id == 0)
                {
                    servicoFabricante.Cadastre(fabricante);
                }
                else
                {
                    servicoFabricante.Atualize(fabricante);
                }

                txtId.Text = fabricante.Id.ToString();
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
            FormFabricantesPesquisa formFabricantesPesquisa = new FormFabricantesPesquisa();

            var fabricante = formFabricantesPesquisa.ExibaPesquisaDeFabricante();

            if (fabricante != null)
            {
                EditeFabricante(fabricante);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePeloId();
            }
        }

        private void FormCadastroFabricante_Load(object sender, EventArgs e)
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

        private void EditeFabricante(Fabricante fabricante)
        {
            if (fabricante != null)
            {
                txtId.Text = fabricante.Id.ToString();
                txtDescricao.Text = fabricante.Descricao;
                txtDataCadastro.Text = fabricante.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = fabricante.Ativo;

                txtId.Enabled = false;

                txtDescricao.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;

                txtId.Focus();

                MessageBox.Show("Fabricante não encontrada", "Fabricante não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PesquisePeloId()
        {
            ServicoFabricante servicoFabricante = new ServicoFabricante();
            var fabricante = servicoFabricante.Consulte(txtId.Text.ToInt());

            EditeFabricante(fabricante);
        }

        #endregion
    }
}
