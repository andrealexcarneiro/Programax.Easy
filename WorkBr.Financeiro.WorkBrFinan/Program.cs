using System;
using System.Windows.Forms;
using Programax.Easy.Servico;
using Programax.Easy.View.Telas;
using Programax.Easy.View.Telas.ConfiguracoesSistema.Login;
using System.Threading;

namespace Programax.Easy.View
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary> 
        [STAThread]
        static void Main()
        {
            FormLogin formLogin = new FormLogin();

            DialogResult resultado = formLogin.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                Application.Run(new FormPrincipal());
            }
        }
    }
}
