namespace Programax.Easy.View.Telas.Cadastros.NaturezasOperacoes
{
    partial class FormCadastroNaturezaOperacao
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
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.txtId = new DevExpress.XtraEditors.TextEdit();
            this.pbPesquisaPessoa = new System.Windows.Forms.PictureBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricao = new DevExpress.XtraEditors.TextEdit();
            this.labCodigo = new DevExpress.XtraEditors.LabelControl();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtDataCadastro = new DevExpress.XtraEditors.TextEdit();
            this.cboTipoMovimentacao = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboOrigemDestino = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroPlanoDeContas = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.btnPesquisaPlanoDeContas = new System.Windows.Forms.PictureBox();
            this.txtDescricaoPlanoContas = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkObrigatorioExistirPedidoVenda = new DevExpress.XtraEditors.CheckEdit();
            this.chkRealizaMovimentacaoEstoque = new DevExpress.XtraEditors.CheckEdit();
            this.chkGeraTitulosFinanceiro = new DevExpress.XtraEditors.CheckEdit();
            this.cboCfop = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnExcluirCfop = new System.Windows.Forms.Button();
            this.btnInserirCfop = new System.Windows.Forms.Button();
            this.gcCfop = new DevExpress.XtraGrid.GridControl();
            this.gridControl2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCfop = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDescricao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoMovimentacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOrigemDestino.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPlanoDeContas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaPlanoDeContas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoPlanoContas.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkObrigatorioExistirPedidoVenda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRealizaMovimentacaoEstoque.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGeraTitulosFinanceiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCfop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCfop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Location = new System.Drawing.Point(7, 0);
            this.painelBotoes.Size = new System.Drawing.Size(523, 60);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.gcCfop);
            this.panelConteudo.Controls.Add(this.btnExcluirCfop);
            this.panelConteudo.Controls.Add(this.btnInserirCfop);
            this.panelConteudo.Controls.Add(this.cboCfop);
            this.panelConteudo.Controls.Add(this.labelControl4);
            this.panelConteudo.Controls.Add(this.groupBox1);
            this.panelConteudo.Controls.Add(this.txtNumeroPlanoDeContas);
            this.panelConteudo.Controls.Add(this.labelControl10);
            this.panelConteudo.Controls.Add(this.btnPesquisaPlanoDeContas);
            this.panelConteudo.Controls.Add(this.txtDescricaoPlanoContas);
            this.panelConteudo.Controls.Add(this.labelControl9);
            this.panelConteudo.Controls.Add(this.cboOrigemDestino);
            this.panelConteudo.Controls.Add(this.labelControl3);
            this.panelConteudo.Controls.Add(this.cboTipoMovimentacao);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.txtDataCadastro);
            this.panelConteudo.Controls.Add(this.labCodigo);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.txtDescricao);
            this.panelConteudo.Controls.Add(this.labDataCadastro);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Controls.Add(this.pbPesquisaPessoa);
            this.panelConteudo.Controls.Add(this.txtId);
            this.panelConteudo.Size = new System.Drawing.Size(711, 402);
            // 
            // btnGravar
            // 
            this.btnGravar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGravar.FlatAppearance.BorderSize = 0;
            this.btnGravar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGravar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGravar.Image = global::Programax.Easy.View.Properties.Resources.iconSalvar;
            this.btnGravar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnGravar.Location = new System.Drawing.Point(0, 0);
            this.btnGravar.Margin = new System.Windows.Forms.Padding(0);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(100, 40);
            this.btnGravar.TabIndex = 997;
            this.btnGravar.Text = " Salvar";
            this.btnGravar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.FlatAppearance.BorderSize = 0;
            this.btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.Image = global::Programax.Easy.View.Properties.Resources.iconLimpar;
            this.btnLimpar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnLimpar.Location = new System.Drawing.Point(100, 0);
            this.btnLimpar.Margin = new System.Windows.Forms.Padding(0);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(100, 40);
            this.btnLimpar.TabIndex = 1000;
            this.btnLimpar.Text = " Limpar";
            this.btnLimpar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(200, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 999;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // txtId
            // 
            this.txtId.EnterMoveNextControl = true;
            this.txtId.Location = new System.Drawing.Point(3, 18);
            this.txtId.Name = "txtId";
            this.txtId.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Properties.Appearance.Options.UseFont = true;
            this.txtId.Properties.Appearance.Options.UseTextOptions = true;
            this.txtId.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtId.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtId.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtId.Properties.Mask.EditMask = "99/";
            this.txtId.Properties.MaxLength = 8;
            this.txtId.Size = new System.Drawing.Size(96, 22);
            this.txtId.TabIndex = 1;
            this.txtId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtId_KeyPress);
            this.txtId.Leave += new System.EventHandler(this.txtId_Leave);
            // 
            // pbPesquisaPessoa
            // 
            this.pbPesquisaPessoa.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaPessoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaPessoa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaPessoa.Location = new System.Drawing.Point(107, 17);
            this.pbPesquisaPessoa.Name = "pbPesquisaPessoa";
            this.pbPesquisaPessoa.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaPessoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaPessoa.TabIndex = 488;
            this.pbPesquisaPessoa.TabStop = false;
            this.pbPesquisaPessoa.Click += new System.EventHandler(this.pbPesquisaPessoa_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(138, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 13);
            this.labelControl1.TabIndex = 490;
            this.labelControl1.Text = "Descrição";
            // 
            // txtDescricao
            // 
            this.txtDescricao.EnterMoveNextControl = true;
            this.txtDescricao.Location = new System.Drawing.Point(138, 18);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.Properties.Appearance.Options.UseFont = true;
            this.txtDescricao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricao.Properties.Mask.EditMask = "99/";
            this.txtDescricao.Properties.MaxLength = 200;
            this.txtDescricao.Size = new System.Drawing.Size(354, 22);
            this.txtDescricao.TabIndex = 2;
            // 
            // labCodigo
            // 
            this.labCodigo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCodigo.Location = new System.Drawing.Point(3, 3);
            this.labCodigo.Name = "labCodigo";
            this.labCodigo.Size = new System.Drawing.Size(67, 13);
            this.labCodigo.TabIndex = 487;
            this.labCodigo.Text = "Cód. Cadastro";
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(605, 18);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboStatus.Properties.DropDownRows = 2;
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboStatus.Size = new System.Drawing.Size(104, 22);
            this.cboStatus.TabIndex = 4;
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Location = new System.Drawing.Point(498, 3);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(56, 13);
            this.labDataCadastro.TabIndex = 494;
            this.labDataCadastro.Text = "Dt Cadastro";
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(605, 3);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 493;
            this.labStatus.Text = "Status";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnGravar);
            this.flowLayoutPanel1.Controls.Add(this.btnLimpar);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(495, 42);
            this.flowLayoutPanel1.TabIndex = 1001;
            // 
            // txtDataCadastro
            // 
            this.txtDataCadastro.EnterMoveNextControl = true;
            this.txtDataCadastro.Location = new System.Drawing.Point(498, 18);
            this.txtDataCadastro.Name = "txtDataCadastro";
            this.txtDataCadastro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataCadastro.Properties.Appearance.Options.UseFont = true;
            this.txtDataCadastro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDataCadastro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDataCadastro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataCadastro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDataCadastro.Properties.Mask.EditMask = "99/";
            this.txtDataCadastro.Properties.MaxLength = 50;
            this.txtDataCadastro.Properties.ReadOnly = true;
            this.txtDataCadastro.Size = new System.Drawing.Size(101, 22);
            this.txtDataCadastro.TabIndex = 3;
            this.txtDataCadastro.TabStop = false;
            // 
            // cboTipoMovimentacao
            // 
            this.cboTipoMovimentacao.EnterMoveNextControl = true;
            this.cboTipoMovimentacao.Location = new System.Drawing.Point(3, 63);
            this.cboTipoMovimentacao.Name = "cboTipoMovimentacao";
            this.cboTipoMovimentacao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoMovimentacao.Properties.Appearance.Options.UseFont = true;
            this.cboTipoMovimentacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTipoMovimentacao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboTipoMovimentacao.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboTipoMovimentacao.Properties.DropDownRows = 2;
            this.cboTipoMovimentacao.Properties.NullText = "";
            this.cboTipoMovimentacao.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboTipoMovimentacao.Size = new System.Drawing.Size(126, 22);
            this.cboTipoMovimentacao.TabIndex = 5;
            this.cboTipoMovimentacao.EditValueChanged += new System.EventHandler(this.cboTipoMovimentacao_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(3, 48);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(94, 13);
            this.labelControl2.TabIndex = 497;
            this.labelControl2.Text = "Tipo Movimentação";
            // 
            // cboOrigemDestino
            // 
            this.cboOrigemDestino.EnterMoveNextControl = true;
            this.cboOrigemDestino.Location = new System.Drawing.Point(138, 63);
            this.cboOrigemDestino.Name = "cboOrigemDestino";
            this.cboOrigemDestino.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOrigemDestino.Properties.Appearance.Options.UseFont = true;
            this.cboOrigemDestino.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboOrigemDestino.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboOrigemDestino.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboOrigemDestino.Properties.DropDownRows = 3;
            this.cboOrigemDestino.Properties.NullText = "";
            this.cboOrigemDestino.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboOrigemDestino.Size = new System.Drawing.Size(126, 22);
            this.cboOrigemDestino.TabIndex = 6;
            this.cboOrigemDestino.EditValueChanged += new System.EventHandler(this.cboOrigemDestino_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(138, 48);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(74, 13);
            this.labelControl3.TabIndex = 499;
            this.labelControl3.Text = "Origem/Destino";
            // 
            // txtNumeroPlanoDeContas
            // 
            this.txtNumeroPlanoDeContas.EnterMoveNextControl = true;
            this.txtNumeroPlanoDeContas.Location = new System.Drawing.Point(270, 63);
            this.txtNumeroPlanoDeContas.Name = "txtNumeroPlanoDeContas";
            this.txtNumeroPlanoDeContas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroPlanoDeContas.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroPlanoDeContas.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNumeroPlanoDeContas.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNumeroPlanoDeContas.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroPlanoDeContas.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroPlanoDeContas.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNumeroPlanoDeContas.Properties.Mask.EditMask = "([0-9]{1})(([\\.][0-9]{2})(([\\.][0-9]{3})([\\.][0-9]{4})?)?)?";
            this.txtNumeroPlanoDeContas.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtNumeroPlanoDeContas.Properties.MaxLength = 50;
            this.txtNumeroPlanoDeContas.Size = new System.Drawing.Size(98, 22);
            this.txtNumeroPlanoDeContas.TabIndex = 7;
            this.txtNumeroPlanoDeContas.Leave += new System.EventHandler(this.txtNumeroPlanoDeContas_Leave);
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl10.Location = new System.Drawing.Point(270, 47);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(95, 13);
            this.labelControl10.TabIndex = 753;
            this.labelControl10.Text = "Nr. Plano de Contas";
            // 
            // btnPesquisaPlanoDeContas
            // 
            this.btnPesquisaPlanoDeContas.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaPlanoDeContas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaPlanoDeContas.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaPlanoDeContas.Location = new System.Drawing.Point(374, 62);
            this.btnPesquisaPlanoDeContas.Name = "btnPesquisaPlanoDeContas";
            this.btnPesquisaPlanoDeContas.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaPlanoDeContas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaPlanoDeContas.TabIndex = 754;
            this.btnPesquisaPlanoDeContas.TabStop = false;
            this.btnPesquisaPlanoDeContas.Click += new System.EventHandler(this.btnPesquisaPlanoDeContas_Click);
            // 
            // txtDescricaoPlanoContas
            // 
            this.txtDescricaoPlanoContas.EnterMoveNextControl = true;
            this.txtDescricaoPlanoContas.Location = new System.Drawing.Point(402, 63);
            this.txtDescricaoPlanoContas.Name = "txtDescricaoPlanoContas";
            this.txtDescricaoPlanoContas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricaoPlanoContas.Properties.Appearance.Options.UseFont = true;
            this.txtDescricaoPlanoContas.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricaoPlanoContas.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricaoPlanoContas.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricaoPlanoContas.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricaoPlanoContas.Properties.Mask.EditMask = "99/";
            this.txtDescricaoPlanoContas.Properties.ReadOnly = true;
            this.txtDescricaoPlanoContas.Size = new System.Drawing.Size(307, 22);
            this.txtDescricaoPlanoContas.TabIndex = 752;
            this.txtDescricaoPlanoContas.TabStop = false;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Location = new System.Drawing.Point(402, 47);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(129, 13);
            this.labelControl9.TabIndex = 755;
            this.labelControl9.Text = "Descrição Plano de Contas";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkObrigatorioExistirPedidoVenda);
            this.groupBox1.Controls.Add(this.chkRealizaMovimentacaoEstoque);
            this.groupBox1.Controls.Add(this.chkGeraTitulosFinanceiro);
            this.groupBox1.Location = new System.Drawing.Point(3, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(706, 47);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parâmetros da Movimentação";
            // 
            // chkObrigatorioExistirPedidoVenda
            // 
            this.chkObrigatorioExistirPedidoVenda.EnterMoveNextControl = true;
            this.chkObrigatorioExistirPedidoVenda.Location = new System.Drawing.Point(513, 22);
            this.chkObrigatorioExistirPedidoVenda.Name = "chkObrigatorioExistirPedidoVenda";
            this.chkObrigatorioExistirPedidoVenda.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkObrigatorioExistirPedidoVenda.Properties.Appearance.Options.UseFont = true;
            this.chkObrigatorioExistirPedidoVenda.Properties.Caption = "Obrigatório Existir Pedido de Venda";
            this.chkObrigatorioExistirPedidoVenda.Size = new System.Drawing.Size(192, 19);
            this.chkObrigatorioExistirPedidoVenda.TabIndex = 11;
            // 
            // chkRealizaMovimentacaoEstoque
            // 
            this.chkRealizaMovimentacaoEstoque.EnterMoveNextControl = true;
            this.chkRealizaMovimentacaoEstoque.Location = new System.Drawing.Point(243, 22);
            this.chkRealizaMovimentacaoEstoque.Name = "chkRealizaMovimentacaoEstoque";
            this.chkRealizaMovimentacaoEstoque.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRealizaMovimentacaoEstoque.Properties.Appearance.Options.UseFont = true;
            this.chkRealizaMovimentacaoEstoque.Properties.Caption = "Realiza Movimentação no Estoque";
            this.chkRealizaMovimentacaoEstoque.Size = new System.Drawing.Size(190, 19);
            this.chkRealizaMovimentacaoEstoque.TabIndex = 10;
            // 
            // chkGeraTitulosFinanceiro
            // 
            this.chkGeraTitulosFinanceiro.EnterMoveNextControl = true;
            this.chkGeraTitulosFinanceiro.Location = new System.Drawing.Point(6, 22);
            this.chkGeraTitulosFinanceiro.Name = "chkGeraTitulosFinanceiro";
            this.chkGeraTitulosFinanceiro.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGeraTitulosFinanceiro.Properties.Appearance.Options.UseFont = true;
            this.chkGeraTitulosFinanceiro.Properties.Caption = "Gera Títulos no Financeiro";
            this.chkGeraTitulosFinanceiro.Size = new System.Drawing.Size(162, 19);
            this.chkGeraTitulosFinanceiro.TabIndex = 9;
            // 
            // cboCfop
            // 
            this.cboCfop.Location = new System.Drawing.Point(3, 169);
            this.cboCfop.Name = "cboCfop";
            this.cboCfop.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCfop.Properties.Appearance.Options.UseFont = true;
            this.cboCfop.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCfop.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboCfop.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboCfop.Properties.NullText = "";
            this.cboCfop.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboCfop.Size = new System.Drawing.Size(632, 22);
            this.cboCfop.TabIndex = 12;
            this.cboCfop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboCfop_KeyDown);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(3, 154);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(22, 13);
            this.labelControl4.TabIndex = 758;
            this.labelControl4.Text = "Cfop";
            // 
            // btnExcluirCfop
            // 
            this.btnExcluirCfop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluirCfop.FlatAppearance.BorderSize = 0;
            this.btnExcluirCfop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExcluirCfop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExcluirCfop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluirCfop.Image = global::Programax.Easy.View.Properties.Resources.icones2_18;
            this.btnExcluirCfop.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcluirCfop.Location = new System.Drawing.Point(675, 168);
            this.btnExcluirCfop.Name = "btnExcluirCfop";
            this.btnExcluirCfop.Size = new System.Drawing.Size(28, 23);
            this.btnExcluirCfop.TabIndex = 14;
            this.btnExcluirCfop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluirCfop.UseVisualStyleBackColor = true;
            this.btnExcluirCfop.Click += new System.EventHandler(this.btnExcluirCfop_Click);
            // 
            // btnInserirCfop
            // 
            this.btnInserirCfop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInserirCfop.FlatAppearance.BorderSize = 0;
            this.btnInserirCfop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnInserirCfop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnInserirCfop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInserirCfop.Image = global::Programax.Easy.View.Properties.Resources.icones2_19;
            this.btnInserirCfop.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInserirCfop.Location = new System.Drawing.Point(641, 168);
            this.btnInserirCfop.Name = "btnInserirCfop";
            this.btnInserirCfop.Size = new System.Drawing.Size(28, 23);
            this.btnInserirCfop.TabIndex = 13;
            this.btnInserirCfop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInserirCfop.UseVisualStyleBackColor = true;
            this.btnInserirCfop.Click += new System.EventHandler(this.btnInserirCfop_Click);
            // 
            // gcCfop
            // 
            this.gcCfop.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcCfop.Location = new System.Drawing.Point(3, 197);
            this.gcCfop.MainView = this.gridControl2;
            this.gcCfop.Name = "gcCfop";
            this.gcCfop.Size = new System.Drawing.Size(705, 201);
            this.gcCfop.TabIndex = 15;
            this.gcCfop.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridControl2,
            this.gridView3});
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
            this.colunaCfop,
            this.colunaDescricao,
            this.colunaStatus});
            this.gridControl2.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridControl2.GridControl = this.gcCfop;
            this.gridControl2.GroupPanelText = "Enderecos";
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.OptionsView.ShowGroupPanel = false;
            this.gridControl2.OptionsView.ShowIndicator = false;
            this.gridControl2.OptionsView.ShowViewCaption = true;
            this.gridControl2.PaintStyleName = "Skin";
            this.gridControl2.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaCfop, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridControl2.ViewCaption = "Cfops";
            // 
            // colunaId
            // 
            this.colunaId.Caption = "Id";
            this.colunaId.FieldName = "Id";
            this.colunaId.Name = "colunaId";
            this.colunaId.OptionsColumn.AllowEdit = false;
            this.colunaId.OptionsColumn.AllowFocus = false;
            this.colunaId.OptionsFilter.AllowFilter = false;
            // 
            // colunaCfop
            // 
            this.colunaCfop.AppearanceCell.Options.UseTextOptions = true;
            this.colunaCfop.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaCfop.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaCfop.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaCfop.Caption = "CFOP";
            this.colunaCfop.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaCfop.FieldName = "Cfop";
            this.colunaCfop.MinWidth = 45;
            this.colunaCfop.Name = "colunaCfop";
            this.colunaCfop.OptionsColumn.AllowEdit = false;
            this.colunaCfop.OptionsColumn.AllowFocus = false;
            this.colunaCfop.OptionsFilter.AllowAutoFilter = false;
            this.colunaCfop.OptionsFilter.AllowFilter = false;
            this.colunaCfop.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "tele_id", "{0}")});
            this.colunaCfop.Visible = true;
            this.colunaCfop.VisibleIndex = 0;
            this.colunaCfop.Width = 99;
            // 
            // colunaDescricao
            // 
            this.colunaDescricao.AppearanceCell.Options.UseTextOptions = true;
            this.colunaDescricao.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaDescricao.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaDescricao.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaDescricao.Caption = "Descrição";
            this.colunaDescricao.FieldName = "Descricao";
            this.colunaDescricao.Name = "colunaDescricao";
            this.colunaDescricao.OptionsColumn.AllowEdit = false;
            this.colunaDescricao.OptionsColumn.AllowFocus = false;
            this.colunaDescricao.OptionsFilter.AllowAutoFilter = false;
            this.colunaDescricao.OptionsFilter.AllowFilter = false;
            this.colunaDescricao.Visible = true;
            this.colunaDescricao.VisibleIndex = 1;
            this.colunaDescricao.Width = 673;
            // 
            // colunaStatus
            // 
            this.colunaStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colunaStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaStatus.Caption = "Status";
            this.colunaStatus.FieldName = "Status";
            this.colunaStatus.MinWidth = 10;
            this.colunaStatus.Name = "colunaStatus";
            this.colunaStatus.OptionsColumn.AllowEdit = false;
            this.colunaStatus.OptionsColumn.AllowFocus = false;
            this.colunaStatus.OptionsFilter.AllowAutoFilter = false;
            this.colunaStatus.OptionsFilter.AllowFilter = false;
            this.colunaStatus.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.colunaStatus.Visible = true;
            this.colunaStatus.VisibleIndex = 2;
            this.colunaStatus.Width = 122;
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gcCfop;
            this.gridView3.Name = "gridView3";
            // 
            // FormCadastroNaturezaOperacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 513);
            this.Name = "FormCadastroNaturezaOperacao";
            this.Load += new System.EventHandler(this.FormCadastroNaturezaOperacao_Load);
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoMovimentacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOrigemDestino.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPlanoDeContas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaPlanoDeContas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoPlanoContas.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkObrigatorioExistirPedidoVenda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRealizaMovimentacaoEstoque.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGeraTitulosFinanceiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCfop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCfop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnSair;
        private DevExpress.XtraEditors.TextEdit txtId;
        private System.Windows.Forms.PictureBox pbPesquisaPessoa;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDescricao;
        private DevExpress.XtraEditors.LabelControl labCodigo;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.TextEdit txtDataCadastro;
        private DevExpress.XtraEditors.LookUpEdit cboOrigemDestino;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit cboTipoMovimentacao;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtNumeroPlanoDeContas;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private System.Windows.Forms.PictureBox btnPesquisaPlanoDeContas;
        private DevExpress.XtraEditors.TextEdit txtDescricaoPlanoContas;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.CheckEdit chkObrigatorioExistirPedidoVenda;
        private DevExpress.XtraEditors.CheckEdit chkRealizaMovimentacaoEstoque;
        private DevExpress.XtraEditors.CheckEdit chkGeraTitulosFinanceiro;
        private DevExpress.XtraEditors.LookUpEdit cboCfop;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.Button btnExcluirCfop;
        private System.Windows.Forms.Button btnInserirCfop;
        private DevExpress.XtraGrid.GridControl gcCfop;
        private DevExpress.XtraGrid.Views.Grid.GridView gridControl2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCfop;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
    }
}