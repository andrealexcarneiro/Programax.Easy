namespace Programax.Easy.View.Telas.Cadastros.Inventarios
{
    partial class FormInventarioPesquisa
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
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcContasBancarias = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColunaDataInicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaContagem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaMarca = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCategoria = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaGrupo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaSubGrupo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.btnPesquisaContaBancaria = new System.Windows.Forms.PictureBox();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.cboSubGrupos = new DevExpress.XtraEditors.LookUpEdit();
            this.cboMarcas = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.cboCategorias = new DevExpress.XtraEditors.LookUpEdit();
            this.labTipoInscricao = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.cboGrupos = new DevExpress.XtraEditors.LookUpEdit();
            this.txtDataInicio = new DevExpress.XtraEditors.DateEdit();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.cboNumeroContagem = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcContasBancarias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaContaBancaria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubGrupos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMarcas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategorias.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGrupos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicio.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNumeroContagem.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Size = new System.Drawing.Size(1197, 40);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.labDataCadastro);
            this.panelConteudo.Controls.Add(this.cboNumeroContagem);
            this.panelConteudo.Controls.Add(this.txtDataInicio);
            this.panelConteudo.Controls.Add(this.btnPesquisaContaBancaria);
            this.panelConteudo.Controls.Add(this.cboCategorias);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.labTipoInscricao);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Controls.Add(this.labelControl8);
            this.panelConteudo.Controls.Add(this.labelControl16);
            this.panelConteudo.Controls.Add(this.gcContasBancarias);
            this.panelConteudo.Controls.Add(this.cboMarcas);
            this.panelConteudo.Controls.Add(this.labelControl9);
            this.panelConteudo.Controls.Add(this.cboGrupos);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.cboSubGrupos);
            this.panelConteudo.Size = new System.Drawing.Size(900, 313);
            // 
            // colunaStatus
            // 
            this.colunaStatus.Caption = "Status";
            this.colunaStatus.FieldName = "Status";
            this.colunaStatus.Name = "colunaStatus";
            this.colunaStatus.OptionsColumn.AllowEdit = false;
            this.colunaStatus.OptionsColumn.AllowFocus = false;
            this.colunaStatus.OptionsFilter.AllowFilter = false;
            this.colunaStatus.Visible = true;
            this.colunaStatus.VisibleIndex = 1;
            this.colunaStatus.Width = 57;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcContasBancarias;
            this.gridView2.Name = "gridView2";
            // 
            // gcContasBancarias
            // 
            this.gcContasBancarias.Location = new System.Drawing.Point(3, 96);
            this.gcContasBancarias.MainView = this.gridView5;
            this.gcContasBancarias.Name = "gcContasBancarias";
            this.gcContasBancarias.Size = new System.Drawing.Size(893, 213);
            this.gcContasBancarias.TabIndex = 8;
            this.gcContasBancarias.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcContasBancarias.DoubleClick += new System.EventHandler(this.gcNcms_DoubleClick);
            this.gcContasBancarias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcNcms_KeyDown);
            // 
            // gridView5
            // 
            this.gridView5.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView5.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaId,
            this.colunaStatus,
            this.ColunaDataInicio,
            this.colunaContagem,
            this.colunaMarca,
            this.colunaCategoria,
            this.colunaGrupo,
            this.colunaSubGrupo});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcContasBancarias;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Inventários";
            // 
            // colunaId
            // 
            this.colunaId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.Caption = "Id";
            this.colunaId.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaId.FieldName = "Id";
            this.colunaId.MinWidth = 45;
            this.colunaId.Name = "colunaId";
            this.colunaId.OptionsColumn.AllowEdit = false;
            this.colunaId.OptionsColumn.AllowFocus = false;
            this.colunaId.OptionsFilter.AllowFilter = false;
            this.colunaId.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colunaId.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaId.Visible = true;
            this.colunaId.VisibleIndex = 0;
            this.colunaId.Width = 45;
            // 
            // ColunaDataInicio
            // 
            this.ColunaDataInicio.Caption = "Data Início";
            this.ColunaDataInicio.FieldName = "DataInicio";
            this.ColunaDataInicio.Name = "ColunaDataInicio";
            this.ColunaDataInicio.OptionsColumn.AllowEdit = false;
            this.ColunaDataInicio.OptionsColumn.AllowFocus = false;
            this.ColunaDataInicio.OptionsFilter.AllowFilter = false;
            this.ColunaDataInicio.Visible = true;
            this.ColunaDataInicio.VisibleIndex = 2;
            this.ColunaDataInicio.Width = 45;
            // 
            // colunaContagem
            // 
            this.colunaContagem.Caption = "Contagem";
            this.colunaContagem.FieldName = "Contagem";
            this.colunaContagem.Name = "colunaContagem";
            this.colunaContagem.OptionsColumn.AllowEdit = false;
            this.colunaContagem.OptionsColumn.AllowFocus = false;
            this.colunaContagem.OptionsFilter.AllowFilter = false;
            this.colunaContagem.Visible = true;
            this.colunaContagem.VisibleIndex = 3;
            this.colunaContagem.Width = 44;
            // 
            // colunaMarca
            // 
            this.colunaMarca.Caption = "Marca";
            this.colunaMarca.FieldName = "Marca";
            this.colunaMarca.Name = "colunaMarca";
            this.colunaMarca.OptionsColumn.AllowEdit = false;
            this.colunaMarca.OptionsColumn.AllowFocus = false;
            this.colunaMarca.OptionsFilter.AllowFilter = false;
            this.colunaMarca.Visible = true;
            this.colunaMarca.VisibleIndex = 4;
            this.colunaMarca.Width = 76;
            // 
            // colunaCategoria
            // 
            this.colunaCategoria.Caption = "Categoria";
            this.colunaCategoria.FieldName = "Categoria";
            this.colunaCategoria.Name = "colunaCategoria";
            this.colunaCategoria.OptionsColumn.AllowEdit = false;
            this.colunaCategoria.OptionsColumn.AllowFocus = false;
            this.colunaCategoria.OptionsFilter.AllowFilter = false;
            this.colunaCategoria.Visible = true;
            this.colunaCategoria.VisibleIndex = 5;
            this.colunaCategoria.Width = 76;
            // 
            // colunaGrupo
            // 
            this.colunaGrupo.Caption = "Grupo";
            this.colunaGrupo.FieldName = "Grupo";
            this.colunaGrupo.Name = "colunaGrupo";
            this.colunaGrupo.OptionsColumn.AllowEdit = false;
            this.colunaGrupo.OptionsColumn.AllowFocus = false;
            this.colunaGrupo.OptionsFilter.AllowFilter = false;
            this.colunaGrupo.Visible = true;
            this.colunaGrupo.VisibleIndex = 6;
            this.colunaGrupo.Width = 76;
            // 
            // colunaSubGrupo
            // 
            this.colunaSubGrupo.Caption = "Sub Grupo";
            this.colunaSubGrupo.FieldName = "SubGrupo";
            this.colunaSubGrupo.Name = "colunaSubGrupo";
            this.colunaSubGrupo.OptionsColumn.AllowEdit = false;
            this.colunaSubGrupo.OptionsColumn.AllowFocus = false;
            this.colunaSubGrupo.OptionsFilter.AllowFilter = false;
            this.colunaSubGrupo.Visible = true;
            this.colunaSubGrupo.VisibleIndex = 7;
            this.colunaSubGrupo.Width = 95;
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(217, 16);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboStatus.Properties.DropDownRows = 3;
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboStatus.Size = new System.Drawing.Size(209, 22);
            this.cboStatus.TabIndex = 2;
            this.cboStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPesquiseEnter_KeyDown);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSelecionar);
            this.flowLayoutPanel1.Controls.Add(this.btnFechar);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(522, 46);
            this.flowLayoutPanel1.TabIndex = 1049;
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelecionar.FlatAppearance.BorderSize = 0;
            this.btnSelecionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionar.Image = global::Programax.Easy.View.Properties.Resources.icone_selecionar1;
            this.btnSelecionar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSelecionar.Location = new System.Drawing.Point(0, 0);
            this.btnSelecionar.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(120, 40);
            this.btnSelecionar.TabIndex = 1047;
            this.btnSelecionar.Text = " Selecionar";
            this.btnSelecionar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnFechar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnFechar.Location = new System.Drawing.Point(120, 0);
            this.btnFechar.Margin = new System.Windows.Forms.Padding(0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(100, 40);
            this.btnFechar.TabIndex = 1048;
            this.btnFechar.Text = " Sair";
            this.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(217, 1);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 643;
            this.labStatus.Text = "Status";
            // 
            // btnPesquisaContaBancaria
            // 
            this.btnPesquisaContaBancaria.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaContaBancaria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaContaBancaria.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaContaBancaria.Location = new System.Drawing.Point(874, 65);
            this.btnPesquisaContaBancaria.Name = "btnPesquisaContaBancaria";
            this.btnPesquisaContaBancaria.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaContaBancaria.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaContaBancaria.TabIndex = 640;
            this.btnPesquisaContaBancaria.TabStop = false;
            this.btnPesquisaContaBancaria.Click += new System.EventHandler(this.btnPesquisaNcm_Click);
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Location = new System.Drawing.Point(648, 50);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(46, 13);
            this.labelControl9.TabIndex = 754;
            this.labelControl9.Text = "Subgrupo";
            // 
            // cboSubGrupos
            // 
            this.cboSubGrupos.Location = new System.Drawing.Point(648, 65);
            this.cboSubGrupos.Name = "cboSubGrupos";
            this.cboSubGrupos.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSubGrupos.Properties.Appearance.Options.UseFont = true;
            this.cboSubGrupos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSubGrupos.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", 5, "Id"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboSubGrupos.Properties.DropDownRows = 5;
            this.cboSubGrupos.Properties.NullText = "";
            this.cboSubGrupos.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboSubGrupos.Size = new System.Drawing.Size(220, 22);
            this.cboSubGrupos.TabIndex = 7;
            this.cboSubGrupos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPesquiseEnter_KeyDown);
            // 
            // cboMarcas
            // 
            this.cboMarcas.EnterMoveNextControl = true;
            this.cboMarcas.Location = new System.Drawing.Point(3, 65);
            this.cboMarcas.Name = "cboMarcas";
            this.cboMarcas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMarcas.Properties.Appearance.Options.UseFont = true;
            this.cboMarcas.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMarcas.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", 5, "Id"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboMarcas.Properties.DropDownRows = 5;
            this.cboMarcas.Properties.NullText = "";
            this.cboMarcas.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboMarcas.Size = new System.Drawing.Size(208, 22);
            this.cboMarcas.TabIndex = 4;
            this.cboMarcas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPesquiseEnter_KeyDown);
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl8.Location = new System.Drawing.Point(4, 50);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(30, 13);
            this.labelControl8.TabIndex = 753;
            this.labelControl8.Text = "Marca";
            // 
            // cboCategorias
            // 
            this.cboCategorias.EnterMoveNextControl = true;
            this.cboCategorias.Location = new System.Drawing.Point(217, 65);
            this.cboCategorias.Name = "cboCategorias";
            this.cboCategorias.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategorias.Properties.Appearance.Options.UseFont = true;
            this.cboCategorias.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCategorias.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", 5, "Id"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboCategorias.Properties.DropDownRows = 5;
            this.cboCategorias.Properties.NullText = "";
            this.cboCategorias.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboCategorias.Size = new System.Drawing.Size(209, 22);
            this.cboCategorias.TabIndex = 5;
            this.cboCategorias.EditValueChanged += new System.EventHandler(this.cboCategorias_EditValueChanged);
            this.cboCategorias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPesquiseEnter_KeyDown);
            // 
            // labTipoInscricao
            // 
            this.labTipoInscricao.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTipoInscricao.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labTipoInscricao.Location = new System.Drawing.Point(217, 50);
            this.labTipoInscricao.Name = "labTipoInscricao";
            this.labTipoInscricao.Size = new System.Drawing.Size(45, 13);
            this.labTipoInscricao.TabIndex = 751;
            this.labTipoInscricao.Text = "Categoria";
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl16.Location = new System.Drawing.Point(432, 50);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(29, 13);
            this.labelControl16.TabIndex = 752;
            this.labelControl16.Text = "Grupo";
            // 
            // cboGrupos
            // 
            this.cboGrupos.EnterMoveNextControl = true;
            this.cboGrupos.Location = new System.Drawing.Point(432, 65);
            this.cboGrupos.Name = "cboGrupos";
            this.cboGrupos.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGrupos.Properties.Appearance.Options.UseFont = true;
            this.cboGrupos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboGrupos.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", 5, "Id"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboGrupos.Properties.DropDownRows = 5;
            this.cboGrupos.Properties.NullText = "";
            this.cboGrupos.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboGrupos.Size = new System.Drawing.Size(210, 22);
            this.cboGrupos.TabIndex = 6;
            this.cboGrupos.EditValueChanged += new System.EventHandler(this.cboGrupos_EditValueChanged);
            this.cboGrupos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPesquiseEnter_KeyDown);
            // 
            // txtDataInicio
            // 
            this.txtDataInicio.EditValue = new System.DateTime(2014, 12, 31, 0, 0, 0, 0);
            this.txtDataInicio.EnterMoveNextControl = true;
            this.txtDataInicio.Location = new System.Drawing.Point(3, 16);
            this.txtDataInicio.Name = "txtDataInicio";
            this.txtDataInicio.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicio.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicio.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicio.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicio.Size = new System.Drawing.Size(208, 22);
            this.txtDataInicio.TabIndex = 1;
            this.txtDataInicio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPesquiseEnter_KeyDown);
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Location = new System.Drawing.Point(3, 1);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(53, 13);
            this.labDataCadastro.TabIndex = 1072;
            this.labDataCadastro.Text = "Data Início";
            // 
            // cboNumeroContagem
            // 
            this.cboNumeroContagem.EnterMoveNextControl = true;
            this.cboNumeroContagem.Location = new System.Drawing.Point(432, 16);
            this.cboNumeroContagem.Name = "cboNumeroContagem";
            this.cboNumeroContagem.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNumeroContagem.Properties.Appearance.Options.UseFont = true;
            this.cboNumeroContagem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNumeroContagem.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboNumeroContagem.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboNumeroContagem.Properties.DropDownRows = 3;
            this.cboNumeroContagem.Properties.NullText = "";
            this.cboNumeroContagem.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboNumeroContagem.Size = new System.Drawing.Size(210, 22);
            this.cboNumeroContagem.TabIndex = 3;
            this.cboNumeroContagem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPesquiseEnter_KeyDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(432, 1);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 13);
            this.labelControl1.TabIndex = 1074;
            this.labelControl1.Text = "Contagem";
            // 
            // FormInventarioPesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 424);
            this.MaximizeBox = false;
            this.Name = "FormInventarioPesquisa";
            this.NomeDaTela = "Pesquisa de Inventarios";
            this.Text = "Pesquisa de Inventarios";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcContasBancarias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaContaBancaria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubGrupos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMarcas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategorias.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGrupos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicio.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNumeroContagem.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gcContasBancarias;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private System.Windows.Forms.PictureBox btnPesquisaContaBancaria;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnSelecionar;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LookUpEdit cboSubGrupos;
        private DevExpress.XtraEditors.LookUpEdit cboMarcas;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LookUpEdit cboCategorias;
        private DevExpress.XtraEditors.LabelControl labTipoInscricao;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LookUpEdit cboGrupos;
        private DevExpress.XtraEditors.DateEdit txtDataInicio;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.LookUpEdit cboNumeroContagem;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn ColunaDataInicio;
        private DevExpress.XtraGrid.Columns.GridColumn colunaContagem;
        private DevExpress.XtraGrid.Columns.GridColumn colunaMarca;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCategoria;
        private DevExpress.XtraGrid.Columns.GridColumn colunaGrupo;
        private DevExpress.XtraGrid.Columns.GridColumn colunaSubGrupo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}