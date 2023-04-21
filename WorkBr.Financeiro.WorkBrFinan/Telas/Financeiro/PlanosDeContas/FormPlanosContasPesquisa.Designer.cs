namespace Programax.Easy.View.Telas.Financeiro.PlanosDeContas
{
    partial class FormPlanosContasPesquisa
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
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.btnPesquisaNcm = new System.Windows.Forms.PictureBox();
            this.txtDescricao = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNumeroPlanoContas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNumeroPlanoContasContador = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDescricao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coluanNatureza = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaTipo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPlanoDeContas = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroPlanoDeContas = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumeroPlanoContasContador = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaNcm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPlanoDeContas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPlanoDeContas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPlanoContasContador.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Size = new System.Drawing.Size(983, 40);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.txtNumeroPlanoContasContador);
            this.panelConteudo.Controls.Add(this.labelControl6);
            this.panelConteudo.Controls.Add(this.label2);
            this.panelConteudo.Controls.Add(this.txtNumeroPlanoDeContas);
            this.panelConteudo.Controls.Add(this.gcPlanoDeContas);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.label1);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Controls.Add(this.txtDescricao);
            this.panelConteudo.Controls.Add(this.btnPesquisaNcm);
            this.panelConteudo.Size = new System.Drawing.Size(906, 337);
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
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelecionar.FlatAppearance.BorderSize = 0;
            this.btnSelecionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionar.Image = global::Programax.Easy.View.Properties.Resources.icone_selecionar1;
            this.btnSelecionar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSelecionar.Location = new System.Drawing.Point(0, 0);
            this.btnSelecionar.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(120, 40);
            this.btnSelecionar.TabIndex = 5;
            this.btnSelecionar.Text = " Selecionar";
            this.btnSelecionar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelecionar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // btnPesquisaNcm
            // 
            this.btnPesquisaNcm.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaNcm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaNcm.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaNcm.Location = new System.Drawing.Point(882, 21);
            this.btnPesquisaNcm.Name = "btnPesquisaNcm";
            this.btnPesquisaNcm.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaNcm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaNcm.TabIndex = 632;
            this.btnPesquisaNcm.TabStop = false;
            this.btnPesquisaNcm.Click += new System.EventHandler(this.btnPesquisaNcm_Click);
            // 
            // txtDescricao
            // 
            this.txtDescricao.EnterMoveNextControl = true;
            this.txtDescricao.Location = new System.Drawing.Point(301, 21);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.Properties.Appearance.Options.UseFont = true;
            this.txtDescricao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricao.Properties.Mask.EditMask = "99/";
            this.txtDescricao.Properties.MaxLength = 50;
            this.txtDescricao.Size = new System.Drawing.Size(421, 22);
            this.txtDescricao.TabIndex = 3;
            this.txtDescricao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboTipoPlanoDeContas_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(298, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 630;
            this.label1.Text = "Descrição";
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
            this.colunaNumeroPlanoContas,
            this.colunaNumeroPlanoContasContador,
            this.colunaDescricao,
            this.coluanNatureza,
            this.colunaTipo,
            this.colunaStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcPlanoDeContas;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.ViewCaption = "Planos de Contas";
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
            this.colunaId.Width = 81;
            // 
            // colunaNumeroPlanoContas
            // 
            this.colunaNumeroPlanoContas.AppearanceCell.Options.UseTextOptions = true;
            this.colunaNumeroPlanoContas.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaNumeroPlanoContas.Caption = "Nr. Plano de Contas";
            this.colunaNumeroPlanoContas.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colunaNumeroPlanoContas.FieldName = "NumeroPlanoDeContas";
            this.colunaNumeroPlanoContas.MinWidth = 10;
            this.colunaNumeroPlanoContas.Name = "colunaNumeroPlanoContas";
            this.colunaNumeroPlanoContas.OptionsColumn.AllowEdit = false;
            this.colunaNumeroPlanoContas.OptionsColumn.AllowFocus = false;
            this.colunaNumeroPlanoContas.OptionsFilter.AllowFilter = false;
            this.colunaNumeroPlanoContas.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaNumeroPlanoContas.Visible = true;
            this.colunaNumeroPlanoContas.VisibleIndex = 0;
            this.colunaNumeroPlanoContas.Width = 155;
            // 
            // colunaNumeroPlanoContasContador
            // 
            this.colunaNumeroPlanoContasContador.Caption = "Nr. Plano Contas Contador";
            this.colunaNumeroPlanoContasContador.FieldName = "NumeroPlanoDeContasContador";
            this.colunaNumeroPlanoContasContador.Name = "colunaNumeroPlanoContasContador";
            this.colunaNumeroPlanoContasContador.OptionsColumn.AllowEdit = false;
            this.colunaNumeroPlanoContasContador.OptionsColumn.AllowFocus = false;
            this.colunaNumeroPlanoContasContador.OptionsFilter.AllowFilter = false;
            this.colunaNumeroPlanoContasContador.Visible = true;
            this.colunaNumeroPlanoContasContador.VisibleIndex = 1;
            this.colunaNumeroPlanoContasContador.Width = 162;
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
            this.colunaDescricao.VisibleIndex = 2;
            this.colunaDescricao.Width = 187;
            // 
            // coluanNatureza
            // 
            this.coluanNatureza.Caption = "Natureza";
            this.coluanNatureza.FieldName = "Natureza";
            this.coluanNatureza.Name = "coluanNatureza";
            this.coluanNatureza.OptionsColumn.AllowEdit = false;
            this.coluanNatureza.OptionsColumn.AllowFocus = false;
            this.coluanNatureza.OptionsFilter.AllowFilter = false;
            this.coluanNatureza.Visible = true;
            this.coluanNatureza.VisibleIndex = 3;
            this.coluanNatureza.Width = 102;
            // 
            // colunaTipo
            // 
            this.colunaTipo.Caption = "Tipo";
            this.colunaTipo.FieldName = "Tipo";
            this.colunaTipo.Name = "colunaTipo";
            this.colunaTipo.OptionsColumn.AllowEdit = false;
            this.colunaTipo.OptionsColumn.AllowFocus = false;
            this.colunaTipo.OptionsFilter.AllowFilter = false;
            this.colunaTipo.Visible = true;
            this.colunaTipo.VisibleIndex = 4;
            this.colunaTipo.Width = 110;
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
            this.colunaStatus.VisibleIndex = 5;
            this.colunaStatus.Width = 106;
            // 
            // gcPlanoDeContas
            // 
            this.gcPlanoDeContas.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcPlanoDeContas.Location = new System.Drawing.Point(6, 49);
            this.gcPlanoDeContas.MainView = this.gridView5;
            this.gcPlanoDeContas.Name = "gcPlanoDeContas";
            this.gcPlanoDeContas.Size = new System.Drawing.Size(898, 285);
            this.gcPlanoDeContas.TabIndex = 7;
            this.gcPlanoDeContas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcPlanoDeContas.DoubleClick += new System.EventHandler(this.gcNcms_DoubleClick);
            this.gcPlanoDeContas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcNcms_KeyDown);
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcPlanoDeContas;
            this.gridView2.Name = "gridView2";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSelecionar);
            this.flowLayoutPanel1.Controls.Add(this.btnFechar);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(361, 46);
            this.flowLayoutPanel1.TabIndex = 1047;
            // 
            // cboStatus
            // 
            this.cboStatus.Location = new System.Drawing.Point(728, 21);
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
            this.cboStatus.TabIndex = 4;
            this.cboStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboTipoPlanoDeContas_KeyDown);
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(728, 5);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 636;
            this.labStatus.Text = "Status";
            // 
            // txtNumeroPlanoDeContas
            // 
            this.txtNumeroPlanoDeContas.EditValue = "";
            this.txtNumeroPlanoDeContas.EnterMoveNextControl = true;
            this.txtNumeroPlanoDeContas.Location = new System.Drawing.Point(6, 21);
            this.txtNumeroPlanoDeContas.Name = "txtNumeroPlanoDeContas";
            this.txtNumeroPlanoDeContas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroPlanoDeContas.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroPlanoDeContas.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroPlanoDeContas.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroPlanoDeContas.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumeroPlanoDeContas.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNumeroPlanoDeContas.Properties.Mask.EditMask = "([0-9]{1})(([\\.][0-9]{2})(([\\.][0-9]{3})([\\.][0-9]{4})?)?)?";
            this.txtNumeroPlanoDeContas.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtNumeroPlanoDeContas.Properties.MaxLength = 50;
            this.txtNumeroPlanoDeContas.Size = new System.Drawing.Size(151, 22);
            this.txtNumeroPlanoDeContas.TabIndex = 1;
            this.txtNumeroPlanoDeContas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboTipoPlanoDeContas_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 642;
            this.label2.Text = "Número Plano de Contas";
            // 
            // txtNumeroPlanoContasContador
            // 
            this.txtNumeroPlanoContasContador.EnterMoveNextControl = true;
            this.txtNumeroPlanoContasContador.Location = new System.Drawing.Point(163, 21);
            this.txtNumeroPlanoContasContador.Name = "txtNumeroPlanoContasContador";
            this.txtNumeroPlanoContasContador.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroPlanoContasContador.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroPlanoContasContador.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroPlanoContasContador.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroPlanoContasContador.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumeroPlanoContasContador.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNumeroPlanoContasContador.Properties.Mask.EditMask = "99/";
            this.txtNumeroPlanoContasContador.Properties.MaxLength = 50;
            this.txtNumeroPlanoContasContador.Size = new System.Drawing.Size(132, 22);
            this.txtNumeroPlanoContasContador.TabIndex = 2;
            this.txtNumeroPlanoContasContador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumeroPlanoContasContador_KeyDown);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(163, 5);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(126, 13);
            this.labelControl6.TabIndex = 509;
            this.labelControl6.Text = "Nr. Plano Contas Contador";
            // 
            // FormPlanosContasPesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 448);
            this.MaximizeBox = false;
            this.Name = "FormPlanosContasPesquisa";
            this.NomeDaTela = "Pesquisa de Plano de Contas";
            this.Text = "Pesquisa de Plano de Contas";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaNcm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPlanoDeContas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPlanoDeContas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPlanoContasContador.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.PictureBox btnPesquisaNcm;
        private DevExpress.XtraEditors.TextEdit txtDescricao;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNumeroPlanoContas;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricao;
        private DevExpress.XtraGrid.GridControl gcPlanoDeContas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraGrid.Columns.GridColumn coluanNatureza;
        private DevExpress.XtraGrid.Columns.GridColumn colunaTipo;
        private DevExpress.XtraEditors.TextEdit txtNumeroPlanoDeContas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.TextEdit txtNumeroPlanoContasContador;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNumeroPlanoContasContador;
    }
}