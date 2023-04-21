namespace Programax.Easy.View.Telas.Financeiro.OperadorasCartoes
{
    partial class FormCadastroOperadorasCartao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCadastroOperadorasCartao));
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.gcOperadoras = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCondicaoPagamento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaParcelaCobrarTaxa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaTaxa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtDataCadastro = new DevExpress.XtraEditors.TextEdit();
            this.chkPermiteParcelamento = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkRecebimentoAntecipado = new System.Windows.Forms.CheckBox();
            this.pbPesquisaBancos = new System.Windows.Forms.PictureBox();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.cboCategoriaDespesa = new DevExpress.XtraEditors.LookUpEdit();
            this.txtBanco = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtPrazoParaCreditar = new DevExpress.XtraEditors.TextEdit();
            this.txtCodigoOperadora = new DevExpress.XtraEditors.TextEdit();
            this.txtDescricao = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pbPesquisaOperadora = new System.Windows.Forms.PictureBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnInserirAtualizarItem = new System.Windows.Forms.Button();
            this.labelControl34 = new DevExpress.XtraEditors.LabelControl();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.cboCondicaoPagamento = new DevExpress.XtraEditors.LookUpEdit();
            this.txtTaxaAdministracao = new Programax.Easy.View.Componentes.AkilTextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtParcelaParaComecarCobrarTaxa = new DevExpress.XtraEditors.TextEdit();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcOperadoras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaBancos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategoriaDespesa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBanco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrazoParaCreditar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoOperadora.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaOperadora)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboCondicaoPagamento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaxaAdministracao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParcelaParaComecarCobrarTaxa.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Location = new System.Drawing.Point(9, 0);
            this.painelBotoes.Size = new System.Drawing.Size(1125, 74);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.groupBox2);
            this.panelConteudo.Controls.Add(this.groupBox1);
            this.panelConteudo.Controls.Add(this.gcOperadoras);
            this.panelConteudo.Margin = new System.Windows.Forms.Padding(5);
            this.panelConteudo.Size = new System.Drawing.Size(1129, 634);
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
            this.btnGravar.Size = new System.Drawing.Size(133, 49);
            this.btnGravar.TabIndex = 11;
            this.btnGravar.Text = " Salvar";
            this.btnGravar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.btnLimpar.Location = new System.Drawing.Point(133, 0);
            this.btnLimpar.Margin = new System.Windows.Forms.Padding(0);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(133, 49);
            this.btnLimpar.TabIndex = 12;
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
            this.btnSair.Location = new System.Drawing.Point(266, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(133, 49);
            this.btnSair.TabIndex = 13;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Appearance.Options.UseFont = true;
            this.labDataCadastro.Location = new System.Drawing.Point(303, 132);
            this.labDataCadastro.Margin = new System.Windows.Forms.Padding(4);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(79, 17);
            this.labDataCadastro.TabIndex = 494;
            this.labDataCadastro.Text = "Dt. Cadastro";
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(9, 152);
            this.cboStatus.Margin = new System.Windows.Forms.Padding(4);
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
            this.cboStatus.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboStatus.Size = new System.Drawing.Size(287, 26);
            this.cboStatus.TabIndex = 9;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(9, 132);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 17);
            this.labelControl3.TabIndex = 498;
            this.labelControl3.Text = "Status";
            // 
            // gcOperadoras
            // 
            this.gcOperadoras.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcOperadoras.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gcOperadoras.Location = new System.Drawing.Point(6, 300);
            this.gcOperadoras.MainView = this.gridView5;
            this.gcOperadoras.Margin = new System.Windows.Forms.Padding(4);
            this.gcOperadoras.Name = "gcOperadoras";
            this.gcOperadoras.Size = new System.Drawing.Size(1115, 316);
            this.gcOperadoras.TabIndex = 504;
            this.gcOperadoras.TabStop = false;
            this.gcOperadoras.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcOperadoras.DoubleClick += new System.EventHandler(this.gcOperadoras_DoubleClick);
            this.gcOperadoras.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcOperadoras_KeyDown);
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
            this.colunaCondicaoPagamento,
            this.colunaParcelaCobrarTaxa,
            this.colunaTaxa});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 288, 219);
            this.gridView5.DetailHeight = 431;
            this.gridView5.GridControl = this.gcOperadoras;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.ViewCaption = "Operadoras de Cartão";
            // 
            // colunaId
            // 
            this.colunaId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.Caption = "Cód Cadastro";
            this.colunaId.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaId.FieldName = "Id";
            this.colunaId.MinWidth = 60;
            this.colunaId.Name = "colunaId";
            this.colunaId.OptionsColumn.AllowEdit = false;
            this.colunaId.OptionsColumn.AllowFocus = false;
            this.colunaId.OptionsFilter.AllowFilter = false;
            this.colunaId.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colunaId.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaId.Width = 111;
            // 
            // colunaCondicaoPagamento
            // 
            this.colunaCondicaoPagamento.AppearanceCell.Options.UseTextOptions = true;
            this.colunaCondicaoPagamento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaCondicaoPagamento.Caption = "Condição de Pagamento";
            this.colunaCondicaoPagamento.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colunaCondicaoPagamento.FieldName = "CondicaoPagamento";
            this.colunaCondicaoPagamento.MinWidth = 13;
            this.colunaCondicaoPagamento.Name = "colunaCondicaoPagamento";
            this.colunaCondicaoPagamento.OptionsColumn.AllowEdit = false;
            this.colunaCondicaoPagamento.OptionsColumn.AllowFocus = false;
            this.colunaCondicaoPagamento.OptionsFilter.AllowFilter = false;
            this.colunaCondicaoPagamento.Visible = true;
            this.colunaCondicaoPagamento.VisibleIndex = 0;
            this.colunaCondicaoPagamento.Width = 188;
            // 
            // colunaParcelaCobrarTaxa
            // 
            this.colunaParcelaCobrarTaxa.Caption = "Parcela para Cobrar Taxa";
            this.colunaParcelaCobrarTaxa.FieldName = "ParcelaCobrarTaxa";
            this.colunaParcelaCobrarTaxa.MinWidth = 27;
            this.colunaParcelaCobrarTaxa.Name = "colunaParcelaCobrarTaxa";
            this.colunaParcelaCobrarTaxa.OptionsColumn.AllowEdit = false;
            this.colunaParcelaCobrarTaxa.OptionsColumn.AllowFocus = false;
            this.colunaParcelaCobrarTaxa.OptionsFilter.AllowFilter = false;
            this.colunaParcelaCobrarTaxa.Visible = true;
            this.colunaParcelaCobrarTaxa.VisibleIndex = 1;
            this.colunaParcelaCobrarTaxa.Width = 241;
            // 
            // colunaTaxa
            // 
            this.colunaTaxa.Caption = "Taxa (%)";
            this.colunaTaxa.FieldName = "Taxa";
            this.colunaTaxa.MinWidth = 27;
            this.colunaTaxa.Name = "colunaTaxa";
            this.colunaTaxa.OptionsColumn.AllowEdit = false;
            this.colunaTaxa.OptionsColumn.AllowFocus = false;
            this.colunaTaxa.OptionsFilter.AllowFilter = false;
            this.colunaTaxa.Visible = true;
            this.colunaTaxa.VisibleIndex = 2;
            this.colunaTaxa.Width = 168;
            // 
            // gridView2
            // 
            this.gridView2.DetailHeight = 431;
            this.gridView2.GridControl = this.gcOperadoras;
            this.gridView2.Name = "gridView2";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnGravar);
            this.flowLayoutPanel1.Controls.Add(this.btnLimpar);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(883, 66);
            this.flowLayoutPanel1.TabIndex = 1001;
            // 
            // txtDataCadastro
            // 
            this.txtDataCadastro.EnterMoveNextControl = true;
            this.txtDataCadastro.Location = new System.Drawing.Point(303, 152);
            this.txtDataCadastro.Margin = new System.Windows.Forms.Padding(4);
            this.txtDataCadastro.Name = "txtDataCadastro";
            this.txtDataCadastro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataCadastro.Properties.Appearance.Options.UseFont = true;
            this.txtDataCadastro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDataCadastro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDataCadastro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataCadastro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDataCadastro.Properties.Mask.EditMask = "([0-9]{1})(([\\.][0-9]{2})(([\\.][0-9]{3})([\\.][0-9]{4})?)?)?";
            this.txtDataCadastro.Properties.MaxLength = 50;
            this.txtDataCadastro.Properties.ReadOnly = true;
            this.txtDataCadastro.Size = new System.Drawing.Size(155, 26);
            this.txtDataCadastro.TabIndex = 10;
            // 
            // chkPermiteParcelamento
            // 
            this.chkPermiteParcelamento.AutoSize = true;
            this.chkPermiteParcelamento.Location = new System.Drawing.Point(163, 105);
            this.chkPermiteParcelamento.Margin = new System.Windows.Forms.Padding(4);
            this.chkPermiteParcelamento.Name = "chkPermiteParcelamento";
            this.chkPermiteParcelamento.Size = new System.Drawing.Size(177, 21);
            this.chkPermiteParcelamento.TabIndex = 5;
            this.chkPermiteParcelamento.Text = "Permite Parcelamento?";
            this.chkPermiteParcelamento.UseVisualStyleBackColor = true;
            this.chkPermiteParcelamento.CheckedChanged += new System.EventHandler(this.chkPermiteParcelamento_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkRecebimentoAntecipado);
            this.groupBox1.Controls.Add(this.pbPesquisaBancos);
            this.groupBox1.Controls.Add(this.labelControl10);
            this.groupBox1.Controls.Add(this.txtDataCadastro);
            this.groupBox1.Controls.Add(this.cboCategoriaDespesa);
            this.groupBox1.Controls.Add(this.txtBanco);
            this.groupBox1.Controls.Add(this.labDataCadastro);
            this.groupBox1.Controls.Add(this.labelControl7);
            this.groupBox1.Controls.Add(this.cboStatus);
            this.groupBox1.Controls.Add(this.labelControl6);
            this.groupBox1.Controls.Add(this.txtPrazoParaCreditar);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.chkPermiteParcelamento);
            this.groupBox1.Controls.Add(this.txtCodigoOperadora);
            this.groupBox1.Controls.Add(this.txtDescricao);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.pbPesquisaOperadora);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Location = new System.Drawing.Point(5, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1116, 188);
            this.groupBox1.TabIndex = 627;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "À Creditar";
            // 
            // chkRecebimentoAntecipado
            // 
            this.chkRecebimentoAntecipado.AutoSize = true;
            this.chkRecebimentoAntecipado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRecebimentoAntecipado.Location = new System.Drawing.Point(348, 104);
            this.chkRecebimentoAntecipado.Margin = new System.Windows.Forms.Padding(4);
            this.chkRecebimentoAntecipado.Name = "chkRecebimentoAntecipado";
            this.chkRecebimentoAntecipado.Size = new System.Drawing.Size(249, 24);
            this.chkRecebimentoAntecipado.TabIndex = 637;
            this.chkRecebimentoAntecipado.Text = "Recebimento Antecipado?";
            this.chkRecebimentoAntecipado.UseVisualStyleBackColor = true;
            // 
            // pbPesquisaBancos
            // 
            this.pbPesquisaBancos.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaBancos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaBancos.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaBancos.Location = new System.Drawing.Point(1080, 46);
            this.pbPesquisaBancos.Margin = new System.Windows.Forms.Padding(4);
            this.pbPesquisaBancos.Name = "pbPesquisaBancos";
            this.pbPesquisaBancos.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaBancos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaBancos.TabIndex = 636;
            this.pbPesquisaBancos.TabStop = false;
            this.pbPesquisaBancos.Click += new System.EventHandler(this.pbPesquisaBancos_Click);
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(624, 26);
            this.labelControl10.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(106, 17);
            this.labelControl10.TabIndex = 635;
            this.labelControl10.Text = "Banco à Creditar";
            // 
            // cboCategoriaDespesa
            // 
            this.cboCategoriaDespesa.EnterMoveNextControl = true;
            this.cboCategoriaDespesa.Location = new System.Drawing.Point(623, 102);
            this.cboCategoriaDespesa.Margin = new System.Windows.Forms.Padding(4);
            this.cboCategoriaDespesa.Name = "cboCategoriaDespesa";
            this.cboCategoriaDespesa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategoriaDespesa.Properties.Appearance.Options.UseFont = true;
            this.cboCategoriaDespesa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCategoriaDespesa.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboCategoriaDespesa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.cboCategoriaDespesa.Properties.DropDownRows = 2;
            this.cboCategoriaDespesa.Properties.NullText = "";
            this.cboCategoriaDespesa.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboCategoriaDespesa.Size = new System.Drawing.Size(449, 26);
            this.cboCategoriaDespesa.TabIndex = 10076;
            // 
            // txtBanco
            // 
            this.txtBanco.EnterMoveNextControl = true;
            this.txtBanco.Location = new System.Drawing.Point(624, 46);
            this.txtBanco.Margin = new System.Windows.Forms.Padding(4);
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBanco.Properties.Appearance.Options.UseFont = true;
            this.txtBanco.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtBanco.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtBanco.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBanco.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtBanco.Properties.MaxLength = 50;
            this.txtBanco.Properties.ReadOnly = true;
            this.txtBanco.Size = new System.Drawing.Size(449, 26);
            this.txtBanco.TabIndex = 3;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(8, 81);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(150, 17);
            this.labelControl7.TabIndex = 634;
            this.labelControl7.Text = "Prazo P/ Creditar (Dias)";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(624, 81);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(141, 17);
            this.labelControl6.TabIndex = 517;
            this.labelControl6.Text = "Categoria de Despesa";
            // 
            // txtPrazoParaCreditar
            // 
            this.txtPrazoParaCreditar.EnterMoveNextControl = true;
            this.txtPrazoParaCreditar.Location = new System.Drawing.Point(8, 102);
            this.txtPrazoParaCreditar.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrazoParaCreditar.Name = "txtPrazoParaCreditar";
            this.txtPrazoParaCreditar.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrazoParaCreditar.Properties.Appearance.Options.UseFont = true;
            this.txtPrazoParaCreditar.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPrazoParaCreditar.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtPrazoParaCreditar.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPrazoParaCreditar.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtPrazoParaCreditar.Properties.Mask.EditMask = "f0";
            this.txtPrazoParaCreditar.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPrazoParaCreditar.Properties.Mask.SaveLiteral = false;
            this.txtPrazoParaCreditar.Properties.MaxLength = 50;
            this.txtPrazoParaCreditar.Size = new System.Drawing.Size(149, 26);
            this.txtPrazoParaCreditar.TabIndex = 4;
            // 
            // txtCodigoOperadora
            // 
            this.txtCodigoOperadora.EnterMoveNextControl = true;
            this.txtCodigoOperadora.Location = new System.Drawing.Point(8, 46);
            this.txtCodigoOperadora.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigoOperadora.Name = "txtCodigoOperadora";
            this.txtCodigoOperadora.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoOperadora.Properties.Appearance.Options.UseFont = true;
            this.txtCodigoOperadora.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCodigoOperadora.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtCodigoOperadora.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoOperadora.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtCodigoOperadora.Properties.MaxLength = 50;
            this.txtCodigoOperadora.Size = new System.Drawing.Size(115, 26);
            this.txtCodigoOperadora.TabIndex = 1;
            this.txtCodigoOperadora.Leave += new System.EventHandler(this.txtCodigoOperadora_Leave);
            // 
            // txtDescricao
            // 
            this.txtDescricao.EnterMoveNextControl = true;
            this.txtDescricao.Location = new System.Drawing.Point(163, 46);
            this.txtDescricao.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.Properties.Appearance.Options.UseFont = true;
            this.txtDescricao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricao.Properties.Mask.EditMask = "99/";
            this.txtDescricao.Properties.MaxLength = 200;
            this.txtDescricao.Size = new System.Drawing.Size(456, 26);
            this.txtDescricao.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(163, 26);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(63, 17);
            this.labelControl1.TabIndex = 632;
            this.labelControl1.Text = "Descrição";
            // 
            // pbPesquisaOperadora
            // 
            this.pbPesquisaOperadora.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaOperadora.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaOperadora.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaOperadora.Location = new System.Drawing.Point(128, 46);
            this.pbPesquisaOperadora.Margin = new System.Windows.Forms.Padding(4);
            this.pbPesquisaOperadora.Name = "pbPesquisaOperadora";
            this.pbPesquisaOperadora.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaOperadora.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaOperadora.TabIndex = 631;
            this.pbPesquisaOperadora.TabStop = false;
            this.pbPesquisaOperadora.Click += new System.EventHandler(this.pbPesquisaOperadora_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(8, 26);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(117, 17);
            this.labelControl2.TabIndex = 633;
            this.labelControl2.Text = "Código Operadora";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnInserirAtualizarItem);
            this.groupBox2.Controls.Add(this.labelControl34);
            this.groupBox2.Controls.Add(this.btnExcluir);
            this.groupBox2.Controls.Add(this.cboCondicaoPagamento);
            this.groupBox2.Controls.Add(this.txtTaxaAdministracao);
            this.groupBox2.Controls.Add(this.labelControl9);
            this.groupBox2.Controls.Add(this.labelControl8);
            this.groupBox2.Controls.Add(this.txtParcelaParaComecarCobrarTaxa);
            this.groupBox2.Location = new System.Drawing.Point(6, 205);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1115, 87);
            this.groupBox2.TabIndex = 628;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Á Debitar";
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
            this.btnInserirAtualizarItem.Location = new System.Drawing.Point(1020, 42);
            this.btnInserirAtualizarItem.Margin = new System.Windows.Forms.Padding(4);
            this.btnInserirAtualizarItem.Name = "btnInserirAtualizarItem";
            this.btnInserirAtualizarItem.Size = new System.Drawing.Size(37, 28);
            this.btnInserirAtualizarItem.TabIndex = 10142;
            this.btnInserirAtualizarItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInserirAtualizarItem.UseVisualStyleBackColor = true;
            this.btnInserirAtualizarItem.Click += new System.EventHandler(this.btnInserirAtualizarItem_Click);
            // 
            // labelControl34
            // 
            this.labelControl34.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl34.Appearance.Options.UseFont = true;
            this.labelControl34.Location = new System.Drawing.Point(10, 20);
            this.labelControl34.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl34.Name = "labelControl34";
            this.labelControl34.Size = new System.Drawing.Size(155, 17);
            this.labelControl34.TabIndex = 10141;
            this.labelControl34.Text = "Condição de Pagamento";
            // 
            // btnExcluir
            // 
            this.btnExcluir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluir.FlatAppearance.BorderSize = 0;
            this.btnExcluir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExcluir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluir.Image = global::Programax.Easy.View.Properties.Resources.icones2_18;
            this.btnExcluir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcluir.Location = new System.Drawing.Point(1064, 42);
            this.btnExcluir.Margin = new System.Windows.Forms.Padding(4);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(37, 28);
            this.btnExcluir.TabIndex = 10140;
            this.btnExcluir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // cboCondicaoPagamento
            // 
            this.cboCondicaoPagamento.EnterMoveNextControl = true;
            this.cboCondicaoPagamento.Location = new System.Drawing.Point(7, 42);
            this.cboCondicaoPagamento.Margin = new System.Windows.Forms.Padding(4);
            this.cboCondicaoPagamento.Name = "cboCondicaoPagamento";
            this.cboCondicaoPagamento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCondicaoPagamento.Properties.Appearance.Options.UseFont = true;
            this.cboCondicaoPagamento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCondicaoPagamento.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboCondicaoPagamento.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboCondicaoPagamento.Properties.NullText = "";
            this.cboCondicaoPagamento.Size = new System.Drawing.Size(580, 26);
            this.cboCondicaoPagamento.TabIndex = 10077;
            // 
            // txtTaxaAdministracao
            // 
            this.txtTaxaAdministracao.EnterMoveNextControl = true;
            this.txtTaxaAdministracao.Inconsistencias = ((System.Collections.Generic.List<string>)(resources.GetObject("txtTaxaAdministracao.Inconsistencias")));
            this.txtTaxaAdministracao.LabelText = null;
            this.txtTaxaAdministracao.Location = new System.Drawing.Point(845, 42);
            this.txtTaxaAdministracao.Margin = new System.Windows.Forms.Padding(4);
            this.txtTaxaAdministracao.Name = "txtTaxaAdministracao";
            this.txtTaxaAdministracao.Obrigatorio = false;
            this.txtTaxaAdministracao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxaAdministracao.Properties.Appearance.Options.UseFont = true;
            this.txtTaxaAdministracao.Properties.Mask.EditMask = "[0-9]{1,2}([\\.\\,][0-9]{0,2})?";
            this.txtTaxaAdministracao.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtTaxaAdministracao.Size = new System.Drawing.Size(167, 26);
            this.txtTaxaAdministracao.TabIndex = 8;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(848, 19);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(57, 17);
            this.labelControl9.TabIndex = 519;
            this.labelControl9.Text = "Taxa (%)";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(597, 19);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(238, 17);
            this.labelControl8.TabIndex = 518;
            this.labelControl8.Text = "Parcela para começar a cobrar a taxa";
            // 
            // txtParcelaParaComecarCobrarTaxa
            // 
            this.txtParcelaParaComecarCobrarTaxa.EnterMoveNextControl = true;
            this.txtParcelaParaComecarCobrarTaxa.Location = new System.Drawing.Point(597, 42);
            this.txtParcelaParaComecarCobrarTaxa.Margin = new System.Windows.Forms.Padding(4);
            this.txtParcelaParaComecarCobrarTaxa.Name = "txtParcelaParaComecarCobrarTaxa";
            this.txtParcelaParaComecarCobrarTaxa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParcelaParaComecarCobrarTaxa.Properties.Appearance.Options.UseFont = true;
            this.txtParcelaParaComecarCobrarTaxa.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtParcelaParaComecarCobrarTaxa.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtParcelaParaComecarCobrarTaxa.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtParcelaParaComecarCobrarTaxa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtParcelaParaComecarCobrarTaxa.Properties.Mask.EditMask = "f0";
            this.txtParcelaParaComecarCobrarTaxa.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtParcelaParaComecarCobrarTaxa.Properties.MaxLength = 50;
            this.txtParcelaParaComecarCobrarTaxa.Size = new System.Drawing.Size(239, 26);
            this.txtParcelaParaComecarCobrarTaxa.TabIndex = 7;
            // 
            // FormCadastroOperadorasCartao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 769);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "FormCadastroOperadorasCartao";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcOperadoras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaBancos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategoriaDespesa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBanco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrazoParaCreditar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoOperadora.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaOperadora)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboCondicaoPagamento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaxaAdministracao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParcelaParaComecarCobrarTaxa.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnSair;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.GridControl gcOperadoras;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCondicaoPagamento;
        private DevExpress.XtraGrid.Columns.GridColumn colunaParcelaCobrarTaxa;
        private DevExpress.XtraGrid.Columns.GridColumn colunaTaxa;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.TextEdit txtDataCadastro;
        private System.Windows.Forms.CheckBox chkPermiteParcelamento;
        private System.Windows.Forms.GroupBox groupBox2;
        private Componentes.AkilTextEdit txtTaxaAdministracao;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtParcelaParaComecarCobrarTaxa;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pbPesquisaBancos;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit txtBanco;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtPrazoParaCreditar;
        private DevExpress.XtraEditors.TextEdit txtCodigoOperadora;
        private DevExpress.XtraEditors.TextEdit txtDescricao;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.PictureBox pbPesquisaOperadora;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit cboCategoriaDespesa;
        private DevExpress.XtraEditors.LookUpEdit cboCondicaoPagamento;
        private System.Windows.Forms.Button btnExcluir;
        private DevExpress.XtraEditors.LabelControl labelControl34;
        private System.Windows.Forms.CheckBox chkRecebimentoAntecipado;
        private System.Windows.Forms.Button btnInserirAtualizarItem;
    }
}