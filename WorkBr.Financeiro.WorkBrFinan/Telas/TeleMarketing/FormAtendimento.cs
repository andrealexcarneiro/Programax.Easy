using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using System.Linq;
using Programax.Easy.View.Telas.Vendas.VendaRapida;
using Programax.Easy.Servico.Telemarketing.TmkServ;
using Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.TeleMarketing.Enumeradores;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;
using System.Data;
using MySql.Data.MySqlClient;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Vendas.RoteiroServ;

namespace Programax.Easy.View.Telas.TeleMarketing
{
    public partial class FormAtendimento : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Pessoa _pessoaCadastro;
        private List<HistoricoAtendimento> _listaDeHistoricos;
        private EnumStatusAtendimento? _statusAnterior;
        private bool _estahSaindoAoConcluir=false;
        private bool _foiFechado = false;
        private int _idCliente;
        private DateTime _inicioAtendimento;
        private bool _ehHistorico;
        private int numChamadas = 0;
        private string ConectionString;
        private DataSet mDataSet;
        private MySqlDataAdapter mAdapter;
        private Boolean altera = false;
        private int status = 0;
        private int VendedorCarteira = 0;
        private bool gerente = false;
        private List<PedidoDeVenda> _listaTmkGridII;
        private Parametros _parametros;

        private int IdVendedor;
        #endregion

        #region " CONSTRUTOR "

        public FormAtendimento(int NumeroPedido, DateTime DataCompra, string strStatus, int intStatus,  int intVendedorCarteira, bool ehHistorico = false)
        {
            InitializeComponent();
            

            if (strStatus.ToString() == "DISPONIVEL")
                {
                    status = 0;
                }
                if (strStatus.ToString() == "CONCLUIDO")
                {
                    status = 1;
                }
                if (strStatus.ToString() == "AGENDADO")
                {
                    status = 2;
                }
                if (strStatus.ToString() == "EMATENDIMENTO")
                {
                    status = 3;
                }
                if (strStatus.ToString() == "CANCELADO")
                {
                    status = 4;
                }
                if (intStatus != 5  )
                {
                    status = intStatus;
                }

            VendedorCarteira = intVendedorCarteira;

            _listaDeHistoricos = new List<HistoricoAtendimento>();
            Libere();
            InformeUsuarioContasAPagarReceber(Sessao.PessoaLogada);

            _ehHistorico = ehHistorico;
            consultaVendedor(NumeroPedido);
            //CarregarInformacoesAtendimento(NumeroPedido, DataCompra);
            CarregarInformacoesAtendimentoII(NumeroPedido, DataCompra);
            mostraContador();

            CarregaVendas();

            PreenchaCboCarteiras();
            ServicoParametros servicoParametros = new ServicoParametros();
            _parametros = servicoParametros.ConsulteParametros();

            this.ActiveControl = txtIdPessoa;
        }

        #endregion
        #region "BUSCACONTADOR"
        private void mostraContador()
        {
            txtNrChamadas.Text = string.Empty;
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
            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();
                string Sql = "Select hisat_contador from historicosatendimento Where hisat_pedido_id = " + txtNumeroPedidoVendas.Text + " order by hisat_contador desc limit 1";

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;

                txtNrChamadas.Text = string.Empty;

                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    txtNrChamadas.Text = returnValue["hisat_contador"].ToString();

                }

            }



        }
        private void consultaVendedor(int numeropedido)
        {
            txtNrChamadas.Text = string.Empty;
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
            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionString))
            {
                ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();
                var pedido = servicoPedido.Consulte(numeropedido);
                conn.Open();
                string Sql = "Select Vendedor from Carteiras Where Id = " + pedido.Carteira.ToInt() ;

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;

                txtNrChamadas.Text = string.Empty;

                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    IdVendedor = returnValue["Vendedor"].ToInt();

                }

            }



        }
        #endregion
        #region " EVENTOS CONTROLES "
        private void CarregaVendas()
        {

            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var cliente = servicoPessoa.ConsultePessoaAtiva(txtIdPessoa.Text.ToInt());

            // PreenchaCliente(cliente, true);

            DateTime dataInicial = new DateTime(2000, 01, 01);
            DateTime dataFinal = DateTime.Now;

            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
            PedidoDeVendaCliente pedidoDeVendaRelatorio = new PedidoDeVendaCliente();

            List<VWVenda> listaVWVendaII = servicoPedidoDeVenda.ConsulteListaVWVendasPorCliente(cliente,
                                                                                                                                                null,
                                                                                                                                           null,
                                                                                                                                                false,
                                                                                                                                                dataInicial,
                                                                                                                                                dataFinal,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                true,
                                                                                                                                                true,
                                                                                                                                                0,
                                                                                                                                                null);





            lblCarteiracom.Visible = false;
            lblUsuarioCarteira.Visible = false;

            List<PedidoDeVenda> movimentacaoItensGrid = new List<PedidoDeVenda>();
            foreach (var listasvendasII in listaVWVendaII)
            {
                PedidoDeVenda vendas = new PedidoDeVenda();


                vendas.Id = listasvendasII.Id;
                vendas.DataElaboracao = listasvendasII.DataElaboracao;
                vendas.ValorTotalII = "R$" + listasvendasII.ValorTotal.ToString("0.00");
                VerificaCarteira();
                //if (listasvendasII.VendedorId != 0)
                //{
                //    vendas.VendedorNome = listasvendasII.VendedorNome.ToString();

                //}
                //if (listasvendasII.VendedorId != 0)
                //{
                //    if (listasvendasII.VendedorId.ToInt() != Sessao.PessoaLogada.Id)
                //    {
                //        ServicoPessoa servicopessoavendedor = new ServicoPessoa();
                //        var pessoavendedor = servicopessoavendedor.Consulte(IdVendedor);
                //        lblUsuarioCarteira.Text = listasvendasII.VendedorNome;
                //        lblCarteiracom.Visible = true;
                //        lblUsuarioCarteira.Visible = true;
                //    }
                //}


                movimentacaoItensGrid.Add(vendas);
            }

            gcAtendimentos.DataSource = movimentacaoItensGrid;
            gcAtendimentos.RefreshDataSource();
            _listaTmkGridII = movimentacaoItensGrid;
        }
        public void VerificaCarteira()
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
                    StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }


            }

            lblCarteiracom.Visible = false;
            lblUsuarioCarteira.Visible = false;

            using (var conn = new MySqlConnection(ConectionString))
            {
                conn.Open();

                var sql = " SELECT PEDIDO_CARTEIRA, carteiras.NomeCarteira FROM pedidosvendas " +
                            " Inner join carteiras ON " +
                            " pedidosvendas.PEDIDO_CARTEIRA = carteiras.id " +
                            " Where pedido_cliente_id= " + txtIdPessoa.Text + " And PEDIDO_CARTEIRA <> " + VendedorCarteira;

                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();

                while (returnValue.Read())
                {
                    lblUsuarioCarteira.Text = returnValue["NomeCarteira"].ToString();
                    lblCarteiracom.Visible = true;
                    lblUsuarioCarteira.Visible = true;
                }
            }
        }
        public class PedidoDeVendaCliente
        {

        }
        private void FormAtendimento_Closed(object sender, System.EventArgs e)
        {
            if (!_ehHistorico)
            {
                if (txtPedidoGerado.Text != string.Empty && _statusAnterior != EnumStatusAtendimento.CONCLUIDO)
                    //if ( _statusAnterior != EnumStatusAtendimento.CONCLUIDO)
                    {
                    if (_estahSaindoAoConcluir) return;

                    MessageBox.Show("Você não pode fechar este Atendimento. Porque gerou pedido para o mesmo! " +
                        "O ATENDIMENTO SERÁ CONCLUÍDO!", "Postergar o Atendimento!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtHistorico.Text = txtHistorico.Text + "Foi concluído automaticamente, porque gerou pedido para o mesmo. " +
                        "O USUARIO TENTOU FECHAR SEM CONCLUIR.";

                    _foiFechado = true;

                    Concluir();

                    return;
                }

                if (!_estahSaindoAoConcluir)
                {
                    ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

                    var pedido = servicoPedido.Consulte(txtNumeroPedidoVendas.Text.ToInt());

                    pedido.StatusAtendimento = ((EnumStatusAtendimento)_statusAnterior);

                    //servicoPedido.Atualize(pedido);
                }
            }   
        }
       
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Concluir();
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
        private void Concluir()
        {
            //if (txtPedidoGerado.Text == string.Empty)
            //{
            //    MessageBox.Show("Para Concluir o Atendimento. Você precisa Gerar um novo pedido!", "Conclusão do Atendimento!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            //    return;
            //}

            if (txtHistorico.Text == string.Empty)
            {
                MessageBox.Show("Para Concluir o Atendimento. Você precisa informar a Descrição do Histório!", "Conclusão do Atendimento!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            if (txtNrChamadas.Text.ToInt() == 10)
            {
                MessageBox.Show("Número de chamadas foi excedido para este cliente.", "Conclusão do Atendimento!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }



            Action actionSalvar = () =>
            {
                Boolean concluso = true;

                ServicoHistoricoAtendimento servicoRoteiro = new ServicoHistoricoAtendimento();
                if (txtPedidoGerado.Text.ToString() == string.Empty)
                {
                    concluso = false;
                }

                var historico = busqueHistoricoAtendimento(concluso);
                servicoRoteiro.ConcluaPostergueAtendimentoEAtualizePedido(true, historico);



            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
            SalvarAgendamento();
            _estahSaindoAoConcluir = true;

            if(!_foiFechado)
                this.Close();
        }
        private void SalvarAgendamento()
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
                    StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }


            }

            int intContador = 0;


            string hisat_descricao_historico = txtHistorico.Text.ToString();
            DateTime hisat_data_historico = DateTime.Now;
            string hisat_tempo_duracao = string.Empty;
            int hisat_pes_usuario_id = 1;
            int hisat_pedido_id = txtNumeroPedidoVendas.Text.ToInt();
            int hisat_novo_pedido_id = 0;
            hisat_novo_pedido_id = txtPedidoGerado.Text.ToInt();
            int hisat_status = 0;

            var data = DateTime.Parse(hisat_data_historico.ToString()).ToString("yyyy-MM-dd HH:mm:ss");

            using (var conn = new MySqlConnection(ConectionString))
            {
                conn.Open();

                string sqlWhere = "  Pedido = " + txtNumeroPedidoVendas.Text;
                var sql = " select contador From agendamentocontato" +
                    " WHERE " + sqlWhere + " order by  contador Desc";

                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();

                while (returnValue.Read())
                {
                    intContador += returnValue["contador"].ToInt();
                }
            }


            int Pedido = txtNumeroPedidoVendas.Text.ToInt();
            DateTime? dataAgendamento = txtAgendamento.Text.ToDateNullabel();

            var dtAgendamento = "";

           

            if (dataAgendamento != null)
            {
                dtAgendamento = DateTime.Parse(dataAgendamento.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                dtAgendamento = "1900-01-01 00:00:00";
            }


            int Contador = intContador;


            {
                using (var conn = new MySqlConnection(ConectionString))

                {
                    conn.Open();

                    MySqlCommand command = new MySqlCommand("INSERT INTO agendamentocontato (Pedido,Data,contador)" +
                    "VALUES(" + Pedido + ", '" + dtAgendamento + "', " + Contador + ")", conn);

                    command.ExecuteNonQuery();
                    conn.Close();


                }
               

            }
        }
        private void btnPostergar_Click(object sender, EventArgs e)
        {
            if (txtPedidoGerado.Text != string.Empty)
            {
                MessageBox.Show("Você não pode postergar este Atendimento. Porque gerou pedido para o mesmo!", "Postergar o Atendimento!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            if (txtHistorico.Text == string.Empty)
            {
                MessageBox.Show("Para Postegar o Atendimento. Você precisa informar a Descrição do Histório!", "Postergar o Atendimento!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            Action actionSalvar = () =>
            {
                ServicoHistoricoAtendimento servicoRoteiro = new ServicoHistoricoAtendimento();

                var historico = busqueHistoricoAtendimento(false);

                servicoRoteiro.ConcluaPostergueAtendimentoEAtualizePedido(false, historico);

            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);

            _estahSaindoAoConcluir = true;
            this.Close();
        }

        private HistoricoAtendimento busqueHistoricoAtendimento(bool ehConclusao)
        {
            HistoricoAtendimento historico = new HistoricoAtendimento();

            historico.Pedido = new PedidoDeVenda { Id = txtNumeroPedidoVendas.Text.ToInt() };

            historico.NovoPedido = new PedidoDeVenda { Id = txtPedidoGerado.Text.ToInt() };

            historico.Usuario = Sessao.PessoaLogada;           
            historico.DescricaoHistorico = txtHistorico.Text;
            historico.DataHistorico = DateTime.Now;
            historico.codCliente = txtIdPessoa.Text.ToInt();
            numChamadas += 1;
            historico.contador = numChamadas;
            //if (txtAgendamento.Text != "")
            //{
            //    historico.d
            //}

            historico.TempoDuracao = (DateTime.Now - _inicioAtendimento).ToString();

            //if (chkPosVenda.Checked)
            if (txtPedidoGerado.Text.ToInt() != 0 )
            {
                if (_parametros.ParametrosVenda.StatusFaturado != true)
                {
                    historico.Status = EnumStatusAtendimento.CONCLUIDO;
                }
                else
                {

                    var roteiro = new ServicoRoteiro().ConsultePorPedido(txtPedidoGerado.Text.ToInt());

                    if (roteiro != null)
                    {
                        historico.Status = EnumStatusAtendimento.AGENDADO;
                    }
                    else
                    {
                        historico.Status = EnumStatusAtendimento.EMATENDIMENTO;
                    }
                    
                        
                }
                
            }
            else if (status == 4)
            {
                historico.Status = EnumStatusAtendimento.CANCELADO;
            }
            else
            {
                if (_parametros.ParametrosVenda.StatusFaturado == true)
                {
                    historico.Status = EnumStatusAtendimento.EMATENDIMENTO;
                }
                else
                { 
                    
                    if (txtAgendamento.Text != "")
                    {
                        historico.Status = EnumStatusAtendimento.AGENDADO;
                    }
                    else
                    {
                        historico.Status = EnumStatusAtendimento.EMATENDIMENTO;
                    }
                }
                
              
            }
               
            //else if (ehConclusao)
            //    historico.Status = EnumStatusAtendimento.FINALIZADO;
            //else
            //    historico.Status = EnumStatusAtendimento.EMATENDIMENTO;                
            
            return historico;
        }
        private HistoricoAtendimento busqueHistoricoAtendimentocancelado(bool ehConclusao)
        {
            HistoricoAtendimento historico = new HistoricoAtendimento();

            historico.Pedido = new PedidoDeVenda { Id = txtNumeroPedidoVendas.Text.ToInt() };

            historico.NovoPedido = new PedidoDeVenda { Id = txtPedidoGerado.Text.ToInt() };

            historico.Usuario = Sessao.PessoaLogada;
            historico.DescricaoHistorico = txtHistorico.Text;
            historico.DataHistorico = DateTime.Now;
            numChamadas += 1;
            historico.contador = numChamadas;

            historico.TempoDuracao = (DateTime.Now - _inicioAtendimento).ToString();
            historico.Status = EnumStatusAtendimento.CANCELADO;
            

            return historico;
        }

        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void txtIdPessoa_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdPessoa.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var vendedor = servicoPessoa.ConsultePessoaAtiva(txtIdPessoa.Text.ToInt());

                PreenchaPessoa(vendedor, true);
            }
            else
            {
                PreenchaPessoa(null);
            }
        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var pessoa = formPessoaPesquisa.PesquisePessoaFuncionarioAtiva();

            if (pessoa != null)
            {
                PreenchaPessoa(pessoa);
            }
        }

        private void btnPesquisaPedidoVendas_Click(object sender, EventArgs e)
        {
            FormPesquisaPedidoDeVenda formPesquisaPedidoDeVenda = new FormPesquisaPedidoDeVenda();

            var pedidoDeVenda = formPesquisaPedidoDeVenda.ExibaPesquisaDePedidosDeVenda();

            if (pedidoDeVenda != null)
            {   
                txtNumeroPedidoVendas.Text = pedidoDeVenda.Id.ToString();
            }
        }
        
        private void btnNovoPedido_Click(object sender, EventArgs e)
        {
            FormVendaRapida formVendaRapida = new FormVendaRapida(0);

            var numeroPedido = formVendaRapida.RetornePedidoVenda(_idCliente);

            if (numeroPedido != 0)
                txtPedidoGerado.Text = numeroPedido.ToString(); 
        }
                
        #endregion

        #region " MÉTODOS AUXILIARES "

        private void CarregarInformacoesAtendimento(int NumeroPedido, DateTime DataCompra)
        {
            ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();
                       
            var pedido = servicoPedido.Consulte(NumeroPedido);

           

            _statusAnterior = pedido.StatusAtendimento;
            _idCliente = pedido.Cliente.Id;

            if (!_ehHistorico)
            {
                pedido.StatusAtendimento = EnumStatusAtendimento.EMATENDIMENTO;
               // servicoPedido.Atualize(pedido);
            }
            

            txtIdPessoa.Text = pedido.Cliente.Id.ToString();
            txtNomePessoa.Text = pedido.Cliente.DadosGerais.Razao;

            txtDataCompra.Text = DataCompra.ToString("dd/MM/yyyy");

            txtCelular.Text = pedido.Cliente.ListaDeTelefones != null && pedido.Cliente.ListaDeTelefones.ToList().Exists(x => x.TipoTelefone == EnumTipoTelefone.CELULAR)?
                              pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.CELULAR).Ddd +
                              pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.CELULAR).Numero:
                              string.Empty;

            txtFoneComercial.Text = pedido.Cliente.ListaDeTelefones != null && pedido.Cliente.ListaDeTelefones.ToList().Exists(x => x.TipoTelefone == EnumTipoTelefone.COMERCIAL)?
                              pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.COMERCIAL).Ddd +
                              pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.COMERCIAL).Numero :
                              string.Empty;

            txtFoneResidencial.Text = pedido.Cliente.ListaDeTelefones != null && pedido.Cliente.ListaDeTelefones.ToList().Exists(x => x.TipoTelefone == EnumTipoTelefone.RESIDENCIAL)?
                             pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.RESIDENCIAL).Ddd +
                             pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.RESIDENCIAL).Numero :
                             string.Empty;

            txtFoneRecado.Text = pedido.Cliente.ListaDeTelefones != null && pedido.Cliente.ListaDeTelefones.ToList().Exists(x => x.TipoTelefone == EnumTipoTelefone.RECADO)?
                         pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.RECADO).Ddd +
                         pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.RECADO).Numero :
                         string.Empty;

            txtOutroFone.Text = pedido.Cliente.ListaDeTelefones != null && pedido.Cliente.ListaDeTelefones.ToList().Exists(x => x.TipoTelefone == EnumTipoTelefone.OUTROS)?
                         pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.OUTROS).Ddd +
                         pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.OUTROS).Numero :
                         string.Empty;

            txtNumeroPedidoVendas.Text = NumeroPedido.ToString();

            txtUsuario.Text = Sessao.PessoaLogada.Id.ToString() + " - " + Sessao.PessoaLogada.DadosGerais.Razao;

            _inicioAtendimento = DateTime.Now;
            txtInicioAtendimento.Text = _inicioAtendimento.ToString("HH:mm:ss");
           

            //Carrega o Grid Com os históricos
            _listaDeHistoricos = new ServicoHistoricoAtendimento().ConsulteLista(NumeroPedido);
            numChamadas = _listaDeHistoricos.Count;

            if (_listaDeHistoricos != null && _listaDeHistoricos.Count != 0)
            {
                _statusAnterior = _listaDeHistoricos.LastOrDefault().Status;

                if(_statusAnterior == EnumStatusAtendimento.CONCLUIDO)
                {
                    if (_listaDeHistoricos.LastOrDefault().NovoPedido.Id == 0)
                    {
                        btnConcluir.Enabled = true;
                        
                    }
                    else
                    {
                        btnConcluir.Enabled = false;

                        txtPedidoGerado.Text = _listaDeHistoricos.LastOrDefault().NovoPedido.Id.ToString();
                    }
            
                }               
            }

            if (_ehHistorico)
            {
                btnConcluir.Enabled = false;
                
            }

            PreenchaGridHistorico();
        }
         private void CarregarInformacoesAtendimentoII(int NumeroPedido, DateTime DataCompra)
        {
            ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();
                       
            var pedido = servicoPedido.Consulte(NumeroPedido);

           

            _statusAnterior = pedido.StatusAtendimento;
            _idCliente = pedido.Cliente.Id;

            if (!_ehHistorico)
            {
                pedido.StatusAtendimento = EnumStatusAtendimento.EMATENDIMENTO;
               // servicoPedido.Atualize(pedido);
            }
            

            txtIdPessoa.Text = pedido.Cliente.Id.ToString();
            txtNomePessoa.Text = pedido.Cliente.DadosGerais.Razao;

            txtDataCompra.Text = DataCompra.ToString("dd/MM/yyyy");

            txtCelular.Text = pedido.Cliente.ListaDeTelefones != null && pedido.Cliente.ListaDeTelefones.ToList().Exists(x => x.TipoTelefone == EnumTipoTelefone.CELULAR)?
                              pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.CELULAR).Ddd +
                              pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.CELULAR).Numero:
                              string.Empty;

            txtFoneComercial.Text = pedido.Cliente.ListaDeTelefones != null && pedido.Cliente.ListaDeTelefones.ToList().Exists(x => x.TipoTelefone == EnumTipoTelefone.COMERCIAL)?
                              pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.COMERCIAL).Ddd +
                              pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.COMERCIAL).Numero :
                              string.Empty;

            txtFoneResidencial.Text = pedido.Cliente.ListaDeTelefones != null && pedido.Cliente.ListaDeTelefones.ToList().Exists(x => x.TipoTelefone == EnumTipoTelefone.RESIDENCIAL)?
                             pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.RESIDENCIAL).Ddd +
                             pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.RESIDENCIAL).Numero :
                             string.Empty;

            txtFoneRecado.Text = pedido.Cliente.ListaDeTelefones != null && pedido.Cliente.ListaDeTelefones.ToList().Exists(x => x.TipoTelefone == EnumTipoTelefone.RECADO)?
                         pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.RECADO).Ddd +
                         pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.RECADO).Numero :
                         string.Empty;

            txtOutroFone.Text = pedido.Cliente.ListaDeTelefones != null && pedido.Cliente.ListaDeTelefones.ToList().Exists(x => x.TipoTelefone == EnumTipoTelefone.OUTROS)?
                         pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.OUTROS).Ddd +
                         pedido.Cliente.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.OUTROS).Numero :
                         string.Empty;

            txtNumeroPedidoVendas.Text = NumeroPedido.ToString();

            txtUsuario.Text = Sessao.PessoaLogada.Id.ToString() + " - " + Sessao.PessoaLogada.DadosGerais.Razao;

            _inicioAtendimento = DateTime.Now;
            txtInicioAtendimento.Text = _inicioAtendimento.ToString("HH:mm:ss");
           

            //Carrega o Grid Com os históricos
            _listaDeHistoricos = new ServicoHistoricoAtendimento().ConsulteListaCliente(_idCliente);
            numChamadas = _listaDeHistoricos.Count;

            if (_listaDeHistoricos != null && _listaDeHistoricos.Count != 0)
            {
                _statusAnterior = _listaDeHistoricos.LastOrDefault().Status;

                if(_statusAnterior == EnumStatusAtendimento.CONCLUIDO)
                {
                    if (_listaDeHistoricos.LastOrDefault().NovoPedido.Id == 0)
                    {
                        btnConcluir.Enabled = true;
                        
                    }
                    else
                    {
                        btnConcluir.Enabled = false;

                        txtPedidoGerado.Text = _listaDeHistoricos.LastOrDefault().NovoPedido.Id.ToString();
                    }
            
                }               
            }

            if (_ehHistorico)
            {
                btnConcluir.Enabled = false;
                
            }

            PreenchaGridHistorico();
        }


        protected virtual ServicoContasPagarReceber RetorneServicoContasPagarOuReceber()
        {
            return null;
        }
                
        protected virtual void PreenchaPessoa(Pessoa pessoa, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (pessoa != null)
            {
                txtIdPessoa.Text = pessoa.Id.ToString();
                txtNomePessoa.Text = pessoa.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Parceiro nao encontrado!", "Parceiro não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdPessoa.Focus();
                }

                txtIdPessoa.Text = string.Empty;
                txtNomePessoa.Text = string.Empty;
            }
        }

        protected virtual void InformeUsuarioContasAPagarReceber(Pessoa pessoaQueCadastrou)
        {
            _pessoaCadastro = pessoaQueCadastrou;
            txtUsuario.Text = pessoaQueCadastrou.Id + " - " + pessoaQueCadastrou.DadosGerais.Razao;
        }

        private void PreenchaGridHistorico()
        {
            List<HistoricoGrid> listaHistGrid = new List<HistoricoGrid>();

            foreach (var item in _listaDeHistoricos)
            {
                HistoricoGrid historicoGrid = new HistoricoGrid();
                historicoGrid.Id = item.Id.ToInt();

                historicoGrid.Atendente = item.Usuario.Id + " - " + item.Usuario.DadosGerais.Razao;

                historicoGrid.DataHistorico = item.DataHistorico.ToString();

                historicoGrid.DescricaoHistorico = item.DescricaoHistorico;

                historicoGrid.PedidoGerado = item.NovoPedido != null? item.NovoPedido.Id.ToString():string.Empty;

                historicoGrid.Status = item.Status.Descricao();

                listaHistGrid.Add(historicoGrid);
            }

            gcHistoricoRoteiro.DataSource = listaHistGrid;
            gcHistoricoRoteiro.RefreshDataSource();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class HistoricoGrid
        {
            public int Id { get; set; }
            public string Atendente { get; set; }

            public string DataHistorico { get; set; }

            public string DescricaoHistorico { get; set; }    
            
            public string PedidoGerado { get; set; }
           
            public string Status { get; set; }
        }

        #endregion
        
        private void btnEditarPedido_Click(object sender, EventArgs e)
        {
            if (txtNumeroPedidoVendas.Text != string.Empty)
            {
                FormCadastroPedidoDeVenda formCadastroPedidoDeVenda = new FormCadastroPedidoDeVenda(txtNumeroPedidoVendas.Text.ToInt());

                formCadastroPedidoDeVenda.Show();
            }
        }

        private void btnWhats_Click(object sender, EventArgs e)
        {
            string link = "https://api.whatsapp.com/send?phone=";
            link = link + "55"+txtCelular.Text;

            System.Diagnostics.Process.Start(link);
        }

        private void btnVerPedidoGerado_Click(object sender, EventArgs e)
        {
            if (txtPedidoGerado.Text != string.Empty)
            {
                FormCadastroPedidoDeVenda formCadastroPedidoDeVenda = new FormCadastroPedidoDeVenda(txtPedidoGerado.Text.ToInt());

                formCadastroPedidoDeVenda.Show();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            if (altera == true)
            {
                if (MessageBox.Show("Deseja salvar as alterações?", "Mudar Carteira", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    this.Close();
                }
                else
                {
                    SalvarCarteira();
                }
       
            }
            this.Close();
        }

        private void btnVerCadastroCliente_Click(object sender, EventArgs e)
        {
            FormCadastroPessoa formCadastroPessoa = new FormCadastroPessoa(txtIdPessoa.Text.ToInt());
            formCadastroPessoa.ShowDialog();
        }
        private void PreenchaCboCarteiras()
        {
            this.Cursor = Cursors.WaitCursor;


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



            string Sql = string.Empty;
            int Usuario = Sessao.PessoaLogada.Id;
            string Vendedor = Usuario.ToString();
            Vendedor = Vendedor + ", 0";
            using (var conn = new MySqlConnection(ConectionString))
            {
                conn.Open();
                string sql = "";

                //if (gerente == true)
                //{
                //    sql = "Select ID, NomeCarteira From carteiras order by ID";
                //}
                //else
                //{
                //    sql = "Select ID, NomeCarteira From carteiras Where Id not in (4,5) order by ID";
                //}
                if (gerente == false)
                {
                    sql = "Select ID, NomeCarteira From carteiras Where Vendedor IN (" + Vendedor + ") And Id not in (4,5) order by ID";
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
        private void cboCarteiras_EditValueChanged(object sender, EventArgs e)
        {
            if (cboCarteiras.EditValue == null)
            {
                btnSalvarCarteira.Visible = false;
                btnConcluir.Visible = true;
                altera = false;
            }
            else
            {
                btnSalvarCarteira.Visible = true;
                btnConcluir.Visible = false;
                altera = true;
            }
          
        }

        private void btnSalvarCarteira_Click(object sender, EventArgs e)
        {
            SalvarCarteira();
        }
        private void SalvarCarteira()
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
                    StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }


            }

            int intContador = 0;


            string hisat_descricao_historico = "Troca de Carteira";
            DateTime hisat_data_historico = DateTime.Now;
            string hisat_tempo_duracao = string.Empty;
            int hisat_pes_usuario_id = Sessao.PessoaLogada.Id.ToInt();
           
            int hisat_pedido_id = txtNumeroPedidoVendas.Text.ToInt();
            int hisat_novo_pedido_id = 0;
            hisat_novo_pedido_id = txtPedidoGerado.Text.ToInt();
            
            int hisat_status = 0;
            int statusPedido = 0;

            var data = DateTime.Parse(hisat_data_historico.ToString()).ToString("yyyy-MM-dd HH:mm:ss");


            using (var conn = new MySqlConnection(ConectionString))
            {
                conn.Open();

                string sqlWhere = "  hisat_pedido_id = " + txtNumeroPedidoVendas.Text;
                var sql = " select hisat_status From historicosatendimento" +
                    " WHERE " + sqlWhere + " order by  hisat_contador Desc  LIMIT 1";

                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();

                while (returnValue.Read())
                {
                    statusPedido += returnValue["hisat_status"].ToInt();
                }
            }

            if (statusPedido == 2 )
            {
                hisat_status = statusPedido;
            }
            using (var conn = new MySqlConnection(ConectionString))
            {
                conn.Open();

                string sqlWhere = "  Pedido = " + txtNumeroPedidoVendas.Text;
                var sql = " select contador From agendamentocontato" +
                    " WHERE " + sqlWhere + " order by  contador Desc";

                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();

                while (returnValue.Read())
                {
                    intContador += returnValue["contador"].ToInt();
                }
            }


            int Pedido = txtNumeroPedidoVendas.Text.ToInt();
            DateTime? dataAgendamento = txtAgendamento.Text.ToDateNullabel();

            var dtAgendamento = "1900-01-01 00:00:00";

            if (dataAgendamento != null)
            {
                dtAgendamento = DateTime.Parse(dataAgendamento.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }


            int Contador = intContador;


            {
                using (var conn = new MySqlConnection(ConectionString))

                {
                    conn.Open();

                    MySqlCommand command = new MySqlCommand("INSERT INTO agendamentocontato (Pedido,Data,contador)" +
                    "VALUES(" + Pedido + ", '" + dtAgendamento + "', " + Contador + ")", conn);

                    command.ExecuteNonQuery();
                    conn.Close();


                }
                using (var conn = new MySqlConnection(ConectionString))
                {
                        conn.Open();

                        numChamadas += 1;
                        MySqlCommand command = new MySqlCommand("INSERT INTO historicosatendimento (hisat_descricao_historico, hisat_data_historico, hisat_tempo_duracao, hisat_pes_usuario_id, " +
                                                                "hisat_pedido_id, hisat_novo_pedido_id, hisat_status, hisat_contador)" +
                                         "VALUES('" + hisat_descricao_historico + "', '" + data + "', '" + hisat_tempo_duracao + "', " + hisat_pes_usuario_id + ", " +
                                                "'" + hisat_pedido_id + "', '" + hisat_novo_pedido_id + "', '" + hisat_status + "', '" + numChamadas + "')", conn);

                        command.ExecuteNonQuery();
                        conn.Close();
                    }

                using (var conn = new MySqlConnection(ConectionString))
                {

                    conn.Open();

                    string Sql = "update pedidosvendas set PEDIDO_CARTEIRA = " + cboCarteiras.EditValue.ToInt() +
                                 " where pedido_id = " + Pedido;


                    MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                    MySqlDataReader MyReader2;


                    var returnValue = MyCommand.ExecuteReader();


                }
                MessageBox.Show("Registro Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnSalvarCarteira.Visible = false;
                altera = false;

            }
        }
        private class CarteiraCbo
        {
            public int ID { get; set; }
            public string Carteira { get; set; }


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (txtHistorico.Text == string.Empty)
            {
                MessageBox.Show("Para Concluir o Atendimento. Você precisa informar a Descrição do Histório!", "Conclusão do Atendimento!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            if (txtNrChamadas.Text.ToInt() == 10)
            {
                MessageBox.Show("Número de chamadas foi excedido para este cliente.", "Conclusão do Atendimento!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }



            Action actionSalvar = () =>
            {
                Boolean concluso = true;

                ServicoHistoricoAtendimento servicoRoteiro = new ServicoHistoricoAtendimento();
                if (txtPedidoGerado.Text.ToString() == string.Empty)
                {
                    concluso = false;
                }

                var historico = busqueHistoricoAtendimentocancelado(concluso);
                servicoRoteiro.ConcluaPostergueAtendimentoEAtualizePedido(true, historico);



            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
            AlteraPedidoStatus();
            _estahSaindoAoConcluir = true;

            if (!_foiFechado)
                this.Close();
        }
        private void AlteraPedidoStatus()
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
                    StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }


            }




            using (var conn = new MySqlConnection(ConectionString))

            {
                conn.Open();

                string Sql = "update historicosatendimento set hisat_status = " + 4 +

                            " where hisat_pedido_id = " + txtNumeroPedidoVendas.Text ;

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                //var returnValue = MyCommand.ExecuteReader();

                MyCommand.ExecuteNonQuery();
                conn.Close();
            }

        }

        private void gcHistoricoRoteiro_Click(object sender, EventArgs e)
        {
            if (_listaDeHistoricos.Count > 0)
            {
                var tmk = _listaDeHistoricos.Find(x => x.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
                txtHistorico.Text = tmk.DescricaoHistorico;
            }
        }

        private void gcVendas_DoubleClick(object sender, EventArgs e)
        {
          

        }

       

        private void gcAtendimentos_DoubleClick(object sender, EventArgs e)
        {
            var tmk = _listaTmkGridII.Find(x => x.Id == Convert.ToInt32(gridView1.Columns.View.GetFocusedRowCellValue(colunaId)));

            txtNumeroPedidoVendas.Text = tmk.Id.ToString();
            if (txtNumeroPedidoVendas.Text != string.Empty)
            {
                FormCadastroPedidoDeVenda formCadastroPedidoDeVenda = new FormCadastroPedidoDeVenda(txtNumeroPedidoVendas.Text.ToInt());

                formCadastroPedidoDeVenda.Show();
            }
        }

        private void gcAtendimentos_Click(object sender, EventArgs e)
        {
            var tmk = _listaTmkGridII.Find(x => x.Id == Convert.ToInt32(gridView1.Columns.View.GetFocusedRowCellValue(colunaId)));

            txtNumeroPedidoVendas.Text = tmk.Id.ToString();
            if (txtNumeroPedidoVendas.Text != string.Empty)
            {
                FormCadastroPedidoDeVenda formCadastroPedidoDeVenda = new FormCadastroPedidoDeVenda(txtNumeroPedidoVendas.Text.ToInt());

                formCadastroPedidoDeVenda.Show();
            }
        }

        private void txtIdPessoa_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
