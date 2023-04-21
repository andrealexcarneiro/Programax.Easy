using System;
using System.Windows.Forms;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio;
using System.Drawing;
using System.Linq;

namespace Programax.Easy.View.Componentes
{
    public partial class FormularioPadrao : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private bool mover;
        private int cX, cY;

        #endregion

        #region " PROPRIEDADES "

        public string NomeDaTela
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        #endregion

        #region " CONSTRUTOR "

        public FormularioPadrao()
        {
            InitializeComponent();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void imgMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                cX = e.X;
                cY = e.Y;
                mover = true;
            }
        }

        private void panel1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mover = false;
        }

        private void panel1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (mover)
            {
                this.Left += e.X - (cX - panel1.Left);
                this.Top += e.Y - (cY - panel1.Top);
            }
        }

        private void imgClose_Click(object sender, EventArgs e)
        {
            FecharFormulario();
        }

        private void FormularioBase_Load(object sender, EventArgs e)
        {
            lblNomeDaTela.Text = this.Text;
        }

        public void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void imgMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public virtual void FecharFormulario()
        {
            
            this.Close();
        }

        protected virtual void TrateUsuarioNaoTemPermissaoAtalho(Panel painel, Control controle, Control atalho, EnumFuncionalidade funcionalidade)
        {
            var permissao = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == funcionalidade);

            if (permissao == null || !permissao.Alterar)
            {
                controle.Size = new Size(painel.Width, controle.Size.Height);

                atalho.Visible = false;
            }
        }

        #endregion
    }
}
