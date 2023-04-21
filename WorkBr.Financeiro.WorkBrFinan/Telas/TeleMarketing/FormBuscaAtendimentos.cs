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
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;
using System.Data;
using MySql.Data.MySqlClient;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;

namespace Programax.Easy.View.Telas.TeleMarketing
{
    public partial class FormBuscaAtendimentos : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Tmk> _listaTmk;
        private List<TmkGrid> _listaTmkGrid;
        private PedidoDeVenda _pedidoDeVendaSelecionado;
        private string ConectionString;
        private DataSet mDataSet;
        private MySqlDataAdapter mAdapter;
        Boolean gerente = false;
        private int contador = 0;
        private Parametros _parametros;

        #endregion

        #region " CONSTRUTOR "

        public FormBuscaAtendimentos(bool somenteImpressao = false)
        {
            InitializeComponent();

            

            _listaTmk = new List<Tmk>();
            Libere();
            PreenchaCboStatusAtendimento();
            PreenchaCboPeriodoPreDeterminado();
            //PesquiseGerente(Sessao.PessoaLogada);
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

            //txtDataInicial.DateTime = DateTime.Now.Date;
            //txtDataFinal.DateTime = DateTime.Now.Date;
            ServicoParametros servicoParametros = new ServicoParametros();
            _parametros = servicoParametros.ConsulteParametros();

            this.ActiveControl = txtDataInicial;
        }

        #endregion
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
        //private void PesquiseGerente(Pessoa pessoaQueCadastrou)
        //{
        //    this.Cursor = Cursors.WaitCursor;
           

        //    string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

        //    ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

        //    var item = conexoes.Conexoes[IndiceBancoDados];
        //    string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
        //    string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
        //    string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
        //    string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
        //    int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

        //    var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

        //    if (serverPrincipalOnline)
        //    {
        //        ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
        //    }
        //    else
        //    {
        //        ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
        //        database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
        //        userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
        //        senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
        //        porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

        //        var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

        //        if (serverSecundarioOnline)
        //        {
        //            StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
        //        }
        //        else
        //        {
        //            //throw new Exception();
        //            //throw new Exception("Servidor de banco de dados não encontrado");
        //        }

        //    }


        //    using (var conn = new MySqlConnection(ConectionString))
        //    {
        //        conn.Open();

        //        string sqlWhere = "  grpacesso_id in (1,2, 4, 5,7, 8,11,15,18) And user_ide = " + pessoaQueCadastrou.Id;
        //        string innerJoin = " ";

        //        innerJoin = innerJoin + " inner join gruposacessos ON user_grpacesso_id = grpacesso_id ";

        //        var sql = " select user_ide " +

        //        " FROM  usuarios " +

        //            innerJoin +

        //            " WHERE " + sqlWhere;



        //        MySqlCommand MyCommand = new MySqlCommand(sql, conn);
        //        MySqlDataReader MyReader2;


        //        var returnValue = MyCommand.ExecuteReader();

        //        while (returnValue.Read())
        //        {
        //            gerente = true;

        //        }


        //        this.Cursor = Cursors.Default;
        //    }
        //}
        #region " EVENTOS CONTROLES "


        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            //if (cboCarteiras.EditValue.ToInt() == 0)
            //{
            //    MessageBox.Show("Escolha a Carteira a ser filtrada!", "Carteira não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            Pesquise();
        }

        private void txtIdCliente_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdCliente.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();
                var cliente = servicoPessoa.ConsultePessoaAtiva(txtIdCliente.Text.ToInt());

                PreenchaCliente(cliente, true);
            }
            else
            {
                PreenchaCliente(null);
            }
        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            var cliente = formPessoaPesquisa.PesquisePessoaClienteAtiva();

            if (cliente != null)
            {
                PreenchaCliente(cliente);
            }
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
            using (var conn = new MySqlConnection(ConectionString))
            {
                conn.Open();

                int Usuario = Sessao.PessoaLogada.Id;
                string Vendedor = Usuario.ToString()  ;
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
        private void PreenchaCboCarteirasVendedor()
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
            using (var conn = new MySqlConnection(ConectionString))
            {
                conn.Open();

                int Usuario = Sessao.PessoaLogada.Id;
                string Vendedor = Usuario.ToString();
                var sql = "";
              
                sql = "Select ID, NomeCarteira From carteiras Where Vendedor = (" + cboVendedores.EditValue + ")  order by ID";
              

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
        private void PreenchaCboVendedores()
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
            using (var conn = new MySqlConnection(ConectionString))
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
        private void cboSituacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
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

            Pessoa cliente = !string.IsNullOrEmpty(txtIdCliente.Text) ? new Pessoa { Id = txtIdCliente.Text.ToInt() } : null;
                        
            EnumStatusAtendimento? statusTmk = (EnumStatusAtendimento?)cboStatusAtendimento.EditValue;

            int marcaId = cboMarcas.EditValue.ToInt();
            
            ServicoTmk servicoTmk = new ServicoTmk();
            _listaTmk = servicoTmk.ConsulteListaParaTMK(cliente, statusTmk, dataInicial, dataFinal, marcaId, cboCarteiras.EditValue.ToInt());

            PreenchaGrid();
           // VerificaContador(txtDataInicial.Text, txtDataFinal.Text);

            this.Cursor = Cursors.Default;
        }
        private void VerificaContador(string DataInicial, string DataFinal)
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

            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();
                string DataIni = "";
                string DataF = "";
                if (DataInicial != "")
                {
                    DataIni= DataInicial.Substring(6, 4) + "-" + DataInicial.Substring(3, 2) + "-" + DataInicial.Substring(0, 2);
                    DataF = DataFinal.Substring(6, 4) + "-" + DataFinal.Substring(3, 2) + "-" + DataFinal.Substring(0, 2);
                }


                int intStatus = 0;

                int marcaId = cboMarcas.EditValue.ToInt();
                string Sql = string.Empty;
                if (cboStatusAtendimento.EditValue != null)
                {
                    if (cboStatusAtendimento.EditValue.ToString() == "DISPONIVEL")
                    {
                        intStatus = 0;
                    }
                    if (cboStatusAtendimento.EditValue.ToString() == "AGENDADO")
                    {
                        intStatus = 2;
                    }
                    if (cboStatusAtendimento.EditValue.ToString() == "CONCLUIDO")
                    {
                        intStatus = 1;
                    }
                    if (cboStatusAtendimento.EditValue.ToString() == "EMATENDIMENTO")
                    {
                        intStatus = 3;
                    }
                    if (cboStatusAtendimento.EditValue.ToString() == "CANCELADO")
                    {
                        intStatus = 4;
                    }
                   
                }
                else
                {
                    intStatus = 0;
                }
                //        }
                //        [Descripion("DISPONIVEL")]
                //        DISPONIVEL,

                //[Description("AGENDADO")]
                //        AGENDADO,

                //[Description("CONCLUIDO")]
                //        CONCLUIDO,

                //[Description("EM ATENDIMENTO")]
                //        EMATENDIMENTO,

                //[Description("CANCELADO")]
                //        CANCELADO,
                if (DataInicial != "")


                   
                {
                    //Sql = "select count(roteiros.rot_pedido_id) as Contador, roteirizacao.roteiro_data_conclusao as DataCompra,  roteiros.rot_pedido_id as NumPedido, " +
                    //            " pedidosvendas.pedido_cliente_id as ClienteId,  historicosatendimento.hisat_status as status, " +
                    //            " historicosatendimento.hisat_novo_pedido_id as NumPedidoNovo, pessoas.pes_razao as DescricaoCliente " +
                    //            " FROM roteiros" +
                    //            " inner join roteirizacao on roteiros.ROT_ROTEIRIZACAO_ID = roteirizacao.roteiro_id" +
                    //            " inner join pedidosvendas on roteiros.rot_pedido_id = pedidosvendas.pedido_id" +
                    //            " left join historicosatendimento on roteiros.rot_pedido_id = historicosatendimento.hisat_pedido_id" +
                    //            " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id" +
                    //            " inner join pedidosvendasitens on pedidosvendasitens.peditem_pedido_id = pedidosvendas.pedido_id" +
                    //            " inner join produtos on pedidosvendasitens.peditem_produto_id = produtos.prod_id" +
                    //            " WHERE rot_id > '0'" +
                    //            " AND roteirizacao.roteiro_data_conclusao Between '" + DataIni + "' And '" + DataF + "'" +
                    //            " AND pedidosvendas.PEDIDO_CARTEIRA  = " + cboCarteiras.EditValue.ToInt() +
                    //            " order by  NumPedido";
                    if (intStatus != 0)
                    {
                        Sql = "select count(pedidosvendas.pedido_id) as Contador, pedidosvendas.pedido_data_elaboracao as DataCompra,  " +
                        " pedidosvendas.pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId,  historicosatendimento.hisat_status as status," +
                        " pessoas.pes_razao FROM  pedidosvendas left join historicosatendimento on pedidosvendas.pedido_id = historicosatendimento.hisat_pedido_id" +
                        " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id  WHERE pedidosvendas.pedido_status <> 3 " +
                        " AND pedidosvendas.pedido_data_elaboracao Between '" + DataIni + "' And '" + DataF + "'" +
                        " AND pedidosvendas.PEDIDO_CARTEIRA =  " + cboCarteiras.EditValue.ToInt() +
                        " AND hisat_status = " + cboStatusAtendimento.EditValue.ToInt() +
                        " order by  NumPedido";
                    }
                    else
                    {
                        Sql = "select count(pedidosvendas.pedido_id) as Contador, pedidosvendas.pedido_data_elaboracao as DataCompra,  " +
                       " pedidosvendas.pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId,  historicosatendimento.hisat_status as status," +
                       " pessoas.pes_razao FROM  pedidosvendas left join historicosatendimento on pedidosvendas.pedido_id = historicosatendimento.hisat_pedido_id" +
                       " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id  WHERE pedidosvendas.pedido_status <> 3 " +
                       " AND pedidosvendas.pedido_data_elaboracao Between '" + DataIni + "' And '" + DataF + "'" +
                       " AND pedidosvendas.PEDIDO_CARTEIRA =  " + cboCarteiras.EditValue.ToInt() +
                       " AND hisat_status is null "  +
                       " order by  NumPedido";
                    }
                }

                else
                {
                    //Sql = "select count(roteiros.rot_pedido_id) as Contador, roteirizacao.roteiro_data_conclusao as DataCompra,  roteiros.rot_pedido_id as NumPedido, " +
                    //            " pedidosvendas.pedido_cliente_id as ClienteId,  historicosatendimento.hisat_status as status, " +
                    //            " historicosatendimento.hisat_novo_pedido_id as NumPedidoNovo, pessoas.pes_razao as DescricaoCliente " +
                    //            " FROM roteiros" +
                    //            " inner join roteirizacao on roteiros.ROT_ROTEIRIZACAO_ID = roteirizacao.roteiro_id" +
                    //            " inner join pedidosvendas on roteiros.rot_pedido_id = pedidosvendas.pedido_id" +
                    //            " left join historicosatendimento on roteiros.rot_pedido_id = historicosatendimento.hisat_pedido_id" +
                    //            " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id" +
                    //            " inner join pedidosvendasitens on pedidosvendasitens.peditem_pedido_id = pedidosvendas.pedido_id" +
                    //            " inner join produtos on pedidosvendasitens.peditem_produto_id = produtos.prod_id" +
                    //            " WHERE rot_id > '0'" +
                    //            " AND pedidosvendas.PEDIDO_CARTEIRA  = " + cboCarteiras.EditValue.ToInt() +
                    //            " order by  NumPedido";
                    if (intStatus != 0)
                    {
                        Sql = "select count(pedidosvendas.pedido_id) as Contador, pedidosvendas.pedido_data_elaboracao as DataCompra,  " +
                             " pedidosvendas.pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId,  historicosatendimento.hisat_status as status," +
                             " pessoas.pes_razao FROM  pedidosvendas left join historicosatendimento on pedidosvendas.pedido_id = historicosatendimento.hisat_pedido_id" +
                             " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id  WHERE pedidosvendas.pedido_status <> 3 " +
                             " AND pedidosvendas.PEDIDO_CARTEIRA =  " + cboCarteiras.EditValue.ToInt() +
                             " AND hisat_status = " + intStatus +
                             " order by  NumPedido";
                    }
                    else
                    {
                        Sql = "select count(pedidosvendas.pedido_id) as Contador, pedidosvendas.pedido_data_elaboracao as DataCompra,  " +
                               " pedidosvendas.pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId,  historicosatendimento.hisat_status as status," +
                               " pessoas.pes_razao FROM  pedidosvendas left join historicosatendimento on pedidosvendas.pedido_id = historicosatendimento.hisat_pedido_id" +
                               " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id  WHERE pedidosvendas.pedido_status <> 3 " +
                               " AND pedidosvendas.PEDIDO_CARTEIRA =  " + cboCarteiras.EditValue.ToInt() +
                               " AND hisat_status is null " +
                               " order by  NumPedido";
                    }
                }
            

                    MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    txtQtdePedidos.Text = returnValue["Contador"].ToString();
                }

            
            }

        }

        private void PreenchaCboStatusAtendimento()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusAtendimento>();

            

            lista.Insert(0, null);

            cboStatusAtendimento.Properties.DisplayMember = "Descricao";
            cboStatusAtendimento.Properties.ValueMember = "Valor";
            cboStatusAtendimento.Properties.DataSource = lista;
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
            if (cliente != null)
            {
                txtIdCliente.Text = cliente.Id.ToString();
                txtNomeCliente.Text = cliente.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Cliente nao encontrado!", "Cliente não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdCliente.Focus();
                }

                txtIdCliente.Text = string.Empty;
                txtNomeCliente.Text = string.Empty;
            }
        }

        private void PreenchaGrid()
        {
            List<TmkGrid> listaTmkGrid = new List<TmkGrid>();
            int numPedido = 0;
            contador = 0;
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
                    if (itemTmk.status == 0)
                    {
                        tmkGrid.Cor = Properties.Resources.CircleGreen;
                        tmkGrid.Status = "DISPONIVEL";
                    }

                    else if (itemTmk.status == 2)
                    {
                        tmkGrid.Cor = Properties.Resources.CircleYellow;
                        tmkGrid.Status = "AGENDADO";
                    }
                    else if (itemTmk.status == 1)
                    {
                        
                        tmkGrid.Cor = Properties.Resources.CircleYellow;
                        tmkGrid.Status = "AGENDADO";
                    }
                    else if (itemTmk.status == 3)
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
                    else if (itemTmk.status == 4)
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

                if (tmkGrid.NumPedidoNovo != 0)
                {
                    tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                    tmkGrid.Status = "CONCLUIDO";
                }

                if (numPedido != itemTmk.NumPedido) 
                {
                    listaTmkGrid.Add(tmkGrid);
                    contador += 1;
                }
               

                numPedido = itemTmk.NumPedido.ToInt();
            }

            txtQtdePedidos.Text = contador.ToString();

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
            //Vai chamar o FormAtendimento para fazer o atendimento selecionado
            if(_listaTmk.Count > 0)
            {
                var tmk = _listaTmk.Find(x => x.NumPedido == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

                FormAtendimento formAtender = new FormAtendimento(tmk.NumPedido.ToInt(), tmk.DataCompra, tmk.status.ToString(), tmk.status.ToInt(),cboCarteiras.EditValue.ToInt());
                formAtender.Show();
            }
            else
            {
                if (_listaTmkGrid != null)
                {
                    var tmk = _listaTmkGrid.Find(x => x.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

                    DateTime dt = tmk.DataCompra.ToDate();
                    FormAtendimento formAtender = new FormAtendimento(tmk.Id.ToInt(), dt, tmk.Status, 5,cboCarteiras.EditValue.ToInt());
                    formAtender.ShowDialog();

                    var tmkretorno = _listaTmkGrid.Find(x => x.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
                    
                    carregaconexao();
                    using (var conn = new MySqlConnection(ConectionString))
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
                                    if(_parametros.ParametrosVenda.StatusFaturado != true)
                                    {
                                        if (tmk.NumPedidoNovo != 0)
                                        {
                                            tmk.Cor = Properties.Resources.circle_Blue16x16;
                                            tmk.Status = "CONCLUIDO";
                                        }
                                    }
                                    else
                                    {
                                        if (returnValue["hisat_status"].ToInt() == 1)
                                        {
                                            tmk.Cor = Properties.Resources.CircleYellow;
                                            tmk.Status = "AGENDADO";
                                        }
                                    }
                                
                          
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
                            tmk.Agendamento = DateTime.Parse(returnValue["Data"].ToString()).ToString("dd-MM-yyyy");
                            if (_parametros.ParametrosVenda.StatusFaturado != true)
                            {
                                if (returnValue["Data"].ToString() != string.Empty)
                                {
                                    if (dta != "01-01-1900")
                                    {
                                        
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

                            if (tmk.Status == "CONCLUIDO")
                            {
                                tmk.Agendamento = "";
                            }
                        }
                    }
                }
            }
        }
        private void ConsultaStatus(int NumeroPedido)
        {
           
        }
        #endregion

        #region " CLASSES AUXILIARES "

        private class TmkGrid
        {
            public int Id { get; set; }
            public int Pedido { get; set; }

            public string DataCompra { get; set; }

            public string CodigoCliente { get; set; }

            public string Cliente { get; set; }                      

            public string Status { get; set; }
            public int  NumPedidoNovo { get; set; }

            public Image Cor { get; set; }

            public double ValorTotal { get; set; }
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

            FormAtendimento formAtender = new FormAtendimento(tmk.NumPedido.ToInt(), tmk.DataCompra,tmk.status.ToString(),5,cboCarteiras.EditValue.ToInt(), true);

            formAtender.Show();
        }
        private void txtId_Leave(object sender, EventArgs e)
        {
            BusqueECarreguePedido();
        }
        private void BusqueECarreguePedido()
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                var pedidoDeVenda = servicoPedidoDeVenda.Consulte(txtId.Text.ToInt());

               

                FormAtendimento formAtender = new FormAtendimento( pedidoDeVenda.Id, pedidoDeVenda.DataElaboracao,"",0,cboCarteiras.EditValue.ToInt());

                formAtender.Show();
            }
        }

        private void PesquiseCarteira(int idCarteira)
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


            ServicoTmk servicoTmk = new ServicoTmk();

            using (var conn = new MySqlConnection(ConectionString))
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
                        var dta = DateTime.Parse(returnValue["Agendamento"].ToString()).ToString("dd-MM-yyyy") ;
                        if (dta !="01-01-1900")
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
                            if (_parametros.ParametrosVenda.StatusFaturado != true)
                            {
                                tmkGrid.Cor = Properties.Resources.CircleYellow;
                                tmkGrid.Status = "AGENDADO";
                            }
                            else
                            {
                                if (intStatus == 1)
                                {
                                    tmkGrid.Cor = Properties.Resources.CircleYellow;
                                    tmkGrid.Status = "AGENDADO";
                                }
                                else
                                {
                                    tmkGrid.Cor = Properties.Resources.CircleRed16x16;
                                    tmkGrid.Status = "EM ATENDIMENTO";
                                }
                            }
                         
                          
                        }
                        else if (intStatus == 2)
                        {
                            if (tmkGrid.NumPedidoNovo != 0)
                            {
                                tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                                tmkGrid.Status = "CONCLUIDO";
                            }
                            else
                            {
                                tmkGrid.Cor = Properties.Resources.CircleGreen;
                                tmkGrid.Status = "DISPONIVEL";
                            }
                        }
                        else if (intStatus == 3)
                        {
                       
                            {
                                if (_parametros.ParametrosVenda.StatusFaturado != true)
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
                                else
                                {
                                    //if (tmkGrid.Agendamento != null)
                                    //{
                                    //    tmkGrid.Cor = Properties.Resources.CircleYellow;
                                    //    tmkGrid.Status = "AGENDADO";
                                    //}
                                    //else
                                    {
                                        tmkGrid.Cor = Properties.Resources.CircleRed16x16;
                                        tmkGrid.Status = "EM ATENDIMENTO";
                                    }
                                }
                            
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
                    if(_parametros.ParametrosVenda.StatusFaturado != true)
                    {
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

                    }

                    if (tmkGrid.Status == "CONCLUIDO")
                    {
                        tmkGrid.Agendamento = "";
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
        private void VerificaContadorII(int IdCarteira)
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

            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();




                string sqlWhere = " pedidosvendas.pedido_status <> 3 ";
                string innerJoin = " ";



                innerJoin = innerJoin + " left join historicosatendimento on pedidosvendas.pedido_id = historicosatendimento.hisat_pedido_id ";
                innerJoin = innerJoin + " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id ";
                sqlWhere = sqlWhere + " AND pedidosvendas.PEDIDO_CARTEIRA = " + "'" + IdCarteira + "'";


                var sql = " select count(pedidosvendas.pedido_id) as Contador, pedidosvendas.pedido_data_elaboracao as DataCompra, " +
                        " pedidosvendas.pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId, " +
                        " historicosatendimento.hisat_status as status,  pessoas.pes_razao" +

                " FROM  pedidosvendas " +

                    innerJoin +

                    " WHERE " + sqlWhere + "order by  NumPedido";


                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    txtQtdePedidos.Text = returnValue["Contador"].ToString();
                }

            }

        }
        private void VerificaVendedor(int IdCarteira)
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

            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();
                    var sql = " SELECT pessoas.pes_razao as Nome FROM carteiras " +
                                " Inner join Pessoas ON carteiras.Vendedor = pessoas.pes_id " +
                                " Where id = " + IdCarteira ;

                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    cboVendedores.EditValue = returnValue["Nome"].ToString();
                }

            }

        }

        private void cboCarteiras_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtIdCliente.Text = "";
            txtNomeCliente.Text = "";
            List<TmkGrid> listaTmkGrid = new List<TmkGrid>();

            TmkGrid tmkGrid = new TmkGrid();
            gcAtendimentos.DataSource = listaTmkGrid;
            gcAtendimentos.RefreshDataSource();
        }

        private void cboVendedores_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCarteirasVendedor();
        }

        private void cboStatusAtendimento_EditValueChanged(object sender, EventArgs e)
        {
            //Pesquise();
        }

        private void btnpesquisacarteiras_Click(object sender, EventArgs e)
        {
            if (cboCarteiras.EditValue != null)
            {
                PesquiseCarteira(cboCarteiras.EditValue.ToInt());
            }
           
        }

        private void txtIdCliente_EditValueChanged(object sender, EventArgs e)
        {

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
    }
}
