namespace Programax.Easy.View.Telas.TeleMarketing
{
    partial class FormGerenciarAtendimentos
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaVendedo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaTotalVenda = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAtendimentos = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaVendedor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaData = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDuracao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaValorVenda = new DevExpress.XtraGrid.Columns.GridColumn();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.cboStatusAtendimento = new DevExpress.XtraEditors.LookUpEdit();
            this.txtDataFinal = new DevExpress.XtraEditors.DateEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataInicial = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.cboParceiro = new DevExpress.XtraEditors.LookUpEdit();
            this.pbPesquisa = new System.Windows.Forms.PictureBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.chtFluxoCaixa = new DevExpress.XtraCharts.ChartControl();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAtendimentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatusAtendimento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboParceiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chtFluxoCaixa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel2);
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Size = new System.Drawing.Size(1184, 49);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.chtFluxoCaixa);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.pbPesquisa);
            this.panelConteudo.Controls.Add(this.cboParceiro);
            this.panelConteudo.Controls.Add(this.txtDataFinal);
            this.panelConteudo.Controls.Add(this.labelControl10);
            this.panelConteudo.Controls.Add(this.txtDataInicial);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.cboStatusAtendimento);
            this.panelConteudo.Controls.Add(this.gcAtendimentos);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Margin = new System.Windows.Forms.Padding(5);
            this.panelConteudo.Size = new System.Drawing.Size(1167, 707);
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaVendedo,
            this.colunaTotalVenda});
            this.gridView2.DetailHeight = 431;
            this.gridView2.GridControl = this.gcAtendimentos;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsPrint.ExpandAllDetails = true;
            this.gridView2.OptionsPrint.PrintDetails = true;
            this.gridView2.OptionsPrint.PrintPreview = true;
            this.gridView2.ViewCaption = "Vendas por Vendedores";
            // 
            // colunaVendedo
            // 
            this.colunaVendedo.Caption = "Vendedor";
            this.colunaVendedo.FieldName = "Vendedor";
            this.colunaVendedo.MinWidth = 25;
            this.colunaVendedo.Name = "colunaVendedo";
            this.colunaVendedo.Visible = true;
            this.colunaVendedo.VisibleIndex = 0;
            this.colunaVendedo.Width = 94;
            // 
            // colunaTotalVenda
            // 
            this.colunaTotalVenda.Caption = "Total Venda";
            this.colunaTotalVenda.FieldName = "TotalVenda";
            this.colunaTotalVenda.MinWidth = 25;
            this.colunaTotalVenda.Name = "colunaTotalVenda";
            this.colunaTotalVenda.OptionsColumn.AllowEdit = false;
            this.colunaTotalVenda.OptionsColumn.AllowFocus = false;
            this.colunaTotalVenda.OptionsFilter.AllowFilter = false;
            this.colunaTotalVenda.Visible = true;
            this.colunaTotalVenda.VisibleIndex = 1;
            this.colunaTotalVenda.Width = 94;
            // 
            // gcAtendimentos
            // 
            this.gcAtendimentos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gcAtendimentos.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            gridLevelNode1.LevelTemplate = this.gridView2;
            gridLevelNode1.RelationName = "Level1";
            this.gcAtendimentos.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcAtendimentos.Location = new System.Drawing.Point(5, 66);
            this.gcAtendimentos.MainView = this.gridView5;
            this.gcAtendimentos.Margin = new System.Windows.Forms.Padding(4);
            this.gcAtendimentos.Name = "gcAtendimentos";
            this.gcAtendimentos.Size = new System.Drawing.Size(1155, 340);
            this.gcAtendimentos.TabIndex = 10;
            this.gcAtendimentos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            // 
            // gridView5
            // 
            this.gridView5.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridView5.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gridView5.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView5.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridView5.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView5.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaId,
            this.colunaStatus,
            this.colunaVendedor,
            this.colunaData,
            this.colunaDuracao,
            this.colunaValorVenda});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 288, 219);
            this.gridView5.DetailHeight = 431;
            this.gridView5.GridControl = this.gcAtendimentos;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsPrint.ExpandAllDetails = true;
            this.gridView5.OptionsPrint.PrintDetails = true;
            this.gridView5.OptionsPrint.PrintPreview = true;
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.ViewCaption = "Atendimentos";
            // 
            // colunaId
            // 
            this.colunaId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaId.Caption = "Id";
            this.colunaId.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaId.FieldName = "Id";
            this.colunaId.MinWidth = 60;
            this.colunaId.Name = "colunaId";
            this.colunaId.OptionsColumn.AllowEdit = false;
            this.colunaId.OptionsColumn.AllowFocus = false;
            this.colunaId.OptionsFilter.AllowFilter = false;
            this.colunaId.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colunaId.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaId.Width = 104;
            // 
            // colunaStatus
            // 
            this.colunaStatus.Caption = "Status";
            this.colunaStatus.FieldName = "Status";
            this.colunaStatus.MinWidth = 25;
            this.colunaStatus.Name = "colunaStatus";
            this.colunaStatus.OptionsColumn.AllowEdit = false;
            this.colunaStatus.OptionsColumn.AllowFocus = false;
            this.colunaStatus.OptionsFilter.AllowFilter = false;
            this.colunaStatus.Visible = true;
            this.colunaStatus.VisibleIndex = 0;
            this.colunaStatus.Width = 145;
            // 
            // colunaVendedor
            // 
            this.colunaVendedor.Caption = "Vendedor";
            this.colunaVendedor.FieldName = "Vendedor";
            this.colunaVendedor.MinWidth = 27;
            this.colunaVendedor.Name = "colunaVendedor";
            this.colunaVendedor.OptionsColumn.AllowEdit = false;
            this.colunaVendedor.OptionsColumn.AllowFocus = false;
            this.colunaVendedor.OptionsFilter.AllowFilter = false;
            this.colunaVendedor.Visible = true;
            this.colunaVendedor.VisibleIndex = 1;
            this.colunaVendedor.Width = 597;
            // 
            // colunaData
            // 
            this.colunaData.Caption = "Data";
            this.colunaData.FieldName = "Data";
            this.colunaData.MinWidth = 27;
            this.colunaData.Name = "colunaData";
            this.colunaData.OptionsColumn.AllowEdit = false;
            this.colunaData.OptionsColumn.AllowFocus = false;
            this.colunaData.OptionsFilter.AllowFilter = false;
            this.colunaData.Visible = true;
            this.colunaData.VisibleIndex = 2;
            this.colunaData.Width = 150;
            // 
            // colunaDuracao
            // 
            this.colunaDuracao.Caption = "Duração";
            this.colunaDuracao.FieldName = "Duracao";
            this.colunaDuracao.MinWidth = 25;
            this.colunaDuracao.Name = "colunaDuracao";
            this.colunaDuracao.OptionsColumn.AllowEdit = false;
            this.colunaDuracao.OptionsColumn.AllowFocus = false;
            this.colunaDuracao.OptionsFilter.AllowFilter = false;
            this.colunaDuracao.Visible = true;
            this.colunaDuracao.VisibleIndex = 3;
            this.colunaDuracao.Width = 126;
            // 
            // colunaValorVenda
            // 
            this.colunaValorVenda.Caption = "Valor Venda";
            this.colunaValorVenda.FieldName = "ValorVenda";
            this.colunaValorVenda.MinWidth = 25;
            this.colunaValorVenda.Name = "colunaValorVenda";
            this.colunaValorVenda.OptionsColumn.AllowEdit = false;
            this.colunaValorVenda.OptionsColumn.AllowFocus = false;
            this.colunaValorVenda.OptionsFilter.AllowFilter = false;
            this.colunaValorVenda.Visible = true;
            this.colunaValorVenda.VisibleIndex = 4;
            this.colunaValorVenda.Width = 135;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnImprimir);
            this.flowLayoutPanel1.Controls.Add(this.btnFechar);
            this.flowLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(725, 59);
            this.flowLayoutPanel1.TabIndex = 1050;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Image = global::Programax.Easy.View.Properties.Resources.icone_selecionar1;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnImprimir.Location = new System.Drawing.Point(0, 0);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(0);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(160, 49);
            this.btnImprimir.TabIndex = 1047;
            this.btnImprimir.Text = " Imprimir";
            this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click_1);
            // 
            // btnFechar
            // 
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnFechar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnFechar.Location = new System.Drawing.Point(160, 0);
            this.btnFechar.Margin = new System.Windows.Forms.Padding(0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(133, 49);
            this.btnFechar.TabIndex = 1048;
            this.btnFechar.Text = " Sair";
            this.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Appearance.Options.UseFont = true;
            this.labStatus.Location = new System.Drawing.Point(333, 12);
            this.labStatus.Margin = new System.Windows.Forms.Padding(4);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(40, 17);
            this.labStatus.TabIndex = 652;
            this.labStatus.Text = "Status";
            // 
            // cboStatusAtendimento
            // 
            this.cboStatusAtendimento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboStatusAtendimento.Location = new System.Drawing.Point(333, 32);
            this.cboStatusAtendimento.Margin = new System.Windows.Forms.Padding(4);
            this.cboStatusAtendimento.Name = "cboStatusAtendimento";
            this.cboStatusAtendimento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatusAtendimento.Properties.Appearance.Options.UseFont = true;
            this.cboStatusAtendimento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatusAtendimento.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatusAtendimento.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboStatusAtendimento.Properties.DropDownRows = 8;
            this.cboStatusAtendimento.Properties.NullText = "";
            this.cboStatusAtendimento.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboStatusAtendimento.Size = new System.Drawing.Size(231, 26);
            this.cboStatusAtendimento.TabIndex = 9;
            this.cboStatusAtendimento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboSituacao_KeyDown);
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtDataFinal.EditValue = "";
            this.txtDataFinal.EnterMoveNextControl = true;
            this.txtDataFinal.Location = new System.Drawing.Point(169, 32);
            this.txtDataFinal.Margin = new System.Windows.Forms.Padding(4);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinal.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinal.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinal.Size = new System.Drawing.Size(156, 26);
            this.txtDataFinal.TabIndex = 2;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(169, 12);
            this.labelControl10.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(64, 17);
            this.labelControl10.TabIndex = 10024;
            this.labelControl10.Text = "Data Final";
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtDataInicial.EditValue = "";
            this.txtDataInicial.EnterMoveNextControl = true;
            this.txtDataInicial.Location = new System.Drawing.Point(5, 32);
            this.txtDataInicial.Margin = new System.Windows.Forms.Padding(4);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicial.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicial.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicial.Size = new System.Drawing.Size(156, 26);
            this.txtDataInicial.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(5, 9);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(69, 17);
            this.labelControl1.TabIndex = 10023;
            this.labelControl1.Text = "Data Inicial";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(596, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(586, 50);
            this.flowLayoutPanel2.TabIndex = 10041;
            // 
            // cboParceiro
            // 
            this.cboParceiro.Location = new System.Drawing.Point(572, 32);
            this.cboParceiro.Margin = new System.Windows.Forms.Padding(4);
            this.cboParceiro.Name = "cboParceiro";
            this.cboParceiro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboParceiro.Properties.Appearance.Options.UseFont = true;
            this.cboParceiro.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboParceiro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboParceiro.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Valor", "Id", 8, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Razão Social", 40, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.cboParceiro.Properties.NullText = "";
            this.cboParceiro.Properties.PopupSizeable = false;
            this.cboParceiro.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cboParceiro.Size = new System.Drawing.Size(557, 26);
            this.cboParceiro.TabIndex = 10025;
            this.cboParceiro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboParceiro_KeyDown);
            // 
            // pbPesquisa
            // 
            this.pbPesquisa.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisa.Location = new System.Drawing.Point(1137, 32);
            this.pbPesquisa.Margin = new System.Windows.Forms.Padding(4);
            this.pbPesquisa.Name = "pbPesquisa";
            this.pbPesquisa.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisa.TabIndex = 10087;
            this.pbPesquisa.TabStop = false;
            this.pbPesquisa.Click += new System.EventHandler(this.pbPesquisa_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(572, 12);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(62, 17);
            this.labelControl2.TabIndex = 10088;
            this.labelControl2.Text = "Vendedor";
            // 
            // chtFluxoCaixa
            // 
            this.chtFluxoCaixa.BorderOptions.Color = System.Drawing.Color.White;
            this.chtFluxoCaixa.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chtFluxoCaixa.Diagram = xyDiagram1;
            this.chtFluxoCaixa.Legend.Name = "Default Legend";
            this.chtFluxoCaixa.Location = new System.Drawing.Point(335, 410);
            this.chtFluxoCaixa.Margin = new System.Windows.Forms.Padding(4);
            this.chtFluxoCaixa.Name = "chtFluxoCaixa";
            series1.ArgumentDataMember = "DataRealizado";
            series1.Name = "Finalizado";
            series1.ValueDataMembersSerializable = "Finalizado";
            series2.ArgumentDataMember = "DataRealizado";
            series2.Name = "Inconcluso";
            series2.ValueDataMembersSerializable = "Inconcluso";
            this.chtFluxoCaixa.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
            this.chtFluxoCaixa.Size = new System.Drawing.Size(455, 250);
            this.chtFluxoCaixa.TabIndex = 10089;
            chartTitle1.Font = new System.Drawing.Font("Tahoma", 9F);
            chartTitle1.Text = "Finalizado x Inconcluso";
            this.chtFluxoCaixa.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // FormGerenciarAtendimentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 842);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "FormGerenciarAtendimentos";
            this.NomeDaTela = "Busca Atendimentos";
            this.Text = "Busca Atendimentos";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAtendimentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboStatusAtendimento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboParceiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chtFluxoCaixa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnFechar;
        private DevExpress.XtraGrid.GridControl gcAtendimentos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaData;
        private DevExpress.XtraGrid.Columns.GridColumn colunaVendedor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraEditors.LookUpEdit cboStatusAtendimento;
        private DevExpress.XtraEditors.DateEdit txtDataFinal;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.DateEdit txtDataInicial;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraEditors.LookUpEdit cboParceiro;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.PictureBox pbPesquisa;
        private DevExpress.XtraCharts.ChartControl chtFluxoCaixa;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDuracao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaValorVenda;
        private DevExpress.XtraGrid.Columns.GridColumn colunaVendedo;
        private DevExpress.XtraGrid.Columns.GridColumn colunaTotalVenda;
    }
}