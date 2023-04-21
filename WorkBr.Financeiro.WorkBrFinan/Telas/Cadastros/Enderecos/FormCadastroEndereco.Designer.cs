namespace Programax.Easy.View.Telas.Cadastros.Enderecos
{
    partial class FormCadastroEndereco
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
            this.txtCep = new DevExpress.XtraEditors.TextEdit();
            this.txtRua = new DevExpress.XtraEditors.TextEdit();
            this.labCepEndereco = new DevExpress.XtraEditors.LabelControl();
            this.labRuaEndereco = new DevExpress.XtraEditors.LabelControl();
            this.cboCidade = new DevExpress.XtraEditors.LookUpEdit();
            this.cboEstado = new DevExpress.XtraEditors.LookUpEdit();
            this.txtBairro = new DevExpress.XtraEditors.TextEdit();
            this.labCidade = new DevExpress.XtraEditors.LabelControl();
            this.labBairro = new DevExpress.XtraEditors.LabelControl();
            this.labEstado = new DevExpress.XtraEditors.LabelControl();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.pbPesquisaPessoa = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnAtalhoCidade = new System.Windows.Forms.Button();
            this.pnlCidade = new System.Windows.Forms.Panel();
            this.txtDataCadastro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRua.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCidade.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBairro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnlCidade.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Location = new System.Drawing.Point(7, 0);
            this.painelBotoes.Size = new System.Drawing.Size(1061, 60);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.txtDataCadastro);
            this.panelConteudo.Controls.Add(this.pnlCidade);
            this.panelConteudo.Controls.Add(this.labelControl9);
            this.panelConteudo.Controls.Add(this.cboEstado);
            this.panelConteudo.Controls.Add(this.labelControl3);
            this.panelConteudo.Controls.Add(this.txtBairro);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.pbPesquisaPessoa);
            this.panelConteudo.Controls.Add(this.labRuaEndereco);
            this.panelConteudo.Controls.Add(this.labBairro);
            this.panelConteudo.Controls.Add(this.labCepEndereco);
            this.panelConteudo.Controls.Add(this.labEstado);
            this.panelConteudo.Controls.Add(this.txtRua);
            this.panelConteudo.Controls.Add(this.txtCep);
            this.panelConteudo.Size = new System.Drawing.Size(600, 144);
            // 
            // txtCep
            // 
            this.txtCep.EnterMoveNextControl = true;
            this.txtCep.Location = new System.Drawing.Point(3, 20);
            this.txtCep.Name = "txtCep";
            this.txtCep.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCep.Properties.Appearance.Options.UseFont = true;
            this.txtCep.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCep.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtCep.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtCep.Properties.HideSelection = false;
            this.txtCep.Properties.Mask.EditMask = "99999-999";
            this.txtCep.Properties.Mask.IgnoreMaskBlank = false;
            this.txtCep.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.txtCep.Properties.MaxLength = 80;
            this.txtCep.Size = new System.Drawing.Size(124, 22);
            this.txtCep.TabIndex = 1;
            this.txtCep.EditValueChanged += new System.EventHandler(this.txtCep_EditValueChanged);
            this.txtCep.Leave += new System.EventHandler(this.txtCep_Leave);
            // 
            // txtRua
            // 
            this.txtRua.EnterMoveNextControl = true;
            this.txtRua.Location = new System.Drawing.Point(323, 69);
            this.txtRua.Name = "txtRua";
            this.txtRua.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRua.Properties.Appearance.Options.UseFont = true;
            this.txtRua.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRua.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtRua.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtRua.Properties.Mask.EditMask = "99/";
            this.txtRua.Properties.MaxLength = 80;
            this.txtRua.Size = new System.Drawing.Size(275, 22);
            this.txtRua.TabIndex = 5;
            // 
            // labCepEndereco
            // 
            this.labCepEndereco.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCepEndereco.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labCepEndereco.Appearance.Options.UseFont = true;
            this.labCepEndereco.Appearance.Options.UseForeColor = true;
            this.labCepEndereco.Location = new System.Drawing.Point(3, 3);
            this.labCepEndereco.Name = "labCepEndereco";
            this.labCepEndereco.Size = new System.Drawing.Size(19, 13);
            this.labCepEndereco.TabIndex = 461;
            this.labCepEndereco.Text = "Cep";
            // 
            // labRuaEndereco
            // 
            this.labRuaEndereco.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labRuaEndereco.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labRuaEndereco.Appearance.Options.UseFont = true;
            this.labRuaEndereco.Appearance.Options.UseForeColor = true;
            this.labRuaEndereco.Location = new System.Drawing.Point(323, 51);
            this.labRuaEndereco.Name = "labRuaEndereco";
            this.labRuaEndereco.Size = new System.Drawing.Size(20, 13);
            this.labRuaEndereco.TabIndex = 460;
            this.labRuaEndereco.Text = "Rua";
            // 
            // cboCidade
            // 
            this.cboCidade.EnterMoveNextControl = true;
            this.cboCidade.Location = new System.Drawing.Point(0, 17);
            this.cboCidade.Name = "cboCidade";
            this.cboCidade.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCidade.Properties.Appearance.Options.UseFont = true;
            this.cboCidade.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCidade.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboCidade.Properties.NullText = "";
            this.cboCidade.Size = new System.Drawing.Size(212, 22);
            this.cboCidade.TabIndex = 3;
            // 
            // cboEstado
            // 
            this.cboEstado.EnterMoveNextControl = true;
            this.cboEstado.Location = new System.Drawing.Point(161, 20);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEstado.Properties.Appearance.Options.UseFont = true;
            this.cboEstado.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEstado.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UF", "UF", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nome", "Nome")});
            this.cboEstado.Properties.NullText = "";
            this.cboEstado.Size = new System.Drawing.Size(185, 22);
            this.cboEstado.TabIndex = 2;
            this.cboEstado.EditValueChanged += new System.EventHandler(this.cboEstado_EditValueChanged);
            // 
            // txtBairro
            // 
            this.txtBairro.EnterMoveNextControl = true;
            this.txtBairro.Location = new System.Drawing.Point(3, 69);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBairro.Properties.Appearance.Options.UseFont = true;
            this.txtBairro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtBairro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtBairro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtBairro.Properties.Mask.EditMask = "99/";
            this.txtBairro.Properties.MaxLength = 30;
            this.txtBairro.Size = new System.Drawing.Size(314, 22);
            this.txtBairro.TabIndex = 4;
            // 
            // labCidade
            // 
            this.labCidade.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCidade.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labCidade.Appearance.Options.UseFont = true;
            this.labCidade.Appearance.Options.UseForeColor = true;
            this.labCidade.Location = new System.Drawing.Point(0, 0);
            this.labCidade.Name = "labCidade";
            this.labCidade.Size = new System.Drawing.Size(33, 13);
            this.labCidade.TabIndex = 467;
            this.labCidade.Text = "Cidade";
            // 
            // labBairro
            // 
            this.labBairro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labBairro.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labBairro.Appearance.Options.UseFont = true;
            this.labBairro.Appearance.Options.UseForeColor = true;
            this.labBairro.Location = new System.Drawing.Point(3, 53);
            this.labBairro.Name = "labBairro";
            this.labBairro.Size = new System.Drawing.Size(27, 13);
            this.labBairro.TabIndex = 466;
            this.labBairro.Text = "Bairro";
            // 
            // labEstado
            // 
            this.labEstado.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labEstado.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labEstado.Appearance.Options.UseFont = true;
            this.labEstado.Appearance.Options.UseForeColor = true;
            this.labEstado.Location = new System.Drawing.Point(161, 3);
            this.labEstado.Name = "labEstado";
            this.labEstado.Size = new System.Drawing.Size(33, 13);
            this.labEstado.TabIndex = 465;
            this.labEstado.Text = "Estado";
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
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
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
            this.btnGravar.TabIndex = 7;
            this.btnGravar.Text = " Salvar";
            this.btnGravar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGravar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // pbPesquisaPessoa
            // 
            this.pbPesquisaPessoa.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaPessoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaPessoa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaPessoa.Location = new System.Drawing.Point(133, 20);
            this.pbPesquisaPessoa.Name = "pbPesquisaPessoa";
            this.pbPesquisaPessoa.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaPessoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaPessoa.TabIndex = 457;
            this.pbPesquisaPessoa.TabStop = false;
            this.pbPesquisaPessoa.Click += new System.EventHandler(this.pbPesquisaPessoa_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnGravar);
            this.flowLayoutPanel1.Controls.Add(this.btnLimpar);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(665, 42);
            this.flowLayoutPanel1.TabIndex = 1000;
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
            this.btnLimpar.TabIndex = 1001;
            this.btnLimpar.Text = " Limpar";
            this.btnLimpar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnAtalhoCidade
            // 
            this.btnAtalhoCidade.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtalhoCidade.FlatAppearance.BorderSize = 0;
            this.btnAtalhoCidade.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAtalhoCidade.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAtalhoCidade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtalhoCidade.Image = global::Programax.Easy.View.Properties.Resources.icones2_19;
            this.btnAtalhoCidade.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAtalhoCidade.Location = new System.Drawing.Point(218, 16);
            this.btnAtalhoCidade.Name = "btnAtalhoCidade";
            this.btnAtalhoCidade.Size = new System.Drawing.Size(28, 23);
            this.btnAtalhoCidade.TabIndex = 10014;
            this.btnAtalhoCidade.TabStop = false;
            this.btnAtalhoCidade.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAtalhoCidade.UseVisualStyleBackColor = true;
            this.btnAtalhoCidade.Click += new System.EventHandler(this.btnAtalhoCidade_Click);
            // 
            // pnlCidade
            // 
            this.pnlCidade.Controls.Add(this.labCidade);
            this.pnlCidade.Controls.Add(this.btnAtalhoCidade);
            this.pnlCidade.Controls.Add(this.cboCidade);
            this.pnlCidade.Location = new System.Drawing.Point(352, 3);
            this.pnlCidade.Name = "pnlCidade";
            this.pnlCidade.Size = new System.Drawing.Size(246, 46);
            this.pnlCidade.TabIndex = 3;
            // 
            // txtDataCadastro
            // 
            this.txtDataCadastro.EnterMoveNextControl = true;
            this.txtDataCadastro.Location = new System.Drawing.Point(118, 117);
            this.txtDataCadastro.Name = "txtDataCadastro";
            this.txtDataCadastro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataCadastro.Properties.Appearance.Options.UseFont = true;
            this.txtDataCadastro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDataCadastro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDataCadastro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataCadastro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDataCadastro.Properties.Mask.EditMask = "99/";
            this.txtDataCadastro.Properties.MaxLength = 30;
            this.txtDataCadastro.Properties.ReadOnly = true;
            this.txtDataCadastro.Size = new System.Drawing.Size(113, 22);
            this.txtDataCadastro.TabIndex = 7;
            this.txtDataCadastro.TabStop = false;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.Location = new System.Drawing.Point(118, 102);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(59, 13);
            this.labelControl9.TabIndex = 10017;
            this.labelControl9.Text = "Dt. Cadastro";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(3, 102);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(30, 13);
            this.labelControl3.TabIndex = 10015;
            this.labelControl3.Text = "Status";
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(3, 117);
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
            this.cboStatus.TabIndex = 6;
            // 
            // FormCadastroEndereco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 255);
            this.Name = "FormCadastroEndereco";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRua.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCidade.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBairro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnlCidade.ResumeLayout(false);
            this.pnlCidade.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPesquisaPessoa;
        private DevExpress.XtraEditors.TextEdit txtCep;
        private DevExpress.XtraEditors.TextEdit txtRua;
        private DevExpress.XtraEditors.LabelControl labCepEndereco;
        private DevExpress.XtraEditors.LabelControl labRuaEndereco;
        private DevExpress.XtraEditors.LookUpEdit cboCidade;
        private DevExpress.XtraEditors.LookUpEdit cboEstado;
        private DevExpress.XtraEditors.TextEdit txtBairro;
        private DevExpress.XtraEditors.LabelControl labCidade;
        private DevExpress.XtraEditors.LabelControl labBairro;
        private DevExpress.XtraEditors.LabelControl labEstado;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Panel pnlCidade;
        private System.Windows.Forms.Button btnAtalhoCidade;
        private DevExpress.XtraEditors.TextEdit txtDataCadastro;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
    }
}