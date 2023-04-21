namespace Programax.Easy.View.Telas.Cadastros.Cidades
{
    partial class FormCidadePesquisa
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
            this.gcCidades = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCodigoIBGE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNomeCidade = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaUf = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pbPesquisaCadastro = new System.Windows.Forms.PictureBox();
            this.cboEstado = new DevExpress.XtraEditors.LookUpEdit();
            this.labEstado = new DevExpress.XtraEditors.LabelControl();
            this.txtNomeCidade = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeCidade.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Size = new System.Drawing.Size(738, 40);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.gcCidades);
            this.panelConteudo.Controls.Add(this.txtNomeCidade);
            this.panelConteudo.Controls.Add(this.labEstado);
            this.panelConteudo.Controls.Add(this.pbPesquisaCadastro);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.cboEstado);
            this.panelConteudo.Size = new System.Drawing.Size(551, 311);
            // 
            // gcCidades
            // 
            this.gcCidades.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcCidades.Location = new System.Drawing.Point(3, 44);
            this.gcCidades.MainView = this.gridView5;
            this.gcCidades.Name = "gcCidades";
            this.gcCidades.Size = new System.Drawing.Size(545, 264);
            this.gcCidades.TabIndex = 3;
            this.gcCidades.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcCidades.DoubleClick += new System.EventHandler(this.gcCidades_DoubleClick);
            this.gcCidades.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcCidades_KeyDown);
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
            this.colunaCodigoIBGE,
            this.colunaNomeCidade,
            this.colunaUf,
            this.colunaStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcCidades;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Cidades";
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
            // colunaCodigoIBGE
            // 
            this.colunaCodigoIBGE.Caption = "Código IBGE";
            this.colunaCodigoIBGE.FieldName = "CodigoIbge";
            this.colunaCodigoIBGE.Name = "colunaCodigoIBGE";
            this.colunaCodigoIBGE.OptionsColumn.AllowEdit = false;
            this.colunaCodigoIBGE.OptionsColumn.AllowFocus = false;
            this.colunaCodigoIBGE.OptionsFilter.AllowFilter = false;
            this.colunaCodigoIBGE.Visible = true;
            this.colunaCodigoIBGE.VisibleIndex = 0;
            // 
            // colunaNomeCidade
            // 
            this.colunaNomeCidade.Caption = "Nome da Cidade";
            this.colunaNomeCidade.FieldName = "NomeCidade";
            this.colunaNomeCidade.Name = "colunaNomeCidade";
            this.colunaNomeCidade.OptionsColumn.AllowEdit = false;
            this.colunaNomeCidade.OptionsColumn.AllowFocus = false;
            this.colunaNomeCidade.OptionsFilter.AllowAutoFilter = false;
            this.colunaNomeCidade.OptionsFilter.AllowFilter = false;
            this.colunaNomeCidade.Visible = true;
            this.colunaNomeCidade.VisibleIndex = 1;
            this.colunaNomeCidade.Width = 188;
            // 
            // colunaUf
            // 
            this.colunaUf.Caption = "UF";
            this.colunaUf.FieldName = "Uf";
            this.colunaUf.Name = "colunaUf";
            this.colunaUf.OptionsColumn.AllowEdit = false;
            this.colunaUf.OptionsColumn.AllowFocus = false;
            this.colunaUf.OptionsFilter.AllowAutoFilter = false;
            this.colunaUf.OptionsFilter.AllowFilter = false;
            this.colunaUf.Visible = true;
            this.colunaUf.VisibleIndex = 2;
            this.colunaUf.Width = 30;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcCidades;
            this.gridView2.Name = "gridView2";
            // 
            // pbPesquisaCadastro
            // 
            this.pbPesquisaCadastro.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaCadastro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaCadastro.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaCadastro.Location = new System.Drawing.Point(526, 17);
            this.pbPesquisaCadastro.Name = "pbPesquisaCadastro";
            this.pbPesquisaCadastro.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaCadastro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaCadastro.TabIndex = 486;
            this.pbPesquisaCadastro.TabStop = false;
            this.pbPesquisaCadastro.Click += new System.EventHandler(this.pbPesquisaCadastro_Click);
            // 
            // cboEstado
            // 
            this.cboEstado.Location = new System.Drawing.Point(256, 18);
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
            this.cboEstado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboEstado_KeyDown);
            // 
            // labEstado
            // 
            this.labEstado.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labEstado.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labEstado.Location = new System.Drawing.Point(256, 4);
            this.labEstado.Name = "labEstado";
            this.labEstado.Size = new System.Drawing.Size(33, 13);
            this.labEstado.TabIndex = 485;
            this.labEstado.Text = "Estado";
            // 
            // txtNomeCidade
            // 
            this.txtNomeCidade.Location = new System.Drawing.Point(3, 18);
            this.txtNomeCidade.Name = "txtNomeCidade";
            this.txtNomeCidade.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeCidade.Properties.Appearance.Options.UseFont = true;
            this.txtNomeCidade.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNomeCidade.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNomeCidade.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNomeCidade.Properties.Mask.EditMask = "99999-999";
            this.txtNomeCidade.Properties.MaxLength = 80;
            this.txtNomeCidade.Size = new System.Drawing.Size(247, 22);
            this.txtNomeCidade.TabIndex = 1;
            this.txtNomeCidade.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomeCidade_KeyDown);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(64, 13);
            this.labelControl2.TabIndex = 491;
            this.labelControl2.Text = "Nome Cidade";
            // 
            // btnFechar
            // 
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnFechar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnFechar.Location = new System.Drawing.Point(129, 3);
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
            this.btnSelecionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionar.Image = global::Programax.Easy.View.Properties.Resources.icone_selecionar1;
            this.btnSelecionar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSelecionar.Location = new System.Drawing.Point(3, 3);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(120, 40);
            this.btnSelecionar.TabIndex = 6;
            this.btnSelecionar.Text = " Selecionar";
            this.btnSelecionar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelecionar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSelecionar);
            this.flowLayoutPanel1.Controls.Add(this.btnFechar);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(428, 39);
            this.flowLayoutPanel1.TabIndex = 438;
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(372, 1);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 645;
            this.labStatus.Text = "Status";
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(372, 18);
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
            this.colunaStatus.VisibleIndex = 3;
            // 
            // FormCidadePesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 422);
            this.MaximizeBox = false;
            this.Name = "FormCidadePesquisa";
            this.NomeDaTela = "Pesquisa de Cidades";
            this.Text = "Pesquisa de Cidades";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeCidade.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcCidades;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaUf;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNomeCidade;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.PictureBox pbPesquisaCadastro;
        private DevExpress.XtraEditors.LookUpEdit cboEstado;
        private DevExpress.XtraEditors.LabelControl labEstado;
        private DevExpress.XtraEditors.TextEdit txtNomeCidade;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCodigoIBGE;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
    }
}