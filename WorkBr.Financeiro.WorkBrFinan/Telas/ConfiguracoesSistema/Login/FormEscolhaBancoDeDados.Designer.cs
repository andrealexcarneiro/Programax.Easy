namespace Programax.Easy.View.Telas.ConfiguracoesSistema.Login
{
    partial class FormEscolhaBancoDeDados
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
            this.cboEmpresa = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmpresa.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.btnSelecionar);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.labelControl8);
            this.panelConteudo.Size = new System.Drawing.Size(445, 155);
            // 
            // cboEmpresa
            // 
            this.cboEmpresa.EnterMoveNextControl = true;
            this.cboEmpresa.Location = new System.Drawing.Point(26, 116);
            this.cboEmpresa.Name = "cboEmpresa";
            this.cboEmpresa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEmpresa.Properties.Appearance.Options.UseFont = true;
            this.cboEmpresa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEmpresa.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboEmpresa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao")});
            this.cboEmpresa.Properties.DropDownRows = 10;
            this.cboEmpresa.Properties.NullText = "";
            this.cboEmpresa.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboEmpresa.Size = new System.Drawing.Size(439, 30);
            this.cboEmpresa.TabIndex = 10047;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Location = new System.Drawing.Point(3, 39);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(193, 24);
            this.labelControl8.TabIndex = 10048;
            this.labelControl8.Text = "Escolha Uma Empresa";
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelecionar.FlatAppearance.BorderSize = 0;
            this.btnSelecionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionar.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionar.Image = global::Programax.Easy.View.Properties.Resources.icone_selecionar1;
            this.btnSelecionar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSelecionar.Location = new System.Drawing.Point(3, 0);
            this.btnSelecionar.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(120, 40);
            this.btnSelecionar.TabIndex = 1004;
            this.btnSelecionar.Text = " Selecionar";
            this.btnSelecionar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // FormEscolhaBancoDeDados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 266);
            this.Controls.Add(this.cboEmpresa);
            this.Name = "FormEscolhaBancoDeDados";
            this.NomeDaTela = "Seleção de Empresas";
            this.Text = "Seleção de Empresas";
            this.Load += new System.EventHandler(this.FormEscolhaBancoDeDados_Load);
            this.Controls.SetChildIndex(this.cboEmpresa, 0);
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmpresa.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit cboEmpresa;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private System.Windows.Forms.Button btnSelecionar;
    }
}