using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using System.Windows.Documents;
using System.Collections.Generic;
using Programax.Easy.Servico.Estoque.EntradaMercadoriaServ;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Servico.Fiscal.NcmServ;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.TransferenciaServ;
using Programax.Infraestrutura.Negocio.Utils;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Programax.Easy.Report.RelatoriosDevExpress.Cadastros
{
    public partial class RelatorioProdutos : RelatorioBasePaisagem
    {
        #region " VARIÁVEIS PRIVADAS "

        private Categoria _categoria;
        private Grupo _grupo;
        private SubGrupo _subGrupo;
        private Marca _marca;
        private Fabricante _fabricante;
        private Tamanho _tamanho;
        private bool _itensComEstoqueMinimo;
        private int? _itemComDDVAbaixoDe;
        private EnumOrdenacaoPesquisaProduto _ordenacaoPesquisaProduto;
        private string _status;
        private double totalVenda;
        private double totalcusto;
        private bool _fiscal;
        private double totalSaldo;
        private int? _estoqueMairQue;
        private bool _mostrarNcms;
        private List<Ncm> listaNcms;
        private string ConectionString;
        private DateTime _dataInicialPeriodo;
        private DateTime _dataFinalPeriodo;
        private double dblReserva = 0;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioProdutos(Categoria categoria,
                                            Grupo grupo,
                                            SubGrupo subGrupo,
                                            Marca marca,
                                            Fabricante fabricante,
                                            Tamanho tamanho,
                                            bool itensComEstoqueMinimo,
                                            bool mostrarNcms,
                                            int? itemComDDVAbaixoDe,
                                            EnumOrdenacaoPesquisaProduto ordenacaoPesquisaProduto,
                                            string status,
                                            bool fiscal,
                                            DateTime dataInicialPeriodo,
                                            DateTime dataFinalPeriodo,
                                            int? estoqueMaiorQue = null)
        {
            InitializeComponent();

            _categoria = categoria;
            _grupo = grupo;
            _subGrupo = subGrupo;
            _marca = marca;
            _fabricante = fabricante;
            _tamanho = tamanho;
            _itensComEstoqueMinimo = itensComEstoqueMinimo;
            _itemComDDVAbaixoDe = itemComDDVAbaixoDe;
            _ordenacaoPesquisaProduto = ordenacaoPesquisaProduto;
            _status = status;
            _estoqueMairQue = estoqueMaiorQue;
            _mostrarNcms = mostrarNcms;
            _fiscal = fiscal;
            _dataInicialPeriodo = dataInicialPeriodo;
            _dataFinalPeriodo = dataFinalPeriodo;

            _tituloRelatorio = "RELATÓRIO DE ITENS";
        }

        #endregion

        #region " MÉTODOS SOBRECARREGADOS "

        protected override void CarregueDadosRelatorio()
        {
            if(_mostrarNcms)
            {
                //lblNcm.Visible = true;
                //txtNcm.Visible = true;
                listaNcms = new ServicoNcm().ConsulteLista();
            }
            
            ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();
            ServicoProduto servicoProduto = new ServicoProduto();
            
            List<VWProduto> listaProdutos = servicoProduto.ConsulteLista(_categoria,
                                                                                                  _grupo,
                                                                                                  _subGrupo,
                                                                                                  _marca,
                                                                                                  _fabricante,
                                                                                                  _tamanho,
                                                                                                  _itensComEstoqueMinimo,
                                                                                                  _itemComDDVAbaixoDe,
                                                                                                  _ordenacaoPesquisaProduto,
                                                                                                  _estoqueMairQue);

            List<ProdutoRelatorio> listaProdutosRelatorio = new List<ProdutoRelatorio>();
            double quantidadesubestoque = 0;
            carregaconexao();


            foreach (var produto in listaProdutos)
            {
                Produto produtoAtivo = new Produto();

                if (_status != "T")
                {
                    if (_status == "A")
                    {
                        produtoAtivo = servicoProduto.ConsulteProdutoAtivo(produto.Id);
                        if (produtoAtivo == null)
                            continue;
                    }
                    //status = "I"
                    else
                    {
                        produtoAtivo = servicoProduto.ConsulteProdutoAtivo(produto.Id);
                        if (produtoAtivo != null)
                            continue;
                    }
                }
                    
                    ProdutoRelatorio produtoRelatorio = new ProdutoRelatorio();
                    double estoquereservado = 0;

                    produtoRelatorio.Categoria = produto.CategoriaDescricao;
                    produtoRelatorio.Grupo = produto.GrupoDescricao;
                    produtoRelatorio.SubGrupo = produto.SubGrupoDescricao;
                    produtoRelatorio.Tamanho = produto.TamanhoDescricao;
                    produtoRelatorio.Unidade = produto.UnidadeDescricao;
                    produtoRelatorio.Marca = produto.MarcaDescricao;

                    produtoRelatorio.Id = produto.Id;
                    produtoRelatorio.CodigoBarras = produto.CodigoBarras;
                    produtoRelatorio.Descricao = produto.Descricao;

                    produtoRelatorio.NCM = _mostrarNcms && produto.CodigoFiscal !=0? listaNcms.Find(x=>x.Id ==produto.CodigoFiscal).CodigoNcm : string.Empty;

                    produtoRelatorio.ValorVenda = produto.ValorVenda.ToString("#,###,##0.00");

                    produtoRelatorio.ValorCompra = produto.PrecoCompra.ToString("R$ #,###,##0.00");

                    produtoRelatorio.Saldo = produto.Estoque.ToString("#,###,##0");

                    produtoRelatorio.EstoqueMinimo = produto.QtdMinima.ToString("#,###,##0");
                    ConsultaReserva(produto.Id);
                //if (dblReserva == 0)
                //{
                //    txtreserva.Text = "0.0000";
                //}
                    
                    produtoRelatorio.Reserva = dblReserva.ToString("#,###,##0");
                    produtoRelatorio.Disponivel = (produto.Estoque - estoquereservado).ToString("#,###,##0");

                ServicoItemTransferencia servicoItemTransferencia = new ServicoItemTransferencia();
                    var ItemTransferencia = servicoItemTransferencia.ConsulteProduto(produto.Id);
                    quantidadesubestoque = 0;

                    if (ItemTransferencia != null)
                    {
                        foreach (var itemproduto in ItemTransferencia)
                        {
                            quantidadesubestoque += itemproduto.QuantidadeEstoque;
                        }

                        produtoRelatorio.QtdSub = quantidadesubestoque.ToString("#0.00");
                    }
                    else
                    {
                        produtoRelatorio.QtdSub = "0.00";
                    }



                    produtoRelatorio.DDV = produto.DDV.ToString("#,###,##0");
                if (_fiscal)
                {
                   
                
                    string sql = string.Empty;
                    using (var conn = new MySqlConnection(ConectionString))
                    {
                        conn.Open();

                        sql = "SELECT itement_prod_id FROM entradas " +
                              " inner join entradasitens ON entradas.entrada_id = entradasitens.itement_entrada_id " +
                              " where  itement_prod_id = " + produto.Id + " limit 1";

                            //entrada_data_cadastro >= '" + _dataInicialPeriodo + "' and entrada_data_cadastro <= '" + _dataFinalPeriodo + "' And " +


                        MySqlCommand MyCommand = new MySqlCommand(sql, conn);

                        var returnValue = MyCommand.ExecuteReader();
                        while (returnValue.Read())
                        {
                            totalVenda += (produto.ValorVenda * produto.Estoque);
                            totalcusto += (produto.PrecoCompra * produto.Estoque);
                            totalSaldo += produto.Estoque;
                            listaProdutosRelatorio.Add(produtoRelatorio);
                        }
                    }

                }
                else
                {
                    totalVenda += (produto.ValorVenda * produto.Estoque);
                    totalcusto += (produto.PrecoCompra * produto.Estoque);
                    totalSaldo += produto.Estoque;

                    listaProdutosRelatorio.Add(produtoRelatorio);
                }



            }

            ConteudoRelatorio.DataSource = listaProdutosRelatorio;

            txtTotalRegistros.Text = listaProdutosRelatorio.Count.ToString();
            txtTotalSaldos.Text = totalSaldo.ToString("#,###,##0");            
            txtTotalVendas.Text = totalVenda.ToString("#,###,##0.00");
            txtTotalCustos.Text = totalcusto.ToString("#,###,##0.00");
        }
        private void carregaconexao()
        {
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
                    StringConexaoII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }

            }

        }
        private void ConsultaReserva(int CodProduto)
        {

            carregaconexao();


            string Sql = string.Empty;
            using (var conn = new MySqlConnection(ConectionString))
            {
                conn.Open();

                var sql = "";

                sql = "SELECT sum(PEDITEM_QUANTIDADE) as Reserva FROM pedidosvendasitens where peditem_produto_id = " + CodProduto + " And PEDITEM_RESERVA > 0 ";



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                
                while (returnValue.Read())
                {
                    dblReserva = returnValue["Reserva"].ToDouble();
                }
            }
        }
        #endregion

        #region " CLASSES AUXILIARES "

        public class ProdutoRelatorio
        {
            public int Id { get; set; }

            public string CodigoBarras { get; set; }

            public string Descricao { get; set; }

            public string NCM { get; set; }

            public string Unidade { get; set; }

            public string Categoria { get; set; }

            public string Grupo { get; set; }

            public string SubGrupo { get; set; }

            public string Marca { get; set; }

            public string Tamanho { get; set; }

            public string ValorCompra { get; set; }

            public string ValorVenda { get; set; }

            public string EstoqueMinimo { get; set; }
            public string Reserva { get; set; }
            public string Disponivel { get; set; }

            public string Saldo { get; set; }
            public string QtdSub { get; set; }

            public string DDV { get; set; }
        }

        #endregion
    }
}
