namespace Programax.Easy.View.Telas.Fiscal.CancelamentosNotasFiscais
{
    partial class FormPesquisaNotasFiscaisResumido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPesquisaNotasFiscaisResumido));
            this.btnPesquisaDocumentos = new System.Windows.Forms.PictureBox();
            this.cboTipoDocumento = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataFinal = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataInicial = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroDocumento = new DevExpress.XtraEditors.TextEdit();
            this.gcDocumentos = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNumeroNFe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaSerie = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColunaDataEmissao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCodigoCliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCpfCnpj = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaParceiro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaTipoDocumento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNumeroDocumento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaUsuario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaValorTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.cboModeloNotaFiscal = new DevExpress.XtraEditors.LookUpEdit();
            this.labTipoInscricao = new DevExpress.XtraEditors.LabelControl();
            this.cboStatusNfe = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaDocumentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDocumentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboModeloNotaFiscal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatusNfe.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.labelControl5);
            this.panelConteudo.Controls.Add(this.cboStatusNfe);
            this.panelConteudo.Controls.Add(this.labTipoInscricao);
            this.panelConteudo.Controls.Add(this.cboModeloNotaFiscal);
            this.panelConteudo.Controls.Add(this.gcDocumentos);
            this.panelConteudo.Controls.Add(this.btnPesquisaDocumentos);
            this.panelConteudo.Controls.Add(this.cboTipoDocumento);
            this.panelConteudo.Controls.Add(this.labelControl4);
            this.panelConteudo.Controls.Add(this.txtDataFinal);
            this.panelConteudo.Controls.Add(this.labelControl3);
            this.panelConteudo.Controls.Add(this.txtDataInicial);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.txtNumeroDocumento);
            this.panelConteudo.Size = new System.Drawing.Size(965, 315);
            // 
            // btnPesquisaDocumentos
            // 
            this.btnPesquisaDocumentos.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaDocumentos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaDocumentos.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisaDocumentos.Image")));
            this.btnPesquisaDocumentos.Location = new System.Drawing.Point(925, 20);
            this.btnPesquisaDocumentos.Name = "btnPesquisaDocumentos";
            this.btnPesquisaDocumentos.Size = new System.Drawing.Size(27, 23);
            this.btnPesquisaDocumentos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnPesquisaDocumentos.TabIndex = 657;
            this.btnPesquisaDocumentos.TabStop = false;
            this.btnPesquisaDocumentos.Click += new System.EventHandler(this.btnPesquisaDocumentos_Click);
            // 
            // cboTipoDocumento
            // 
            this.cboTipoDocumento.EnterMoveNextControl = true;
            this.cboTipoDocumento.Location = new System.Drawing.Point(361, 21);
            this.cboTipoDocumento.Name = "cboTipoDocumento";
            this.cboTipoDocumento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoDocumento.Properties.Appearance.Options.UseFont = true;
            this.cboTipoDocumento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTipoDocumento.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboTipoDocumento.Properties.NullText = "";
            this.cboTipoDocumento.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboTipoDocumento.Size = new System.Drawing.Size(202, 22);
            this.cboTipoDocumento.TabIndex = 653;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(361, 6);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(79, 13);
            this.labelControl4.TabIndex = 654;
            this.labelControl4.Text = "Tipo Documento";
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.EditValue = new System.DateTime(2014, 12, 5, 8, 26, 24, 0);
            this.txtDataFinal.EnterMoveNextControl = true;
            this.txtDataFinal.Location = new System.Drawing.Point(241, 21);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinal.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinal.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinal.Size = new System.Drawing.Size(114, 22);
            this.txtDataFinal.TabIndex = 651;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(241, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 13);
            this.labelControl3.TabIndex = 652;
            this.labelControl3.Text = "Data Final";
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.EditValue = new System.DateTime(2014, 12, 5, 8, 26, 24, 0);
            this.txtDataInicial.EnterMoveNextControl = true;
            this.txtDataInicial.Location = new System.Drawing.Point(121, 21);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicial.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicial.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicial.Size = new System.Drawing.Size(114, 22);
            this.txtDataInicial.TabIndex = 649;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(121, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 13);
            this.labelControl1.TabIndex = 650;
            this.labelControl1.Text = "Data Inicial";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(3, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 13);
            this.labelControl2.TabIndex = 648;
            this.labelControl2.Text = "Nr. Documento";
            // 
            // txtNumeroDocumento
            // 
            this.txtNumeroDocumento.EnterMoveNextControl = true;
            this.txtNumeroDocumento.Location = new System.Drawing.Point(3, 21);
            this.txtNumeroDocumento.Name = "txtNumeroDocumento";
            this.txtNumeroDocumento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroDocumento.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroDocumento.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNumeroDocumento.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNumeroDocumento.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroDocumento.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroDocumento.Properties.Mask.EditMask = "[0-9]*";
            this.txtNumeroDocumento.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtNumeroDocumento.Properties.MaxLength = 8;
            this.txtNumeroDocumento.Size = new System.Drawing.Size(112, 22);
            this.txtNumeroDocumento.TabIndex = 647;
            // 
            // gcDocumentos
            // 
            this.gcDocumentos.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcDocumentos.Location = new System.Drawing.Point(3, 49);
            this.gcDocumentos.MainView = this.gridView5;
            this.gcDocumentos.Name = "gcDocumentos";
            this.gcDocumentos.Size = new System.Drawing.Size(958, 263);
            this.gcDocumentos.TabIndex = 10060;
            this.gcDocumentos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcDocumentos.Click += new System.EventHandler(this.gcDocumentos_Click);
            this.gcDocumentos.DoubleClick += new System.EventHandler(this.gcDocumentos_DoubleClick);
            this.gcDocumentos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcDocumentos_KeyDown);
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
            this.gridView5.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridView5.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.gridView5.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridView5.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridView5.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridView5.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gridView5.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView5.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaId,
            this.colunaNumeroNFe,
            this.colunaSerie,
            this.ColunaDataEmissao,
            this.colunaCodigoCliente,
            this.colunaCpfCnpj,
            this.colunaParceiro,
            this.colunaTipoDocumento,
            this.colunaNumeroDocumento,
            this.colunaUsuario,
            this.colunaValorTotal,
            this.colunaStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcDocumentos;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.ViewCaption = "Documentos";
            // 
            // colunaId
            // 
            this.colunaId.Caption = "Id";
            this.colunaId.FieldName = "Id";
            this.colunaId.Name = "colunaId";
            this.colunaId.OptionsColumn.AllowEdit = false;
            this.colunaId.OptionsColumn.AllowFocus = false;
            this.colunaId.OptionsFilter.AllowFilter = false;
            // 
            // colunaNumeroNFe
            // 
            this.colunaNumeroNFe.AppearanceCell.Options.UseTextOptions = true;
            this.colunaNumeroNFe.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaNumeroNFe.Caption = "Nr. NF-e";
            this.colunaNumeroNFe.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaNumeroNFe.FieldName = "NumeroNfe";
            this.colunaNumeroNFe.MinWidth = 10;
            this.colunaNumeroNFe.Name = "colunaNumeroNFe";
            this.colunaNumeroNFe.OptionsColumn.AllowEdit = false;
            this.colunaNumeroNFe.OptionsColumn.AllowFocus = false;
            this.colunaNumeroNFe.OptionsFilter.AllowFilter = false;
            this.colunaNumeroNFe.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaNumeroNFe.Visible = true;
            this.colunaNumeroNFe.VisibleIndex = 0;
            this.colunaNumeroNFe.Width = 63;
            // 
            // colunaSerie
            // 
            this.colunaSerie.Caption = "Serie";
            this.colunaSerie.FieldName = "Serie";
            this.colunaSerie.Name = "colunaSerie";
            this.colunaSerie.OptionsColumn.AllowEdit = false;
            this.colunaSerie.OptionsColumn.AllowFocus = false;
            this.colunaSerie.OptionsFilter.AllowFilter = false;
            this.colunaSerie.Visible = true;
            this.colunaSerie.VisibleIndex = 1;
            this.colunaSerie.Width = 45;
            // 
            // ColunaDataEmissao
            // 
            this.ColunaDataEmissao.Caption = "Data Emissão";
            this.ColunaDataEmissao.FieldName = "DataEmissao";
            this.ColunaDataEmissao.Name = "ColunaDataEmissao";
            this.ColunaDataEmissao.OptionsColumn.AllowEdit = false;
            this.ColunaDataEmissao.OptionsColumn.AllowFocus = false;
            this.ColunaDataEmissao.OptionsFilter.AllowFilter = false;
            this.ColunaDataEmissao.Visible = true;
            this.ColunaDataEmissao.VisibleIndex = 2;
            this.ColunaDataEmissao.Width = 91;
            // 
            // colunaCodigoCliente
            // 
            this.colunaCodigoCliente.Caption = "Cod Cliente";
            this.colunaCodigoCliente.FieldName = "CodigoCliente";
            this.colunaCodigoCliente.Name = "colunaCodigoCliente";
            this.colunaCodigoCliente.OptionsColumn.AllowEdit = false;
            this.colunaCodigoCliente.OptionsColumn.AllowFocus = false;
            this.colunaCodigoCliente.OptionsFilter.AllowFilter = false;
            this.colunaCodigoCliente.Visible = true;
            this.colunaCodigoCliente.VisibleIndex = 3;
            this.colunaCodigoCliente.Width = 67;
            // 
            // colunaCpfCnpj
            // 
            this.colunaCpfCnpj.Caption = "Cpf/Cnpj";
            this.colunaCpfCnpj.FieldName = "CpfCnpj";
            this.colunaCpfCnpj.Name = "colunaCpfCnpj";
            this.colunaCpfCnpj.OptionsColumn.AllowEdit = false;
            this.colunaCpfCnpj.OptionsColumn.AllowFocus = false;
            this.colunaCpfCnpj.OptionsFilter.AllowFilter = false;
            this.colunaCpfCnpj.Visible = true;
            this.colunaCpfCnpj.VisibleIndex = 4;
            this.colunaCpfCnpj.Width = 82;
            // 
            // colunaParceiro
            // 
            this.colunaParceiro.Caption = "Parceiro";
            this.colunaParceiro.FieldName = "Parceiro";
            this.colunaParceiro.Name = "colunaParceiro";
            this.colunaParceiro.OptionsColumn.AllowEdit = false;
            this.colunaParceiro.OptionsColumn.AllowFocus = false;
            this.colunaParceiro.OptionsFilter.AllowFilter = false;
            this.colunaParceiro.Visible = true;
            this.colunaParceiro.VisibleIndex = 5;
            this.colunaParceiro.Width = 117;
            // 
            // colunaTipoDocumento
            // 
            this.colunaTipoDocumento.Caption = "Tipo Documento";
            this.colunaTipoDocumento.FieldName = "TipoDocumento";
            this.colunaTipoDocumento.Name = "colunaTipoDocumento";
            this.colunaTipoDocumento.OptionsColumn.AllowEdit = false;
            this.colunaTipoDocumento.OptionsColumn.AllowFocus = false;
            this.colunaTipoDocumento.OptionsFilter.AllowFilter = false;
            this.colunaTipoDocumento.Visible = true;
            this.colunaTipoDocumento.VisibleIndex = 6;
            this.colunaTipoDocumento.Width = 107;
            // 
            // colunaNumeroDocumento
            // 
            this.colunaNumeroDocumento.Caption = "Nr Documento";
            this.colunaNumeroDocumento.FieldName = "NumeroDocumento";
            this.colunaNumeroDocumento.Name = "colunaNumeroDocumento";
            this.colunaNumeroDocumento.OptionsColumn.AllowEdit = false;
            this.colunaNumeroDocumento.OptionsColumn.AllowFocus = false;
            this.colunaNumeroDocumento.OptionsFilter.AllowFilter = false;
            this.colunaNumeroDocumento.Visible = true;
            this.colunaNumeroDocumento.VisibleIndex = 7;
            this.colunaNumeroDocumento.Width = 82;
            // 
            // colunaUsuario
            // 
            this.colunaUsuario.Caption = "Usuario";
            this.colunaUsuario.FieldName = "Usuario";
            this.colunaUsuario.Name = "colunaUsuario";
            this.colunaUsuario.OptionsColumn.AllowEdit = false;
            this.colunaUsuario.OptionsColumn.AllowFocus = false;
            this.colunaUsuario.OptionsFilter.AllowFilter = false;
            this.colunaUsuario.Visible = true;
            this.colunaUsuario.VisibleIndex = 8;
            this.colunaUsuario.Width = 89;
            // 
            // colunaValorTotal
            // 
            this.colunaValorTotal.Caption = "Vlr Total";
            this.colunaValorTotal.FieldName = "ValorTotal";
            this.colunaValorTotal.Name = "colunaValorTotal";
            this.colunaValorTotal.OptionsColumn.AllowEdit = false;
            this.colunaValorTotal.OptionsColumn.AllowFocus = false;
            this.colunaValorTotal.OptionsFilter.AllowFilter = false;
            this.colunaValorTotal.Visible = true;
            this.colunaValorTotal.VisibleIndex = 9;
            this.colunaValorTotal.Width = 95;
            // 
            // colunaStatus
            // 
            this.colunaStatus.Caption = "Status NF-e";
            this.colunaStatus.FieldName = "StatusNFe";
            this.colunaStatus.Name = "colunaStatus";
            this.colunaStatus.OptionsColumn.AllowEdit = false;
            this.colunaStatus.OptionsColumn.AllowFocus = false;
            this.colunaStatus.OptionsFilter.AllowFilter = false;
            this.colunaStatus.Visible = true;
            this.colunaStatus.VisibleIndex = 10;
            this.colunaStatus.Width = 117;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcDocumentos;
            this.gridView2.Name = "gridView2";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSelecionar);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(435, 41);
            this.flowLayoutPanel1.TabIndex = 10043;
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
            this.btnSelecionar.Size = new System.Drawing.Size(115, 40);
            this.btnSelecionar.TabIndex = 10036;
            this.btnSelecionar.Text = " Selecionar";
            this.btnSelecionar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(115, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 10037;
            this.btnSair.Text = " Sair";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // cboModeloNotaFiscal
            // 
            this.cboModeloNotaFiscal.EnterMoveNextControl = true;
            this.cboModeloNotaFiscal.Location = new System.Drawing.Point(570, 21);
            this.cboModeloNotaFiscal.Name = "cboModeloNotaFiscal";
            this.cboModeloNotaFiscal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboModeloNotaFiscal.Properties.Appearance.Options.UseFont = true;
            this.cboModeloNotaFiscal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboModeloNotaFiscal.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboModeloNotaFiscal.Properties.DropDownRows = 5;
            this.cboModeloNotaFiscal.Properties.NullText = "";
            this.cboModeloNotaFiscal.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboModeloNotaFiscal.Size = new System.Drawing.Size(167, 22);
            this.cboModeloNotaFiscal.TabIndex = 10061;
            // 
            // labTipoInscricao
            // 
            this.labTipoInscricao.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTipoInscricao.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labTipoInscricao.Appearance.Options.UseFont = true;
            this.labTipoInscricao.Appearance.Options.UseForeColor = true;
            this.labTipoInscricao.Location = new System.Drawing.Point(570, 6);
            this.labTipoInscricao.Name = "labTipoInscricao";
            this.labTipoInscricao.Size = new System.Drawing.Size(91, 13);
            this.labTipoInscricao.TabIndex = 10062;
            this.labTipoInscricao.Text = "Modelo Nota Fiscal";
            // 
            // cboStatusNfe
            // 
            this.cboStatusNfe.EnterMoveNextControl = true;
            this.cboStatusNfe.Location = new System.Drawing.Point(743, 21);
            this.cboStatusNfe.Name = "cboStatusNfe";
            this.cboStatusNfe.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatusNfe.Properties.Appearance.Options.UseFont = true;
            this.cboStatusNfe.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatusNfe.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboStatusNfe.Properties.DropDownRows = 8;
            this.cboStatusNfe.Properties.NullText = "";
            this.cboStatusNfe.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboStatusNfe.Size = new System.Drawing.Size(176, 22);
            this.cboStatusNfe.TabIndex = 10063;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(743, 6);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(56, 13);
            this.labelControl5.TabIndex = 10064;
            this.labelControl5.Text = "Status NF-e";
            // 
            // FormPesquisaNotasFiscaisResumido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 426);
            this.Name = "FormPesquisaNotasFiscaisResumido";
            this.NomeDaTela = "Pesquisa Notas Fiscais Resumido";
            this.Text = "Pesquisa Notas Fiscais Resumido";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaDocumentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDocumentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboModeloNotaFiscal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatusNfe.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox btnPesquisaDocumentos;
        private DevExpress.XtraEditors.LookUpEdit cboTipoDocumento;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit txtDataFinal;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit txtDataInicial;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtNumeroDocumento;
        private DevExpress.XtraGrid.GridControl gcDocumentos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNumeroNFe;
        private DevExpress.XtraGrid.Columns.GridColumn colunaSerie;
        private DevExpress.XtraGrid.Columns.GridColumn ColunaDataEmissao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCodigoCliente;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCpfCnpj;
        private DevExpress.XtraGrid.Columns.GridColumn colunaParceiro;
        private DevExpress.XtraGrid.Columns.GridColumn colunaTipoDocumento;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNumeroDocumento;
        private DevExpress.XtraGrid.Columns.GridColumn colunaUsuario;
        private DevExpress.XtraGrid.Columns.GridColumn colunaValorTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.Button btnSair;
        private DevExpress.XtraEditors.LookUpEdit cboModeloNotaFiscal;
        private DevExpress.XtraEditors.LabelControl labTipoInscricao;
        private DevExpress.XtraEditors.LookUpEdit cboStatusNfe;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}