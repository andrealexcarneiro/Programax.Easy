namespace Programax.Easy.View.Telas.Financeiro.FormasPagamento
{
    partial class FormCadastroFormaPagamento
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCadastroFormaPagamento));
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricao = new DevExpress.XtraEditors.TextEdit();
            this.labCodigo = new DevExpress.XtraEditors.LabelControl();
            this.chkDisponivelParaPdv = new DevExpress.XtraEditors.CheckEdit();
            this.chkDisponivelParaContasAReceber = new DevExpress.XtraEditors.CheckEdit();
            this.chkDisponivelParaContasAPagar = new DevExpress.XtraEditors.CheckEdit();
            this.pbPesquisaFormaPagamento = new System.Windows.Forms.PictureBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cboCondicaoPagamento = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl34 = new DevExpress.XtraEditors.LabelControl();
            this.gcItens = new DevExpress.XtraGrid.GridControl();
            this.gridControl2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDescricaoCondicaoPagamento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnExcluirCondicao = new System.Windows.Forms.Button();
            this.btnInserirCondicao = new System.Windows.Forms.Button();
            this.chkDisponivelPedidoVenda = new DevExpress.XtraEditors.CheckEdit();
            this.txtDataCadastro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.pnlInformacoesCadastrais2 = new System.Windows.Forms.Panel();
            this.pnlInformacoesCadastrais1 = new System.Windows.Forms.Panel();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisponivelParaPdv.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisponivelParaContasAReceber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisponivelParaContasAPagar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaFormaPagamento)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboCondicaoPagamento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisponivelPedidoVenda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).BeginInit();
            this.pnlInformacoesCadastrais2.SuspendLayout();
            this.pnlInformacoesCadastrais1.SuspendLayout();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Location = new System.Drawing.Point(7, 0);
            this.painelBotoes.Size = new System.Drawing.Size(452, 40);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.pnlInformacoesCadastrais1);
            this.panelConteudo.Controls.Add(this.pnlInformacoesCadastrais2);
            this.panelConteudo.Controls.Add(this.txtId);
            this.panelConteudo.Controls.Add(this.labCodigo);
            this.panelConteudo.Controls.Add(this.pbPesquisaFormaPagamento);
            this.panelConteudo.Size = new System.Drawing.Size(544, 304);
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(211, 17);
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
            this.cboStatus.Size = new System.Drawing.Size(109, 22);
            this.cboStatus.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(211, 1);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(30, 13);
            this.labelControl3.TabIndex = 505;
            this.labelControl3.Text = "Status";
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
            this.txtId.Leave += new System.EventHandler(this.txtId_Leave);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(2, 1);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 13);
            this.labelControl1.TabIndex = 503;
            this.labelControl1.Text = "Descrição";
            // 
            // txtDescricao
            // 
            this.txtDescricao.EnterMoveNextControl = true;
            this.txtDescricao.Location = new System.Drawing.Point(2, 17);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.Properties.Appearance.Options.UseFont = true;
            this.txtDescricao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricao.Properties.Mask.EditMask = "99/";
            this.txtDescricao.Properties.MaxLength = 50;
            this.txtDescricao.Size = new System.Drawing.Size(203, 22);
            this.txtDescricao.TabIndex = 2;
            // 
            // labCodigo
            // 
            this.labCodigo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCodigo.Location = new System.Drawing.Point(3, 2);
            this.labCodigo.Name = "labCodigo";
            this.labCodigo.Size = new System.Drawing.Size(67, 13);
            this.labCodigo.TabIndex = 501;
            this.labCodigo.Text = "Cód. Cadastro";
            // 
            // chkDisponivelParaPdv
            // 
            this.chkDisponivelParaPdv.EnterMoveNextControl = true;
            this.chkDisponivelParaPdv.Location = new System.Drawing.Point(214, 228);
            this.chkDisponivelParaPdv.Name = "chkDisponivelParaPdv";
            this.chkDisponivelParaPdv.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisponivelParaPdv.Properties.Appearance.Options.UseFont = true;
            this.chkDisponivelParaPdv.Properties.Caption = "Usa Contas a Receber";
            this.chkDisponivelParaPdv.Size = new System.Drawing.Size(129, 19);
            this.chkDisponivelParaPdv.TabIndex = 10;
            // 
            // chkDisponivelParaContasAReceber
            // 
            this.chkDisponivelParaContasAReceber.EnterMoveNextControl = true;
            this.chkDisponivelParaContasAReceber.Location = new System.Drawing.Point(81, 228);
            this.chkDisponivelParaContasAReceber.Name = "chkDisponivelParaContasAReceber";
            this.chkDisponivelParaContasAReceber.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisponivelParaContasAReceber.Properties.Appearance.Options.UseFont = true;
            this.chkDisponivelParaContasAReceber.Properties.Caption = "Usa Contas a Pagar";
            this.chkDisponivelParaContasAReceber.Size = new System.Drawing.Size(118, 19);
            this.chkDisponivelParaContasAReceber.TabIndex = 9;
            // 
            // chkDisponivelParaContasAPagar
            // 
            this.chkDisponivelParaContasAPagar.EnterMoveNextControl = true;
            this.chkDisponivelParaContasAPagar.Location = new System.Drawing.Point(0, 228);
            this.chkDisponivelParaContasAPagar.Name = "chkDisponivelParaContasAPagar";
            this.chkDisponivelParaContasAPagar.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisponivelParaContasAPagar.Properties.Appearance.Options.UseFont = true;
            this.chkDisponivelParaContasAPagar.Properties.Caption = "Usa PDV";
            this.chkDisponivelParaContasAPagar.Size = new System.Drawing.Size(75, 19);
            this.chkDisponivelParaContasAPagar.TabIndex = 8;
            // 
            // pbPesquisaFormaPagamento
            // 
            this.pbPesquisaFormaPagamento.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaFormaPagamento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaFormaPagamento.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaFormaPagamento.Location = new System.Drawing.Point(105, 18);
            this.pbPesquisaFormaPagamento.Name = "pbPesquisaFormaPagamento";
            this.pbPesquisaFormaPagamento.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaFormaPagamento.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaFormaPagamento.TabIndex = 502;
            this.pbPesquisaFormaPagamento.TabStop = false;
            this.pbPesquisaFormaPagamento.Click += new System.EventHandler(this.pbPesquisaFormaPagamento_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(200, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 1039;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.FlatAppearance.BorderSize = 0;
            this.btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpar.Image = global::Programax.Easy.View.Properties.Resources.iconLimpar;
            this.btnLimpar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnLimpar.Location = new System.Drawing.Point(100, 0);
            this.btnLimpar.Margin = new System.Windows.Forms.Padding(0);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(100, 40);
            this.btnLimpar.TabIndex = 1038;
            this.btnLimpar.Text = " Limpar";
            this.btnLimpar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Image = global::Programax.Easy.View.Properties.Resources.iconSalvar;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSalvar.Location = new System.Drawing.Point(0, 0);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(0);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 40);
            this.btnSalvar.TabIndex = 1037;
            this.btnSalvar.Text = " Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSalvar);
            this.flowLayoutPanel1.Controls.Add(this.btnLimpar);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(451, 53);
            this.flowLayoutPanel1.TabIndex = 1040;
            // 
            // cboCondicaoPagamento
            // 
            this.cboCondicaoPagamento.EnterMoveNextControl = true;
            this.cboCondicaoPagamento.Location = new System.Drawing.Point(0, 22);
            this.cboCondicaoPagamento.Name = "cboCondicaoPagamento";
            this.cboCondicaoPagamento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCondicaoPagamento.Properties.Appearance.Options.UseFont = true;
            this.cboCondicaoPagamento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCondicaoPagamento.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboCondicaoPagamento.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboCondicaoPagamento.Properties.NullText = "";
            this.cboCondicaoPagamento.Size = new System.Drawing.Size(470, 22);
            this.cboCondicaoPagamento.TabIndex = 4;
            // 
            // labelControl34
            // 
            this.labelControl34.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl34.Location = new System.Drawing.Point(0, 3);
            this.labelControl34.Name = "labelControl34";
            this.labelControl34.Size = new System.Drawing.Size(117, 13);
            this.labelControl34.TabIndex = 10009;
            this.labelControl34.Text = "Condição de Pagamento";
            // 
            // gcItens
            // 
            this.gcItens.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcItens.Location = new System.Drawing.Point(0, 50);
            this.gcItens.MainView = this.gridControl2;
            this.gcItens.Name = "gcItens";
            this.gcItens.Size = new System.Drawing.Size(538, 172);
            this.gcItens.TabIndex = 7;
            this.gcItens.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
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
            this.colunaDescricaoCondicaoPagamento});
            this.gridControl2.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridControl2.GridControl = this.gcItens;
            this.gridControl2.GroupPanelText = "Enderecos";
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.OptionsView.ShowGroupPanel = false;
            this.gridControl2.OptionsView.ShowIndicator = false;
            this.gridControl2.OptionsView.ShowViewCaption = true;
            this.gridControl2.PaintStyleName = "Skin";
            this.gridControl2.ViewCaption = "Condições de Pagamento";
            // 
            // colunaId
            // 
            this.colunaId.Caption = "ID";
            this.colunaId.FieldName = "Id";
            this.colunaId.Name = "colunaId";
            // 
            // colunaDescricaoCondicaoPagamento
            // 
            this.colunaDescricaoCondicaoPagamento.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaDescricaoCondicaoPagamento.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaDescricaoCondicaoPagamento.Caption = "Descrição Condição de Pagamento";
            this.colunaDescricaoCondicaoPagamento.FieldName = "Descricao";
            this.colunaDescricaoCondicaoPagamento.Name = "colunaDescricaoCondicaoPagamento";
            this.colunaDescricaoCondicaoPagamento.OptionsColumn.AllowEdit = false;
            this.colunaDescricaoCondicaoPagamento.OptionsColumn.AllowFocus = false;
            this.colunaDescricaoCondicaoPagamento.OptionsFilter.AllowAutoFilter = false;
            this.colunaDescricaoCondicaoPagamento.OptionsFilter.AllowFilter = false;
            this.colunaDescricaoCondicaoPagamento.Visible = true;
            this.colunaDescricaoCondicaoPagamento.VisibleIndex = 0;
            this.colunaDescricaoCondicaoPagamento.Width = 50;
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gcItens;
            this.gridView3.Name = "gridView3";
            // 
            // btnExcluirCondicao
            // 
            this.btnExcluirCondicao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluirCondicao.FlatAppearance.BorderSize = 0;
            this.btnExcluirCondicao.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExcluirCondicao.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExcluirCondicao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluirCondicao.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluirCondicao.Image")));
            this.btnExcluirCondicao.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcluirCondicao.Location = new System.Drawing.Point(510, 21);
            this.btnExcluirCondicao.Name = "btnExcluirCondicao";
            this.btnExcluirCondicao.Size = new System.Drawing.Size(28, 23);
            this.btnExcluirCondicao.TabIndex = 6;
            this.btnExcluirCondicao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluirCondicao.UseVisualStyleBackColor = true;
            this.btnExcluirCondicao.Click += new System.EventHandler(this.btnExcluirCondicao_Click);
            // 
            // btnInserirCondicao
            // 
            this.btnInserirCondicao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInserirCondicao.FlatAppearance.BorderSize = 0;
            this.btnInserirCondicao.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnInserirCondicao.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnInserirCondicao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInserirCondicao.Image = ((System.Drawing.Image)(resources.GetObject("btnInserirCondicao.Image")));
            this.btnInserirCondicao.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInserirCondicao.Location = new System.Drawing.Point(476, 21);
            this.btnInserirCondicao.Name = "btnInserirCondicao";
            this.btnInserirCondicao.Size = new System.Drawing.Size(28, 23);
            this.btnInserirCondicao.TabIndex = 5;
            this.btnInserirCondicao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInserirCondicao.UseVisualStyleBackColor = true;
            this.btnInserirCondicao.Click += new System.EventHandler(this.btnInserirCondicao_Click);
            // 
            // chkDisponivelPedidoVenda
            // 
            this.chkDisponivelPedidoVenda.EnterMoveNextControl = true;
            this.chkDisponivelPedidoVenda.Location = new System.Drawing.Point(357, 228);
            this.chkDisponivelPedidoVenda.Name = "chkDisponivelPedidoVenda";
            this.chkDisponivelPedidoVenda.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisponivelPedidoVenda.Properties.Appearance.Options.UseFont = true;
            this.chkDisponivelPedidoVenda.Properties.Caption = "Usa no Pedido de Venda";
            this.chkDisponivelPedidoVenda.Size = new System.Drawing.Size(139, 19);
            this.chkDisponivelPedidoVenda.TabIndex = 11;
            // 
            // txtDataCadastro
            // 
            this.txtDataCadastro.Enabled = false;
            this.txtDataCadastro.EnterMoveNextControl = true;
            this.txtDataCadastro.Location = new System.Drawing.Point(326, 17);
            this.txtDataCadastro.Name = "txtDataCadastro";
            this.txtDataCadastro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataCadastro.Properties.Appearance.Options.UseFont = true;
            this.txtDataCadastro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDataCadastro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDataCadastro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataCadastro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDataCadastro.Properties.Mask.EditMask = "99/";
            this.txtDataCadastro.Properties.MaxLength = 30;
            this.txtDataCadastro.Properties.ReadOnly = true;
            this.txtDataCadastro.Size = new System.Drawing.Size(84, 22);
            this.txtDataCadastro.TabIndex = 10018;
            this.txtDataCadastro.TabStop = false;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Location = new System.Drawing.Point(326, 2);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(59, 13);
            this.labelControl9.TabIndex = 10019;
            this.labelControl9.Text = "Dt. Cadastro";
            // 
            // pnlInformacoesCadastrais2
            // 
            this.pnlInformacoesCadastrais2.Controls.Add(this.labelControl34);
            this.pnlInformacoesCadastrais2.Controls.Add(this.chkDisponivelParaContasAPagar);
            this.pnlInformacoesCadastrais2.Controls.Add(this.chkDisponivelParaContasAReceber);
            this.pnlInformacoesCadastrais2.Controls.Add(this.chkDisponivelPedidoVenda);
            this.pnlInformacoesCadastrais2.Controls.Add(this.chkDisponivelParaPdv);
            this.pnlInformacoesCadastrais2.Controls.Add(this.btnExcluirCondicao);
            this.pnlInformacoesCadastrais2.Controls.Add(this.cboCondicaoPagamento);
            this.pnlInformacoesCadastrais2.Controls.Add(this.btnInserirCondicao);
            this.pnlInformacoesCadastrais2.Controls.Add(this.gcItens);
            this.pnlInformacoesCadastrais2.Enabled = false;
            this.pnlInformacoesCadastrais2.Location = new System.Drawing.Point(3, 48);
            this.pnlInformacoesCadastrais2.Name = "pnlInformacoesCadastrais2";
            this.pnlInformacoesCadastrais2.Size = new System.Drawing.Size(541, 255);
            this.pnlInformacoesCadastrais2.TabIndex = 10020;
            // 
            // pnlInformacoesCadastrais1
            // 
            this.pnlInformacoesCadastrais1.Controls.Add(this.txtDescricao);
            this.pnlInformacoesCadastrais1.Controls.Add(this.txtDataCadastro);
            this.pnlInformacoesCadastrais1.Controls.Add(this.labelControl3);
            this.pnlInformacoesCadastrais1.Controls.Add(this.labelControl9);
            this.pnlInformacoesCadastrais1.Controls.Add(this.labelControl1);
            this.pnlInformacoesCadastrais1.Controls.Add(this.cboStatus);
            this.pnlInformacoesCadastrais1.Enabled = false;
            this.pnlInformacoesCadastrais1.Location = new System.Drawing.Point(133, 2);
            this.pnlInformacoesCadastrais1.Name = "pnlInformacoesCadastrais1";
            this.pnlInformacoesCadastrais1.Size = new System.Drawing.Size(411, 41);
            this.pnlInformacoesCadastrais1.TabIndex = 10010;
            // 
            // FormCadastroFormaPagamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 415);
            this.Name = "FormCadastroFormaPagamento";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisponivelParaPdv.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisponivelParaContasAReceber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisponivelParaContasAPagar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaFormaPagamento)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboCondicaoPagamento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisponivelPedidoVenda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).EndInit();
            this.pnlInformacoesCadastrais2.ResumeLayout(false);
            this.pnlInformacoesCadastrais2.PerformLayout();
            this.pnlInformacoesCadastrais1.ResumeLayout(false);
            this.pnlInformacoesCadastrais1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtId;
        private System.Windows.Forms.PictureBox pbPesquisaFormaPagamento;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDescricao;
        private DevExpress.XtraEditors.LabelControl labCodigo;
        private DevExpress.XtraEditors.CheckEdit chkDisponivelParaPdv;
        private DevExpress.XtraEditors.CheckEdit chkDisponivelParaContasAReceber;
        private DevExpress.XtraEditors.CheckEdit chkDisponivelParaContasAPagar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.LookUpEdit cboCondicaoPagamento;
        private DevExpress.XtraEditors.LabelControl labelControl34;
        private DevExpress.XtraGrid.GridControl gcItens;
        private DevExpress.XtraGrid.Views.Grid.GridView gridControl2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricaoCondicaoPagamento;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.Button btnExcluirCondicao;
        private System.Windows.Forms.Button btnInserirCondicao;
        private DevExpress.XtraEditors.CheckEdit chkDisponivelPedidoVenda;
        private DevExpress.XtraEditors.TextEdit txtDataCadastro;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private System.Windows.Forms.Panel pnlInformacoesCadastrais1;
        private System.Windows.Forms.Panel pnlInformacoesCadastrais2;
    }
}
