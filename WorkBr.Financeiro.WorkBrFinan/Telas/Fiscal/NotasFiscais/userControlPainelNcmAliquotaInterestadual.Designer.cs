using Programax.Easy.View.Componentes;
using System.Windows.Forms;
namespace Programax.Easy.View.Telas.Fiscal.NotasFiscais
{
    partial class userControlPainelNcmAliquotaInterestadual
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAliquota = new System.Windows.Forms.Label();
            this.lblFCP = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCodigoNcm = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtAliquotaInterna = new Programax.Easy.View.Componentes.AkilTextEdit();
            this.txtFCP = new Programax.Easy.View.Componentes.AkilTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliquotaInterna.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFCP.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAliquota
            // 
            this.lblAliquota.AutoSize = true;
            this.lblAliquota.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAliquota.ForeColor = System.Drawing.Color.Black;
            this.lblAliquota.Location = new System.Drawing.Point(101, 24);
            this.lblAliquota.Name = "lblAliquota";
            this.lblAliquota.Size = new System.Drawing.Size(83, 13);
            this.lblAliquota.TabIndex = 10161;
            this.lblAliquota.Text = "Alíquota Interna";
            // 
            // lblFCP
            // 
            this.lblFCP.AutoSize = true;
            this.lblFCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFCP.ForeColor = System.Drawing.Color.Black;
            this.lblFCP.Location = new System.Drawing.Point(15, 24);
            this.lblFCP.Name = "lblFCP";
            this.lblFCP.Size = new System.Drawing.Size(27, 13);
            this.lblFCP.TabIndex = 10160;
            this.lblFCP.Text = "FCP";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(209)))), ((int)(((byte)(211)))));
            this.panel2.Location = new System.Drawing.Point(87, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(173, 1);
            this.panel2.TabIndex = 10157;
            // 
            // lblCodigoNcm
            // 
            this.lblCodigoNcm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoNcm.Location = new System.Drawing.Point(33, 0);
            this.lblCodigoNcm.Name = "lblCodigoNcm";
            this.lblCodigoNcm.Size = new System.Drawing.Size(48, 13);
            this.lblCodigoNcm.TabIndex = 10156;
            this.lblCodigoNcm.Text = "12345678";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(209)))), ((int)(((byte)(211)))));
            this.panel4.Location = new System.Drawing.Point(-1, 7);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(31, 1);
            this.panel4.TabIndex = 10155;
            // 
            // txtAliquotaInterna
            // 
            this.txtAliquotaInterna.EnterMoveNextControl = true;
            this.txtAliquotaInterna.LabelText = this.lblAliquota;
            this.txtAliquotaInterna.Location = new System.Drawing.Point(101, 41);
            this.txtAliquotaInterna.Name = "txtAliquotaInterna";
            this.txtAliquotaInterna.Obrigatorio = true;
            this.txtAliquotaInterna.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAliquotaInterna.Properties.Appearance.Options.UseFont = true;
            this.txtAliquotaInterna.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtAliquotaInterna.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtAliquotaInterna.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAliquotaInterna.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtAliquotaInterna.Properties.Mask.EditMask = "[0-9]{1,2}([\\.\\,][0-9]{0,4})?";
            this.txtAliquotaInterna.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtAliquotaInterna.Properties.Mask.ShowPlaceHolders = false;
            this.txtAliquotaInterna.Properties.MaxLength = 30;
            this.txtAliquotaInterna.Size = new System.Drawing.Size(83, 22);
            this.txtAliquotaInterna.TabIndex = 10159;
            this.txtAliquotaInterna.Tag = "AliquotaInterna";
            // 
            // txtFCP
            // 
            this.txtFCP.EnterMoveNextControl = true;
            this.txtFCP.LabelText = this.lblFCP;
            this.txtFCP.Location = new System.Drawing.Point(15, 41);
            this.txtFCP.Name = "txtFCP";
            this.txtFCP.Obrigatorio = false;
            this.txtFCP.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFCP.Properties.Appearance.Options.UseFont = true;
            this.txtFCP.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtFCP.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtFCP.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFCP.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtFCP.Properties.Mask.EditMask = "[0-2]{1,1}([\\.\\,][0-9]{0,4})?";
            this.txtFCP.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtFCP.Properties.Mask.ShowPlaceHolders = false;
            this.txtFCP.Properties.MaxLength = 30;
            this.txtFCP.Size = new System.Drawing.Size(80, 22);
            this.txtFCP.TabIndex = 10158;
            this.txtFCP.Tag = "FCP";
            // 
            // userControlPainelNcmAliquotaInterestadual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtAliquotaInterna);
            this.Controls.Add(this.lblAliquota);
            this.Controls.Add(this.txtFCP);
            this.Controls.Add(this.lblFCP);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblCodigoNcm);
            this.Controls.Add(this.panel4);
            this.Name = "userControlPainelNcmAliquotaInterestadual";
            this.Size = new System.Drawing.Size(201, 70);
            ((System.ComponentModel.ISupportInitialize)(this.txtAliquotaInterna.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFCP.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AkilTextEdit txtAliquotaInterna;
        private Label lblAliquota;
        private AkilTextEdit txtFCP;
        private Label lblFCP;
        private System.Windows.Forms.Panel panel2;
        private Label lblCodigoNcm;
        private System.Windows.Forms.Panel panel4;
    }
}
