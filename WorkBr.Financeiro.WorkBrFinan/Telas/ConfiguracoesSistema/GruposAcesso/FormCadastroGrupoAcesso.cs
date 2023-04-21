using System;
using System.Windows.Forms;
using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.GrupoAcessoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.ConfiguracoesSistema.GruposAcesso
{
    public partial class FormCadastroGrupoAcesso : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroGrupoAcesso()
        {
            InitializeComponent();

            this.ActiveControl = txtId;

            this.NomeDaTela = "Grupo de Acesso";
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
            GrupoAcesso grupoAcesso = new GrupoAcesso();

            grupoAcesso.Id = txtId.Text.ToInt();
            grupoAcesso.Descricao = txtDescricao.Text;
            grupoAcesso.Tesoureiro = chkEhTesoureiro.Checked;

            Action actionSalvar = () =>
            {
                ServicoGrupoAcesso servicoGrupoAcesso = new ServicoGrupoAcesso();

                if (grupoAcesso.Id == 0)
                {
                    servicoGrupoAcesso.Cadastre(grupoAcesso);
                }
                else
                {
                    servicoGrupoAcesso.Atualize(grupoAcesso);
                }

                LimpeFormulario();
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
            FormGruposAcessoPesquisa formGruposAcessoPesquisa = new FormGruposAcessoPesquisa();

            var grupoAcesso = formGruposAcessoPesquisa.ExibaPesquisaDeGruposAcesso();

            if (grupoAcesso != null)
            {
                EditeGrupoAcesso(grupoAcesso);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                ServicoGrupoAcesso servicoGrupoAcesso = new ServicoGrupoAcesso();
                var grupoAcesso = servicoGrupoAcesso.Consulte(txtId.Text.ToInt());

                EditeGrupoAcesso(grupoAcesso);
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void LimpeFormulario()
        {
            txtId.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            chkEhTesoureiro.Checked = false;

            txtId.Enabled = true;
            txtId.Focus();
        }

        private void EditeGrupoAcesso(GrupoAcesso grupoAcesso)
        {
            if (grupoAcesso != null)
            {
                txtId.Text = grupoAcesso.Id.ToString();
                txtDescricao.Text = grupoAcesso.Descricao;
                chkEhTesoureiro.Checked = grupoAcesso.Tesoureiro;

                txtId.Enabled = false;

                txtDescricao.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                chkEhTesoureiro.Checked = false;

                txtId.Focus();

                MessageBox.Show("Grupo de acesso não encontrado", "Grupo de acesso não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion
    }
}
