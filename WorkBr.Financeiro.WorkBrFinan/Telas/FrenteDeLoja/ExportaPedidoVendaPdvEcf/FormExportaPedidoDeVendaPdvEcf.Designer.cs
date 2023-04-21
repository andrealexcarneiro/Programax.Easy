namespace Programax.Easy.View.Telas.FrenteDeLoja.ExportaPedidoVendaPdvEcf
{
    partial class FormExportaPedidoDeVendaPdvEcf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExportaPedidoDeVendaPdvEcf));
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExportarVendas = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.gcVendas = new DevExpress.XtraGrid.GridControl();
            this.gridControl2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaDataElaboracao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaClienteId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCpfCnpj = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaParceiro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaValorTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaVendedor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatusDocumento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaVendaJaExportado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnPesquisaVendas = new System.Windows.Forms.PictureBox();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cboVendaJahExportada = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataFinal = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataInicial = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtPedidoDeVendaId = new DevExpress.XtraEditors.TextEdit();
            this.labBanco = new DevExpress.XtraEditors.LabelControl();
            this.cboTipoPesquisa = new DevExpress.XtraEditors.LookUpEdit();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcVendas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaVendas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVendaJahExportada.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPedidoDeVendaId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoPesquisa.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel2);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.labBanco);
            this.panelConteudo.Controls.Add(this.cboTipoPesquisa);
            this.panelConteudo.Controls.Add(this.btnPesquisaVendas);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.labelControl5);
            this.panelConteudo.Controls.Add(this.cboVendaJahExportada);
            this.panelConteudo.Controls.Add(this.labelControl4);
            this.panelConteudo.Controls.Add(this.txtDataFinal);
            this.panelConteudo.Controls.Add(this.labelControl3);
            this.panelConteudo.Controls.Add(this.txtDataInicial);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.txtPedidoDeVendaId);
            this.panelConteudo.Controls.Add(this.gcVendas);
            this.panelConteudo.Size = new System.Drawing.Size(854, 389);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnExportarVendas);
            this.flowLayoutPanel2.Controls.Add(this.btnSair);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 1);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(798, 39);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // btnExportarVendas
            // 
            this.btnExportarVendas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarVendas.FlatAppearance.BorderSize = 0;
            this.btnExportarVendas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarVendas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarVendas.Image = global::Programax.Easy.View.Properties.Resources.icone_selecionar1;
            this.btnExportarVendas.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExportarVendas.Location = new System.Drawing.Point(0, 0);
            this.btnExportarVendas.Margin = new System.Windows.Forms.Padding(0);
            this.btnExportarVendas.Name = "btnExportarVendas";
            this.btnExportarVendas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnExportarVendas.Size = new System.Drawing.Size(159, 40);
            this.btnExportarVendas.TabIndex = 10036;
            this.btnExportarVendas.Text = " Exportar Venda";
            this.btnExportarVendas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportarVendas.UseVisualStyleBackColor = true;
            this.btnExportarVendas.Visible = false;
            this.btnExportarVendas.Click += new System.EventHandler(this.btnExportarVendas_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(159, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 10037;
            this.btnSair.Text = " Sair";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // gcVendas
            // 
            this.gcVendas.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcVendas.Location = new System.Drawing.Point(0, 49);
            this.gcVendas.MainView = this.gridControl2;
            this.gcVendas.Name = "gcVendas";
            this.gcVendas.Size = new System.Drawing.Size(854, 337);
            this.gcVendas.TabIndex = 7;
            this.gcVendas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridControl2,
            this.gridView3});
            this.gcVendas.Click += new System.EventHandler(this.gcVendas_Click);
            this.gcVendas.DoubleClick += new System.EventHandler(this.gcVendas_DoubleClick);
            this.gcVendas.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gcVendas_KeyUp);
            // 
            // gridControl2
            // 
            this.gridControl2.Appearance.FocusedCell.BackColor = System.Drawing.Color.Transparent;
            this.gridControl2.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.Transparent;
            this.gridControl2.Appearance.FocusedCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridControl2.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridControl2.Appearance.FocusedRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridControl2.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gridControl2.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridControl2.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridControl2.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridControl2.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridControl2.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridControl2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridControl2.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridControl2.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridControl2.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.gridControl2.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridControl2.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridControl2.Appearance.SelectedRow.BackColor = System.Drawing.Color.Transparent;
            this.gridControl2.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.Transparent;
            this.gridControl2.Appearance.SelectedRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridControl2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaDataElaboracao,
            this.colunaId,
            this.colunaClienteId,
            this.colunaCpfCnpj,
            this.colunaParceiro,
            this.colunaValorTotal,
            this.colunaVendedor,
            this.colunaStatusDocumento,
            this.colunaVendaJaExportado});
            this.gridControl2.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridControl2.GridControl = this.gcVendas;
            this.gridControl2.GroupPanelText = "Enderecos";
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            this.gridControl2.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.False;
            this.gridControl2.OptionsView.ShowGroupPanel = false;
            this.gridControl2.OptionsView.ShowIndicator = false;
            this.gridControl2.OptionsView.ShowViewCaption = true;
            this.gridControl2.PaintStyleName = "Skin";
            this.gridControl2.ViewCaption = "Vendas para Exportação";
            // 
            // colunaDataElaboracao
            // 
            this.colunaDataElaboracao.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaDataElaboracao.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaDataElaboracao.Caption = "Data Elaboração";
            this.colunaDataElaboracao.FieldName = "DataElaboracao";
            this.colunaDataElaboracao.Name = "colunaDataElaboracao";
            this.colunaDataElaboracao.OptionsColumn.AllowEdit = false;
            this.colunaDataElaboracao.OptionsColumn.AllowFocus = false;
            this.colunaDataElaboracao.OptionsFilter.AllowFilter = false;
            this.colunaDataElaboracao.Visible = true;
            this.colunaDataElaboracao.VisibleIndex = 0;
            this.colunaDataElaboracao.Width = 87;
            // 
            // colunaId
            // 
            this.colunaId.Caption = "Nr Pedido Venda";
            this.colunaId.FieldName = "Id";
            this.colunaId.Name = "colunaId";
            this.colunaId.OptionsColumn.AllowEdit = false;
            this.colunaId.OptionsColumn.AllowFocus = false;
            this.colunaId.OptionsFilter.AllowFilter = false;
            this.colunaId.Visible = true;
            this.colunaId.VisibleIndex = 1;
            this.colunaId.Width = 98;
            // 
            // colunaClienteId
            // 
            this.colunaClienteId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaClienteId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaClienteId.Caption = "Cod Parceiro";
            this.colunaClienteId.FieldName = "ClienteId";
            this.colunaClienteId.Name = "colunaClienteId";
            this.colunaClienteId.OptionsColumn.AllowEdit = false;
            this.colunaClienteId.OptionsColumn.AllowFocus = false;
            this.colunaClienteId.OptionsFilter.AllowAutoFilter = false;
            this.colunaClienteId.OptionsFilter.AllowFilter = false;
            this.colunaClienteId.Visible = true;
            this.colunaClienteId.VisibleIndex = 2;
            this.colunaClienteId.Width = 78;
            // 
            // colunaCpfCnpj
            // 
            this.colunaCpfCnpj.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaCpfCnpj.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaCpfCnpj.Caption = "CpfCnpj";
            this.colunaCpfCnpj.FieldName = "ClienteCpfCnpj";
            this.colunaCpfCnpj.Name = "colunaCpfCnpj";
            this.colunaCpfCnpj.OptionsColumn.AllowEdit = false;
            this.colunaCpfCnpj.OptionsColumn.AllowFocus = false;
            this.colunaCpfCnpj.OptionsFilter.AllowFilter = false;
            this.colunaCpfCnpj.Visible = true;
            this.colunaCpfCnpj.VisibleIndex = 3;
            this.colunaCpfCnpj.Width = 104;
            // 
            // colunaParceiro
            // 
            this.colunaParceiro.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaParceiro.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaParceiro.Caption = "Parceiro";
            this.colunaParceiro.FieldName = "ClienteNomeFantasia";
            this.colunaParceiro.Name = "colunaParceiro";
            this.colunaParceiro.OptionsColumn.AllowEdit = false;
            this.colunaParceiro.OptionsColumn.AllowFocus = false;
            this.colunaParceiro.OptionsFilter.AllowFilter = false;
            this.colunaParceiro.Visible = true;
            this.colunaParceiro.VisibleIndex = 4;
            this.colunaParceiro.Width = 143;
            // 
            // colunaValorTotal
            // 
            this.colunaValorTotal.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaValorTotal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaValorTotal.Caption = "Valor Total";
            this.colunaValorTotal.DisplayFormat.FormatString = "#,###,##0.00";
            this.colunaValorTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaValorTotal.FieldName = "ValorTotal";
            this.colunaValorTotal.Name = "colunaValorTotal";
            this.colunaValorTotal.OptionsColumn.AllowEdit = false;
            this.colunaValorTotal.OptionsColumn.AllowFocus = false;
            this.colunaValorTotal.OptionsFilter.AllowFilter = false;
            this.colunaValorTotal.Visible = true;
            this.colunaValorTotal.VisibleIndex = 5;
            this.colunaValorTotal.Width = 85;
            // 
            // colunaVendedor
            // 
            this.colunaVendedor.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaVendedor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaVendedor.Caption = "Vendedor";
            this.colunaVendedor.FieldName = "VendedorNomeFantasia";
            this.colunaVendedor.Name = "colunaVendedor";
            this.colunaVendedor.OptionsColumn.AllowEdit = false;
            this.colunaVendedor.OptionsColumn.AllowFocus = false;
            this.colunaVendedor.OptionsFilter.AllowFilter = false;
            this.colunaVendedor.Visible = true;
            this.colunaVendedor.VisibleIndex = 6;
            this.colunaVendedor.Width = 115;
            // 
            // colunaStatusDocumento
            // 
            this.colunaStatusDocumento.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaStatusDocumento.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaStatusDocumento.Caption = "Situação";
            this.colunaStatusDocumento.FieldName = "StatusDocumento";
            this.colunaStatusDocumento.Name = "colunaStatusDocumento";
            this.colunaStatusDocumento.OptionsColumn.AllowEdit = false;
            this.colunaStatusDocumento.OptionsColumn.AllowFocus = false;
            this.colunaStatusDocumento.OptionsFilter.AllowFilter = false;
            this.colunaStatusDocumento.Visible = true;
            this.colunaStatusDocumento.VisibleIndex = 7;
            this.colunaStatusDocumento.Width = 96;
            // 
            // colunaVendaJaExportado
            // 
            this.colunaVendaJaExportado.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaVendaJaExportado.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaVendaJaExportado.Caption = "Exportado";
            this.colunaVendaJaExportado.FieldName = "VendaJahExportada";
            this.colunaVendaJaExportado.Name = "colunaVendaJaExportado";
            this.colunaVendaJaExportado.OptionsColumn.AllowEdit = false;
            this.colunaVendaJaExportado.OptionsColumn.AllowFocus = false;
            this.colunaVendaJaExportado.OptionsFilter.AllowFilter = false;
            this.colunaVendaJaExportado.Visible = true;
            this.colunaVendaJaExportado.VisibleIndex = 8;
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gcVendas;
            this.gridView3.Name = "gridView3";
            // 
            // btnPesquisaVendas
            // 
            this.btnPesquisaVendas.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaVendas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaVendas.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisaVendas.Image")));
            this.btnPesquisaVendas.Location = new System.Drawing.Point(824, 20);
            this.btnPesquisaVendas.Name = "btnPesquisaVendas";
            this.btnPesquisaVendas.Size = new System.Drawing.Size(27, 23);
            this.btnPesquisaVendas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnPesquisaVendas.TabIndex = 10118;
            this.btnPesquisaVendas.TabStop = false;
            this.btnPesquisaVendas.Click += new System.EventHandler(this.btnPesquisaVendas_Click);
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(557, 21);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboStatus.Properties.DropDownRows = 8;
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboStatus.Size = new System.Drawing.Size(145, 22);
            this.cboStatus.TabIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(557, 6);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(115, 13);
            this.labelControl5.TabIndex = 10117;
            this.labelControl5.Text = "Status Pedido de Venda";
            // 
            // cboVendaJahExportada
            // 
            this.cboVendaJahExportada.EnterMoveNextControl = true;
            this.cboVendaJahExportada.Location = new System.Drawing.Point(708, 21);
            this.cboVendaJahExportada.Name = "cboVendaJahExportada";
            this.cboVendaJahExportada.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVendaJahExportada.Properties.Appearance.Options.UseFont = true;
            this.cboVendaJahExportada.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboVendaJahExportada.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboVendaJahExportada.Properties.NullText = "";
            this.cboVendaJahExportada.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboVendaJahExportada.Size = new System.Drawing.Size(109, 22);
            this.cboVendaJahExportada.TabIndex = 6;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(708, 6);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(93, 13);
            this.labelControl4.TabIndex = 10115;
            this.labelControl4.Text = "Venda já Exportada";
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.EditValue = new System.DateTime(2014, 12, 5, 8, 26, 24, 0);
            this.txtDataFinal.EnterMoveNextControl = true;
            this.txtDataFinal.Location = new System.Drawing.Point(437, 21);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinal.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinal.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinal.Size = new System.Drawing.Size(114, 22);
            this.txtDataFinal.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(437, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 13);
            this.labelControl3.TabIndex = 10113;
            this.labelControl3.Text = "Data Final";
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.EditValue = new System.DateTime(2014, 12, 5, 8, 26, 24, 0);
            this.txtDataInicial.EnterMoveNextControl = true;
            this.txtDataInicial.Location = new System.Drawing.Point(317, 21);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicial.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicial.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicial.Size = new System.Drawing.Size(114, 22);
            this.txtDataInicial.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(317, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 13);
            this.labelControl1.TabIndex = 10111;
            this.labelControl1.Text = "Data Inicial";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(2, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(84, 13);
            this.labelControl2.TabIndex = 10109;
            this.labelControl2.Text = "Nr. Pedido Venda";
            // 
            // txtPedidoDeVendaId
            // 
            this.txtPedidoDeVendaId.Location = new System.Drawing.Point(2, 21);
            this.txtPedidoDeVendaId.Name = "txtPedidoDeVendaId";
            this.txtPedidoDeVendaId.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPedidoDeVendaId.Properties.Appearance.Options.UseFont = true;
            this.txtPedidoDeVendaId.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPedidoDeVendaId.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPedidoDeVendaId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPedidoDeVendaId.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtPedidoDeVendaId.Properties.Mask.EditMask = "[0-9]*";
            this.txtPedidoDeVendaId.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtPedidoDeVendaId.Properties.MaxLength = 8;
            this.txtPedidoDeVendaId.Size = new System.Drawing.Size(151, 22);
            this.txtPedidoDeVendaId.TabIndex = 1;
            this.txtPedidoDeVendaId.EditValueChanged += new System.EventHandler(this.txtPedidoDeVendaId_EditValueChanged);
            this.txtPedidoDeVendaId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPedidoDeVendaId_KeyDown);
            // 
            // labBanco
            // 
            this.labBanco.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labBanco.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labBanco.Location = new System.Drawing.Point(159, 6);
            this.labBanco.Name = "labBanco";
            this.labBanco.Size = new System.Drawing.Size(100, 13);
            this.labBanco.TabIndex = 10120;
            this.labBanco.Text = "Pesquisa por data de";
            // 
            // cboTipoPesquisa
            // 
            this.cboTipoPesquisa.EnterMoveNextControl = true;
            this.cboTipoPesquisa.Location = new System.Drawing.Point(159, 21);
            this.cboTipoPesquisa.Name = "cboTipoPesquisa";
            this.cboTipoPesquisa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoPesquisa.Properties.Appearance.Options.UseFont = true;
            this.cboTipoPesquisa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTipoPesquisa.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboTipoPesquisa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboTipoPesquisa.Properties.NullText = "";
            this.cboTipoPesquisa.Size = new System.Drawing.Size(149, 22);
            this.cboTipoPesquisa.TabIndex = 2;
            // 
            // FormExportaPedidoDeVendaPdvEcf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Name = "FormExportaPedidoDeVendaPdvEcf";
            this.NomeDaTela = "Exportação Pedido de Venda Pdv-Ecf";
            this.Text = "Exportação Pedido de Venda Pdv-Ecf";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcVendas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaVendas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVendaJahExportada.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPedidoDeVendaId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoPesquisa.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnExportarVendas;
        private System.Windows.Forms.Button btnSair;
        private DevExpress.XtraGrid.GridControl gcVendas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridControl2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataElaboracao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaClienteId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCpfCnpj;
        private DevExpress.XtraGrid.Columns.GridColumn colunaParceiro;
        private DevExpress.XtraGrid.Columns.GridColumn colunaValorTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colunaVendedor;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatusDocumento;
        private DevExpress.XtraGrid.Columns.GridColumn colunaVendaJaExportado;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.PictureBox btnPesquisaVendas;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LookUpEdit cboVendaJahExportada;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit txtDataFinal;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit txtDataInicial;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtPedidoDeVendaId;
        private DevExpress.XtraEditors.LabelControl labBanco;
        private DevExpress.XtraEditors.LookUpEdit cboTipoPesquisa;
    }
}