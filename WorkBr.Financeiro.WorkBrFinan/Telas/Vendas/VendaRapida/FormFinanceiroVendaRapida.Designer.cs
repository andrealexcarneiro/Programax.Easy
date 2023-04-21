namespace Programax.Easy.View.Telas.Vendas.VendaRapida
{
    partial class FormFinanceiroVendaRapida
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFinanceiroVendaRapida));
            this.labelControl51 = new DevExpress.XtraEditors.LabelControl();
            this.gcFinanceiro = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaFinanceiroId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaParcela = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCondicaoPagamento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaFormaPagamento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaVencimento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaValor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaOperadora = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtParcelaFinanceiro = new DevExpress.XtraEditors.TextEdit();
            this.txtValorFinanceiro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl57 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataVencimento = new DevExpress.XtraEditors.DateEdit();
            this.labelControl56 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl55 = new DevExpress.XtraEditors.LabelControl();
            this.cboFormaPagamentoFinanceiro = new DevExpress.XtraEditors.LookUpEdit();
            this.btnCancelarParcela = new System.Windows.Forms.Button();
            this.btnAtualizarParcela = new System.Windows.Forms.Button();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.cboOperadorasCredito = new DevExpress.XtraEditors.LookUpEdit();
            this.lblCredito = new DevExpress.XtraEditors.LabelControl();
            this.txtRestante = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnAdicionarParcela = new System.Windows.Forms.Button();
            this.btnReiniciar = new System.Windows.Forms.Button();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboCondicaoPagamento = new DevExpress.XtraEditors.LookUpEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.chkTodasOperadoras = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gcFinanceiro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParcelaFinanceiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValorFinanceiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVencimento.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVencimento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFormaPagamentoFinanceiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOperadorasCredito.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRestante.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCondicaoPagamento.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl51
            // 
            this.labelControl51.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl51.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl51.Appearance.Options.UseFont = true;
            this.labelControl51.Appearance.Options.UseForeColor = true;
            this.labelControl51.Location = new System.Drawing.Point(16, 83);
            this.labelControl51.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl51.Name = "labelControl51";
            this.labelControl51.Size = new System.Drawing.Size(43, 17);
            this.labelControl51.TabIndex = 10034;
            this.labelControl51.Text = "Parcela";
            // 
            // gcFinanceiro
            // 
            this.gcFinanceiro.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcFinanceiro.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gcFinanceiro.Location = new System.Drawing.Point(16, 200);
            this.gcFinanceiro.MainView = this.gridView5;
            this.gcFinanceiro.Margin = new System.Windows.Forms.Padding(4);
            this.gcFinanceiro.Name = "gcFinanceiro";
            this.gcFinanceiro.Size = new System.Drawing.Size(1156, 242);
            this.gcFinanceiro.TabIndex = 10032;
            this.gcFinanceiro.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcFinanceiro.DoubleClick += new System.EventHandler(this.gcFinanceiro_DoubleClick);
            this.gcFinanceiro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcFinanceiro_KeyDown);
            // 
            // gridView5
            // 
            this.gridView5.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView5.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaFinanceiroId,
            this.colunaParcela,
            this.colunaCondicaoPagamento,
            this.colunaFormaPagamento,
            this.colunaVencimento,
            this.colunaValor,
            this.colunaOperadora});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 288, 219);
            this.gridView5.DetailHeight = 431;
            this.gridView5.GridControl = this.gcFinanceiro;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaFinanceiroId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Financeiro";
            // 
            // colunaFinanceiroId
            // 
            this.colunaFinanceiroId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaFinanceiroId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaFinanceiroId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaFinanceiroId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaFinanceiroId.Caption = "ID";
            this.colunaFinanceiroId.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaFinanceiroId.FieldName = "Id";
            this.colunaFinanceiroId.MinWidth = 60;
            this.colunaFinanceiroId.Name = "colunaFinanceiroId";
            this.colunaFinanceiroId.OptionsColumn.AllowEdit = false;
            this.colunaFinanceiroId.OptionsColumn.AllowFocus = false;
            this.colunaFinanceiroId.OptionsFilter.AllowFilter = false;
            this.colunaFinanceiroId.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colunaFinanceiroId.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaFinanceiroId.Width = 95;
            // 
            // colunaParcela
            // 
            this.colunaParcela.Caption = "Parcela";
            this.colunaParcela.FieldName = "Parcela";
            this.colunaParcela.MinWidth = 27;
            this.colunaParcela.Name = "colunaParcela";
            this.colunaParcela.OptionsColumn.AllowEdit = false;
            this.colunaParcela.OptionsColumn.AllowFocus = false;
            this.colunaParcela.OptionsFilter.AllowFilter = false;
            this.colunaParcela.Visible = true;
            this.colunaParcela.VisibleIndex = 0;
            this.colunaParcela.Width = 99;
            // 
            // colunaCondicaoPagamento
            // 
            this.colunaCondicaoPagamento.Caption = "Cond. Pagamento";
            this.colunaCondicaoPagamento.FieldName = "CondicaoPagamento";
            this.colunaCondicaoPagamento.MinWidth = 27;
            this.colunaCondicaoPagamento.Name = "colunaCondicaoPagamento";
            this.colunaCondicaoPagamento.OptionsColumn.AllowEdit = false;
            this.colunaCondicaoPagamento.OptionsColumn.AllowFocus = false;
            this.colunaCondicaoPagamento.OptionsFilter.AllowFilter = false;
            this.colunaCondicaoPagamento.Visible = true;
            this.colunaCondicaoPagamento.VisibleIndex = 1;
            this.colunaCondicaoPagamento.Width = 248;
            // 
            // colunaFormaPagamento
            // 
            this.colunaFormaPagamento.Caption = "Forma de Pagamento";
            this.colunaFormaPagamento.FieldName = "FormaPagamento";
            this.colunaFormaPagamento.MinWidth = 27;
            this.colunaFormaPagamento.Name = "colunaFormaPagamento";
            this.colunaFormaPagamento.OptionsColumn.AllowEdit = false;
            this.colunaFormaPagamento.OptionsColumn.AllowFocus = false;
            this.colunaFormaPagamento.OptionsFilter.AllowFilter = false;
            this.colunaFormaPagamento.Visible = true;
            this.colunaFormaPagamento.VisibleIndex = 2;
            this.colunaFormaPagamento.Width = 232;
            // 
            // colunaVencimento
            // 
            this.colunaVencimento.Caption = "Vencimento";
            this.colunaVencimento.FieldName = "DataVencimento";
            this.colunaVencimento.MinWidth = 27;
            this.colunaVencimento.Name = "colunaVencimento";
            this.colunaVencimento.OptionsColumn.AllowEdit = false;
            this.colunaVencimento.OptionsColumn.AllowFocus = false;
            this.colunaVencimento.OptionsFilter.AllowFilter = false;
            this.colunaVencimento.Visible = true;
            this.colunaVencimento.VisibleIndex = 3;
            this.colunaVencimento.Width = 160;
            // 
            // colunaValor
            // 
            this.colunaValor.Caption = "Valor (R$)";
            this.colunaValor.FieldName = "Valor";
            this.colunaValor.MinWidth = 27;
            this.colunaValor.Name = "colunaValor";
            this.colunaValor.OptionsColumn.AllowEdit = false;
            this.colunaValor.OptionsColumn.AllowFocus = false;
            this.colunaValor.OptionsFilter.AllowFilter = false;
            this.colunaValor.Visible = true;
            this.colunaValor.VisibleIndex = 4;
            this.colunaValor.Width = 197;
            // 
            // colunaOperadora
            // 
            this.colunaOperadora.Caption = "Operadora";
            this.colunaOperadora.FieldName = "Operadora";
            this.colunaOperadora.MinWidth = 25;
            this.colunaOperadora.Name = "colunaOperadora";
            this.colunaOperadora.OptionsColumn.AllowEdit = false;
            this.colunaOperadora.OptionsColumn.AllowFocus = false;
            this.colunaOperadora.OptionsFilter.AllowFilter = false;
            this.colunaOperadora.Visible = true;
            this.colunaOperadora.VisibleIndex = 5;
            this.colunaOperadora.Width = 218;
            // 
            // gridView2
            // 
            this.gridView2.DetailHeight = 431;
            this.gridView2.GridControl = this.gcFinanceiro;
            this.gridView2.Name = "gridView2";
            // 
            // txtParcelaFinanceiro
            // 
            this.txtParcelaFinanceiro.EnterMoveNextControl = true;
            this.txtParcelaFinanceiro.Location = new System.Drawing.Point(16, 103);
            this.txtParcelaFinanceiro.Margin = new System.Windows.Forms.Padding(4);
            this.txtParcelaFinanceiro.Name = "txtParcelaFinanceiro";
            this.txtParcelaFinanceiro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParcelaFinanceiro.Properties.Appearance.Options.UseFont = true;
            this.txtParcelaFinanceiro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtParcelaFinanceiro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtParcelaFinanceiro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtParcelaFinanceiro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtParcelaFinanceiro.Properties.Mask.EditMask = "99/";
            this.txtParcelaFinanceiro.Properties.MaxLength = 80;
            this.txtParcelaFinanceiro.Properties.ReadOnly = true;
            this.txtParcelaFinanceiro.Size = new System.Drawing.Size(88, 26);
            this.txtParcelaFinanceiro.TabIndex = 10033;
            this.txtParcelaFinanceiro.TabStop = false;
            // 
            // txtValorFinanceiro
            // 
            this.txtValorFinanceiro.EnterMoveNextControl = true;
            this.txtValorFinanceiro.Location = new System.Drawing.Point(616, 103);
            this.txtValorFinanceiro.Margin = new System.Windows.Forms.Padding(4);
            this.txtValorFinanceiro.Name = "txtValorFinanceiro";
            this.txtValorFinanceiro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorFinanceiro.Properties.Appearance.Options.UseFont = true;
            this.txtValorFinanceiro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtValorFinanceiro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtValorFinanceiro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtValorFinanceiro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtValorFinanceiro.Properties.Mask.EditMask = "[0-9]{1,11}([\\.\\,][0-9]{0,2})?";
            this.txtValorFinanceiro.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtValorFinanceiro.Properties.Mask.ShowPlaceHolders = false;
            this.txtValorFinanceiro.Properties.MaxLength = 30;
            this.txtValorFinanceiro.Size = new System.Drawing.Size(179, 26);
            this.txtValorFinanceiro.TabIndex = 10039;
            // 
            // labelControl57
            // 
            this.labelControl57.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl57.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl57.Appearance.Options.UseFont = true;
            this.labelControl57.Appearance.Options.UseForeColor = true;
            this.labelControl57.Location = new System.Drawing.Point(616, 83);
            this.labelControl57.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl57.Name = "labelControl57";
            this.labelControl57.Size = new System.Drawing.Size(65, 17);
            this.labelControl57.TabIndex = 10040;
            this.labelControl57.Text = "Valor (R$)";
            // 
            // txtDataVencimento
            // 
            this.txtDataVencimento.EditValue = "";
            this.txtDataVencimento.EnterMoveNextControl = true;
            this.txtDataVencimento.Location = new System.Drawing.Point(440, 103);
            this.txtDataVencimento.Margin = new System.Windows.Forms.Padding(4);
            this.txtDataVencimento.Name = "txtDataVencimento";
            this.txtDataVencimento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataVencimento.Properties.Appearance.Options.UseFont = true;
            this.txtDataVencimento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataVencimento.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataVencimento.Size = new System.Drawing.Size(165, 26);
            this.txtDataVencimento.TabIndex = 10037;
            // 
            // labelControl56
            // 
            this.labelControl56.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl56.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl56.Appearance.Options.UseFont = true;
            this.labelControl56.Appearance.Options.UseForeColor = true;
            this.labelControl56.Location = new System.Drawing.Point(440, 83);
            this.labelControl56.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl56.Name = "labelControl56";
            this.labelControl56.Size = new System.Drawing.Size(74, 17);
            this.labelControl56.TabIndex = 10038;
            this.labelControl56.Text = "Vencimento";
            // 
            // labelControl55
            // 
            this.labelControl55.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl55.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl55.Appearance.Options.UseFont = true;
            this.labelControl55.Appearance.Options.UseForeColor = true;
            this.labelControl55.Location = new System.Drawing.Point(112, 83);
            this.labelControl55.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl55.Name = "labelControl55";
            this.labelControl55.Size = new System.Drawing.Size(136, 17);
            this.labelControl55.TabIndex = 10036;
            this.labelControl55.Text = "Forma de Pagamento";
            // 
            // cboFormaPagamentoFinanceiro
            // 
            this.cboFormaPagamentoFinanceiro.EnterMoveNextControl = true;
            this.cboFormaPagamentoFinanceiro.Location = new System.Drawing.Point(112, 103);
            this.cboFormaPagamentoFinanceiro.Margin = new System.Windows.Forms.Padding(4);
            this.cboFormaPagamentoFinanceiro.Name = "cboFormaPagamentoFinanceiro";
            this.cboFormaPagamentoFinanceiro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFormaPagamentoFinanceiro.Properties.Appearance.Options.UseFont = true;
            this.cboFormaPagamentoFinanceiro.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFormaPagamentoFinanceiro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboFormaPagamentoFinanceiro.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboFormaPagamentoFinanceiro.Properties.NullText = "";
            this.cboFormaPagamentoFinanceiro.Size = new System.Drawing.Size(320, 26);
            this.cboFormaPagamentoFinanceiro.TabIndex = 10035;
            this.cboFormaPagamentoFinanceiro.EditValueChanged += new System.EventHandler(this.cboFormaPagamentoFinanceiro_EditValueChanged);
            // 
            // btnCancelarParcela
            // 
            this.btnCancelarParcela.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarParcela.FlatAppearance.BorderSize = 0;
            this.btnCancelarParcela.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancelarParcela.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancelarParcela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarParcela.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarParcela.Image")));
            this.btnCancelarParcela.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelarParcela.Location = new System.Drawing.Point(1142, 101);
            this.btnCancelarParcela.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelarParcela.Name = "btnCancelarParcela";
            this.btnCancelarParcela.Size = new System.Drawing.Size(37, 28);
            this.btnCancelarParcela.TabIndex = 10042;
            this.btnCancelarParcela.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelarParcela.UseVisualStyleBackColor = true;
            this.btnCancelarParcela.Click += new System.EventHandler(this.btnCancelarParcela_Click);
            // 
            // btnAtualizarParcela
            // 
            this.btnAtualizarParcela.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizarParcela.FlatAppearance.BorderSize = 0;
            this.btnAtualizarParcela.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAtualizarParcela.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAtualizarParcela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizarParcela.Image = global::Programax.Easy.View.Properties.Resources.icon_atualizar;
            this.btnAtualizarParcela.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAtualizarParcela.Location = new System.Drawing.Point(1102, 101);
            this.btnAtualizarParcela.Margin = new System.Windows.Forms.Padding(4);
            this.btnAtualizarParcela.Name = "btnAtualizarParcela";
            this.btnAtualizarParcela.Size = new System.Drawing.Size(37, 28);
            this.btnAtualizarParcela.TabIndex = 10041;
            this.btnAtualizarParcela.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAtualizarParcela.UseVisualStyleBackColor = true;
            this.btnAtualizarParcela.Click += new System.EventHandler(this.btnAtualizarParcela_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(16, 17);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(184, 36);
            this.labelControl1.TabIndex = 10043;
            this.labelControl1.Text = "FINANCEIRO";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-112, 60);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1440, 12);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10044;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-127, 466);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1440, 12);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10045;
            this.pictureBox2.TabStop = false;
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(1050, 485);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(133, 49);
            this.btnSair.TabIndex = 10046;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizar.FlatAppearance.BorderSize = 0;
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizar.Image = global::Programax.Easy.View.Properties.Resources.icone_selecionar1;
            this.btnAtualizar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAtualizar.Location = new System.Drawing.Point(16, 485);
            this.btnAtualizar.Margin = new System.Windows.Forms.Padding(0);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(147, 49);
            this.btnAtualizar.TabIndex = 10047;
            this.btnAtualizar.TabStop = false;
            this.btnAtualizar.Text = " Atualizar";
            this.btnAtualizar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAtualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // cboOperadorasCredito
            // 
            this.cboOperadorasCredito.EnterMoveNextControl = true;
            this.cboOperadorasCredito.Location = new System.Drawing.Point(803, 103);
            this.cboOperadorasCredito.Margin = new System.Windows.Forms.Padding(4);
            this.cboOperadorasCredito.Name = "cboOperadorasCredito";
            this.cboOperadorasCredito.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOperadorasCredito.Properties.Appearance.Options.UseFont = true;
            this.cboOperadorasCredito.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboOperadorasCredito.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboOperadorasCredito.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "ID", 6, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboOperadorasCredito.Properties.NullText = "";
            this.cboOperadorasCredito.Size = new System.Drawing.Size(291, 26);
            this.cboOperadorasCredito.TabIndex = 10131;
            // 
            // lblCredito
            // 
            this.lblCredito.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCredito.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblCredito.Appearance.Options.UseFont = true;
            this.lblCredito.Appearance.Options.UseForeColor = true;
            this.lblCredito.Location = new System.Drawing.Point(806, 81);
            this.lblCredito.Margin = new System.Windows.Forms.Padding(4);
            this.lblCredito.Name = "lblCredito";
            this.lblCredito.Size = new System.Drawing.Size(154, 17);
            this.lblCredito.TabIndex = 10132;
            this.lblCredito.Text = "Operadora de Cartão";
            // 
            // txtRestante
            // 
            this.txtRestante.EnterMoveNextControl = true;
            this.txtRestante.Location = new System.Drawing.Point(804, 161);
            this.txtRestante.Margin = new System.Windows.Forms.Padding(4);
            this.txtRestante.Name = "txtRestante";
            this.txtRestante.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRestante.Properties.Appearance.Options.UseFont = true;
            this.txtRestante.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRestante.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtRestante.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRestante.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtRestante.Properties.Mask.EditMask = "[0-9]{1,11}([\\.\\,][0-9]{0,2})?";
            this.txtRestante.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtRestante.Properties.Mask.ShowPlaceHolders = false;
            this.txtRestante.Properties.MaxLength = 30;
            this.txtRestante.Properties.ReadOnly = true;
            this.txtRestante.Size = new System.Drawing.Size(165, 26);
            this.txtRestante.TabIndex = 10135;
            this.txtRestante.TabStop = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(804, 142);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(140, 16);
            this.labelControl3.TabIndex = 10136;
            this.labelControl3.Text = "Saldo Restante (R$)";
            // 
            // btnAdicionarParcela
            // 
            this.btnAdicionarParcela.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarParcela.FlatAppearance.BorderSize = 0;
            this.btnAdicionarParcela.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarParcela.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarParcela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarParcela.Image = global::Programax.Easy.View.Properties.Resources.icones2_19;
            this.btnAdicionarParcela.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdicionarParcela.Location = new System.Drawing.Point(977, 159);
            this.btnAdicionarParcela.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdicionarParcela.Name = "btnAdicionarParcela";
            this.btnAdicionarParcela.Size = new System.Drawing.Size(37, 28);
            this.btnAdicionarParcela.TabIndex = 10137;
            this.btnAdicionarParcela.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionarParcela.UseVisualStyleBackColor = true;
            this.btnAdicionarParcela.Click += new System.EventHandler(this.btnAdicionarParcela_Click);
            // 
            // btnReiniciar
            // 
            this.btnReiniciar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReiniciar.FlatAppearance.BorderSize = 0;
            this.btnReiniciar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnReiniciar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnReiniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReiniciar.Image = global::Programax.Easy.View.Properties.Resources.icones2_18;
            this.btnReiniciar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReiniciar.Location = new System.Drawing.Point(1022, 159);
            this.btnReiniciar.Margin = new System.Windows.Forms.Padding(4);
            this.btnReiniciar.Name = "btnReiniciar";
            this.btnReiniciar.Size = new System.Drawing.Size(37, 28);
            this.btnReiniciar.TabIndex = 10138;
            this.btnReiniciar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReiniciar.UseVisualStyleBackColor = true;
            this.btnReiniciar.Click += new System.EventHandler(this.btnReiniciar_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(440, 138);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(155, 17);
            this.labelControl2.TabIndex = 10140;
            this.labelControl2.Text = "Condição de Pagamento";
            // 
            // cboCondicaoPagamento
            // 
            this.cboCondicaoPagamento.EnterMoveNextControl = true;
            this.cboCondicaoPagamento.Location = new System.Drawing.Point(440, 159);
            this.cboCondicaoPagamento.Margin = new System.Windows.Forms.Padding(4);
            this.cboCondicaoPagamento.Name = "cboCondicaoPagamento";
            this.cboCondicaoPagamento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCondicaoPagamento.Properties.Appearance.Options.UseFont = true;
            this.cboCondicaoPagamento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCondicaoPagamento.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboCondicaoPagamento.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboCondicaoPagamento.Properties.NullText = "";
            this.cboCondicaoPagamento.Size = new System.Drawing.Size(355, 26);
            this.cboCondicaoPagamento.TabIndex = 10139;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(247, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 20);
            this.label1.TabIndex = 10141;
            this.label1.Text = "Personalizar >>>>";
            // 
            // chkTodasOperadoras
            // 
            this.chkTodasOperadoras.AutoSize = true;
            this.chkTodasOperadoras.Location = new System.Drawing.Point(973, 81);
            this.chkTodasOperadoras.Name = "chkTodasOperadoras";
            this.chkTodasOperadoras.Size = new System.Drawing.Size(70, 21);
            this.chkTodasOperadoras.TabIndex = 10142;
            this.chkTodasOperadoras.Text = "Todos";
            this.chkTodasOperadoras.UseVisualStyleBackColor = true;
            // 
            // FormFinanceiroVendaRapida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 535);
            this.Controls.Add(this.chkTodasOperadoras);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cboCondicaoPagamento);
            this.Controls.Add(this.btnReiniciar);
            this.Controls.Add(this.btnAdicionarParcela);
            this.Controls.Add(this.txtRestante);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.cboOperadorasCredito);
            this.Controls.Add(this.lblCredito);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl51);
            this.Controls.Add(this.btnCancelarParcela);
            this.Controls.Add(this.gcFinanceiro);
            this.Controls.Add(this.btnAtualizarParcela);
            this.Controls.Add(this.txtParcelaFinanceiro);
            this.Controls.Add(this.txtValorFinanceiro);
            this.Controls.Add(this.labelControl57);
            this.Controls.Add(this.txtDataVencimento);
            this.Controls.Add(this.labelControl56);
            this.Controls.Add(this.labelControl55);
            this.Controls.Add(this.cboFormaPagamentoFinanceiro);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormFinanceiroVendaRapida";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormFinanceiroVendaRapida";
            ((System.ComponentModel.ISupportInitialize)(this.gcFinanceiro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParcelaFinanceiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValorFinanceiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVencimento.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVencimento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFormaPagamentoFinanceiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOperadorasCredito.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRestante.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCondicaoPagamento.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl51;
        private System.Windows.Forms.Button btnCancelarParcela;
        public DevExpress.XtraGrid.GridControl gcFinanceiro;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaFinanceiroId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaParcela;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCondicaoPagamento;
        private DevExpress.XtraGrid.Columns.GridColumn colunaFormaPagamento;
        private DevExpress.XtraGrid.Columns.GridColumn colunaVencimento;
        private DevExpress.XtraGrid.Columns.GridColumn colunaValor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.Button btnAtualizarParcela;
        private DevExpress.XtraEditors.TextEdit txtParcelaFinanceiro;
        private DevExpress.XtraEditors.TextEdit txtValorFinanceiro;
        private DevExpress.XtraEditors.LabelControl labelControl57;
        private DevExpress.XtraEditors.DateEdit txtDataVencimento;
        private DevExpress.XtraEditors.LabelControl labelControl56;
        private DevExpress.XtraEditors.LabelControl labelControl55;
        private DevExpress.XtraEditors.LookUpEdit cboFormaPagamentoFinanceiro;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnAtualizar;
        private DevExpress.XtraEditors.LookUpEdit cboOperadorasCredito;
        private DevExpress.XtraEditors.LabelControl lblCredito;
        private DevExpress.XtraEditors.TextEdit txtRestante;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Button btnAdicionarParcela;
        private DevExpress.XtraGrid.Columns.GridColumn colunaOperadora;
        private System.Windows.Forms.Button btnReiniciar;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit cboCondicaoPagamento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkTodasOperadoras;
    }
}