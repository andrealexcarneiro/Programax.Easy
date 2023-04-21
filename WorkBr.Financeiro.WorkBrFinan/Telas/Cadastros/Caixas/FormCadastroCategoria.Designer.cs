namespace Programax.Easy.View.Telas.Cadastros.Caixas
{
    partial class FormCadastroCaixa
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
            this.txtId = new DevExpress.XtraEditors.TextEdit();
            this.pbPesquisaPessoa = new System.Windows.Forms.PictureBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtNomeCaixa = new DevExpress.XtraEditors.TextEdit();
            this.labCodigo = new DevExpress.XtraEditors.LabelControl();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.txtDataCadastro = new DevExpress.XtraEditors.TextEdit();
            this.cboFuncionarios = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.cboPerfilCaixa = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeCaixa.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFuncionarios.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPerfilCaixa.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Location = new System.Drawing.Point(7, 0);
            this.painelBotoes.Size = new System.Drawing.Size(459, 60);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.cboPerfilCaixa);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.cboFuncionarios);
            this.panelConteudo.Controls.Add(this.labelControl8);
            this.panelConteudo.Controls.Add(this.txtDataCadastro);
            this.panelConteudo.Controls.Add(this.labCodigo);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.txtNomeCaixa);
            this.panelConteudo.Controls.Add(this.labDataCadastro);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Controls.Add(this.pbPesquisaPessoa);
            this.panelConteudo.Controls.Add(this.txtId);
            this.panelConteudo.Size = new System.Drawing.Size(557, 98);
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
            // txtId
            // 
            this.txtId.EnterMoveNextControl = true;
            this.txtId.Location = new System.Drawing.Point(3, 18);
            this.txtId.Name = "txtId";
            this.txtId.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Properties.Appearance.Options.UseFont = true;
            this.txtId.Properties.Appearance.Options.UseTextOptions = true;
            this.txtId.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtId.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtId.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtId.Properties.Mask.EditMask = "99/";
            this.txtId.Properties.MaxLength = 8;
            this.txtId.Size = new System.Drawing.Size(96, 22);
            this.txtId.TabIndex = 1;
            this.txtId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtId_KeyPress);
            this.txtId.Leave += new System.EventHandler(this.txtId_Leave);
            // 
            // pbPesquisaPessoa
            // 
            this.pbPesquisaPessoa.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaPessoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaPessoa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaPessoa.Location = new System.Drawing.Point(107, 17);
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
            this.labelControl1.Location = new System.Drawing.Point(138, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 13);
            this.labelControl1.TabIndex = 490;
            this.labelControl1.Text = "Nome do Caixa";
            // 
            // txtNomeCaixa
            // 
            this.txtNomeCaixa.EnterMoveNextControl = true;
            this.txtNomeCaixa.Location = new System.Drawing.Point(138, 18);
            this.txtNomeCaixa.Name = "txtNomeCaixa";
            this.txtNomeCaixa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeCaixa.Properties.Appearance.Options.UseFont = true;
            this.txtNomeCaixa.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNomeCaixa.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNomeCaixa.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeCaixa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNomeCaixa.Properties.Mask.EditMask = "99/";
            this.txtNomeCaixa.Properties.MaxLength = 50;
            this.txtNomeCaixa.Size = new System.Drawing.Size(199, 22);
            this.txtNomeCaixa.TabIndex = 2;
            // 
            // labCodigo
            // 
            this.labCodigo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCodigo.Location = new System.Drawing.Point(3, 3);
            this.labCodigo.Name = "labCodigo";
            this.labCodigo.Size = new System.Drawing.Size(43, 13);
            this.labCodigo.TabIndex = 487;
            this.labCodigo.Text = "Nr. Caixa";
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Location = new System.Drawing.Point(343, 3);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(59, 13);
            this.labDataCadastro.TabIndex = 494;
            this.labDataCadastro.Text = "Dt. Cadastro";
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(450, 3);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 493;
            this.labStatus.Text = "Status";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnGravar);
            this.flowLayoutPanel1.Controls.Add(this.btnLimpar);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(508, 52);
            this.flowLayoutPanel1.TabIndex = 1001;
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(450, 18);
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
            this.cboStatus.Size = new System.Drawing.Size(104, 22);
            this.cboStatus.TabIndex = 4;
            // 
            // txtDataCadastro
            // 
            this.txtDataCadastro.Enabled = false;
            this.txtDataCadastro.EnterMoveNextControl = true;
            this.txtDataCadastro.Location = new System.Drawing.Point(343, 18);
            this.txtDataCadastro.Name = "txtDataCadastro";
            this.txtDataCadastro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataCadastro.Properties.Appearance.Options.UseFont = true;
            this.txtDataCadastro.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDataCadastro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtDataCadastro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDataCadastro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDataCadastro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtDataCadastro.Properties.Mask.EditMask = "99/";
            this.txtDataCadastro.Properties.MaxLength = 8;
            this.txtDataCadastro.Size = new System.Drawing.Size(101, 22);
            this.txtDataCadastro.TabIndex = 3;
            // 
            // cboFuncionarios
            // 
            this.cboFuncionarios.EnterMoveNextControl = true;
            this.cboFuncionarios.Location = new System.Drawing.Point(3, 60);
            this.cboFuncionarios.Name = "cboFuncionarios";
            this.cboFuncionarios.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFuncionarios.Properties.Appearance.Options.UseFont = true;
            this.cboFuncionarios.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFuncionarios.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descricao")});
            this.cboFuncionarios.Properties.DropDownRows = 5;
            this.cboFuncionarios.Properties.NullText = "";
            this.cboFuncionarios.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboFuncionarios.Size = new System.Drawing.Size(334, 22);
            this.cboFuncionarios.TabIndex = 5;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl8.Location = new System.Drawing.Point(4, 45);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(55, 13);
            this.labelControl8.TabIndex = 10061;
            this.labelControl8.Text = "Funcionário";
            // 
            // cboPerfilCaixa
            // 
            this.cboPerfilCaixa.EnterMoveNextControl = true;
            this.cboPerfilCaixa.Location = new System.Drawing.Point(343, 60);
            this.cboPerfilCaixa.Name = "cboPerfilCaixa";
            this.cboPerfilCaixa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPerfilCaixa.Properties.Appearance.Options.UseFont = true;
            this.cboPerfilCaixa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPerfilCaixa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descricao")});
            this.cboPerfilCaixa.Properties.DropDownRows = 5;
            this.cboPerfilCaixa.Properties.NullText = "";
            this.cboPerfilCaixa.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboPerfilCaixa.Size = new System.Drawing.Size(211, 22);
            this.cboPerfilCaixa.TabIndex = 10062;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Location = new System.Drawing.Point(344, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(23, 13);
            this.labelControl2.TabIndex = 10063;
            this.labelControl2.Text = "Perfil";
            // 
            // FormCadastroCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 209);
            this.Name = "FormCadastroCaixa";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeCaixa.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFuncionarios.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPerfilCaixa.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnSair;
        private DevExpress.XtraEditors.TextEdit txtId;
        private System.Windows.Forms.PictureBox pbPesquisaPessoa;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtNomeCaixa;
        private DevExpress.XtraEditors.LabelControl labCodigo;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.TextEdit txtDataCadastro;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LookUpEdit cboFuncionarios;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LookUpEdit cboPerfilCaixa;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}