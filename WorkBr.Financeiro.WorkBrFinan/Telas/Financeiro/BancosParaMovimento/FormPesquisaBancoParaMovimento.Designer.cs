namespace Programax.Easy.View.Telas.Financeiro.BancosParaMovimento
{
    partial class FormPesquisaBancoParaMovimento
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
            this.txtNomeCaixa = new DevExpress.XtraEditors.TextEdit();
            this.lblDescricao = new DevExpress.XtraEditors.LabelControl();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.pbPesquisaCadastro = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.gcBancos = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNomeBanco = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaBancoPadrão = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeCaixa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBancos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Size = new System.Drawing.Size(490, 40);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.lblDescricao);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Controls.Add(this.gcBancos);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.txtNomeCaixa);
            this.panelConteudo.Controls.Add(this.pbPesquisaCadastro);
            this.panelConteudo.Size = new System.Drawing.Size(543, 228);
            // 
            // cboStatus
            // 
            this.cboStatus.Location = new System.Drawing.Point(331, 22);
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
            this.cboStatus.Size = new System.Drawing.Size(181, 22);
            this.cboStatus.TabIndex = 3;
            this.cboStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboStatus_KeyDown);
            // 
            // txtNomeCaixa
            // 
            this.txtNomeCaixa.EnterMoveNextControl = true;
            this.txtNomeCaixa.Location = new System.Drawing.Point(3, 22);
            this.txtNomeCaixa.Name = "txtNomeCaixa";
            this.txtNomeCaixa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeCaixa.Properties.Appearance.Options.UseFont = true;
            this.txtNomeCaixa.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNomeCaixa.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNomeCaixa.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeCaixa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNomeCaixa.Properties.MaxLength = 50;
            this.txtNomeCaixa.Size = new System.Drawing.Size(326, 22);
            this.txtNomeCaixa.TabIndex = 1;
            this.txtNomeCaixa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomeCaixa_KeyDown);
            // 
            // lblDescricao
            // 
            this.lblDescricao.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricao.Location = new System.Drawing.Point(3, 5);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(97, 13);
            this.lblDescricao.TabIndex = 468;
            this.lblDescricao.Text = "Descrição do Banco";
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(402, 5);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 466;
            this.labStatus.Text = "Status";
            // 
            // pbPesquisaCadastro
            // 
            this.pbPesquisaCadastro.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaCadastro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaCadastro.ErrorImage = null;
            this.pbPesquisaCadastro.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaCadastro.Location = new System.Drawing.Point(517, 22);
            this.pbPesquisaCadastro.Name = "pbPesquisaCadastro";
            this.pbPesquisaCadastro.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaCadastro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaCadastro.TabIndex = 651;
            this.pbPesquisaCadastro.TabStop = false;
            this.pbPesquisaCadastro.Click += new System.EventHandler(this.pbPesquisaPessoa_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSelecionar);
            this.flowLayoutPanel1.Controls.Add(this.btnFechar);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(278, 47);
            this.flowLayoutPanel1.TabIndex = 1047;
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelecionar.FlatAppearance.BorderSize = 0;
            this.btnSelecionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionar.Image = global::Programax.Easy.View.Properties.Resources.icone_atualizar;
            this.btnSelecionar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSelecionar.Location = new System.Drawing.Point(0, 0);
            this.btnSelecionar.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(120, 40);
            this.btnSelecionar.TabIndex = 435;
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
            this.btnFechar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // gcBancos
            // 
            this.gcBancos.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcBancos.Location = new System.Drawing.Point(3, 50);
            this.gcBancos.MainView = this.gridView5;
            this.gcBancos.Name = "gcBancos";
            this.gcBancos.Size = new System.Drawing.Size(536, 172);
            this.gcBancos.TabIndex = 5;
            this.gcBancos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcBancos.DoubleClick += new System.EventHandler(this.gcCaixas_DoubleClick);
            this.gcBancos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcCaixas_EditorKeyDown);
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
            this.colunaNomeBanco,
            this.colunaBancoPadrão,
            this.colunaStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcBancos;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Bancos";
            // 
            // colunaId
            // 
            this.colunaId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.Caption = "Nr. Caixa";
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
            this.colunaId.Width = 81;
            // 
            // colunaNomeBanco
            // 
            this.colunaNomeBanco.Caption = "Descrição do Banco";
            this.colunaNomeBanco.FieldName = "NomeBanco";
            this.colunaNomeBanco.Name = "colunaNomeBanco";
            this.colunaNomeBanco.OptionsColumn.AllowEdit = false;
            this.colunaNomeBanco.OptionsColumn.AllowFocus = false;
            this.colunaNomeBanco.Visible = true;
            this.colunaNomeBanco.VisibleIndex = 1;
            this.colunaNomeBanco.Width = 280;
            // 
            // colunaBancoPadrão
            // 
            this.colunaBancoPadrão.Caption = "Banco Padrão?";
            this.colunaBancoPadrão.FieldName = "BancoPadrao";
            this.colunaBancoPadrão.Name = "colunaBancoPadrão";
            this.colunaBancoPadrão.OptionsColumn.AllowEdit = false;
            this.colunaBancoPadrão.OptionsColumn.AllowFocus = false;
            this.colunaBancoPadrão.OptionsFilter.AllowFilter = false;
            this.colunaBancoPadrão.Visible = true;
            this.colunaBancoPadrão.VisibleIndex = 2;
            this.colunaBancoPadrão.Width = 110;
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
            this.colunaStatus.VisibleIndex = 3;
            this.colunaStatus.Width = 63;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcBancos;
            this.gridView2.Name = "gridView2";
            // 
            // FormPesquisaBancoParaMovimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 339);
            this.MaximizeBox = false;
            this.Name = "FormPesquisaBancoParaMovimento";
            this.NomeDaTela = "Pesquisa de Bancos Para Movimento";
            this.Text = "Pesquisa de Bancos Para Movimento";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeCaixa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBancos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.TextEdit txtNomeCaixa;
        private DevExpress.XtraEditors.LabelControl lblDescricao;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private System.Windows.Forms.PictureBox pbPesquisaCadastro;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.Button btnFechar;
        private DevExpress.XtraGrid.GridControl gcBancos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNomeBanco;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraGrid.Columns.GridColumn colunaBancoPadrão;
    }
}