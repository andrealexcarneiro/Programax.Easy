namespace Programax.Easy.View.Telas.Financeiro.Bancos
{
    partial class FormCadastroBanco
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
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.pbPesquisaPessoa = new System.Windows.Forms.PictureBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricao = new DevExpress.XtraEditors.TextEdit();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigoCompensacao = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtSite = new DevExpress.XtraEditors.TextEdit();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtDataCadastro = new DevExpress.XtraEditors.TextEdit();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoCompensacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSite.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Size = new System.Drawing.Size(424, 43);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.txtDataCadastro);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.labelControl3);
            this.panelConteudo.Controls.Add(this.txtSite);
            this.panelConteudo.Controls.Add(this.txtDescricao);
            this.panelConteudo.Controls.Add(this.txtCodigoCompensacao);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.pbPesquisaPessoa);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Controls.Add(this.labDataCadastro);
            this.panelConteudo.Size = new System.Drawing.Size(449, 99);
            // 
            // btnGravar
            // 
            this.btnGravar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGravar.FlatAppearance.BorderSize = 0;
            this.btnGravar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGravar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGravar.Image = global::Programax.Easy.View.Properties.Resources.iconSalvar;
            this.btnGravar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnGravar.Location = new System.Drawing.Point(0, 0);
            this.btnGravar.Margin = new System.Windows.Forms.Padding(0);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(100, 40);
            this.btnGravar.TabIndex = 997;
            this.btnGravar.Text = " Salvar";
            this.btnGravar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.FlatAppearance.BorderSize = 0;
            this.btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.Image = global::Programax.Easy.View.Properties.Resources.iconLimpar;
            this.btnLimpar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnLimpar.Location = new System.Drawing.Point(100, 0);
            this.btnLimpar.Margin = new System.Windows.Forms.Padding(0);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(100, 40);
            this.btnLimpar.TabIndex = 1000;
            this.btnLimpar.Text = " Limpar";
            this.btnLimpar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(200, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 999;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // pbPesquisaPessoa
            // 
            this.pbPesquisaPessoa.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaPessoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaPessoa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaPessoa.Location = new System.Drawing.Point(106, 18);
            this.pbPesquisaPessoa.Name = "pbPesquisaPessoa";
            this.pbPesquisaPessoa.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaPessoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaPessoa.TabIndex = 488;
            this.pbPesquisaPessoa.TabStop = false;
            this.pbPesquisaPessoa.Click += new System.EventHandler(this.pbPesquisaPessoa_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(134, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 13);
            this.labelControl1.TabIndex = 490;
            this.labelControl1.Text = "Descrição";
            // 
            // txtDescricao
            // 
            this.txtDescricao.EnterMoveNextControl = true;
            this.txtDescricao.Location = new System.Drawing.Point(134, 18);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.Properties.Appearance.Options.UseFont = true;
            this.txtDescricao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricao.Properties.Mask.EditMask = "99/";
            this.txtDescricao.Properties.MaxLength = 50;
            this.txtDescricao.Size = new System.Drawing.Size(311, 22);
            this.txtDescricao.TabIndex = 2;
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Location = new System.Drawing.Point(341, 51);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(59, 13);
            this.labDataCadastro.TabIndex = 494;
            this.labDataCadastro.Text = "Dt. Cadastro";
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(3, 51);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(18, 13);
            this.labStatus.TabIndex = 493;
            this.labStatus.Text = "Site";
            // 
            // txtCodigoCompensacao
            // 
            this.txtCodigoCompensacao.EnterMoveNextControl = true;
            this.txtCodigoCompensacao.Location = new System.Drawing.Point(3, 18);
            this.txtCodigoCompensacao.Name = "txtCodigoCompensacao";
            this.txtCodigoCompensacao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoCompensacao.Properties.Appearance.Options.UseFont = true;
            this.txtCodigoCompensacao.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCodigoCompensacao.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtCodigoCompensacao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCodigoCompensacao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtCodigoCompensacao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCodigoCompensacao.Properties.Mask.EditMask = "99/";
            this.txtCodigoCompensacao.Properties.MaxLength = 8;
            this.txtCodigoCompensacao.Size = new System.Drawing.Size(97, 22);
            this.txtCodigoCompensacao.TabIndex = 1;
            this.txtCodigoCompensacao.Leave += new System.EventHandler(this.txtCodigoCompensacao_Leave);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(3, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(67, 13);
            this.labelControl2.TabIndex = 496;
            this.labelControl2.Text = "Código Banco";
            // 
            // txtSite
            // 
            this.txtSite.EnterMoveNextControl = true;
            this.txtSite.Location = new System.Drawing.Point(3, 67);
            this.txtSite.Name = "txtSite";
            this.txtSite.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSite.Properties.Appearance.Options.UseFont = true;
            this.txtSite.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSite.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtSite.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSite.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtSite.Properties.Mask.EditMask = "99/";
            this.txtSite.Properties.MaxLength = 50;
            this.txtSite.Size = new System.Drawing.Size(217, 22);
            this.txtSite.TabIndex = 3;
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(226, 67);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboStatus.Properties.DropDownRows = 2;
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboStatus.Size = new System.Drawing.Size(109, 22);
            this.cboStatus.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(226, 51);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(30, 13);
            this.labelControl3.TabIndex = 498;
            this.labelControl3.Text = "Status";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnGravar);
            this.flowLayoutPanel1.Controls.Add(this.btnLimpar);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(503, 44);
            this.flowLayoutPanel1.TabIndex = 1001;
            // 
            // txtDataCadastro
            // 
            this.txtDataCadastro.EnterMoveNextControl = true;
            this.txtDataCadastro.Location = new System.Drawing.Point(341, 67);
            this.txtDataCadastro.Name = "txtDataCadastro";
            this.txtDataCadastro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataCadastro.Properties.Appearance.Options.UseFont = true;
            this.txtDataCadastro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDataCadastro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDataCadastro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataCadastro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDataCadastro.Properties.Mask.EditMask = "99/";
            this.txtDataCadastro.Properties.MaxLength = 50;
            this.txtDataCadastro.Properties.ReadOnly = true;
            this.txtDataCadastro.Size = new System.Drawing.Size(104, 22);
            this.txtDataCadastro.TabIndex = 499;
            this.txtDataCadastro.TabStop = false;
            // 
            // FormCadastroBanco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 210);
            this.Name = "FormCadastroBanco";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoCompensacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSite.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.PictureBox pbPesquisaPessoa;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDescricao;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraEditors.TextEdit txtCodigoCompensacao;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtSite;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.TextEdit txtDataCadastro;
    }
}