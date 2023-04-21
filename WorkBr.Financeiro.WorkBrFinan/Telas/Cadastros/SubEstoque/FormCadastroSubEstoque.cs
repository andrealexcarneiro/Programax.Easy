using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Cadastros.SubEstoqueServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Cadastros.Marcas
{
    public partial class FormCadastroSubEstoque : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroSubEstoque()
        {
            InitializeComponent();
            
            this.ActiveControl = txtId;

            this.NomeDaTela = "Cadastro de Sub Estoque";
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
                SubEstoque subestoque = new SubEstoque();

                subestoque.Id = txtId.Text.ToInt();
                subestoque.Descricao = txtDescricao.Text;
                subestoque.DataCadastro = txtDataCadastro.Text.ToDate();
                subestoque.Ativo = cboStatus.EditValue.ToString();

                ServicoSubEstoque servicoSubEstoque = new ServicoSubEstoque();

                if (subestoque.Id == 0)
                {
                    servicoSubEstoque.Cadastre(subestoque);
                }
                else
                {
                    servicoSubEstoque.Atualize(subestoque);
                }

                txtId.Text = subestoque.Id.ToString();
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
            FormSubEstoquePesquisa formSubEstoquePesquisa = new FormSubEstoquePesquisa();

            var subestoque = formSubEstoquePesquisa.ExibaPesquisaDeMarca();

            if (subestoque != null)
            {
                EditeMarca(subestoque);
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

        private void EditeMarca(SubEstoque subestoque)
        {
            if (subestoque != null)
            {
                txtId.Text = subestoque.Id.ToString();
                txtDescricao.Text = subestoque.Descricao;
                txtDataCadastro.Text = subestoque.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = subestoque.Ativo;

                txtId.Enabled = false;

                txtDescricao.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;

                txtId.Focus();

                MessageBox.Show("Sub Estoque não encontrado", "Sub Estoque não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PesquisePeloId()
        {
            ServicoSubEstoque servicoSubestoque = new ServicoSubEstoque();
            var subestoque = servicoSubestoque.Consulte(txtId.Text.ToInt());

           

            EditeMarca(subestoque);
        }

        #endregion

        private void txtId_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
