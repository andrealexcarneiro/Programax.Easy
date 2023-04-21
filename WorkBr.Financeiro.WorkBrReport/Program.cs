using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Programax.Easy.Report
{
   public  static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);

            Application.EnableVisualStyles();
        }
       public static void Acessar(string pUnlock)
       {
           if (pUnlock != "BRT9809769_mmmnz45")
           {
               MessageBox.Show(null , "Módulo não autorizado. Entre em contato com a Programax para adquirir o produto.", "Atenção",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                
           }
           else
           {

               //frmReport _frmReportPrincipal = new frmReport();
               //_frmReportPrincipal.ShowDialog();
           }

       }
    }
}
