namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    partial class FormCaixaAbertoPdv
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrHoraAtual = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRazaoSocial = new DevExpress.XtraEditors.TextEdit();
            this.txtCpfCnpj = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.lblVersaoSistema = new System.Windows.Forms.Label();
            this.lblStausCaixa = new System.Windows.Forms.Label();
            this.lblOperadorCaixa = new System.Windows.Forms.Label();
            this.lblNumeroCaixa = new System.Windows.Forms.Label();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDataHoraAtual = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRazaoSocial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCpfCnpj.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrHoraAtual
            // 
            this.tmrHoraAtual.Enabled = true;
            this.tmrHoraAtual.Interval = 1000;
            this.tmrHoraAtual.Tick += new System.EventHandler(this.tmrHoraAtual_Tick);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.panel16);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtRazaoSocial);
            this.panel3.Controls.Add(this.txtCpfCnpj);
            this.panel3.Location = new System.Drawing.Point(12, 338);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1000, 111);
            this.panel3.TabIndex = 30;
            // 
            // panel16
            // 
            this.panel16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(209)))), ((int)(((byte)(211)))));
            this.panel16.Location = new System.Drawing.Point(0, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(1537, 1);
            this.panel16.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(7, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 31);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cliente";
            // 
            // txtRazaoSocial
            // 
            this.txtRazaoSocial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRazaoSocial.EditValue = "";
            this.txtRazaoSocial.Location = new System.Drawing.Point(444, 46);
            this.txtRazaoSocial.Name = "txtRazaoSocial";
            this.txtRazaoSocial.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRazaoSocial.Properties.Appearance.Options.UseFont = true;
            this.txtRazaoSocial.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRazaoSocial.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtRazaoSocial.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtRazaoSocial.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRazaoSocial.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtRazaoSocial.Properties.HideSelection = false;
            this.txtRazaoSocial.Properties.Mask.EditMask = "000.000.000-00";
            this.txtRazaoSocial.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.txtRazaoSocial.Properties.MaxLength = 80;
            this.txtRazaoSocial.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRazaoSocial.Size = new System.Drawing.Size(538, 56);
            this.txtRazaoSocial.TabIndex = 29;
            // 
            // txtCpfCnpj
            // 
            this.txtCpfCnpj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCpfCnpj.EditValue = "";
            this.txtCpfCnpj.Location = new System.Drawing.Point(11, 46);
            this.txtCpfCnpj.Name = "txtCpfCnpj";
            this.txtCpfCnpj.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCpfCnpj.Properties.Appearance.Options.UseFont = true;
            this.txtCpfCnpj.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCpfCnpj.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtCpfCnpj.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtCpfCnpj.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCpfCnpj.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtCpfCnpj.Properties.HideSelection = false;
            this.txtCpfCnpj.Properties.Mask.EditMask = "[0-9]{1,20}";
            this.txtCpfCnpj.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtCpfCnpj.Properties.MaxLength = 20;
            this.txtCpfCnpj.Size = new System.Drawing.Size(427, 56);
            this.txtCpfCnpj.TabIndex = 8;
            this.txtCpfCnpj.EditValueChanged += new System.EventHandler(this.txtCpfCnpj_EditValueChanged);
            this.txtCpfCnpj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCpfCnpj_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1023, 216);
            this.label1.TabIndex = 6;
            this.label1.Text = "CAIXA DISPONÍVEL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel11);
            this.panel1.Controls.Add(this.lblVersaoSistema);
            this.panel1.Controls.Add(this.lblStausCaixa);
            this.panel1.Controls.Add(this.lblOperadorCaixa);
            this.panel1.Controls.Add(this.lblNumeroCaixa);
            this.panel1.Controls.Add(this.lblEmpresa);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblDataHoraAtual);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1230, 105);
            this.panel1.TabIndex = 7;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(237)))), ((int)(((byte)(240)))));
            this.panel11.Location = new System.Drawing.Point(227, 14);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1, 79);
            this.panel11.TabIndex = 19;
            // 
            // lblVersaoSistema
            // 
            this.lblVersaoSistema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersaoSistema.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersaoSistema.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(129)))), ((int)(((byte)(132)))));
            this.lblVersaoSistema.Location = new System.Drawing.Point(832, 45);
            this.lblVersaoSistema.Name = "lblVersaoSistema";
            this.lblVersaoSistema.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblVersaoSistema.Size = new System.Drawing.Size(166, 16);
            this.lblVersaoSistema.TabIndex = 18;
            this.lblVersaoSistema.Text = "Versão 1.1.8.0";
            // 
            // lblStausCaixa
            // 
            this.lblStausCaixa.AutoSize = true;
            this.lblStausCaixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStausCaixa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(81)))), ((int)(((byte)(36)))));
            this.lblStausCaixa.Location = new System.Drawing.Point(543, 74);
            this.lblStausCaixa.Name = "lblStausCaixa";
            this.lblStausCaixa.Size = new System.Drawing.Size(57, 20);
            this.lblStausCaixa.TabIndex = 17;
            this.lblStausCaixa.Text = "Aberto";
            // 
            // lblOperadorCaixa
            // 
            this.lblOperadorCaixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperadorCaixa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(81)))), ((int)(((byte)(36)))));
            this.lblOperadorCaixa.Location = new System.Drawing.Point(354, 72);
            this.lblOperadorCaixa.Name = "lblOperadorCaixa";
            this.lblOperadorCaixa.Size = new System.Drawing.Size(176, 22);
            this.lblOperadorCaixa.TabIndex = 16;
            this.lblOperadorCaixa.Text = "José Pereira";
            // 
            // lblNumeroCaixa
            // 
            this.lblNumeroCaixa.AutoSize = true;
            this.lblNumeroCaixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroCaixa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(81)))), ((int)(((byte)(36)))));
            this.lblNumeroCaixa.Location = new System.Drawing.Point(271, 72);
            this.lblNumeroCaixa.Name = "lblNumeroCaixa";
            this.lblNumeroCaixa.Size = new System.Drawing.Size(36, 20);
            this.lblNumeroCaixa.TabIndex = 15;
            this.lblNumeroCaixa.Text = "001";
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpresa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(81)))), ((int)(((byte)(36)))));
            this.lblEmpresa.Location = new System.Drawing.Point(341, 20);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(324, 20);
            this.lblEmpresa.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(129)))), ((int)(((byte)(132)))));
            this.label11.Location = new System.Drawing.Point(543, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 16);
            this.label11.TabIndex = 13;
            this.label11.Text = "Status Caixa";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(129)))), ((int)(((byte)(132)))));
            this.label10.Location = new System.Drawing.Point(355, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "Operador";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(129)))), ((int)(((byte)(132)))));
            this.label9.Location = new System.Drawing.Point(269, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "Caixa";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(129)))), ((int)(((byte)(132)))));
            this.label8.Location = new System.Drawing.Point(269, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "Empresa:";
            // 
            // lblDataHoraAtual
            // 
            this.lblDataHoraAtual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDataHoraAtual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataHoraAtual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(81)))), ((int)(((byte)(36)))));
            this.lblDataHoraAtual.Location = new System.Drawing.Point(755, 20);
            this.lblDataHoraAtual.Name = "lblDataHoraAtual";
            this.lblDataHoraAtual.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblDataHoraAtual.Size = new System.Drawing.Size(243, 20);
            this.lblDataHoraAtual.TabIndex = 9;
            this.lblDataHoraAtual.Text = "DataHoraAtual";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Programax.Easy.View.Properties.Resources.Akil_03;
            this.pictureBox1.Location = new System.Drawing.Point(20, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(173, 89);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label12);
            this.panel2.Location = new System.Drawing.Point(0, 463);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1025, 33);
            this.panel2.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.label12.Location = new System.Drawing.Point(0, 8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1019, 18);
            this.label12.TabIndex = 0;
            this.label12.Text = "Esc Sair  |  Enter Nova Venda  |  F3 Importar Pré-Venda  | F5 Suprimento Caixa  |" +
    "  F6 Mov. Caixa  |  F7 Fechar Caixa";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // FormCaixaAbertoPdv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.ClientSize = new System.Drawing.Size(1024, 496);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FormCaixaAbertoPdv";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCaixaAbertoPdv";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormCaixaAbertoPdv_KeyDown);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRazaoSocial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCpfCnpj.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label lblVersaoSistema;
        private System.Windows.Forms.Label lblStausCaixa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOperadorCaixa;
        private System.Windows.Forms.Label lblNumeroCaixa;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDataHoraAtual;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Timer tmrHoraAtual;
        private DevExpress.XtraEditors.TextEdit txtCpfCnpj;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtRazaoSocial;
        private System.Windows.Forms.Panel panel16;
    }
}