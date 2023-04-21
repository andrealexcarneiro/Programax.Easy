namespace Programax.Easy.View.Telas.Financeiro.Agencias
{
    partial class FormAgenciaPesquisa
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
            this.gcAgencias = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaBanco = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaAgencia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNumero = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDigito = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.btnPesquisaNcm = new System.Windows.Forms.PictureBox();
            this.txtNomeAgencia = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBancos = new DevExpress.XtraEditors.LookUpEdit();
            this.labBanco = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAgencias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaNcm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeAgencia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBancos.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Size = new System.Drawing.Size(863, 40);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.labBanco);
            this.panelConteudo.Controls.Add(this.cboBancos);
            this.panelConteudo.Controls.Add(this.label1);
            this.panelConteudo.Controls.Add(this.gcAgencias);
            this.panelConteudo.Controls.Add(this.txtNomeAgencia);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.btnPesquisaNcm);
            this.panelConteudo.Size = new System.Drawing.Size(664, 269);
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
            this.colunaStatus.VisibleIndex = 4;
            this.colunaStatus.Width = 82;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcAgencias;
            this.gridView2.Name = "gridView2";
            // 
            // gcAgencias
            // 
            this.gcAgencias.Location = new System.Drawing.Point(3, 49);
            this.gcAgencias.MainView = this.gridView5;
            this.gcAgencias.Name = "gcAgencias";
            this.gcAgencias.Size = new System.Drawing.Size(656, 213);
            this.gcAgencias.TabIndex = 4;
            this.gcAgencias.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcAgencias.DoubleClick += new System.EventHandler(this.gcNcms_DoubleClick);
            this.gcAgencias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcNcms_KeyDown);
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
            this.colunaBanco,
            this.colunaAgencia,
            this.colunaNumero,
            this.colunaDigito,
            this.colunaStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcAgencias;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Agências";
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
            this.colunaId.Width = 48;
            // 
            // colunaBanco
            // 
            this.colunaBanco.Caption = "Banco";
            this.colunaBanco.FieldName = "Banco";
            this.colunaBanco.Name = "colunaBanco";
            this.colunaBanco.OptionsColumn.AllowEdit = false;
            this.colunaBanco.OptionsColumn.AllowFocus = false;
            this.colunaBanco.OptionsFilter.AllowFilter = false;
            this.colunaBanco.Visible = true;
            this.colunaBanco.VisibleIndex = 0;
            this.colunaBanco.Width = 159;
            // 
            // colunaAgencia
            // 
            this.colunaAgencia.Caption = "Nome Agência";
            this.colunaAgencia.FieldName = "NomeAgencia";
            this.colunaAgencia.Name = "colunaAgencia";
            this.colunaAgencia.OptionsColumn.AllowEdit = false;
            this.colunaAgencia.OptionsColumn.AllowFocus = false;
            this.colunaAgencia.OptionsFilter.AllowFilter = false;
            this.colunaAgencia.Visible = true;
            this.colunaAgencia.VisibleIndex = 1;
            this.colunaAgencia.Width = 225;
            // 
            // colunaNumero
            // 
            this.colunaNumero.Caption = "Número Agência";
            this.colunaNumero.FieldName = "NumeroAgencia";
            this.colunaNumero.Name = "colunaNumero";
            this.colunaNumero.OptionsColumn.AllowEdit = false;
            this.colunaNumero.OptionsColumn.AllowFocus = false;
            this.colunaNumero.OptionsFilter.AllowFilter = false;
            this.colunaNumero.Visible = true;
            this.colunaNumero.VisibleIndex = 2;
            this.colunaNumero.Width = 113;
            // 
            // colunaDigito
            // 
            this.colunaDigito.Caption = "Dígito Agência";
            this.colunaDigito.FieldName = "DigitoAgencia";
            this.colunaDigito.Name = "colunaDigito";
            this.colunaDigito.OptionsColumn.AllowEdit = false;
            this.colunaDigito.OptionsColumn.AllowFocus = false;
            this.colunaDigito.OptionsFilter.AllowFilter = false;
            this.colunaDigito.Visible = true;
            this.colunaDigito.VisibleIndex = 3;
            this.colunaDigito.Width = 85;
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(478, 21);
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
            this.cboStatus.TabIndex = 3;
            this.cboStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboStatus_KeyDown);
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
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(478, 4);
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
            this.btnPesquisaNcm.Location = new System.Drawing.Point(632, 20);
            this.btnPesquisaNcm.Name = "btnPesquisaNcm";
            this.btnPesquisaNcm.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaNcm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaNcm.TabIndex = 640;
            this.btnPesquisaNcm.TabStop = false;
            this.btnPesquisaNcm.Click += new System.EventHandler(this.btnPesquisaNcm_Click);
            // 
            // txtNomeAgencia
            // 
            this.txtNomeAgencia.Location = new System.Drawing.Point(235, 21);
            this.txtNomeAgencia.Name = "txtNomeAgencia";
            this.txtNomeAgencia.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeAgencia.Properties.Appearance.Options.UseFont = true;
            this.txtNomeAgencia.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNomeAgencia.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNomeAgencia.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeAgencia.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNomeAgencia.Properties.Mask.EditMask = "99/";
            this.txtNomeAgencia.Properties.MaxLength = 10;
            this.txtNomeAgencia.Size = new System.Drawing.Size(237, 22);
            this.txtNomeAgencia.TabIndex = 2;
            this.txtNomeAgencia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescricao_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(232, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 639;
            this.label1.Text = "Nome Agência";
            // 
            // cboBancos
            // 
            this.cboBancos.Enabled = false;
            this.cboBancos.EnterMoveNextControl = true;
            this.cboBancos.Location = new System.Drawing.Point(3, 21);
            this.cboBancos.Name = "cboBancos";
            this.cboBancos.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBancos.Properties.Appearance.Options.UseFont = true;
            this.cboBancos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboBancos.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboBancos.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Codigo", 6, "Codigo"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboBancos.Properties.NullText = "";
            this.cboBancos.Size = new System.Drawing.Size(226, 22);
            this.cboBancos.TabIndex = 1;
            // 
            // labBanco
            // 
            this.labBanco.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labBanco.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labBanco.Location = new System.Drawing.Point(3, 4);
            this.labBanco.Name = "labBanco";
            this.labBanco.Size = new System.Drawing.Size(31, 13);
            this.labBanco.TabIndex = 645;
            this.labBanco.Text = "Banco";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSelecionar);
            this.flowLayoutPanel1.Controls.Add(this.btnFechar);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(544, 48);
            this.flowLayoutPanel1.TabIndex = 1049;
            // 
            // FormAgenciaPesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 380);
            this.MaximizeBox = false;
            this.Name = "FormAgenciaPesquisa";
            this.NomeDaTela = "Pesquisa de Agências";
            this.Text = "Pesquisa de Agências";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAgencias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaNcm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeAgencia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBancos.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gcAgencias;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaBanco;
        private DevExpress.XtraGrid.Columns.GridColumn colunaAgencia;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private System.Windows.Forms.PictureBox btnPesquisaNcm;
        private DevExpress.XtraEditors.TextEdit txtNomeAgencia;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNumero;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDigito;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnSelecionar;
        private DevExpress.XtraEditors.LookUpEdit cboBancos;
        private DevExpress.XtraEditors.LabelControl labBanco;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}