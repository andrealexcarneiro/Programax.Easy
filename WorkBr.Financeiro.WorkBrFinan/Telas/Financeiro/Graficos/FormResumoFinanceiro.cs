using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Infraestrutura.Negocio.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programax.Easy.View.Telas.Financeiro.Graficos
{
    public partial class FormResumoFinanceiro : Form
    {
    #region "Variáveis Globais"

        bool _estah3D = false;
        bool _estahPizza = false;
        string _anoCorrente = DateTime.Now.Year.ToString();
        string _MesCorrente;

        #endregion

     #region "Inicialização do formulário"

        public FormResumoFinanceiro()
        {
            InitializeComponent();
            CalculeRecebimentoDoDia();
            CalculePagamentoDoDia();
            CalculeRecebimentoEmAtraso();
            CalculePagamentoEmAtraso();
            CalculeSaldoAtual();
            BuscarValoresAReceberAnualGrafico();
            BuscarValoresAPagarAnualGrafico();

            chtResumoFinanceiro.Titles.Clear();
            chtResumoPizza.Titles.Clear();
            chtResumoFinanceiro.Titles.Add("Anual - "+_anoCorrente);
            chtResumoPizza.Titles.Add("Anual - "+_anoCorrente);
            _MesCorrente = RetorneMesAtual();
        }

        #endregion

    #region "Métodos gerais"

        private string RetorneMesAtual()
        {
            switch (DateTime.Now.Month)
            {
                case 1: return "Janeiro";
                case 2: return "Fevereiro";
                case 3: return "Março";
                case 4: return "Abril";
                case 5: return "Maio";
                case 6: return "Junho";
                case 7: return "Julho";
                case 8: return "Agosto";
                case 9: return "Setembro";
                case 10: return "Outubro";
                case 11: return "Novembro";
                case 12: return "Dezembro";
            }

            return "";
        }

        private void CalculeRecebimentoDoDia()
        {
            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var TotalHoje = servicoContasPagarReceber.ConsuteTotalARebecerHoje();

            lblTotalAReceberHoje.Text = TotalHoje.TotalAReceber != null ? TotalHoje.TotalAReceber.ToString("C") : string.Format("0.00");
        }

        private void CalculePagamentoDoDia()
        {
            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var TotalHoje = servicoContasPagarReceber.ConsuteTotalAPagarHoje();

            lblPagarHoje.Text = TotalHoje.TotalAPagar != null ? TotalHoje.TotalAPagar.ToString("C") : string.Format("0.00");
        }

        private void CalculeRecebimentoEmAtraso()
        {
            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var TotalEmAtraso = servicoContasPagarReceber.ConsuteTotalARebecerEmAtraso();

            lblRecebimentosEmAtraso.Text = TotalEmAtraso.TotalAReceberEmAtraso != null ? TotalEmAtraso.TotalAReceberEmAtraso.ToString("C") : string.Format("0.00");
        }

        private void CalculePagamentoEmAtraso()
        {
            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var TotalEmAtraso = servicoContasPagarReceber.ConsuteTotalAPagarEmAtraso();

            lblPagamentosEmAtraso.Text = TotalEmAtraso.TotalAPagarEmAtraso != null ? TotalEmAtraso.TotalAPagarEmAtraso.ToString("C") : string.Format("0.00");
        }

        private void CalculeSaldoAtual()
        {
            ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();

            var SaldoAtual = servicoMovimentacaoCaixa.ConsulteSaldoAtualCaixa();

            if (SaldoAtual == null)
            {
                lblSaldoCaixaAtual.Text = string.Format("0.00");
                return;
            }

            lblSaldoCaixaAtual.Text = SaldoAtual.SaldoAtualDinheiro != 0 ? SaldoAtual.SaldoAtualDinheiro.ToString("C") : string.Format("0.00");
        }

        private void BuscarValoresAReceberAnualGrafico()
        {
            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var GraficoAnualReceber = servicoContasPagarReceber.ConsulteTotalAReceberAnual();

            if (GraficoAnualReceber == null) return;

            chtResumoFinanceiro.DataSource = GraficoAnualReceber;
            chtResumoFinanceiro.Series["A Receber"].XValueMember = "MESVENCIMENTO";
            chtResumoFinanceiro.Series["A Receber"].YValueMembers = "VALORPARCELARECEBER";
            chtResumoFinanceiro.DataBind();
            chtResumoFinanceiro.Visible = true;
        }

        private void BuscarValoresAPagarAnualGrafico()
        {
            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var GraficoAnualPagar = servicoContasPagarReceber.ConsulteTotalAPagarAnual();

            if (GraficoAnualPagar == null) return;
            
            if (GraficoAnualPagar.Count == 0)
            {
                chtResumoFinanceiro.Series["A Pagar"].Points.AddXY(0,0);
            }
            
            foreach (var item in GraficoAnualPagar)
            {
                chtResumoFinanceiro.Series["A Pagar"].Points.AddXY(item.MesVencimento,item.ValorParcelaPagar);       
            }

            chtResumoFinanceiro.DataBind();
            chtResumoFinanceiro.Visible = true;
        }

        private void BuscarValoresAReceberMensalGrafico()
        {
            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var GraficoMensalReceber = servicoContasPagarReceber.ConsulteTotalAReceberMensal();

            if (GraficoMensalReceber == null) return;

            chtResumoFinanceiro.DataSource = GraficoMensalReceber;
            chtResumoFinanceiro.Series["A Receber"].XValueMember = "DIA";
            chtResumoFinanceiro.Series["A Receber"].YValueMembers = "VALORPARCELARECEBER";
            chtResumoFinanceiro.DataBind();
            chtResumoFinanceiro.Visible = true;
        }

        private void BuscarValoresAPagarMensalGrafico()
        {
            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var GraficoMensalPagar = servicoContasPagarReceber.ConsulteTotalAPagarMensal();

            if (GraficoMensalPagar == null) return;

            if(GraficoMensalPagar.Count==0)
            {
                chtResumoFinanceiro.Series["A Pagar"].Points.AddXY(0,0);
            }

            foreach (var item in GraficoMensalPagar)
            {
                chtResumoFinanceiro.Series["A Pagar"].Points.AddXY(item.Dia, item.ValorParcelaPagar);
            }

            chtResumoFinanceiro.DataBind();
            chtResumoFinanceiro.Visible = true;
        }

        private void BuscarValoresAPagar_Receber_Anual_GraficoPizza()
        {
            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var ValoresAnualReceber = servicoContasPagarReceber.ConsulteTotalAReceberAnual();

            var somaReceber = ValoresAnualReceber.Sum(x => x.ValorParcelaReceber);

            var ValoresAnualPagar = servicoContasPagarReceber.ConsulteTotalAPagarAnual();

            var somaPagar = ValoresAnualPagar.Sum(x => x.ValorParcelaPagar);

            var PercentualReceber = Math.Round((somaReceber.ToDouble() / (somaReceber.ToDouble() + somaPagar.ToDouble())) * (double)100, 2);

            var PercentualPagar = Math.Round((somaPagar.ToDouble() / (somaReceber.ToDouble() + somaPagar.ToDouble())) * (double)100, 2);

            chtResumoPizza.Series["PagarReceber"].Points.AddXY("A Receber " + PercentualReceber + "%", somaReceber);
            chtResumoPizza.Series["PagarReceber"].Points.AddXY("A Pagar " + PercentualPagar + "%", somaPagar);

            chtResumoPizza.DataBind();
        }

        private void BuscarValoresA_Pagar_Receber_Mensal_GraficoPizza()
        {
            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var ValoresMensalReceber = servicoContasPagarReceber.ConsulteTotalAReceberMensal();

            var somaReceber = ValoresMensalReceber.Sum(x => x.ValorParcelaReceber);

            var ValoresMensalPagar = servicoContasPagarReceber.ConsulteTotalAPagarMensal();

            var somaPagar = ValoresMensalPagar.Sum(x => x.ValorParcelaPagar);

            var PercentualReceber = Math.Round((somaReceber.ToDouble() / (somaReceber.ToDouble() + somaPagar.ToDouble())) * (double)100, 2);

            var PercentualPagar = Math.Round((somaPagar.ToDouble() / (somaReceber.ToDouble() + somaPagar.ToDouble())) * (double)100, 2);

            chtResumoPizza.Series["PagarReceber"].Points.AddXY("A Receber " + PercentualReceber + "%", somaReceber);
            chtResumoPizza.Series["PagarReceber"].Points.AddXY("A Pagar " + PercentualPagar + "%", somaPagar);

            chtResumoPizza.DataBind();
        }

        private void BuscarValoresA_Pagar_Receber_Semanal_GraficoPizza()
        {
            var DiaDaSemana = DateTime.Now.DayOfWeek;

            var DataFinal = RetorneDataFinal(DiaDaSemana);
            var DataInicial = RetorneDataInicial(DiaDaSemana);

            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var ValoresMensalReceber = servicoContasPagarReceber.ConsulteTotalAReceberSemanal(DataInicial.Date, DataFinal.Date);

            var somaReceber = ValoresMensalReceber.Sum(x => x.ValorParcelaReceber);

            var ValoresMensalPagar = servicoContasPagarReceber.ConsulteTotalAPagarSemanal(DataInicial.Date, DataFinal.Date);

            var somaPagar = ValoresMensalPagar.Sum(x => x.ValorParcelaPagar);

            var PercentualReceber = Math.Round((somaReceber.ToDouble() / (somaReceber.ToDouble() + somaPagar.ToDouble())) * (double)100, 2);

            var PercentualPagar = Math.Round((somaPagar.ToDouble() / (somaReceber.ToDouble() + somaPagar.ToDouble())) * (double)100, 2);

            if(ValoresMensalReceber.Count == 0)
            {
                chtResumoPizza.Series["PagarReceber"].Points.AddXY("A Receber " + 0 + "%", 0);
            }
            else
            {
                chtResumoPizza.Series["PagarReceber"].Points.AddXY("A Receber " + PercentualReceber + "%", somaReceber);
            }

            if(ValoresMensalPagar.Count ==0)
            {
                chtResumoPizza.Series["PagarReceber"].Points.AddXY("A Pagar " + 0 + "%", 0);
            }
            else
            {
                chtResumoPizza.Series["PagarReceber"].Points.AddXY("A Pagar " + PercentualPagar + "%", somaPagar);
            }
            
            chtResumoPizza.DataBind();
        }

        private void LimparPontosColunas()
        {
            chtResumoFinanceiro.DataSource = null;

            foreach (var series in chtResumoFinanceiro.Series)
            {
                series.Points.Clear();
                series.Points.Dispose();
            }
        }

        private void LimparPontosPizza()
        {
            foreach (var series in chtResumoPizza.Series)
            {
                series.Points.Clear();
            }
        }

        private void ConverterPara3D()
        {
            if (!_estah3D)
            {
                if (_estahPizza)
                    chtResumoPizza.ChartAreas[0].Area3DStyle.Enable3D = true;
                else
                    chtResumoFinanceiro.ChartAreas[0].Area3DStyle.Enable3D = true;

                _estah3D = true;
            }
            else
            {
                if (_estahPizza)
                    chtResumoPizza.ChartAreas[0].Area3DStyle.Enable3D = false;
                else
                    chtResumoFinanceiro.ChartAreas[0].Area3DStyle.Enable3D = false;

                _estah3D = false;
            }
        }

        private void BuscarValoresA_Receber_Semanal()
        {
            var DiaDaSemana = DateTime.Now.DayOfWeek;

            var DataFinal = RetorneDataFinal(DiaDaSemana);
            var DataInicial = RetorneDataInicial(DiaDaSemana);

            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var ValoresSemanalReceber = servicoContasPagarReceber.ConsulteTotalAReceberSemanal(DataInicial.Date, DataFinal.Date);

            var ValorAgrupado = ValoresSemanalReceber.GroupBy(x => x.Vencimento).ToList();

            foreach (var item in ValorAgrupado)
            {
                var diaDaSemanaReceber = RetorneDescricaoDiaSemana(item.Key.DayOfWeek);

                var somaTotalReceber = item.Sum(x => x.ValorParcelaReceber);

                chtResumoFinanceiro.Series["A Receber"].Points.AddXY(diaDaSemanaReceber, somaTotalReceber);
            }

            if(ValorAgrupado.Count == 0)
            {
                chtResumoFinanceiro.Series["A Receber"].Points.AddXY(0, 0);
            }

            chtResumoFinanceiro.DataBind();
            chtResumoFinanceiro.Visible = true;
        }

        private string RetorneDescricaoDiaSemana(DayOfWeek DiaDaSemana)
        {
            switch (DiaDaSemana)
            {
                case DayOfWeek.Monday:
                    return "Seg";
                case DayOfWeek.Tuesday:
                    return "Ter";
                case DayOfWeek.Wednesday:
                    return "Qua";
                case DayOfWeek.Thursday:
                    return "Qui";
                case DayOfWeek.Friday:
                    return "Sex";
                case DayOfWeek.Saturday:
                    return "Sab";
                case DayOfWeek.Sunday:
                    return "Dom";
                default:
                    break;
            }

            return "";
        }

        private void BuscarValoresA_Pagar_Semanal()
        {
            var DiaDaSemana = DateTime.Now.DayOfWeek;

            var DataFinal = RetorneDataFinal(DiaDaSemana);
            var DataInicial = RetorneDataInicial(DiaDaSemana);

            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var ValoresSemanalPagar = servicoContasPagarReceber.ConsulteTotalAPagarSemanal(DataInicial.Date, DataFinal.Date);

            var ValorAgrupado = ValoresSemanalPagar.GroupBy(x => x.Vencimento).ToList();

            foreach (var item in ValorAgrupado)
            {
                var diaDaSemanaPagar = RetorneDescricaoDiaSemana(item.Key.DayOfWeek);

                var somaTotalPagar = item.Sum(x => x.ValorParcelaPagar);

                chtResumoFinanceiro.Series["A Pagar"].Points.AddXY(diaDaSemanaPagar, somaTotalPagar);
            }

            if (ValorAgrupado.Count ==0)
            {
                chtResumoFinanceiro.Series["A Pagar"].Points.AddXY(0, 0);
            }

            chtResumoFinanceiro.DataBind();
            chtResumoFinanceiro.Visible = true;
        }

        private DateTime RetorneDataFinal(DayOfWeek DiaDaSemana)
        {
            switch (DiaDaSemana)
            {
                case DayOfWeek.Monday:
                    return DateTime.Now.AddDays(5);
                case DayOfWeek.Tuesday:
                    return DateTime.Now.AddDays(4);
                case DayOfWeek.Wednesday:
                    return DateTime.Now.AddDays(3);
                case DayOfWeek.Thursday:
                    return DateTime.Now.AddDays(2);
                case DayOfWeek.Friday:
                    return DateTime.Now.AddDays(1);
                case DayOfWeek.Saturday:
                    return DateTime.Now;
                case DayOfWeek.Sunday:
                    return DateTime.Now.AddDays(-1);

                default:
                    break;
            }

            return DateTime.Now;
        }

        private DateTime RetorneDataInicial(DayOfWeek DiaDaSemana)
        {
            switch (DiaDaSemana)
            {
                case DayOfWeek.Monday:
                    return DateTime.Now;
                case DayOfWeek.Tuesday:
                    return DateTime.Now.AddDays(-1);
                case DayOfWeek.Wednesday:
                    return DateTime.Now.AddDays(-2);
                case DayOfWeek.Thursday:
                    return DateTime.Now.AddDays(-3);
                case DayOfWeek.Friday:
                    return DateTime.Now.AddDays(-4);
                case DayOfWeek.Saturday:
                    return DateTime.Now.AddDays(-5);
                case DayOfWeek.Sunday:
                    return DateTime.Now.AddDays(1);
                default:
                    break;
            }

            return DateTime.Now;
        }

        #endregion

        #region "Eventos Gerais"

        private void button1_Click(object sender, EventArgs e)
        {
            ConverterPara3D();
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, btn3D.ClientRectangle,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            rdbAnual.Checked = true;
            CalculeRecebimentoDoDia();
            CalculePagamentoDoDia();
            CalculeRecebimentoEmAtraso();
            CalculePagamentoEmAtraso();
            CalculeSaldoAtual();
            BuscarValoresAReceberAnualGrafico();
            BuscarValoresAPagarAnualGrafico();

            chtResumoFinanceiro.Titles.Clear();
            chtResumoPizza.Titles.Clear();
            chtResumoFinanceiro.Titles.Add("Anual - "+_anoCorrente);
            chtResumoPizza.Titles.Add("Anual - "+_anoCorrente);
        }

        private void rdbAnual_CheckedChanged(object sender, EventArgs e)
        {
            if(!_estahPizza)
            {
                LimparPontosColunas();
                BuscarValoresAReceberAnualGrafico();
                BuscarValoresAPagarAnualGrafico();
            }
            else
            {
                LimparPontosPizza();
                BuscarValoresAPagar_Receber_Anual_GraficoPizza();
            }

            chtResumoFinanceiro.Titles.Clear();
            chtResumoPizza.Titles.Clear();

            chtResumoFinanceiro.Titles.Add("Anual - "+_anoCorrente);
            chtResumoPizza.Titles.Add("Anual - "+_anoCorrente);
        }

        private void rdbMensal_CheckedChanged(object sender, EventArgs e)
        {
            if(!_estahPizza)
            {
                LimparPontosColunas();
                BuscarValoresAReceberMensalGrafico();
                BuscarValoresAPagarMensalGrafico();
            }
            else
            {
                LimparPontosPizza();
                BuscarValoresA_Pagar_Receber_Mensal_GraficoPizza();
            }

            chtResumoFinanceiro.Titles.Clear();
            chtResumoPizza.Titles.Clear();
            chtResumoFinanceiro.Titles.Add("Mensal - "+ _MesCorrente);
            chtResumoPizza.Titles.Add("Mensal - "+ _MesCorrente);
        }

        private void rdbSemanal_CheckedChanged(object sender, EventArgs e)
        {
            if (!_estahPizza)
            {
                LimparPontosColunas();
                BuscarValoresA_Receber_Semanal();
                BuscarValoresA_Pagar_Semanal();
            }
            else
            {
                LimparPontosPizza();
                BuscarValoresA_Pagar_Receber_Semanal_GraficoPizza();
            }

            chtResumoFinanceiro.Titles.Clear();
            chtResumoPizza.Titles.Clear();
            chtResumoFinanceiro.Titles.Add("Semanal - Atual");
            chtResumoPizza.Titles.Add("Semanal - Atual");
        }

        private void btnTipoGrafico_Click(object sender, EventArgs e)
        {
            if (!_estahPizza)
            {
                LimparPontosPizza();
                
                chtResumoFinanceiro.Visible = false;
                chtResumoPizza.Visible = true;

                if(rdbAnual.Checked)
                {
                    BuscarValoresAPagar_Receber_Anual_GraficoPizza();
                }
                else if(rdbMensal.Checked)
                {
                    BuscarValoresA_Pagar_Receber_Mensal_GraficoPizza();
                }
                else
                {
                    BuscarValoresA_Pagar_Receber_Semanal_GraficoPizza();
                }
                    
                _estahPizza = true;                
                btnTipoGrafico.Text = "Colunas";                
            }
            else
            {
                LimparPontosColunas();
               
                chtResumoPizza.Visible = false;
                if(rdbAnual.Checked)
                {
                    BuscarValoresAPagarAnualGrafico();
                    BuscarValoresAReceberAnualGrafico();
                }
                else if(rdbMensal.Checked)
                {
                    BuscarValoresAReceberMensalGrafico();
                    BuscarValoresAPagarMensalGrafico();
                }
                else
                {
                    BuscarValoresA_Receber_Semanal();
                    BuscarValoresA_Pagar_Semanal();
                }
               
                _estahPizza = false;                
                btnTipoGrafico.Text = "Pizza";                
            }

            ConverterPara3D();
        }

        private void FormResumoFinanceiro_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                if (Application.OpenForms.OfType<FormPrincipal>().Count() > 0)
                    Application.OpenForms["FormPrincipal"].WindowState = FormWindowState.Maximized;
            }
        }

        #endregion




    }
}
