using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Report.RelatoriosDevExpress.Cadastros;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Servico.Cadastros.PessoaServ;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormRelatorioCondicaoPagamento : FormularioBase
    {
        #region " VARIÁVEIS PRIVADA "
                

        #endregion

        #region " CONSTRUTOR "

        public FormRelatorioCondicaoPagamento()
        {
            InitializeComponent();
            
            PreenchaPrimeiroEUltimoDiaMes();                     
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DateTime dataInicial = txtDataInicialPeriodo.Text.ToDate();
            DateTime dataFinal = txtDataFinalPeriodo.Text.ToDate();
            
            RelatorioCondicaoPagamento relatorioCondicaoPagamento = new RelatorioCondicaoPagamento(dataInicial, dataFinal);

            TratamentosDeTela.ExibirRelatorio(relatorioCondicaoPagamento);

            this.Cursor = Cursors.Default;
        }
        
        #endregion

        #region " MÉTODOS AUXILIARES "
       
        private void PreenchaPrimeiroEUltimoDiaMes()
        {
            var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

            txtDataInicialPeriodo.DateTime = primeiroDiaMes;
            txtDataFinalPeriodo.DateTime = ultimoDiaMes;
        }
        
        #endregion
               
    }
}
