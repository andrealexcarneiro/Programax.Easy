using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms.ComponentModel;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace Programax.Easy.View.Componentes
{
    public class AkilTextEdit : TextEdit
    {
        #region " VARIÁVEIS PRIVADAS "

        private bool _obrigatorio;
        private PictureBox _pictureBoxImagemObrigatorio;
        private Padding _paddingPadrao;
        private Color _backgroundColorPadrao;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit fProperties;
        private Color _foreColorPadrao;

        private Color _foreColorLabel;

        private bool _estahInconsistente;
        private static FormMensagemInconsistenciaCampo _formMensagemInconsistencias;

        #endregion

        #region " PROPRIEDADES "

        public bool Obrigatorio
        {
            get
            {
                return _obrigatorio;
            }
            set
            {
                if (value == true)
                {
                    InsiraObrigatoriedade();
                }
                else
                {
                    RemovaObrigatoriedade();
                }

                _obrigatorio = value;
            }
        }

        public Label LabelText { get; set; }

        public List<string> Inconsistencias { get; set; }

        #endregion

        #region " CONSTRUTOR "

        public AkilTextEdit()
        {
            InitializeComponent();

            _pictureBoxImagemObrigatorio = new PictureBox();
            _pictureBoxImagemObrigatorio.Image = Programax.Easy.View.Properties.Resources.icone_obrigatorio;
            _pictureBoxImagemObrigatorio.SizeMode = PictureBoxSizeMode.AutoSize;
            _backgroundColorPadrao = this.BackColor;
            _foreColorPadrao = this.ForeColor;

            _paddingPadrao = this.Properties.MaskBoxPadding;

            if (LabelText != null)
            {
                _foreColorLabel = LabelText.ForeColor;
            }

            Inconsistencias = new List<string>();
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public bool CampoEstahPreenchido()
        {
            if (this.Obrigatorio)
            {
                if (string.IsNullOrWhiteSpace(this.Text))
                {
                    _estahInconsistente = true;

                    //this.BackColor = Color.FromArgb(1, 205, 66, 68);
                    this.BackColor = Color.Red;
                    this.ForeColor = Color.White;
                    _pictureBoxImagemObrigatorio.Image = Programax.Easy.View.Properties.Resources.icone_obrigatorio_branco;

                    _pictureBoxImagemObrigatorio.BackColor = Color.Transparent;

                    _pictureBoxImagemObrigatorio.Refresh();

                    if (LabelText != null)
                    {
                        LabelText.ForeColor = this.BackColor;
                    }

                    return false;
                }
            }

            return true;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void InsiraObrigatoriedade()
        {
            this.Controls.Add(_pictureBoxImagemObrigatorio);

            _pictureBoxImagemObrigatorio.BringToFront();
            _pictureBoxImagemObrigatorio.Location = new Point(8, this.Height / 2 - 4);

            this.Properties.MaskBoxPadding = new Padding(18, this.Properties.MaskBoxPadding.Top, this.Properties.MaskBoxPadding.Right, this.Properties.MaskBoxPadding.Bottom);
        }

        private void RemovaObrigatoriedade()
        {
            RemovaLayoutInconsistente();

            this.Controls.Remove(_pictureBoxImagemObrigatorio);
            this.Properties.MaskBoxPadding = _paddingPadrao;
        }

        private void RemovaLayoutInconsistente()
        {
            if (_estahInconsistente)
            {
                _estahInconsistente = false;
                this.BackColor = _backgroundColorPadrao;
                this.ForeColor = _foreColorPadrao;

                _pictureBoxImagemObrigatorio.Image = Programax.Easy.View.Properties.Resources.icone_obrigatorio;

                _pictureBoxImagemObrigatorio.Refresh();

                if (LabelText != null)
                {
                    LabelText.ForeColor = _foreColorLabel;
                }
            }
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void AkilTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            RemovaLayoutInconsistente();
        }

        private void AkilTextEdit_Enter(object sender, EventArgs e)
        {
            _pictureBoxImagemObrigatorio.Image = Programax.Easy.View.Properties.Resources.icone_obrigatorio;
        }

        private void AkilTextEdit_Leave(object sender, EventArgs e)
        {
            if (_estahInconsistente)
            {
                _pictureBoxImagemObrigatorio.Image = Programax.Easy.View.Properties.Resources.icone_obrigatorio_branco;
            }
            else
            {
                _pictureBoxImagemObrigatorio.Image = Programax.Easy.View.Properties.Resources.icone_obrigatorio;
            }
        }

        #endregion

        #region " INITIALIZE "

        private void InitializeComponent()
        {
            this.fProperties = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.fProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // fProperties
            // 
            this.fProperties.Name = "fProperties";
            // 
            // AkilTextEdit
            // 
            this.EditValueChanged += new System.EventHandler(this.AkilTextEdit_EditValueChanged);
            this.Enter += new System.EventHandler(this.AkilTextEdit_Enter);
            this.Leave += new System.EventHandler(this.AkilTextEdit_Leave);
            this.MouseEnter += new System.EventHandler(this.AkilTextEdit_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.AkilTextEdit_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.fProperties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private void AkilTextEdit_MouseEnter(object sender, EventArgs e)
        {
            if (_formMensagemInconsistencias != null)
            {
                _formMensagemInconsistencias.Close();
                _formMensagemInconsistencias = null;
            }

            if (Inconsistencias == null || Inconsistencias.Count == 0)
            {
                return;
            }

            int x = 0;
            int y = 0;

            DefinaXEY(this, ref x, ref y);

            _formMensagemInconsistencias = new FormMensagemInconsistenciaCampo(Inconsistencias);

            x += this.Width / 2 - _formMensagemInconsistencias.Size.Width / 2;
            y -= (_formMensagemInconsistencias.Size.Height);

            _formMensagemInconsistencias.Location = new Point(x, y);

            _formMensagemInconsistencias.Show();
        }

        private void DefinaXEY(Control controle, ref int x, ref int y)
        {
            x += controle.Location.X;
            y += controle.Location.Y;

            if (controle.Parent != null)
            {
                DefinaXEY(controle.Parent, ref x, ref y);
            }
        }

        private void AkilTextEdit_MouseLeave(object sender, EventArgs e)
        {
            if (_formMensagemInconsistencias != null)
            {
                _formMensagemInconsistencias.Close();
                _formMensagemInconsistencias = null;
            }
        }
    }
}
