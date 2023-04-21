namespace Programax.Easy.View.Telas.Financeiro.FormasPagamento
{
    partial class FormFormaPagamentoPesquisa
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
            this.gcFormasPagamento = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDescricao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaSite = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaUsaPedidoVenda = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.btnPesquisaNcm = new System.Windows.Forms.PictureBox();
            this.txtDescricao = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcFormasPagamento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaNcm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Size = new System.Drawing.Size(903, 40);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.label1);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.txtDescricao);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Controls.Add(this.btnPesquisaNcm);
            this.panelConteudo.Controls.Add(this.gcFormasPagamento);
            this.panelConteudo.Size = new System.Drawing.Size(732, 267);
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
            this.colunaStatus.VisibleIndex = 6;
            this.colunaStatus.Width = 83;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcFormasPagamento;
            this.gridView2.Name = "gridView2";
            // 
            // gcFormasPagamento
            // 
            this.gcFormasPagamento.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcFormasPagamento.Location = new System.Drawing.Point(3, 49);
            this.gcFormasPagamento.MainView = this.gridView5;
            this.gcFormasPagamento.Name = "gcFormasPagamento";
            this.gcFormasPagamento.Size = new System.Drawing.Size(726, 213);
            this.gcFormasPagamento.TabIndex = 3;
            this.gcFormasPagamento.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcFormasPagamento.DoubleClick += new System.EventHandler(this.gcNcms_DoubleClick);
            this.gcFormasPagamento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcNcms_KeyDown);
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
            this.colunaDescricao,
            this.colunaSite,
            this.gridColumn1,
            this.gridColumn2,
            this.colunaUsaPedidoVenda,
            this.colunaStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcFormasPagamento;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Formas de Pagamento";
            // 
            // colunaId
            // 
            this.colunaId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.Caption = "Cód. Cadastro";
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
            this.colunaId.Width = 98;
            // 
            // colunaDescricao
            // 
            this.colunaDescricao.Caption = "Descrição";
            this.colunaDescricao.FieldName = "Descricao";
            this.colunaDescricao.Name = "colunaDescricao";
            this.colunaDescricao.OptionsColumn.AllowEdit = false;
            this.colunaDescricao.OptionsColumn.AllowFocus = false;
            this.colunaDescricao.OptionsFilter.AllowFilter = false;
            this.colunaDescricao.Visible = true;
            this.colunaDescricao.VisibleIndex = 1;
            this.colunaDescricao.Width = 159;
            // 
            // colunaSite
            // 
            this.colunaSite.Caption = "Usa PDV";
            this.colunaSite.FieldName = "DisponivelParaPdv";
            this.colunaSite.Name = "colunaSite";
            this.colunaSite.OptionsColumn.AllowEdit = false;
            this.colunaSite.OptionsColumn.AllowFocus = false;
            this.colunaSite.OptionsFilter.AllowFilter = false;
            this.colunaSite.Visible = true;
            this.colunaSite.VisibleIndex = 2;
            this.colunaSite.Width = 56;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Usa Contas a Pagar";
            this.gridColumn1.FieldName = "DisponivelParaContasPagar";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 114;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Usa Contas a Receber";
            this.gridColumn2.FieldName = "DisponivelParaContasReceber";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            this.gridColumn2.Width = 124;
            // 
            // colunaUsaPedidoVenda
            // 
            this.colunaUsaPedidoVenda.Caption = "Usa Pedido Venda";
            this.colunaUsaPedidoVenda.FieldName = "DisponivelParaPedidoVenda";
            this.colunaUsaPedidoVenda.Name = "colunaUsaPedidoVenda";
            this.colunaUsaPedidoVenda.OptionsColumn.AllowEdit = false;
            this.colunaUsaPedidoVenda.OptionsColumn.AllowFocus = false;
            this.colunaUsaPedidoVenda.OptionsFilter.AllowFilter = false;
            this.colunaUsaPedidoVenda.Visible = true;
            this.colunaUsaPedidoVenda.VisibleIndex = 5;
            this.colunaUsaPedidoVenda.Width = 107;
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(553, 21);
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
            this.cboStatus.Size = new System.Drawing.Size(148, 22);
            this.cboStatus.TabIndex = 2;
            this.cboStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboStatus_KeyDown);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSelecionar);
            this.flowLayoutPanel1.Controls.Add(this.btnFechar);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(329, 46);
            this.flowLayoutPanel1.TabIndex = 1049;
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelecionar.FlatAppearance.BorderSize = 0;
            this.btnSelecionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionar.Image = global::Programax.Easy.View.Properties.Resources.icon_atualizar;
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
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.labStatus.Location = new System.Drawing.Point(553, 4);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 643;
            this.labStatus.Text = "Status";
            // 
            // btnPesquisaNcm
            // 
            this.btnPesquisaNcm.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaNcm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaNcm.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaNcm.Location = new System.Drawing.Point(707, 20);
            this.btnPesquisaNcm.Name = "btnPesquisaNcm";
            this.btnPesquisaNcm.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaNcm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaNcm.TabIndex = 640;
            this.btnPesquisaNcm.TabStop = false;
            this.btnPesquisaNcm.Click += new System.EventHandler(this.btnPesquisaNcm_Click);
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(3, 21);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.Properties.Appearance.Options.UseFont = true;
            this.txtDescricao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricao.Properties.Mask.EditMask = "99/";
            this.txtDescricao.Properties.MaxLength = 10;
            this.txtDescricao.Size = new System.Drawing.Size(544, 22);
            this.txtDescricao.TabIndex = 1;
            this.txtDescricao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescricao_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 639;
            this.label1.Text = "Descrição";
            // 
            // FormFormaPagamentoPesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 378);
            this.MaximizeBox = false;
            this.Name = "FormFormaPagamentoPesquisa";
            this.NomeDaTela = "Pesquisa de Forma de Pagamento";
            this.Text = "Pesquisa de Forma de Pagamento";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcFormasPagamento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaNcm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gcFormasPagamento;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaSite;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private System.Windows.Forms.PictureBox btnPesquisaNcm;
        private DevExpress.XtraEditors.TextEdit txtDescricao;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraGrid.Columns.GridColumn colunaUsaPedidoVenda;
    }
}