namespace Programax.Easy.View.Telas.Vendas.Roteiros
{
    partial class FormEditarRoteiro
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
            this.components = new System.ComponentModel.Container();
            this.labelControl67 = new DevExpress.XtraEditors.LabelControl();
            this.txtIdPessoa = new DevExpress.XtraEditors.TextEdit();
            this.txtUsuario = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtNomePessoa = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroPedidoVendas = new DevExpress.XtraEditors.TextEdit();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnPesquisaPessoa = new System.Windows.Forms.PictureBox();
            this.pnlInformacoesContaPagarReceber = new System.Windows.Forms.Panel();
            this.txtNumeroAgendaRoteiro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataAgenda = new DevExpress.XtraEditors.DateEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.cboPeriodo = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.hint = new System.Windows.Forms.ToolTip(this.components);
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdPessoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsuario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomePessoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPedidoVendas.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaPessoa)).BeginInit();
            this.pnlInformacoesContaPagarReceber.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroAgendaRoteiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataAgenda.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataAgenda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPeriodo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Location = new System.Drawing.Point(9, 0);
            this.painelBotoes.Size = new System.Drawing.Size(1645, 74);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.pnlInformacoesContaPagarReceber);
            this.panelConteudo.Margin = new System.Windows.Forms.Padding(5);
            this.panelConteudo.Size = new System.Drawing.Size(933, 255);
            // 
            // labelControl67
            // 
            this.labelControl67.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl67.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl67.Appearance.Options.UseFont = true;
            this.labelControl67.Appearance.Options.UseForeColor = true;
            this.labelControl67.Location = new System.Drawing.Point(7, 13);
            this.labelControl67.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl67.Name = "labelControl67";
            this.labelControl67.Size = new System.Drawing.Size(103, 17);
            this.labelControl67.TabIndex = 719;
            this.labelControl67.Text = "Cód Funcionário";
            // 
            // txtIdPessoa
            // 
            this.txtIdPessoa.EnterMoveNextControl = true;
            this.txtIdPessoa.Location = new System.Drawing.Point(7, 33);
            this.txtIdPessoa.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdPessoa.Name = "txtIdPessoa";
            this.txtIdPessoa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdPessoa.Properties.Appearance.Options.UseFont = true;
            this.txtIdPessoa.Properties.Appearance.Options.UseTextOptions = true;
            this.txtIdPessoa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtIdPessoa.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtIdPessoa.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtIdPessoa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIdPessoa.Properties.MaxLength = 10;
            this.txtIdPessoa.Size = new System.Drawing.Size(132, 26);
            this.txtIdPessoa.TabIndex = 1;
            this.txtIdPessoa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSomenteNumeros_KeyPress);
            this.txtIdPessoa.Leave += new System.EventHandler(this.txtIdPessoa_Leave);
            // 
            // txtUsuario
            // 
            this.txtUsuario.Enabled = false;
            this.txtUsuario.EnterMoveNextControl = true;
            this.txtUsuario.Location = new System.Drawing.Point(551, 181);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Properties.Appearance.Options.UseFont = true;
            this.txtUsuario.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtUsuario.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtUsuario.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsuario.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtUsuario.Properties.Mask.EditMask = "99/";
            this.txtUsuario.Properties.MaxLength = 50;
            this.txtUsuario.Properties.ReadOnly = true;
            this.txtUsuario.Size = new System.Drawing.Size(363, 26);
            this.txtUsuario.TabIndex = 17;
            this.txtUsuario.TabStop = false;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Location = new System.Drawing.Point(555, 160);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(49, 17);
            this.labelControl5.TabIndex = 731;
            this.labelControl5.Text = "Usuário";
            // 
            // txtNomePessoa
            // 
            this.txtNomePessoa.EnterMoveNextControl = true;
            this.txtNomePessoa.Location = new System.Drawing.Point(177, 33);
            this.txtNomePessoa.Margin = new System.Windows.Forms.Padding(4);
            this.txtNomePessoa.Name = "txtNomePessoa";
            this.txtNomePessoa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomePessoa.Properties.Appearance.Options.UseFont = true;
            this.txtNomePessoa.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNomePessoa.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNomePessoa.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomePessoa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNomePessoa.Properties.Mask.EditMask = "99/";
            this.txtNomePessoa.Properties.ReadOnly = true;
            this.txtNomePessoa.Size = new System.Drawing.Size(737, 26);
            this.txtNomePessoa.TabIndex = 2;
            this.txtNomePessoa.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(177, 13);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(135, 17);
            this.labelControl1.TabIndex = 736;
            this.labelControl1.Text = "Nome do Funcionário";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Appearance.Options.UseForeColor = true;
            this.labelControl10.Location = new System.Drawing.Point(10, 160);
            this.labelControl10.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(67, 17);
            this.labelControl10.TabIndex = 748;
            this.labelControl10.Text = "Nr. Pedido";
            // 
            // txtNumeroPedidoVendas
            // 
            this.txtNumeroPedidoVendas.EnterMoveNextControl = true;
            this.txtNumeroPedidoVendas.Location = new System.Drawing.Point(10, 181);
            this.txtNumeroPedidoVendas.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumeroPedidoVendas.Name = "txtNumeroPedidoVendas";
            this.txtNumeroPedidoVendas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroPedidoVendas.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroPedidoVendas.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNumeroPedidoVendas.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNumeroPedidoVendas.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroPedidoVendas.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroPedidoVendas.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNumeroPedidoVendas.Properties.Mask.EditMask = "([0-9]{1})(([\\.][0-9]{2})(([\\.][0-9]{3})([\\.][0-9]{4})?)?)?";
            this.txtNumeroPedidoVendas.Properties.MaxLength = 50;
            this.txtNumeroPedidoVendas.Properties.ReadOnly = true;
            this.txtNumeroPedidoVendas.Size = new System.Drawing.Size(256, 26);
            this.txtNumeroPedidoVendas.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSalvar);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(964, 64);
            this.flowLayoutPanel1.TabIndex = 1040;
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
            this.btnSalvar.TabIndex = 33;
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
            this.btnSair.TabIndex = 35;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnPesquisaPessoa
            // 
            this.btnPesquisaPessoa.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaPessoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaPessoa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaPessoa.Location = new System.Drawing.Point(147, 36);
            this.btnPesquisaPessoa.Margin = new System.Windows.Forms.Padding(4);
            this.btnPesquisaPessoa.Name = "btnPesquisaPessoa";
            this.btnPesquisaPessoa.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaPessoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaPessoa.TabIndex = 732;
            this.btnPesquisaPessoa.TabStop = false;
            this.btnPesquisaPessoa.Click += new System.EventHandler(this.btnPesquisaPessoa_Click);
            // 
            // pnlInformacoesContaPagarReceber
            // 
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.cboStatus);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.labelControl3);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.txtNumeroAgendaRoteiro);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.labelControl2);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.txtDataAgenda);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.labelControl6);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.cboPeriodo);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.labelControl4);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.labelControl67);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.labelControl1);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.txtNomePessoa);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.txtNumeroPedidoVendas);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.btnPesquisaPessoa);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.labelControl10);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.txtUsuario);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.labelControl5);
            this.pnlInformacoesContaPagarReceber.Controls.Add(this.txtIdPessoa);
            this.pnlInformacoesContaPagarReceber.Location = new System.Drawing.Point(4, 16);
            this.pnlInformacoesContaPagarReceber.Margin = new System.Windows.Forms.Padding(4);
            this.pnlInformacoesContaPagarReceber.Name = "pnlInformacoesContaPagarReceber";
            this.pnlInformacoesContaPagarReceber.Size = new System.Drawing.Size(918, 229);
            this.pnlInformacoesContaPagarReceber.TabIndex = 10050;
            this.pnlInformacoesContaPagarReceber.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlInformacoesContaPagarReceber_Paint);
            // 
            // txtNumeroAgendaRoteiro
            // 
            this.txtNumeroAgendaRoteiro.EnterMoveNextControl = true;
            this.txtNumeroAgendaRoteiro.Location = new System.Drawing.Point(282, 181);
            this.txtNumeroAgendaRoteiro.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumeroAgendaRoteiro.Name = "txtNumeroAgendaRoteiro";
            this.txtNumeroAgendaRoteiro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroAgendaRoteiro.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroAgendaRoteiro.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNumeroAgendaRoteiro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNumeroAgendaRoteiro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroAgendaRoteiro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroAgendaRoteiro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNumeroAgendaRoteiro.Properties.Mask.EditMask = "([0-9]{1})(([\\.][0-9]{2})(([\\.][0-9]{3})([\\.][0-9]{4})?)?)?";
            this.txtNumeroAgendaRoteiro.Properties.MaxLength = 50;
            this.txtNumeroAgendaRoteiro.Properties.ReadOnly = true;
            this.txtNumeroAgendaRoteiro.Size = new System.Drawing.Size(256, 26);
            this.txtNumeroAgendaRoteiro.TabIndex = 10048;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(286, 160);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(122, 17);
            this.labelControl2.TabIndex = 10049;
            this.labelControl2.Text = "Nr. Roteiro/Agenda";
            // 
            // txtDataAgenda
            // 
            this.txtDataAgenda.EditValue = new System.DateTime(2014, 12, 5, 8, 26, 24, 0);
            this.txtDataAgenda.EnterMoveNextControl = true;
            this.txtDataAgenda.Location = new System.Drawing.Point(7, 108);
            this.txtDataAgenda.Margin = new System.Windows.Forms.Padding(4);
            this.txtDataAgenda.Name = "txtDataAgenda";
            this.txtDataAgenda.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataAgenda.Properties.Appearance.Options.UseFont = true;
            this.txtDataAgenda.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataAgenda.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataAgenda.Size = new System.Drawing.Size(259, 26);
            this.txtDataAgenda.TabIndex = 10046;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(7, 88);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(113, 16);
            this.labelControl6.TabIndex = 10047;
            this.labelControl6.Text = "Data da Agenda";
            // 
            // cboPeriodo
            // 
            this.cboPeriodo.EnterMoveNextControl = true;
            this.cboPeriodo.Location = new System.Drawing.Point(275, 108);
            this.cboPeriodo.Margin = new System.Windows.Forms.Padding(4);
            this.cboPeriodo.Name = "cboPeriodo";
            this.cboPeriodo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPeriodo.Properties.Appearance.Options.UseFont = true;
            this.cboPeriodo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPeriodo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboPeriodo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboPeriodo.Properties.DropDownRows = 6;
            this.cboPeriodo.Properties.NullText = "";
            this.cboPeriodo.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboPeriodo.Size = new System.Drawing.Size(263, 26);
            this.cboPeriodo.TabIndex = 10043;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(275, 88);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(55, 16);
            this.labelControl4.TabIndex = 10044;
            this.labelControl4.Text = "Período";
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(551, 108);
            this.cboStatus.Margin = new System.Windows.Forms.Padding(4);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboStatus.Properties.DropDownRows = 6;
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Properties.PopupFormMinSize = new System.Drawing.Size(27, 25);
            this.cboStatus.Size = new System.Drawing.Size(258, 26);
            this.cboStatus.TabIndex = 10050;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(551, 84);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(43, 16);
            this.labelControl3.TabIndex = 10051;
            this.labelControl3.Text = "Status";
            // 
            // FormEditarRoteiro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 390);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "FormEditarRoteiro";
            this.NomeDaTela = "Editar de Roteiro";
            this.Text = "Editar de Roteiro";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtIdPessoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsuario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomePessoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPedidoVendas.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaPessoa)).EndInit();
            this.pnlInformacoesContaPagarReceber.ResumeLayout(false);
            this.pnlInformacoesContaPagarReceber.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroAgendaRoteiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataAgenda.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataAgenda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPeriodo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnSalvar;
        private DevExpress.XtraEditors.LabelControl labelControl67;
        private DevExpress.XtraEditors.TextEdit txtUsuario;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.PictureBox btnPesquisaPessoa;
        private DevExpress.XtraEditors.TextEdit txtNomePessoa;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit txtNumeroPedidoVendas;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel pnlInformacoesContaPagarReceber;
        private System.Windows.Forms.ToolTip hint;
        protected DevExpress.XtraEditors.TextEdit txtIdPessoa;
        private DevExpress.XtraEditors.LookUpEdit cboPeriodo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit txtDataAgenda;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtNumeroAgendaRoteiro;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}
