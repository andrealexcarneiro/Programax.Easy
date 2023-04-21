namespace Programax.Easy.View.Telas.Relatorios
{
    partial class FormRelatorioMovimentacaoCaixa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRelatorioMovimentacaoCaixa));
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gcItens = new DevExpress.XtraGrid.GridControl();
            this.gridControl2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCaixa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataHoraAbertura = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataHoraFechamento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnPesquiarRegistros = new System.Windows.Forms.PictureBox();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataFinalPeriodo = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataInicialPeriodo = new DevExpress.XtraEditors.DateEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.cboDataFiltrar = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.txtIdCaixa = new DevExpress.XtraEditors.TextEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.btnPesquisarCaixa = new System.Windows.Forms.PictureBox();
            this.txtNomeCaixa = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquiarRegistros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDataFiltrar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdCaixa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisarCaixa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeCaixa.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImprimir
            // 
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Image = global::Programax.Easy.View.Properties.Resources.icone_Imprimir;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnImprimir.Location = new System.Drawing.Point(12, 397);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(0);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(110, 40);
            this.btnImprimir.TabIndex = 10141;
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
            this.btnSair.Location = new System.Drawing.Point(690, 397);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 10145;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-96, 384);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(920, 10);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10144;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-79, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(901, 10);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10143;
            this.pictureBox1.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.labelControl1.Location = new System.Drawing.Point(12, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(494, 29);
            this.labelControl1.TabIndex = 10142;
            this.labelControl1.Text = "RELATÓRIO DE MOVIMENTAÇÃO DE CAIXA";
            // 
            // gcItens
            // 
            this.gcItens.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcItens.Location = new System.Drawing.Point(13, 161);
            this.gcItens.MainView = this.gridControl2;
            this.gcItens.Name = "gcItens";
            this.gcItens.Size = new System.Drawing.Size(777, 214);
            this.gcItens.TabIndex = 10160;
            this.gcItens.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridControl2,
            this.gridView3});
            this.gcItens.DoubleClick += new System.EventHandler(this.gcItens_DoubleClick);
            this.gcItens.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcItens_KeyDown);
            // 
            // gridControl2
            // 
            this.gridControl2.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridControl2.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridControl2.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridControl2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridControl2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaId,
            this.colunaCaixa,
            this.colunaStatus,
            this.colunaDataHoraAbertura,
            this.colunaDataHoraFechamento});
            this.gridControl2.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridControl2.GridControl = this.gcItens;
            this.gridControl2.GroupPanelText = "Enderecos";
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.OptionsView.ShowGroupPanel = false;
            this.gridControl2.OptionsView.ShowIndicator = false;
            this.gridControl2.OptionsView.ShowViewCaption = true;
            this.gridControl2.PaintStyleName = "Skin";
            this.gridControl2.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridControl2.ViewCaption = "Movimentação de Caixa";
            // 
            // colunaId
            // 
            this.colunaId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.Caption = "Nr. Registro Caixa";
            this.colunaId.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaId.FieldName = "Id";
            this.colunaId.MinWidth = 45;
            this.colunaId.Name = "colunaId";
            this.colunaId.OptionsColumn.AllowEdit = false;
            this.colunaId.OptionsColumn.AllowFocus = false;
            this.colunaId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colunaId.OptionsFilter.AllowAutoFilter = false;
            this.colunaId.OptionsFilter.AllowFilter = false;
            this.colunaId.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "tele_id", "{0}")});
            this.colunaId.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaId.Visible = true;
            this.colunaId.VisibleIndex = 0;
            this.colunaId.Width = 167;
            // 
            // colunaCaixa
            // 
            this.colunaCaixa.AppearanceCell.Options.UseTextOptions = true;
            this.colunaCaixa.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaCaixa.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaCaixa.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaCaixa.Caption = "Caixa";
            this.colunaCaixa.FieldName = "Caixa";
            this.colunaCaixa.Name = "colunaCaixa";
            this.colunaCaixa.OptionsColumn.AllowEdit = false;
            this.colunaCaixa.OptionsColumn.AllowFocus = false;
            this.colunaCaixa.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colunaCaixa.OptionsFilter.AllowAutoFilter = false;
            this.colunaCaixa.OptionsFilter.AllowFilter = false;
            this.colunaCaixa.Visible = true;
            this.colunaCaixa.VisibleIndex = 1;
            this.colunaCaixa.Width = 135;
            // 
            // colunaStatus
            // 
            this.colunaStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colunaStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaStatus.Caption = "Status";
            this.colunaStatus.FieldName = "Status";
            this.colunaStatus.Name = "colunaStatus";
            this.colunaStatus.OptionsColumn.AllowEdit = false;
            this.colunaStatus.OptionsColumn.AllowFocus = false;
            this.colunaStatus.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colunaStatus.OptionsFilter.AllowAutoFilter = false;
            this.colunaStatus.OptionsFilter.AllowFilter = false;
            this.colunaStatus.Visible = true;
            this.colunaStatus.VisibleIndex = 2;
            this.colunaStatus.Width = 242;
            // 
            // colunaDataHoraAbertura
            // 
            this.colunaDataHoraAbertura.AppearanceCell.Options.UseTextOptions = true;
            this.colunaDataHoraAbertura.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaDataHoraAbertura.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaDataHoraAbertura.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaDataHoraAbertura.Caption = "Data/Hora Abertura";
            this.colunaDataHoraAbertura.FieldName = "DataHoraAbertura";
            this.colunaDataHoraAbertura.Name = "colunaDataHoraAbertura";
            this.colunaDataHoraAbertura.OptionsColumn.AllowEdit = false;
            this.colunaDataHoraAbertura.OptionsColumn.AllowFocus = false;
            this.colunaDataHoraAbertura.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colunaDataHoraAbertura.OptionsFilter.AllowAutoFilter = false;
            this.colunaDataHoraAbertura.OptionsFilter.AllowFilter = false;
            this.colunaDataHoraAbertura.Visible = true;
            this.colunaDataHoraAbertura.VisibleIndex = 3;
            this.colunaDataHoraAbertura.Width = 156;
            // 
            // colunaDataHoraFechamento
            // 
            this.colunaDataHoraFechamento.AppearanceCell.Options.UseTextOptions = true;
            this.colunaDataHoraFechamento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaDataHoraFechamento.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaDataHoraFechamento.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaDataHoraFechamento.Caption = "Data/Hora Fechamento";
            this.colunaDataHoraFechamento.FieldName = "DataHoraFechamento";
            this.colunaDataHoraFechamento.Name = "colunaDataHoraFechamento";
            this.colunaDataHoraFechamento.OptionsColumn.AllowEdit = false;
            this.colunaDataHoraFechamento.OptionsColumn.AllowFocus = false;
            this.colunaDataHoraFechamento.OptionsFilter.AllowFilter = false;
            this.colunaDataHoraFechamento.Visible = true;
            this.colunaDataHoraFechamento.VisibleIndex = 4;
            this.colunaDataHoraFechamento.Width = 189;
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gcItens;
            this.gridView3.Name = "gridView3";
            // 
            // btnPesquiarRegistros
            // 
            this.btnPesquiarRegistros.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquiarRegistros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquiarRegistros.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquiarRegistros.Location = new System.Drawing.Point(768, 133);
            this.btnPesquiarRegistros.Name = "btnPesquiarRegistros";
            this.btnPesquiarRegistros.Size = new System.Drawing.Size(22, 22);
            this.btnPesquiarRegistros.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquiarRegistros.TabIndex = 10159;
            this.btnPesquiarRegistros.TabStop = false;
            this.btnPesquiarRegistros.Click += new System.EventHandler(this.btnPesquiarRegistros_Click);
            // 
            // cboStatus
            // 
            this.cboStatus.Location = new System.Drawing.Point(558, 133);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboStatus.Properties.DropDownRows = 3;
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Size = new System.Drawing.Size(204, 22);
            this.cboStatus.TabIndex = 10157;
            this.cboStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboStatus_KeyDown);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Location = new System.Drawing.Point(558, 117);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(30, 13);
            this.labelControl2.TabIndex = 10158;
            this.labelControl2.Text = "Status";
            // 
            // txtDataFinalPeriodo
            // 
            this.txtDataFinalPeriodo.EditValue = "";
            this.txtDataFinalPeriodo.Enabled = false;
            this.txtDataFinalPeriodo.EnterMoveNextControl = true;
            this.txtDataFinalPeriodo.Location = new System.Drawing.Point(422, 133);
            this.txtDataFinalPeriodo.Name = "txtDataFinalPeriodo";
            this.txtDataFinalPeriodo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinalPeriodo.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinalPeriodo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinalPeriodo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinalPeriodo.Size = new System.Drawing.Size(130, 22);
            this.txtDataFinalPeriodo.TabIndex = 10153;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(422, 118);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(89, 13);
            this.labelControl3.TabIndex = 10156;
            this.labelControl3.Text = "Data Final Período";
            // 
            // txtDataInicialPeriodo
            // 
            this.txtDataInicialPeriodo.EditValue = "";
            this.txtDataInicialPeriodo.Enabled = false;
            this.txtDataInicialPeriodo.EnterMoveNextControl = true;
            this.txtDataInicialPeriodo.Location = new System.Drawing.Point(286, 133);
            this.txtDataInicialPeriodo.Name = "txtDataInicialPeriodo";
            this.txtDataInicialPeriodo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicialPeriodo.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicialPeriodo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicialPeriodo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicialPeriodo.Size = new System.Drawing.Size(130, 22);
            this.txtDataInicialPeriodo.TabIndex = 10152;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(286, 118);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(94, 13);
            this.labelControl6.TabIndex = 10155;
            this.labelControl6.Text = "Data Inicial Período";
            // 
            // cboDataFiltrar
            // 
            this.cboDataFiltrar.EnterMoveNextControl = true;
            this.cboDataFiltrar.Location = new System.Drawing.Point(13, 133);
            this.cboDataFiltrar.Name = "cboDataFiltrar";
            this.cboDataFiltrar.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDataFiltrar.Properties.Appearance.Options.UseFont = true;
            this.cboDataFiltrar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDataFiltrar.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboDataFiltrar.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboDataFiltrar.Properties.DropDownRows = 3;
            this.cboDataFiltrar.Properties.NullText = "";
            this.cboDataFiltrar.Size = new System.Drawing.Size(267, 22);
            this.cboDataFiltrar.TabIndex = 10151;
            this.cboDataFiltrar.EditValueChanged += new System.EventHandler(this.cboDataFiltrar_EditValueChanged);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl7.Location = new System.Drawing.Point(13, 117);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(60, 13);
            this.labelControl7.TabIndex = 10154;
            this.labelControl7.Text = "Data a Filtrar";
            // 
            // labelControl17
            // 
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl17.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl17.Location = new System.Drawing.Point(12, 68);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(43, 13);
            this.labelControl17.TabIndex = 10148;
            this.labelControl17.Text = "Nr. Caixa";
            // 
            // txtIdCaixa
            // 
            this.txtIdCaixa.EnterMoveNextControl = true;
            this.txtIdCaixa.Location = new System.Drawing.Point(12, 84);
            this.txtIdCaixa.Name = "txtIdCaixa";
            this.txtIdCaixa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdCaixa.Properties.Appearance.Options.UseFont = true;
            this.txtIdCaixa.Properties.Appearance.Options.UseTextOptions = true;
            this.txtIdCaixa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtIdCaixa.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtIdCaixa.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtIdCaixa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIdCaixa.Properties.MaxLength = 10;
            this.txtIdCaixa.Size = new System.Drawing.Size(99, 22);
            this.txtIdCaixa.TabIndex = 10146;
            this.txtIdCaixa.Leave += new System.EventHandler(this.txtIdCaixa_Leave);
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl18.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl18.Location = new System.Drawing.Point(145, 68);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(72, 13);
            this.labelControl18.TabIndex = 10150;
            this.labelControl18.Text = "Nome do Caixa";
            // 
            // btnPesquisarCaixa
            // 
            this.btnPesquisarCaixa.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisarCaixa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisarCaixa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisarCaixa.Location = new System.Drawing.Point(117, 83);
            this.btnPesquisarCaixa.Name = "btnPesquisarCaixa";
            this.btnPesquisarCaixa.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisarCaixa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisarCaixa.TabIndex = 10149;
            this.btnPesquisarCaixa.TabStop = false;
            this.btnPesquisarCaixa.Click += new System.EventHandler(this.btnPesquisarCaixa_Click);
            // 
            // txtNomeCaixa
            // 
            this.txtNomeCaixa.EnterMoveNextControl = true;
            this.txtNomeCaixa.Location = new System.Drawing.Point(145, 84);
            this.txtNomeCaixa.Name = "txtNomeCaixa";
            this.txtNomeCaixa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeCaixa.Properties.Appearance.Options.UseFont = true;
            this.txtNomeCaixa.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNomeCaixa.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNomeCaixa.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeCaixa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNomeCaixa.Properties.Mask.EditMask = "99/";
            this.txtNomeCaixa.Properties.ReadOnly = true;
            this.txtNomeCaixa.Size = new System.Drawing.Size(645, 22);
            this.txtNomeCaixa.TabIndex = 10147;
            this.txtNomeCaixa.TabStop = false;
            // 
            // FormRelatorioMovimentacaoCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 443);
            this.Controls.Add(this.gcItens);
            this.Controls.Add(this.btnPesquiarRegistros);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtDataFinalPeriodo);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtDataInicialPeriodo);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.cboDataFiltrar);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl17);
            this.Controls.Add(this.txtIdCaixa);
            this.Controls.Add(this.labelControl18);
            this.Controls.Add(this.btnPesquisarCaixa);
            this.Controls.Add(this.txtNomeCaixa);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRelatorioMovimentacaoCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório Movimentação Caixa";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquiarRegistros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDataFiltrar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdCaixa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisarCaixa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeCaixa.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gcItens;
        private DevExpress.XtraGrid.Views.Grid.GridView gridControl2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCaixa;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataHoraAbertura;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataHoraFechamento;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.PictureBox btnPesquiarRegistros;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit txtDataFinalPeriodo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit txtDataInicialPeriodo;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LookUpEdit cboDataFiltrar;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.TextEdit txtIdCaixa;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private System.Windows.Forms.PictureBox btnPesquisarCaixa;
        private DevExpress.XtraEditors.TextEdit txtNomeCaixa;
    }
}