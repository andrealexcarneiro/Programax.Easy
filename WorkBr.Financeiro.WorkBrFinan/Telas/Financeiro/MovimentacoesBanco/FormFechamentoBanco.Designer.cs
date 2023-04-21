namespace Programax.Easy.View.Telas.Financeiro.MovimentacoesBanco
{
    partial class FormFechamentoBanco
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFechamentoBanco));
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricaoBanco = new DevExpress.XtraEditors.TextEdit();
            this.txtIdBanco = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtUsuarioFechamento = new DevExpress.XtraEditors.TextEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataHoraFechamento = new DevExpress.XtraEditors.TextEdit();
            this.labelControl70 = new DevExpress.XtraEditors.LabelControl();
            this.txtObs = new DevExpress.XtraEditors.MemoEdit();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFecharCaixa = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtNrRegistroBanco = new DevExpress.XtraEditors.TextEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSaldoFinal = new Programax.Easy.View.Componentes.AkilTextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoBanco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdBanco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsuarioFechamento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataHoraFechamento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObs.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNrRegistroBanco.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaldoFinal.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.groupBox2);
            this.panelConteudo.Controls.Add(this.labelControl8);
            this.panelConteudo.Controls.Add(this.txtNrRegistroBanco);
            this.panelConteudo.Controls.Add(this.labelControl70);
            this.panelConteudo.Controls.Add(this.txtObs);
            this.panelConteudo.Controls.Add(this.labelControl5);
            this.panelConteudo.Controls.Add(this.txtUsuarioFechamento);
            this.panelConteudo.Controls.Add(this.labelControl19);
            this.panelConteudo.Controls.Add(this.txtDataHoraFechamento);
            this.panelConteudo.Controls.Add(this.labelControl17);
            this.panelConteudo.Controls.Add(this.labelControl18);
            this.panelConteudo.Controls.Add(this.txtDescricaoBanco);
            this.panelConteudo.Controls.Add(this.txtIdBanco);
            this.panelConteudo.Size = new System.Drawing.Size(496, 306);
            // 
            // labelControl17
            // 
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl17.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl17.Location = new System.Drawing.Point(117, 3);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(46, 13);
            this.labelControl17.TabIndex = 10071;
            this.labelControl17.Text = "Id. Banco";
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl18.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl18.Location = new System.Drawing.Point(221, 3);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(97, 13);
            this.labelControl18.TabIndex = 10072;
            this.labelControl18.Text = "Descrição do Banco";
            // 
            // txtDescricaoBanco
            // 
            this.txtDescricaoBanco.EnterMoveNextControl = true;
            this.txtDescricaoBanco.Location = new System.Drawing.Point(221, 19);
            this.txtDescricaoBanco.Name = "txtDescricaoBanco";
            this.txtDescricaoBanco.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricaoBanco.Properties.Appearance.Options.UseFont = true;
            this.txtDescricaoBanco.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricaoBanco.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricaoBanco.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricaoBanco.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricaoBanco.Properties.Mask.EditMask = "99/";
            this.txtDescricaoBanco.Properties.ReadOnly = true;
            this.txtDescricaoBanco.Size = new System.Drawing.Size(267, 22);
            this.txtDescricaoBanco.TabIndex = 10070;
            this.txtDescricaoBanco.TabStop = false;
            // 
            // txtIdBanco
            // 
            this.txtIdBanco.EnterMoveNextControl = true;
            this.txtIdBanco.Location = new System.Drawing.Point(117, 19);
            this.txtIdBanco.Name = "txtIdBanco";
            this.txtIdBanco.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdBanco.Properties.Appearance.Options.UseFont = true;
            this.txtIdBanco.Properties.Appearance.Options.UseTextOptions = true;
            this.txtIdBanco.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtIdBanco.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtIdBanco.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtIdBanco.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIdBanco.Properties.MaxLength = 10;
            this.txtIdBanco.Properties.ReadOnly = true;
            this.txtIdBanco.Size = new System.Drawing.Size(98, 22);
            this.txtIdBanco.TabIndex = 10069;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(165, 264);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(98, 13);
            this.labelControl5.TabIndex = 10083;
            this.labelControl5.Text = "Usuário Fechamento";
            // 
            // txtUsuarioFechamento
            // 
            this.txtUsuarioFechamento.EnterMoveNextControl = true;
            this.txtUsuarioFechamento.Location = new System.Drawing.Point(165, 279);
            this.txtUsuarioFechamento.Name = "txtUsuarioFechamento";
            this.txtUsuarioFechamento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuarioFechamento.Properties.Appearance.Options.UseFont = true;
            this.txtUsuarioFechamento.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtUsuarioFechamento.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtUsuarioFechamento.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsuarioFechamento.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtUsuarioFechamento.Properties.Mask.EditMask = "99/";
            this.txtUsuarioFechamento.Properties.MaxLength = 50;
            this.txtUsuarioFechamento.Properties.ReadOnly = true;
            this.txtUsuarioFechamento.Size = new System.Drawing.Size(323, 22);
            this.txtUsuarioFechamento.TabIndex = 10081;
            this.txtUsuarioFechamento.TabStop = false;
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl19.Location = new System.Drawing.Point(9, 264);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(119, 13);
            this.labelControl19.TabIndex = 10082;
            this.labelControl19.Text = "Data / Hora Fechamento";
            // 
            // txtDataHoraFechamento
            // 
            this.txtDataHoraFechamento.EnterMoveNextControl = true;
            this.txtDataHoraFechamento.Location = new System.Drawing.Point(9, 279);
            this.txtDataHoraFechamento.Name = "txtDataHoraFechamento";
            this.txtDataHoraFechamento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataHoraFechamento.Properties.Appearance.Options.UseFont = true;
            this.txtDataHoraFechamento.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDataHoraFechamento.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDataHoraFechamento.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataHoraFechamento.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDataHoraFechamento.Properties.Mask.EditMask = "99/";
            this.txtDataHoraFechamento.Properties.MaxLength = 50;
            this.txtDataHoraFechamento.Properties.ReadOnly = true;
            this.txtDataHoraFechamento.Size = new System.Drawing.Size(151, 22);
            this.txtDataHoraFechamento.TabIndex = 10080;
            this.txtDataHoraFechamento.TabStop = false;
            // 
            // labelControl70
            // 
            this.labelControl70.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl70.Location = new System.Drawing.Point(9, 126);
            this.labelControl70.Name = "labelControl70";
            this.labelControl70.Size = new System.Drawing.Size(63, 13);
            this.labelControl70.TabIndex = 10085;
            this.labelControl70.Text = "Observações";
            // 
            // txtObs
            // 
            this.txtObs.Location = new System.Drawing.Point(9, 145);
            this.txtObs.Name = "txtObs";
            this.txtObs.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObs.Properties.Appearance.Options.UseFont = true;
            this.txtObs.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtObs.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtObs.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObs.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtObs.Properties.MaxLength = 2000;
            this.txtObs.Size = new System.Drawing.Size(479, 110);
            this.txtObs.TabIndex = 10084;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnFecharCaixa);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(457, 52);
            this.flowLayoutPanel1.TabIndex = 1003;
            // 
            // btnFecharCaixa
            // 
            this.btnFecharCaixa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFecharCaixa.FlatAppearance.BorderSize = 0;
            this.btnFecharCaixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFecharCaixa.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFecharCaixa.Image = global::Programax.Easy.View.Properties.Resources.iconfinder_safebox_3339041;
            this.btnFecharCaixa.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnFecharCaixa.Location = new System.Drawing.Point(0, 0);
            this.btnFecharCaixa.Margin = new System.Windows.Forms.Padding(0);
            this.btnFecharCaixa.Name = "btnFecharCaixa";
            this.btnFecharCaixa.Size = new System.Drawing.Size(155, 40);
            this.btnFecharCaixa.TabIndex = 997;
            this.btnFecharCaixa.Text = " Fechar o Banco";
            this.btnFecharCaixa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFecharCaixa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFecharCaixa.UseVisualStyleBackColor = true;
            this.btnFecharCaixa.Click += new System.EventHandler(this.btnFecharCaixa_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(155, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 999;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl8.Location = new System.Drawing.Point(9, 3);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(90, 13);
            this.labelControl8.TabIndex = 10088;
            this.labelControl8.Text = "Nr. Registro Banco";
            // 
            // txtNrRegistroBanco
            // 
            this.txtNrRegistroBanco.EnterMoveNextControl = true;
            this.txtNrRegistroBanco.Location = new System.Drawing.Point(9, 19);
            this.txtNrRegistroBanco.Name = "txtNrRegistroBanco";
            this.txtNrRegistroBanco.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNrRegistroBanco.Properties.Appearance.Options.UseFont = true;
            this.txtNrRegistroBanco.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNrRegistroBanco.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNrRegistroBanco.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNrRegistroBanco.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNrRegistroBanco.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNrRegistroBanco.Properties.MaxLength = 10;
            this.txtNrRegistroBanco.Properties.ReadOnly = true;
            this.txtNrRegistroBanco.Size = new System.Drawing.Size(102, 22);
            this.txtNrRegistroBanco.TabIndex = 10087;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSaldoFinal);
            this.groupBox2.Controls.Add(this.labelControl9);
            this.groupBox2.Location = new System.Drawing.Point(9, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(479, 73);
            this.groupBox2.TabIndex = 10089;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Total";
            // 
            // txtSaldoFinal
            // 
            this.txtSaldoFinal.EnterMoveNextControl = true;
            this.txtSaldoFinal.Inconsistencias = ((System.Collections.Generic.List<string>)(resources.GetObject("txtSaldoFinal.Inconsistencias")));
            this.txtSaldoFinal.LabelText = null;
            this.txtSaldoFinal.Location = new System.Drawing.Point(4, 41);
            this.txtSaldoFinal.Name = "txtSaldoFinal";
            this.txtSaldoFinal.Obrigatorio = false;
            this.txtSaldoFinal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldoFinal.Properties.Appearance.Options.UseFont = true;
            this.txtSaldoFinal.Properties.Mask.EditMask = "[0-9]{1,11}([\\.\\,][0-9]{0,2})?";
            this.txtSaldoFinal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtSaldoFinal.Properties.ReadOnly = true;
            this.txtSaldoFinal.Size = new System.Drawing.Size(202, 22);
            this.txtSaldoFinal.TabIndex = 10091;
            this.txtSaldoFinal.TabStop = false;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Location = new System.Drawing.Point(4, 22);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(52, 13);
            this.labelControl9.TabIndex = 10084;
            this.labelControl9.Text = "Saldo Final";
            // 
            // FormFechamentoBanco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 417);
            this.Name = "FormFechamentoBanco";
            this.NomeDaTela = "Fechamento de Banco";
            this.Text = "Fechamento de Banco";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoBanco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdBanco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsuarioFechamento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataHoraFechamento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObs.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNrRegistroBanco.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaldoFinal.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.TextEdit txtDescricaoBanco;
        private DevExpress.XtraEditors.TextEdit txtIdBanco;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtUsuarioFechamento;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.TextEdit txtDataHoraFechamento;
        private DevExpress.XtraEditors.LabelControl labelControl70;
        private DevExpress.XtraEditors.MemoEdit txtObs;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnFecharCaixa;
        private System.Windows.Forms.Button btnSair;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtNrRegistroBanco;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private Componentes.AkilTextEdit txtSaldoFinal;
    }
}