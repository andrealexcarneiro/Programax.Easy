namespace Programax.Easy.View.Telas.Relatorios
{
    partial class FormRelatorioProdutos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRelatorioProdutos));
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.rdbEstoqueMinimo = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pnlOrdenacao = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.chkOrdenarPorCodigo = new System.Windows.Forms.RadioButton();
            this.cboGrupos = new DevExpress.XtraEditors.LookUpEdit();
            this.cboMarcas = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.cboSubGrupos = new DevExpress.XtraEditors.LookUpEdit();
            this.labTipoInscricao = new DevExpress.XtraEditors.LabelControl();
            this.cboCategorias = new DevExpress.XtraEditors.LookUpEdit();
            this.cboFabricantes = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl53 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMostrarNcms = new System.Windows.Forms.CheckBox();
            this.txtEstoqueMaiorQue = new DevExpress.XtraEditors.TextEdit();
            this.labEstoqueMaiorQue = new DevExpress.XtraEditors.LabelControl();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.rdbItensComEstoqueMinimo = new System.Windows.Forms.RadioButton();
            this.rdbItensDDVAbaixoDe = new System.Windows.Forms.RadioButton();
            this.txtDdvAbaixeDe = new DevExpress.XtraEditors.TextEdit();
            this.labelControl44 = new DevExpress.XtraEditors.LabelControl();
            this.cboTamanhos = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.rdbEstoqueMinimo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlOrdenacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGrupos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMarcas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubGrupos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategorias.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFabricantes.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEstoqueMaiorQue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDdvAbaixeDe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTamanhos.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizar.FlatAppearance.BorderSize = 0;
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizar.Image = global::Programax.Easy.View.Properties.Resources.icone_Imprimir;
            this.btnAtualizar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAtualizar.Location = new System.Drawing.Point(16, 361);
            this.btnAtualizar.Margin = new System.Windows.Forms.Padding(0);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(147, 49);
            this.btnAtualizar.TabIndex = 50;
            this.btnAtualizar.Text = " Imprimir";
            this.btnAtualizar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAtualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(920, 361);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(133, 49);
            this.btnSair.TabIndex = 10082;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // rdbEstoqueMinimo
            // 
            this.rdbEstoqueMinimo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbEstoqueMinimo.Image = ((System.Drawing.Image)(resources.GetObject("rdbEstoqueMinimo.Image")));
            this.rdbEstoqueMinimo.Location = new System.Drawing.Point(-128, 345);
            this.rdbEstoqueMinimo.Margin = new System.Windows.Forms.Padding(4);
            this.rdbEstoqueMinimo.Name = "rdbEstoqueMinimo";
            this.rdbEstoqueMinimo.Size = new System.Drawing.Size(1309, 12);
            this.rdbEstoqueMinimo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rdbEstoqueMinimo.TabIndex = 10081;
            this.rdbEstoqueMinimo.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-105, 60);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1284, 12);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10080;
            this.pictureBox1.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(16, 17);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(317, 36);
            this.labelControl1.TabIndex = 10079;
            this.labelControl1.Text = "RELATÓRIO DE ITENS";
            // 
            // pnlOrdenacao
            // 
            this.pnlOrdenacao.Controls.Add(this.radioButton2);
            this.pnlOrdenacao.Controls.Add(this.radioButton1);
            this.pnlOrdenacao.Controls.Add(this.chkOrdenarPorCodigo);
            this.pnlOrdenacao.Location = new System.Drawing.Point(16, 85);
            this.pnlOrdenacao.Margin = new System.Windows.Forms.Padding(4);
            this.pnlOrdenacao.Name = "pnlOrdenacao";
            this.pnlOrdenacao.Padding = new System.Windows.Forms.Padding(4);
            this.pnlOrdenacao.Size = new System.Drawing.Size(337, 124);
            this.pnlOrdenacao.TabIndex = 1;
            this.pnlOrdenacao.TabStop = false;
            this.pnlOrdenacao.Text = "Ordernar Por";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(17, 79);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(107, 21);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Tag = "2";
            this.radioButton2.Text = "Valor Venda";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbOrdemListaCodigo_KeyDown);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(196, 34);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(92, 21);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.Tag = "1";
            this.radioButton1.Text = "Descrição";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbOrdemListaCodigo_KeyDown);
            // 
            // chkOrdenarPorCodigo
            // 
            this.chkOrdenarPorCodigo.AutoSize = true;
            this.chkOrdenarPorCodigo.Checked = true;
            this.chkOrdenarPorCodigo.Location = new System.Drawing.Point(17, 34);
            this.chkOrdenarPorCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.chkOrdenarPorCodigo.Name = "chkOrdenarPorCodigo";
            this.chkOrdenarPorCodigo.Size = new System.Drawing.Size(73, 21);
            this.chkOrdenarPorCodigo.TabIndex = 1;
            this.chkOrdenarPorCodigo.TabStop = true;
            this.chkOrdenarPorCodigo.Tag = "0";
            this.chkOrdenarPorCodigo.Text = "Código";
            this.chkOrdenarPorCodigo.UseVisualStyleBackColor = true;
            this.chkOrdenarPorCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbOrdemListaCodigo_KeyDown);
            // 
            // cboGrupos
            // 
            this.cboGrupos.EnterMoveNextControl = true;
            this.cboGrupos.Location = new System.Drawing.Point(361, 302);
            this.cboGrupos.Margin = new System.Windows.Forms.Padding(4);
            this.cboGrupos.Name = "cboGrupos";
            this.cboGrupos.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGrupos.Properties.Appearance.Options.UseFont = true;
            this.cboGrupos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboGrupos.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboGrupos.Properties.DropDownRows = 5;
            this.cboGrupos.Properties.NullText = "";
            this.cboGrupos.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboGrupos.Size = new System.Drawing.Size(337, 26);
            this.cboGrupos.TabIndex = 10086;
            this.cboGrupos.EditValueChanged += new System.EventHandler(this.cboGrupos_EditValueChanged);
            // 
            // cboMarcas
            // 
            this.cboMarcas.EnterMoveNextControl = true;
            this.cboMarcas.Location = new System.Drawing.Point(16, 245);
            this.cboMarcas.Margin = new System.Windows.Forms.Padding(4);
            this.cboMarcas.Name = "cboMarcas";
            this.cboMarcas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMarcas.Properties.Appearance.Options.UseFont = true;
            this.cboMarcas.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMarcas.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboMarcas.Properties.DropDownRows = 5;
            this.cboMarcas.Properties.NullText = "";
            this.cboMarcas.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboMarcas.Size = new System.Drawing.Size(337, 26);
            this.cboMarcas.TabIndex = 10083;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(361, 283);
            this.labelControl16.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(40, 17);
            this.labelControl16.TabIndex = 10089;
            this.labelControl16.Text = "Grupo";
            // 
            // cboSubGrupos
            // 
            this.cboSubGrupos.EnterMoveNextControl = true;
            this.cboSubGrupos.Location = new System.Drawing.Point(707, 302);
            this.cboSubGrupos.Margin = new System.Windows.Forms.Padding(4);
            this.cboSubGrupos.Name = "cboSubGrupos";
            this.cboSubGrupos.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSubGrupos.Properties.Appearance.Options.UseFont = true;
            this.cboSubGrupos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSubGrupos.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboSubGrupos.Properties.DropDownRows = 5;
            this.cboSubGrupos.Properties.NullText = "";
            this.cboSubGrupos.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboSubGrupos.Size = new System.Drawing.Size(336, 26);
            this.cboSubGrupos.TabIndex = 10087;
            // 
            // labTipoInscricao
            // 
            this.labTipoInscricao.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTipoInscricao.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labTipoInscricao.Appearance.Options.UseFont = true;
            this.labTipoInscricao.Appearance.Options.UseForeColor = true;
            this.labTipoInscricao.Location = new System.Drawing.Point(16, 283);
            this.labTipoInscricao.Margin = new System.Windows.Forms.Padding(4);
            this.labTipoInscricao.Name = "labTipoInscricao";
            this.labTipoInscricao.Size = new System.Drawing.Size(61, 17);
            this.labTipoInscricao.TabIndex = 10088;
            this.labTipoInscricao.Text = "Categoria";
            // 
            // cboCategorias
            // 
            this.cboCategorias.EnterMoveNextControl = true;
            this.cboCategorias.Location = new System.Drawing.Point(16, 302);
            this.cboCategorias.Margin = new System.Windows.Forms.Padding(4);
            this.cboCategorias.Name = "cboCategorias";
            this.cboCategorias.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategorias.Properties.Appearance.Options.UseFont = true;
            this.cboCategorias.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCategorias.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboCategorias.Properties.DropDownRows = 5;
            this.cboCategorias.Properties.NullText = "";
            this.cboCategorias.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboCategorias.Size = new System.Drawing.Size(337, 26);
            this.cboCategorias.TabIndex = 10085;
            this.cboCategorias.EditValueChanged += new System.EventHandler(this.cboCategorias_EditValueChanged);
            // 
            // cboFabricantes
            // 
            this.cboFabricantes.EnterMoveNextControl = true;
            this.cboFabricantes.Location = new System.Drawing.Point(361, 245);
            this.cboFabricantes.Margin = new System.Windows.Forms.Padding(4);
            this.cboFabricantes.Name = "cboFabricantes";
            this.cboFabricantes.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFabricantes.Properties.Appearance.Options.UseFont = true;
            this.cboFabricantes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFabricantes.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboFabricantes.Properties.DropDownRows = 5;
            this.cboFabricantes.Properties.NullText = "";
            this.cboFabricantes.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboFabricantes.Size = new System.Drawing.Size(337, 26);
            this.cboFabricantes.TabIndex = 10084;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Appearance.Options.UseForeColor = true;
            this.labelControl8.Location = new System.Drawing.Point(16, 226);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(39, 17);
            this.labelControl8.TabIndex = 10090;
            this.labelControl8.Text = "Marca";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.Location = new System.Drawing.Point(707, 283);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(62, 17);
            this.labelControl9.TabIndex = 10091;
            this.labelControl9.Text = "Subgrupo";
            // 
            // labelControl53
            // 
            this.labelControl53.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl53.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl53.Appearance.Options.UseFont = true;
            this.labelControl53.Appearance.Options.UseForeColor = true;
            this.labelControl53.Location = new System.Drawing.Point(361, 226);
            this.labelControl53.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl53.Name = "labelControl53";
            this.labelControl53.Size = new System.Drawing.Size(67, 17);
            this.labelControl53.TabIndex = 10092;
            this.labelControl53.Text = "Fabricante";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMostrarNcms);
            this.groupBox1.Controls.Add(this.txtEstoqueMaiorQue);
            this.groupBox1.Controls.Add(this.labEstoqueMaiorQue);
            this.groupBox1.Controls.Add(this.labStatus);
            this.groupBox1.Controls.Add(this.cboStatus);
            this.groupBox1.Controls.Add(this.rdbItensComEstoqueMinimo);
            this.groupBox1.Controls.Add(this.rdbItensDDVAbaixoDe);
            this.groupBox1.Controls.Add(this.txtDdvAbaixeDe);
            this.groupBox1.Location = new System.Drawing.Point(361, 85);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(681, 124);
            this.groupBox1.TabIndex = 10093;
            this.groupBox1.TabStop = false;
            // 
            // chkMostrarNcms
            // 
            this.chkMostrarNcms.AutoSize = true;
            this.chkMostrarNcms.Location = new System.Drawing.Point(550, 87);
            this.chkMostrarNcms.Name = "chkMostrarNcms";
            this.chkMostrarNcms.Size = new System.Drawing.Size(119, 21);
            this.chkMostrarNcms.TabIndex = 10102;
            this.chkMostrarNcms.Text = "Mostrar NCMs";
            this.chkMostrarNcms.UseVisualStyleBackColor = true;
            // 
            // txtEstoqueMaiorQue
            // 
            this.txtEstoqueMaiorQue.EnterMoveNextControl = true;
            this.txtEstoqueMaiorQue.Location = new System.Drawing.Point(443, 84);
            this.txtEstoqueMaiorQue.Margin = new System.Windows.Forms.Padding(4);
            this.txtEstoqueMaiorQue.Name = "txtEstoqueMaiorQue";
            this.txtEstoqueMaiorQue.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstoqueMaiorQue.Properties.Appearance.Options.UseFont = true;
            this.txtEstoqueMaiorQue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtEstoqueMaiorQue.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtEstoqueMaiorQue.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEstoqueMaiorQue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtEstoqueMaiorQue.Properties.Mask.EditMask = "[0-9]{0,11}";
            this.txtEstoqueMaiorQue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtEstoqueMaiorQue.Properties.Mask.ShowPlaceHolders = false;
            this.txtEstoqueMaiorQue.Properties.MaxLength = 30;
            this.txtEstoqueMaiorQue.Size = new System.Drawing.Size(73, 26);
            this.txtEstoqueMaiorQue.TabIndex = 10101;
            // 
            // labEstoqueMaiorQue
            // 
            this.labEstoqueMaiorQue.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labEstoqueMaiorQue.Appearance.Options.UseFont = true;
            this.labEstoqueMaiorQue.Location = new System.Drawing.Point(211, 90);
            this.labEstoqueMaiorQue.Margin = new System.Windows.Forms.Padding(4);
            this.labEstoqueMaiorQue.Name = "labEstoqueMaiorQue";
            this.labEstoqueMaiorQue.Size = new System.Drawing.Size(209, 17);
            this.labEstoqueMaiorQue.TabIndex = 10100;
            this.labEstoqueMaiorQue.Text = "Itens com estoque maior que";
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Appearance.Options.UseFont = true;
            this.labStatus.Location = new System.Drawing.Point(345, 17);
            this.labStatus.Margin = new System.Windows.Forms.Padding(4);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(40, 17);
            this.labStatus.TabIndex = 10099;
            this.labStatus.Text = "Status";
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(345, 41);
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
            this.cboStatus.Size = new System.Drawing.Size(328, 26);
            this.cboStatus.TabIndex = 10098;
            // 
            // rdbItensComEstoqueMinimo
            // 
            this.rdbItensComEstoqueMinimo.AutoSize = true;
            this.rdbItensComEstoqueMinimo.Location = new System.Drawing.Point(8, 87);
            this.rdbItensComEstoqueMinimo.Margin = new System.Windows.Forms.Padding(4);
            this.rdbItensComEstoqueMinimo.Name = "rdbItensComEstoqueMinimo";
            this.rdbItensComEstoqueMinimo.Size = new System.Drawing.Size(192, 21);
            this.rdbItensComEstoqueMinimo.TabIndex = 623;
            this.rdbItensComEstoqueMinimo.Tag = "0";
            this.rdbItensComEstoqueMinimo.Text = "Itens com estoque mínimo";
            this.rdbItensComEstoqueMinimo.UseVisualStyleBackColor = true;
            // 
            // rdbItensDDVAbaixoDe
            // 
            this.rdbItensDDVAbaixoDe.AutoSize = true;
            this.rdbItensDDVAbaixoDe.Checked = true;
            this.rdbItensDDVAbaixoDe.Location = new System.Drawing.Point(8, 43);
            this.rdbItensDDVAbaixoDe.Margin = new System.Windows.Forms.Padding(4);
            this.rdbItensDDVAbaixoDe.Name = "rdbItensDDVAbaixoDe";
            this.rdbItensDDVAbaixoDe.Size = new System.Drawing.Size(187, 21);
            this.rdbItensDDVAbaixoDe.TabIndex = 622;
            this.rdbItensDDVAbaixoDe.TabStop = true;
            this.rdbItensDDVAbaixoDe.Tag = "1";
            this.rdbItensDDVAbaixoDe.Text = "Itens com DDV abaixo de";
            this.rdbItensDDVAbaixoDe.UseVisualStyleBackColor = true;
            this.rdbItensDDVAbaixoDe.CheckedChanged += new System.EventHandler(this.rdbItensDDVAbaixoDe_CheckedChanged);
            // 
            // txtDdvAbaixeDe
            // 
            this.txtDdvAbaixeDe.EnterMoveNextControl = true;
            this.txtDdvAbaixeDe.Location = new System.Drawing.Point(211, 41);
            this.txtDdvAbaixeDe.Margin = new System.Windows.Forms.Padding(4);
            this.txtDdvAbaixeDe.Name = "txtDdvAbaixeDe";
            this.txtDdvAbaixeDe.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDdvAbaixeDe.Properties.Appearance.Options.UseFont = true;
            this.txtDdvAbaixeDe.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDdvAbaixeDe.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDdvAbaixeDe.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDdvAbaixeDe.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDdvAbaixeDe.Properties.Mask.EditMask = "[0-9]{0,11}";
            this.txtDdvAbaixeDe.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtDdvAbaixeDe.Properties.Mask.ShowPlaceHolders = false;
            this.txtDdvAbaixeDe.Properties.MaxLength = 30;
            this.txtDdvAbaixeDe.Size = new System.Drawing.Size(127, 26);
            this.txtDdvAbaixeDe.TabIndex = 619;
            // 
            // labelControl44
            // 
            this.labelControl44.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl44.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl44.Appearance.Options.UseFont = true;
            this.labelControl44.Appearance.Options.UseForeColor = true;
            this.labelControl44.Location = new System.Drawing.Point(707, 226);
            this.labelControl44.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl44.Name = "labelControl44";
            this.labelControl44.Size = new System.Drawing.Size(60, 17);
            this.labelControl44.TabIndex = 10095;
            this.labelControl44.Text = "Tamanho";
            // 
            // cboTamanhos
            // 
            this.cboTamanhos.EnterMoveNextControl = true;
            this.cboTamanhos.Location = new System.Drawing.Point(707, 245);
            this.cboTamanhos.Margin = new System.Windows.Forms.Padding(4);
            this.cboTamanhos.Name = "cboTamanhos";
            this.cboTamanhos.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTamanhos.Properties.Appearance.Options.UseFont = true;
            this.cboTamanhos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTamanhos.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboTamanhos.Properties.DropDownRows = 5;
            this.cboTamanhos.Properties.NullText = "";
            this.cboTamanhos.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboTamanhos.Size = new System.Drawing.Size(336, 26);
            this.cboTamanhos.TabIndex = 10094;
            // 
            // FormRelatorioProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 415);
            this.Controls.Add(this.labelControl44);
            this.Controls.Add(this.cboTamanhos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboGrupos);
            this.Controls.Add(this.cboMarcas);
            this.Controls.Add(this.labelControl16);
            this.Controls.Add(this.cboSubGrupos);
            this.Controls.Add(this.labTipoInscricao);
            this.Controls.Add(this.cboCategorias);
            this.Controls.Add(this.cboFabricantes);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl53);
            this.Controls.Add(this.pnlOrdenacao);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.rdbEstoqueMinimo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormRelatorioProdutos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRelatorioParceiro";
            ((System.ComponentModel.ISupportInitialize)(this.rdbEstoqueMinimo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlOrdenacao.ResumeLayout(false);
            this.pnlOrdenacao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGrupos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMarcas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSubGrupos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategorias.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFabricantes.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEstoqueMaiorQue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDdvAbaixeDe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTamanhos.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.PictureBox rdbEstoqueMinimo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.GroupBox pnlOrdenacao;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton chkOrdenarPorCodigo;
        private DevExpress.XtraEditors.LookUpEdit cboGrupos;
        private DevExpress.XtraEditors.LookUpEdit cboMarcas;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LookUpEdit cboSubGrupos;
        private DevExpress.XtraEditors.LabelControl labTipoInscricao;
        private DevExpress.XtraEditors.LookUpEdit cboCategorias;
        private DevExpress.XtraEditors.LookUpEdit cboFabricantes;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl53;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit txtDdvAbaixeDe;
        private DevExpress.XtraEditors.LabelControl labelControl44;
        private DevExpress.XtraEditors.LookUpEdit cboTamanhos;
        private System.Windows.Forms.RadioButton rdbItensComEstoqueMinimo;
        private System.Windows.Forms.RadioButton rdbItensDDVAbaixoDe;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.TextEdit txtEstoqueMaiorQue;
        private DevExpress.XtraEditors.LabelControl labEstoqueMaiorQue;
        private System.Windows.Forms.CheckBox chkMostrarNcms;
    }
}