namespace Programax.Easy.View.Telas.Financeiro.RelacionarCategorias
{
    partial class FormRelacionarCategoriasFinanceiras
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
            this.cboGrupos = new DevExpress.XtraEditors.LookUpEdit();
            this.labTipoInscricao = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdicionarGrupo = new System.Windows.Forms.Button();
            this.pnlCategoria = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnAdicionarSubGrupo = new System.Windows.Forms.Button();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnAdicionarCategoria = new System.Windows.Forms.Button();
            this.txtId = new DevExpress.XtraEditors.TextEdit();
            this.labCodigo = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricao = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pbPesquisaCategoria = new System.Windows.Forms.PictureBox();
            this.gcGrupos = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDescricao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaGrupo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cboOrdemGrupo = new DevExpress.XtraEditors.LookUpEdit();
            this.pbPesquisaOrdemGrupo = new System.Windows.Forms.PictureBox();
            this.lookUpEdit2 = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGrupos.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnlCategoria.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCategoria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGrupos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOrdemGrupo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaOrdemGrupo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Location = new System.Drawing.Point(9, 0);
            this.painelBotoes.Size = new System.Drawing.Size(696, 74);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.lookUpEdit2);
            this.panelConteudo.Controls.Add(this.labelControl4);
            this.panelConteudo.Controls.Add(this.pbPesquisaOrdemGrupo);
            this.panelConteudo.Controls.Add(this.cboOrdemGrupo);
            this.panelConteudo.Controls.Add(this.labelControl3);
            this.panelConteudo.Controls.Add(this.button5);
            this.panelConteudo.Controls.Add(this.button4);
            this.panelConteudo.Controls.Add(this.button3);
            this.panelConteudo.Controls.Add(this.gcGrupos);
            this.panelConteudo.Controls.Add(this.panel7);
            this.panelConteudo.Controls.Add(this.panel6);
            this.panelConteudo.Controls.Add(this.pnlCategoria);
            this.panelConteudo.Margin = new System.Windows.Forms.Padding(5);
            this.panelConteudo.Size = new System.Drawing.Size(1386, 645);
            this.panelConteudo.Paint += new System.Windows.Forms.PaintEventHandler(this.panelConteudo_Paint);
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
            this.btnLimpar.Location = new System.Drawing.Point(133, 0);
            this.btnLimpar.Margin = new System.Windows.Forms.Padding(0);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(133, 49);
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
            this.btnSair.Location = new System.Drawing.Point(266, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(133, 49);
            this.btnSair.TabIndex = 999;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // cboGrupos
            // 
            this.cboGrupos.EnterMoveNextControl = true;
            this.cboGrupos.Location = new System.Drawing.Point(3, 22);
            this.cboGrupos.Margin = new System.Windows.Forms.Padding(4);
            this.cboGrupos.Name = "cboGrupos";
            this.cboGrupos.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGrupos.Properties.Appearance.Options.UseFont = true;
            this.cboGrupos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboGrupos.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Cód. Cadastro", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboGrupos.Properties.DropDownRows = 5;
            this.cboGrupos.Properties.NullText = "";
            this.cboGrupos.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboGrupos.Size = new System.Drawing.Size(696, 26);
            this.cboGrupos.TabIndex = 647;
            // 
            // labTipoInscricao
            // 
            this.labTipoInscricao.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTipoInscricao.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labTipoInscricao.Appearance.Options.UseFont = true;
            this.labTipoInscricao.Appearance.Options.UseForeColor = true;
            this.labTipoInscricao.Location = new System.Drawing.Point(4, 2);
            this.labTipoInscricao.Margin = new System.Windows.Forms.Padding(4);
            this.labTipoInscricao.Name = "labTipoInscricao";
            this.labTipoInscricao.Size = new System.Drawing.Size(42, 16);
            this.labTipoInscricao.TabIndex = 648;
            this.labTipoInscricao.Text = "Grupo";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnGravar);
            this.flowLayoutPanel1.Controls.Add(this.btnLimpar);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(549, 50);
            this.flowLayoutPanel1.TabIndex = 1001;
            // 
            // btnAdicionarGrupo
            // 
            this.btnAdicionarGrupo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarGrupo.FlatAppearance.BorderSize = 0;
            this.btnAdicionarGrupo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarGrupo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarGrupo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarGrupo.Image = global::Programax.Easy.View.Properties.Resources.iconfinder_199_CircledPlus_183316;
            this.btnAdicionarGrupo.Location = new System.Drawing.Point(704, 20);
            this.btnAdicionarGrupo.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdicionarGrupo.Name = "btnAdicionarGrupo";
            this.btnAdicionarGrupo.Size = new System.Drawing.Size(31, 28);
            this.btnAdicionarGrupo.TabIndex = 10026;
            this.btnAdicionarGrupo.TabStop = false;
            this.btnAdicionarGrupo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionarGrupo.UseVisualStyleBackColor = true;
            this.btnAdicionarGrupo.Click += new System.EventHandler(this.btnAtalhoCategoria_Click);
            // 
            // pnlCategoria
            // 
            this.pnlCategoria.Controls.Add(this.labTipoInscricao);
            this.pnlCategoria.Controls.Add(this.btnAdicionarGrupo);
            this.pnlCategoria.Controls.Add(this.cboGrupos);
            this.pnlCategoria.Location = new System.Drawing.Point(6, 23);
            this.pnlCategoria.Margin = new System.Windows.Forms.Padding(4);
            this.pnlCategoria.Name = "pnlCategoria";
            this.pnlCategoria.Size = new System.Drawing.Size(746, 55);
            this.pnlCategoria.TabIndex = 10027;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.labelControl2);
            this.panel6.Controls.Add(this.btnAdicionarSubGrupo);
            this.panel6.Controls.Add(this.lookUpEdit1);
            this.panel6.Location = new System.Drawing.Point(6, 86);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(746, 55);
            this.panel6.TabIndex = 10028;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(4, 2);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 16);
            this.labelControl2.TabIndex = 648;
            this.labelControl2.Text = "SubGrupo";
            // 
            // btnAdicionarSubGrupo
            // 
            this.btnAdicionarSubGrupo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarSubGrupo.FlatAppearance.BorderSize = 0;
            this.btnAdicionarSubGrupo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarSubGrupo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarSubGrupo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarSubGrupo.Image = global::Programax.Easy.View.Properties.Resources.iconfinder_199_CircledPlus_183316;
            this.btnAdicionarSubGrupo.Location = new System.Drawing.Point(704, 20);
            this.btnAdicionarSubGrupo.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdicionarSubGrupo.Name = "btnAdicionarSubGrupo";
            this.btnAdicionarSubGrupo.Size = new System.Drawing.Size(31, 28);
            this.btnAdicionarSubGrupo.TabIndex = 10026;
            this.btnAdicionarSubGrupo.TabStop = false;
            this.btnAdicionarSubGrupo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionarSubGrupo.UseVisualStyleBackColor = true;
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.EnterMoveNextControl = true;
            this.lookUpEdit1.Location = new System.Drawing.Point(3, 22);
            this.lookUpEdit1.Margin = new System.Windows.Forms.Padding(4);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Cód. Cadastro", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.lookUpEdit1.Properties.DropDownRows = 5;
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.lookUpEdit1.Size = new System.Drawing.Size(696, 26);
            this.lookUpEdit1.TabIndex = 647;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnAdicionarCategoria);
            this.panel7.Controls.Add(this.txtId);
            this.panel7.Controls.Add(this.labCodigo);
            this.panel7.Controls.Add(this.txtDescricao);
            this.panel7.Controls.Add(this.labelControl1);
            this.panel7.Controls.Add(this.pbPesquisaCategoria);
            this.panel7.Location = new System.Drawing.Point(6, 149);
            this.panel7.Margin = new System.Windows.Forms.Padding(4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(746, 63);
            this.panel7.TabIndex = 10029;
            // 
            // btnAdicionarCategoria
            // 
            this.btnAdicionarCategoria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarCategoria.FlatAppearance.BorderSize = 0;
            this.btnAdicionarCategoria.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarCategoria.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarCategoria.Image = global::Programax.Easy.View.Properties.Resources.iconfinder_199_CircledPlus_183316;
            this.btnAdicionarCategoria.Location = new System.Drawing.Point(704, 22);
            this.btnAdicionarCategoria.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdicionarCategoria.Name = "btnAdicionarCategoria";
            this.btnAdicionarCategoria.Size = new System.Drawing.Size(31, 28);
            this.btnAdicionarCategoria.TabIndex = 10027;
            this.btnAdicionarCategoria.TabStop = false;
            this.btnAdicionarCategoria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionarCategoria.UseVisualStyleBackColor = true;
            // 
            // txtId
            // 
            this.txtId.EnterMoveNextControl = true;
            this.txtId.Location = new System.Drawing.Point(7, 24);
            this.txtId.Margin = new System.Windows.Forms.Padding(4);
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
            this.txtId.Size = new System.Drawing.Size(128, 26);
            this.txtId.TabIndex = 491;
            // 
            // labCodigo
            // 
            this.labCodigo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCodigo.Appearance.Options.UseFont = true;
            this.labCodigo.Location = new System.Drawing.Point(9, 3);
            this.labCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.labCodigo.Name = "labCodigo";
            this.labCodigo.Size = new System.Drawing.Size(94, 17);
            this.labCodigo.TabIndex = 492;
            this.labCodigo.Text = "Cod. Categoria";
            this.labCodigo.Click += new System.EventHandler(this.labCodigo_Click);
            // 
            // txtDescricao
            // 
            this.txtDescricao.EnterMoveNextControl = true;
            this.txtDescricao.Location = new System.Drawing.Point(189, 25);
            this.txtDescricao.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.Properties.Appearance.Options.UseFont = true;
            this.txtDescricao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricao.Properties.Mask.EditMask = "99/";
            this.txtDescricao.Properties.MaxLength = 50;
            this.txtDescricao.Properties.ReadOnly = true;
            this.txtDescricao.Size = new System.Drawing.Size(510, 26);
            this.txtDescricao.TabIndex = 494;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(189, 5);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(128, 17);
            this.labelControl1.TabIndex = 495;
            this.labelControl1.Text = "Descrição Categoria";
            // 
            // pbPesquisaCategoria
            // 
            this.pbPesquisaCategoria.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaCategoria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaCategoria.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaCategoria.Location = new System.Drawing.Point(148, 26);
            this.pbPesquisaCategoria.Margin = new System.Windows.Forms.Padding(4);
            this.pbPesquisaCategoria.Name = "pbPesquisaCategoria";
            this.pbPesquisaCategoria.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaCategoria.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaCategoria.TabIndex = 493;
            this.pbPesquisaCategoria.TabStop = false;
            this.pbPesquisaCategoria.Click += new System.EventHandler(this.pbPesquisaPessoa_Click);
            // 
            // gcGrupos
            // 
            this.gcGrupos.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gcGrupos.Location = new System.Drawing.Point(6, 255);
            this.gcGrupos.MainView = this.gridView5;
            this.gcGrupos.Margin = new System.Windows.Forms.Padding(4);
            this.gcGrupos.Name = "gcGrupos";
            this.gcGrupos.Size = new System.Drawing.Size(740, 312);
            this.gcGrupos.TabIndex = 10030;
            this.gcGrupos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
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
            this.colunaDescricao,
            this.colunaGrupo,
            this.colunaStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 288, 219);
            this.gridView5.DetailHeight = 431;
            this.gridView5.GridControl = this.gcGrupos;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Categorias";
            // 
            // colunaId
            // 
            this.colunaId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.Caption = "Cód. Cadastro";
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
            this.colunaId.Visible = true;
            this.colunaId.VisibleIndex = 0;
            this.colunaId.Width = 171;
            // 
            // colunaDescricao
            // 
            this.colunaDescricao.Caption = "Descrição";
            this.colunaDescricao.FieldName = "Descricao";
            this.colunaDescricao.MinWidth = 27;
            this.colunaDescricao.Name = "colunaDescricao";
            this.colunaDescricao.OptionsColumn.AllowEdit = false;
            this.colunaDescricao.OptionsColumn.AllowFocus = false;
            this.colunaDescricao.Visible = true;
            this.colunaDescricao.VisibleIndex = 1;
            this.colunaDescricao.Width = 257;
            // 
            // colunaGrupo
            // 
            this.colunaGrupo.Caption = "Grupo";
            this.colunaGrupo.FieldName = "Grupo";
            this.colunaGrupo.MinWidth = 27;
            this.colunaGrupo.Name = "colunaGrupo";
            this.colunaGrupo.OptionsColumn.AllowEdit = false;
            this.colunaGrupo.OptionsColumn.AllowFocus = false;
            this.colunaGrupo.OptionsFilter.AllowAutoFilter = false;
            this.colunaGrupo.Visible = true;
            this.colunaGrupo.VisibleIndex = 2;
            this.colunaGrupo.Width = 180;
            // 
            // colunaStatus
            // 
            this.colunaStatus.Caption = "Status";
            this.colunaStatus.FieldName = "Status";
            this.colunaStatus.MinWidth = 27;
            this.colunaStatus.Name = "colunaStatus";
            this.colunaStatus.OptionsColumn.AllowEdit = false;
            this.colunaStatus.OptionsColumn.AllowFocus = false;
            this.colunaStatus.OptionsFilter.AllowFilter = false;
            this.colunaStatus.Visible = true;
            this.colunaStatus.VisibleIndex = 3;
            this.colunaStatus.Width = 187;
            // 
            // gridView2
            // 
            this.gridView2.DetailHeight = 431;
            this.gridView2.GridControl = this.gcGrupos;
            this.gridView2.Name = "gridView2";
            // 
            // button3
            // 
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = global::Programax.Easy.View.Properties.Resources.icones2_19;
            this.button3.Location = new System.Drawing.Point(710, 219);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(31, 28);
            this.button3.TabIndex = 10031;
            this.button3.TabStop = false;
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Image = global::Programax.Easy.View.Properties.Resources.icones2_24;
            this.button4.Location = new System.Drawing.Point(661, 219);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(31, 28);
            this.button4.TabIndex = 10032;
            this.button4.TabStop = false;
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Image = global::Programax.Easy.View.Properties.Resources.icones2_18;
            this.button5.Location = new System.Drawing.Point(612, 220);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(31, 28);
            this.button5.TabIndex = 10033;
            this.button5.TabStop = false;
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(15, 225);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(114, 16);
            this.labelControl3.TabIndex = 10034;
            this.labelControl3.Text = "Ordem do Grupo";
            // 
            // cboOrdemGrupo
            // 
            this.cboOrdemGrupo.EnterMoveNextControl = true;
            this.cboOrdemGrupo.Location = new System.Drawing.Point(134, 222);
            this.cboOrdemGrupo.Margin = new System.Windows.Forms.Padding(4);
            this.cboOrdemGrupo.Name = "cboOrdemGrupo";
            this.cboOrdemGrupo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOrdemGrupo.Properties.Appearance.Options.UseFont = true;
            this.cboOrdemGrupo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboOrdemGrupo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Cód. Cadastro", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboOrdemGrupo.Properties.DropDownRows = 5;
            this.cboOrdemGrupo.Properties.NullText = "";
            this.cboOrdemGrupo.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboOrdemGrupo.Size = new System.Drawing.Size(66, 26);
            this.cboOrdemGrupo.TabIndex = 10035;
            // 
            // pbPesquisaOrdemGrupo
            // 
            this.pbPesquisaOrdemGrupo.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaOrdemGrupo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaOrdemGrupo.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaOrdemGrupo.Location = new System.Drawing.Point(207, 223);
            this.pbPesquisaOrdemGrupo.Margin = new System.Windows.Forms.Padding(4);
            this.pbPesquisaOrdemGrupo.Name = "pbPesquisaOrdemGrupo";
            this.pbPesquisaOrdemGrupo.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaOrdemGrupo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaOrdemGrupo.TabIndex = 10036;
            this.pbPesquisaOrdemGrupo.TabStop = false;
            // 
            // lookUpEdit2
            // 
            this.lookUpEdit2.EnterMoveNextControl = true;
            this.lookUpEdit2.Location = new System.Drawing.Point(445, 221);
            this.lookUpEdit2.Margin = new System.Windows.Forms.Padding(4);
            this.lookUpEdit2.Name = "lookUpEdit2";
            this.lookUpEdit2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEdit2.Properties.Appearance.Options.UseFont = true;
            this.lookUpEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit2.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Cód. Cadastro", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.lookUpEdit2.Properties.DropDownRows = 5;
            this.lookUpEdit2.Properties.NullText = "";
            this.lookUpEdit2.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.lookUpEdit2.Size = new System.Drawing.Size(62, 26);
            this.lookUpEdit2.TabIndex = 10038;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(309, 225);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(131, 16);
            this.labelControl4.TabIndex = 10037;
            this.labelControl4.Text = "Detalhar Categoria";
            // 
            // FormRelacionarCategoriasFinanceiras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1446, 780);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "FormRelacionarCategoriasFinanceiras";
            this.Load += new System.EventHandler(this.FormCadastroCategoriasFinanceiras_Load);
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGrupos.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnlCategoria.ResumeLayout(false);
            this.pnlCategoria.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCategoria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGrupos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOrdemGrupo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaOrdemGrupo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnSair;
        private DevExpress.XtraEditors.LookUpEdit cboGrupos;
        private DevExpress.XtraEditors.LabelControl labTipoInscricao;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAdicionarGrupo;
        private System.Windows.Forms.Panel pnlCategoria;
        private System.Windows.Forms.Panel panel6;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Button btnAdicionarSubGrupo;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnAdicionarCategoria;
        private DevExpress.XtraEditors.TextEdit txtId;
        private DevExpress.XtraEditors.LabelControl labCodigo;
        private DevExpress.XtraEditors.TextEdit txtDescricao;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.PictureBox pbPesquisaCategoria;
        private DevExpress.XtraGrid.GridControl gcGrupos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaGrupo;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private DevExpress.XtraEditors.LookUpEdit cboOrdemGrupo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.PictureBox pbPesquisaOrdemGrupo;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}