namespace Programax.Easy.View.Telas.Relatorios
{
    partial class FormRelatorioMovimentacaoItens
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRelatorioMovimentacaoItens));
            this.btnSair = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.txtDataFinalMovimentacao = new DevExpress.XtraEditors.DateEdit();
            this.labelControl60 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataInicialMovimentacao = new DevExpress.XtraEditors.DateEdit();
            this.labelControl61 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl59 = new DevExpress.XtraEditors.LabelControl();
            this.cboOrigemMovimentacao = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl58 = new DevExpress.XtraEditors.LabelControl();
            this.cboTipoMovimentacao = new DevExpress.XtraEditors.LookUpEdit();
            this.btnPesquisaProduto2 = new System.Windows.Forms.Button();
            this.labelControl28 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl29 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricaoProduto = new DevExpress.XtraEditors.TextEdit();
            this.labDescricao = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigoDeBarrasProduto = new DevExpress.XtraEditors.TextEdit();
            this.txtIdProduto = new DevExpress.XtraEditors.TextEdit();
            this.cboGrupos = new DevExpress.XtraEditors.LookUpEdit();
            this.cboSubGrupos = new DevExpress.XtraEditors.LookUpEdit();
            this.cboMarcas = new DevExpress.XtraEditors.LookUpEdit();
            this.cboCategorias = new DevExpress.XtraEditors.LookUpEdit();
            this.cboFabricantes = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl53 = new DevExpress.XtraEditors.LabelControl();
            this.labTipoInscricao = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalMovimentacao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalMovimentacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialMovimentacao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialMovimentacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOrigemMovimentacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoMovimentacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoProduto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoDeBarrasProduto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdProduto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGrupos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubGrupos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMarcas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategorias.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFabricantes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(671, 242);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 10165;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-96, 228);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(908, 10);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10164;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-79, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(891, 10);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10163;
            this.pictureBox1.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.labelControl1.Location = new System.Drawing.Point(151, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(496, 29);
            this.labelControl1.TabIndex = 10162;
            this.labelControl1.Text = "RELATÓRIO DE MOVIMENTAÇÃO DE ITENS";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Image = global::Programax.Easy.View.Properties.Resources.icone_Imprimir;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnImprimir.Location = new System.Drawing.Point(11, 242);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(0);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(110, 40);
            this.btnImprimir.TabIndex = 10161;
            this.btnImprimir.Text = " Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // txtDataFinalMovimentacao
            // 
            this.txtDataFinalMovimentacao.EditValue = null;
            this.txtDataFinalMovimentacao.EnterMoveNextControl = true;
            this.txtDataFinalMovimentacao.Location = new System.Drawing.Point(132, 139);
            this.txtDataFinalMovimentacao.Name = "txtDataFinalMovimentacao";
            this.txtDataFinalMovimentacao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinalMovimentacao.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinalMovimentacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinalMovimentacao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinalMovimentacao.Size = new System.Drawing.Size(114, 22);
            this.txtDataFinalMovimentacao.TabIndex = 4;
            // 
            // labelControl60
            // 
            this.labelControl60.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl60.Location = new System.Drawing.Point(132, 124);
            this.labelControl60.Name = "labelControl60";
            this.labelControl60.Size = new System.Drawing.Size(48, 13);
            this.labelControl60.TabIndex = 10173;
            this.labelControl60.Text = "Data Final";
            // 
            // txtDataInicialMovimentacao
            // 
            this.txtDataInicialMovimentacao.EditValue = null;
            this.txtDataInicialMovimentacao.EnterMoveNextControl = true;
            this.txtDataInicialMovimentacao.Location = new System.Drawing.Point(12, 139);
            this.txtDataInicialMovimentacao.Name = "txtDataInicialMovimentacao";
            this.txtDataInicialMovimentacao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicialMovimentacao.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicialMovimentacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicialMovimentacao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicialMovimentacao.Size = new System.Drawing.Size(114, 22);
            this.txtDataInicialMovimentacao.TabIndex = 3;
            // 
            // labelControl61
            // 
            this.labelControl61.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl61.Location = new System.Drawing.Point(12, 124);
            this.labelControl61.Name = "labelControl61";
            this.labelControl61.Size = new System.Drawing.Size(53, 13);
            this.labelControl61.TabIndex = 10172;
            this.labelControl61.Text = "Data Inicial";
            // 
            // labelControl59
            // 
            this.labelControl59.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl59.Location = new System.Drawing.Point(396, 124);
            this.labelControl59.Name = "labelControl59";
            this.labelControl59.Size = new System.Drawing.Size(33, 13);
            this.labelControl59.TabIndex = 10171;
            this.labelControl59.Text = "Origem";
            // 
            // cboOrigemMovimentacao
            // 
            this.cboOrigemMovimentacao.EnterMoveNextControl = true;
            this.cboOrigemMovimentacao.Location = new System.Drawing.Point(396, 139);
            this.cboOrigemMovimentacao.Name = "cboOrigemMovimentacao";
            this.cboOrigemMovimentacao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOrigemMovimentacao.Properties.Appearance.Options.UseFont = true;
            this.cboOrigemMovimentacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboOrigemMovimentacao.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", 5, "Id"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboOrigemMovimentacao.Properties.DropDownRows = 5;
            this.cboOrigemMovimentacao.Properties.NullText = "";
            this.cboOrigemMovimentacao.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboOrigemMovimentacao.Size = new System.Drawing.Size(172, 22);
            this.cboOrigemMovimentacao.TabIndex = 6;
            // 
            // labelControl58
            // 
            this.labelControl58.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl58.Location = new System.Drawing.Point(252, 124);
            this.labelControl58.Name = "labelControl58";
            this.labelControl58.Size = new System.Drawing.Size(21, 13);
            this.labelControl58.TabIndex = 10170;
            this.labelControl58.Text = "Tipo";
            // 
            // cboTipoMovimentacao
            // 
            this.cboTipoMovimentacao.EnterMoveNextControl = true;
            this.cboTipoMovimentacao.Location = new System.Drawing.Point(252, 139);
            this.cboTipoMovimentacao.Name = "cboTipoMovimentacao";
            this.cboTipoMovimentacao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoMovimentacao.Properties.Appearance.Options.UseFont = true;
            this.cboTipoMovimentacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTipoMovimentacao.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", 5, "Id"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboTipoMovimentacao.Properties.DropDownRows = 5;
            this.cboTipoMovimentacao.Properties.NullText = "";
            this.cboTipoMovimentacao.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboTipoMovimentacao.Size = new System.Drawing.Size(138, 22);
            this.cboTipoMovimentacao.TabIndex = 5;
            // 
            // btnPesquisaProduto2
            // 
            this.btnPesquisaProduto2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaProduto2.FlatAppearance.BorderSize = 0;
            this.btnPesquisaProduto2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaProduto2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaProduto2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisaProduto2.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaProduto2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisaProduto2.Location = new System.Drawing.Point(268, 87);
            this.btnPesquisaProduto2.Name = "btnPesquisaProduto2";
            this.btnPesquisaProduto2.Size = new System.Drawing.Size(28, 23);
            this.btnPesquisaProduto2.TabIndex = 10180;
            this.btnPesquisaProduto2.TabStop = false;
            this.btnPesquisaProduto2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisaProduto2.UseVisualStyleBackColor = true;
            this.btnPesquisaProduto2.Click += new System.EventHandler(this.btnPesquisaProduto2_Click);
            // 
            // labelControl28
            // 
            this.labelControl28.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl28.Location = new System.Drawing.Point(12, 73);
            this.labelControl28.Name = "labelControl28";
            this.labelControl28.Size = new System.Drawing.Size(67, 13);
            this.labelControl28.TabIndex = 10179;
            this.labelControl28.Text = "Codigo Barras";
            // 
            // labelControl29
            // 
            this.labelControl29.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl29.Location = new System.Drawing.Point(170, 73);
            this.labelControl29.Name = "labelControl29";
            this.labelControl29.Size = new System.Drawing.Size(73, 13);
            this.labelControl29.TabIndex = 10177;
            this.labelControl29.Text = "Código do Item";
            // 
            // txtDescricaoProduto
            // 
            this.txtDescricaoProduto.EnterMoveNextControl = true;
            this.txtDescricaoProduto.Location = new System.Drawing.Point(298, 89);
            this.txtDescricaoProduto.Name = "txtDescricaoProduto";
            this.txtDescricaoProduto.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricaoProduto.Properties.Appearance.Options.UseFont = true;
            this.txtDescricaoProduto.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricaoProduto.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricaoProduto.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricaoProduto.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricaoProduto.Properties.Mask.EditMask = "99/";
            this.txtDescricaoProduto.Properties.MaxLength = 120;
            this.txtDescricaoProduto.Properties.ReadOnly = true;
            this.txtDescricaoProduto.Size = new System.Drawing.Size(270, 22);
            this.txtDescricaoProduto.TabIndex = 10176;
            this.txtDescricaoProduto.TabStop = false;
            // 
            // labDescricao
            // 
            this.labDescricao.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDescricao.Location = new System.Drawing.Point(302, 73);
            this.labDescricao.Name = "labDescricao";
            this.labDescricao.Size = new System.Drawing.Size(102, 13);
            this.labDescricao.TabIndex = 10178;
            this.labDescricao.Text = "Descrição do Produto";
            // 
            // txtCodigoDeBarrasProduto
            // 
            this.txtCodigoDeBarrasProduto.EnterMoveNextControl = true;
            this.txtCodigoDeBarrasProduto.Location = new System.Drawing.Point(12, 89);
            this.txtCodigoDeBarrasProduto.Name = "txtCodigoDeBarrasProduto";
            this.txtCodigoDeBarrasProduto.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoDeBarrasProduto.Properties.Appearance.Options.UseFont = true;
            this.txtCodigoDeBarrasProduto.Size = new System.Drawing.Size(152, 22);
            this.txtCodigoDeBarrasProduto.TabIndex = 1;
            this.txtCodigoDeBarrasProduto.Leave += new System.EventHandler(this.txtCodigoDeBarrasProduto_Leave);
            // 
            // txtIdProduto
            // 
            this.txtIdProduto.EnterMoveNextControl = true;
            this.txtIdProduto.Location = new System.Drawing.Point(170, 89);
            this.txtIdProduto.Name = "txtIdProduto";
            this.txtIdProduto.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdProduto.Properties.Appearance.Options.UseFont = true;
            this.txtIdProduto.Size = new System.Drawing.Size(92, 22);
            this.txtIdProduto.TabIndex = 2;
            this.txtIdProduto.Leave += new System.EventHandler(this.txtIdProduto_Leave);
            // 
            // cboGrupos
            // 
            this.cboGrupos.EnterMoveNextControl = true;
            this.cboGrupos.Location = new System.Drawing.Point(467, 194);
            this.cboGrupos.Name = "cboGrupos";
            this.cboGrupos.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGrupos.Properties.Appearance.Options.UseFont = true;
            this.cboGrupos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboGrupos.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", 5, "Id"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboGrupos.Properties.DropDownRows = 5;
            this.cboGrupos.Properties.NullText = "";
            this.cboGrupos.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboGrupos.Size = new System.Drawing.Size(146, 22);
            this.cboGrupos.TabIndex = 10184;
            this.cboGrupos.EditValueChanged += new System.EventHandler(this.cboGrupos_EditValueChanged);
            // 
            // cboSubGrupos
            // 
            this.cboSubGrupos.EnterMoveNextControl = true;
            this.cboSubGrupos.Location = new System.Drawing.Point(619, 194);
            this.cboSubGrupos.Name = "cboSubGrupos";
            this.cboSubGrupos.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSubGrupos.Properties.Appearance.Options.UseFont = true;
            this.cboSubGrupos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSubGrupos.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", 5, "Id"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboSubGrupos.Properties.DropDownRows = 5;
            this.cboSubGrupos.Properties.NullText = "";
            this.cboSubGrupos.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboSubGrupos.Size = new System.Drawing.Size(146, 22);
            this.cboSubGrupos.TabIndex = 10185;
            // 
            // cboMarcas
            // 
            this.cboMarcas.EnterMoveNextControl = true;
            this.cboMarcas.Location = new System.Drawing.Point(12, 194);
            this.cboMarcas.Name = "cboMarcas";
            this.cboMarcas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMarcas.Properties.Appearance.Options.UseFont = true;
            this.cboMarcas.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMarcas.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", 5, "Id"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboMarcas.Properties.DropDownRows = 5;
            this.cboMarcas.Properties.NullText = "";
            this.cboMarcas.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboMarcas.Size = new System.Drawing.Size(146, 22);
            this.cboMarcas.TabIndex = 10181;
            this.cboMarcas.EditValueChanged += new System.EventHandler(this.cboMarcas_EditValueChanged);
            // 
            // cboCategorias
            // 
            this.cboCategorias.EnterMoveNextControl = true;
            this.cboCategorias.Location = new System.Drawing.Point(315, 194);
            this.cboCategorias.Name = "cboCategorias";
            this.cboCategorias.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategorias.Properties.Appearance.Options.UseFont = true;
            this.cboCategorias.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCategorias.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", 5, "Id"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboCategorias.Properties.DropDownRows = 5;
            this.cboCategorias.Properties.NullText = "";
            this.cboCategorias.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboCategorias.Size = new System.Drawing.Size(146, 22);
            this.cboCategorias.TabIndex = 10183;
            this.cboCategorias.EditValueChanged += new System.EventHandler(this.cboCategorias_EditValueChanged);
            // 
            // cboFabricantes
            // 
            this.cboFabricantes.EnterMoveNextControl = true;
            this.cboFabricantes.Location = new System.Drawing.Point(164, 194);
            this.cboFabricantes.Name = "cboFabricantes";
            this.cboFabricantes.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFabricantes.Properties.Appearance.Options.UseFont = true;
            this.cboFabricantes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFabricantes.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", 5, "Id"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboFabricantes.Properties.DropDownRows = 5;
            this.cboFabricantes.Properties.NullText = "";
            this.cboFabricantes.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboFabricantes.Size = new System.Drawing.Size(145, 22);
            this.cboFabricantes.TabIndex = 10182;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl8.Location = new System.Drawing.Point(12, 176);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(30, 13);
            this.labelControl8.TabIndex = 10188;
            this.labelControl8.Text = "Marca";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Location = new System.Drawing.Point(619, 176);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(46, 13);
            this.labelControl9.TabIndex = 10189;
            this.labelControl9.Text = "Subgrupo";
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl16.Location = new System.Drawing.Point(467, 176);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(29, 13);
            this.labelControl16.TabIndex = 10187;
            this.labelControl16.Text = "Grupo";
            // 
            // labelControl53
            // 
            this.labelControl53.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl53.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl53.Location = new System.Drawing.Point(164, 176);
            this.labelControl53.Name = "labelControl53";
            this.labelControl53.Size = new System.Drawing.Size(50, 13);
            this.labelControl53.TabIndex = 10190;
            this.labelControl53.Text = "Fabricante";
            // 
            // labTipoInscricao
            // 
            this.labTipoInscricao.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTipoInscricao.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labTipoInscricao.Location = new System.Drawing.Point(315, 176);
            this.labTipoInscricao.Name = "labTipoInscricao";
            this.labTipoInscricao.Size = new System.Drawing.Size(45, 13);
            this.labTipoInscricao.TabIndex = 10186;
            this.labTipoInscricao.Text = "Categoria";
            // 
            // FormRelatorioMovimentacaoItens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 291);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl16);
            this.Controls.Add(this.labelControl53);
            this.Controls.Add(this.labTipoInscricao);
            this.Controls.Add(this.cboGrupos);
            this.Controls.Add(this.cboSubGrupos);
            this.Controls.Add(this.cboMarcas);
            this.Controls.Add(this.cboCategorias);
            this.Controls.Add(this.cboFabricantes);
            this.Controls.Add(this.btnPesquisaProduto2);
            this.Controls.Add(this.labelControl28);
            this.Controls.Add(this.labelControl29);
            this.Controls.Add(this.txtDescricaoProduto);
            this.Controls.Add(this.labDescricao);
            this.Controls.Add(this.txtCodigoDeBarrasProduto);
            this.Controls.Add(this.txtIdProduto);
            this.Controls.Add(this.txtDataFinalMovimentacao);
            this.Controls.Add(this.labelControl60);
            this.Controls.Add(this.txtDataInicialMovimentacao);
            this.Controls.Add(this.labelControl61);
            this.Controls.Add(this.labelControl59);
            this.Controls.Add(this.cboOrigemMovimentacao);
            this.Controls.Add(this.labelControl58);
            this.Controls.Add(this.cboTipoMovimentacao);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnImprimir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRelatorioMovimentacaoItens";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório Movimentação Itens";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalMovimentacao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalMovimentacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialMovimentacao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialMovimentacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOrigemMovimentacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoMovimentacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoProduto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoDeBarrasProduto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdProduto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGrupos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubGrupos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMarcas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategorias.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFabricantes.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Button btnImprimir;
        private DevExpress.XtraEditors.DateEdit txtDataFinalMovimentacao;
        private DevExpress.XtraEditors.LabelControl labelControl60;
        private DevExpress.XtraEditors.DateEdit txtDataInicialMovimentacao;
        private DevExpress.XtraEditors.LabelControl labelControl61;
        private DevExpress.XtraEditors.LabelControl labelControl59;
        private DevExpress.XtraEditors.LookUpEdit cboOrigemMovimentacao;
        private DevExpress.XtraEditors.LabelControl labelControl58;
        private DevExpress.XtraEditors.LookUpEdit cboTipoMovimentacao;
        private System.Windows.Forms.Button btnPesquisaProduto2;
        private DevExpress.XtraEditors.LabelControl labelControl28;
        private DevExpress.XtraEditors.LabelControl labelControl29;
        private DevExpress.XtraEditors.TextEdit txtDescricaoProduto;
        private DevExpress.XtraEditors.LabelControl labDescricao;
        private DevExpress.XtraEditors.TextEdit txtCodigoDeBarrasProduto;
        private DevExpress.XtraEditors.TextEdit txtIdProduto;
        private DevExpress.XtraEditors.LookUpEdit cboGrupos;
        private DevExpress.XtraEditors.LookUpEdit cboSubGrupos;
        private DevExpress.XtraEditors.LookUpEdit cboMarcas;
        private DevExpress.XtraEditors.LookUpEdit cboCategorias;
        private DevExpress.XtraEditors.LookUpEdit cboFabricantes;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl labelControl53;
        private DevExpress.XtraEditors.LabelControl labTipoInscricao;
    }
}