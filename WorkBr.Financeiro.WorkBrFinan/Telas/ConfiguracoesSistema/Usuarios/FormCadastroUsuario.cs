using System;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.UsuarioObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.UsuarioServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using System.Collections.Generic;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.GrupoAcessoServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.ObjetoDeNegocio;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.ConfiguracoesSistema.Usuarios
{
    public partial class FormCadastroUsuario : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroUsuario()
        {
            InitializeComponent();

            LimpeFormulario();

            PreenchaOStatus();
            PreenchaGrupoAcesso();

            this.ActiveControl = txtId;

            this.NomeDaTela = "Usuário";
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
            Usuario usuario = new Usuario();

            usuario.Id = txtId.Text.ToInt();
            usuario.Login = txtLogin.Text;
            usuario.DataCadastro = txtDataCadastro.Text.ToDate();
            usuario.Senha = txtSenha.Text;
            usuario.Ativo = (bool)cboStatus.EditValue;
            usuario.GrupoAcesso = cboGrupoAcesso.EditValue != null ? new GrupoAcesso { Id = cboGrupoAcesso.EditValue.ToInt() } : null;

            Action actionSalvar = () =>
            {
                ServicoUsuario servicoUsuario = new ServicoUsuario();

                if (usuario.Id == 0)
                {
                    servicoUsuario.Cadastre(usuario, chkAtualizarSenha.Checked);
                }
                else
                {
                    servicoUsuario.Atualize(usuario, chkAtualizarSenha.Checked);
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
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var pessoa = formPessoaPesquisa.PesquisePessoa();

            if (pessoa != null)
            {
                var servicoUsuario = new ServicoUsuario();

                var usuario = servicoUsuario.Consulte(pessoa.Id);

                EditeUsuario(usuario, pessoa);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();
                var pessoa = servicoPessoa.Consulte(txtId.Text.ToInt());

                ServicoUsuario servicoUsuario = new ServicoUsuario();
                var usuario = servicoUsuario.Consulte(txtId.Text.ToInt());

                EditeUsuario(usuario, pessoa);
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void LimpeFormulario()
        {
            txtId.Text = string.Empty;
            txtLogin.Text = string.Empty;
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            cboStatus.EditValue = true;
            txtSenha.Text = string.Empty;
            txtNomePessoa.Text = string.Empty;
            cboGrupoAcesso.EditValue = null;

            txtId.Enabled = true;
            txtId.Focus();

            chkAtualizarSenha.Checked = true;
            chkAtualizarSenha.Enabled = false;
        }

        private void EditeUsuario(Usuario usuario, Pessoa pessoa)
        {
            if (pessoa != null)
            {
                txtId.Text = pessoa.Id.ToString();
                txtNomePessoa.Text = pessoa.DadosGerais.Razao;

                txtId.Enabled = false;

                txtLogin.Focus();

                chkAtualizarSenha.Enabled = false;
                chkAtualizarSenha.Checked = true;

                ServicoUsuario servicoUsuario = new ServicoUsuario();
                var usuarioLogado = servicoUsuario.Consulte(Sessao.PessoaLogada.Id);

                if (usuarioLogado.GrupoAcesso.Id == 1)
                {
                    cboGrupoAcesso.ReadOnly = false;
                }
                else
                {
                    cboGrupoAcesso.ReadOnly = true;
                    if (usuario.GrupoAcesso.Id ==1)
                    {
                        txtLogin.Enabled = false;
                        txtSenha.Enabled = false;
                        cboStatus.ReadOnly = true;
                        chkAtualizarSenha.Enabled = false;
                    }
                    else
                    {
                        txtLogin.Enabled = true;
                        txtSenha.Enabled = true;
                        cboStatus.ReadOnly = false;
                        chkAtualizarSenha.Enabled = true;
                    }
                   
                }
            }
            else
            {
                txtId.Text = string.Empty;
                txtNomePessoa.Text = string.Empty;

                txtId.Focus();

                txtId.Enabled = true;

                MessageBox.Show("Pessoa não encontrada.", "Pessoa não encontrada.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (usuario != null)
            {
                txtLogin.Text = usuario.Login;
                txtDataCadastro.Text = usuario.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = usuario.Ativo;
                txtSenha.Text = string.Empty;
                cboGrupoAcesso.EditValue = usuario.GrupoAcesso != null ? (int?)usuario.GrupoAcesso.Id : null;

                chkAtualizarSenha.Enabled = true;
                chkAtualizarSenha.Checked = false;

                ServicoUsuario servicoUsuario = new ServicoUsuario();
                var usuarioLogado = servicoUsuario.Consulte(Sessao.PessoaLogada.Id);

                if (usuarioLogado.GrupoAcesso.Id == 1)
                {
                    cboGrupoAcesso.ReadOnly = false;
                }
                else
                {
                    cboGrupoAcesso.ReadOnly = true;
                    if (usuario.GrupoAcesso.Id == 1)
                    {
                        txtLogin.Enabled = false;
                        txtSenha.Enabled = false;
                        cboStatus.ReadOnly = true;
                        chkAtualizarSenha.Enabled = false;
                    }
                    else
                    {
                        txtLogin.Enabled = true;
                        txtSenha.Enabled = true;
                        cboStatus.ReadOnly = false;
                        chkAtualizarSenha.Enabled = true;
                    }
                }
            }
            else
            {
                txtLogin.Text = string.Empty;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                cboStatus.EditValue = true;
                txtSenha.Text = string.Empty;
                cboGrupoAcesso.EditValue = null;
            }

        }

        private void PreenchaOStatus()
        {
            ObjetoParaComboBox objetoComboBoxAtivo = new ObjetoParaComboBox();
            objetoComboBoxAtivo.Valor = true;
            objetoComboBoxAtivo.Descricao = "Ativo";

            ObjetoParaComboBox objetoComboBoxInativo = new ObjetoParaComboBox();
            objetoComboBoxInativo.Valor = false;
            objetoComboBoxInativo.Descricao = "Inativo";

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivo);
            listaDeItensParaOComboBox.Add(objetoComboBoxInativo);

            cboStatus.Properties.DataSource = listaDeItensParaOComboBox;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";

            cboStatus.EditValue = true;
        }

        private void PreenchaGrupoAcesso()
        {
            ServicoGrupoAcesso servicoGrupoAcesso = new ServicoGrupoAcesso();

            var listaGruposAcesso = servicoGrupoAcesso.ConsulteLista();
            listaGruposAcesso.Insert(0, null);

            cboGrupoAcesso.Properties.DataSource = listaGruposAcesso;
            cboGrupoAcesso.Properties.ValueMember = "Id";
            cboGrupoAcesso.Properties.DisplayMember = "Descricao";
        }

        #endregion

        private void chkAtualizarSenha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAtualizarSenha.Checked)
            {
                txtSenha.Enabled = true;
            }
            else
            {
                txtSenha.Text = string.Empty;
                txtSenha.Enabled = false;
            }
        }
    }
}
