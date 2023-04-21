using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using MySql.Data.MySqlClient;
using Programax.Easy.Negocio.Financeiro.PlanoContasDreObj.ObjetoDeNegocio;
using System.Data;
using static Programax.Easy.Report.RelatoriosDevExpress.Financeiro.RelatoriodeDRE;
using Newtonsoft.Json;
using Programax.Easy.Servico.Financeiro.PlanoContasDreServ;

namespace Programax.Easy.Report.RelatoriosDevExpress.Financeiro
{
    public partial class RelatoriodeDRE : RelatorioBasePaisagem
    {
        #region " VARIÁVEIS PRIVADAS "

       
        private MySqlDataAdapter Adapter;
        private List<PlanoContaDre> _listaDePlanosDeContas;
        private ReportHeaderBand ReportHeader;
        private XRLine xrLine13;
        private XRLabel xrLabel6;
        private XRLabel xrLabel2;
        private XRLabel Descricao;
        private XRLabel Valor;
        //private System.Windows.Forms.BindingSource bindingSource1;
        private List<RelatorioDRE> relatorioDRE;
        private string _DataInicial;
        private XRLabel xrLabel3;
        private System.Windows.Forms.BindingSource bindingSource1;
        private IContainer components;
        private XRLabel xrLabel4;
        private XRLabel txtRef;
        private string _DataFinal;
        private string _Referencia;

        public string ConectionString { get; private set; }

        #endregion

        #region " CONSTRUTOR "

        public RelatoriodeDRE( string DataInicial, string DataFinal, string Referencia)
        {
            InitializeComponent();
            _tituloRelatorio = "Demostração de Resultado do Exercício";
            _DataInicial = DataInicial;
            _DataFinal = DataFinal;
            _Referencia = Referencia;
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            //PreenchaTotaisCheques();
            AtualizarGridDePlanoDeContas();
            PrenchaListaDre();
            PreenchaDataSource();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void AtualizarGridDePlanoDeContas()
        {
            ServicoPlanoDeContasDre servicoPlanoDeContas = new ServicoPlanoDeContasDre();
            _listaDePlanosDeContas = servicoPlanoDeContas.ConsulteLista();
        }

        private void PrenchaListaDre()
        {


            txtRef.Text = _Referencia;

            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

            var item = conexoes.Conexoes[IndiceBancoDados];

            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
            }
            else
            {
                ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverSecundarioOnline)
                {
                    StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }





            }
            

            //string DataInicial = cboAno.Text + "-" + strMes + "-01";
            //string DataFinal = intAno.ToString() + "-" + strMes.ToString() + "-" + diaMes.ToString();

            DateTime dateTimeValue = _DataInicial.ToDate();
            DateTime dateTimeValue2 = _DataFinal.ToDate();

            //List<PlanoDeContasAuxiliar> listaDePlanosDeContasAuxiliares = new List<PlanoDeContasAuxiliar>();
            List<RelatorioDRE> listaDre = new List<RelatorioDRE>();
            double dblSalarios = 0;
            double dblFGTS = 0;
            double dblReceita = 0;
            double dblSimples = 0;
            double dblComissao = 0;
            double dblFerias = 0;
            double dblInss = 0;
            double dblDecimo = 0;
            double dblEncargos = 0;
            double dblLucroOperacional = 0;
            double dblDespesaOperacional = 0;
            double dblDespesaComercial = 0;
            double dblDespesaGeral = 0;
            double dblDespesaFinanceiras = 0;
            double dblLucroLiquido = 0;
            double dblMutuo = 0;
            double dblLucroLiquidoFinal = 0;

            foreach (var planoDeContas in _listaDePlanosDeContas)
            {
                RelatorioDRE ItemDRE = new RelatorioDRE();

                ItemDRE.Id = planoDeContas.Id;
                
                if (planoDeContas.Grau == "1")
                {
                    ItemDRE.Descricao = "        " + planoDeContas.Descricao;
                }
                else
                {
                    ItemDRE.Descricao = planoDeContas.Descricao;
                }

                ItemDRE.Id = planoDeContas.Id;

                //if (planoDeContas.Grau == "1")
                //{
                switch (planoDeContas.Id)
                {
                    case 2:
                        using (var conn = new MySqlConnection(ConectionString))
                        {
                            MySqlCommand command = new MySqlCommand("DRE", conn);
                            command.CommandType = CommandType.StoredProcedure;


                            command.Parameters.AddWithValue("varDataInicial", dateTimeValue);
                            command.Parameters.AddWithValue("varDataFinal", dateTimeValue2);

                            conn.Open();
                            command.ExecuteNonQuery();

                            var returnValue = command.ExecuteReader();
                            double variavel = 0;
                            while (returnValue.Read())
                            {
                                variavel = returnValue["Total"].ToDouble();
                                ItemDRE.Valor = "R$ " + string.Format("{0:N}", variavel);
                                dblReceita = variavel.ToDouble();
                            }
                            conn.Close();
                        }
                        break;
                    case 7:
                        dblReceita = dblReceita - dblSimples;
                        ItemDRE.Valor = "R$ " + string.Format("{0:N}", dblReceita);
                        break;

                    case 8:
                        using (var conn = new MySqlConnection(ConectionString))
                        {
                            MySqlCommand command = new MySqlCommand("Entradas", conn);
                            command.CommandType = CommandType.StoredProcedure;


                            command.Parameters.AddWithValue("varDataInicial", dateTimeValue);
                            command.Parameters.AddWithValue("varDataFinal", dateTimeValue2);

                            conn.Open();
                            command.ExecuteNonQuery();

                            var returnValue = command.ExecuteReader();
                            double variavel = 0;
                            while (returnValue.Read())
                            {
                                variavel = returnValue["Valor"].ToDouble();
                                ItemDRE.Valor = "R$ " + string.Format("{0:N}", variavel);

                            }
                            conn.Close();
                        }
                        break;
                    case 10:
                        dblLucroOperacional = dblReceita;
                        ItemDRE.Valor = "R$ " + string.Format("{0:N}", dblLucroOperacional);
                        break;

                    case 17:
                        dblFGTS = (dblSalarios * 8) / 100;
                        dblInss = (dblSalarios * 26) / 100;
                        dblFGTS = dblFGTS + dblInss;
                        ItemDRE.Valor = "R$ " + string.Format("{0:N}", dblFGTS);
                        break;
                    case 18:
                        dblFerias = ((dblSalarios + dblComissao) * 33) / 100;
                        ItemDRE.Valor = "R$ " + string.Format("{0:N}", dblFerias);
                        break;
                    case 19:
                        dblDecimo = (dblSalarios + dblComissao) / 12;
                        ItemDRE.Valor = "R$ " + string.Format("{0:N}", dblDecimo);
                        break;
                    case 20:
                        dblEncargos = (((dblFerias * 8) / 100) + ((dblDecimo * 8) / 100));
                        ItemDRE.Valor = "R$ " + string.Format("{0:N}", dblEncargos);
                        break;
                    case 42:
                        ItemDRE.Valor = "R$ " + string.Format("{0:N}", dblDespesaOperacional);
                        break;
                    case 50:
                        ItemDRE.Valor = "R$ " + string.Format("{0:N}", dblDespesaComercial);
                        break;
                    case 90:
                        ItemDRE.Valor = "R$ " + string.Format("{0:N}", dblDespesaGeral);
                        break;
                    case 99:
                        ItemDRE.Valor = "R$ " + string.Format("{0:N}", dblDespesaFinanceiras);
                        break;
                    case 104:
                        dblLucroLiquidoFinal = dblLucroOperacional - dblDespesaOperacional - dblDespesaGeral - dblDespesaFinanceiras - dblDespesaComercial - dblMutuo;
                        ItemDRE.Valor = "R$ " + string.Format("{0:N}", dblLucroLiquidoFinal);
                        break;
                    case 107:
                        dblLucroLiquido = dblLucroOperacional - dblDespesaOperacional - dblDespesaGeral - dblDespesaFinanceiras - dblDespesaComercial - dblMutuo;
                        ItemDRE.Valor = "R$ " + string.Format("{0:N}", dblLucroLiquido);
                        break;
                    default:
                        double variavelImposto = 0;

                        using (var conn = new MySqlConnection(ConectionString))
                        {
                            MySqlCommand commandImposto = new MySqlCommand("DREImpostos", conn);
                            commandImposto.CommandType = CommandType.StoredProcedure;


                            commandImposto.Parameters.AddWithValue("varDataInicial", dateTimeValue);
                            commandImposto.Parameters.AddWithValue("varDataFinal", dateTimeValue2);
                            commandImposto.Parameters.AddWithValue("varCategoria", planoDeContas.NumeroPlanoContasContador);


                            conn.Open();
                            commandImposto.ExecuteNonQuery();

                            var returnValueImposto = commandImposto.ExecuteReader();

                            while (returnValueImposto.Read())
                            {
                                variavelImposto = returnValueImposto["Valor"].ToDouble();

                                if (variavelImposto == 0)
                                {
                                    switch (planoDeContas.Id)
                                    {
                                        case 1:
                                            ItemDRE.Valor = "";
                                            break;
                                        case 3:
                                            ItemDRE.Valor = "";
                                            break;
                                        case 11:
                                            ItemDRE.Valor = "";
                                            break;
                                        case 43:
                                            ItemDRE.Valor = "";
                                            break;
                                        case 51:
                                            ItemDRE.Valor = "";
                                            break;
                                        case 91:
                                            ItemDRE.Valor = "";
                                            break;
                                        case 100:
                                            ItemDRE.Valor = "";
                                            break;
                                        case 104:
                                            ItemDRE.Valor = "";
                                            break;
                                        default:
                                            ItemDRE.Valor = "R$ " + "0,00";
                                            break;
                                    }

                                }
                                else
                                {
                                    switch (planoDeContas.Id)
                                    {
                                        case 4:
                                            dblSimples = variavelImposto;
                                            break;
                                        case 12:
                                            dblSalarios = variavelImposto;
                                            dblDespesaOperacional += dblSalarios;
                                            break;
                                        case 13:
                                            dblComissao = variavelImposto;
                                            dblDespesaOperacional += dblComissao;
                                            break;

                                        default:
                                            if (15 <= planoDeContas.Id && planoDeContas.Id < 42)
                                            {
                                                dblDespesaOperacional += variavelImposto;
                                            }

                                            if (44 <= planoDeContas.Id && planoDeContas.Id < 50)
                                            {
                                                dblDespesaComercial += variavelImposto;
                                            }

                                            if (52 <= planoDeContas.Id && planoDeContas.Id < 90)
                                            {
                                                dblDespesaGeral += variavelImposto;
                                            }

                                            if (92 <= planoDeContas.Id && planoDeContas.Id < 99)
                                            {
                                                dblDespesaFinanceiras += variavelImposto;
                                            }
                                            if (planoDeContas.Id == 103)
                                            {
                                                dblMutuo += variavelImposto;
                                            }
                                            break;
                                    }
                                    ItemDRE.Valor = "R$ " + string.Format("{0:N}", variavelImposto);
                                }

                            }

                            conn.Close();
                        }
                        if (variavelImposto == 0)
                        {
                            using (var conn = new MySqlConnection(ConectionString))
                            {
                                MySqlCommand commandCaixa = new MySqlCommand("Caixa", conn);
                                commandCaixa.CommandType = CommandType.StoredProcedure;


                                commandCaixa.Parameters.AddWithValue("varDataInicial", dateTimeValue);
                                commandCaixa.Parameters.AddWithValue("varDataFinal", dateTimeValue2);
                                commandCaixa.Parameters.AddWithValue("varCategoria", planoDeContas.NumeroPlanoContasContador);


                                conn.Open();
                                commandCaixa.ExecuteNonQuery();

                                var returnValueCaixa = commandCaixa.ExecuteReader();
                                double variavelCaixa = 0;
                                while (returnValueCaixa.Read())
                                {
                                    variavelCaixa = returnValueCaixa["Valor"].ToDouble();

                                    if (variavelCaixa == 0)
                                    {

                                        switch (planoDeContas.Id)
                                        {
                                            case 1:
                                                ItemDRE.Valor = "";
                                                break;
                                            case 3:
                                                ItemDRE.Valor = "";
                                                break;
                                            case 11:
                                                ItemDRE.Valor = "";
                                                break;
                                            case 43:
                                                ItemDRE.Valor = "";
                                                break;
                                            case 51:
                                                ItemDRE.Valor = "";
                                                break;
                                            case 91:
                                                ItemDRE.Valor = "";
                                                break;
                                            case 100:
                                                ItemDRE.Valor = "";
                                                break;
                                            //case 104:
                                            //    ItemDRE.Valor = "";
                                            //    break;
                                            default:
                                                ItemDRE.Valor = "R$ " + "0,00";
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (planoDeContas.Id)
                                        {
                                            case 13:
                                                dblComissao = variavelCaixa;
                                                dblDespesaOperacional += dblComissao;
                                                break;
                                            default:
                                                if (15 <= planoDeContas.Id && planoDeContas.Id < 42)
                                                {
                                                    dblDespesaOperacional += variavelCaixa;
                                                }
                                                if (44 <= planoDeContas.Id && planoDeContas.Id < 50)
                                                {
                                                    dblDespesaComercial += variavelCaixa;
                                                }
                                                if (52 <= planoDeContas.Id && planoDeContas.Id < 90)
                                                {
                                                    dblDespesaGeral += variavelCaixa;
                                                }
                                                if (92 <= planoDeContas.Id && planoDeContas.Id < 99)
                                                {
                                                    dblDespesaFinanceiras += variavelCaixa;
                                                }
                                                break;
                                        }
                                        ItemDRE.Valor = "R$ " + string.Format("{0:N}", variavelCaixa); ;
                                    }

                                }

                                conn.Close();
                            }


                        }
                        break;
                }

                listaDre.Add(ItemDRE);
            }


            relatorioDRE = listaDre;
        }
       

        private void PreenchaDataSource()
        {
            ConteudoRelatorio.DataSource = relatorioDRE;
        }

        #endregion

        #region " CLASSES AUXILIARES "

       
        public class RelatorioDRE
        {
            public int Id { get; set; }

            public string NumeroPlanoDeContas { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }

            public string Natureza { get; set; }

            public string Tipo { get; set; }

            public string NumeroPlanoDeContasContador { get; set; }

            public int Grau { get; set; }

            public string Valor { get; set; }
        }

        #endregion

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine13 = new DevExpress.XtraReports.UI.XRLine();
            this.Descricao = new DevExpress.XtraReports.UI.XRLabel();
            this.Valor = new DevExpress.XtraReports.UI.XRLabel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.txtRef = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // ConteudoRelatorio
            // 
            this.ConteudoRelatorio.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.ReportHeader});
            this.ConteudoRelatorio.DataSource = this.bindingSource1;
            this.ConteudoRelatorio.Controls.SetChildIndex(this.ReportHeader, 0);
            this.ConteudoRelatorio.Controls.SetChildIndex(this.Detail1, 0);
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Valor,
            this.Descricao});
            this.Detail1.Expanded = true;
            this.Detail1.HeightF = 17.70833F;
            this.Detail1.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth;
            // 
            // lblTelefone
            // 
            this.lblTelefone.StylePriority.UseFont = false;
            this.lblTelefone.StylePriority.UseTextAlignment = false;
            // 
            // lblCidade
            // 
            this.lblCidade.StylePriority.UseFont = false;
            this.lblCidade.StylePriority.UseTextAlignment = false;
            // 
            // lblEndereco
            // 
            this.lblEndereco.StylePriority.UseFont = false;
            this.lblEndereco.StylePriority.UseTextAlignment = false;
            // 
            // lblNomeEmpresa
            // 
            this.lblNomeEmpresa.StylePriority.UseFont = false;
            this.lblNomeEmpresa.StylePriority.UseTextAlignment = false;
            // 
            // lblDataEmissao
            // 
            this.lblDataEmissao.StylePriority.UseFont = false;
            this.lblDataEmissao.StylePriority.UseTextAlignment = false;
            // 
            // lblUsuario
            // 
            this.lblUsuario.StylePriority.UseFont = false;
            this.lblUsuario.StylePriority.UseTextAlignment = false;
            // 
            // lblPagina
            // 
            this.lblPagina.StylePriority.UseFont = false;
            this.lblPagina.StylePriority.UseTextAlignment = false;
            // 
            // lblTituloRelatorio
            // 
            this.lblTituloRelatorio.StylePriority.UseFont = false;
            this.lblTituloRelatorio.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel1
            // 
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtRef,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLine13});
            this.ReportHeader.HeightF = 25F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(3.750006F, 0F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(660.8578F, 15.62F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "Descrição";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(664.6078F, 0F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(130.9467F, 15.62F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Valor";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLine13
            // 
            this.xrLine13.LocationFloat = new DevExpress.Utils.PointFloat(3.750014F, 15.62F);
            this.xrLine13.Name = "xrLine13";
            this.xrLine13.SizeF = new System.Drawing.SizeF(1141F, 6.749998F);
            // 
            // Descricao
            // 
            this.Descricao.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Descricao]")});
            this.Descricao.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Descricao.LocationFloat = new DevExpress.Utils.PointFloat(24.58331F, 0F);
            this.Descricao.Multiline = true;
            this.Descricao.Name = "Descricao";
            this.Descricao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Descricao.SizeF = new System.Drawing.SizeF(640.0245F, 15.62F);
            this.Descricao.StylePriority.UseFont = false;
            this.Descricao.Text = "Descricao";
            // 
            // Valor
            // 
            this.Valor.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Valor]")});
            this.Valor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Valor.LocationFloat = new DevExpress.Utils.PointFloat(664.6078F, 0F);
            this.Valor.Multiline = true;
            this.Valor.Name = "Valor";
            this.Valor.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.Valor.SizeF = new System.Drawing.SizeF(130.0651F, 15.62F);
            this.Valor.StylePriority.UseFont = false;
            this.Valor.StylePriority.UseTextAlignment = false;
            this.Valor.Text = "Descricao";
            this.Valor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Programax.Easy.Report.RelatoriosDevExpress.Financeiro.RelatoriodeDRE.RelatorioDRE);
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(923.9602F, 0F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(78.93823F, 15.62F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "Referência:";
            // 
            // txtRef
            // 
            this.txtRef.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRef.LocationFloat = new DevExpress.Utils.PointFloat(1002.898F, 0F);
            this.txtRef.Multiline = true;
            this.txtRef.Name = "txtRef";
            this.txtRef.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtRef.SizeF = new System.Drawing.SizeF(141.8516F, 15.62F);
            this.txtRef.StylePriority.UseFont = false;
            this.txtRef.Text = "Referência:";
            // 
            // RelatoriodeDRE
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.PageHeader,
            this.PageFooter,
            this.ConteudoRelatorio});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.bindingSource1});
            this.DataSource = this.bindingSource1;
            this.Version = "18.1";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}


