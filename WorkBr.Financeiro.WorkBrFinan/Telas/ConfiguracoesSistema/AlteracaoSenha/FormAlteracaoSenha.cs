using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.ConfiguracoesSistema.UsuarioServ;
using Programax.Easy.Negocio;
using Programax.Easy.View.ClassesAuxiliares;

namespace Programax.Easy.View.Telas.ConfiguracoesSistema.AlteracaoSenha
{
    public partial class FormAlteracaoSenha : FormularioPadrao
    {
        public FormAlteracaoSenha()
        {
            InitializeComponent();

            this.ActiveControl = txtSenha;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (txtSenha.Text != txtConfirmacaoSenha.Text)
            {
                MessageBox.Show("As senhas informadas estão divergentes!", "Senhas Divergentes", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            ServicoUsuario servicoUsuario = new ServicoUsuario();
            var usuario = servicoUsuario.Consulte(Sessao.PessoaLogada.Id);

            usuario.Senha = txtSenha.Text;

            Action actionSalvar = () =>
            {
                servicoUsuario.Atualize(usuario, true);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }
    }
}
