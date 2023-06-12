using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Infraestrutura.Negocio.Utils;
//using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Report.RelatoriosDevExpress
{
    public partial class RelatorioBaseNfe : DevExpress.XtraReports.UI.XtraReport
    {
        #region " VARIÁVEIS "

        protected string _tituloRelatorio;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioBaseNfe()
        {
            InitializeComponent();
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public void GereRelatorio()
        {
           
            CarregueRodape();

            CarregueDadosRelatorio();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void lblPagina_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            lblPagina.Text = "Página: " + (e.PageIndex + 1) + " de " + e.PageCount;
        }

        #endregion

        #region " CABEÇALHO "

        

       

        #endregion

        #region " DADOS RELATORIO "

        protected virtual void CarregueDadosRelatorio()
        {
            throw new Exception("É necessário sobrescrever o método CarregueDadosRelatorio");
        }

        #endregion

        #region " RODAPÉ "

        private void CarregueRodape()
        {
            lblUsuario.Text = "Usuário: " + Sessao.PessoaLogada.DadosGerais.NomeFantasia;
            lblDataEmissao.Text = "Emitido em: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        #endregion
    }
}
