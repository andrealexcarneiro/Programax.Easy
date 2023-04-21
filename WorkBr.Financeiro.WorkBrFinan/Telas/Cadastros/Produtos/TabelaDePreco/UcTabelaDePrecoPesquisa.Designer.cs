namespace Programax.Easy.View.Telas.Produtos.TabelaDePreco
{
    partial class UcTabelaDePrecoPesquisa
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcTabelaDePrecoPesquisa));
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcTabelasPrecos = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDescricao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataCadastro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataValidade = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtDataValidade = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.pbPesquisaCadastro = new System.Windows.Forms.PictureBox();
            this.txtChave = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTabelasPrecos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataValidade.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataValidade.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChave.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cboStatus
            // 
            this.cboStatus.Location = new System.Drawing.Point(379, 17);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboStatus.Properties.DisplayMember = "Descricao";
            this.cboStatus.Properties.DropDownRows = 3;
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboStatus.Properties.ValueMember = "Valor";
            this.cboStatus.Size = new System.Drawing.Size(149, 22);
            this.cboStatus.TabIndex = 3;
            this.cboStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboStatus_KeyDown);
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(379, 0);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 452;
            this.labStatus.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 450;
            this.label1.Text = "Dt. Validade";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcTabelasPrecos;
            this.gridView2.Name = "gridView2";
            // 
            // gcTabelasPrecos
            // 
            this.gcTabelasPrecos.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcTabelasPrecos.Location = new System.Drawing.Point(3, 44);
            this.gcTabelasPrecos.MainView = this.gridView5;
            this.gcTabelasPrecos.Name = "gcTabelasPrecos";
            this.gcTabelasPrecos.Size = new System.Drawing.Size(553, 251);
            this.gcTabelasPrecos.TabIndex = 4;
            this.gcTabelasPrecos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcTabelasPrecos.DoubleClick += new System.EventHandler(this.gcTabelasPrecos_DoubleClick);
            this.gcTabelasPrecos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcTabelasPrecos_KeyDown);
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
            this.colunaDataCadastro,
            this.colunaDataValidade,
            this.colunaStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcTabelasPrecos;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Tabelas de Preços";
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
            this.colunaId.Width = 100;
            // 
            // colunaDescricao
            // 
            this.colunaDescricao.Caption = "Nome da Tabela";
            this.colunaDescricao.FieldName = "NomeTabela";
            this.colunaDescricao.Name = "colunaDescricao";
            this.colunaDescricao.OptionsColumn.AllowEdit = false;
            this.colunaDescricao.OptionsColumn.AllowFocus = false;
            this.colunaDescricao.OptionsFilter.AllowFilter = false;
            this.colunaDescricao.Visible = true;
            this.colunaDescricao.VisibleIndex = 1;
            this.colunaDescricao.Width = 209;
            // 
            // colunaDataCadastro
            // 
            this.colunaDataCadastro.AppearanceCell.Options.UseTextOptions = true;
            this.colunaDataCadastro.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaDataCadastro.Caption = "Data Cadastro";
            this.colunaDataCadastro.FieldName = "DataDeCadastro";
            this.colunaDataCadastro.MinWidth = 10;
            this.colunaDataCadastro.Name = "colunaDataCadastro";
            this.colunaDataCadastro.OptionsColumn.AllowEdit = false;
            this.colunaDataCadastro.OptionsColumn.AllowFocus = false;
            this.colunaDataCadastro.OptionsFilter.AllowFilter = false;
            this.colunaDataCadastro.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.colunaDataCadastro.Visible = true;
            this.colunaDataCadastro.VisibleIndex = 2;
            this.colunaDataCadastro.Width = 103;
            // 
            // colunaDataValidade
            // 
            this.colunaDataValidade.Caption = "Data Validade";
            this.colunaDataValidade.FieldName = "DataDeValidade";
            this.colunaDataValidade.Name = "colunaDataValidade";
            this.colunaDataValidade.OptionsColumn.AllowEdit = false;
            this.colunaDataValidade.OptionsColumn.AllowFocus = false;
            this.colunaDataValidade.OptionsFilter.AllowFilter = false;
            this.colunaDataValidade.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.colunaDataValidade.Visible = true;
            this.colunaDataValidade.VisibleIndex = 3;
            this.colunaDataValidade.Width = 93;
            // 
            // colunaStatus
            // 
            this.colunaStatus.Caption = "Status";
            this.colunaStatus.FieldName = "DescricaoStatus";
            this.colunaStatus.Name = "colunaStatus";
            this.colunaStatus.OptionsColumn.AllowEdit = false;
            this.colunaStatus.OptionsColumn.AllowFocus = false;
            this.colunaStatus.OptionsFilter.AllowFilter = false;
            this.colunaStatus.Visible = true;
            this.colunaStatus.VisibleIndex = 4;
            this.colunaStatus.Width = 91;
            // 
            // txtDataValidade
            // 
            this.txtDataValidade.EditValue = null;
            this.txtDataValidade.EnterMoveNextControl = true;
            this.txtDataValidade.Location = new System.Drawing.Point(268, 17);
            this.txtDataValidade.Name = "txtDataValidade";
            this.txtDataValidade.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataValidade.Properties.Appearance.Options.UseFont = true;
            this.txtDataValidade.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataValidade.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataValidade.Size = new System.Drawing.Size(105, 22);
            this.txtDataValidade.TabIndex = 2;
            this.txtDataValidade.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDataValidade_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, -1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 446;
            this.label2.Text = "Descrição";
            // 
            // pbPesquisaCadastro
            // 
            this.pbPesquisaCadastro.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaCadastro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaCadastro.Image = ((System.Drawing.Image)(resources.GetObject("pbPesquisaCadastro.Image")));
            this.pbPesquisaCadastro.Location = new System.Drawing.Point(534, 16);
            this.pbPesquisaCadastro.Name = "pbPesquisaCadastro";
            this.pbPesquisaCadastro.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaCadastro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaCadastro.TabIndex = 453;
            this.pbPesquisaCadastro.TabStop = false;
            this.pbPesquisaCadastro.Click += new System.EventHandler(this.pbPesquisaCadastro_Click);
            // 
            // txtChave
            // 
            this.txtChave.EnterMoveNextControl = true;
            this.txtChave.Location = new System.Drawing.Point(3, 16);
            this.txtChave.Name = "txtChave";
            this.txtChave.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChave.Properties.Appearance.Options.UseFont = true;
            this.txtChave.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtChave.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtChave.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtChave.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtChave.Properties.Mask.EditMask = "99/";
            this.txtChave.Properties.MaxLength = 50;
            this.txtChave.Size = new System.Drawing.Size(259, 22);
            this.txtChave.TabIndex = 1;
            this.txtChave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChave_KeyDown_1);
            // 
            // UcTabelaDePrecoPesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtChave);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.labStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDataValidade);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gcTabelasPrecos);
            this.Controls.Add(this.pbPesquisaCadastro);
            this.Name = "UcTabelaDePrecoPesquisa";
            this.Size = new System.Drawing.Size(558, 297);
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTabelasPrecos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataValidade.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataValidade.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChave.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gcTabelasPrecos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataCadastro;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataValidade;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraEditors.DateEdit txtDataValidade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbPesquisaCadastro;
        private DevExpress.XtraEditors.TextEdit txtChave;

    }
}
