namespace Programax.Easy.View.Telas.Cadastros.Enderecos
{
    partial class UcEnderecoPesquisa
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
            this.gcEnderecos = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCep = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaUf = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCidade = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaBairro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaRua = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pbPesquisaCadastro = new System.Windows.Forms.PictureBox();
            this.cboCidade = new DevExpress.XtraEditors.LookUpEdit();
            this.cboEstado = new DevExpress.XtraEditors.LookUpEdit();
            this.labCidade = new DevExpress.XtraEditors.LabelControl();
            this.labEstado = new DevExpress.XtraEditors.LabelControl();
            this.txtCep = new DevExpress.XtraEditors.TextEdit();
            this.lblDescricao = new DevExpress.XtraEditors.LabelControl();
            this.txtBairro = new DevExpress.XtraEditors.TextEdit();
            this.labBairro = new DevExpress.XtraEditors.LabelControl();
            this.txtRua = new DevExpress.XtraEditors.TextEdit();
            this.labRuaEndereco = new DevExpress.XtraEditors.LabelControl();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcEnderecos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCidade.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBairro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRua.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcEnderecos
            // 
            this.gcEnderecos.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcEnderecos.Location = new System.Drawing.Point(0, 42);
            this.gcEnderecos.MainView = this.gridView5;
            this.gcEnderecos.Name = "gcEnderecos";
            this.gcEnderecos.Size = new System.Drawing.Size(919, 263);
            this.gcEnderecos.TabIndex = 481;
            this.gcEnderecos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcEnderecos.DoubleClick += new System.EventHandler(this.gcEnderecos_DoubleClick);
            this.gcEnderecos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcEnderecos_KeyDown);
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
            this.colunaCep,
            this.colunaUf,
            this.colunaCidade,
            this.colunaBairro,
            this.colunaRua,
            this.colunaStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcEnderecos;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Endereços";
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
            this.colunaId.Width = 51;
            // 
            // colunaCep
            // 
            this.colunaCep.Caption = "CEP";
            this.colunaCep.FieldName = "CEP";
            this.colunaCep.Name = "colunaCep";
            this.colunaCep.OptionsColumn.AllowEdit = false;
            this.colunaCep.OptionsColumn.AllowFocus = false;
            this.colunaCep.Visible = true;
            this.colunaCep.VisibleIndex = 0;
            this.colunaCep.Width = 82;
            // 
            // colunaUf
            // 
            this.colunaUf.Caption = "UF";
            this.colunaUf.FieldName = "UF";
            this.colunaUf.Name = "colunaUf";
            this.colunaUf.OptionsColumn.AllowEdit = false;
            this.colunaUf.OptionsColumn.AllowFocus = false;
            this.colunaUf.OptionsFilter.AllowAutoFilter = false;
            this.colunaUf.OptionsFilter.AllowFilter = false;
            this.colunaUf.Visible = true;
            this.colunaUf.VisibleIndex = 1;
            this.colunaUf.Width = 30;
            // 
            // colunaCidade
            // 
            this.colunaCidade.Caption = "Cidade";
            this.colunaCidade.FieldName = "Cidade";
            this.colunaCidade.Name = "colunaCidade";
            this.colunaCidade.OptionsColumn.AllowEdit = false;
            this.colunaCidade.OptionsColumn.AllowFocus = false;
            this.colunaCidade.OptionsFilter.AllowAutoFilter = false;
            this.colunaCidade.OptionsFilter.AllowFilter = false;
            this.colunaCidade.Visible = true;
            this.colunaCidade.VisibleIndex = 2;
            this.colunaCidade.Width = 188;
            // 
            // colunaBairro
            // 
            this.colunaBairro.AppearanceCell.Options.UseTextOptions = true;
            this.colunaBairro.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaBairro.Caption = "Bairro";
            this.colunaBairro.FieldName = "Bairro";
            this.colunaBairro.MinWidth = 10;
            this.colunaBairro.Name = "colunaBairro";
            this.colunaBairro.OptionsColumn.AllowEdit = false;
            this.colunaBairro.OptionsColumn.AllowFocus = false;
            this.colunaBairro.OptionsFilter.AllowFilter = false;
            this.colunaBairro.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaBairro.Visible = true;
            this.colunaBairro.VisibleIndex = 3;
            this.colunaBairro.Width = 203;
            // 
            // colunaRua
            // 
            this.colunaRua.Caption = "Rua";
            this.colunaRua.FieldName = "Rua";
            this.colunaRua.Name = "colunaRua";
            this.colunaRua.OptionsColumn.AllowEdit = false;
            this.colunaRua.OptionsColumn.AllowFocus = false;
            this.colunaRua.OptionsFilter.AllowAutoFilter = false;
            this.colunaRua.OptionsFilter.AllowFilter = false;
            this.colunaRua.Visible = true;
            this.colunaRua.VisibleIndex = 4;
            this.colunaRua.Width = 151;
            // 
            // colunaStatus
            // 
            this.colunaStatus.Caption = "Status";
            this.colunaStatus.FieldName = "Status";
            this.colunaStatus.Name = "colunaStatus";
            this.colunaStatus.OptionsColumn.AllowEdit = false;
            this.colunaStatus.OptionsColumn.AllowFocus = false;
            this.colunaStatus.OptionsFilter.AllowAutoFilter = false;
            this.colunaStatus.OptionsFilter.AllowFilter = false;
            this.colunaStatus.Visible = true;
            this.colunaStatus.VisibleIndex = 5;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcEnderecos;
            this.gridView2.Name = "gridView2";
            // 
            // pbPesquisaCadastro
            // 
            this.pbPesquisaCadastro.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaCadastro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaCadastro.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaCadastro.Location = new System.Drawing.Point(897, 14);
            this.pbPesquisaCadastro.Name = "pbPesquisaCadastro";
            this.pbPesquisaCadastro.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaCadastro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaCadastro.TabIndex = 480;
            this.pbPesquisaCadastro.TabStop = false;
            this.pbPesquisaCadastro.Click += new System.EventHandler(this.pbPesquisaCadastro_Click);
            // 
            // cboCidade
            // 
            this.cboCidade.EnterMoveNextControl = true;
            this.cboCidade.Location = new System.Drawing.Point(201, 14);
            this.cboCidade.Name = "cboCidade";
            this.cboCidade.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCidade.Properties.Appearance.Options.UseFont = true;
            this.cboCidade.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCidade.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboCidade.Properties.NullText = "";
            this.cboCidade.Size = new System.Drawing.Size(150, 22);
            this.cboCidade.TabIndex = 3;
            // 
            // cboEstado
            // 
            this.cboEstado.EnterMoveNextControl = true;
            this.cboEstado.Location = new System.Drawing.Point(85, 14);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEstado.Properties.Appearance.Options.UseFont = true;
            this.cboEstado.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEstado.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UF", 5, "UF"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nome", "Nome")});
            this.cboEstado.Properties.NullText = "";
            this.cboEstado.Size = new System.Drawing.Size(110, 22);
            this.cboEstado.TabIndex = 2;
            this.cboEstado.EditValueChanged += new System.EventHandler(this.cboEstado_EditValueChanged);
            // 
            // labCidade
            // 
            this.labCidade.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCidade.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labCidade.Location = new System.Drawing.Point(201, 0);
            this.labCidade.Name = "labCidade";
            this.labCidade.Size = new System.Drawing.Size(33, 13);
            this.labCidade.TabIndex = 479;
            this.labCidade.Text = "Cidade";
            // 
            // labEstado
            // 
            this.labEstado.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labEstado.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labEstado.Location = new System.Drawing.Point(84, 0);
            this.labEstado.Name = "labEstado";
            this.labEstado.Size = new System.Drawing.Size(33, 13);
            this.labEstado.TabIndex = 478;
            this.labEstado.Text = "Estado";
            // 
            // txtCep
            // 
            this.txtCep.EnterMoveNextControl = true;
            this.txtCep.Location = new System.Drawing.Point(0, 14);
            this.txtCep.Name = "txtCep";
            this.txtCep.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCep.Properties.Appearance.Options.UseFont = true;
            this.txtCep.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCep.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtCep.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtCep.Properties.Mask.EditMask = "99999-999";
            this.txtCep.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.txtCep.Properties.MaxLength = 80;
            this.txtCep.Size = new System.Drawing.Size(79, 22);
            this.txtCep.TabIndex = 1;
            this.txtCep.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCep_KeyDown);
            // 
            // lblDescricao
            // 
            this.lblDescricao.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricao.Location = new System.Drawing.Point(0, 0);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(21, 13);
            this.lblDescricao.TabIndex = 475;
            this.lblDescricao.Text = "CEP";
            // 
            // txtBairro
            // 
            this.txtBairro.EnterMoveNextControl = true;
            this.txtBairro.Location = new System.Drawing.Point(357, 14);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBairro.Properties.Appearance.Options.UseFont = true;
            this.txtBairro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtBairro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtBairro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtBairro.Properties.Mask.EditMask = "99/";
            this.txtBairro.Properties.MaxLength = 30;
            this.txtBairro.Size = new System.Drawing.Size(187, 22);
            this.txtBairro.TabIndex = 4;
            // 
            // labBairro
            // 
            this.labBairro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labBairro.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labBairro.Location = new System.Drawing.Point(357, 0);
            this.labBairro.Name = "labBairro";
            this.labBairro.Size = new System.Drawing.Size(27, 13);
            this.labBairro.TabIndex = 483;
            this.labBairro.Text = "Bairro";
            // 
            // txtRua
            // 
            this.txtRua.EnterMoveNextControl = true;
            this.txtRua.Location = new System.Drawing.Point(550, 14);
            this.txtRua.Name = "txtRua";
            this.txtRua.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRua.Properties.Appearance.Options.UseFont = true;
            this.txtRua.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRua.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtRua.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtRua.Properties.Mask.EditMask = "99/";
            this.txtRua.Properties.MaxLength = 80;
            this.txtRua.Size = new System.Drawing.Size(187, 22);
            this.txtRua.TabIndex = 5;
            this.txtRua.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRua_KeyDown);
            // 
            // labRuaEndereco
            // 
            this.labRuaEndereco.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labRuaEndereco.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labRuaEndereco.Location = new System.Drawing.Point(550, 0);
            this.labRuaEndereco.Name = "labRuaEndereco";
            this.labRuaEndereco.Size = new System.Drawing.Size(20, 13);
            this.labRuaEndereco.TabIndex = 485;
            this.labRuaEndereco.Text = "Rua";
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(743, 0);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 645;
            this.labStatus.Text = "Status";
            // 
            // cboStatus
            // 
            this.cboStatus.Location = new System.Drawing.Point(743, 14);
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
            this.cboStatus.TabIndex = 644;
            // 
            // UcEnderecoPesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labStatus);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.txtRua);
            this.Controls.Add(this.labRuaEndereco);
            this.Controls.Add(this.txtBairro);
            this.Controls.Add(this.labBairro);
            this.Controls.Add(this.gcEnderecos);
            this.Controls.Add(this.pbPesquisaCadastro);
            this.Controls.Add(this.cboCidade);
            this.Controls.Add(this.cboEstado);
            this.Controls.Add(this.labCidade);
            this.Controls.Add(this.labEstado);
            this.Controls.Add(this.txtCep);
            this.Controls.Add(this.lblDescricao);
            this.Name = "UcEnderecoPesquisa";
            this.Size = new System.Drawing.Size(919, 307);
            this.Load += new System.EventHandler(this.UcEnderecoPesquisa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcEnderecos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCidade.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBairro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRua.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcEnderecos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCep;
        private DevExpress.XtraGrid.Columns.GridColumn colunaUf;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCidade;
        private DevExpress.XtraGrid.Columns.GridColumn colunaBairro;
        private DevExpress.XtraGrid.Columns.GridColumn colunaRua;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.PictureBox pbPesquisaCadastro;
        private DevExpress.XtraEditors.LookUpEdit cboCidade;
        private DevExpress.XtraEditors.LookUpEdit cboEstado;
        private DevExpress.XtraEditors.LabelControl labCidade;
        private DevExpress.XtraEditors.LabelControl labEstado;
        private DevExpress.XtraEditors.TextEdit txtCep;
        private DevExpress.XtraEditors.LabelControl lblDescricao;
        private DevExpress.XtraEditors.TextEdit txtBairro;
        private DevExpress.XtraEditors.LabelControl labBairro;
        private DevExpress.XtraEditors.TextEdit txtRua;
        private DevExpress.XtraEditors.LabelControl labRuaEndereco;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        public DevExpress.XtraEditors.LookUpEdit cboStatus;
    }
}
