namespace Programax.Easy.View.Telas.Relatorios
{
    partial class FormRelatorioCustoFinanceiro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRelatorioCustoFinanceiro));
            this.chkOrdenarPorCodigo = new System.Windows.Forms.RadioButton();
            this.pnlOrdenacao = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.rdbEstoqueMinimo = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataFinalEmissao = new DevExpress.XtraEditors.DateEdit();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.txtDataInicialEmissao = new DevExpress.XtraEditors.DateEdit();
            this.lstVendedores = new DevExpress.XtraEditors.ListBoxControl();
            this.lblVendedores = new DevExpress.XtraEditors.LabelControl();
            this.labelControl60 = new DevExpress.XtraEditors.LabelControl();
            this.txtCusto = new DevExpress.XtraEditors.TextEdit();
            this.pnlOrdenacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdbEstoqueMinimo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEmissao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEmissao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEmissao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEmissao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstVendedores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusto.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chkOrdenarPorCodigo
            // 
            this.chkOrdenarPorCodigo.AutoSize = true;
            this.chkOrdenarPorCodigo.Checked = true;
            this.chkOrdenarPorCodigo.Location = new System.Drawing.Point(13, 28);
            this.chkOrdenarPorCodigo.Name = "chkOrdenarPorCodigo";
            this.chkOrdenarPorCodigo.Size = new System.Drawing.Size(58, 17);
            this.chkOrdenarPorCodigo.TabIndex = 1;
            this.chkOrdenarPorCodigo.TabStop = true;
            this.chkOrdenarPorCodigo.Tag = "0";
            this.chkOrdenarPorCodigo.Text = "Código";
            this.chkOrdenarPorCodigo.UseVisualStyleBackColor = true;
            // 
            // pnlOrdenacao
            // 
            this.pnlOrdenacao.Controls.Add(this.radioButton2);
            this.pnlOrdenacao.Controls.Add(this.radioButton1);
            this.pnlOrdenacao.Controls.Add(this.chkOrdenarPorCodigo);
            this.pnlOrdenacao.Location = new System.Drawing.Point(12, 69);
            this.pnlOrdenacao.Name = "pnlOrdenacao";
            this.pnlOrdenacao.Size = new System.Drawing.Size(385, 57);
            this.pnlOrdenacao.TabIndex = 10096;
            this.pnlOrdenacao.TabStop = false;
            this.pnlOrdenacao.Text = "Ordernar Por";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(290, 28);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(76, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Tag = "2";
            this.radioButton2.Text = "Valor Total";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(140, 28);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(90, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.Tag = "1";
            this.radioButton1.Text = "Data Emissão";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Image = global::Programax.Easy.View.Properties.Resources.icone_Imprimir;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnImprimir.Location = new System.Drawing.Point(12, 258);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(0);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(110, 40);
            this.btnImprimir.TabIndex = 10097;
            this.btnImprimir.Text = " Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(690, 258);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 10101;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // rdbEstoqueMinimo
            // 
            this.rdbEstoqueMinimo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbEstoqueMinimo.Image = ((System.Drawing.Image)(resources.GetObject("rdbEstoqueMinimo.Image")));
            this.rdbEstoqueMinimo.Location = new System.Drawing.Point(-96, 245);
            this.rdbEstoqueMinimo.Name = "rdbEstoqueMinimo";
            this.rdbEstoqueMinimo.Size = new System.Drawing.Size(956, 10);
            this.rdbEstoqueMinimo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rdbEstoqueMinimo.TabIndex = 10100;
            this.rdbEstoqueMinimo.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-79, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(939, 10);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10099;
            this.pictureBox1.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(746, 29);
            this.labelControl1.TabIndex = 10098;
            this.labelControl1.Text = "RELATÓRIO DE CUSTO FINANCEIRO DE VENDA POR VENDEDOR";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.labelControl3);
            this.groupBox14.Controls.Add(this.txtDataFinalEmissao);
            this.groupBox14.Controls.Add(this.labDataCadastro);
            this.groupBox14.Controls.Add(this.txtDataInicialEmissao);
            this.groupBox14.Location = new System.Drawing.Point(12, 132);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(385, 61);
            this.groupBox14.TabIndex = 10123;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Período ";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(208, 31);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 13);
            this.labelControl3.TabIndex = 10143;
            this.labelControl3.Text = "Data Final";
            // 
            // txtDataFinalEmissao
            // 
            this.txtDataFinalEmissao.EditValue = null;
            this.txtDataFinalEmissao.EnterMoveNextControl = true;
            this.txtDataFinalEmissao.Location = new System.Drawing.Point(262, 26);
            this.txtDataFinalEmissao.Name = "txtDataFinalEmissao";
            this.txtDataFinalEmissao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinalEmissao.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinalEmissao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinalEmissao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinalEmissao.Size = new System.Drawing.Size(101, 22);
            this.txtDataFinalEmissao.TabIndex = 6;
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Appearance.Options.UseFont = true;
            this.labDataCadastro.Location = new System.Drawing.Point(12, 31);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(53, 13);
            this.labDataCadastro.TabIndex = 10141;
            this.labDataCadastro.Text = "Data Inicial";
            // 
            // txtDataInicialEmissao
            // 
            this.txtDataInicialEmissao.EditValue = null;
            this.txtDataInicialEmissao.EnterMoveNextControl = true;
            this.txtDataInicialEmissao.Location = new System.Drawing.Point(71, 26);
            this.txtDataInicialEmissao.Name = "txtDataInicialEmissao";
            this.txtDataInicialEmissao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicialEmissao.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicialEmissao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicialEmissao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicialEmissao.Size = new System.Drawing.Size(101, 22);
            this.txtDataInicialEmissao.TabIndex = 5;
            // 
            // lstVendedores
            // 
            this.lstVendedores.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Standard;
            this.lstVendedores.HotTrackSelectMode = DevExpress.XtraEditors.HotTrackSelectMode.SelectItemOnClick;
            this.lstVendedores.Location = new System.Drawing.Point(414, 94);
            this.lstVendedores.Name = "lstVendedores";
            this.lstVendedores.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstVendedores.Size = new System.Drawing.Size(374, 143);
            this.lstVendedores.TabIndex = 10129;
            // 
            // lblVendedores
            // 
            this.lblVendedores.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVendedores.Appearance.Options.UseFont = true;
            this.lblVendedores.Location = new System.Drawing.Point(414, 78);
            this.lblVendedores.Name = "lblVendedores";
            this.lblVendedores.Size = new System.Drawing.Size(63, 13);
            this.lblVendedores.TabIndex = 10130;
            this.lblVendedores.Text = "Vendedor(es)";
            // 
            // labelControl60
            // 
            this.labelControl60.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl60.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl60.Appearance.Options.UseFont = true;
            this.labelControl60.Appearance.Options.UseForeColor = true;
            this.labelControl60.Location = new System.Drawing.Point(12, 197);
            this.labelControl60.Name = "labelControl60";
            this.labelControl60.Size = new System.Drawing.Size(27, 13);
            this.labelControl60.TabIndex = 10132;
            this.labelControl60.Text = "Custo";
            // 
            // txtCusto
            // 
            this.txtCusto.EnterMoveNextControl = true;
            this.txtCusto.Location = new System.Drawing.Point(12, 212);
            this.txtCusto.Name = "txtCusto";
            this.txtCusto.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCusto.Properties.Appearance.Options.UseFont = true;
            this.txtCusto.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCusto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.txtCusto.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCusto.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtCusto.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCusto.Properties.MaxLength = 10;
            this.txtCusto.Size = new System.Drawing.Size(86, 22);
            this.txtCusto.TabIndex = 10131;
            // 
            // FormRelatorioCustoFinanceiro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 310);
            this.Controls.Add(this.labelControl60);
            this.Controls.Add(this.txtCusto);
            this.Controls.Add(this.lblVendedores);
            this.Controls.Add(this.lstVendedores);
            this.Controls.Add(this.groupBox14);
            this.Controls.Add(this.pnlOrdenacao);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.rdbEstoqueMinimo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormRelatorioCustoFinanceiro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRelatorioVendasPorVendedor";
            this.pnlOrdenacao.ResumeLayout(false);
            this.pnlOrdenacao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdbEstoqueMinimo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEmissao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEmissao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEmissao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEmissao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstVendedores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusto.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton chkOrdenarPorCodigo;
        private System.Windows.Forms.GroupBox pnlOrdenacao;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.PictureBox rdbEstoqueMinimo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.GroupBox groupBox14;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit txtDataFinalEmissao;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.DateEdit txtDataInicialEmissao;
        private DevExpress.XtraEditors.ListBoxControl lstVendedores;
        private DevExpress.XtraEditors.LabelControl lblVendedores;
        private DevExpress.XtraEditors.LabelControl labelControl60;
        private DevExpress.XtraEditors.TextEdit txtCusto;
    }
}