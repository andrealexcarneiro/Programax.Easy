using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Cadastros.Categorias
{
    public partial class FormCadastroCategoria : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroCategoria()
        {
            InitializeComponent();

            this.ActiveControl = txtId;

            this.NomeDaTela = "Cadastro da Categoria dos Produtos";
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
                Categoria categoria = new Categoria();

                categoria.Id = txtId.Text.ToInt();
                categoria.Descricao = txtDescricao.Text;
                categoria.DataCadastro = txtDataCadastro.Text.ToDate();
                categoria.Status = cboStatus.EditValue.ToString();

                ServicoCategoria servicoCategoria = new ServicoCategoria();

                if (categoria.Id == 0)
                {
                    servicoCategoria.Cadastre(categoria);
                }
                else
                {
                    servicoCategoria.Atualize(categoria);
                }

                txtId.Text = categoria.Id.ToString();
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
            FormPesquisaCategoria formPesquisaDeCategoriaes = new FormPesquisaCategoria();

            var caregoria = formPesquisaDeCategoriaes.PesquiseUmCategoria();

            if (caregoria != null)
            {
                EditeCategoria(caregoria);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePeloId();
            }
        }

        private void FormCadastroCategoria_Load(object sender, EventArgs e)
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

        private void EditeCategoria(Categoria caregoria)
        {
            if (caregoria != null)
            {
                txtId.Text = caregoria.Id.ToString();
                txtDescricao.Text = caregoria.Descricao;
                txtDataCadastro.Text = caregoria.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = caregoria.Status;

                txtId.Enabled = false;

                txtDescricao.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;

                txtId.Focus();

                MessageBox.Show("Categoria não encontrada", "Categoria não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PesquisePeloId()
        {
            ServicoCategoria servicoCategoria = new ServicoCategoria();
            var caregoria = servicoCategoria.Consulte(txtId.Text.ToInt());

            EditeCategoria(caregoria);
        }

        #endregion
    }
}
