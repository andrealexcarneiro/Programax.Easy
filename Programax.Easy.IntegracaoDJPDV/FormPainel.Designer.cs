namespace Programax.Easy.IntegracaoDJPDV
{
    partial class FormPainel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPainel));
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIconIntegrador = new System.Windows.Forms.NotifyIcon();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExportarTodosDados = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.ltbLog = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ltbPreVendas = new System.Windows.Forms.ListBox();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.tabControl1);
            this.panelConteudo.Controls.Add(this.label1);
            this.panelConteudo.Size = new System.Drawing.Size(721, 347);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "AKIL INTEGRADOR";
            // 
            // notifyIconIntegrador
            // 
            this.notifyIconIntegrador.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconIntegrador.Icon")));
            this.notifyIconIntegrador.Text = "Akill Integrador";
            this.notifyIconIntegrador.Visible = true;
            this.notifyIconIntegrador.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconIntegrador_MouseDoubleClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnExportarTodosDados);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(360, 41);
            this.flowLayoutPanel1.TabIndex = 10040;
            // 
            // btnExportarTodosDados
            // 
            this.btnExportarTodosDados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarTodosDados.FlatAppearance.BorderSize = 0;
            this.btnExportarTodosDados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarTodosDados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarTodosDados.Image = global::Programax.Easy.IntegracaoDJPDV.Properties.Resources.icone_importar;
            this.btnExportarTodosDados.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExportarTodosDados.Location = new System.Drawing.Point(0, 0);
            this.btnExportarTodosDados.Margin = new System.Windows.Forms.Padding(0);
            this.btnExportarTodosDados.Name = "btnExportarTodosDados";
            this.btnExportarTodosDados.Size = new System.Drawing.Size(157, 40);
            this.btnExportarTodosDados.TabIndex = 10037;
            this.btnExportarTodosDados.Text = "Exportar \r\n Todos os Dados";
            this.btnExportarTodosDados.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportarTodosDados.UseVisualStyleBackColor = true;
            this.btnExportarTodosDados.Click += new System.EventHandler(this.btnExportarTodosDados_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.IntegracaoDJPDV.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(157, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 10036;
            this.btnSair.Text = " Sair";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // ltbLog
            // 
            this.ltbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ltbLog.FormattingEnabled = true;
            this.ltbLog.Location = new System.Drawing.Point(3, 3);
            this.ltbLog.Name = "ltbLog";
            this.ltbLog.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ltbLog.Size = new System.Drawing.Size(705, 248);
            this.ltbLog.TabIndex = 2;
            this.ltbLog.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ltbLog_KeyDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControl1.Location = new System.Drawing.Point(2, 64);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(719, 283);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ltbLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(711, 254);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cadastros";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ltbPreVendas);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(711, 254);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Pré-Vendas";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ltbPreVendas
            // 
            this.ltbPreVendas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ltbPreVendas.FormattingEnabled = true;
            this.ltbPreVendas.Location = new System.Drawing.Point(3, 3);
            this.ltbPreVendas.Name = "ltbPreVendas";
            this.ltbPreVendas.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ltbPreVendas.Size = new System.Drawing.Size(705, 248);
            this.ltbPreVendas.TabIndex = 3;
            // 
            // FormPainel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 458);
            this.Name = "FormPainel";
            this.NomeDaTela = "Akill Integrador";
            this.Text = "Akill Integrador";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPainel_FormClosed);
            this.Load += new System.EventHandler(this.FormPainel_Load);
            this.Resize += new System.EventHandler(this.FormPainel_Resize);
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIconIntegrador;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnExportarTodosDados;
        private System.Windows.Forms.ListBox ltbLog;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox ltbPreVendas;
    }
}