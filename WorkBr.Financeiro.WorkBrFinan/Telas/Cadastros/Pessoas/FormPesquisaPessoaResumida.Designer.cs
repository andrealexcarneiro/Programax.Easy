namespace Programax.Easy.View.Telas.Cadastros.Pessoas
{
    partial class FormPesquisaPessoaResumida
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
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.pbPesquisaCadastro = new System.Windows.Forms.PictureBox();
            this.txtNomeFantasia = new DevExpress.XtraEditors.TextEdit();
            this.lblDescricao = new DevExpress.XtraEditors.LabelControl();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcPessoas = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunCpfCnpj = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNomeRazao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNomeFantasia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaUf = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCidade = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboTipoCadastroParceiro = new DevExpress.XtraEditors.LookUpEdit();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeFantasia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPessoas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoCadastroParceiro.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.cboTipoCadastroParceiro);
            this.panelConteudo.Controls.Add(this.gcPessoas);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.pbPesquisaCadastro);
            this.panelConteudo.Controls.Add(this.lblDescricao);
            this.panelConteudo.Controls.Add(this.txtNomeFantasia);
            this.panelConteudo.Size = new System.Drawing.Size(864, 300);
            // 
            // cboStatus
            // 
            this.cboStatus.Location = new System.Drawing.Point(686, 17);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboStatus.Properties.DropDownRows = 3;
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboStatus.Size = new System.Drawing.Size(148, 20);
            this.cboStatus.TabIndex = 4;
            this.cboStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ElementoPesquisar_KeyDown);
            // 
            // pbPesquisaCadastro
            // 
            this.pbPesquisaCadastro.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaCadastro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaCadastro.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaCadastro.Location = new System.Drawing.Point(840, 15);
            this.pbPesquisaCadastro.Name = "pbPesquisaCadastro";
            this.pbPesquisaCadastro.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaCadastro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaCadastro.TabIndex = 489;
            this.pbPesquisaCadastro.TabStop = false;
            this.pbPesquisaCadastro.Click += new System.EventHandler(this.pbPesquisaCadastro_Click);
            // 
            // txtNomeFantasia
            // 
            this.txtNomeFantasia.EnterMoveNextControl = true;
            this.txtNomeFantasia.Location = new System.Drawing.Point(3, 17);
            this.txtNomeFantasia.Name = "txtNomeFantasia";
            this.txtNomeFantasia.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNomeFantasia.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNomeFantasia.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeFantasia.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNomeFantasia.Properties.MaxLength = 80;
            this.txtNomeFantasia.Size = new System.Drawing.Size(479, 20);
            this.txtNomeFantasia.TabIndex = 2;
            this.txtNomeFantasia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ElementoPesquisar_KeyDown);
            // 
            // lblDescricao
            // 
            this.lblDescricao.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricao.Location = new System.Drawing.Point(3, 2);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(71, 13);
            this.lblDescricao.TabIndex = 484;
            this.lblDescricao.Text = "Nome Fantasia";
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(686, 2);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 480;
            this.labStatus.Text = "Status";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcPessoas;
            this.gridView2.Name = "gridView2";
            // 
            // gcPessoas
            // 
            this.gcPessoas.CausesValidation = false;
            this.gcPessoas.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcPessoas.Location = new System.Drawing.Point(3, 43);
            this.gcPessoas.MainView = this.gridView5;
            this.gcPessoas.Name = "gcPessoas";
            this.gcPessoas.Size = new System.Drawing.Size(859, 254);
            this.gcPessoas.TabIndex = 5;
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
            this.colunaId.Width = 45;
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
            this.colunCpfCnpj.Width = 61;
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
            this.colunaNomeRazao.Width = 97;
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
            this.colunaNomeFantasia.Width = 80;
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
            this.colunaUf.Width = 20;
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
            this.colunaCidade.Width = 42;
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
            this.gridStatus.Width = 32;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(488, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(108, 13);
            this.labelControl2.TabIndex = 492;
            this.labelControl2.Text = "Tipo Cadastro Parceiro";
            // 
            // cboTipoCadastroParceiro
            // 
            this.cboTipoCadastroParceiro.EnterMoveNextControl = true;
            this.cboTipoCadastroParceiro.Location = new System.Drawing.Point(488, 17);
            this.cboTipoCadastroParceiro.Name = "cboTipoCadastroParceiro";
            this.cboTipoCadastroParceiro.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTipoCadastroParceiro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboTipoCadastroParceiro.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboTipoCadastroParceiro.Properties.DropDownRows = 4;
            this.cboTipoCadastroParceiro.Properties.NullText = "";
            this.cboTipoCadastroParceiro.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboTipoCadastroParceiro.Size = new System.Drawing.Size(192, 20);
            this.cboTipoCadastroParceiro.TabIndex = 3;
            this.cboTipoCadastroParceiro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ElementoPesquisar_KeyDown);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnEditar);
            this.flowLayoutPanel1.Controls.Add(this.btnFechar);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(810, 41);
            this.flowLayoutPanel1.TabIndex = 439;
            // 
            // btnEditar
            // 
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.Image = global::Programax.Easy.View.Properties.Resources.icone_selecionar1;
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnEditar.Location = new System.Drawing.Point(0, 0);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(0);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(120, 40);
            this.btnEditar.TabIndex = 435;
            this.btnEditar.Text = " Selecionar";
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
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
            this.btnFechar.TabIndex = 437;
            this.btnFechar.Text = " Sair";
            this.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // FormPesquisaPessoaResumida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 411);
            this.Name = "FormPesquisaPessoaResumida";
            this.NomeDaTela = "Pesquisa de Parceiros";
            this.Text = "Pesquisa de Parceiros";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeFantasia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPessoas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoCadastroParceiro.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.LookUpEdit cboStatus;
        private System.Windows.Forms.PictureBox pbPesquisaCadastro;
        private DevExpress.XtraEditors.TextEdit txtNomeFantasia;
        private DevExpress.XtraEditors.LabelControl lblDescricao;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gcPessoas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunCpfCnpj;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNomeRazao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNomeFantasia;
        private DevExpress.XtraGrid.Columns.GridColumn colunaUf;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCidade;
        private DevExpress.XtraGrid.Columns.GridColumn gridStatus;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.LookUpEdit cboTipoCadastroParceiro;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnFechar;
    }
}