namespace Programax.Easy.View.Telas.Financeiro.MovimentacoesBanco
{
    partial class FormAberturaBanco
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
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.txtNomeBanco = new DevExpress.XtraEditors.TextEdit();
            this.btnPesquisarBanco = new System.Windows.Forms.PictureBox();
            this.txtIdBanco = new DevExpress.XtraEditors.TextEdit();
            this.labComplementoEndereco = new DevExpress.XtraEditors.LabelControl();
            this.txtValorDinheiro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtUsuarioAbertura = new DevExpress.XtraEditors.TextEdit();
            this.labelControl70 = new DevExpress.XtraEditors.LabelControl();
            this.txtObs = new DevExpress.XtraEditors.MemoEdit();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAbrirBanco = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.cboCategoria = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnAdicionarCategoria = new System.Windows.Forms.Button();
            this.txtDataHoraAbertura = new DevExpress.XtraEditors.DateEdit();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeBanco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisarBanco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdBanco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValorDinheiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsuarioAbertura.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObs.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategoria.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataHoraAbertura.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataHoraAbertura.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.txtDataHoraAbertura);
            this.panelConteudo.Controls.Add(this.btnAdicionarCategoria);
            this.panelConteudo.Controls.Add(this.cboCategoria);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.labelControl70);
            this.panelConteudo.Controls.Add(this.txtObs);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.txtUsuarioAbertura);
            this.panelConteudo.Controls.Add(this.labelControl19);
            this.panelConteudo.Controls.Add(this.labComplementoEndereco);
            this.panelConteudo.Controls.Add(this.txtValorDinheiro);
            this.panelConteudo.Controls.Add(this.labelControl17);
            this.panelConteudo.Controls.Add(this.labelControl18);
            this.panelConteudo.Controls.Add(this.txtNomeBanco);
            this.panelConteudo.Controls.Add(this.btnPesquisarBanco);
            this.panelConteudo.Controls.Add(this.txtIdBanco);
            this.panelConteudo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelConteudo.Size = new System.Drawing.Size(465, 187);
            // 
            // labelControl17
            // 
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl17.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl17.Appearance.Options.UseFont = true;
            this.labelControl17.Appearance.Options.UseForeColor = true;
            this.labelControl17.Location = new System.Drawing.Point(3, 6);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(46, 13);
            this.labelControl17.TabIndex = 10071;
            this.labelControl17.Text = "Id. Banco";
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl18.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl18.Appearance.Options.UseFont = true;
            this.labelControl18.Appearance.Options.UseForeColor = true;
            this.labelControl18.Location = new System.Drawing.Point(136, 6);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(97, 13);
            this.labelControl18.TabIndex = 10073;
            this.labelControl18.Text = "Descrição do Banco";
            // 
            // txtNomeBanco
            // 
            this.txtNomeBanco.EnterMoveNextControl = true;
            this.txtNomeBanco.Location = new System.Drawing.Point(136, 22);
            this.txtNomeBanco.Name = "txtNomeBanco";
            this.txtNomeBanco.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeBanco.Properties.Appearance.Options.UseFont = true;
            this.txtNomeBanco.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNomeBanco.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNomeBanco.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeBanco.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNomeBanco.Properties.Mask.EditMask = "99/";
            this.txtNomeBanco.Properties.ReadOnly = true;
            this.txtNomeBanco.Size = new System.Drawing.Size(325, 22);
            this.txtNomeBanco.TabIndex = 2;
            this.txtNomeBanco.TabStop = false;
            // 
            // btnPesquisarBanco
            // 
            this.btnPesquisarBanco.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisarBanco.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisarBanco.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisarBanco.Location = new System.Drawing.Point(108, 21);
            this.btnPesquisarBanco.Name = "btnPesquisarBanco";
            this.btnPesquisarBanco.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisarBanco.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisarBanco.TabIndex = 10072;
            this.btnPesquisarBanco.TabStop = false;
            this.btnPesquisarBanco.Click += new System.EventHandler(this.btnPesquisarBanco_Click);
            // 
            // txtIdBanco
            // 
            this.txtIdBanco.EnterMoveNextControl = true;
            this.txtIdBanco.Location = new System.Drawing.Point(3, 22);
            this.txtIdBanco.Name = "txtIdBanco";
            this.txtIdBanco.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdBanco.Properties.Appearance.Options.UseFont = true;
            this.txtIdBanco.Properties.Appearance.Options.UseTextOptions = true;
            this.txtIdBanco.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtIdBanco.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtIdBanco.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtIdBanco.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIdBanco.Properties.MaxLength = 10;
            this.txtIdBanco.Size = new System.Drawing.Size(99, 22);
            this.txtIdBanco.TabIndex = 1;
            this.txtIdBanco.TabStop = false;
            this.txtIdBanco.Leave += new System.EventHandler(this.txtIdBanco_Leave);
            // 
            // labComplementoEndereco
            // 
            this.labComplementoEndereco.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labComplementoEndereco.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labComplementoEndereco.Appearance.Options.UseFont = true;
            this.labComplementoEndereco.Appearance.Options.UseForeColor = true;
            this.labComplementoEndereco.Location = new System.Drawing.Point(3, 50);
            this.labComplementoEndereco.Name = "labComplementoEndereco";
            this.labComplementoEndereco.Size = new System.Drawing.Size(66, 13);
            this.labComplementoEndereco.TabIndex = 10075;
            this.labComplementoEndereco.Text = "Valor Dinheiro";
            // 
            // txtValorDinheiro
            // 
            this.txtValorDinheiro.EnterMoveNextControl = true;
            this.txtValorDinheiro.Location = new System.Drawing.Point(3, 65);
            this.txtValorDinheiro.Name = "txtValorDinheiro";
            this.txtValorDinheiro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorDinheiro.Properties.Appearance.Options.UseFont = true;
            this.txtValorDinheiro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtValorDinheiro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtValorDinheiro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtValorDinheiro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtValorDinheiro.Properties.Mask.EditMask = "[0-9]{1,11}([\\.\\,][0-9]{0,2})?";
            this.txtValorDinheiro.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtValorDinheiro.Properties.MaxLength = 30;
            this.txtValorDinheiro.Size = new System.Drawing.Size(127, 22);
            this.txtValorDinheiro.TabIndex = 2;
            this.txtValorDinheiro.TabStop = false;
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl19.Appearance.Options.UseFont = true;
            this.labelControl19.Location = new System.Drawing.Point(3, 144);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(100, 13);
            this.labelControl19.TabIndex = 10077;
            this.labelControl19.Text = "Data / Hora Abertura";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(136, 144);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(79, 13);
            this.labelControl1.TabIndex = 10079;
            this.labelControl1.Text = "Usuário Abertura";
            // 
            // txtUsuarioAbertura
            // 
            this.txtUsuarioAbertura.EnterMoveNextControl = true;
            this.txtUsuarioAbertura.Location = new System.Drawing.Point(136, 159);
            this.txtUsuarioAbertura.Name = "txtUsuarioAbertura";
            this.txtUsuarioAbertura.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuarioAbertura.Properties.Appearance.Options.UseFont = true;
            this.txtUsuarioAbertura.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtUsuarioAbertura.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtUsuarioAbertura.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsuarioAbertura.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtUsuarioAbertura.Properties.Mask.EditMask = "99/";
            this.txtUsuarioAbertura.Properties.MaxLength = 50;
            this.txtUsuarioAbertura.Properties.ReadOnly = true;
            this.txtUsuarioAbertura.Size = new System.Drawing.Size(325, 22);
            this.txtUsuarioAbertura.TabIndex = 6;
            this.txtUsuarioAbertura.TabStop = false;
            // 
            // labelControl70
            // 
            this.labelControl70.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl70.Appearance.Options.UseFont = true;
            this.labelControl70.Location = new System.Drawing.Point(3, 93);
            this.labelControl70.Name = "labelControl70";
            this.labelControl70.Size = new System.Drawing.Size(63, 13);
            this.labelControl70.TabIndex = 10081;
            this.labelControl70.Text = "Observações";
            // 
            // txtObs
            // 
            this.txtObs.Location = new System.Drawing.Point(3, 110);
            this.txtObs.Name = "txtObs";
            this.txtObs.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObs.Properties.Appearance.Options.UseFont = true;
            this.txtObs.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtObs.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtObs.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObs.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtObs.Properties.MaxLength = 2000;
            this.txtObs.Size = new System.Drawing.Size(458, 28);
            this.txtObs.TabIndex = 3;
            this.txtObs.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnAbrirBanco);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(457, 52);
            this.flowLayoutPanel1.TabIndex = 1002;
            // 
            // btnAbrirBanco
            // 
            this.btnAbrirBanco.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirBanco.FlatAppearance.BorderSize = 0;
            this.btnAbrirBanco.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirBanco.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrirBanco.Image = global::Programax.Easy.View.Properties.Resources.iconfinder_strongbox_3339047;
            this.btnAbrirBanco.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAbrirBanco.Location = new System.Drawing.Point(0, 0);
            this.btnAbrirBanco.Margin = new System.Windows.Forms.Padding(0);
            this.btnAbrirBanco.Name = "btnAbrirBanco";
            this.btnAbrirBanco.Size = new System.Drawing.Size(140, 40);
            this.btnAbrirBanco.TabIndex = 997;
            this.btnAbrirBanco.Text = " Abrir o Banco";
            this.btnAbrirBanco.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbrirBanco.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAbrirBanco.UseVisualStyleBackColor = true;
            this.btnAbrirBanco.Click += new System.EventHandler(this.btnAbrirBanco_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(140, 0);
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
            // cboCategoria
            // 
            this.cboCategoria.EnterMoveNextControl = true;
            this.cboCategoria.Location = new System.Drawing.Point(136, 67);
            this.cboCategoria.Name = "cboCategoria";
            this.cboCategoria.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategoria.Properties.Appearance.Options.UseFont = true;
            this.cboCategoria.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCategoria.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboCategoria.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboCategoria.Properties.DropDownRows = 5;
            this.cboCategoria.Properties.NullText = "";
            this.cboCategoria.Properties.ReadOnly = true;
            this.cboCategoria.Size = new System.Drawing.Size(292, 22);
            this.cboCategoria.TabIndex = 10082;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(136, 50);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(45, 13);
            this.labelControl2.TabIndex = 10083;
            this.labelControl2.Text = "Categoria";
            // 
            // btnAdicionarCategoria
            // 
            this.btnAdicionarCategoria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarCategoria.Enabled = false;
            this.btnAdicionarCategoria.FlatAppearance.BorderSize = 0;
            this.btnAdicionarCategoria.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarCategoria.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarCategoria.Image = global::Programax.Easy.View.Properties.Resources.iconfinder_199_CircledPlus_183316;
            this.btnAdicionarCategoria.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdicionarCategoria.Location = new System.Drawing.Point(430, 66);
            this.btnAdicionarCategoria.Name = "btnAdicionarCategoria";
            this.btnAdicionarCategoria.Size = new System.Drawing.Size(28, 23);
            this.btnAdicionarCategoria.TabIndex = 10084;
            this.btnAdicionarCategoria.TabStop = false;
            this.btnAdicionarCategoria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionarCategoria.UseVisualStyleBackColor = true;
            this.btnAdicionarCategoria.Click += new System.EventHandler(this.btnAdicionarCategoria_Click);
            // 
            // txtDataHoraAbertura
            // 
            this.txtDataHoraAbertura.EditValue = new System.DateTime(2014, 12, 5, 8, 26, 24, 0);
            this.txtDataHoraAbertura.EnterMoveNextControl = true;
            this.txtDataHoraAbertura.Location = new System.Drawing.Point(3, 159);
            this.txtDataHoraAbertura.Name = "txtDataHoraAbertura";
            this.txtDataHoraAbertura.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataHoraAbertura.Properties.Appearance.Options.UseFont = true;
            this.txtDataHoraAbertura.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataHoraAbertura.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataHoraAbertura.Size = new System.Drawing.Size(117, 22);
            this.txtDataHoraAbertura.TabIndex = 10089;
            // 
            // FormAberturaBanco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 298);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FormAberturaBanco";
            this.NomeDaTela = "Abertura de Banco";
            this.Text = "Abertura de Banco";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeBanco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisarBanco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdBanco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValorDinheiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsuarioAbertura.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObs.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboCategoria.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataHoraAbertura.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataHoraAbertura.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.TextEdit txtNomeBanco;
        private System.Windows.Forms.PictureBox btnPesquisarBanco;
        private DevExpress.XtraEditors.TextEdit txtIdBanco;
        private DevExpress.XtraEditors.LabelControl labComplementoEndereco;
        private DevExpress.XtraEditors.TextEdit txtValorDinheiro;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtUsuarioAbertura;
        private DevExpress.XtraEditors.LabelControl labelControl70;
        private DevExpress.XtraEditors.MemoEdit txtObs;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAbrirBanco;
        private System.Windows.Forms.Button btnSair;
        private DevExpress.XtraEditors.LookUpEdit cboCategoria;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Button btnAdicionarCategoria;
        private DevExpress.XtraEditors.DateEdit txtDataHoraAbertura;
    }
}