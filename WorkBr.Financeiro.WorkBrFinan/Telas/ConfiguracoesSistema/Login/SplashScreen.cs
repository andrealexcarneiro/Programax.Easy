using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Programax.Easy.Servico;

namespace Programax.Easy.View.Telas.ConfiguracoesSistema.Login
{
    public partial class SplashScreen : Form, IDisposable
    {
        private int i = 0;
        private Thread _thread;

        public DialogResult Resultado { get; set; }

        public SplashScreen()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;

            _thread = new Thread(new ThreadStart(this.CarregarConfiguracoesBanco));
            _thread.Start();
        }

        private void CarregarConfiguracoesBanco()
        {
            try
            {
                StructureMap.ObjectFactory.Initialize(
                       x =>
                       {
                           x.Scan(scan =>
                           {
                               scan.Assembly(typeof(RegistroDeMapeamentos).Assembly);
                               scan.WithDefaultConventions();
                           }
                               );
                           x.AddRegistry<RegistroDeMapeamentos>();
                       });

                Resultado = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                Resultado = System.Windows.Forms.DialogResult.Cancel;

                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }


            }

            this.Close();
            this.Dispose();
        }

        public void Dispose()
        {
            _thread.Interrupt();
            timer1.Stop();
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i == 0)
            {
                pictureBox1.Image = Properties.Resources.Tela_EntradaSistema_03;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.Tela_EntradaSistema_04;
            }

            i++;

            pictureBox1.Refresh();
        }
    }
}
