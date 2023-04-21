using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Drawing;

namespace Programax.Easy.View.Componentes
{
    public class AkilLookUpEdit : LookUpEdit
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

        #endregion

        #region " CONSTRUTOR "

        public AkilLookUpEdit()
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
            ((System.ComponentModel.ISupportInitialize)(this.fProperties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
