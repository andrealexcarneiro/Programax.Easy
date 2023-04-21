namespace Programax.Easy.View.Telas.Financeiro.MovimentacoesBanco
{
    partial class FormConciliacaoBancaria
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
            this.txtDataFinalPeriodo = new DevExpress.XtraEditors.DateEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataInicialPeriodo = new DevExpress.XtraEditors.DateEdit();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.cboStatusFiltrar = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnPesquisaConciliacao = new System.Windows.Forms.PictureBox();
            this.btnDesfazer = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnConciliar = new System.Windows.Forms.Button();
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnIgnorar = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.cboDataFiltrar = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lnkInstrucoes = new System.Windows.Forms.LinkLabel();
            this.btnPagarReceber = new System.Windows.Forms.Button();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.repositoryItemFontEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcConciliacaoBancaria = new Programax.Easy.View.Componentes.AkilGridControl();
            this.gridViewConciliacaoBancaria = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaOrigem1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDescricao1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataVencimento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaValor1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaOrigem2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaId2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDescricao2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataDocumento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaValorPago = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaImagem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatusFiltrar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaConciliacao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDataFiltrar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcConciliacaoBancaria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewConciliacaoBancaria)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.btnPagarReceber);
            this.painelBotoes.Controls.Add(this.btnFinalizar);
            this.painelBotoes.Controls.Add(this.btnIgnorar);
            this.painelBotoes.Controls.Add(this.btnImportar);
            this.painelBotoes.Controls.Add(this.btnDesfazer);
            this.painelBotoes.Controls.Add(this.btnSair);
            this.painelBotoes.Controls.Add(this.btnConciliar);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.lnkInstrucoes);
            this.panelConteudo.Controls.Add(this.cboDataFiltrar);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.label12);
            this.panelConteudo.Controls.Add(this.txtDataFinalPeriodo);
            this.panelConteudo.Controls.Add(this.labelControl5);
            this.panelConteudo.Controls.Add(this.txtDataInicialPeriodo);
            this.panelConteudo.Controls.Add(this.labDataCadastro);
            this.panelConteudo.Controls.Add(this.cboStatusFiltrar);
            this.panelConteudo.Controls.Add(this.labelControl3);
            this.panelConteudo.Controls.Add(this.btnPesquisaConciliacao);
            this.panelConteudo.Controls.Add(this.gcConciliacaoBancaria);
            this.panelConteudo.Size = new System.Drawing.Size(1157, 504);
            // 
            // txtDataFinalPeriodo
            // 
            this.txtDataFinalPeriodo.EditValue = "";
            this.txtDataFinalPeriodo.Enabled = false;
            this.txtDataFinalPeriodo.EnterMoveNextControl = true;
            this.txtDataFinalPeriodo.Location = new System.Drawing.Point(550, 28);
            this.txtDataFinalPeriodo.Name = "txtDataFinalPeriodo";
            this.txtDataFinalPeriodo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinalPeriodo.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinalPeriodo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinalPeriodo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinalPeriodo.Size = new System.Drawing.Size(112, 22);
            this.txtDataFinalPeriodo.TabIndex = 3;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(550, 13);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(89, 13);
            this.labelControl5.TabIndex = 10044;
            this.labelControl5.Text = "Data Final Período";
            // 
            // txtDataInicialPeriodo
            // 
            this.txtDataInicialPeriodo.EditValue = "";
            this.txtDataInicialPeriodo.Enabled = false;
            this.txtDataInicialPeriodo.EnterMoveNextControl = true;
            this.txtDataInicialPeriodo.Location = new System.Drawing.Point(431, 28);
            this.txtDataInicialPeriodo.Name = "txtDataInicialPeriodo";
            this.txtDataInicialPeriodo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicialPeriodo.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicialPeriodo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicialPeriodo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicialPeriodo.Size = new System.Drawing.Size(112, 22);
            this.txtDataInicialPeriodo.TabIndex = 2;
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Location = new System.Drawing.Point(431, 13);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(94, 13);
            this.labDataCadastro.TabIndex = 10043;
            this.labDataCadastro.Text = "Data Inicial Período";
            // 
            // cboStatusFiltrar
            // 
            this.cboStatusFiltrar.EnterMoveNextControl = true;
            this.cboStatusFiltrar.Location = new System.Drawing.Point(3, 28);
            this.cboStatusFiltrar.Name = "cboStatusFiltrar";
            this.cboStatusFiltrar.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatusFiltrar.Properties.Appearance.Options.UseFont = true;
            this.cboStatusFiltrar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatusFiltrar.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatusFiltrar.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboStatusFiltrar.Properties.DropDownRows = 4;
            this.cboStatusFiltrar.Properties.NullText = "";
            this.cboStatusFiltrar.Size = new System.Drawing.Size(236, 22);
            this.cboStatusFiltrar.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl3.Location = new System.Drawing.Point(3, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(67, 13);
            this.labelControl3.TabIndex = 10042;
            this.labelControl3.Text = "Status a Filtrar";
            // 
            // btnPesquisaConciliacao
            // 
            this.btnPesquisaConciliacao.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaConciliacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaConciliacao.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaConciliacao.Location = new System.Drawing.Point(671, 28);
            this.btnPesquisaConciliacao.Name = "btnPesquisaConciliacao";
            this.btnPesquisaConciliacao.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaConciliacao.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaConciliacao.TabIndex = 10041;
            this.btnPesquisaConciliacao.TabStop = false;
            this.btnPesquisaConciliacao.Click += new System.EventHandler(this.btnPesquisaConciliacao_Click);
            // 
            // btnDesfazer
            // 
            this.btnDesfazer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDesfazer.FlatAppearance.BorderSize = 0;
            this.btnDesfazer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDesfazer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesfazer.Image = global::Programax.Easy.View.Properties.Resources.icone_estornar;
            this.btnDesfazer.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDesfazer.Location = new System.Drawing.Point(315, -1);
            this.btnDesfazer.Margin = new System.Windows.Forms.Padding(0);
            this.btnDesfazer.Name = "btnDesfazer";
            this.btnDesfazer.Size = new System.Drawing.Size(104, 40);
            this.btnDesfazer.TabIndex = 8;
            this.btnDesfazer.Text = "Desfazer";
            this.btnDesfazer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDesfazer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDesfazer.UseVisualStyleBackColor = true;
            this.btnDesfazer.Click += new System.EventHandler(this.btnDesfazer_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(856, -2);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(77, 40);
            this.btnSair.TabIndex = 10;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnConciliar
            // 
            this.btnConciliar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConciliar.FlatAppearance.BorderSize = 0;
            this.btnConciliar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConciliar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConciliar.Image = global::Programax.Easy.View.Properties.Resources.icone_atualizar;
            this.btnConciliar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnConciliar.Location = new System.Drawing.Point(208, -1);
            this.btnConciliar.Margin = new System.Windows.Forms.Padding(0);
            this.btnConciliar.Name = "btnConciliar";
            this.btnConciliar.Size = new System.Drawing.Size(98, 40);
            this.btnConciliar.TabIndex = 7;
            this.btnConciliar.Text = "Conciliar";
            this.btnConciliar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConciliar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConciliar.UseVisualStyleBackColor = true;
            this.btnConciliar.Click += new System.EventHandler(this.btnConciliar_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportar.FlatAppearance.BorderSize = 0;
            this.btnImportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportar.Image = global::Programax.Easy.View.Properties.Resources.icone_importar;
            this.btnImportar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnImportar.Location = new System.Drawing.Point(101, -1);
            this.btnImportar.Margin = new System.Windows.Forms.Padding(0);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(98, 40);
            this.btnImportar.TabIndex = 6;
            this.btnImportar.Text = "Importar";
            this.btnImportar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnIgnorar
            // 
            this.btnIgnorar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIgnorar.FlatAppearance.BorderSize = 0;
            this.btnIgnorar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIgnorar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIgnorar.Image = global::Programax.Easy.View.Properties.Resources.icone_cancelar_venda;
            this.btnIgnorar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnIgnorar.Location = new System.Drawing.Point(3, 0);
            this.btnIgnorar.Margin = new System.Windows.Forms.Padding(0);
            this.btnIgnorar.Name = "btnIgnorar";
            this.btnIgnorar.Size = new System.Drawing.Size(98, 40);
            this.btnIgnorar.TabIndex = 5;
            this.btnIgnorar.Text = " Ignorar";
            this.btnIgnorar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIgnorar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIgnorar.UseVisualStyleBackColor = true;
            this.btnIgnorar.Click += new System.EventHandler(this.btnIgnorar_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinalizar.FlatAppearance.BorderSize = 0;
            this.btnFinalizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Image = global::Programax.Easy.View.Properties.Resources.iconSalvar;
            this.btnFinalizar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnFinalizar.Location = new System.Drawing.Point(430, 0);
            this.btnFinalizar.Margin = new System.Windows.Forms.Padding(0);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(97, 40);
            this.btnFinalizar.TabIndex = 9;
            this.btnFinalizar.Text = "Finalizar";
            this.btnFinalizar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFinalizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(3, 479);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1151, 20);
            this.label12.TabIndex = 10076;
            this.label12.Text = "F1 Sair  | F2 Pesquisar  |  F3 Ignorar  |  F4 Importar |  F5 Conciliar  |  F6 Des" +
    "fazer  |  F7 Pagar/Receber | F8 Finalizar";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboDataFiltrar
            // 
            this.cboDataFiltrar.EnterMoveNextControl = true;
            this.cboDataFiltrar.Location = new System.Drawing.Point(246, 28);
            this.cboDataFiltrar.Name = "cboDataFiltrar";
            this.cboDataFiltrar.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDataFiltrar.Properties.Appearance.Options.UseFont = true;
            this.cboDataFiltrar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDataFiltrar.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboDataFiltrar.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboDataFiltrar.Properties.DropDownRows = 4;
            this.cboDataFiltrar.Properties.NullText = "";
            this.cboDataFiltrar.Size = new System.Drawing.Size(179, 22);
            this.cboDataFiltrar.TabIndex = 10077;
            this.cboDataFiltrar.EditValueChanged += new System.EventHandler(this.cboDataFiltrar_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Location = new System.Drawing.Point(247, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 13);
            this.labelControl1.TabIndex = 10078;
            this.labelControl1.Text = "Data a Filtrar";
            // 
            // lnkInstrucoes
            // 
            this.lnkInstrucoes.AutoSize = true;
            this.lnkInstrucoes.Location = new System.Drawing.Point(1058, 31);
            this.lnkInstrucoes.Name = "lnkInstrucoes";
            this.lnkInstrucoes.Size = new System.Drawing.Size(89, 13);
            this.lnkInstrucoes.TabIndex = 10079;
            this.lnkInstrucoes.TabStop = true;
            this.lnkInstrucoes.Text = "Clique para ajuda";
            this.lnkInstrucoes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstrucoes_LinkClicked);
            // 
            // btnPagarReceber
            // 
            this.btnPagarReceber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPagarReceber.FlatAppearance.BorderSize = 0;
            this.btnPagarReceber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagarReceber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagarReceber.Image = global::Programax.Easy.View.Properties.Resources.icone_tres_pontos1;
            this.btnPagarReceber.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnPagarReceber.Location = new System.Drawing.Point(703, -1);
            this.btnPagarReceber.Margin = new System.Windows.Forms.Padding(0);
            this.btnPagarReceber.Name = "btnPagarReceber";
            this.btnPagarReceber.Size = new System.Drawing.Size(138, 40);
            this.btnPagarReceber.TabIndex = 21;
            this.btnPagarReceber.Text = " Pagar/Receber";
            this.btnPagarReceber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPagarReceber.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPagarReceber.UseVisualStyleBackColor = true;
            this.btnPagarReceber.Click += new System.EventHandler(this.btnPagarReceber_Click);
            // 
            // repositoryItemColorEdit1
            // 
            this.repositoryItemColorEdit1.AutoHeight = false;
            this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            // 
            // repositoryItemFontEdit1
            // 
            this.repositoryItemFontEdit1.AutoHeight = false;
            this.repositoryItemFontEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFontEdit1.Name = "repositoryItemFontEdit1";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcConciliacaoBancaria;
            this.gridView2.Name = "gridView2";
            // 
            // gcConciliacaoBancaria
            // 
            this.gcConciliacaoBancaria.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcConciliacaoBancaria.Location = new System.Drawing.Point(3, 54);
            this.gcConciliacaoBancaria.MainView = this.gridViewConciliacaoBancaria;
            this.gcConciliacaoBancaria.ModoImpressao = Programax.Easy.View.Componentes.Enumeradores.EnumAkilGridControlModoImpressao.PAISAGEM;
            this.gcConciliacaoBancaria.Name = "gcConciliacaoBancaria";
            this.gcConciliacaoBancaria.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemColorEdit1,
            this.repositoryItemFontEdit1});
            this.gcConciliacaoBancaria.Size = new System.Drawing.Size(1151, 419);
            this.gcConciliacaoBancaria.TabIndex = 4;
            this.gcConciliacaoBancaria.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewConciliacaoBancaria,
            this.gridView2});
            this.gcConciliacaoBancaria.Click += new System.EventHandler(this.gcConciliacaoBancaria_Click);
            this.gcConciliacaoBancaria.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gcConciliacaoBancaria_KeyUp);
            // 
            // gridViewConciliacaoBancaria
            // 
            this.gridViewConciliacaoBancaria.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridViewConciliacaoBancaria.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gridViewConciliacaoBancaria.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewConciliacaoBancaria.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridViewConciliacaoBancaria.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridViewConciliacaoBancaria.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewConciliacaoBancaria.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewConciliacaoBancaria.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridViewConciliacaoBancaria.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridViewConciliacaoBancaria.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.gridViewConciliacaoBancaria.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridViewConciliacaoBancaria.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridViewConciliacaoBancaria.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridViewConciliacaoBancaria.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridViewConciliacaoBancaria.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridViewConciliacaoBancaria.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaId,
            this.colunaOrigem1,
            this.colunaId1,
            this.colunaDescricao1,
            this.colunaDataVencimento,
            this.colunaValor1,
            this.colunaOrigem2,
            this.colunaId2,
            this.colunaDescricao2,
            this.colunaDataDocumento,
            this.colunaValorPago,
            this.colunaStatus,
            this.colunaImagem});
            this.gridViewConciliacaoBancaria.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridViewConciliacaoBancaria.GridControl = this.gcConciliacaoBancaria;
            this.gridViewConciliacaoBancaria.GroupPanelText = "Arraste para agrupar";
            this.gridViewConciliacaoBancaria.Name = "gridViewConciliacaoBancaria";
            this.gridViewConciliacaoBancaria.OptionsSelection.MultiSelect = true;
            this.gridViewConciliacaoBancaria.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewConciliacaoBancaria.OptionsView.ShowIndicator = false;
            this.gridViewConciliacaoBancaria.OptionsView.ShowViewCaption = true;
            this.gridViewConciliacaoBancaria.PaintStyleName = "Skin";
            this.gridViewConciliacaoBancaria.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridViewConciliacaoBancaria.ViewCaption = "Lançamentos Semelhantes no Contas Pagar/Receber/Movimentação Bancária";
            // 
            // colunaId
            // 
            this.colunaId.Caption = "Id";
            this.colunaId.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaId.FieldName = "Id";
            this.colunaId.Name = "colunaId";
            this.colunaId.OptionsColumn.AllowEdit = false;
            this.colunaId.OptionsColumn.AllowFocus = false;
            this.colunaId.OptionsFilter.AllowFilter = false;
            this.colunaId.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colunaId.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            // 
            // colunaOrigem1
            // 
            this.colunaOrigem1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colunaOrigem1.AppearanceCell.Options.UseFont = true;
            this.colunaOrigem1.AppearanceHeader.BackColor = System.Drawing.Color.Black;
            this.colunaOrigem1.AppearanceHeader.BackColor2 = System.Drawing.Color.Black;
            this.colunaOrigem1.AppearanceHeader.BorderColor = System.Drawing.Color.Black;
            this.colunaOrigem1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colunaOrigem1.AppearanceHeader.Options.UseBackColor = true;
            this.colunaOrigem1.AppearanceHeader.Options.UseBorderColor = true;
            this.colunaOrigem1.AppearanceHeader.Options.UseFont = true;
            this.colunaOrigem1.Caption = "Origem_1";
            this.colunaOrigem1.FieldName = "Origem1";
            this.colunaOrigem1.Name = "colunaOrigem1";
            this.colunaOrigem1.OptionsColumn.AllowEdit = false;
            this.colunaOrigem1.OptionsColumn.AllowFocus = false;
            this.colunaOrigem1.OptionsFilter.AllowFilter = false;
            this.colunaOrigem1.Visible = true;
            this.colunaOrigem1.VisibleIndex = 0;
            this.colunaOrigem1.Width = 135;
            // 
            // colunaId1
            // 
            this.colunaId1.Caption = "N° Documento";
            this.colunaId1.FieldName = "NumDoc";
            this.colunaId1.Name = "colunaId1";
            this.colunaId1.OptionsColumn.AllowEdit = false;
            this.colunaId1.OptionsColumn.AllowFocus = false;
            this.colunaId1.OptionsFilter.AllowFilter = false;
            this.colunaId1.Visible = true;
            this.colunaId1.VisibleIndex = 1;
            this.colunaId1.Width = 94;
            // 
            // colunaDescricao1
            // 
            this.colunaDescricao1.Caption = "Desc. Documento";
            this.colunaDescricao1.FieldName = "DescricaoDoc";
            this.colunaDescricao1.Name = "colunaDescricao1";
            this.colunaDescricao1.OptionsColumn.AllowEdit = false;
            this.colunaDescricao1.OptionsColumn.AllowFocus = false;
            this.colunaDescricao1.OptionsFilter.AllowFilter = false;
            this.colunaDescricao1.Visible = true;
            this.colunaDescricao1.VisibleIndex = 2;
            this.colunaDescricao1.Width = 114;
            // 
            // colunaDataVencimento
            // 
            this.colunaDataVencimento.Caption = "Dt Vencimento";
            this.colunaDataVencimento.FieldName = "DataVencimento";
            this.colunaDataVencimento.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DateMonth;
            this.colunaDataVencimento.Name = "colunaDataVencimento";
            this.colunaDataVencimento.OptionsColumn.AllowEdit = false;
            this.colunaDataVencimento.OptionsColumn.AllowFocus = false;
            this.colunaDataVencimento.OptionsFilter.AllowFilter = false;
            this.colunaDataVencimento.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.colunaDataVencimento.Visible = true;
            this.colunaDataVencimento.VisibleIndex = 3;
            this.colunaDataVencimento.Width = 84;
            // 
            // colunaValor1
            // 
            this.colunaValor1.Caption = "VL. Documento";
            this.colunaValor1.DisplayFormat.FormatString = "#,##0.00";
            this.colunaValor1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaValor1.FieldName = "ValorDoc";
            this.colunaValor1.Name = "colunaValor1";
            this.colunaValor1.OptionsColumn.AllowEdit = false;
            this.colunaValor1.OptionsColumn.AllowFocus = false;
            this.colunaValor1.OptionsFilter.AllowFilter = false;
            this.colunaValor1.Visible = true;
            this.colunaValor1.VisibleIndex = 4;
            this.colunaValor1.Width = 85;
            // 
            // colunaOrigem2
            // 
            this.colunaOrigem2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colunaOrigem2.AppearanceCell.Options.UseFont = true;
            this.colunaOrigem2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colunaOrigem2.AppearanceHeader.Options.UseFont = true;
            this.colunaOrigem2.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaOrigem2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaOrigem2.Caption = "Origem_2";
            this.colunaOrigem2.FieldName = "Origem2";
            this.colunaOrigem2.Name = "colunaOrigem2";
            this.colunaOrigem2.OptionsColumn.AllowEdit = false;
            this.colunaOrigem2.OptionsColumn.AllowFocus = false;
            this.colunaOrigem2.OptionsFilter.AllowFilter = false;
            this.colunaOrigem2.Visible = true;
            this.colunaOrigem2.VisibleIndex = 5;
            this.colunaOrigem2.Width = 123;
            // 
            // colunaId2
            // 
            this.colunaId2.Caption = "N° Lançamento";
            this.colunaId2.FieldName = "NumLancto";
            this.colunaId2.Name = "colunaId2";
            this.colunaId2.OptionsColumn.AllowEdit = false;
            this.colunaId2.OptionsColumn.AllowFocus = false;
            this.colunaId2.OptionsFilter.AllowFilter = false;
            this.colunaId2.Visible = true;
            this.colunaId2.VisibleIndex = 6;
            this.colunaId2.Width = 87;
            // 
            // colunaDescricao2
            // 
            this.colunaDescricao2.Caption = "Desc. Lançamento";
            this.colunaDescricao2.FieldName = "DescricaoLancto";
            this.colunaDescricao2.Name = "colunaDescricao2";
            this.colunaDescricao2.OptionsColumn.AllowEdit = false;
            this.colunaDescricao2.OptionsColumn.AllowFocus = false;
            this.colunaDescricao2.OptionsFilter.AllowFilter = false;
            this.colunaDescricao2.Visible = true;
            this.colunaDescricao2.VisibleIndex = 7;
            this.colunaDescricao2.Width = 111;
            // 
            // colunaDataDocumento
            // 
            this.colunaDataDocumento.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colunaDataDocumento.AppearanceCell.Options.UseFont = true;
            this.colunaDataDocumento.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colunaDataDocumento.AppearanceHeader.Options.UseFont = true;
            this.colunaDataDocumento.Caption = "Dt. Lançamento";
            this.colunaDataDocumento.FieldName = "DataLancto";
            this.colunaDataDocumento.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DateMonth;
            this.colunaDataDocumento.Name = "colunaDataDocumento";
            this.colunaDataDocumento.OptionsColumn.AllowEdit = false;
            this.colunaDataDocumento.OptionsColumn.AllowFocus = false;
            this.colunaDataDocumento.OptionsFilter.AllowFilter = false;
            this.colunaDataDocumento.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.colunaDataDocumento.Visible = true;
            this.colunaDataDocumento.VisibleIndex = 8;
            this.colunaDataDocumento.Width = 106;
            // 
            // colunaValorPago
            // 
            this.colunaValorPago.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colunaValorPago.AppearanceCell.Options.UseFont = true;
            this.colunaValorPago.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colunaValorPago.AppearanceHeader.Options.UseFont = true;
            this.colunaValorPago.Caption = "VL. Lançamento";
            this.colunaValorPago.DisplayFormat.FormatString = "#,##0.00";
            this.colunaValorPago.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaValorPago.FieldName = "ValorLancto";
            this.colunaValorPago.Name = "colunaValorPago";
            this.colunaValorPago.OptionsColumn.AllowEdit = false;
            this.colunaValorPago.OptionsColumn.AllowFocus = false;
            this.colunaValorPago.OptionsFilter.AllowFilter = false;
            this.colunaValorPago.Visible = true;
            this.colunaValorPago.VisibleIndex = 9;
            this.colunaValorPago.Width = 111;
            // 
            // colunaStatus
            // 
            this.colunaStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colunaStatus.Caption = "Status";
            this.colunaStatus.FieldName = "StatusConciliacao";
            this.colunaStatus.Name = "colunaStatus";
            this.colunaStatus.OptionsColumn.AllowEdit = false;
            this.colunaStatus.OptionsColumn.AllowFocus = false;
            this.colunaStatus.OptionsFilter.AllowFilter = false;
            this.colunaStatus.Visible = true;
            this.colunaStatus.VisibleIndex = 10;
            // 
            // colunaImagem
            // 
            this.colunaImagem.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaImagem.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colunaImagem.Caption = " ";
            this.colunaImagem.FieldName = "Imagem";
            this.colunaImagem.Name = "colunaImagem";
            this.colunaImagem.OptionsColumn.AllowEdit = false;
            this.colunaImagem.OptionsColumn.AllowFocus = false;
            this.colunaImagem.OptionsFilter.AllowFilter = false;
            this.colunaImagem.Visible = true;
            this.colunaImagem.VisibleIndex = 11;
            this.colunaImagem.Width = 24;
            // 
            // FormConciliacaoBancaria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 615);
            this.Name = "FormConciliacaoBancaria";
            this.NomeDaTela = "Importações Associadas";
            this.Text = "Importações Associadas";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormConciliacaoBancaria_KeyDown);
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatusFiltrar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaConciliacao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDataFiltrar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcConciliacaoBancaria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewConciliacaoBancaria)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.DateEdit txtDataFinalPeriodo;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.DateEdit txtDataInicialPeriodo;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.LookUpEdit cboStatusFiltrar;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.PictureBox btnPesquisaConciliacao;
        private System.Windows.Forms.Button btnDesfazer;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnConciliar;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Button btnIgnorar;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.LookUpEdit cboDataFiltrar;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.LinkLabel lnkInstrucoes;
        private System.Windows.Forms.Button btnPagarReceber;
        public Componentes.AkilGridControl gcConciliacaoBancaria;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewConciliacaoBancaria;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaOrigem1;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId1;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricao1;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataVencimento;
        private DevExpress.XtraGrid.Columns.GridColumn colunaValor1;
        private DevExpress.XtraGrid.Columns.GridColumn colunaOrigem2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricao2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataDocumento;
        private DevExpress.XtraGrid.Columns.GridColumn colunaValorPago;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colunaImagem;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
    }
}