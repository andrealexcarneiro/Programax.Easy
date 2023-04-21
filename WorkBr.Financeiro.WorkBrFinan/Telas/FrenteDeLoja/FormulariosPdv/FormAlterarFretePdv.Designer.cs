namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    partial class FormAlterarFretePdv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlterarFretePdv));
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtValorFrete = new DevExpress.XtraEditors.TextEdit();
            this.txtDataPrevisaoEntrega = new DevExpress.XtraEditors.DateEdit();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.cboTipoFrete = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboTransportadoras = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtValorFrete.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataPrevisaoEntrega.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataPrevisaoEntrega.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoFrete.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransportadoras.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(279, 169);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(79, 20);
            this.labelControl3.TabIndex = 10122;
            this.labelControl3.Text = "Valor Frete";
            // 
            // txtValorFrete
            // 
            this.txtValorFrete.Enabled = false;
            this.txtValorFrete.Location = new System.Drawing.Point(279, 195);
            this.txtValorFrete.Name = "txtValorFrete";
            this.txtValorFrete.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorFrete.Properties.Appearance.Options.UseFont = true;
            this.txtValorFrete.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtValorFrete.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtValorFrete.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtValorFrete.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtValorFrete.Properties.Mask.EditMask = "[0-9]{1,11}([\\.\\,][0-9]{0,2})?";
            this.txtValorFrete.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtValorFrete.Size = new System.Drawing.Size(265, 32);
            this.txtValorFrete.TabIndex = 4;
            this.txtValorFrete.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValorFrete_KeyDown);
            // 
            // txtDataPrevisaoEntrega
            // 
            this.txtDataPrevisaoEntrega.EditValue = "";
            this.txtDataPrevisaoEntrega.EnterMoveNextControl = true;
            this.txtDataPrevisaoEntrega.Location = new System.Drawing.Point(8, 195);
            this.txtDataPrevisaoEntrega.Name = "txtDataPrevisaoEntrega";
            this.txtDataPrevisaoEntrega.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataPrevisaoEntrega.Properties.Appearance.Options.UseFont = true;
            this.txtDataPrevisaoEntrega.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataPrevisaoEntrega.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataPrevisaoEntrega.Size = new System.Drawing.Size(265, 32);
            this.txtDataPrevisaoEntrega.TabIndex = 3;
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Location = new System.Drawing.Point(8, 169);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(135, 20);
            this.labDataCadastro.TabIndex = 10120;
            this.labDataCadastro.Text = "Data Prev. Entrega";
            // 
            // cboTipoFrete
            // 
            this.cboTipoFrete.EnterMoveNextControl = true;
            this.cboTipoFrete.Location = new System.Drawing.Point(279, 99);
            this.cboTipoFrete.Name = "cboTipoFrete";
            this.cboTipoFrete.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoFrete.Properties.Appearance.Options.UseFont = true;
            this.cboTipoFrete.Properties.AutoHeight = false;
            this.cboTipoFrete.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTipoFrete.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboTipoFrete.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboTipoFrete.Properties.NullText = "";
            this.cboTipoFrete.Size = new System.Drawing.Size(265, 32);
            this.cboTipoFrete.TabIndex = 2;
            this.cboTipoFrete.EditValueChanged += new System.EventHandler(this.cboTipoFrete_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(279, 73);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 20);
            this.labelControl2.TabIndex = 10118;
            this.labelControl2.Text = "Tipo Frete";
            // 
            // cboTransportadoras
            // 
            this.cboTransportadoras.EnterMoveNextControl = true;
            this.cboTransportadoras.Location = new System.Drawing.Point(8, 99);
            this.cboTransportadoras.Name = "cboTransportadoras";
            this.cboTransportadoras.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTransportadoras.Properties.Appearance.Options.UseFont = true;
            this.cboTransportadoras.Properties.AutoHeight = false;
            this.cboTransportadoras.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTransportadoras.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboTransportadoras.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboTransportadoras.Properties.NullText = "";
            this.cboTransportadoras.Size = new System.Drawing.Size(265, 32);
            this.cboTransportadoras.TabIndex = 1;
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl19.Location = new System.Drawing.Point(8, 73);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(109, 20);
            this.labelControl19.TabIndex = 10117;
            this.labelControl19.Text = "Transportadora";
            // 
            // panel15
            // 
            this.panel15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(209)))), ((int)(((byte)(211)))));
            this.panel15.Location = new System.Drawing.Point(0, 309);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(772, 1);
            this.panel15.TabIndex = 10114;
            // 
            // panel12
            // 
            this.panel12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(209)))), ((int)(((byte)(211)))));
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(1, 389);
            this.panel12.TabIndex = 10113;
            // 
            // panel14
            // 
            this.panel14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(209)))), ((int)(((byte)(211)))));
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(772, 1);
            this.panel14.TabIndex = 10112;
            // 
            // panel13
            // 
            this.panel13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(209)))), ((int)(((byte)(211)))));
            this.panel13.Location = new System.Drawing.Point(554, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(1, 358);
            this.panel13.TabIndex = 10111;
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizar.FlatAppearance.BorderSize = 0;
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizar.Image = global::Programax.Easy.View.Properties.Resources.icone_selecionar1;
            this.btnAtualizar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAtualizar.Location = new System.Drawing.Point(17, 256);
            this.btnAtualizar.Margin = new System.Windows.Forms.Padding(0);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(171, 40);
            this.btnAtualizar.TabIndex = 10110;
            this.btnAtualizar.TabStop = false;
            this.btnAtualizar.Text = " Concluir (F3)";
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
            this.btnSair.Location = new System.Drawing.Point(426, 256);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(118, 40);
            this.btnSair.TabIndex = 10109;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair (Esc)";
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
            this.pictureBox2.Location = new System.Drawing.Point(-91, 243);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(767, 10);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10108;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-72, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(748, 10);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10107;
            this.pictureBox1.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.labelControl1.Location = new System.Drawing.Point(17, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(57, 29);
            this.labelControl1.TabIndex = 10106;
            this.labelControl1.Text = "Frete";
            // 
            // FormAlterarFretePdv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(555, 310);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtValorFrete);
            this.Controls.Add(this.txtDataPrevisaoEntrega);
            this.Controls.Add(this.labDataCadastro);
            this.Controls.Add(this.cboTipoFrete);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cboTransportadoras);
            this.Controls.Add(this.labelControl19);
            this.Controls.Add(this.panel15);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel14);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FormAlterarFretePdv";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAlterarFretePdv";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormAlterarFretePdv_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.txtValorFrete.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataPrevisaoEntrega.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataPrevisaoEntrega.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoFrete.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTransportadoras.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtValorFrete;
        private DevExpress.XtraEditors.DateEdit txtDataPrevisaoEntrega;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.LookUpEdit cboTipoFrete;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit cboTransportadoras;
        private DevExpress.XtraEditors.LabelControl labelControl19;
    }
}