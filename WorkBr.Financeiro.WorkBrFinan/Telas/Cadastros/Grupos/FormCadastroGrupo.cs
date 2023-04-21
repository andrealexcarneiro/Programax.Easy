using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Cadastros.Categorias;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using System.Linq;
using System.Drawing;

namespace Programax.Easy.View.Telas.Cadastros.Grupos
{
    public partial class FormCadastroGrupo : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroGrupo()
        {
            InitializeComponent();

            this.NomeDaTela = "Cadastro do Grupo Dos Produtos";

            TrateUsuarioNaoTemPermissaoCadastroCategoria();

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
                Grupo grupo = new Grupo();

                grupo.Id = txtId.Text.ToInt();
                grupo.Descricao = txtDescricao.Text;
                grupo.DataCadastro = txtDataCadastro.Text.ToDate();
                grupo.Status = cboStatus.EditValue.ToString();

                if (cboCategorias.EditValue != null)
                {
                    grupo.Categoria = new Categoria { Id = cboCategorias.EditValue.ToInt() };
                }

                ServicoGrupo servicoGrupo = new ServicoGrupo();

                if (grupo.Id == 0)
                {
                    servicoGrupo.Cadastre(grupo);
                }
                else
                {
                    servicoGrupo.Atualize(grupo);
                }

                txtId.Text = grupo.Id.ToString();
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
            FormPesquisaGrupo formPesquisaDeGrupoes = new FormPesquisaGrupo();

            var grupo = formPesquisaDeGrupoes.PesquiseUmGrupo();

            if (grupo != null)
            {
                EditeGrupo(grupo);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePeloId();
            }
        }

        private void FormCadastroGrupo_Load(object sender, EventArgs e)
        {
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            PreenchaOStatus();
            PreenchaCboCategoria();
        }

        private void btnAtalhoCategoria_Click(object sender, EventArgs e)
        {
            ExibaCadastroCategoria();
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public void ExibaCadastroGrupo(Categoria categoria)
        {
            cboCategorias.EditValue = categoria != null ? (int?)categoria.Id : null;
            this.ShowDialog();
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

        private void PreenchaCboCategoria()
        {
            ServicoCategoria servicoLinha = new ServicoCategoria();

            var categorias = servicoLinha.ConsulteListaAtiva();

            categorias.Insert(0, null);

            cboCategorias.Properties.DataSource = categorias;
            cboCategorias.Properties.DisplayMember = "Descricao";
            cboCategorias.Properties.ValueMember = "Id";

            if (cboCategorias.EditValue != null)
            {
                if (!categorias.Exists(caregoria => caregoria != null && caregoria.Id == cboCategorias.EditValue.ToInt()))
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

        private void EditeGrupo(Grupo grupo)
        {
            if (grupo != null)
            {
                txtId.Text = grupo.Id.ToString();
                txtDescricao.Text = grupo.Descricao;
                txtDataCadastro.Text = grupo.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = grupo.Status;

                if (grupo.Categoria != null)
                {
                    cboCategorias.EditValue = grupo.Categoria.Id;
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

        private void ExibaCadastroCategoria()
        {
            FormCadastroCategoria formCadastroCategoria = new FormCadastroCategoria();
            formCadastroCategoria.ShowDialog();

            PreenchaCboCategoria();
        }

        private void TrateUsuarioNaoTemPermissaoCadastroCategoria()
        {
            var permissao = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.CATEGORIAS);

            if (!permissao.Alterar)
            {
                cboCategorias.Size = new Size(pnlCategoria.Width, cboCategorias.Size.Height);

                btnAtalhoCategoria.Visible = false;
            }
        }

        private void PesquisePeloId()
        {
            ServicoGrupo servicoGrupo = new ServicoGrupo();
            var grupo = servicoGrupo.Consulte(txtId.Text.ToInt());

            EditeGrupo(grupo);
        }

        #endregion
    }
}
