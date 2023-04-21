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

namespace Programax.Easy.View.Telas.Financeiro.Categorias
{
    public partial class FormCadastroCategoriasFinanceiras : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroCategoriasFinanceiras()
        {
            InitializeComponent();

            this.NomeDaTela = "Cadastro de Categorias Financeiras";

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
                categoria.DataCadastro = txtDataCadastro.Text.ToDate();
                categoria.Status = cboStatus.EditValue.ToString();
                categoria.TipoCategoria = (EnumTipoCategoria)pnlTipoCategoria.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();

                if (cboCategorias.EditValue != null)
                {
                    categoria.GrupoCategoria = new GrupoCategoria { Id = cboCategorias.EditValue.ToInt() };
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
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

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
            cboCategorias.EditValue = categoria != null ? (int?)categoria.Id : null;
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

            cboStatus.Properties.DataSource = listaDeItensParaOComboBox;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";

            cboStatus.EditValue = "A";
        }

        private void PreenchaCboGrupoCategoria()
        {
            ServicoGrupoCategoria servicoGrupoCategoria = new ServicoGrupoCategoria();

            var gruposCategorias = servicoGrupoCategoria.ConsulteListaAtiva();

            gruposCategorias.Insert(0, null);

            cboCategorias.Properties.DataSource = gruposCategorias;
            cboCategorias.Properties.DisplayMember = "Descricao";
            cboCategorias.Properties.ValueMember = "Id";

            if (cboCategorias.EditValue != null)
            {
                if (!gruposCategorias.Exists(caregoria => caregoria != null && caregoria.Id == cboCategorias.EditValue.ToInt()))
                {
                    cboCategorias.EditValue = null;
                }
            }
        }

        private void LimpeFormulario()
        {
            txtId.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            cboStatus.EditValue = "A";
            cboCategorias.EditValue = null;

            txtId.Enabled = true;
            txtId.Focus();
        }

        private void EditeCategoria(CategoriaFinanceira categoria)
        {
            if (categoria != null)
            {
                txtId.Text = categoria.Id.ToString();
                txtDescricao.Text = categoria.Descricao;
                txtDataCadastro.Text = categoria.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = categoria.Status;
                rdbTipoCategoriaDespesa.Checked = categoria.TipoCategoria == EnumTipoCategoria.DESPESA? true : false;
                rdbTipoCategoriaReceita.Checked = categoria.TipoCategoria == EnumTipoCategoria.RECEITA ? true : false;

                if (categoria.GrupoCategoria != null)
                {
                    cboCategorias.EditValue = categoria.GrupoCategoria.Id;
                }
                else
                {
                    cboCategorias.EditValue = null;
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
    }
}
