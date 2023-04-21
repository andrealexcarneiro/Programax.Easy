using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.CategoriaServ;
using Programax.Easy.Servico.Financeiro.GruposCategoriasServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Cadastros.GruposCategorias;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using System.Linq;
using Programax.Easy.View.Telas.Financeiro.Categorias;

namespace Programax.Easy.View.Telas.Financeiro.RelacionarCategorias
{
    public partial class FormRelacionarCategoriasFinanceiras : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormRelacionarCategoriasFinanceiras()
        {
            InitializeComponent();

            this.NomeDaTela = "Relacionar Categorias";

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
                CategoriaFinanceira categoria = new CategoriaFinanceira();

                categoria.Id = txtId.Text.ToInt();
                categoria.Descricao = txtDescricao.Text;
               
                if (cboGrupos.EditValue != null)
                {
                    categoria.SubGrupoCategoria = new SubGrupoCategoria { Id = cboGrupos.EditValue.ToInt() };
                }

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
            FormPesquisaCategoriasFinanceiras formPesquisaDeCategorias = new FormPesquisaCategoriasFinanceiras();

            var categoria = formPesquisaDeCategorias.PesquiseUmaCategoria();

            if (categoria != null)
            {
                EditeCategoria(categoria);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePeloId();
            }
        }

        private void FormCadastroCategoriasFinanceiras_Load(object sender, EventArgs e)
        {
            PreenchaStatus();
            PreenchaCboGrupoCategoria();
        }

        private void btnAtalhoCategoria_Click(object sender, EventArgs e)
        {
            ExibaCadastroGrupoCategoria();
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public void ExibaCadastroGrupo(CategoriaFinanceira categoria)
        {
            cboGrupos.EditValue = categoria != null ? (int?)categoria.Id : null;
            this.ShowDialog();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaStatus()
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
            
        }

        private void PreenchaCboGrupoCategoria()
        {
            ServicoGrupoCategoria servicoGrupoCategoria = new ServicoGrupoCategoria();

            var gruposCategorias = servicoGrupoCategoria.ConsulteListaAtiva();

            gruposCategorias.Insert(0, null);

            cboGrupos.Properties.DataSource = gruposCategorias;
            cboGrupos.Properties.DisplayMember = "Descricao";
            cboGrupos.Properties.ValueMember = "Id";

            if (cboGrupos.EditValue != null)
            {
                if (!gruposCategorias.Exists(caregoria => caregoria != null && caregoria.Id == cboGrupos.EditValue.ToInt()))
                {
                    cboGrupos.EditValue = null;
                }
            }
        }

        private void LimpeFormulario()
        {
            txtId.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            
            cboGrupos.EditValue = null;

            txtId.Enabled = true;
            txtId.Focus();
        }

        private void EditeCategoria(CategoriaFinanceira categoria)
        {
            if (categoria != null)
            {
                txtId.Text = categoria.Id.ToString();
                txtDescricao.Text = categoria.Descricao;
               
                if (categoria.SubGrupoCategoria != null)
                {
                    cboGrupos.EditValue = categoria.SubGrupoCategoria.Id;
                }
                else
                {
                    cboGrupos.EditValue = null;
                }

                txtId.Enabled = false;

                txtDescricao.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;

                txtId.Focus();

                MessageBox.Show("Unidade de medida não encontrada", "Grupo não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ExibaCadastroGrupoCategoria()
        {
            FormCadastroGruposCategorias formCadastroGruposCategorias = new FormCadastroGruposCategorias();
            formCadastroGruposCategorias.ShowDialog();

            PreenchaCboGrupoCategoria();
        }

        private void PesquisePeloId()
        {
            ServicoCategoria servicoCategoria = new ServicoCategoria();
            var categoria = servicoCategoria.Consulte(txtId.Text.ToInt());

            EditeCategoria(categoria);
        }

        #endregion

        private void panelConteudo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labCodigo_Click(object sender, EventArgs e)
        {

        }
    }
}
