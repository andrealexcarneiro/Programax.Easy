namespace Programax.Easy.View.Telas.Fiscal.Ncms
{
    partial class FormNcmPesquisa
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
            this.gcNcms = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaNcm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDescricao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtCodigoNcm = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescricao = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPesquisaNcm = new System.Windows.Forms.PictureBox();
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcNcms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoNcm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaNcm)).BeginInit();
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
            this.panelConteudo.Controls.Add(this.gcNcms);
            this.panelConteudo.Controls.Add(this.txtCodigoNcm);
            this.panelConteudo.Controls.Add(this.btnPesquisaNcm);
            this.panelConteudo.Controls.Add(this.label3);
            this.panelConteudo.Controls.Add(this.txtDescricao);
            this.panelConteudo.Size = new System.Drawing.Size(662, 282);
            // 
            // gcNcms
            // 
            this.gcNcms.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcNcms.Location = new System.Drawing.Point(4, 48);
            this.gcNcms.MainView = this.gridView5;
            this.gcNcms.Name = "gcNcms";
            this.gcNcms.Size = new System.Drawing.Size(656, 213);
            this.gcNcms.TabIndex = 4;
            this.gcNcms.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcNcms.DoubleClick += new System.EventHandler(this.gcNcms_DoubleClick);
            this.gcNcms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcNcms_KeyDown);
            // 
            // gridView5
            // 
            this.gridView5.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView5.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaNcm,
            this.colunaDescricao,
            this.colunaId,
            this.colunaStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcNcms;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.ViewCaption = "NCMs";
            // 
            // colunaNcm
            // 
            this.colunaNcm.AppearanceCell.Options.UseTextOptions = true;
            this.colunaNcm.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaNcm.Caption = "Código NCM";
            this.colunaNcm.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaNcm.FieldName = "CodigoNcm";
            this.colunaNcm.MinWidth = 10;
            this.colunaNcm.Name = "colunaNcm";
            this.colunaNcm.OptionsColumn.AllowEdit = false;
            this.colunaNcm.OptionsColumn.AllowFocus = false;
            this.colunaNcm.OptionsFilter.AllowFilter = false;
            this.colunaNcm.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaNcm.Visible = true;
            this.colunaNcm.VisibleIndex = 0;
            this.colunaNcm.Width = 112;
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
            this.gridView2.GridControl = this.gcNcms;
            this.gridView2.Name = "gridView2";
            // 
            // txtCodigoNcm
            // 
            this.txtCodigoNcm.EnterMoveNextControl = true;
            this.txtCodigoNcm.Location = new System.Drawing.Point(4, 20);
            this.txtCodigoNcm.Name = "txtCodigoNcm";
            this.txtCodigoNcm.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoNcm.Properties.Appearance.Options.UseFont = true;
            this.txtCodigoNcm.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCodigoNcm.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtCodigoNcm.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoNcm.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtCodigoNcm.Properties.Mask.EditMask = "99/";
            this.txtCodigoNcm.Properties.MaxLength = 10;
            this.txtCodigoNcm.Size = new System.Drawing.Size(124, 22);
            this.txtCodigoNcm.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 447;
            this.label1.Text = "Código NCM";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(134, 20);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.Properties.Appearance.Options.UseFont = true;
            this.txtDescricao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricao.Properties.Mask.EditMask = "99/";
            this.txtDescricao.Properties.MaxLength = 80;
            this.txtDescricao.Size = new System.Drawing.Size(337, 22);
            this.txtDescricao.TabIndex = 2;
            this.txtDescricao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescricao_KeyDown);
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
            // btnPesquisaNcm
            // 
            this.btnPesquisaNcm.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaNcm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaNcm.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaNcm.Location = new System.Drawing.Point(635, 20);
            this.btnPesquisaNcm.Name = "btnPesquisaNcm";
            this.btnPesquisaNcm.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaNcm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaNcm.TabIndex = 623;
            this.btnPesquisaNcm.TabStop = false;
            this.btnPesquisaNcm.Click += new System.EventHandler(this.btnPesquisaNcm_Click);
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
            // FormNcmPesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 393);
            this.MaximizeBox = false;
            this.Name = "FormNcmPesquisa";
            this.NomeDaTela = "Pesquisa de NCM";
            this.Text = "Pesquisa de NCM";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcNcms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoNcm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaNcm)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcNcms;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNcm;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.TextEdit txtCodigoNcm;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtDescricao;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox btnPesquisaNcm;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnSelecionar;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
    }
}