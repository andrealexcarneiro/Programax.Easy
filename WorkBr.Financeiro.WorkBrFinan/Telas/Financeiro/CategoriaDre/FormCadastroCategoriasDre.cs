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
using Programax.Easy.Negocio.Financeiro.CategoriaDreObj.ObjetoDeNeocio;
using Programax.Easy.Servico.Financeiro.CategoriaDreServ;

namespace Programax.Easy.View.Telas.Financeiro.Categorias
{
    public partial class FormCadastroCategoriasDre : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroCategoriasDre()
        {
            InitializeComponent();

            this.NomeDaTela = "Cadastro de Categorias D.R.E";

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
                CategoriaDre categoria = new CategoriaDre();

                categoria.Id = txtId.Text.ToInt();
                categoria.Descricao = txtDescricao.Text;
                categoria.DataCadastro = txtDataCadastro.Text.ToDate();
                categoria.Status = cboStatus.EditValue.ToString();
                categoria.TipoCategoria = (EnumTipoCategoria)pnlTipoCategoria.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();

                if (cboCategorias.EditValue != null)
                {
                    categoria.SubGrupoCategoria = new SubGrupoCategoria { Id = cboCategorias.EditValue.ToInt() };
                }

                ServicoCategoriaDre servicoCategoriadre = new ServicoCategoriaDre();

                if (categoria.Id == 0)
                {
                    servicoCategoriadre.Cadastre(categoria);
                }
                else
                {
                    if (categoria.Id == 2 || categoria.Id == 24 || categoria.Id == 18)
                    {
                        MessageBox.Show("Esta Categoria não pode ser alterada!","Alteração",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    servicoCategoriadre.Atualize(categoria);
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
            FormPesquisaCategoriasDre formPesquisaDre = new FormPesquisaCategoriasDre();

            var categoria = formPesquisaDre.PesquiseUmaCategoria();

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

        public void ExibaCadastroGrupo(CategoriaDre categoria)
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
            ServicoSubGrupoCategoria servicoGrupoCategoria = new ServicoSubGrupoCategoria();

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

        private void EditeCategoria(CategoriaDre categoria)
        {
            if (categoria != null)
            {
                txtId.Text = categoria.Id.ToString();
                txtDescricao.Text = categoria.Descricao;
                txtDataCadastro.Text = categoria.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = categoria.Status;
                rdbTipoCategoriaDespesa.Checked = categoria.TipoCategoria == EnumTipoCategoria.DESPESA? true : false;
                rdbTipoCategoriaReceita.Checked = categoria.TipoCategoria == EnumTipoCategoria.RECEITA ? true : false;

                if (categoria.SubGrupoCategoria != null)
                {
                    cboCategorias.EditValue = categoria.SubGrupoCategoria.Id;
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
            FormCadastroSubGruposCategorias formCadastroSubGruposCategorias = new FormCadastroSubGruposCategorias();
            formCadastroSubGruposCategorias.ShowDialog();

            PreenchaCboGrupoCategoria();
        }

        private void PesquisePeloId()
        {
            ServicoCategoriaDre servicoCategoria = new ServicoCategoriaDre();
            var categoria = servicoCategoria.Consulte(txtId.Text.ToInt());

            EditeCategoria(categoria);
        }

        #endregion

        private void cboCategorias_EditValueChanged(object sender, EventArgs e)
        {
            rdbTipoCategoriaDespesa.Enabled = true;
            rdbTipoCategoriaReceita.Enabled = true;

            if (cboCategorias.Text.Contains("RECEBIMENTO"))
            {
                rdbTipoCategoriaDespesa.Enabled = false;
                rdbTipoCategoriaReceita.Checked = true;
            }
            else
            {
                rdbTipoCategoriaReceita.Enabled = false;
                rdbTipoCategoriaDespesa.Checked = true;
            }
        }
    }
}
