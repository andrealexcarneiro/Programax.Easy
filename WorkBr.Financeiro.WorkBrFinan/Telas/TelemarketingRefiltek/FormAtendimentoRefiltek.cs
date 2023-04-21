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
using MySql.Data.MySqlClient;
using System.Data;


namespace Programax.Easy.View.Telas.TeleMarketing
{
    public partial class FormAtendimentoRefiltek : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Pessoa _pessoaCadastro;
        private List<HistoricoAtendimento> _listaDeHistoricos;
        private EnumStatusAtendimento? _statusAnterior;
        private bool _estahSaindoAoConcluir = false;
        private bool _foiFechado = false;
        private int _idCliente;
        private DateTime _inicioAtendimento;
        private bool _ehHistorico;
        private string ConectionStringII;
        private string CodigoCliente;
        private string ConectionString;
        private int status = 0;
        private string strStatus = "";
        private int numChamadas = 0;
        private List<PedidoDeVenda> _listaTmkGridII;
        private bool gerente = false;
        private Boolean altera = false;
        #endregion

        #region " CONSTRUTOR "

        public FormAtendimentoRefiltek(int NumeroPedido, DateTime DataCompra, string CodigoCliente, string strStatus, int intStatus)
        {
            InitializeComponent();
            
            this.Cursor = Cursors.Default;

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
            if (intStatus != 5)
            {
                status = intStatus;
            }

            strStatus = strStatus;

            _listaDeHistoricos = new List<HistoricoAtendimento>();
            Libere();
            InformeUsuarioContasAPagarReceber(Sessao.PessoaLogada);



            CarregarInformacoesAtendimento(NumeroPedido, DataCompra, CodigoCliente);

            this.ActiveControl = txtIdPessoa;
            PesquiseGerente(Sessao.PessoaLogada);
            PreenchaGridHistorico();
            PreenchaCboCarteiras();
            NumeroChamadas(NumeroPedido);
            CarregaVendas();

            this.Cursor = Cursors.Default;
        }
        private void NumeroChamadas(int Pedido)
        {
            carregaconexaoII();
            using (var conn = new MySqlConnection(ConectionStringII))
            {
                conn.Open();
                string sql = "";

                sql = "SELECT count(hisat_id) as Numero FROM historicosatendimento Where hisat_pedido_id = " + Pedido;

                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    numChamadas = returnValue["Numero"].ToInt();
                }

            }

        }
        private void PreenchaCboCarteiras()
        {
            this.Cursor = Cursors.WaitCursor;
            carregaconexaoII();

            string Sql = string.Empty;
            int Usuario = Sessao.PessoaLogada.Id;
            string Vendedor = Usuario.ToString();
            Vendedor = Vendedor + ", 0";
            using (var conn = new MySqlConnection(ConectionStringII))
            {
                conn.Open();
                string sql = "";

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
        private void SalvarCarteira()
        {
            carregaconexaoII();

            int intContador = 0;


            string hisat_descricao_historico = "Troca de Carteira";
            DateTime hisat_data_historico = DateTime.Now;
            string hisat_tempo_duracao = string.Empty;
            int hisat_pes_usuario_id = Sessao.PessoaLogada.Id.ToInt();

            int hisat_pedido_id = txtNumeroPedidoVendas.Text.ToInt();
            int hisat_novo_pedido_id = 0;
            hisat_novo_pedido_id = txtPedidoGerado.Text.ToInt();
            int hisat_status = 0;

            var data = DateTime.Parse(hisat_data_historico.ToString()).ToString("yyyy-MM-dd HH:mm:ss");

            using (var conn = new MySqlConnection(ConectionStringII))
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
                using (var conn = new MySqlConnection(ConectionStringII))

                {
                    conn.Open();

                    MySqlCommand command = new MySqlCommand("INSERT INTO agendamentocontato (Pedido,Data,contador)" +
                    "VALUES(" + Pedido + ", '" + dtAgendamento + "', " + Contador + ")", conn);

                    command.ExecuteNonQuery();
                    conn.Close();


                }
                using (var conn = new MySqlConnection(ConectionStringII))
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

                using (var conn = new MySqlConnection(ConectionStringII))
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
        private void PesquiseGerente(Pessoa pessoaQueCadastrou)
        {
            this.Cursor = Cursors.WaitCursor;
            btnCancelar.Visible = false;

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


            using (var conn = new MySqlConnection(ConectionString))
            {
                conn.Open();

                string sqlWhere = "  grpacesso_id in (1,2, 4, 5,7,8,11,15,18) And user_ide = " + pessoaQueCadastrou.Id;
                string innerJoin = " ";

                innerJoin = innerJoin + " inner join gruposacessos ON user_grpacesso_id = grpacesso_id ";

                var sql = " select user_ide " +

                " FROM  usuarios " +

                    innerJoin +

                    " WHERE " + sqlWhere;



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();

                while (returnValue.Read())
                {
                    btnCancelar.Visible = true;
                    this.Cursor = Cursors.Default;
                    return;
                }
                if (strStatus == "CANCELADO")
                {
                    btnConcluir.Visible = false;
                }
                if (strStatus == "CONCLUIDO")
                {
                    btnConcluir.Visible = false;
                }

                this.Cursor = Cursors.Default;
            }
        }
        public class PedidoDeVendaCliente
        {

        }
        private void CarregaVendas()
        {
            carregaconexaoII();

            using (var conn = new MySqlConnection(ConectionStringII))
            {
                conn.Open();

                string sqlWhere = "  pedido_cliente_id = " + txtIdPessoa.Text.ToInt() + " order by pedido_data_elaboracao ";
                string innerJoin = " ";

                innerJoin = innerJoin + " left join pessoas ON pedido_vendedor_id = pessoas.pes_id ";

                var sql = " Select pedido_id, pedido_data_elaboracao, pedido_valor_total, pessoas.pes_fantasia " +

                " FROM  pedidosvendas " +

                    innerJoin +

                    " WHERE " + sqlWhere;



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                List<PedidoDeVenda> movimentacaoItensGrid = new List<PedidoDeVenda>();
                while (returnValue.Read())
                {
                    PedidoDeVenda vendas = new PedidoDeVenda();

                    var dt = DateTime.Parse(returnValue["pedido_data_elaboracao"].ToString()).ToString("dd-MM-yyyy");
                    vendas.Id = returnValue["pedido_id"].ToInt(); 
                    vendas.DataElaboracaoII = dt;
                    vendas.ValorTotalII = "R$ " + double.Parse(returnValue["pedido_valor_total"].ToString()).ToString("0.00");
                    if (returnValue["pes_fantasia"].ToString() != string.Empty)
                    {
                        vendas.VendedorNome = returnValue["pes_fantasia"].ToString();
                    }


                    movimentacaoItensGrid.Add(vendas);
                }

                gcAtendimentos.DataSource = movimentacaoItensGrid;
                gcAtendimentos.RefreshDataSource();
                _listaTmkGridII = movimentacaoItensGrid;
                this.Cursor = Cursors.Default;
            }
          
        }
        private void AlteraStatus(int Pedido)
        {
            carregaconexaoII();


            using (var conn = new MySqlConnection(ConectionStringII))
            {

                conn.Open();

                string Sql = "update roteiros set rot_status = " + 1 +
                                " where rot_pedido_id = " + Pedido;


                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();


            }

        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void FormAtendimento_Closed(object sender, System.EventArgs e)
        {
            if (!_ehHistorico)
            {
                if (txtPedidoGerado.Text != string.Empty && _statusAnterior != EnumStatusAtendimento.CONCLUIDO)
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

                //if (!_estahSaindoAoConcluir)
                //{
                //    ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

                //    var pedido = servicoPedido.Consulte(txtNumeroPedidoVendas.Text.ToInt());

                //    pedido.StatusAtendimento = ((EnumStatusAtendimento)_statusAnterior);

                //    servicoPedido.Atualize(pedido);
                //}
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Concluir();
        }

        private void Concluir()
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
            carregaconexaoII();

            string hisat_descricao_historico = txtHistorico.Text.ToString();
            DateTime hisat_data_historico = DateTime.Now;
            string hisat_tempo_duracao = string.Empty;
            int hisat_pes_usuario_id = 1;
            int hisat_pedido_id = txtNumeroPedidoVendas.Text.ToInt();
            int hisat_novo_pedido_id = 0;
            hisat_novo_pedido_id = txtPedidoGerado.Text.ToInt();
            int hisat_status = 3;
            int statusroteiro = 0;
            int hisat_contador = numChamadas;

            if (hisat_novo_pedido_id != 0)
            {
                hisat_status = 2;
                statusroteiro = 2;
            }
            else
            {
                statusroteiro = 1;
            }
            var data = DateTime.Parse(hisat_data_historico.ToString()).ToString("yyyy-MM-dd HH:mm:ss");

            if (txtAgendamento.Text != string.Empty)
            {
                hisat_status = 2;
            }
            {
                using (var conn = new MySqlConnection(ConectionStringII))

                {
                    conn.Open();
                    if (strStatus != "CANCELADO")

                    {
                        MySqlCommand command = new MySqlCommand("INSERT INTO historicosatendimento (hisat_descricao_historico, hisat_data_historico, hisat_tempo_duracao, hisat_pes_usuario_id, " +
                                                                "hisat_pedido_id, hisat_novo_pedido_id, hisat_status, hisat_contador)" +
                                         "VALUES('" + hisat_descricao_historico + "', '" + data + "', '" + hisat_tempo_duracao + "', " + hisat_pes_usuario_id + ", " +
                                                "'" + hisat_pedido_id + "', '" + hisat_novo_pedido_id + "', '" + hisat_status + "', " + hisat_contador + ")", conn);

                        command.ExecuteNonQuery();
                        //conn.Close();

                        string Sql = "";
                        //MessageBox.Show("Registro Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //if (hisat_novo_pedido_id != 0)
                        //{
                        //    Sql = "update historicosatendimento set hisat_status = " + 2 +
                        //        " where hisat_pedido_id = " + txtNumeroPedidoVendas.Text.ToInt();
                        //}
                        //else
                        //{
                        //    Sql = "update historicosatendimento set hisat_status = " + 3 +
                        //           " where hisat_pedido_id = " + txtNumeroPedidoVendas.Text.ToInt();
                        //}

                        //string Sql = "update roteiros set rot_status = " + statusroteiro +
                        //         " where rot_pedido_id = " + txtNumeroPedidoVendas.Text.ToInt();

                        //MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                        //MySqlDataReader MyReader2;

                        ////command.ExecuteNonQuery();
                        //var returnValue = MyCommand.ExecuteReader();

                        MessageBox.Show("Registro Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //return;

                    }
                    else
                    {

                        string Sql = "update historicosatendimento set hisat_status = " + 1 +
                                     " where hisat_pedido_id = " + txtNumeroPedidoVendas.Text.ToInt();

                        MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                        MySqlDataReader MyReader2;


                        var returnValue = MyCommand.ExecuteReader();

                        MessageBox.Show("Registro Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    SalvarAgendamento();
                }
            }
           
        }

        private void SalvarAgendamento()
        {
           

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

            using (var conn = new MySqlConnection(ConectionStringII))
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
                using (var conn = new MySqlConnection(ConectionStringII))

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

            historico.TempoDuracao = (DateTime.Now - _inicioAtendimento).ToString();



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

        private void CarregarInformacoesAtendimento(int NumeroPedido, DateTime DataCompra, string CodCliente)
        {
            txtDataCompra.Text = DataCompra.ToString();
            txtIdPessoa.Text = CodCliente.ToString();
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
            }





            PedidoDeVenda pedido = new PedidoDeVenda();



            using (var conn = new MySqlConnection(ConectionStringII))
            {
                MySqlCommand command = new MySqlCommand("ConsultaPedidoRefiltek", conn);
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("varPedido", NumeroPedido);


                conn.Open();
                command.ExecuteNonQuery();

                var returnValue = command.ExecuteReader();
                double variavel = 0;
                while (returnValue.Read())
                {
                    //variavel = returnValue["Total"].ToDouble();
                    //planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", variavel);
                    //dblReceita = variavel.ToDouble();

                    //{

                    _statusAnterior = pedido.StatusAtendimento;
                    //_idCliente = pedido.Cliente.Id;

                    //if (!_ehHistorico)
                    //{
                    //    pedido.StatusAtendimento = EnumStatusAtendimento.EMATENDIMENTO;
                    //    servicoPedido.Atualize(pedido);
                    //}


                    txtIdPessoa.Text = returnValue["pedido_cliente_id"].ToString();
                    txtNomePessoa.Text = returnValue["pes_razao"].ToString();

                    txtDataCompra.Text = returnValue["pedido_data_elaboracao"].ToString();

                    txtCelular.Text = returnValue["tele_fone"].ToString();

                    //txtFoneComercial.Text = returnValue["tele_fone"].ToString(); 

                    //txtFoneResidencial.Text = returnValue["tele_fone"].ToString(); 

                    //txtFoneRecado.Text = returnValue["tele_fone"].ToString();

                    //txtOutroFone.Text = returnValue["tele_fone"].ToString(); 
                    txtNumeroPedidoVendas.Text = returnValue["pedido_id"].ToString();
                    _idCliente = returnValue["pedido_cliente_id"].ToInt();
                    txtUsuario.Text = Sessao.PessoaLogada.Id.ToString() + " - " + Sessao.PessoaLogada.DadosGerais.Razao;

                    _inicioAtendimento = DateTime.Now;
                    txtInicioAtendimento.Text = _inicioAtendimento.ToString("HH:mm:ss");


                    ////Carrega o Grid Com os históricos
                    //_listaDeHistoricos = new ServicoHistoricoAtendimento().ConsulteLista(NumeroPedido);

                    //if (_listaDeHistoricos != null && _listaDeHistoricos.Count != 0)
                    //{
                    //    _statusAnterior = _listaDeHistoricos.LastOrDefault().Status;

                    //    if(_statusAnterior == EnumStatusAtendimento.FINALIZADO)
                    //    {
                    //        btnConcluir.Enabled = false;
                    //        btnPostergar.Enabled = false;
                    //        txtPedidoGerado.Text = _listaDeHistoricos.LastOrDefault().NovoPedido.Id.ToString();
                    //    }               
                    //}

                    //if (_ehHistorico)
                    //{
                    //    btnConcluir.Enabled = false;
                    //    btnPostergar.Enabled = false;
                    //}
                }
                conn.Close();
            }
            //PreenchaGridHistorico();
        }


        protected virtual ServicoContasPagarReceber RetorneServicoContasPagarOuReceber()
        {
            return null;
        }

        protected virtual void PreenchaPessoa(Pessoa pessoa, bool exibirMensagemDeNaoEncontrado = false)
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
            }


            using (var conn = new MySqlConnection(ConectionStringII))
            {
                MySqlCommand command = new MySqlCommand("ConsultaClienteRefiltek", conn);
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("varCodigo", txtIdPessoa.Text);


                conn.Open();
                command.ExecuteNonQuery();

                var returnValue = command.ExecuteReader();
                double variavel = 0;
                while (returnValue.Read())
                {

                    //txtIdPessoa.Text = pessoa.Id.ToString();
                    txtNomePessoa.Text = returnValue["pes_razao"].ToString();
                }




                //txtIdPessoa.Text = pessoa.Id.ToString();
                //txtNomePessoa.Text = pessoa.DadosGerais.Razao;


            }
        }

        protected virtual void InformeUsuarioContasAPagarReceber(Pessoa pessoaQueCadastrou)
        {
            _pessoaCadastro = pessoaQueCadastrou;
            txtUsuario.Text = pessoaQueCadastrou.Id + " - " + pessoaQueCadastrou.DadosGerais.Razao;
        }

        private void PreenchaGridHistorico()
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
            }


            using (var conn = new MySqlConnection(ConectionStringII))
            {
                conn.Open();

                string sqlWhere = "  hisat_pedido_id = " + txtNumeroPedidoVendas.Text.ToInt() + " Order by hisat_contador Desc";
                string innerJoin = " ";

                innerJoin = innerJoin + " inner join usuarios ON historicosatendimento.hisat_pes_usuario_id = usuarios.user_ide ";

                var sql = " select *, user_login " +

                " FROM  historicosatendimento " +

                    innerJoin +

                    " WHERE " + sqlWhere;



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                List<HistoricoGrid> listaHistGrid = new List<HistoricoGrid>();
                while (returnValue.Read())
                {



                    HistoricoGrid historicoGrid = new HistoricoGrid();

                    historicoGrid.Atendente = returnValue["hisat_pes_usuario_id"].ToInt() + " - " + returnValue["user_login"].ToString();

                    historicoGrid.DataHistorico = returnValue["hisat_data_historico"].ToString();

                    historicoGrid.DescricaoHistorico = returnValue["hisat_descricao_historico"].ToString();

                    historicoGrid.PedidoGerado = returnValue["hisat_novo_pedido_id"].ToString();

                    historicoGrid.Status = returnValue["hisat_status"].ToString();
                    numChamadas += 1;

                    if (returnValue["hisat_status"].ToInt() == 0)
                    {
                        historicoGrid.Status = "DISPONIVEL";
                    }

                    else if (returnValue["hisat_status"].ToInt() == 2)
                    {
                        historicoGrid.Status = "AGENDADO";
                    }
                    else if (returnValue["hisat_status"].ToInt() == 1)
                    {
                        historicoGrid.Status = "CONCLUIDO";
                    }
                    else if (returnValue["hisat_status"].ToInt() == 3)
                    {
                        if (historicoGrid.PedidoGerado.ToInt() != 0)
                        {
                            historicoGrid.Status = "CONCLUIDO";
                        }
                        else
                        {
                            historicoGrid.Status = "EM ATENDIMENTO";
                        }

                    }
                    else if (returnValue["hisat_status"].ToInt() == 4)
                    {
                        historicoGrid.Status = "CANCELADO";
                    }
                    else
                    {
                        historicoGrid.Status = "DISPONIVEL";
                    }
        
                    if (returnValue["hisat_novo_pedido_id"].ToInt() != 0)
                    {
                        historicoGrid.PedidoGerado = returnValue["hisat_novo_pedido_id"].ToString();
                        historicoGrid.Status = "CONCLUIDO";
                    }






                    listaHistGrid.Add(historicoGrid);


                    gcHistoricoRoteiro.DataSource = listaHistGrid;
                    gcHistoricoRoteiro.RefreshDataSource();



                }

            }

        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class HistoricoGrid
        {
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
                FormCadastroPedidoDeVendaRefiltek formCadastroPedidoDeVendaRefiltek = new FormCadastroPedidoDeVendaRefiltek(txtNumeroPedidoVendas.Text.ToInt(), txtIdPessoa.Text.ToInt());

                formCadastroPedidoDeVendaRefiltek.Show();
            }
        }

        private void btnWhats_Click(object sender, EventArgs e)
        {
            string link = "https://api.whatsapp.com/send?phone=";
            link = link + "55" + txtCelular.Text;

            System.Diagnostics.Process.Start(link);
        }

        private void btnVerPedidoGerado_Click(object sender, EventArgs e)
        {

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVerCadastroCliente_Click(object sender, EventArgs e)
        {
            FormCadastroPessoa formCadastroPessoa = new FormCadastroPessoa(txtIdPessoa.Text.ToInt());
            formCadastroPessoa.ShowDialog();
        }

        private void txtIdPessoa_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (txtHistorico.Text == string.Empty)
            {
                MessageBox.Show("Para Concluir o Atendimento. Você precisa informar a Descrição do Histório!", "Conclusão do Atendimento!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
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
            }

            string hisat_descricao_historico = txtHistorico.Text.ToString();
            DateTime hisat_data_historico = DateTime.Now;
            string hisat_tempo_duracao = string.Empty;
            int hisat_pes_usuario_id = Sessao.PessoaLogada.Id;
            int hisat_pedido_id = txtNumeroPedidoVendas.Text.ToInt();
            int hisat_novo_pedido_id = 0;
            hisat_novo_pedido_id = txtPedidoGerado.Text.ToInt();
            int hisat_status = 4;

            var data = DateTime.Parse(hisat_data_historico.ToString()).ToString("yyyy-MM-dd HH:mm:ss");

            using (var conn = new MySqlConnection(ConectionStringII))

            {
                conn.Open();
                if (strStatus == "DISPONIVEL")

                {
                    MySqlCommand command = new MySqlCommand("INSERT INTO historicosatendimento (hisat_descricao_historico, hisat_data_historico, hisat_tempo_duracao, hisat_pes_usuario_id, " +
                                                            "hisat_pedido_id, hisat_novo_pedido_id, hisat_status)" +
                                     "VALUES('" + hisat_descricao_historico + "', '" + data + "', '" + hisat_tempo_duracao + "', " + 1 + ", " +
                                            "'" + hisat_pedido_id + "', '" + hisat_novo_pedido_id + "', '" + hisat_status + "')", conn);



                    command.ExecuteNonQuery();
                    conn.Close();


                    MessageBox.Show("Registro Cancelado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    string Sql = "update historicosatendimento set hisat_status = " + 4 +
                                 " where hisat_pedido_id = " + txtNumeroPedidoVendas.Text.ToInt();


                    MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                    MySqlDataReader MyReader2;


                    var returnValue = MyCommand.ExecuteReader();
                    //command.ExecuteNonQuery();

                    MessageBox.Show("Registro Cancelado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

        }

        private void gcHistoricoRoteiro_Click(object sender, EventArgs e)
        {
            {
                //var tmk = gcHistoricoRoteiro.Find(x => x.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

                //txtHistorico.Text = tmk.DescricaoHistorico.ToString();
            }
        }

        private void btnSalvarCarteira_Click(object sender, EventArgs e)
        {
            SalvarCarteira();
        }
        
        private class CarteiraCbo
        {
            public int ID { get; set; }
            public string Carteira { get; set; }


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
    }
}
    

