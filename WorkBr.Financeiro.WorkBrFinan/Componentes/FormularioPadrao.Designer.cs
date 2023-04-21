using System.Windows.Forms;
using Programax.Easy.View.ClassesAuxiliares;
namespace Programax.Easy.View.Componentes
{
    partial class FormularioPadrao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioPadrao));
            this.painelBotoes = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.imgClose = new System.Windows.Forms.PictureBox();
            this.imgMaximize = new System.Windows.Forms.PictureBox();
            this.imgMinimize = new System.Windows.Forms.PictureBox();
            this.lblNomeDaTela = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.painelQueContemPainelConteudo = new Programax.Easy.View.ClassesAuxiliares.ProgramaxPanel();
            this.panelConteudo = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgMaximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgMinimize)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.painelQueContemPainelConteudo.SuspendLayout();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Location = new System.Drawing.Point(13, 0);
            this.painelBotoes.Margin = new System.Windows.Forms.Padding(0);
            this.painelBotoes.Name = "painelBotoes";
            this.painelBotoes.Size = new System.Drawing.Size(1244, 49);
            this.painelBotoes.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.lblNomeDaTela);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(885, 33);
            this.panel1.TabIndex = 3;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.imgClose);
            this.flowLayoutPanel1.Controls.Add(this.imgMaximize);
            this.flowLayoutPanel1.Controls.Add(this.imgMinimize);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(787, 2);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(88, 30);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // imgClose
            // 
            this.imgClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgClose.Image = global::Programax.Easy.View.Properties.Resources.close;
            this.imgClose.Location = new System.Drawing.Point(68, 4);
            this.imgClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.imgClose.Name = "imgClose";
            this.imgClose.Size = new System.Drawing.Size(16, 16);
            this.imgClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgClose.TabIndex = 5;
            this.imgClose.TabStop = false;
            this.imgClose.Click += new System.EventHandler(this.imgClose_Click);
            // 
            // imgMaximize
            // 
            this.imgMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgMaximize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgMaximize.Image = global::Programax.Easy.View.Properties.Resources.minimize;
            this.imgMaximize.Location = new System.Drawing.Point(44, 4);
            this.imgMaximize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.imgMaximize.Name = "imgMaximize";
            this.imgMaximize.Size = new System.Drawing.Size(16, 16);
            this.imgMaximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgMaximize.TabIndex = 6;
            this.imgMaximize.TabStop = false;
            this.imgMaximize.Visible = false;
            this.imgMaximize.Click += new System.EventHandler(this.imgMaximize_Click);
            // 
            // imgMinimize
            // 
            this.imgMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgMinimize.Image = global::Programax.Easy.View.Properties.Resources.minimize;
            this.imgMinimize.Location = new System.Drawing.Point(20, 4);
            this.imgMinimize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.imgMinimize.Name = "imgMinimize";
            this.imgMinimize.Size = new System.Drawing.Size(16, 16);
            this.imgMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgMinimize.TabIndex = 4;
            this.imgMinimize.TabStop = false;
            this.imgMinimize.Click += new System.EventHandler(this.imgMinimize_Click);
            // 
            // lblNomeDaTela
            // 
            this.lblNomeDaTela.AutoSize = true;
            this.lblNomeDaTela.BackColor = System.Drawing.Color.Transparent;
            this.lblNomeDaTela.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeDaTela.ForeColor = System.Drawing.Color.White;
            this.lblNomeDaTela.Location = new System.Drawing.Point(9, 6);
            this.lblNomeDaTela.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNomeDaTela.Name = "lblNomeDaTela";
            this.lblNomeDaTela.Size = new System.Drawing.Size(113, 20);
            this.lblNomeDaTela.TabIndex = 3;
            this.lblNomeDaTela.Text = "Nome da Tela";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.panel3.Location = new System.Drawing.Point(0, 33);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.tableLayoutPanel1.SetRowSpan(this.panel3, 2);
            this.panel3.Size = new System.Drawing.Size(8, 404);
            this.panel3.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 3);
            this.panel2.Location = new System.Drawing.Point(0, 437);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(885, 12);
            this.panel2.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.painelQueContemPainelConteudo, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(885, 449);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.painelBotoes);
            this.panel5.Location = new System.Drawing.Point(17, 385);
            this.panel5.Margin = new System.Windows.Forms.Padding(9, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.panel5.Size = new System.Drawing.Size(857, 49);
            this.panel5.TabIndex = 1;
            // 
            // painelQueContemPainelConteudo
            // 
            this.painelQueContemPainelConteudo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.painelQueContemPainelConteudo.BackColor = System.Drawing.Color.Transparent;
            this.painelQueContemPainelConteudo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(201)))), ((int)(((byte)(203)))));
            this.painelQueContemPainelConteudo.Controls.Add(this.panelConteudo);
            this.painelQueContemPainelConteudo.Location = new System.Drawing.Point(17, 45);
            this.painelQueContemPainelConteudo.Margin = new System.Windows.Forms.Padding(9, 12, 9, 0);
            this.painelQueContemPainelConteudo.Name = "painelQueContemPainelConteudo";
            this.painelQueContemPainelConteudo.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.painelQueContemPainelConteudo.Radius = 15;
            this.painelQueContemPainelConteudo.Size = new System.Drawing.Size(851, 338);
            this.painelQueContemPainelConteudo.TabIndex = 7;
            // 
            // panelConteudo
            // 
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(13, 12);
            this.panelConteudo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Size = new System.Drawing.Size(825, 314);
            this.panelConteudo.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.panel4.Location = new System.Drawing.Point(877, 33);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.tableLayoutPanel1.SetRowSpan(this.panel4, 2);
            this.panel4.Size = new System.Drawing.Size(8, 404);
            this.panel4.TabIndex = 9;
            // 
            // FormularioPadrao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(885, 449);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FormularioPadrao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormularioBase_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgMaximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgMinimize)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.painelQueContemPainelConteudo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel painelBotoes;
        private System.Windows.Forms.PictureBox imgClose;
        private System.Windows.Forms.PictureBox imgMinimize;
        private System.Windows.Forms.Label lblNomeDaTela;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private Panel panel4;
        public Panel panelConteudo;
        public ProgramaxPanel painelQueContemPainelConteudo;
        private Panel panel5;
        private FlowLayoutPanel flowLayoutPanel1;
        private PictureBox imgMaximize;

    }
}
