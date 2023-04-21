namespace Programax.Easy.View.Telas.Fiscal.Cests
{
    partial class FormCestPesquisa
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
            this.gcCests = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaCEST = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDescricao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtCodigoCest = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescricaoCest = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPesquisaCest = new System.Windows.Forms.PictureBox();
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoCest.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoCest.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaCest)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Size = new System.Drawing.Size(853, 40);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.label1);
            this.panelConteudo.Controls.Add(this.gcCests);
            this.panelConteudo.Controls.Add(this.txtCodigoCest);
            this.panelConteudo.Controls.Add(this.btnPesquisaCest);
            this.panelConteudo.Controls.Add(this.label3);
            this.panelConteudo.Controls.Add(this.txtDescricaoCest);
            this.panelConteudo.Size = new System.Drawing.Size(662, 282);
            // 
            // gcCests
            // 
            this.gcCests.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcCests.Location = new System.Drawing.Point(4, 48);
            this.gcCests.MainView = this.gridView5;
            this.gcCests.Name = "gcCests";
            this.gcCests.Size = new System.Drawing.Size(656, 213);
            this.gcCests.TabIndex = 4;
            this.gcCests.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcCests.DoubleClick += new System.EventHandler(this.gcNcms_DoubleClick);
            this.gcCests.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcNcms_KeyDown);
            // 
            // gridView5
            // 
            this.gridView5.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView5.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaCEST,
            this.colunaDescricao,
            this.colunaId,
            this.colunaStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcCests;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.ViewCaption = "CESTs";
            // 
            // colunaCEST
            // 
            this.colunaCEST.AppearanceCell.Options.UseTextOptions = true;
            this.colunaCEST.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaCEST.Caption = "Código CEST";
            this.colunaCEST.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaCEST.FieldName = "CodigoCest";
            this.colunaCEST.MinWidth = 10;
            this.colunaCEST.Name = "colunaCEST";
            this.colunaCEST.OptionsColumn.AllowEdit = false;
            this.colunaCEST.OptionsColumn.AllowFocus = false;
            this.colunaCEST.OptionsFilter.AllowFilter = false;
            this.colunaCEST.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaCEST.Visible = true;
            this.colunaCEST.VisibleIndex = 0;
            this.colunaCEST.Width = 112;
            // 
            // colunaDescricao
            // 
            this.colunaDescricao.Caption = "Descrição";
            this.colunaDescricao.FieldName = "Descricao";
            this.colunaDescricao.Name = "colunaDescricao";
            this.colunaDescricao.OptionsColumn.AllowEdit = false;
            this.colunaDescricao.OptionsColumn.AllowFocus = false;
            this.colunaDescricao.Visible = true;
            this.colunaDescricao.VisibleIndex = 1;
            this.colunaDescricao.Width = 406;
            // 
            // colunaId
            // 
            this.colunaId.Caption = "gridColumn1";
            this.colunaId.FieldName = "Id";
            this.colunaId.Name = "colunaId";
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
            this.colunaStatus.VisibleIndex = 2;
            this.colunaStatus.Width = 78;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcCests;
            this.gridView2.Name = "gridView2";
            // 
            // txtCodigoCest
            // 
            this.txtCodigoCest.EnterMoveNextControl = true;
            this.txtCodigoCest.Location = new System.Drawing.Point(4, 20);
            this.txtCodigoCest.Name = "txtCodigoCest";
            this.txtCodigoCest.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoCest.Properties.Appearance.Options.UseFont = true;
            this.txtCodigoCest.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCodigoCest.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtCodigoCest.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoCest.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtCodigoCest.Properties.Mask.EditMask = "99/";
            this.txtCodigoCest.Properties.MaxLength = 10;
            this.txtCodigoCest.Size = new System.Drawing.Size(124, 22);
            this.txtCodigoCest.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 447;
            this.label1.Text = "Código CEST";
            // 
            // txtDescricaoCest
            // 
            this.txtDescricaoCest.Location = new System.Drawing.Point(134, 20);
            this.txtDescricaoCest.Name = "txtDescricaoCest";
            this.txtDescricaoCest.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricaoCest.Properties.Appearance.Options.UseFont = true;
            this.txtDescricaoCest.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricaoCest.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricaoCest.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricaoCest.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricaoCest.Properties.Mask.EditMask = "99/";
            this.txtDescricaoCest.Properties.MaxLength = 80;
            this.txtDescricaoCest.Size = new System.Drawing.Size(337, 22);
            this.txtDescricaoCest.TabIndex = 2;
            this.txtDescricaoCest.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescricao_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 449;
            this.label3.Text = "Descrição";
            // 
            // btnPesquisaCest
            // 
            this.btnPesquisaCest.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaCest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaCest.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaCest.Location = new System.Drawing.Point(635, 20);
            this.btnPesquisaCest.Name = "btnPesquisaCest";
            this.btnPesquisaCest.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaCest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaCest.TabIndex = 623;
            this.btnPesquisaCest.TabStop = false;
            this.btnPesquisaCest.Click += new System.EventHandler(this.btnPesquisaNcm_Click);
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
            this.flowLayoutPanel1.Size = new System.Drawing.Size(614, 43);
            this.flowLayoutPanel1.TabIndex = 438;
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(477, 3);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 645;
            this.labStatus.Text = "Status";
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(477, 20);
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
            // FormCestPesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 393);
            this.MaximizeBox = false;
            this.Name = "FormCestPesquisa";
            this.NomeDaTela = "Pesquisa de CEST";
            this.Text = "Pesquisa de CEST";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoCest.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoCest.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaCest)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcCests;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCEST;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.TextEdit txtCodigoCest;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtDescricaoCest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox btnPesquisaCest;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnSelecionar;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
    }
}