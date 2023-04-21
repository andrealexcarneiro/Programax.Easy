namespace Programax.Easy.View.Telas.Financeiro.ContasPagarReceber
{
    partial class FormInserirPagamentoParcial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInserirPagamentoParcial));
            this.labelControl68 = new DevExpress.XtraEditors.LabelControl();
            this.cboFormaPagamento = new DevExpress.XtraEditors.LookUpEdit();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataPagamento = new DevExpress.XtraEditors.DateEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtValor = new DevExpress.XtraEditors.TextEdit();
            this.txtObservacoes = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtValorAReceber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtValorTitulo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataVencimento = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.cboFormaPagamento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataPagamento.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataPagamento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacoes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValorAReceber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValorTitulo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVencimento.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl68
            // 
            this.labelControl68.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl68.Location = new System.Drawing.Point(12, 81);
            this.labelControl68.Name = "labelControl68";
            this.labelControl68.Size = new System.Drawing.Size(86, 13);
            this.labelControl68.TabIndex = 10072;
            this.labelControl68.Text = "Forma Pagamento";
            // 
            // cboFormaPagamento
            // 
            this.cboFormaPagamento.EnterMoveNextControl = true;
            this.cboFormaPagamento.Location = new System.Drawing.Point(12, 97);
            this.cboFormaPagamento.Name = "cboFormaPagamento";
            this.cboFormaPagamento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFormaPagamento.Properties.Appearance.Options.UseFont = true;
            this.cboFormaPagamento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFormaPagamento.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboFormaPagamento.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 5, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboFormaPagamento.Properties.NullText = "";
            this.cboFormaPagamento.Size = new System.Drawing.Size(346, 22);
            this.cboFormaPagamento.TabIndex = 1;
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizar.FlatAppearance.BorderSize = 0;
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizar.Image = global::Programax.Easy.View.Properties.Resources.icone_selecionar1;
            this.btnAtualizar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAtualizar.Location = new System.Drawing.Point(12, 328);
            this.btnAtualizar.Margin = new System.Windows.Forms.Padding(0);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(110, 40);
            this.btnAtualizar.TabIndex = 10070;
            this.btnAtualizar.TabStop = false;
            this.btnAtualizar.Text = " Salvar";
            this.btnAtualizar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAtualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(537, 328);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(85, 40);
            this.btnSair.TabIndex = 10069;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-96, 315);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(746, 10);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10068;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-79, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(737, 10);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10067;
            this.pictureBox1.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.labelControl1.Location = new System.Drawing.Point(12, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(346, 29);
            this.labelControl1.TabIndex = 10066;
            this.labelControl1.Text = "BAIXA / PAGAMENTO PARCIAL";
            // 
            // txtDataPagamento
            // 
            this.txtDataPagamento.EditValue = "";
            this.txtDataPagamento.EnterMoveNextControl = true;
            this.txtDataPagamento.Location = new System.Drawing.Point(496, 97);
            this.txtDataPagamento.Name = "txtDataPagamento";
            this.txtDataPagamento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataPagamento.Properties.Appearance.Options.UseFont = true;
            this.txtDataPagamento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataPagamento.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataPagamento.Size = new System.Drawing.Size(123, 22);
            this.txtDataPagamento.TabIndex = 4;
            this.txtDataPagamento.EditValueChanged += new System.EventHandler(this.txtDataPagamento_EditValueChanged);
            // 
            // labelControl17
            // 
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl17.Location = new System.Drawing.Point(496, 82);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(80, 13);
            this.labelControl17.TabIndex = 10095;
            this.labelControl17.Text = "Data Pagamento";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl8.Location = new System.Drawing.Point(469, 128);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(64, 13);
            this.labelControl8.TabIndex = 10094;
            this.labelControl8.Text = "Valor à Pagar";
            // 
            // txtValor
            // 
            this.txtValor.EnterMoveNextControl = true;
            this.txtValor.Location = new System.Drawing.Point(469, 144);
            this.txtValor.Name = "txtValor";
            this.txtValor.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.Properties.Appearance.Options.UseFont = true;
            this.txtValor.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtValor.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtValor.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtValor.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtValor.Properties.Mask.EditMask = "99/";
            this.txtValor.Size = new System.Drawing.Size(150, 22);
            this.txtValor.TabIndex = 6;
            // 
            // txtObservacoes
            // 
            this.txtObservacoes.Location = new System.Drawing.Point(11, 229);
            this.txtObservacoes.Name = "txtObservacoes";
            this.txtObservacoes.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservacoes.Properties.Appearance.Options.UseFont = true;
            this.txtObservacoes.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtObservacoes.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtObservacoes.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservacoes.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtObservacoes.Properties.MaxLength = 2000;
            this.txtObservacoes.Size = new System.Drawing.Size(611, 76);
            this.txtObservacoes.TabIndex = 7;
            this.txtObservacoes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtObservacoes_KeyPress);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(11, 212);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(63, 13);
            this.labelControl2.TabIndex = 10096;
            this.labelControl2.Text = "Observações";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl3.Location = new System.Drawing.Point(244, 128);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(77, 13);
            this.labelControl3.TabIndex = 10098;
            this.labelControl3.Text = "Valor à Receber";
            // 
            // txtValorAReceber
            // 
            this.txtValorAReceber.EnterMoveNextControl = true;
            this.txtValorAReceber.Location = new System.Drawing.Point(244, 144);
            this.txtValorAReceber.Name = "txtValorAReceber";
            this.txtValorAReceber.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorAReceber.Properties.Appearance.Options.UseFont = true;
            this.txtValorAReceber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtValorAReceber.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtValorAReceber.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtValorAReceber.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtValorAReceber.Properties.Mask.EditMask = "99/";
            this.txtValorAReceber.Properties.ReadOnly = true;
            this.txtValorAReceber.Size = new System.Drawing.Size(150, 22);
            this.txtValorAReceber.TabIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl5.Location = new System.Drawing.Point(11, 128);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(55, 13);
            this.labelControl5.TabIndex = 10102;
            this.labelControl5.Text = "Valor Título";
            // 
            // txtValorTitulo
            // 
            this.txtValorTitulo.EnterMoveNextControl = true;
            this.txtValorTitulo.Location = new System.Drawing.Point(12, 144);
            this.txtValorTitulo.Name = "txtValorTitulo";
            this.txtValorTitulo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorTitulo.Properties.Appearance.Options.UseFont = true;
            this.txtValorTitulo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtValorTitulo.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtValorTitulo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtValorTitulo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtValorTitulo.Properties.Mask.EditMask = "99/";
            this.txtValorTitulo.Properties.ReadOnly = true;
            this.txtValorTitulo.Size = new System.Drawing.Size(150, 22);
            this.txtValorTitulo.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Location = new System.Drawing.Point(366, 81);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(82, 13);
            this.labelControl4.TabIndex = 10104;
            this.labelControl4.Text = "Data Vencimento";
            // 
            // txtDataVencimento
            // 
            this.txtDataVencimento.EnterMoveNextControl = true;
            this.txtDataVencimento.Location = new System.Drawing.Point(367, 97);
            this.txtDataVencimento.Name = "txtDataVencimento";
            this.txtDataVencimento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataVencimento.Properties.Appearance.Options.UseFont = true;
            this.txtDataVencimento.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDataVencimento.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDataVencimento.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataVencimento.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDataVencimento.Properties.Mask.EditMask = "99/";
            this.txtDataVencimento.Properties.ReadOnly = true;
            this.txtDataVencimento.Size = new System.Drawing.Size(123, 22);
            this.txtDataVencimento.TabIndex = 3;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl6.Location = new System.Drawing.Point(133, 187);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(357, 13);
            this.labelControl6.TabIndex = 10105;
            this.labelControl6.Text = "Obs.: Ao Informar Valor à Pagar igual Valor à Receber, o título será quitado.";
            // 
            // FormInserirPagamentoParcial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 374);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtDataVencimento);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtValorTitulo);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtValorAReceber);
            this.Controls.Add(this.txtObservacoes);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtDataPagamento);
            this.Controls.Add(this.labelControl17);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.labelControl68);
            this.Controls.Add(this.cboFormaPagamento);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormInserirPagamentoParcial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormInserirPagamentoParcial";
            ((System.ComponentModel.ISupportInitialize)(this.cboFormaPagamento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataPagamento.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataPagamento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacoes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValorAReceber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValorTitulo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVencimento.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl68;
        private DevExpress.XtraEditors.LookUpEdit cboFormaPagamento;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit txtDataPagamento;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtValor;
        private DevExpress.XtraEditors.MemoEdit txtObservacoes;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtValorAReceber;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtValorTitulo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtDataVencimento;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}