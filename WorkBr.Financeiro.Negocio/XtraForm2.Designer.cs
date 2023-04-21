namespace WorkBr.Financeiro.WorkBrFinanc.Negocio
{
    partial class XtraForm2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraForm2));
            this.backstageViewControl1 = new DevExpress.XtraBars.Ribbon.BackstageViewControl();
            this.backstageViewClientControl1 = new DevExpress.XtraBars.Ribbon.BackstageViewClientControl();
            this.backstageViewTabItem1 = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
            this.labTelefone = new DevExpress.XtraEditors.LabelControl();
            this.txtTelefone = new DevExpress.XtraEditors.TextEdit();
            this.labObservacao = new DevExpress.XtraEditors.LabelControl();
            this.txtObservacao = new DevExpress.XtraEditors.TextEdit();
            this.txtId = new DevExpress.XtraEditors.TextEdit();
            this.pbPesquisaPessoa = new System.Windows.Forms.PictureBox();
            this.txtRazao = new DevExpress.XtraEditors.TextEdit();
            this.txtCodPessoa = new DevExpress.XtraEditors.TextEdit();
            this.labCodigo = new DevExpress.XtraEditors.LabelControl();
            this.cboTipo = new DevExpress.XtraEditors.LookUpEdit();
            this.labId = new DevExpress.XtraEditors.LabelControl();
            this.labTipo = new DevExpress.XtraEditors.LabelControl();
            this.backstageViewControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelefone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRazao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodPessoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // backstageViewControl1
            // 
            this.backstageViewControl1.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.Yellow;
            this.backstageViewControl1.Controls.Add(this.backstageViewClientControl1);
            this.backstageViewControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.backstageViewControl1.Items.Add(this.backstageViewTabItem1);
            this.backstageViewControl1.Location = new System.Drawing.Point(0, 93);
            this.backstageViewControl1.Name = "backstageViewControl1";
            this.backstageViewControl1.SelectedTab = this.backstageViewTabItem1;
            this.backstageViewControl1.SelectedTabIndex = 0;
            this.backstageViewControl1.Size = new System.Drawing.Size(706, 253);
            this.backstageViewControl1.TabIndex = 0;
            this.backstageViewControl1.Text = "backstageViewControl1";
            // 
            // backstageViewClientControl1
            // 
            this.backstageViewClientControl1.Name = "backstageViewClientControl1";
            this.backstageViewClientControl1.TabIndex = 0;
            // 
            // backstageViewTabItem1
            // 
            this.backstageViewTabItem1.Caption = "backstageViewTabItem1";
            this.backstageViewTabItem1.ContentControl = this.backstageViewClientControl1;
            this.backstageViewTabItem1.Name = "backstageViewTabItem1";
            this.backstageViewTabItem1.Selected = true;
            // 
            // labTelefone
            // 
            this.labTelefone.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTelefone.Location = new System.Drawing.Point(523, 10);
            this.labTelefone.Name = "labTelefone";
            this.labTelefone.Size = new System.Drawing.Size(42, 13);
            this.labTelefone.TabIndex = 103;
            this.labTelefone.Text = "Telefone";
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(523, 29);
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTelefone.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtTelefone.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtTelefone.Properties.Mask.EditMask = "99/";
            this.txtTelefone.Size = new System.Drawing.Size(98, 20);
            this.txtTelefone.TabIndex = 102;
            // 
            // labObservacao
            // 
            this.labObservacao.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labObservacao.Location = new System.Drawing.Point(13, 60);
            this.labObservacao.Name = "labObservacao";
            this.labObservacao.Size = new System.Drawing.Size(65, 13);
            this.labObservacao.TabIndex = 101;
            this.labObservacao.Text = "Observação :";
            // 
            // txtObservacao
            // 
            this.txtObservacao.Location = new System.Drawing.Point(85, 57);
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtObservacao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtObservacao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtObservacao.Properties.Mask.EditMask = "99/";
            this.txtObservacao.Size = new System.Drawing.Size(536, 20);
            this.txtObservacao.TabIndex = 95;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(13, 30);
            this.txtId.Name = "txtId";
            this.txtId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtId.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtId.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtId.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtId.Properties.Mask.EditMask = "99/";
            this.txtId.Size = new System.Drawing.Size(40, 20);
            this.txtId.TabIndex = 92;
            // 
            // pbPesquisaPessoa
            // 
            this.pbPesquisaPessoa.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaPessoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaPessoa.Image = ((System.Drawing.Image)(resources.GetObject("pbPesquisaPessoa.Image")));
            this.pbPesquisaPessoa.Location = new System.Drawing.Point(108, 30);
            this.pbPesquisaPessoa.Name = "pbPesquisaPessoa";
            this.pbPesquisaPessoa.Size = new System.Drawing.Size(25, 20);
            this.pbPesquisaPessoa.TabIndex = 100;
            this.pbPesquisaPessoa.TabStop = false;
            // 
            // txtRazao
            // 
            this.txtRazao.Location = new System.Drawing.Point(139, 29);
            this.txtRazao.Name = "txtRazao";
            this.txtRazao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRazao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtRazao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtRazao.Properties.Mask.EditMask = "99/";
            this.txtRazao.Size = new System.Drawing.Size(283, 20);
            this.txtRazao.TabIndex = 99;
            // 
            // txtCodPessoa
            // 
            this.txtCodPessoa.EditValue = "";
            this.txtCodPessoa.Location = new System.Drawing.Point(59, 30);
            this.txtCodPessoa.Name = "txtCodPessoa";
            this.txtCodPessoa.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCodPessoa.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtCodPessoa.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCodPessoa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtCodPessoa.Properties.Mask.EditMask = "99/";
            this.txtCodPessoa.Size = new System.Drawing.Size(45, 20);
            this.txtCodPessoa.TabIndex = 93;
            // 
            // labCodigo
            // 
            this.labCodigo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCodigo.Location = new System.Drawing.Point(59, 12);
            this.labCodigo.Name = "labCodigo";
            this.labCodigo.Size = new System.Drawing.Size(100, 13);
            this.labCodigo.TabIndex = 98;
            this.labCodigo.Text = "Cadastro de Pessoas";
            // 
            // cboTipo
            // 
            this.cboTipo.Location = new System.Drawing.Point(428, 29);
            this.cboTipo.Name = "cboTipo";
            this.cboTipo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTipo.Properties.NullText = "";
            this.cboTipo.Size = new System.Drawing.Size(89, 20);
            this.cboTipo.TabIndex = 94;
            // 
            // labId
            // 
            this.labId.Appearance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labId.Location = new System.Drawing.Point(13, 12);
            this.labId.Name = "labId";
            this.labId.Size = new System.Drawing.Size(12, 13);
            this.labId.TabIndex = 96;
            this.labId.Text = "Id";
            // 
            // labTipo
            // 
            this.labTipo.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTipo.Location = new System.Drawing.Point(428, 12);
            this.labTipo.Name = "labTipo";
            this.labTipo.Size = new System.Drawing.Size(20, 13);
            this.labTipo.TabIndex = 97;
            this.labTipo.Text = "Tipo";
            // 
            // XtraForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 346);
            this.Controls.Add(this.labTelefone);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.labObservacao);
            this.Controls.Add(this.txtObservacao);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.pbPesquisaPessoa);
            this.Controls.Add(this.txtRazao);
            this.Controls.Add(this.txtCodPessoa);
            this.Controls.Add(this.labCodigo);
            this.Controls.Add(this.cboTipo);
            this.Controls.Add(this.labId);
            this.Controls.Add(this.labTipo);
            this.Controls.Add(this.backstageViewControl1);
            this.Name = "XtraForm2";
            this.Text = "XtraForm1";
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            this.backstageViewControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTelefone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRazao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodPessoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.BackstageViewControl backstageViewControl1;
        private DevExpress.XtraEditors.LabelControl labTelefone;
        private DevExpress.XtraEditors.TextEdit txtTelefone;
        private DevExpress.XtraEditors.LabelControl labObservacao;
        private DevExpress.XtraEditors.TextEdit txtObservacao;
        private DevExpress.XtraEditors.TextEdit txtId;
        private System.Windows.Forms.PictureBox pbPesquisaPessoa;
        private DevExpress.XtraEditors.TextEdit txtRazao;
        private DevExpress.XtraEditors.TextEdit txtCodPessoa;
        private DevExpress.XtraEditors.LabelControl labCodigo;
        private DevExpress.XtraEditors.LookUpEdit cboTipo;
        private DevExpress.XtraEditors.LabelControl labId;
        private DevExpress.XtraEditors.LabelControl labTipo;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl1;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem1;
    }
}