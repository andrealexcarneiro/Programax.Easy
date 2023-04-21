using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace BoletoNet.Arquivo
{
    public partial class FormVisualizarImagem : Form
    {   

        public FormVisualizarImagem(string fileName)
        {
            InitializeComponent();

            pictureBox1.Image = Image.FromFile(fileName);
            
        }

        void FormVisualizarImagem_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            pictureBox1.Image.Dispose();

            string file = Path.Combine(Environment.CurrentDirectory, "boleto.bmp");

            StreamWriter sw = new StreamWriter(file, true);

            sw.Close();
        }
    }
}
