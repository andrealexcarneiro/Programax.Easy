namespace Programax.Easy.View.Telas.Vendas.Roteiros
{
    partial class FormHistoricoRoteiro
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
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtUsuario = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtIdRoteiro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.gcHistoricoRoteiro = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaUsuario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataHistorico = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDescricaoHistorico = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.txtHistorico = new System.Windows.Forms.RichTextBox();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsuario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdRoteiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcHistoricoRoteiro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.txtHistorico);
            this.panelConteudo.Controls.Add(this.gcHistoricoRoteiro);
            this.panelConteudo.Controls.Add(this.labelControl13);
            this.panelConteudo.Controls.Add(this.labelControl7);
            this.panelConteudo.Controls.Add(this.txtUsuario);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.txtIdRoteiro);
            this.panelConteudo.Margin = new System.Windows.Forms.Padding(5);
            this.panelConteudo.Size = new System.Drawing.Size(964, 669);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Appearance.Options.UseForeColor = true;
            this.labelControl7.Location = new System.Drawing.Point(187, 21);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(49, 17);
            this.labelControl7.TabIndex = 10072;
            this.labelControl7.Text = "Usuário";
            // 
            // txtUsuario
            // 
            this.txtUsuario.EnterMoveNextControl = true;
            this.txtUsuario.Location = new System.Drawing.Point(183, 41);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Properties.Appearance.Options.UseFont = true;
            this.txtUsuario.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtUsuario.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtUsuario.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsuario.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtUsuario.Properties.Mask.EditMask = "99/";
            this.txtUsuario.Properties.ReadOnly = true;
            this.txtUsuario.Size = new System.Drawing.Size(760, 26);
            this.txtUsuario.TabIndex = 18;
            this.txtUsuario.TabStop = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(18, 24);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 17);
            this.labelControl2.TabIndex = 10067;
            this.labelControl2.Text = "Nr. Roteiro";
            // 
            // txtIdRoteiro
            // 
            this.txtIdRoteiro.EnterMoveNextControl = true;
            this.txtIdRoteiro.Location = new System.Drawing.Point(15, 41);
            this.txtIdRoteiro.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdRoteiro.Name = "txtIdRoteiro";
            this.txtIdRoteiro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdRoteiro.Properties.Appearance.Options.UseFont = true;
            this.txtIdRoteiro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtIdRoteiro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtIdRoteiro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdRoteiro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtIdRoteiro.Properties.Mask.EditMask = "99/";
            this.txtIdRoteiro.Properties.ReadOnly = true;
            this.txtIdRoteiro.Size = new System.Drawing.Size(160, 26);
            this.txtIdRoteiro.TabIndex = 1;
            this.txtIdRoteiro.TabStop = false;
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl13.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl13.Appearance.Options.UseFont = true;
            this.labelControl13.Appearance.Options.UseForeColor = true;
            this.labelControl13.Location = new System.Drawing.Point(15, 75);
            this.labelControl13.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(128, 17);
            this.labelControl13.TabIndex = 10083;
            this.labelControl13.Text = "Descreva o ocorrido";
            // 
            // gcHistoricoRoteiro
            // 
            this.gcHistoricoRoteiro.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcHistoricoRoteiro.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gcHistoricoRoteiro.Location = new System.Drawing.Point(16, 337);
            this.gcHistoricoRoteiro.MainView = this.gridView5;
            this.gcHistoricoRoteiro.Margin = new System.Windows.Forms.Padding(4);
            this.gcHistoricoRoteiro.Name = "gcHistoricoRoteiro";
            this.gcHistoricoRoteiro.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gcHistoricoRoteiro.Size = new System.Drawing.Size(927, 315);
            this.gcHistoricoRoteiro.TabIndex = 19;
            this.gcHistoricoRoteiro.TabStop = false;
            this.gcHistoricoRoteiro.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
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
            this.colunaUsuario,
            this.colunaDataHistorico,
            this.colunaDescricaoHistorico});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 288, 219);
            this.gridView5.DetailHeight = 431;
            this.gridView5.GridControl = this.gcHistoricoRoteiro;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.ViewCaption = "Histórico do Roteiro";
            // 
            // colunaId
            // 
            this.colunaId.Caption = "Id";
            this.colunaId.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaId.FieldName = "Id";
            this.colunaId.MinWidth = 25;
            this.colunaId.Name = "colunaId";
            this.colunaId.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaId.Width = 94;
            // 
            // colunaUsuario
            // 
            this.colunaUsuario.Caption = "Usuário";
            this.colunaUsuario.FieldName = "Usuario";
            this.colunaUsuario.MinWidth = 27;
            this.colunaUsuario.Name = "colunaUsuario";
            this.colunaUsuario.OptionsColumn.AllowEdit = false;
            this.colunaUsuario.OptionsColumn.AllowFocus = false;
            this.colunaUsuario.OptionsFilter.AllowFilter = false;
            this.colunaUsuario.Visible = true;
            this.colunaUsuario.VisibleIndex = 0;
            this.colunaUsuario.Width = 145;
            // 
            // colunaDataHistorico
            // 
            this.colunaDataHistorico.Caption = "Data Histórico";
            this.colunaDataHistorico.FieldName = "DataHistorico";
            this.colunaDataHistorico.MinWidth = 27;
            this.colunaDataHistorico.Name = "colunaDataHistorico";
            this.colunaDataHistorico.OptionsColumn.AllowEdit = false;
            this.colunaDataHistorico.OptionsColumn.AllowFocus = false;
            this.colunaDataHistorico.OptionsFilter.AllowFilter = false;
            this.colunaDataHistorico.Visible = true;
            this.colunaDataHistorico.VisibleIndex = 1;
            this.colunaDataHistorico.Width = 146;
            // 
            // colunaDescricaoHistorico
            // 
            this.colunaDescricaoHistorico.Caption = "Descrição Histórico";
            this.colunaDescricaoHistorico.ColumnEdit = this.repositoryItemTextEdit1;
            this.colunaDescricaoHistorico.FieldName = "DescricaoHistorico";
            this.colunaDescricaoHistorico.MinWidth = 27;
            this.colunaDescricaoHistorico.Name = "colunaDescricaoHistorico";
            this.colunaDescricaoHistorico.OptionsColumn.AllowEdit = false;
            this.colunaDescricaoHistorico.OptionsColumn.AllowFocus = false;
            this.colunaDescricaoHistorico.OptionsFilter.AllowFilter = false;
            this.colunaDescricaoHistorico.Visible = true;
            this.colunaDescricaoHistorico.VisibleIndex = 2;
            this.colunaDescricaoHistorico.Width = 641;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // gridView2
            // 
            this.gridView2.DetailHeight = 431;
            this.gridView2.GridControl = this.gcHistoricoRoteiro;
            this.gridView2.Name = "gridView2";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSalvar);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(935, 64);
            this.flowLayoutPanel1.TabIndex = 1041;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.Image = global::Programax.Easy.View.Properties.Resources.iconSalvar;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSalvar.Location = new System.Drawing.Point(0, 0);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(0);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(133, 49);
            this.btnSalvar.TabIndex = 1037;
            this.btnSalvar.Text = " Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(133, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(133, 49);
            this.btnSair.TabIndex = 1042;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // txtHistorico
            // 
            this.txtHistorico.Location = new System.Drawing.Point(15, 99);
            this.txtHistorico.Name = "txtHistorico";
            this.txtHistorico.Size = new System.Drawing.Size(928, 218);
            this.txtHistorico.TabIndex = 10084;
            this.txtHistorico.Text = "";
            // 
            // FormHistoricoRoteiro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 804);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "FormHistoricoRoteiro";
            this.NomeDaTela = "Histórico do Roteiro";
            this.Text = "Histórico do Roteiro";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsuario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdRoteiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcHistoricoRoteiro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtUsuario;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtIdRoteiro;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        public DevExpress.XtraGrid.GridControl gcHistoricoRoteiro;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaUsuario;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataHistorico;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricaoHistorico;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.RichTextBox txtHistorico;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
    }
}