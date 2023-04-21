using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Negocio.TeleMarketing.Enumeradores;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Telemarketing.TmkServ;
using Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Data;
using Programax.Easy.Negocio;
using Programax.Easy.View.ClassesAuxiliares;

namespace Programax.Easy.View.Telas.TeleMarketing
{
    public partial class FormBuscaAtendimentosRefiltek : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Tmk> _listaTmk;
        private List<TmkGrid> _listaTmkGrid;
                     
        private PedidoDeVenda _pedidoDeVendaSelecionado;
        private string ConectionStringII;
       
        private DataSet mDataSet;
        private MySqlDataAdapter mAdapter;
        Boolean gerente = false;
        private int contador = 0;

        #endregion

        #region " CONSTRUTOR "

        public FormBuscaAtendimentosRefiltek(bool somenteImpressao = false)
        {
            InitializeComponent();


            _listaTmk = new List<Tmk>();

            Libere();
            PreenchaCboStatusAtendimento();
            PreenchaCboPeriodoPreDeterminado();
           
            PreenchaCboMarcas();

            PreenchaCboCarteiras();
            PreenchaCboVendedores();
            if (gerente == false)
                cboVendedores.Enabled = false;

            if (somenteImpressao)
            {
                this.NomeDaTela = "Telemarketing";
                btnAtender.Visible = false;
            }

            txtDataInicial.DateTime = DateTime.Now.Date;
            txtDataFinal.DateTime = DateTime.Now.Date;

            this.ActiveControl = txtDataInicial;
        }

        private void PreenchaCboVendedores()
        {
            this.Cursor = Cursors.WaitCursor;


            carregaconexaoII();

            string Sql = string.Empty;
            using (var conn = new MySqlConnection(ConectionStringII))
            {
                conn.Open();

                int Usuario = Sessao.PessoaLogada.Id;
                string Vendedor = Usuario.ToString();
                var sql = "";

                sql = "SELECT Vendedor as ID, pessoas.pes_razao as Nome FROM carteiras " +
                        " inner join pessoas ON carteiras.Vendedor = pessoas.pes_id ORDER BY pes_razao";



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                int numPedido = 0;
                int intNum = 1;
                List<VendedorCbo> listaVenedor = new List<VendedorCbo>();
                while (returnValue.Read())
                {
                    VendedorCbo listVenedor = new VendedorCbo();

                    listVenedor.Vendedor = returnValue["Nome"].ToString();
                    listVenedor.ID = returnValue["ID"].ToInt();
                    listaVenedor.Add(listVenedor);
                }
                var lista = listaVenedor;

                lista.Insert(0, null);

                cboVendedores.Properties.DisplayMember = "Vendedor";
                cboVendedores.Properties.ValueMember = "ID";
                cboVendedores.Properties.DataSource = lista;

            }
            this.Cursor = Cursors.Default;


        }
        private void PreenchaCboCarteiras()
        {
            this.Cursor = Cursors.WaitCursor;


            carregaconexaoII();

            string Sql = string.Empty;
            using (var conn = new MySqlConnection(ConectionStringII))
            {
                conn.Open();

                int Usuario = Sessao.PessoaLogada.Id;
                string Vendedor = Usuario.ToString();
                var sql = "";
                if (gerente == false)
                {
                    sql = "Select ID, NomeCarteira From carteiras Where Vendedor IN (" + Vendedor + ")  order by ID";
                }
                else
                {
                    sql = "Select ID, NomeCarteira From carteiras   order by ID";
                }


                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                int numPedido = 0;
                int intNum = 1;
                List<CarteiraCbo> listaCarteira = new List<CarteiraCbo>();
                while (returnValue.Read())
                {
                    CarteiraCbo listCarteira = new CarteiraCbo();

                    listCarteira.Carteira = returnValue["NomeCarteira"].ToString();
                    listCarteira.ID = returnValue["ID"].ToInt();
                    listaCarteira.Add(listCarteira);
                }
                var lista = listaCarteira;

                lista.Insert(0, null);

                cboCarteiras.Properties.DisplayMember = "Carteira";
                cboCarteiras.Properties.ValueMember = "ID";
                cboCarteiras.Properties.DataSource = lista;

            }
            this.Cursor = Cursors.Default;


        }
        private void Libere()
        {
            foreach (var permissao in Sessao.ListaDePermissoes)
            {
                if (string.IsNullOrWhiteSpace(permissao.NomeMenu))
                {
                    continue;
                }

                if (permissao.NomeMenu == "tspMenuCarteiras")
                {
                    if (permissao != null && permissao.Acessar)
                    {
                        gerente = true;
                    }
                    else
                    {
                        gerente = false;
                    }
                }
            }
        }
        #endregion


        #region " EVENTOS CONTROLES "

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            // Pesquise();
            PesquiseStatus(cboStatusAtendimento.EditValue.ToInt());
            this.Cursor = Cursors.Default;
        }

        private void txtIdCliente_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdCliente.Text))
            {
                Pesquisecliente();
            }
            else
            {
                PreenchaCliente(null);
            }
        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisarefiltek formPessoaPesquisarefiltek = new FormPessoaPesquisarefiltek();

            var cliente = formPessoaPesquisarefiltek.PesquisePessoaClienteAtiva();


            if (cliente != null)
            {
                PreenchaCliente(cliente);
            }
        }

        private void cboSituacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PesquiseStatus(cboStatusAtendimento.EditValue.ToInt());
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcPedidosDeVenda_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcPedidosDeVenda_KeyDown(object sender, KeyEventArgs e)
        {           

            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void gcPedidosDeVenda_Click(object sender, EventArgs e)
        {           

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
           
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboMarcas()
        {
            ServicoMarca servicoMarcas = new ServicoMarca();

            var marcas = servicoMarcas.ConsulteListaAtiva();

            marcas.Insert(0, null);

            cboMarcas.Properties.DataSource = marcas;
            cboMarcas.Properties.DisplayMember = "Descricao";
            cboMarcas.Properties.ValueMember = "Id";

            if (cboMarcas.EditValue != null)
            {
                if (!marcas.Exists(marca => marca != null && marca.Id == cboMarcas.EditValue.ToInt()))
                {
                    cboMarcas.EditValue = null;
                }
            }
        }

        public PedidoDeVenda ExibaPesquisaDePedidosDeVenda()
        {
            this.ShowDialog();

            return _pedidoDeVendaSelecionado;
        }

        public PedidoDeVenda ExibaPesquisaDePedidosDeVendaFaturadosEEmitdosNfe()
        {
            List<ObjetoDescricaoValor> lista = new List<ObjetoDescricaoValor>();

            ObjetoDescricaoValor objetoDescricaoValorFaturado = new ObjetoDescricaoValor();
            objetoDescricaoValorFaturado.Descricao = EnumStatusPedidoDeVenda.FATURADO.Descricao();
            objetoDescricaoValorFaturado.Valor = EnumStatusPedidoDeVenda.FATURADO;

            ObjetoDescricaoValor objetoDescricaoValorEmitidoNfe = new ObjetoDescricaoValor();
            objetoDescricaoValorEmitidoNfe.Descricao = EnumStatusPedidoDeVenda.EMITIDONFE.Descricao();
            objetoDescricaoValorEmitidoNfe.Valor = EnumStatusPedidoDeVenda.EMITIDONFE;

            lista.Add(objetoDescricaoValorFaturado);
            lista.Add(objetoDescricaoValorEmitidoNfe);

            cboStatusAtendimento.Properties.DataSource = lista;
            cboStatusAtendimento.EditValue = EnumStatusPedidoDeVenda.FATURADO;

            this.ShowDialog();

            return _pedidoDeVendaSelecionado;
        }

        private void Pesquise()
        {
            this.Cursor = Cursors.WaitCursor;

            DateTime? dataInicial = txtDataInicial.Text.ToDateNullabel();
            DateTime? dataFinal = txtDataFinal.Text.ToDateNullabel();

            if (cboPeriodoPreDeterminado.EditValue != null)
            {
                if ((EnumPeriodoPreDeterminado)cboPeriodoPreDeterminado.EditValue == EnumPeriodoPreDeterminado.SEISMESES)
                {
                    dataInicial = DateTime.Now.AddMonths(-6);
                    dataFinal = DateTime.Now;

                    //dataInicial = DateTime.Now.AddMonths(-1);
                    //dataFinal = dataInicial.Value.AddYears(6);

                }
                else if ((EnumPeriodoPreDeterminado)cboPeriodoPreDeterminado.EditValue == EnumPeriodoPreDeterminado.UMANO)
                {
                    dataInicial = DateTime.Now.AddYears(-2);
                    dataFinal = dataInicial.Value.AddYears(1);
                }
                else if ((EnumPeriodoPreDeterminado)cboPeriodoPreDeterminado.EditValue == EnumPeriodoPreDeterminado.UMANOEMEIO)
                {
                    dataInicial = DateTime.Now.AddYears(-3);
                    dataFinal = dataInicial.Value.AddYears(1).AddMonths(6);
                }

                txtDataInicial.Text = dataInicial.Value.ToString("dd/MM/yyyy");
                txtDataFinal.Text = dataFinal.Value.ToString("dd/MM/yyyy");
            }
            string conexoesStringII = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoesII.json");

            ConexoesJsonII conexoesII = JsonConvert.DeserializeObject<ConexoesJsonII>(conexoesStringII);

            var item = conexoesII.ConexoesII[IndiceBancoDados];
            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionStringII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
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



            Pessoa cliente = !string.IsNullOrEmpty(txtIdCliente.Text) ? new Pessoa { Id = txtIdCliente.Text.ToInt() } : null;
                        
            //EnumStatusAtendimento? statusTmk = (EnumStatusAtendimento?)cboStatusAtendimento.EditValue;

            int marcaId = cboMarcas.EditValue.ToInt();
            
            ServicoTmk servicoTmk = new ServicoTmk();

            using (var conn = new MySqlConnection(ConectionStringII))
            {
                MySqlCommand command = new MySqlCommand("ConsultaRefiltek", conn);
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("varDataInicial", dataInicial);
                command.Parameters.AddWithValue("varDataFinal", dataFinal);

                conn.Open();
                command.ExecuteNonQuery();

                var returnValue = command.ExecuteReader();
                
                double variavel = 0;

                List<TmkGrid> listaTmkGrid = new List<TmkGrid>();
                int numPedido = 0;
                while (returnValue.Read())
                {

                    TmkGrid tmkGrid = new TmkGrid();

                    tmkGrid.Id = returnValue["NumPedido"].ToInt();

                    tmkGrid.DataCompra = string.Format(returnValue["DataCompra"].ToString(), "dd/MM/yyyy".ToString());

                    tmkGrid.CodigoCliente = returnValue["ClienteId"].ToInt() != 0 ? returnValue["ClienteId"].ToString() : null;

                    tmkGrid.Cliente = returnValue["pes_razao"].ToString() != "" ? returnValue["pes_razao"].ToString() : null; 
                    tmkGrid.Status = returnValue["status"].ToInt() != 0 ? returnValue["status"].ToString() : null;
                    
                   
                    //tmkGrid.Status = statusTmk.ToString();
                    //tmkGrid.Status = statusTmk.ToString();
                    tmkGrid.NumPedidoNovo = returnValue["NumPedidoNovo"].ToInt();


                    if (returnValue["status"].ToString() != null)
                    {
                        if (returnValue["status"].ToInt() == 3)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleGreen;
                            tmkGrid.Status = "DISPONIVEL";
                        }
                        else if (returnValue["status"].ToInt() == 1)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleRed16x16;
                            tmkGrid.Status = "EM ATENDIMENTO";
                        }
                        else if (returnValue["status"].ToInt() == 2)
                        {
                            tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                            tmkGrid.Status = "CONCLUIDO";
                        }
                        else if (returnValue["status"].ToInt() == 4)
                        {
                            tmkGrid.Cor = Properties.Resources.circle_black;
                            tmkGrid.Status = "CANCELADO";
                        }

                        else
                        {
                            tmkGrid.Cor = Properties.Resources.CircleGreen;
                            tmkGrid.Status = "DISPONIVEL";
                        }
                    }

                    if (numPedido != tmkGrid.Id)
                    {
                        listaTmkGrid.Add(tmkGrid);
                    }


                    numPedido = tmkGrid.Id.ToInt();

                   


                }
                gcAtendimentos.DataSource = listaTmkGrid;
                gcAtendimentos.RefreshDataSource();
                

                _listaTmkGrid = listaTmkGrid;
                VerificaContador(dataInicial.ToString(), dataFinal.ToString());
            }
             _listaTmk = servicoTmk.ConsulteListaParaTMK(cliente, null, dataInicial, dataFinal, marcaId,0);

           

            this.Cursor = Cursors.Default;
        }
        private void VerificaContador(string DataInicial, string DataFinal)
        {
            string conexoesStringII = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoesII.json");

            ConexoesJsonII conexoesII = JsonConvert.DeserializeObject<ConexoesJsonII>(conexoesStringII);

            var item = conexoesII.ConexoesII[IndiceBancoDados];
            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionStringII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
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

            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionStringII))
            {

                conn.Open();
                string DataIni = DataInicial.Substring(6, 4) + "-" + DataInicial.Substring(3, 2) + "-" + DataInicial.Substring(0, 2);
                string DataF = DataFinal.Substring(6, 4) + "-" + DataFinal.Substring(3, 2) + "-" + DataFinal.Substring(0, 2);

                string Sql = "select count(roteiros.rot_pedido_id) as Contador, roteirizacao.roteiro_data_conclusao as DataCompra," +
                                " roteiros.rot_pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId," +
                                " roteiros.rot_status as status, historicosatendimento.hisat_novo_pedido_id as NumPedidoNovo" +
                                " FROM roteiros" +
                                " inner join roteirizacao on roteiros.ROT_ROTEIRIZACAO_ID = roteirizacao.roteiro_id" +
                                " inner join pedidosvendas on roteiros.rot_pedido_id = pedidosvendas.pedido_id" +
                                " left join historicosatendimento on roteiros.rot_pedido_id = historicosatendimento.hisat_pedido_id" +
                                " Where roteirizacao.roteiro_data_conclusao Between '" + DataIni + "' And '" + DataF + "' order by DataCompra";

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    txtQtdePedidos.Text = returnValue["Contador"].ToString();
                }

            }

        }
        private void PesquiseStatus(int status)
        {
            this.Cursor = Cursors.WaitCursor;

            string conexoesStringII = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoesII.json");

            ConexoesJsonII conexoesII = JsonConvert.DeserializeObject<ConexoesJsonII>(conexoesStringII);

            var item = conexoesII.ConexoesII[IndiceBancoDados];
            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionStringII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
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

            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionStringII))
            {
               
                //ServicoTmk servicoTmk = new ServicoTmk();
                conn.Open();
                string DataIni = txtDataInicial.Text.Substring(6, 4) + "-" + txtDataInicial.Text.Substring(3, 2) + "-" + txtDataInicial.Text.Substring(0, 2);
                string DataF = txtDataFinal.Text.Substring(6, 4) + "-" + txtDataFinal.Text.Substring(3, 2) + "-" + txtDataFinal.Text.Substring(0, 2);

                string sqlWhere = "  date(pedidosvendas.pedido_data_elaboracao) Between '" + DataIni + "' And '" + DataF + "' ";
                string innerJoin = " ";

                //innerJoin = innerJoin + " inner join roteirizacao on roteiros.ROT_ROTEIRIZACAO_ID = roteirizacao.roteiro_id ";
                //innerJoin = innerJoin + " inner join pedidosvendas on roteiros.rot_pedido_id = pedidosvendas.pedido_id ";
                innerJoin = innerJoin + " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id ";
                innerJoin = innerJoin + " left join historicosatendimento on pedidosvendas.pedido_id = historicosatendimento.hisat_pedido_id ";


                if (status != 0)
                {
                    if (status == 1)
                    {
                        sqlWhere = sqlWhere + " AND (historicosatendimento.hisat_status = 1 OR historicosatendimento.hisat_status is null)";
                    }
                    else
                    {
                        sqlWhere = sqlWhere + " AND historicosatendimento.hisat_status = " + status;
                    }
                   

                }

                if (txtIdCliente.Text != "")
                {
                    sqlWhere = sqlWhere + " AND pedidosvendas.pedido_cliente_id = " + txtIdCliente.Text.ToInt() ;
                }


                var sql = "select  hisat_data_historico as DataPedido, pedidosvendas.pedido_data_elaboracao as DataCompra, " +
                                     " pedidosvendas.pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId, " +
                                     " historicosatendimento.hisat_status as status, historicosatendimento.hisat_novo_pedido_id as NumPedidoNovo, " +
                                     " pessoas.pes_razao " +
                            " FROM  pedidosvendas " +

                    innerJoin +

                    " WHERE " + sqlWhere + " order by DataCompra, NumPedido ";



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                List<TmkGrid> listaTmkGrid = new List<TmkGrid>();
                int numPedido = 0;
                int numQtd = 0;

                while (returnValue.Read())
                {

                    TmkGrid tmkGrid = new TmkGrid();

                    tmkGrid.Id = returnValue["NumPedido"].ToInt();

                    tmkGrid.DataCompra = string.Format(returnValue["DataCompra"].ToString(), "dd/MM/yyyy".ToString());

                    tmkGrid.CodigoCliente = returnValue["ClienteId"].ToInt() != 0 ? returnValue["ClienteId"].ToString() : null;

                    tmkGrid.Cliente = returnValue["pes_razao"].ToString() != "" ? returnValue["pes_razao"].ToString() : null;
                    tmkGrid.Status = returnValue["status"].ToInt() != 0 ? returnValue["status"].ToString() : null;


                    //tmkGrid.Status = statusTmk.ToString();
                    //tmkGrid.Status = statusTmk.ToString();
                    tmkGrid.NumPedidoNovo = returnValue["NumPedidoNovo"].ToInt();
                    if (tmkGrid.NumPedidoNovo !=0)
                    {
                        tmkGrid.DataPedido = string.Format(returnValue["DataPedido"].ToString(), "dd/MM/yyyy".ToString());
                    }

                    if (returnValue["status"].ToString() != null)
                    {
                        if (returnValue["status"].ToInt() == 1)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleGreen;
                            tmkGrid.Status = "DISPONIVEL";
                            tmkGrid.NumPedidoNovo = 0;
                            tmkGrid.DataPedido = "";

                        }
                        else if (returnValue["status"].ToInt() == 3)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleRed16x16;
                            tmkGrid.Status = "EM ATENDIMENTO";
                        }
                        else if (returnValue["status"].ToInt() == 2)
                        {
                            tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                            tmkGrid.Status = "CONCLUIDO";
                        }
                        else if (returnValue["status"].ToInt() == 4)
                        {
                            tmkGrid.Cor = Properties.Resources.circle_black;
                            tmkGrid.Status = "CANCELADO";
                            tmkGrid.NumPedidoNovo = 0;
                            tmkGrid.DataPedido = "";
                        }

                        else
                        {
                            tmkGrid.Cor = Properties.Resources.CircleGreen;
                            tmkGrid.Status = "DISPONIVEL";
                        }
                    }


                   
                    if (numPedido != tmkGrid.Id)
                    {
                        listaTmkGrid.Add(tmkGrid);
                        numQtd += 1;
                    }


                    numPedido = tmkGrid.Id.ToInt();


                }
                gcAtendimentos.DataSource = listaTmkGrid;
                gcAtendimentos.RefreshDataSource();

                txtQtdePedidos.Text = numQtd.ToString();
                _listaTmkGrid = listaTmkGrid;


              



                this.Cursor = Cursors.Default;
            }
            this.Cursor = Cursors.Default;
        }
        private void Pesquisecliente()
        {
            this.Cursor = Cursors.WaitCursor;

            string conexoesStringII = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoesII.json");

            ConexoesJsonII conexoesII = JsonConvert.DeserializeObject<ConexoesJsonII>(conexoesStringII);

            var item = conexoesII.ConexoesII[IndiceBancoDados];
            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionStringII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
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

            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionStringII))
            {

                conn.Open();
              
                string sqlWhere = "  pes_id = " + txtIdCliente.Text;
               

                var sql = "select  pes_id as IdCliente, pes_razao as Cliente" +
                            " FROM  pessoas " +

                    " WHERE " + sqlWhere   ;



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
               // MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();

                while (returnValue.Read())
                {
                    txtNomeCliente.Text = returnValue["Cliente"].ToString();
                }

                this.Cursor = Cursors.Default;
            }
            this.Cursor = Cursors.Default;
        }
        private void Limpar()
        {
            this.Cursor = Cursors.WaitCursor;
            string DataLimpar = "01/01/1900";
            string DataIni = DataLimpar.Substring(6, 4) + "-" + DataLimpar.Substring(3, 2) + "-" + DataLimpar.Substring(0, 2);
            string DataF = DataLimpar.Substring(6, 4) + "-" + DataLimpar.Substring(3, 2) + "-" + DataLimpar.Substring(0, 2);

           
            string conexoesStringII = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoesII.json");

            ConexoesJsonII conexoesII = JsonConvert.DeserializeObject<ConexoesJsonII>(conexoesStringII);

            var item = conexoesII.ConexoesII[IndiceBancoDados];
            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionStringII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
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



            Pessoa cliente = !string.IsNullOrEmpty(txtIdCliente.Text) ? new Pessoa { Id = txtIdCliente.Text.ToInt() } : null;

            EnumStatusAtendimento? statusTmk = (EnumStatusAtendimento?)cboStatusAtendimento.EditValue;

            int marcaId = cboMarcas.EditValue.ToInt();

            ServicoTmk servicoTmk = new ServicoTmk();

            using (var conn = new MySqlConnection(ConectionStringII))
            {
                MySqlCommand command = new MySqlCommand("ConsultaRefiltek", conn);
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("varDataInicial", DataIni);
                command.Parameters.AddWithValue("varDataFinal", DataF);

                conn.Open();
                command.ExecuteNonQuery();

                var returnValue = command.ExecuteReader();

                double variavel = 0;

                List<TmkGrid> listaTmkGrid = new List<TmkGrid>();
                while (returnValue.Read())
                {

                    TmkGrid tmkGrid = new TmkGrid();

                    tmkGrid.Id = returnValue["NumPedido"].ToInt();

                    tmkGrid.DataCompra = string.Format(returnValue["DataCompra"].ToString(), "dd/MM/yyyy".ToString());

                    tmkGrid.CodigoCliente = returnValue["ClienteId"].ToInt() != 0 ? returnValue["ClienteId"].ToString() : null;

                    tmkGrid.Cliente = returnValue["pes_razao"].ToString() != "" ? returnValue["pes_razao"].ToString() : null;
                    tmkGrid.Status = returnValue["status"].ToInt() != 0 ? returnValue["status"].ToString() : null;


                    tmkGrid.Status = statusTmk.ToString();
                    //tmkGrid.Status = statusTmk.ToString();
                    tmkGrid.NumPedidoNovo = returnValue["NumPedidoNovo"].ToInt();


                    if (returnValue["status"].ToString() != null)
                    {
                        if (returnValue["status"].ToInt() == 3)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleGreen;
                            tmkGrid.Status = "DISPONIVEL";
                        }
                        else if (returnValue["status"].ToInt() == 1)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleRed16x16;
                            tmkGrid.Status = "EM ATENDIMENTO";
                        }
                        else if (returnValue["status"].ToInt() == 2)
                        {
                            tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                            tmkGrid.Status = "FINALIZADO";
                        }

                        else
                        {
                            tmkGrid.Cor = Properties.Resources.CircleGreen;
                            tmkGrid.Status = "DISPONIVEL";
                        }
                    }


                    listaTmkGrid.Add(tmkGrid);


                }
                gcAtendimentos.DataSource = listaTmkGrid;
                gcAtendimentos.RefreshDataSource();


                _listaTmkGrid = listaTmkGrid;
                
            }
            txtQtdePedidos.Text = "0";
            txtIdCliente.Text = "";
            txtNomeCliente.Text = "";


            this.Cursor = Cursors.Default;
        }


        private void PreenchaCboStatusAtendimento()
        {
          

            ObjetoParaComboBox objetoCombo2020 = new ObjetoParaComboBox();
            objetoCombo2020.Valor = "1";
            objetoCombo2020.Descricao = "DISPONIVEL";

            ObjetoParaComboBox objetoCombo2021 = new ObjetoParaComboBox();
            objetoCombo2021.Valor = "5";
            objetoCombo2021.Descricao = "AGENDADO";

            ObjetoParaComboBox objetoCombo2022 = new ObjetoParaComboBox();
            objetoCombo2022.Valor = "2";
            objetoCombo2022.Descricao = "CONCLUIDO";

            ObjetoParaComboBox objetoCombo2023 = new ObjetoParaComboBox();
            objetoCombo2023.Valor = "3";
            objetoCombo2023.Descricao = "EM ATENDIMENTO";

            ObjetoParaComboBox objetoCombo2024 = new ObjetoParaComboBox();
            objetoCombo2024.Valor = "4";
            objetoCombo2024.Descricao = "CANCELADO";



            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Insert(0, null);
            listaDeItensParaOComboBox.Add(objetoCombo2020);
            listaDeItensParaOComboBox.Add(objetoCombo2021);
            listaDeItensParaOComboBox.Add(objetoCombo2022);
            listaDeItensParaOComboBox.Add(objetoCombo2023);
            listaDeItensParaOComboBox.Add(objetoCombo2024);

            cboStatusAtendimento.Properties.DisplayMember = "Descricao";
            cboStatusAtendimento.Properties.ValueMember = "Valor";
            cboStatusAtendimento.Properties.DataSource = listaDeItensParaOComboBox;

            //cboAno.Properties.DataSource = listaDeItensParaOComboBox;
            //cboAno.Properties.ValueMember = "Valor";
            //cboAno.Properties.DisplayMember = "Descricao";
            //var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusAtendimento>();

            ////lista.RemoveAt(4);

            //lista.Insert(0, null);

            //cboStatusAtendimento.Properties.DisplayMember = "Descricao";
            //cboStatusAtendimento.Properties.ValueMember = "Valor";
            //cboStatusAtendimento.Properties.DataSource = lista;
        }

        private void PreenchaCboPeriodoPreDeterminado()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumPeriodoPreDeterminado>();

            lista.Insert(0, null);

            cboPeriodoPreDeterminado.Properties.DisplayMember = "Descricao";
            cboPeriodoPreDeterminado.Properties.ValueMember = "Valor";
            cboPeriodoPreDeterminado.Properties.DataSource = lista;
        }

        private void PreenchaCliente(Pessoa cliente, bool exibirMensagemDeNaoEncontrado = false)
        {

            string conexoesStringII = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoesII.json");

            ConexoesJsonII conexoesII = JsonConvert.DeserializeObject<ConexoesJsonII>(conexoesStringII);

            var item = conexoesII.ConexoesII[IndiceBancoDados];
            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionStringII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
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

            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionStringII))
            {

                conn.Open();

                string sqlWhere = "  pes_id = " + cliente.Id.ToString();

                var sql = "select  * " +
                            " FROM  pessoas " +
                    " WHERE " + sqlWhere;

                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                var returnValue = MyCommand.ExecuteReader();

                while (returnValue.Read())
                {
                    txtIdCliente.Text = cliente.Id.ToString();
                    txtNomeCliente.Text = returnValue["pes_razao"].ToString();
                }

            }



            //if (cliente != null)
            //{
            //    txtIdCliente.Text = cliente.Id.ToString();
            //    txtNomeCliente.Text = cliente.DadosGerais.Razao;
            //}
            //else
            //{
            //    if (exibirMensagemDeNaoEncontrado)
            //    {
            //        MessageBox.Show("Cliente nao encontrado!", "Cliente não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtIdCliente.Focus();
            //    }

            //    txtIdCliente.Text = string.Empty;
            //    txtNomeCliente.Text = string.Empty;
            //}
        }

        private void PreenchaGrid()
        {
            List<TmkGrid> listaTmkGrid = new List<TmkGrid>();

            foreach (var itemTmk in _listaTmk)
            {
                TmkGrid tmkGrid = new TmkGrid();

                tmkGrid.Id = itemTmk.NumPedido.ToInt();

                tmkGrid.DataCompra = itemTmk.DataCompra.ToString("dd/MM/yyyy");

                tmkGrid.CodigoCliente = itemTmk.ClienteId != 0 ? itemTmk.ClienteId.ToString() : null;

                tmkGrid.Cliente = itemTmk.DescricaoCliente;
                tmkGrid.Status = itemTmk.status != 0 ? itemTmk.status.ToString() : null;
                EnumStatusAtendimento? statusTmk = (EnumStatusAtendimento?)itemTmk.status;
                tmkGrid.Status = statusTmk.ToString();
                tmkGrid.NumPedidoNovo = itemTmk.NumPedidoNovo.ToInt();


                if (tmkGrid.Status != null)
                {
                    if (tmkGrid.Status == EnumStatusAtendimento.EMATENDIMENTO.Descricao())
                    {
                        tmkGrid.Cor = Properties.Resources.BolaCinza16x16;
                    }
                    else if (tmkGrid.Status == EnumStatusAtendimento.AGENDADO.Descricao())
                    {
                        tmkGrid.Cor = Properties.Resources.CircleYellow;
                    }
                    else if (tmkGrid.Status == EnumStatusAtendimento.CONCLUIDO.Descricao())
                    {
                        tmkGrid.Cor = Properties.Resources.CircleRed16x16;
                    }
                    else if (tmkGrid.Status == EnumStatusAtendimento.DISPONIVEL.Descricao())
                    {
                        tmkGrid.Cor = Properties.Resources.CircleGreen;
                    }
                    else if (tmkGrid.Status == EnumStatusAtendimento.CANCELADO.Descricao())
                    {
                        tmkGrid.Cor = Properties.Resources.circle_black;
                    }
                    else
                    {
                        tmkGrid.Cor = Properties.Resources.CircleGreen;
                    }
                }





                listaTmkGrid.Add(tmkGrid);
            }

           

            gcAtendimentos.DataSource = listaTmkGrid;
            gcAtendimentos.RefreshDataSource();

            
        }

        private void gcAtendimentos_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "ValueColumn1")
            {
                var displayText = view.GetRowCellDisplayText(e.RowHandle, e.Column);
                var cellValue = e.CellValue.ToString();
                if (displayText != cellValue)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void Selecione()
        {
            if (_listaTmk.Count > 0)
            {
                var tmk = _listaTmk.Find(x => x.NumPedido == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

                FormAtendimentoRefiltek formAtender = new FormAtendimentoRefiltek(tmk.NumPedido.ToInt(), tmk.DataCompra, tmk.ClienteId.ToString(), tmk.status.ToString(), tmk.status.ToInt());
                formAtender.Show();
            }
            else
            {
                if (_listaTmkGrid != null)
                {
                    var tmk = _listaTmkGrid.Find(x => x.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

                    DateTime dt = tmk.DataCompra.ToDate();
                    FormAtendimentoRefiltek formAtender = new FormAtendimentoRefiltek(tmk.Id.ToInt(), dt, tmk.Cliente.ToString(),tmk.Status, 5);
                    formAtender.ShowDialog();

                    var tmkretorno = _listaTmkGrid.Find(x => x.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

                    carregaconexaoII();
                    using (var conn = new MySqlConnection(ConectionStringII))
                    {
                        conn.Open();
                        string sqlWhere = "  hisat_pedido_id = " + tmkretorno.Id.ToInt() + " order by hisat_contador desc, agendamentocontato.id desc LIMIT 1";
                        string innerJoin = " ";
                        innerJoin = innerJoin + " left join agendamentocontato ON hisat_pedido_id = Pedido  ";
                        var sql = " select hisat_status, Data, hisat_contador, hisat_novo_pedido_id, hisat_descricao_historico " +
                        " FROM  historicosatendimento " +
                            innerJoin +
                            " WHERE " + sqlWhere;
                        MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                        MySqlDataReader MyReader2;

                        var returnValue = MyCommand.ExecuteReader();
                        while (returnValue.Read())
                        {
                            if (returnValue["hisat_descricao_historico"].ToString() == "Troca de Carteira")
                            {
                                PesquiseCarteira(cboCarteiras.EditValue.ToInt());
                                return;
                            }
                            if (returnValue["hisat_status"].ToInt() == 0)
                            {
                                tmk.Cor = Properties.Resources.CircleGreen;
                                tmk.Status = "DISPONIVEL";
                            }

                            else if (returnValue["hisat_status"].ToInt() == 2)
                            {
                                tmk.Cor = Properties.Resources.CircleYellow;
                                tmk.Status = "AGENDADO";
                            }
                            else if (returnValue["hisat_status"].ToInt() == 1)
                            {
                                tmk.Cor = Properties.Resources.circle_Blue16x16;
                                tmk.Status = "CONCLUIDO";
                            }
                            else if (returnValue["hisat_status"].ToInt() == 3)
                            {
                                if (tmk.NumPedidoNovo != 0)
                                {
                                    tmk.Cor = Properties.Resources.circle_Blue16x16;
                                    tmk.Status = "CONCLUIDO";
                                }
                                else
                                {
                                    tmk.Cor = Properties.Resources.CircleRed16x16;
                                    tmk.Status = "EM ATENDIMENTO";
                                }

                            }
                            else if (returnValue["hisat_status"].ToInt() == 4)
                            {
                                tmk.Cor = Properties.Resources.circle_black;
                                tmk.Status = "CANCELADO";
                            }
                            else
                            {
                                tmk.Cor = Properties.Resources.CircleGreen;
                                tmk.Status = "DISPONIVEL";
                            }
                            var dta = DateTime.Parse(returnValue["Data"].ToString()).ToString("dd-MM-yyyy");

                            if (returnValue["Data"].ToString() != string.Empty)
                            {
                                if (dta != "01-01-1900")
                                {
                                    tmk.Agendamento = DateTime.Parse(returnValue["Data"].ToString()).ToString("dd-MM-yyyy");
                                    tmk.Cor = Properties.Resources.CircleYellow;
                                    tmk.Status = "AGENDADO";
                                }

                            }
                            if (returnValue["hisat_novo_pedido_id"].ToInt() != 0)
                            {
                                tmk.NumPedidoNovo = returnValue["hisat_novo_pedido_id"].ToInt();
                                tmk.Cor = Properties.Resources.circle_Blue16x16;
                                tmk.Status = "CONCLUIDO";
                            }


                        }
                    }
                }
            }
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class TmkGrid
        {
            public int Id { get; set; }

            public string DataCompra { get; set; }

            public string CodigoCliente { get; set; }

            public string Cliente { get; set; }                      

            public string Status { get; set; }
            public int  NumPedidoNovo { get; set; }

            public Image Cor { get; set; }

            public double ValorTotal { get; set; }
            public string DataPedido { get; set; }
            public string Agendamento { get; set; }
        }
        private class CarteiraCbo
        {
            public int ID { get; set; }
            public string Carteira { get; set; }


        }
        private class VendedorCbo
        {
            public int ID { get; set; }
            public string Vendedor { get; set; }


        }

        #endregion

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            //Vai chamar o FormAtendimento para fazer o atendimento selecionado

            var tmk = _listaTmk.Find(x => x.NumPedido == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

            FormAtendimento formAtender = new FormAtendimento(tmk.NumPedido.ToInt(), tmk.DataCompra,"",0,0,true);

            formAtender.Show();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void cboStatusAtendimento_EditValueChanged(object sender, EventArgs e)
        {
           
             
           
        }

        private void txtIdCliente_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnpesquisacarteiras_Click(object sender, EventArgs e)
        {
            if (cboCarteiras.EditValue != null)
            {
                PesquiseCarteira(cboCarteiras.EditValue.ToInt());
            }
        }
        private void PesquiseCarteira(int idCarteira)
        {

            this.Cursor = Cursors.WaitCursor;


            carregaconexaoII();


            ServicoTmk servicoTmk = new ServicoTmk();

            using (var conn = new MySqlConnection(ConectionStringII))
            {
                conn.Open();

                string sqlWhere = "  pedidosvendas.pedido_status <> 3 ";
                string innerJoin = " ";


                innerJoin = innerJoin + " left join historicosatendimento on pedidosvendas.pedido_id = historicosatendimento.hisat_pedido_id ";
                innerJoin = innerJoin + " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id ";
                innerJoin = innerJoin + " left join agendamentocontato on pedidosvendas.pedido_id = agendamentocontato.pedido ";
                sqlWhere = sqlWhere + " AND pedidosvendas.PEDIDO_CARTEIRA = " + "'" + idCarteira + "'";


                var sql = " select pedidosvendas.pedido_data_elaboracao as DataCompra, " +
                        " pedidosvendas.pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId, " +
                        " historicosatendimento.hisat_status as status,  pessoas.pes_razao, agendamentocontato.Data as Agendamento, " +
                        " historicosatendimento.hisat_novo_pedido_id PedidoNovo " +
                " FROM  pedidosvendas " +

                    innerJoin +

                   " WHERE " + sqlWhere + " order by  NumPedido , agendamentocontato.Id DESC, hisat_contador  DESC";



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                int numPedido = 0;
                List<TmkGrid> listaTmkGrid = new List<TmkGrid>();
                contador = 0;

                while (returnValue.Read())
                {

                    TmkGrid tmkGrid = new TmkGrid();

                    tmkGrid.Id = returnValue["NumPedido"].ToInt();

                    var dt = DateTime.Parse(returnValue["DataCompra"].ToString()).ToString("dd-MM-yyyy");

                    tmkGrid.DataCompra = dt;
                    if (returnValue["Agendamento"].ToString() != string.Empty)
                    {
                        var dta = DateTime.Parse(returnValue["Agendamento"].ToString()).ToString("dd-MM-yyyy");
                        if (dta != "01-01-1900")
                        {
                            tmkGrid.Agendamento = dta;
                        }

                    }


                    tmkGrid.CodigoCliente = returnValue["ClienteId"].ToInt() != 0 ? returnValue["ClienteId"].ToString() : null;

                    tmkGrid.Cliente = returnValue["pes_razao"].ToString() != "" ? returnValue["pes_razao"].ToString() : null;
                    tmkGrid.NumPedidoNovo = returnValue["PedidoNovo"].ToInt() != 0 ? returnValue["PedidoNovo"].ToInt() : 0;

                    tmkGrid.Status = returnValue["status"].ToInt() != 0 ? returnValue["status"].ToString() : null;
                    int intStatus = returnValue["status"].ToInt();

                   
                    tmkGrid.Status = intStatus != 0 ? tmkGrid.Status.ToString() : null;
                    EnumStatusAtendimento? statusTmk = (EnumStatusAtendimento?)intStatus;
                    tmkGrid.Status = statusTmk.ToString();


                    if (tmkGrid.Status != null)
                    {
                        if (intStatus == 0)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleGreen;
                            tmkGrid.Status = "DISPONIVEL";
                        }

                        else if (intStatus == 1)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleYellow;
                            tmkGrid.Status = "AGENDADO";
                        }
                        else if (intStatus == 2)
                        {
                            tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                            tmkGrid.Status = "CONCLUIDO";
                        }
                        else if (intStatus == 3)
                        {
                            if (tmkGrid.NumPedidoNovo != 0)
                            {
                                tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                                tmkGrid.Status = "CONCLUIDO";
                            }
                            else
                            {
                                tmkGrid.Cor = Properties.Resources.CircleRed16x16;
                                tmkGrid.Status = "EM ATENDIMENTO";
                            }

                        }
                        else if (intStatus == 4)
                        {
                            tmkGrid.Cor = Properties.Resources.circle_black;
                            tmkGrid.Status = "CANCELADO";
                        }
                        else
                        {
                            tmkGrid.Cor = Properties.Resources.CircleGreen;
                            tmkGrid.Status = "DISPONIVEL";
                        }
                    }
                    if (tmkGrid.Agendamento != null)
                    {
                        tmkGrid.Cor = Properties.Resources.CircleYellow;
                        tmkGrid.Status = "AGENDADO";
                    }
                    if (tmkGrid.NumPedidoNovo != 0)
                    {
                        tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                        tmkGrid.Status = "CONCLUIDO";
                    }

                    if (numPedido != tmkGrid.Id)
                    {
                        listaTmkGrid.Add(tmkGrid);
                        contador += 1;
                    }

                    numPedido = tmkGrid.Id.ToInt();
                }
                gcAtendimentos.DataSource = listaTmkGrid;
                gcAtendimentos.RefreshDataSource();


                _listaTmkGrid = listaTmkGrid;

                txtQtdePedidos.Text = contador.ToString();



                //VerificaContadorII(idCarteira);

                VerificaVendedor(idCarteira);




            }

            _listaTmk = new List<Tmk>();


            this.Cursor = Cursors.Default;
        }

        private void carregaconexaoII()
        {
            string conexoesStringII = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoesII.json");

            ConexoesJsonII conexoesII = JsonConvert.DeserializeObject<ConexoesJsonII>(conexoesStringII);

            var item = conexoesII.ConexoesII[IndiceBancoDados];
            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionStringII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
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
        private void VerificaVendedor(int IdCarteira)
        {
           

            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionStringII))
            {

                conn.Open();
                var sql = " SELECT pessoas.pes_razao as Nome FROM carteiras " +
                            " Inner join Pessoas ON carteiras.Vendedor = pessoas.pes_id " +
                            " Where id = " + IdCarteira;

                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    cboVendedores.EditValue = returnValue["Nome"].ToString();
                }

            }

        }
    }
}
