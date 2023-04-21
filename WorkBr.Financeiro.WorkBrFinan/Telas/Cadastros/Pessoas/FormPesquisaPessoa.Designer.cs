namespace Programax.Easy.View.Telas.Cadastros.Pessoas
{
    partial class FormPesquisaPessoa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPesquisaPessoa));
            this.gcPessoas = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunCpfCnpj = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNomeRazao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNomeFantasia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnSelecionar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.cbbTipoPesquisa = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pbPesquisaCadastro = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ColunaCpfCnpj = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtChave = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gcPessoas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).BeginInit();
            this.SuspendLayout();
            // 
            // gcPessoas
            // 
            this.gcPessoas.Location = new System.Drawing.Point(2, 47);
            this.gcPessoas.MainView = this.gridView5;
            this.gcPessoas.Name = "gcPessoas";
            this.gcPessoas.Size = new System.Drawing.Size(774, 253);
            this.gcPessoas.TabIndex = 420;
            this.gcPessoas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcPessoas.DoubleClick += new System.EventHandler(this.gcPessoas_DoubleClick);
            this.gcPessoas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcPessoas_KeyDown);
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
            this.colunCpfCnpj,
            this.colunaNomeRazao,
            this.colunaNomeFantasia});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcPessoas;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Clientes";
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
            this.colunaId.Visible = true;
            this.colunaId.VisibleIndex = 0;
            this.colunaId.Width = 66;
            // 
            // colunCpfCnpj
            // 
            this.colunCpfCnpj.Caption = "Cpf/Cnpj";
            this.colunCpfCnpj.FieldName = "CpfCnpj";
            this.colunCpfCnpj.Name = "colunCpfCnpj";
            this.colunCpfCnpj.OptionsColumn.AllowEdit = false;
            this.colunCpfCnpj.OptionsColumn.AllowFocus = false;
            this.colunCpfCnpj.Visible = true;
            this.colunCpfCnpj.VisibleIndex = 1;
            this.colunCpfCnpj.Width = 128;
            // 
            // colunaNomeRazao
            // 
            this.colunaNomeRazao.AppearanceCell.Options.UseTextOptions = true;
            this.colunaNomeRazao.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaNomeRazao.Caption = "Nome/Razão Social";
            this.colunaNomeRazao.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaNomeRazao.FieldName = "Razao";
            this.colunaNomeRazao.MinWidth = 10;
            this.colunaNomeRazao.Name = "colunaNomeRazao";
            this.colunaNomeRazao.OptionsColumn.AllowEdit = false;
            this.colunaNomeRazao.OptionsColumn.AllowFocus = false;
            this.colunaNomeRazao.OptionsFilter.AllowFilter = false;
            this.colunaNomeRazao.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaNomeRazao.Visible = true;
            this.colunaNomeRazao.VisibleIndex = 2;
            this.colunaNomeRazao.Width = 279;
            // 
            // colunaNomeFantasia
            // 
            this.colunaNomeFantasia.Caption = "Nome Fantasia";
            this.colunaNomeFantasia.FieldName = "NomeFantasia";
            this.colunaNomeFantasia.Name = "colunaNomeFantasia";
            this.colunaNomeFantasia.OptionsColumn.AllowEdit = false;
            this.colunaNomeFantasia.OptionsColumn.AllowFocus = false;
            this.colunaNomeFantasia.Visible = true;
            this.colunaNomeFantasia.VisibleIndex = 3;
            this.colunaNomeFantasia.Width = 339;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcPessoas;
            this.gridView2.Name = "gridView2";
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionar.Appearance.Options.UseFont = true;
            this.btnSelecionar.Image = ((System.Drawing.Image)(resources.GetObject("btnSelecionar.Image")));
            this.btnSelecionar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnSelecionar.Location = new System.Drawing.Point(2, 306);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(108, 25);
            this.btnSelecionar.TabIndex = 421;
            this.btnSelecionar.Text = "Selecionar";
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Appearance.Options.UseFont = true;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnCancelar.Location = new System.Drawing.Point(678, 307);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(98, 24);
            this.btnCancelar.TabIndex = 422;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // cbbTipoPesquisa
            // 
            this.cbbTipoPesquisa.FormattingEnabled = true;
            this.cbbTipoPesquisa.Items.AddRange(new object[] {
            "Nome/Razão Social",
            "Nome Fantasia",
            "Cpf",
            "Cnpj"});
            this.cbbTipoPesquisa.Location = new System.Drawing.Point(2, 20);
            this.cbbTipoPesquisa.Name = "cbbTipoPesquisa";
            this.cbbTipoPesquisa.Size = new System.Drawing.Size(168, 21);
            this.cbbTipoPesquisa.TabIndex = 423;
            this.cbbTipoPesquisa.Text = "Nome/Razão Social";
            this.cbbTipoPesquisa.SelectedIndexChanged += new System.EventHandler(this.cbbTipoPesquisa_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 424;
            this.label1.Text = "Tipo da pesquisa";
            // 
            // pbPesquisaCadastro
            // 
            this.pbPesquisaCadastro.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaCadastro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaCadastro.Image = ((System.Drawing.Image)(resources.GetObject("pbPesquisaCadastro.Image")));
            this.pbPesquisaCadastro.Location = new System.Drawing.Point(751, 20);
            this.pbPesquisaCadastro.Name = "pbPesquisaCadastro";
            this.pbPesquisaCadastro.Size = new System.Drawing.Size(25, 20);
            this.pbPesquisaCadastro.TabIndex = 426;
            this.pbPesquisaCadastro.TabStop = false;
            this.pbPesquisaCadastro.Click += new System.EventHandler(this.pbPesquisaCadastro_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 427;
            this.label2.Text = "Chave";
            // 
            // ColunaCpfCnpj
            // 
            this.ColunaCpfCnpj.Caption = "Cpf/Cnpj";
            this.ColunaCpfCnpj.Name = "ColunaCpfCnpj";
            this.ColunaCpfCnpj.Visible = true;
            this.ColunaCpfCnpj.VisibleIndex = 3;
            // 
            // txtChave
            // 
            this.txtChave.Location = new System.Drawing.Point(180, 21);
            this.txtChave.Name = "txtChave";
            this.txtChave.Size = new System.Drawing.Size(565, 20);
            this.txtChave.TabIndex = 428;
            this.txtChave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChave_KeyDown);
            // 
            // FormPesquisaPessoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 343);
            this.Controls.Add(this.txtChave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbPesquisaCadastro);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbTipoPesquisa);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSelecionar);
            this.Controls.Add(this.gcPessoas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPesquisaPessoa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisa de cliente";
            ((System.ComponentModel.ISupportInitialize)(this.gcPessoas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcPessoas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNomeRazao;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SimpleButton btnSelecionar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private System.Windows.Forms.ComboBox cbbTipoPesquisa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbPesquisaCadastro;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNomeFantasia;
        private DevExpress.XtraGrid.Columns.GridColumn colunCpfCnpj;
        private DevExpress.XtraGrid.Columns.GridColumn ColunaCpfCnpj;
        private System.Windows.Forms.MaskedTextBox txtChave;
    }
}