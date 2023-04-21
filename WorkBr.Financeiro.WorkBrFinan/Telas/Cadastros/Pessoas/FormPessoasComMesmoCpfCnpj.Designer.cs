namespace Programax.Easy.View.Telas.Cadastros.Pessoas
{
    partial class FormPessoasComMesmoCpfCnpj
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPessoasComMesmoCpfCnpj));
            this.gcPessoas = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunCpfCnpj = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNomeRazao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNomeFantasia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaUf = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCidade = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnSelecionarParceiro = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gcPessoas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // gcPessoas
            // 
            this.gcPessoas.CausesValidation = false;
            this.gcPessoas.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcPessoas.Location = new System.Drawing.Point(12, 66);
            this.gcPessoas.MainView = this.gridView5;
            this.gcPessoas.Name = "gcPessoas";
            this.gcPessoas.Size = new System.Drawing.Size(1040, 225);
            this.gcPessoas.TabIndex = 472;
            this.gcPessoas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcPessoas.DoubleClick += new System.EventHandler(this.gcPessoas_DoubleClick);
            this.gcPessoas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcPessoas_KeyDown);
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
            this.gridView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaId,
            this.colunCpfCnpj,
            this.colunaNomeRazao,
            this.colunaNomeFantasia,
            this.colunaUf,
            this.colunaCidade,
            this.gridStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcPessoas;
            this.gridView5.GroupPanelText = "Arraste as colunas para agrupar as colunas";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Parceiros";
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
            this.colunaId.Width = 132;
            // 
            // colunCpfCnpj
            // 
            this.colunCpfCnpj.Caption = "Cpf/Cnpj";
            this.colunCpfCnpj.FieldName = "CpfCnpj";
            this.colunCpfCnpj.Name = "colunCpfCnpj";
            this.colunCpfCnpj.OptionsColumn.AllowEdit = false;
            this.colunCpfCnpj.OptionsColumn.AllowFocus = false;
            this.colunCpfCnpj.OptionsFilter.AllowFilter = false;
            this.colunCpfCnpj.Visible = true;
            this.colunCpfCnpj.VisibleIndex = 1;
            this.colunCpfCnpj.Width = 209;
            // 
            // colunaNomeRazao
            // 
            this.colunaNomeRazao.AppearanceCell.Options.UseTextOptions = true;
            this.colunaNomeRazao.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaNomeRazao.Caption = "Nome/Razão Social";
            this.colunaNomeRazao.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaNomeRazao.FieldName = "RazaoSocial";
            this.colunaNomeRazao.MinWidth = 10;
            this.colunaNomeRazao.Name = "colunaNomeRazao";
            this.colunaNomeRazao.OptionsColumn.AllowEdit = false;
            this.colunaNomeRazao.OptionsColumn.AllowFocus = false;
            this.colunaNomeRazao.OptionsFilter.AllowFilter = false;
            this.colunaNomeRazao.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaNomeRazao.Visible = true;
            this.colunaNomeRazao.VisibleIndex = 2;
            this.colunaNomeRazao.Width = 333;
            // 
            // colunaNomeFantasia
            // 
            this.colunaNomeFantasia.Caption = "Nome Fantasia";
            this.colunaNomeFantasia.FieldName = "NomeFantasia";
            this.colunaNomeFantasia.Name = "colunaNomeFantasia";
            this.colunaNomeFantasia.OptionsColumn.AllowEdit = false;
            this.colunaNomeFantasia.OptionsColumn.AllowFocus = false;
            this.colunaNomeFantasia.OptionsFilter.AllowFilter = false;
            this.colunaNomeFantasia.Visible = true;
            this.colunaNomeFantasia.VisibleIndex = 3;
            this.colunaNomeFantasia.Width = 275;
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
            this.colunaUf.VisibleIndex = 4;
            this.colunaUf.Width = 68;
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
            this.colunaCidade.VisibleIndex = 5;
            this.colunaCidade.Width = 144;
            // 
            // gridStatus
            // 
            this.gridStatus.Caption = "Status";
            this.gridStatus.FieldName = "Status";
            this.gridStatus.Name = "gridStatus";
            this.gridStatus.OptionsColumn.AllowEdit = false;
            this.gridStatus.OptionsColumn.AllowFocus = false;
            this.gridStatus.OptionsFilter.AllowAutoFilter = false;
            this.gridStatus.OptionsFilter.AllowFilter = false;
            this.gridStatus.Visible = true;
            this.gridStatus.VisibleIndex = 6;
            this.gridStatus.Width = 117;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcPessoas;
            this.gridView2.Name = "gridView2";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-84, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1167, 10);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10048;
            this.pictureBox1.TabStop = false;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.labelControl8.Location = new System.Drawing.Point(12, 12);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(346, 29);
            this.labelControl8.TabIndex = 10047;
            this.labelControl8.Text = "Parceiros Com Mesmo Cpf/Cnpj";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-84, 307);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1186, 10);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10051;
            this.pictureBox2.TabStop = false;
            // 
            // btnSelecionarParceiro
            // 
            this.btnSelecionarParceiro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelecionarParceiro.FlatAppearance.BorderSize = 0;
            this.btnSelecionarParceiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionarParceiro.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionarParceiro.Image = global::Programax.Easy.View.Properties.Resources.icone_selecionar1;
            this.btnSelecionarParceiro.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSelecionarParceiro.Location = new System.Drawing.Point(12, 323);
            this.btnSelecionarParceiro.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelecionarParceiro.Name = "btnSelecionarParceiro";
            this.btnSelecionarParceiro.Size = new System.Drawing.Size(197, 40);
            this.btnSelecionarParceiro.TabIndex = 10049;
            this.btnSelecionarParceiro.Text = " Selecionar Parceiro";
            this.btnSelecionarParceiro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelecionarParceiro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelecionarParceiro.UseVisualStyleBackColor = true;
            this.btnSelecionarParceiro.Click += new System.EventHandler(this.btnSelecionarParceiro_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(952, 323);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 10050;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // FormPessoasComMesmoCpfCnpj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 371);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnSelecionarParceiro);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.gcPessoas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPessoasComMesmoCpfCnpj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pessoas Com Mesmo Cpf ou Cnpj";
            ((System.ComponentModel.ISupportInitialize)(this.gcPessoas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcPessoas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunCpfCnpj;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNomeRazao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNomeFantasia;
        private DevExpress.XtraGrid.Columns.GridColumn colunaUf;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCidade;
        private DevExpress.XtraGrid.Columns.GridColumn gridStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnSelecionarParceiro;
        private System.Windows.Forms.Button btnSair;
    }
}