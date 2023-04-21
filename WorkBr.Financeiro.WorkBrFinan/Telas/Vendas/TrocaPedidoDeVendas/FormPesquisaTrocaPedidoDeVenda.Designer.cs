namespace Programax.Easy.View.Telas.Vendas.TrocaPedidoDeVendas
{
    partial class FormPesquisaTrocaPedidoDeVenda
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.gcTrocaPedidosDeVenda = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaPedidoId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataElaboracaoPedido = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataElaboracaoTroca = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNomeCliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaSituacao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaValorDiferenca = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.cboSituacao = new DevExpress.XtraEditors.LookUpEdit();
            this.btnPesquisa = new System.Windows.Forms.PictureBox();
            this.txtDataFinal = new DevExpress.XtraEditors.DateEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataInicial = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.txtClienteCpfCnpj = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.txtNomeCliente = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.txtClienteId = new DevExpress.XtraEditors.TextEdit();
            this.btnPesquisaPedido = new System.Windows.Forms.Button();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtPedidoId = new DevExpress.XtraEditors.TextEdit();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTrocaPedidosDeVenda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSituacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClienteCpfCnpj.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClienteId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPedidoId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Size = new System.Drawing.Size(1122, 40);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.labelControl16);
            this.panelConteudo.Controls.Add(this.txtClienteCpfCnpj);
            this.panelConteudo.Controls.Add(this.labelControl13);
            this.panelConteudo.Controls.Add(this.txtNomeCliente);
            this.panelConteudo.Controls.Add(this.labelControl12);
            this.panelConteudo.Controls.Add(this.txtClienteId);
            this.panelConteudo.Controls.Add(this.btnPesquisaPedido);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.txtPedidoId);
            this.panelConteudo.Controls.Add(this.txtDataFinal);
            this.panelConteudo.Controls.Add(this.labelControl10);
            this.panelConteudo.Controls.Add(this.txtDataInicial);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.btnPesquisa);
            this.panelConteudo.Controls.Add(this.cboSituacao);
            this.panelConteudo.Controls.Add(this.gcTrocaPedidosDeVenda);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Size = new System.Drawing.Size(969, 423);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSelecionar);
            this.flowLayoutPanel1.Controls.Add(this.btnFechar);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(544, 48);
            this.flowLayoutPanel1.TabIndex = 1050;
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
            // gcTrocaPedidosDeVenda
            // 
            this.gcTrocaPedidosDeVenda.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcTrocaPedidosDeVenda.Location = new System.Drawing.Point(3, 48);
            this.gcTrocaPedidosDeVenda.MainView = this.gridView5;
            this.gcTrocaPedidosDeVenda.Name = "gcTrocaPedidosDeVenda";
            this.gcTrocaPedidosDeVenda.Size = new System.Drawing.Size(962, 372);
            this.gcTrocaPedidosDeVenda.TabIndex = 5;
            this.gcTrocaPedidosDeVenda.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcTrocaPedidosDeVenda.DoubleClick += new System.EventHandler(this.gcPedidosDeVenda_DoubleClick);
            this.gcTrocaPedidosDeVenda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcPedidosDeVenda_KeyDown);
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
            this.colunaPedidoId,
            this.colunaDataElaboracaoPedido,
            this.colunaDataElaboracaoTroca,
            this.colunaNomeCliente,
            this.colunaSituacao,
            this.colunaValorDiferenca});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcTrocaPedidosDeVenda;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaPedidoId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Trocas de Pedidos de Venda";
            // 
            // colunaId
            // 
            this.colunaId.Caption = "Nr. Troca";
            this.colunaId.FieldName = "Id";
            this.colunaId.Name = "colunaId";
            this.colunaId.OptionsColumn.AllowEdit = false;
            this.colunaId.OptionsColumn.AllowFocus = false;
            this.colunaId.OptionsFilter.AllowFilter = false;
            this.colunaId.Visible = true;
            this.colunaId.VisibleIndex = 0;
            this.colunaId.Width = 92;
            // 
            // colunaPedidoId
            // 
            this.colunaPedidoId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaPedidoId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaPedidoId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaPedidoId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaPedidoId.Caption = "Nr. Pedido";
            this.colunaPedidoId.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaPedidoId.FieldName = "PedidoId";
            this.colunaPedidoId.MinWidth = 45;
            this.colunaPedidoId.Name = "colunaPedidoId";
            this.colunaPedidoId.OptionsColumn.AllowEdit = false;
            this.colunaPedidoId.OptionsColumn.AllowFocus = false;
            this.colunaPedidoId.OptionsFilter.AllowFilter = false;
            this.colunaPedidoId.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colunaPedidoId.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaPedidoId.Visible = true;
            this.colunaPedidoId.VisibleIndex = 1;
            this.colunaPedidoId.Width = 99;
            // 
            // colunaDataElaboracaoPedido
            // 
            this.colunaDataElaboracaoPedido.Caption = "Data Elaboração Pedido";
            this.colunaDataElaboracaoPedido.FieldName = "DataElaboracaoPedido";
            this.colunaDataElaboracaoPedido.Name = "colunaDataElaboracaoPedido";
            this.colunaDataElaboracaoPedido.OptionsColumn.AllowEdit = false;
            this.colunaDataElaboracaoPedido.OptionsColumn.AllowFocus = false;
            this.colunaDataElaboracaoPedido.OptionsFilter.AllowFilter = false;
            this.colunaDataElaboracaoPedido.Visible = true;
            this.colunaDataElaboracaoPedido.VisibleIndex = 2;
            this.colunaDataElaboracaoPedido.Width = 150;
            // 
            // colunaDataElaboracaoTroca
            // 
            this.colunaDataElaboracaoTroca.Caption = "Data Elaboração Troca";
            this.colunaDataElaboracaoTroca.FieldName = "DataElaboracaoTroca";
            this.colunaDataElaboracaoTroca.Name = "colunaDataElaboracaoTroca";
            this.colunaDataElaboracaoTroca.OptionsColumn.AllowEdit = false;
            this.colunaDataElaboracaoTroca.OptionsColumn.AllowFocus = false;
            this.colunaDataElaboracaoTroca.OptionsFilter.AllowFilter = false;
            this.colunaDataElaboracaoTroca.Visible = true;
            this.colunaDataElaboracaoTroca.VisibleIndex = 3;
            this.colunaDataElaboracaoTroca.Width = 151;
            // 
            // colunaNomeCliente
            // 
            this.colunaNomeCliente.Caption = "Nome Cliente";
            this.colunaNomeCliente.FieldName = "Cliente";
            this.colunaNomeCliente.Name = "colunaNomeCliente";
            this.colunaNomeCliente.OptionsColumn.AllowEdit = false;
            this.colunaNomeCliente.OptionsColumn.AllowFocus = false;
            this.colunaNomeCliente.OptionsFilter.AllowFilter = false;
            this.colunaNomeCliente.Visible = true;
            this.colunaNomeCliente.VisibleIndex = 4;
            this.colunaNomeCliente.Width = 294;
            // 
            // colunaSituacao
            // 
            this.colunaSituacao.Caption = "Situação";
            this.colunaSituacao.FieldName = "Situacao";
            this.colunaSituacao.Name = "colunaSituacao";
            this.colunaSituacao.OptionsColumn.AllowEdit = false;
            this.colunaSituacao.OptionsColumn.AllowFocus = false;
            this.colunaSituacao.OptionsFilter.AllowFilter = false;
            this.colunaSituacao.Visible = true;
            this.colunaSituacao.VisibleIndex = 5;
            this.colunaSituacao.Width = 152;
            // 
            // colunaValorDiferenca
            // 
            this.colunaValorDiferenca.AppearanceCell.Options.UseTextOptions = true;
            this.colunaValorDiferenca.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaValorDiferenca.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaValorDiferenca.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaValorDiferenca.Caption = "Valor Diferença";
            this.colunaValorDiferenca.FieldName = "ValorDiferenca";
            this.colunaValorDiferenca.Name = "colunaValorDiferenca";
            this.colunaValorDiferenca.OptionsColumn.AllowEdit = false;
            this.colunaValorDiferenca.OptionsColumn.AllowFocus = false;
            this.colunaValorDiferenca.OptionsFilter.AllowFilter = false;
            this.colunaValorDiferenca.Visible = true;
            this.colunaValorDiferenca.VisibleIndex = 6;
            this.colunaValorDiferenca.Width = 156;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcTrocaPedidosDeVenda;
            this.gridView2.Name = "gridView2";
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(789, 5);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(42, 13);
            this.labStatus.TabIndex = 652;
            this.labStatus.Text = "Situação";
            // 
            // cboSituacao
            // 
            this.cboSituacao.Location = new System.Drawing.Point(789, 20);
            this.cboSituacao.Name = "cboSituacao";
            this.cboSituacao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSituacao.Properties.Appearance.Options.UseFont = true;
            this.cboSituacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSituacao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboSituacao.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboSituacao.Properties.DropDownRows = 8;
            this.cboSituacao.Properties.NullText = "";
            this.cboSituacao.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboSituacao.Size = new System.Drawing.Size(148, 22);
            this.cboSituacao.TabIndex = 4;
            this.cboSituacao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboSituacao_KeyDown);
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisa.Location = new System.Drawing.Point(943, 20);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisa.TabIndex = 651;
            this.btnPesquisa.TabStop = false;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.EditValue = "";
            this.txtDataFinal.EnterMoveNextControl = true;
            this.txtDataFinal.Location = new System.Drawing.Point(126, 20);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinal.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinal.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinal.Size = new System.Drawing.Size(117, 22);
            this.txtDataFinal.TabIndex = 2;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Location = new System.Drawing.Point(126, 4);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(48, 13);
            this.labelControl10.TabIndex = 10024;
            this.labelControl10.Text = "Data Final";
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.EditValue = "";
            this.txtDataInicial.EnterMoveNextControl = true;
            this.txtDataInicial.Location = new System.Drawing.Point(3, 20);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicial.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicial.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicial.Size = new System.Drawing.Size(117, 22);
            this.txtDataInicial.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 13);
            this.labelControl1.TabIndex = 10023;
            this.labelControl1.Text = "Data Inicial";
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl16.Location = new System.Drawing.Point(662, 4);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(52, 13);
            this.labelControl16.TabIndex = 10034;
            this.labelControl16.Text = "CPF/CNPJ";
            // 
            // txtClienteCpfCnpj
            // 
            this.txtClienteCpfCnpj.EnterMoveNextControl = true;
            this.txtClienteCpfCnpj.Location = new System.Drawing.Point(662, 20);
            this.txtClienteCpfCnpj.Name = "txtClienteCpfCnpj";
            this.txtClienteCpfCnpj.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClienteCpfCnpj.Properties.Appearance.Options.UseFont = true;
            this.txtClienteCpfCnpj.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtClienteCpfCnpj.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtClienteCpfCnpj.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClienteCpfCnpj.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtClienteCpfCnpj.Properties.Mask.EditMask = "99/";
            this.txtClienteCpfCnpj.Properties.ReadOnly = true;
            this.txtClienteCpfCnpj.Size = new System.Drawing.Size(120, 22);
            this.txtClienteCpfCnpj.TabIndex = 10033;
            this.txtClienteCpfCnpj.TabStop = false;
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl13.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl13.Location = new System.Drawing.Point(460, 4);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(78, 13);
            this.labelControl13.TabIndex = 10031;
            this.labelControl13.Text = "Nome do Cliente";
            // 
            // txtNomeCliente
            // 
            this.txtNomeCliente.EnterMoveNextControl = true;
            this.txtNomeCliente.Location = new System.Drawing.Point(460, 20);
            this.txtNomeCliente.Name = "txtNomeCliente";
            this.txtNomeCliente.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeCliente.Properties.Appearance.Options.UseFont = true;
            this.txtNomeCliente.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNomeCliente.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNomeCliente.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeCliente.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNomeCliente.Properties.Mask.EditMask = "99/";
            this.txtNomeCliente.Properties.ReadOnly = true;
            this.txtNomeCliente.Size = new System.Drawing.Size(198, 22);
            this.txtNomeCliente.TabIndex = 10030;
            this.txtNomeCliente.TabStop = false;
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl12.Location = new System.Drawing.Point(360, 4);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(57, 13);
            this.labelControl12.TabIndex = 10029;
            this.labelControl12.Text = "Cód. Cliente";
            // 
            // txtClienteId
            // 
            this.txtClienteId.EnterMoveNextControl = true;
            this.txtClienteId.Location = new System.Drawing.Point(360, 20);
            this.txtClienteId.Name = "txtClienteId";
            this.txtClienteId.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClienteId.Properties.Appearance.Options.UseFont = true;
            this.txtClienteId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtClienteId.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtClienteId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClienteId.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtClienteId.Properties.Mask.EditMask = "99/";
            this.txtClienteId.Properties.ReadOnly = true;
            this.txtClienteId.Size = new System.Drawing.Size(94, 22);
            this.txtClienteId.TabIndex = 10028;
            this.txtClienteId.TabStop = false;
            // 
            // btnPesquisaPedido
            // 
            this.btnPesquisaPedido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaPedido.FlatAppearance.BorderSize = 0;
            this.btnPesquisaPedido.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaPedido.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaPedido.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisaPedido.Location = new System.Drawing.Point(326, 16);
            this.btnPesquisaPedido.Name = "btnPesquisaPedido";
            this.btnPesquisaPedido.Size = new System.Drawing.Size(28, 23);
            this.btnPesquisaPedido.TabIndex = 10027;
            this.btnPesquisaPedido.TabStop = false;
            this.btnPesquisaPedido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisaPedido.UseVisualStyleBackColor = true;
            this.btnPesquisaPedido.Click += new System.EventHandler(this.btnPesquisaPedido_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(249, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 10026;
            this.labelControl2.Text = "Nr. Pedido";
            // 
            // txtPedidoId
            // 
            this.txtPedidoId.EnterMoveNextControl = true;
            this.txtPedidoId.Location = new System.Drawing.Point(249, 20);
            this.txtPedidoId.Name = "txtPedidoId";
            this.txtPedidoId.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPedidoId.Properties.Appearance.Options.UseFont = true;
            this.txtPedidoId.Size = new System.Drawing.Size(71, 22);
            this.txtPedidoId.TabIndex = 3;
            this.txtPedidoId.Leave += new System.EventHandler(this.txtPedidoId_Leave);
            // 
            // FormPesquisaTrocaPedidoDeVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 534);
            this.Name = "FormPesquisaTrocaPedidoDeVenda";
            this.NomeDaTela = "Pesquisa Troca de Pedido de Venda";
            this.Text = "Pesquisa Troca de Pedido de Venda";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTrocaPedidosDeVenda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSituacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClienteCpfCnpj.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClienteId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPedidoId.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.Button btnFechar;
        private DevExpress.XtraGrid.GridControl gcTrocaPedidosDeVenda;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaPedidoId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataElaboracaoPedido;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraEditors.LookUpEdit cboSituacao;
        private System.Windows.Forms.PictureBox btnPesquisa;
        private DevExpress.XtraEditors.DateEdit txtDataFinal;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.DateEdit txtDataInicial;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colunaSituacao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaValorDiferenca;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.TextEdit txtClienteCpfCnpj;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.TextEdit txtNomeCliente;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.TextEdit txtClienteId;
        private System.Windows.Forms.Button btnPesquisaPedido;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtPedidoId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataElaboracaoTroca;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNomeCliente;
    }
}