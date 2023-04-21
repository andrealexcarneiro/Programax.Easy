using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Programax.Easy.View.Componentes
{
    public partial class MessageBoxAkil : FormularioBase
    {
        private DialogResult _dialogResult;

        private MessageBoxAkil()
        {
            InitializeComponent();

            _dialogResult = DialogResult.Cancel;
        }

        public static DialogResult Show(string conteudo)
        {
            return ExibirMessageBox(conteudo, string.Empty, MessageBoxButtons.OK);
        }

        public static DialogResult Show(string conteudo, string titulo)
        {
            return ExibirMessageBox(conteudo, titulo, MessageBoxButtons.OK);
        }

        public static DialogResult Show(string conteudo, MessageBoxButtons messageBoxButtons)
        {
            return ExibirMessageBox(conteudo, string.Empty, messageBoxButtons);
        }

        public static DialogResult Show(string conteudo, string titulo, MessageBoxButtons messageBoxButtons)
        {
            return ExibirMessageBox(conteudo, titulo, messageBoxButtons);
        }

        private static DialogResult ExibirMessageBox(string conteudo, string titulo, MessageBoxButtons messageBoxButtons)
        {
            MessageBoxAkil messageBox = new MessageBoxAkil();
            messageBox.lblTitulo.Text = titulo;
            messageBox.lblConteudo.Text = conteudo;

            if (messageBoxButtons == MessageBoxButtons.OK)
            {
                messageBox.btnOk.Visible = true;
            }
            else if (messageBoxButtons == MessageBoxButtons.OKCancel)
            {
                messageBox.btnOk.Visible = true;
                messageBox.btnCancelar.Visible = true;
            }
            
            messageBox.pnlIcone.Visible = false;

            messageBox.AbrirTelaModal(true);

            return messageBox._dialogResult;
        }

        private void MessageBoxAkil_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Ok();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Cancele();
            }
        }

        private void Cancele()
        {
            _dialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void Ok()
        {
            _dialogResult = DialogResult.OK;

            this.Close();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Ok();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Cancele();
        }
    }
}
