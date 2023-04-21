namespace Programax.Easy.View.Telas.Fiscal.CartasCorrecoes
{
    partial class FormCadastroCartaCorrecao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCadastroCartaCorrecao));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInformacoesComplementares = new DevExpress.XtraEditors.MemoEdit();
            this.label49 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEnviarCCe = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtInformacoesComplementares.Properties)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel2);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.txtInformacoesComplementares);
            this.panelConteudo.Controls.Add(this.label49);
            this.panelConteudo.Controls.Add(this.label2);
            this.panelConteudo.Controls.Add(this.label1);
            this.panelConteudo.Size = new System.Drawing.Size(873, 333);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(867, 98);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Condições de Uso da Carta de Correção";
            // 
            // txtInformacoesComplementares
            // 
            this.txtInformacoesComplementares.Location = new System.Drawing.Point(6, 167);
            this.txtInformacoesComplementares.Name = "txtInformacoesComplementares";
            this.txtInformacoesComplementares.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInformacoesComplementares.Properties.Appearance.Options.UseFont = true;
            this.txtInformacoesComplementares.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtInformacoesComplementares.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtInformacoesComplementares.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtInformacoesComplementares.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtInformacoesComplementares.Properties.MaxLength = 1000;
            this.txtInformacoesComplementares.Size = new System.Drawing.Size(864, 163);
            this.txtInformacoesComplementares.TabIndex = 1133;
            this.txtInformacoesComplementares.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInformacoesComplementares_KeyPress);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(3, 151);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(245, 13);
            this.label49.TabIndex = 1134;
            this.label49.Text = "Correção ( mínimo 15 e máximo 1.000 caracteres)";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnEnviarCCe);
            this.flowLayoutPanel2.Controls.Add(this.btnSair);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(462, 41);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // btnEnviarCCe
            // 
            this.btnEnviarCCe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviarCCe.FlatAppearance.BorderSize = 0;
            this.btnEnviarCCe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviarCCe.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarCCe.Image = global::Programax.Easy.View.Properties.Resources.iconSalvar;
            this.btnEnviarCCe.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnEnviarCCe.Location = new System.Drawing.Point(0, 0);
            this.btnEnviarCCe.Margin = new System.Windows.Forms.Padding(0);
            this.btnEnviarCCe.Name = "btnEnviarCCe";
            this.btnEnviarCCe.Size = new System.Drawing.Size(130, 40);
            this.btnEnviarCCe.TabIndex = 1001;
            this.btnEnviarCCe.Text = " Enviar CC-e";
            this.btnEnviarCCe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEnviarCCe.UseVisualStyleBackColor = true;
            this.btnEnviarCCe.Click += new System.EventHandler(this.btnEnviarCCe_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(130, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 1002;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // FormCadastroCartaCorrecao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 444);
            this.Name = "FormCadastroCartaCorrecao";
            this.NomeDaTela = "Carta de Correção";
            this.Text = "Carta de Correção";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtInformacoesComplementares.Properties)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.MemoEdit txtInformacoesComplementares;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnEnviarCCe;
        private System.Windows.Forms.Button btnSair;
    }
}