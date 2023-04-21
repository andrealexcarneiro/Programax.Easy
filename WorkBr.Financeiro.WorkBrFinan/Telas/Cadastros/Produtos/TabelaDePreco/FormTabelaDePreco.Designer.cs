namespace Programax.Easy.View.Telas.Produtos.TabelaDePreco
{
    partial class FormTabelaDePreco
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTabelaDePreco));
            this.dtValidade = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtAcrescimo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricao = new DevExpress.XtraEditors.TextEdit();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.labDescricao = new DevExpress.XtraEditors.LabelControl();
            this.labId = new DevExpress.XtraEditors.LabelControl();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.pbPesquisaCadastro = new System.Windows.Forms.PictureBox();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDecrescimo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtFrete = new DevExpress.XtraEditors.TextEdit();
            this.panel7 = new System.Windows.Forms.Panel();
            this.rdbAcrescimoPercentual = new System.Windows.Forms.RadioButton();
            this.rdbAcrescimoValor = new System.Windows.Forms.RadioButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rdbDecrescimoPercentual = new System.Windows.Forms.RadioButton();
            this.rdbDecrescimoValor = new System.Windows.Forms.RadioButton();
            this.panel8 = new System.Windows.Forms.Panel();
            this.rdbFretePercentual = new System.Windows.Forms.RadioButton();
            this.rdbFreteValor = new System.Windows.Forms.RadioButton();
            this.txtDtCadastro = new DevExpress.XtraEditors.TextEdit();
            this.txtId = new DevExpress.XtraEditors.TextEdit();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtValidade.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtValidade.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAcrescimo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecrescimo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrete.Properties)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtCadastro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Location = new System.Drawing.Point(7, 0);
            this.painelBotoes.Size = new System.Drawing.Size(603, 60);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.txtId);
            this.panelConteudo.Controls.Add(this.txtDtCadastro);
            this.panelConteudo.Controls.Add(this.panel8);
            this.panelConteudo.Controls.Add(this.panel6);
            this.panelConteudo.Controls.Add(this.panel7);
            this.panelConteudo.Controls.Add(this.labelControl3);
            this.panelConteudo.Controls.Add(this.txtFrete);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.txtDecrescimo);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.pbPesquisaCadastro);
            this.panelConteudo.Controls.Add(this.labId);
            this.panelConteudo.Controls.Add(this.labDescricao);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Controls.Add(this.labDataCadastro);
            this.panelConteudo.Controls.Add(this.dtValidade);
            this.panelConteudo.Controls.Add(this.txtDescricao);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.labelControl8);
            this.panelConteudo.Controls.Add(this.txtAcrescimo);
            this.panelConteudo.Size = new System.Drawing.Size(535, 106);
            // 
            // dtValidade
            // 
            this.dtValidade.EditValue = null;
            this.dtValidade.EnterMoveNextControl = true;
            this.dtValidade.Location = new System.Drawing.Point(418, 70);
            this.dtValidade.Name = "dtValidade";
            this.dtValidade.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtValidade.Properties.Appearance.Options.UseFont = true;
            this.dtValidade.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtValidade.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtValidade.Size = new System.Drawing.Size(113, 22);
            this.dtValidade.TabIndex = 17;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(418, 54);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 151;
            this.labelControl1.Text = "Validade";
            // 
            // txtAcrescimo
            // 
            this.txtAcrescimo.EnterMoveNextControl = true;
            this.txtAcrescimo.Location = new System.Drawing.Point(3, 70);
            this.txtAcrescimo.Name = "txtAcrescimo";
            this.txtAcrescimo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAcrescimo.Properties.Appearance.Options.UseFont = true;
            this.txtAcrescimo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtAcrescimo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtAcrescimo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtAcrescimo.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtAcrescimo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtAcrescimo.Properties.Mask.EditMask = "[0-9]{1,11}([\\.\\,][0-9]{0,2})?";
            this.txtAcrescimo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtAcrescimo.Size = new System.Drawing.Size(133, 22);
            this.txtAcrescimo.TabIndex = 8;
            this.txtAcrescimo.EditValueChanged += new System.EventHandler(this.txtAcrescimo_EditValueChanged);
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Location = new System.Drawing.Point(3, 54);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(59, 13);
            this.labelControl8.TabIndex = 144;
            this.labelControl8.Text = "Acréscimo";
            // 
            // txtDescricao
            // 
            this.txtDescricao.EnterMoveNextControl = true;
            this.txtDescricao.Location = new System.Drawing.Point(131, 19);
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
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Location = new System.Drawing.Point(447, 3);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(56, 13);
            this.labDataCadastro.TabIndex = 141;
            this.labDataCadastro.Text = "Dt Cadastro";
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(340, 3);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 140;
            this.labStatus.Text = "Status";
            // 
            // labDescricao
            // 
            this.labDescricao.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDescricao.Location = new System.Drawing.Point(131, 3);
            this.labDescricao.Name = "labDescricao";
            this.labDescricao.Size = new System.Drawing.Size(93, 13);
            this.labDescricao.TabIndex = 139;
            this.labDescricao.Text = "Nome da Tabela";
            // 
            // labId
            // 
            this.labId.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labId.Location = new System.Drawing.Point(5, 3);
            this.labId.Name = "labId";
            this.labId.Size = new System.Drawing.Size(83, 13);
            this.labId.TabIndex = 137;
            this.labId.Text = "Cód. Cadastro";
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
            this.btnSair.TabIndex = 154;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
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
            this.btnGravar.TabIndex = 12;
            this.btnGravar.Text = " Salvar";
            this.btnGravar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGravar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(340, 19);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboStatus.Properties.DisplayMember = "Descricao";
            this.cboStatus.Properties.DropDownRows = 3;
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboStatus.Properties.ValueMember = "Valor";
            this.cboStatus.Size = new System.Drawing.Size(101, 22);
            this.cboStatus.TabIndex = 3;
            // 
            // pbPesquisaCadastro
            // 
            this.pbPesquisaCadastro.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaCadastro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaCadastro.Image = ((System.Drawing.Image)(resources.GetObject("pbPesquisaCadastro.Image")));
            this.pbPesquisaCadastro.Location = new System.Drawing.Point(103, 19);
            this.pbPesquisaCadastro.Name = "pbPesquisaCadastro";
            this.pbPesquisaCadastro.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaCadastro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaCadastro.TabIndex = 217;
            this.pbPesquisaCadastro.TabStop = false;
            this.pbPesquisaCadastro.Click += new System.EventHandler(this.pbPesquisaCadastro_Click);
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
            this.btnLimpar.TabIndex = 1001;
            this.btnLimpar.Text = " Limpar";
            this.btnLimpar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnGravar);
            this.flowLayoutPanel1.Controls.Add(this.btnLimpar);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(455, 46);
            this.flowLayoutPanel1.TabIndex = 1002;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(142, 54);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(67, 13);
            this.labelControl2.TabIndex = 219;
            this.labelControl2.Text = "Decréscimo";
            // 
            // txtDecrescimo
            // 
            this.txtDecrescimo.EnterMoveNextControl = true;
            this.txtDecrescimo.Location = new System.Drawing.Point(142, 70);
            this.txtDecrescimo.Name = "txtDecrescimo";
            this.txtDecrescimo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDecrescimo.Properties.Appearance.Options.UseFont = true;
            this.txtDecrescimo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDecrescimo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtDecrescimo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDecrescimo.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDecrescimo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDecrescimo.Properties.Mask.EditMask = "[0-9]{1,11}([\\.\\,][0-9]{0,2})?";
            this.txtDecrescimo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtDecrescimo.Size = new System.Drawing.Size(141, 22);
            this.txtDecrescimo.TabIndex = 12;
            this.txtDecrescimo.EditValueChanged += new System.EventHandler(this.txtDecrescimo_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(289, 54);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(29, 13);
            this.labelControl3.TabIndex = 221;
            this.labelControl3.Text = "Frete";
            // 
            // txtFrete
            // 
            this.txtFrete.EnterMoveNextControl = true;
            this.txtFrete.Location = new System.Drawing.Point(289, 70);
            this.txtFrete.Name = "txtFrete";
            this.txtFrete.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrete.Properties.Appearance.Options.UseFont = true;
            this.txtFrete.Properties.Appearance.Options.UseTextOptions = true;
            this.txtFrete.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtFrete.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtFrete.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtFrete.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtFrete.Properties.Mask.EditMask = "[0-9]{1,11}([\\.\\,][0-9]{0,2})?";
            this.txtFrete.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtFrete.Size = new System.Drawing.Size(123, 22);
            this.txtFrete.TabIndex = 16;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.rdbAcrescimoPercentual);
            this.panel7.Controls.Add(this.rdbAcrescimoValor);
            this.panel7.Location = new System.Drawing.Point(68, 54);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(68, 14);
            this.panel7.TabIndex = 5;
            // 
            // rdbAcrescimoPercentual
            // 
            this.rdbAcrescimoPercentual.Checked = true;
            this.rdbAcrescimoPercentual.Location = new System.Drawing.Point(0, 0);
            this.rdbAcrescimoPercentual.Name = "rdbAcrescimoPercentual";
            this.rdbAcrescimoPercentual.Size = new System.Drawing.Size(33, 15);
            this.rdbAcrescimoPercentual.TabIndex = 6;
            this.rdbAcrescimoPercentual.TabStop = true;
            this.rdbAcrescimoPercentual.Text = "%";
            this.rdbAcrescimoPercentual.UseVisualStyleBackColor = true;
            this.rdbAcrescimoPercentual.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbAcrescimoPercentual_KeyDown);
            // 
            // rdbAcrescimoValor
            // 
            this.rdbAcrescimoValor.Location = new System.Drawing.Point(34, 0);
            this.rdbAcrescimoValor.Name = "rdbAcrescimoValor";
            this.rdbAcrescimoValor.Size = new System.Drawing.Size(31, 15);
            this.rdbAcrescimoValor.TabIndex = 6;
            this.rdbAcrescimoValor.Text = "$";
            this.rdbAcrescimoValor.UseVisualStyleBackColor = true;
            this.rdbAcrescimoValor.CheckedChanged += new System.EventHandler(this.rdbAcrescimoValor_CheckedChanged);
            this.rdbAcrescimoValor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbAcrescimoPercentual_KeyDown);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.rdbDecrescimoPercentual);
            this.panel6.Controls.Add(this.rdbDecrescimoValor);
            this.panel6.Location = new System.Drawing.Point(215, 54);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(68, 14);
            this.panel6.TabIndex = 9;
            // 
            // rdbDecrescimoPercentual
            // 
            this.rdbDecrescimoPercentual.Checked = true;
            this.rdbDecrescimoPercentual.Location = new System.Drawing.Point(0, 0);
            this.rdbDecrescimoPercentual.Name = "rdbDecrescimoPercentual";
            this.rdbDecrescimoPercentual.Size = new System.Drawing.Size(33, 15);
            this.rdbDecrescimoPercentual.TabIndex = 10;
            this.rdbDecrescimoPercentual.TabStop = true;
            this.rdbDecrescimoPercentual.Text = "%";
            this.rdbDecrescimoPercentual.UseVisualStyleBackColor = true;
            this.rdbDecrescimoPercentual.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbDecrescimoPercentual_KeyDown);
            // 
            // rdbDecrescimoValor
            // 
            this.rdbDecrescimoValor.Location = new System.Drawing.Point(34, 0);
            this.rdbDecrescimoValor.Name = "rdbDecrescimoValor";
            this.rdbDecrescimoValor.Size = new System.Drawing.Size(31, 15);
            this.rdbDecrescimoValor.TabIndex = 10;
            this.rdbDecrescimoValor.Text = "$";
            this.rdbDecrescimoValor.UseVisualStyleBackColor = true;
            this.rdbDecrescimoValor.CheckedChanged += new System.EventHandler(this.rdbDecrescimoValor_CheckedChanged);
            this.rdbDecrescimoValor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbDecrescimoPercentual_KeyDown);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.rdbFretePercentual);
            this.panel8.Controls.Add(this.rdbFreteValor);
            this.panel8.Location = new System.Drawing.Point(324, 55);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(68, 14);
            this.panel8.TabIndex = 13;
            // 
            // rdbFretePercentual
            // 
            this.rdbFretePercentual.Checked = true;
            this.rdbFretePercentual.Location = new System.Drawing.Point(0, 0);
            this.rdbFretePercentual.Name = "rdbFretePercentual";
            this.rdbFretePercentual.Size = new System.Drawing.Size(33, 15);
            this.rdbFretePercentual.TabIndex = 14;
            this.rdbFretePercentual.TabStop = true;
            this.rdbFretePercentual.Text = "%";
            this.rdbFretePercentual.UseVisualStyleBackColor = true;
            this.rdbFretePercentual.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbFretePercentual_KeyDown);
            // 
            // rdbFreteValor
            // 
            this.rdbFreteValor.Location = new System.Drawing.Point(34, 0);
            this.rdbFreteValor.Name = "rdbFreteValor";
            this.rdbFreteValor.Size = new System.Drawing.Size(31, 15);
            this.rdbFreteValor.TabIndex = 14;
            this.rdbFreteValor.Text = "$";
            this.rdbFreteValor.UseVisualStyleBackColor = true;
            this.rdbFreteValor.CheckedChanged += new System.EventHandler(this.rdbFreteValor_CheckedChanged);
            this.rdbFreteValor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbFretePercentual_KeyDown);
            // 
            // txtDtCadastro
            // 
            this.txtDtCadastro.EnterMoveNextControl = true;
            this.txtDtCadastro.Location = new System.Drawing.Point(447, 19);
            this.txtDtCadastro.Name = "txtDtCadastro";
            this.txtDtCadastro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDtCadastro.Properties.Appearance.Options.UseFont = true;
            this.txtDtCadastro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDtCadastro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDtCadastro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDtCadastro.Properties.Mask.EditMask = "99/";
            this.txtDtCadastro.Properties.MaxLength = 50;
            this.txtDtCadastro.Properties.ReadOnly = true;
            this.txtDtCadastro.Size = new System.Drawing.Size(84, 22);
            this.txtDtCadastro.TabIndex = 4;
            // 
            // txtId
            // 
            this.txtId.EnterMoveNextControl = true;
            this.txtId.Location = new System.Drawing.Point(3, 19);
            this.txtId.Name = "txtId";
            this.txtId.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Properties.Appearance.Options.UseFont = true;
            this.txtId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtId.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtId.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtId.Properties.Mask.EditMask = "99/";
            this.txtId.Properties.MaxLength = 50;
            this.txtId.Size = new System.Drawing.Size(94, 22);
            this.txtId.TabIndex = 1;
            this.txtId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtId_KeyPress);
            this.txtId.Leave += new System.EventHandler(this.txtId_Leave);
            // 
            // FormTabelaDePreco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 217);
            this.Name = "FormTabelaDePreco";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtValidade.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtValidade.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAcrescimo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDecrescimo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrete.Properties)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDtCadastro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dtValidade;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtAcrescimo;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtDescricao;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraEditors.LabelControl labDescricao;
        private DevExpress.XtraEditors.LabelControl labId;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.PictureBox pbPesquisaCadastro;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnLimpar;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtDecrescimo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtFrete;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.RadioButton rdbFretePercentual;
        private System.Windows.Forms.RadioButton rdbFreteValor;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton rdbDecrescimoPercentual;
        private System.Windows.Forms.RadioButton rdbDecrescimoValor;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.RadioButton rdbAcrescimoPercentual;
        private System.Windows.Forms.RadioButton rdbAcrescimoValor;
        private DevExpress.XtraEditors.TextEdit txtDtCadastro;
        private DevExpress.XtraEditors.TextEdit txtId;
    }
}