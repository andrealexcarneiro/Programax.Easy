﻿namespace Programax.Easy.View.Telas.Cadastros.SubGrupos
{
    partial class FormCadastroSubGrupos
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
            this.txtDescricao = new DevExpress.XtraEditors.TextEdit();
            this.labCodigo = new DevExpress.XtraEditors.LabelControl();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.cboGrupos = new DevExpress.XtraEditors.LookUpEdit();
            this.labTipoInscricao = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtDataCadastro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.btnAtalhoGrupo = new System.Windows.Forms.Button();
            this.pnlGrupo = new System.Windows.Forms.Panel();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGrupos.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).BeginInit();
            this.pnlGrupo.SuspendLayout();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Location = new System.Drawing.Point(9, 0);
            this.painelBotoes.Size = new System.Drawing.Size(695, 74);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.pnlGrupo);
            this.panelConteudo.Controls.Add(this.txtDataCadastro);
            this.panelConteudo.Controls.Add(this.labelControl9);
            this.panelConteudo.Controls.Add(this.txtId);
            this.panelConteudo.Controls.Add(this.labCodigo);
            this.panelConteudo.Controls.Add(this.txtDescricao);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.pbPesquisaPessoa);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Margin = new System.Windows.Forms.Padding(5);
            this.panelConteudo.Size = new System.Drawing.Size(745, 121);
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
            this.btnGravar.Size = new System.Drawing.Size(133, 49);
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
            this.btnLimpar.Location = new System.Drawing.Point(133, 0);
            this.btnLimpar.Margin = new System.Windows.Forms.Padding(0);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(133, 49);
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
            this.btnSair.Location = new System.Drawing.Point(266, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(133, 49);
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
            this.txtId.Location = new System.Drawing.Point(4, 25);
            this.txtId.Margin = new System.Windows.Forms.Padding(4);
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
            this.txtId.Size = new System.Drawing.Size(128, 26);
            this.txtId.TabIndex = 486;
            this.txtId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtId_KeyPress);
            this.txtId.Leave += new System.EventHandler(this.txtId_Leave);
            // 
            // pbPesquisaPessoa
            // 
            this.pbPesquisaPessoa.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaPessoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaPessoa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaPessoa.Location = new System.Drawing.Point(143, 23);
            this.pbPesquisaPessoa.Margin = new System.Windows.Forms.Padding(4);
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
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(184, 6);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(63, 17);
            this.labelControl1.TabIndex = 490;
            this.labelControl1.Text = "Descrição";
            // 
            // txtDescricao
            // 
            this.txtDescricao.EnterMoveNextControl = true;
            this.txtDescricao.Location = new System.Drawing.Point(184, 25);
            this.txtDescricao.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.Properties.Appearance.Options.UseFont = true;
            this.txtDescricao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricao.Properties.Mask.EditMask = "99/";
            this.txtDescricao.Properties.MaxLength = 50;
            this.txtDescricao.Size = new System.Drawing.Size(265, 26);
            this.txtDescricao.TabIndex = 489;
            // 
            // labCodigo
            // 
            this.labCodigo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCodigo.Appearance.Options.UseFont = true;
            this.labCodigo.Location = new System.Drawing.Point(4, 6);
            this.labCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.labCodigo.Name = "labCodigo";
            this.labCodigo.Size = new System.Drawing.Size(90, 17);
            this.labCodigo.TabIndex = 487;
            this.labCodigo.Text = "Cód. Cadastro";
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(457, 25);
            this.cboStatus.Margin = new System.Windows.Forms.Padding(4);
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
            this.cboStatus.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboStatus.Size = new System.Drawing.Size(139, 26);
            this.cboStatus.TabIndex = 492;
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Appearance.Options.UseFont = true;
            this.labStatus.Location = new System.Drawing.Point(457, 6);
            this.labStatus.Margin = new System.Windows.Forms.Padding(4);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(40, 17);
            this.labStatus.TabIndex = 493;
            this.labStatus.Text = "Status";
            // 
            // cboGrupos
            // 
            this.cboGrupos.EnterMoveNextControl = true;
            this.cboGrupos.Location = new System.Drawing.Point(0, 18);
            this.cboGrupos.Margin = new System.Windows.Forms.Padding(4);
            this.cboGrupos.Name = "cboGrupos";
            this.cboGrupos.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGrupos.Properties.Appearance.Options.UseFont = true;
            this.cboGrupos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboGrupos.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Cód. Cadatro", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboGrupos.Properties.DropDownRows = 5;
            this.cboGrupos.Properties.NullText = "";
            this.cboGrupos.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboGrupos.Size = new System.Drawing.Size(696, 26);
            this.cboGrupos.TabIndex = 647;
            // 
            // labTipoInscricao
            // 
            this.labTipoInscricao.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTipoInscricao.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labTipoInscricao.Appearance.Options.UseFont = true;
            this.labTipoInscricao.Appearance.Options.UseForeColor = true;
            this.labTipoInscricao.Location = new System.Drawing.Point(0, 0);
            this.labTipoInscricao.Margin = new System.Windows.Forms.Padding(4);
            this.labTipoInscricao.Name = "labTipoInscricao";
            this.labTipoInscricao.Size = new System.Drawing.Size(40, 17);
            this.labTipoInscricao.TabIndex = 648;
            this.labTipoInscricao.Text = "Grupo";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnGravar);
            this.flowLayoutPanel1.Controls.Add(this.btnLimpar);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(683, 60);
            this.flowLayoutPanel1.TabIndex = 1001;
            // 
            // txtDataCadastro
            // 
            this.txtDataCadastro.EnterMoveNextControl = true;
            this.txtDataCadastro.Location = new System.Drawing.Point(604, 25);
            this.txtDataCadastro.Margin = new System.Windows.Forms.Padding(4);
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
            this.txtDataCadastro.Size = new System.Drawing.Size(135, 26);
            this.txtDataCadastro.TabIndex = 10026;
            this.txtDataCadastro.TabStop = false;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.Location = new System.Drawing.Point(604, 6);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(79, 17);
            this.labelControl9.TabIndex = 10027;
            this.labelControl9.Text = "Dt. Cadastro";
            // 
            // btnAtalhoGrupo
            // 
            this.btnAtalhoGrupo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtalhoGrupo.FlatAppearance.BorderSize = 0;
            this.btnAtalhoGrupo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAtalhoGrupo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAtalhoGrupo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtalhoGrupo.Image = global::Programax.Easy.View.Properties.Resources.icones2_19;
            this.btnAtalhoGrupo.Location = new System.Drawing.Point(704, 17);
            this.btnAtalhoGrupo.Margin = new System.Windows.Forms.Padding(4);
            this.btnAtalhoGrupo.Name = "btnAtalhoGrupo";
            this.btnAtalhoGrupo.Size = new System.Drawing.Size(31, 28);
            this.btnAtalhoGrupo.TabIndex = 10028;
            this.btnAtalhoGrupo.TabStop = false;
            this.btnAtalhoGrupo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAtalhoGrupo.UseVisualStyleBackColor = true;
            this.btnAtalhoGrupo.Click += new System.EventHandler(this.btnAtalhoGrupo_Click);
            // 
            // pnlGrupo
            // 
            this.pnlGrupo.Controls.Add(this.labTipoInscricao);
            this.pnlGrupo.Controls.Add(this.btnAtalhoGrupo);
            this.pnlGrupo.Controls.Add(this.cboGrupos);
            this.pnlGrupo.Location = new System.Drawing.Point(4, 59);
            this.pnlGrupo.Margin = new System.Windows.Forms.Padding(4);
            this.pnlGrupo.Name = "pnlGrupo";
            this.pnlGrupo.Size = new System.Drawing.Size(735, 57);
            this.pnlGrupo.TabIndex = 10029;
            // 
            // FormCadastroSubGrupos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 256);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "FormCadastroSubGrupos";
            this.Load += new System.EventHandler(this.FormCadastroGrupo_Load);
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGrupos.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).EndInit();
            this.pnlGrupo.ResumeLayout(false);
            this.pnlGrupo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnSair;
        private DevExpress.XtraEditors.TextEdit txtId;
        private System.Windows.Forms.PictureBox pbPesquisaPessoa;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDescricao;
        private DevExpress.XtraEditors.LabelControl labCodigo;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraEditors.LookUpEdit cboGrupos;
        private DevExpress.XtraEditors.LabelControl labTipoInscricao;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.TextEdit txtDataCadastro;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private System.Windows.Forms.Button btnAtalhoGrupo;
        private System.Windows.Forms.Panel pnlGrupo;
    }
}