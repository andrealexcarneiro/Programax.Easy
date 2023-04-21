namespace Programax.Easy.View.Telas.Fiscal.FormsIcmsInterestadual
{
    partial class FormCadastroIcmsInterestadual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCadastroIcmsInterestadual));
            this.txtNcmId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtNcmDescricao = new DevExpress.XtraEditors.TextEdit();
            this.btnPesquisaNcm = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.labelControl35 = new DevExpress.XtraEditors.LabelControl();
            this.txtFCP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtAliquotaInterna = new DevExpress.XtraEditors.TextEdit();
            this.cboUF = new DevExpress.XtraEditors.LookUpEdit();
            this.labTipoInscricao = new DevExpress.XtraEditors.LabelControl();
            this.btnExcluirItem = new System.Windows.Forms.Button();
            this.btnCancelarItem = new System.Windows.Forms.Button();
            this.btnInserirAtualizarItem = new System.Windows.Forms.Button();
            this.gcAliquotas = new DevExpress.XtraGrid.GridControl();
            this.gridControl2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaFcp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaAliquotaInterna = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNcmId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNcmDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaNcm)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFCP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliquotaInterna.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUF.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAliquotas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel3);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.gcAliquotas);
            this.panelConteudo.Controls.Add(this.btnExcluirItem);
            this.panelConteudo.Controls.Add(this.btnCancelarItem);
            this.panelConteudo.Controls.Add(this.btnInserirAtualizarItem);
            this.panelConteudo.Controls.Add(this.txtAliquotaInterna);
            this.panelConteudo.Controls.Add(this.cboUF);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.labTipoInscricao);
            this.panelConteudo.Controls.Add(this.txtFCP);
            this.panelConteudo.Controls.Add(this.labelControl35);
            this.panelConteudo.Controls.Add(this.txtNcmId);
            this.panelConteudo.Controls.Add(this.labelControl5);
            this.panelConteudo.Controls.Add(this.labelControl4);
            this.panelConteudo.Controls.Add(this.txtNcmDescricao);
            this.panelConteudo.Controls.Add(this.btnPesquisaNcm);
            this.panelConteudo.Size = new System.Drawing.Size(876, 334);
            // 
            // txtNcmId
            // 
            this.txtNcmId.EnterMoveNextControl = true;
            this.txtNcmId.Location = new System.Drawing.Point(6, 25);
            this.txtNcmId.Name = "txtNcmId";
            this.txtNcmId.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNcmId.Properties.Appearance.Options.UseFont = true;
            this.txtNcmId.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNcmId.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNcmId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNcmId.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNcmId.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNcmId.Properties.Mask.EditMask = "99/";
            this.txtNcmId.Properties.MaxLength = 8;
            this.txtNcmId.Size = new System.Drawing.Size(101, 22);
            this.txtNcmId.TabIndex = 1;
            this.txtNcmId.Leave += new System.EventHandler(this.txtNcmId_Leave);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl5.Location = new System.Drawing.Point(6, 8);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 13);
            this.labelControl5.TabIndex = 626;
            this.labelControl5.Text = "NCM";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Location = new System.Drawing.Point(141, 8);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(75, 13);
            this.labelControl4.TabIndex = 628;
            this.labelControl4.Text = "Descrição NCM";
            // 
            // txtNcmDescricao
            // 
            this.txtNcmDescricao.EnterMoveNextControl = true;
            this.txtNcmDescricao.Location = new System.Drawing.Point(141, 25);
            this.txtNcmDescricao.Name = "txtNcmDescricao";
            this.txtNcmDescricao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNcmDescricao.Properties.Appearance.Options.UseFont = true;
            this.txtNcmDescricao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNcmDescricao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNcmDescricao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNcmDescricao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNcmDescricao.Properties.Mask.EditMask = "99/";
            this.txtNcmDescricao.Properties.MaxLength = 80;
            this.txtNcmDescricao.Properties.ReadOnly = true;
            this.txtNcmDescricao.Size = new System.Drawing.Size(287, 22);
            this.txtNcmDescricao.TabIndex = 625;
            this.txtNcmDescricao.TabStop = false;
            // 
            // btnPesquisaNcm
            // 
            this.btnPesquisaNcm.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaNcm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaNcm.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisaNcm.Image")));
            this.btnPesquisaNcm.Location = new System.Drawing.Point(113, 24);
            this.btnPesquisaNcm.Name = "btnPesquisaNcm";
            this.btnPesquisaNcm.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaNcm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaNcm.TabIndex = 627;
            this.btnPesquisaNcm.TabStop = false;
            this.btnPesquisaNcm.Click += new System.EventHandler(this.btnPesquisaNcm_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.btnGravar);
            this.flowLayoutPanel3.Controls.Add(this.btnLimpar);
            this.flowLayoutPanel3.Controls.Add(this.btnSair);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(6, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(540, 40);
            this.flowLayoutPanel3.TabIndex = 0;
            // 
            // btnGravar
            // 
            this.btnGravar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGravar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnGravar.FlatAppearance.BorderSize = 0;
            this.btnGravar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGravar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGravar.Image = global::Programax.Easy.View.Properties.Resources.iconSalvar;
            this.btnGravar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnGravar.Location = new System.Drawing.Point(0, 0);
            this.btnGravar.Margin = new System.Windows.Forms.Padding(0);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(100, 40);
            this.btnGravar.TabIndex = 1048;
            this.btnGravar.Text = " Salvar";
            this.btnGravar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGravar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnLimpar.FlatAppearance.BorderSize = 0;
            this.btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.Image = global::Programax.Easy.View.Properties.Resources.iconLimpar;
            this.btnLimpar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnLimpar.Location = new System.Drawing.Point(100, 0);
            this.btnLimpar.Margin = new System.Windows.Forms.Padding(0);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(100, 40);
            this.btnLimpar.TabIndex = 1050;
            this.btnLimpar.Text = " Limpar";
            this.btnLimpar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(200, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 1049;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // labelControl35
            // 
            this.labelControl35.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl35.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl35.Location = new System.Drawing.Point(604, 8);
            this.labelControl35.Name = "labelControl35";
            this.labelControl35.Size = new System.Drawing.Size(20, 13);
            this.labelControl35.TabIndex = 622;
            this.labelControl35.Text = "FCP";
            this.labelControl35.ToolTip = "Fundo de Combate à Pobreza";
            // 
            // txtFCP
            // 
            this.txtFCP.EnterMoveNextControl = true;
            this.txtFCP.Location = new System.Drawing.Point(604, 25);
            this.txtFCP.Name = "txtFCP";
            this.txtFCP.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFCP.Properties.Appearance.Options.UseFont = true;
            this.txtFCP.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtFCP.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtFCP.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFCP.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtFCP.Properties.Mask.EditMask = "[0-2]{1,1}([\\.\\,][0-9]{0,4})?";
            this.txtFCP.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtFCP.Properties.Mask.ShowPlaceHolders = false;
            this.txtFCP.Properties.MaxLength = 30;
            this.txtFCP.Size = new System.Drawing.Size(80, 22);
            this.txtFCP.TabIndex = 3;
            this.txtFCP.Tag = "FCP";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Location = new System.Drawing.Point(690, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 13);
            this.labelControl1.TabIndex = 624;
            this.labelControl1.Text = "Alíquota Interna";
            // 
            // txtAliquotaInterna
            // 
            this.txtAliquotaInterna.Location = new System.Drawing.Point(690, 25);
            this.txtAliquotaInterna.Name = "txtAliquotaInterna";
            this.txtAliquotaInterna.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAliquotaInterna.Properties.Appearance.Options.UseFont = true;
            this.txtAliquotaInterna.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtAliquotaInterna.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtAliquotaInterna.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAliquotaInterna.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtAliquotaInterna.Properties.Mask.EditMask = "[0-9]{1,2}([\\.\\,][0-9]{0,4})?";
            this.txtAliquotaInterna.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtAliquotaInterna.Properties.Mask.ShowPlaceHolders = false;
            this.txtAliquotaInterna.Properties.MaxLength = 30;
            this.txtAliquotaInterna.Size = new System.Drawing.Size(83, 22);
            this.txtAliquotaInterna.TabIndex = 4;
            this.txtAliquotaInterna.Tag = "AliquotaInterna";
            this.txtAliquotaInterna.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAliquotaInterna_KeyDown);
            // 
            // cboUF
            // 
            this.cboUF.EnterMoveNextControl = true;
            this.cboUF.Location = new System.Drawing.Point(434, 25);
            this.cboUF.Name = "cboUF";
            this.cboUF.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboUF.Properties.Appearance.Options.UseFont = true;
            this.cboUF.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUF.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nome", "Estado")});
            this.cboUF.Properties.DropDownRows = 15;
            this.cboUF.Properties.NullText = "";
            this.cboUF.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboUF.Size = new System.Drawing.Size(164, 22);
            this.cboUF.TabIndex = 2;
            // 
            // labTipoInscricao
            // 
            this.labTipoInscricao.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTipoInscricao.Location = new System.Drawing.Point(434, 10);
            this.labTipoInscricao.Name = "labTipoInscricao";
            this.labTipoInscricao.Size = new System.Drawing.Size(72, 13);
            this.labTipoInscricao.TabIndex = 10110;
            this.labTipoInscricao.Text = "Estado Destino";
            // 
            // btnExcluirItem
            // 
            this.btnExcluirItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluirItem.FlatAppearance.BorderSize = 0;
            this.btnExcluirItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExcluirItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExcluirItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluirItem.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluirItem.Image")));
            this.btnExcluirItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcluirItem.Location = new System.Drawing.Point(841, 25);
            this.btnExcluirItem.Name = "btnExcluirItem";
            this.btnExcluirItem.Size = new System.Drawing.Size(28, 23);
            this.btnExcluirItem.TabIndex = 10113;
            this.btnExcluirItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluirItem.UseVisualStyleBackColor = true;
            this.btnExcluirItem.Visible = false;
            this.btnExcluirItem.Click += new System.EventHandler(this.btnExcluirItem_Click);
            // 
            // btnCancelarItem
            // 
            this.btnCancelarItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarItem.FlatAppearance.BorderSize = 0;
            this.btnCancelarItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancelarItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancelarItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarItem.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarItem.Image")));
            this.btnCancelarItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelarItem.Location = new System.Drawing.Point(807, 24);
            this.btnCancelarItem.Name = "btnCancelarItem";
            this.btnCancelarItem.Size = new System.Drawing.Size(28, 23);
            this.btnCancelarItem.TabIndex = 10112;
            this.btnCancelarItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelarItem.UseVisualStyleBackColor = true;
            this.btnCancelarItem.Click += new System.EventHandler(this.btnCancelarItem_Click);
            // 
            // btnInserirAtualizarItem
            // 
            this.btnInserirAtualizarItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInserirAtualizarItem.FlatAppearance.BorderSize = 0;
            this.btnInserirAtualizarItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnInserirAtualizarItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnInserirAtualizarItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInserirAtualizarItem.Image = ((System.Drawing.Image)(resources.GetObject("btnInserirAtualizarItem.Image")));
            this.btnInserirAtualizarItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInserirAtualizarItem.Location = new System.Drawing.Point(779, 24);
            this.btnInserirAtualizarItem.Name = "btnInserirAtualizarItem";
            this.btnInserirAtualizarItem.Size = new System.Drawing.Size(28, 23);
            this.btnInserirAtualizarItem.TabIndex = 10111;
            this.btnInserirAtualizarItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInserirAtualizarItem.UseVisualStyleBackColor = true;
            this.btnInserirAtualizarItem.Click += new System.EventHandler(this.btnInserirAtualizarItem_Click);
            // 
            // gcAliquotas
            // 
            this.gcAliquotas.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcAliquotas.Location = new System.Drawing.Point(6, 54);
            this.gcAliquotas.MainView = this.gridControl2;
            this.gcAliquotas.Name = "gcAliquotas";
            this.gcAliquotas.Size = new System.Drawing.Size(863, 278);
            this.gcAliquotas.TabIndex = 10114;
            this.gcAliquotas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridControl2,
            this.gridView3,
            this.cardView1});
            this.gcAliquotas.DoubleClick += new System.EventHandler(this.gcAliquotas_DoubleClick);
            this.gcAliquotas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcAliquotas_KeyDown);
            // 
            // gridControl2
            // 
            this.gridControl2.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridControl2.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridControl2.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridControl2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridControl2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaId,
            this.colunaFcp,
            this.colunaAliquotaInterna});
            this.gridControl2.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridControl2.GridControl = this.gcAliquotas;
            this.gridControl2.GroupPanelText = "Enderecos";
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridControl2.OptionsView.ShowGroupPanel = false;
            this.gridControl2.OptionsView.ShowIndicator = false;
            this.gridControl2.OptionsView.ShowViewCaption = true;
            this.gridControl2.PaintStyleName = "Skin";
            this.gridControl2.ViewCaption = "Alíquotas De NCM Por Estado";
            // 
            // colunaId
            // 
            this.colunaId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.Caption = "UF Destino";
            this.colunaId.FieldName = "UF";
            this.colunaId.Name = "colunaId";
            this.colunaId.OptionsColumn.AllowEdit = false;
            this.colunaId.OptionsColumn.AllowFocus = false;
            this.colunaId.OptionsFilter.AllowAutoFilter = false;
            this.colunaId.OptionsFilter.AllowFilter = false;
            this.colunaId.Visible = true;
            this.colunaId.VisibleIndex = 0;
            this.colunaId.Width = 63;
            // 
            // colunaFcp
            // 
            this.colunaFcp.AppearanceCell.Options.UseTextOptions = true;
            this.colunaFcp.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaFcp.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaFcp.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaFcp.Caption = "FCP";
            this.colunaFcp.FieldName = "FCP";
            this.colunaFcp.MinWidth = 10;
            this.colunaFcp.Name = "colunaFcp";
            this.colunaFcp.OptionsColumn.AllowEdit = false;
            this.colunaFcp.OptionsColumn.AllowFocus = false;
            this.colunaFcp.OptionsFilter.AllowAutoFilter = false;
            this.colunaFcp.OptionsFilter.AllowFilter = false;
            this.colunaFcp.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.colunaFcp.Visible = true;
            this.colunaFcp.VisibleIndex = 1;
            this.colunaFcp.Width = 119;
            // 
            // colunaAliquotaInterna
            // 
            this.colunaAliquotaInterna.AppearanceCell.Options.UseTextOptions = true;
            this.colunaAliquotaInterna.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaAliquotaInterna.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaAliquotaInterna.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaAliquotaInterna.Caption = "Alíquota Interna";
            this.colunaAliquotaInterna.FieldName = "AliquotaInterna";
            this.colunaAliquotaInterna.Name = "colunaAliquotaInterna";
            this.colunaAliquotaInterna.OptionsColumn.AllowEdit = false;
            this.colunaAliquotaInterna.OptionsColumn.AllowFocus = false;
            this.colunaAliquotaInterna.OptionsFilter.AllowAutoFilter = false;
            this.colunaAliquotaInterna.OptionsFilter.AllowFilter = false;
            this.colunaAliquotaInterna.Visible = true;
            this.colunaAliquotaInterna.VisibleIndex = 2;
            this.colunaAliquotaInterna.Width = 112;
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gcAliquotas;
            this.gridView3.Name = "gridView3";
            // 
            // cardView1
            // 
            this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.cardView1.FocusedCardTopFieldIndex = 0;
            this.cardView1.GridControl = this.gcAliquotas;
            this.cardView1.Name = "cardView1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "Teste";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // FormCadastroIcmsInterestadual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 445);
            this.Name = "FormCadastroIcmsInterestadual";
            this.NomeDaTela = "DIFAL E FCP";
            this.Text = "DIFAL E FCP";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNcmId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNcmDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaNcm)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFCP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliquotaInterna.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUF.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAliquotas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtNcmId;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtNcmDescricao;
        private System.Windows.Forms.PictureBox btnPesquisaNcm;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnSair;
        private DevExpress.XtraEditors.TextEdit txtAliquotaInterna;
        private DevExpress.XtraEditors.LookUpEdit cboUF;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labTipoInscricao;
        private DevExpress.XtraEditors.TextEdit txtFCP;
        private DevExpress.XtraEditors.LabelControl labelControl35;
        private System.Windows.Forms.Button btnExcluirItem;
        private System.Windows.Forms.Button btnCancelarItem;
        private System.Windows.Forms.Button btnInserirAtualizarItem;
        private DevExpress.XtraGrid.GridControl gcAliquotas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridControl2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaFcp;
        private DevExpress.XtraGrid.Columns.GridColumn colunaAliquotaInterna;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Views.Card.CardView cardView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}