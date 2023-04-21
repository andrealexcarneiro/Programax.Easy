namespace Programax.Easy.View.Telas.Relatorios
{
    partial class FormRelatorioContasPagarReceber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRelatorioContasPagarReceber));
            this.pnlOrdenacao = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rdbOrdemListaCodigo = new System.Windows.Forms.RadioButton();
            this.cboStatusTitulo = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataFinalPeriodo = new DevExpress.XtraEditors.DateEdit();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.txtDataInicialPeriodo = new DevExpress.XtraEditors.DateEdit();
            this.labelControl60 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl84 = new DevExpress.XtraEditors.LabelControl();
            this.txtIdParceiro = new DevExpress.XtraEditors.TextEdit();
            this.btnPesquisaParceiro = new System.Windows.Forms.PictureBox();
            this.txtNomeParceiro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl61 = new DevExpress.XtraEditors.LabelControl();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTituloRelatorio = new DevExpress.XtraEditors.LabelControl();
            this.cboDataFiltrar = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cboCategorias = new DevExpress.XtraEditors.LookUpEdit();
            this.pnlOrdenacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatusTitulo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdParceiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaParceiro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeParceiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDataFiltrar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategorias.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOrdenacao
            // 
            this.pnlOrdenacao.Controls.Add(this.radioButton2);
            this.pnlOrdenacao.Controls.Add(this.radioButton1);
            this.pnlOrdenacao.Controls.Add(this.rdbOrdemListaCodigo);
            this.pnlOrdenacao.Location = new System.Drawing.Point(12, 67);
            this.pnlOrdenacao.Name = "pnlOrdenacao";
            this.pnlOrdenacao.Size = new System.Drawing.Size(377, 57);
            this.pnlOrdenacao.TabIndex = 10111;
            this.pnlOrdenacao.TabStop = false;
            this.pnlOrdenacao.Text = "Ordernar Por";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(237, 28);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Tag = "2";
            this.radioButton2.Text = "Nome Parceiro";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbOrdemListaCodigo_KeyDown);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(126, 28);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(98, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.Tag = "1";
            this.radioButton1.Text = "Dt. Vencimento";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbOrdemListaCodigo_KeyDown);
            // 
            // rdbOrdemListaCodigo
            // 
            this.rdbOrdemListaCodigo.AutoSize = true;
            this.rdbOrdemListaCodigo.Checked = true;
            this.rdbOrdemListaCodigo.Location = new System.Drawing.Point(13, 28);
            this.rdbOrdemListaCodigo.Name = "rdbOrdemListaCodigo";
            this.rdbOrdemListaCodigo.Size = new System.Drawing.Size(101, 17);
            this.rdbOrdemListaCodigo.TabIndex = 1;
            this.rdbOrdemListaCodigo.TabStop = true;
            this.rdbOrdemListaCodigo.Tag = "0";
            this.rdbOrdemListaCodigo.Text = "Nr. Lançamento";
            this.rdbOrdemListaCodigo.UseVisualStyleBackColor = true;
            this.rdbOrdemListaCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbOrdemListaCodigo_KeyDown);
            // 
            // cboStatusTitulo
            // 
            this.cboStatusTitulo.EnterMoveNextControl = true;
            this.cboStatusTitulo.Location = new System.Drawing.Point(417, 149);
            this.cboStatusTitulo.Name = "cboStatusTitulo";
            this.cboStatusTitulo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatusTitulo.Properties.Appearance.Options.UseFont = true;
            this.cboStatusTitulo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatusTitulo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatusTitulo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 5, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboStatusTitulo.Properties.NullText = "";
            this.cboStatusTitulo.Size = new System.Drawing.Size(373, 22);
            this.cboStatusTitulo.TabIndex = 9;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(690, 76);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 13);
            this.labelControl2.TabIndex = 10143;
            this.labelControl2.Text = "Data Final";
            // 
            // txtDataFinalPeriodo
            // 
            this.txtDataFinalPeriodo.EditValue = null;
            this.txtDataFinalPeriodo.EnterMoveNextControl = true;
            this.txtDataFinalPeriodo.Location = new System.Drawing.Point(689, 93);
            this.txtDataFinalPeriodo.Name = "txtDataFinalPeriodo";
            this.txtDataFinalPeriodo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinalPeriodo.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinalPeriodo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinalPeriodo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinalPeriodo.Size = new System.Drawing.Size(101, 22);
            this.txtDataFinalPeriodo.TabIndex = 6;
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Appearance.Options.UseFont = true;
            this.labDataCadastro.Location = new System.Drawing.Point(582, 76);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(53, 13);
            this.labDataCadastro.TabIndex = 10141;
            this.labDataCadastro.Text = "Data Inicial";
            // 
            // txtDataInicialPeriodo
            // 
            this.txtDataInicialPeriodo.EditValue = null;
            this.txtDataInicialPeriodo.EnterMoveNextControl = true;
            this.txtDataInicialPeriodo.Location = new System.Drawing.Point(582, 93);
            this.txtDataInicialPeriodo.Name = "txtDataInicialPeriodo";
            this.txtDataInicialPeriodo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicialPeriodo.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicialPeriodo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicialPeriodo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicialPeriodo.Size = new System.Drawing.Size(101, 22);
            this.txtDataInicialPeriodo.TabIndex = 5;
            // 
            // labelControl60
            // 
            this.labelControl60.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl60.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl60.Appearance.Options.UseFont = true;
            this.labelControl60.Appearance.Options.UseForeColor = true;
            this.labelControl60.Location = new System.Drawing.Point(12, 134);
            this.labelControl60.Name = "labelControl60";
            this.labelControl60.Size = new System.Drawing.Size(64, 13);
            this.labelControl60.TabIndex = 10130;
            this.labelControl60.Text = "Cód. Parceiro";
            // 
            // labelControl84
            // 
            this.labelControl84.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl84.Appearance.Options.UseFont = true;
            this.labelControl84.Location = new System.Drawing.Point(416, 134);
            this.labelControl84.Name = "labelControl84";
            this.labelControl84.Size = new System.Drawing.Size(86, 13);
            this.labelControl84.TabIndex = 10139;
            this.labelControl84.Text = "Situação do Titulo";
            // 
            // txtIdParceiro
            // 
            this.txtIdParceiro.EnterMoveNextControl = true;
            this.txtIdParceiro.Location = new System.Drawing.Point(12, 149);
            this.txtIdParceiro.Name = "txtIdParceiro";
            this.txtIdParceiro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdParceiro.Properties.Appearance.Options.UseFont = true;
            this.txtIdParceiro.Properties.Appearance.Options.UseTextOptions = true;
            this.txtIdParceiro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtIdParceiro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtIdParceiro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtIdParceiro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIdParceiro.Properties.MaxLength = 10;
            this.txtIdParceiro.Size = new System.Drawing.Size(86, 22);
            this.txtIdParceiro.TabIndex = 7;
            this.txtIdParceiro.Leave += new System.EventHandler(this.txtIdParceiro_Leave);
            // 
            // btnPesquisaParceiro
            // 
            this.btnPesquisaParceiro.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaParceiro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaParceiro.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisaParceiro.Image")));
            this.btnPesquisaParceiro.Location = new System.Drawing.Point(107, 149);
            this.btnPesquisaParceiro.Name = "btnPesquisaParceiro";
            this.btnPesquisaParceiro.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaParceiro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaParceiro.TabIndex = 10131;
            this.btnPesquisaParceiro.TabStop = false;
            this.btnPesquisaParceiro.Click += new System.EventHandler(this.btnPesquisaParceiro_Click);
            // 
            // txtNomeParceiro
            // 
            this.txtNomeParceiro.EnterMoveNextControl = true;
            this.txtNomeParceiro.Location = new System.Drawing.Point(137, 149);
            this.txtNomeParceiro.Name = "txtNomeParceiro";
            this.txtNomeParceiro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeParceiro.Properties.Appearance.Options.UseFont = true;
            this.txtNomeParceiro.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNomeParceiro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNomeParceiro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNomeParceiro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNomeParceiro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNomeParceiro.Properties.Mask.EditMask = "99/";
            this.txtNomeParceiro.Properties.ReadOnly = true;
            this.txtNomeParceiro.Size = new System.Drawing.Size(252, 22);
            this.txtNomeParceiro.TabIndex = 8;
            this.txtNomeParceiro.TabStop = false;
            // 
            // labelControl61
            // 
            this.labelControl61.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl61.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl61.Appearance.Options.UseFont = true;
            this.labelControl61.Appearance.Options.UseForeColor = true;
            this.labelControl61.Location = new System.Drawing.Point(137, 134);
            this.labelControl61.Name = "labelControl61";
            this.labelControl61.Size = new System.Drawing.Size(70, 13);
            this.labelControl61.TabIndex = 10132;
            this.labelControl61.Text = "Nome Parceiro";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Image = global::Programax.Easy.View.Properties.Resources.icone_Imprimir;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnImprimir.Location = new System.Drawing.Point(12, 298);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(0);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(110, 40);
            this.btnImprimir.TabIndex = 10120;
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
            this.btnSair.Location = new System.Drawing.Point(690, 295);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 10124;
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
            this.pictureBox2.Location = new System.Drawing.Point(-96, 255);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1025, 10);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10123;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-79, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1006, 10);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10122;
            this.pictureBox1.TabStop = false;
            // 
            // lblTituloRelatorio
            // 
            this.lblTituloRelatorio.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloRelatorio.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.lblTituloRelatorio.Appearance.Options.UseFont = true;
            this.lblTituloRelatorio.Appearance.Options.UseForeColor = true;
            this.lblTituloRelatorio.Location = new System.Drawing.Point(12, 12);
            this.lblTituloRelatorio.Name = "lblTituloRelatorio";
            this.lblTituloRelatorio.Size = new System.Drawing.Size(322, 29);
            this.lblTituloRelatorio.TabIndex = 10121;
            this.lblTituloRelatorio.Text = "RELATÓRIO DE PARCEIROS";
            // 
            // cboDataFiltrar
            // 
            this.cboDataFiltrar.EnterMoveNextControl = true;
            this.cboDataFiltrar.Location = new System.Drawing.Point(417, 93);
            this.cboDataFiltrar.Name = "cboDataFiltrar";
            this.cboDataFiltrar.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDataFiltrar.Properties.Appearance.Options.UseFont = true;
            this.cboDataFiltrar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDataFiltrar.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboDataFiltrar.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboDataFiltrar.Properties.DropDownRows = 4;
            this.cboDataFiltrar.Properties.NullText = "";
            this.cboDataFiltrar.Size = new System.Drawing.Size(159, 22);
            this.cboDataFiltrar.TabIndex = 4;
            this.cboDataFiltrar.EditValueChanged += new System.EventHandler(this.cboDataFiltrar_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(417, 77);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 13);
            this.labelControl3.TabIndex = 10145;
            this.labelControl3.Text = "Data a Filtrar";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(12, 181);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(50, 13);
            this.labelControl4.TabIndex = 10148;
            this.labelControl4.Text = "Categorias";
            // 
            // cboCategorias
            // 
            this.cboCategorias.EnterMoveNextControl = true;
            this.cboCategorias.Location = new System.Drawing.Point(12, 199);
            this.cboCategorias.Name = "cboCategorias";
            this.cboCategorias.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategorias.Properties.Appearance.Options.UseFont = true;
            this.cboCategorias.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCategorias.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Cód. Cadatro", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboCategorias.Properties.DropDownRows = 5;
            this.cboCategorias.Properties.NullText = "";
            this.cboCategorias.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboCategorias.Size = new System.Drawing.Size(373, 22);
            this.cboCategorias.TabIndex = 10149;
            // 
            // FormRelatorioContasPagarReceber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 347);
            this.Controls.Add(this.cboCategorias);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.cboDataFiltrar);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.pnlOrdenacao);
            this.Controls.Add(this.txtDataFinalPeriodo);
            this.Controls.Add(this.cboStatusTitulo);
            this.Controls.Add(this.labDataCadastro);
            this.Controls.Add(this.txtDataInicialPeriodo);
            this.Controls.Add(this.labelControl60);
            this.Controls.Add(this.labelControl84);
            this.Controls.Add(this.txtIdParceiro);
            this.Controls.Add(this.btnPesquisaParceiro);
            this.Controls.Add(this.txtNomeParceiro);
            this.Controls.Add(this.labelControl61);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTituloRelatorio);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRelatorioContasPagarReceber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório Contas Pagar R";
            this.pnlOrdenacao.ResumeLayout(false);
            this.pnlOrdenacao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatusTitulo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdParceiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaParceiro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeParceiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDataFiltrar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategorias.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox pnlOrdenacao;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rdbOrdemListaCodigo;
        private DevExpress.XtraEditors.LookUpEdit cboStatusTitulo;
        private DevExpress.XtraEditors.LabelControl labelControl60;
        private DevExpress.XtraEditors.LabelControl labelControl84;
        private DevExpress.XtraEditors.TextEdit txtIdParceiro;
        private System.Windows.Forms.PictureBox btnPesquisaParceiro;
        private DevExpress.XtraEditors.TextEdit txtNomeParceiro;
        private DevExpress.XtraEditors.LabelControl labelControl61;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl lblTituloRelatorio;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit txtDataFinalPeriodo;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.DateEdit txtDataInicialPeriodo;
        private DevExpress.XtraEditors.LookUpEdit cboDataFiltrar;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit cboCategorias;
    }
}