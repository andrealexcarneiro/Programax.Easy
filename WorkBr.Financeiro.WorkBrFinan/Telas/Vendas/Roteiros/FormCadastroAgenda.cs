using System;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.RoteiroServ;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using System.Linq;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using System.Drawing;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using DevExpress.XtraReports.UI;
using DevExpress.DXCore.Controls.LookAndFeel;
using System.Transactions;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using MySql.Data.MySqlClient;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;

namespace Programax.Easy.View.Telas.Vendas.Roteiros
{
    public partial class FormCadastroAgenda : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Pessoa _pessoaCadastro;        
        private Pessoa _clienteSelecionado;
        private int _proximasDatas = 0;
        private DateTime _dataInicial;
        private Parametros _parametros;
        private int _quantidademanha;
        private int _quantidadetarde;
        private string ConectionString;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroAgenda(Pessoa cliente, int pedidoId)
        {
            InitializeComponent();

            PreenchaPeriodos();
            PreenchaCboTipoEndereco();
            PreenchaTipoServico();

            InformeUsuarioContasAPagarReceber(Sessao.PessoaLogada);

            txtDataAgenda.DateTime = DateTime.Now.Date;
            
            //Endereço
            _clienteSelecionado = cliente;
            cboTipoEndereco.EditValue = EnumTipoEndereco.PRINCIPAL;
            PreenchaDadosClienteEndereco(_clienteSelecionado);

            CarregaPeriodos(DateTime.Now);
            txtPedido.Text = pedidoId.ToString();

            BuscaAgendamentoDoPedido(pedidoId);
            CarregueParametros();
        }

        #endregion
        private void CarregueParametros()
        {
            ServicoParametros servicoParametros = new ServicoParametros();

            _parametros = servicoParametros.ConsulteParametros();
            _quantidademanha = _parametros.ParametrosVenda.LimiteDiarioManha ;
            _quantidadetarde = _parametros.ParametrosVenda.LimiteDiarioTarde;

        }
        #region " EVENTOS CONTROLES "

        private void cboTipoEndereco_EditValueChanged(object sender, EventArgs e)
        {
            if (cboTipoEndereco.Enabled == true && cboTipoEndereco.EditValue != null)
            {
                if (_clienteSelecionado != null)
                {
                    if (_clienteSelecionado.ListaDeEnderecos != null)
                    {
                        PreenchaDadosClienteEndereco(_clienteSelecionado, (EnumTipoEndereco)cboTipoEndereco.EditValue);
                    }
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(cboTipoEndereco.EditValue == null || cboTipoServico.EditValue == null)
            {
                MessageBox.Show("Para continuar, você precisa informar o Tipo de ENDEREÇO e de SERVIÇO.\n\nPor favor informe para continuarmos.", 
                                "Criando Agenda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }
            if (cboPeriodo.EditValue.ToString() == "MANHA")
            {
                validaPeriodos(txtDataAgenda.Text.ToDate());
                if (_quantidademanha <= 0)
                {
                    MessageBox.Show("Limite de Agendamentos excedido, escolha outro peíodo ou outra data.\n\nPor favor informe para continuarmos.",
                         "Criando Agenda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }
            }
            if (cboPeriodo.EditValue.ToString() == "TARDE")
            {
                validaPeriodos(txtDataAgenda.Text.ToDate());
                if (_quantidadetarde <= 0)
                {
                    MessageBox.Show("Limite de Agendamentos excedido, escolha outro peíodo ou outra data.\n\nPor favor informe para continuarmos.",
                         "Criando Agenda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }
            }


            Action actionSalvar = () =>
            {            
                var roteiro = BuscarRoteiroParaEdicao();


                EditarEndereco();

                ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

                servicoRoteiro.SalveAgenda(roteiro, txtPedido.Text.ToInt());

                txtAgendamento.Text = roteiro.Id.ToString();

                if(_parametros.ParametrosVenda.StatusFaturado == true)
                {
                    AlteraPedidoStatus(txtPedido.Text.ToInt());
                }
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }
        private void AlteraPedidoStatus(int Pedido)
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

                string Sql = "update historicosatendimento set hisat_status = " + 1 +

                            " where hisat_novo_pedido_id = " + Pedido;

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                //var returnValue = MyCommand.ExecuteReader();

                MyCommand.ExecuteNonQuery();
                conn.Close();
            }

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
        private void EditarEndereco()
        {
            carregaconexao();

            if (_clienteSelecionado == null) return;
            

            var cliente = new ServicoPessoa().ConsulteClienteAtivo(_clienteSelecionado.Id);

            if (cliente != null && cliente.ListaDeEnderecos != null && cliente.ListaDeEnderecos.Count > 0)
            {
                var endereco = cliente.ListaDeEnderecos.FirstOrDefault(end => end.TipoEndereco == (EnumTipoEndereco)cboTipoEndereco.EditValue);
                if (endereco != null)
                {
                    using (var conn = new MySqlConnection(ConectionString))
                    {

                        conn.Open();
                        int tipoendereco = cboTipoEndereco.ItemIndex - 1;

                        string Sql = " Update pedidosvendas Set PEDIDO_CEP_ENDERECO = '" + endereco.CEP + "', " +
                                        " PEDIDO_RUA_ENDERECO = '" + endereco.Rua.Replace("'","") + "', " +
                                        " PEDIDO_BAIRRO_ENDERECO = '" + endereco.Bairro + "', " +
                                        " PEDIDO_CIDADE_ID_ENDERECO = " + endereco.Cidade.Id + " , " +
                                        " pedido_tipo_endereco = " + tipoendereco +
                                        " Where pedido_id = " + txtPedido.Text;


                        MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                        MySqlDataReader MyReader2;


                        var returnValue = MyCommand.ExecuteReader();


                    }
                }
                
            }
        }
        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void btnManhaData1_Click(object sender, EventArgs e)
        {
            txtDataAgenda.Text = lblData1.Text;
            cboPeriodo.EditValue = EnumPeriodo.MANHA;
        }

        private void btnManhaData2_Click(object sender, EventArgs e)
        {
            txtDataAgenda.Text = lblData2.Text;
            cboPeriodo.EditValue = EnumPeriodo.MANHA;
        }

        private void btnManhaData3_Click(object sender, EventArgs e)
        {
            txtDataAgenda.Text = lblData3.Text;
            cboPeriodo.EditValue = EnumPeriodo.MANHA;
        }

        private void btnManhaData4_Click(object sender, EventArgs e)
        {
            txtDataAgenda.Text = lblData4.Text;
            cboPeriodo.EditValue = EnumPeriodo.MANHA;
        }

        private void btnManhaData5_Click(object sender, EventArgs e)
        {
            txtDataAgenda.Text = lblData5.Text;
            cboPeriodo.EditValue = EnumPeriodo.MANHA;
        }

        private void btnManhaData6_Click(object sender, EventArgs e)
        {
            txtDataAgenda.Text = lblData6.Text;
            cboPeriodo.EditValue = EnumPeriodo.MANHA;
        }

        private void btnTardeData1_Click(object sender, EventArgs e)
        {
            txtDataAgenda.Text = lblData1.Text;
            cboPeriodo.EditValue = EnumPeriodo.TARDE;
        }

        private void btnTardeData2_Click(object sender, EventArgs e)
        {
            txtDataAgenda.Text = lblData2.Text;
            cboPeriodo.EditValue = EnumPeriodo.TARDE;
        }

        private void btnTardeData3_Click(object sender, EventArgs e)
        {
            txtDataAgenda.Text = lblData3.Text;
            cboPeriodo.EditValue = EnumPeriodo.TARDE;
        }

        private void btnTardeData4_Click(object sender, EventArgs e)
        {
            txtDataAgenda.Text = lblData4.Text;
            cboPeriodo.EditValue = EnumPeriodo.TARDE;
        }

        private void btnTardeData5_Click(object sender, EventArgs e)
        {
            txtDataAgenda.Text = lblData5.Text;
            cboPeriodo.EditValue = EnumPeriodo.TARDE;
        }

        private void btnTardeData6_Click(object sender, EventArgs e)
        {
            txtDataAgenda.Text = lblData6.Text;
            cboPeriodo.EditValue = EnumPeriodo.TARDE;
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {   
            if (_proximasDatas == 0)
            {
                _dataInicial = DateTime.Now.AddDays(5);
            }
            else
            {
                _dataInicial = _dataInicial.AddDays(5);
            }

            CarregaPeriodos(_dataInicial);

            _proximasDatas = _proximasDatas + 5;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (_proximasDatas == 0)
            {
                _dataInicial = DateTime.Now.AddDays(-5);
            }
            else
            {
                _dataInicial = _dataInicial.AddDays(-5);
            }

            CarregaPeriodos(_dataInicial);

            _proximasDatas = _proximasDatas + 5;
        }
        #endregion

        #region " MÉTODOS AUXILIARES "
        
        private Roteiro BuscarRoteiroParaEdicao()
        {
            Roteiro agenda = new Roteiro();

            agenda.PedidoVenda = new PedidoDeVenda { Id = txtPedido.Text.ToInt() };
            agenda.Id = txtAgendamento.Text.ToInt();
            agenda.Usuario = _pessoaCadastro;
            agenda.TipoEndereco = (EnumTipoEndereco)cboTipoEndereco.EditValue;
            agenda.DataElaboracao = txtDataAgenda.Text.ToDate();
            agenda.Periodo = (EnumPeriodo)cboPeriodo.EditValue;
            agenda.TipoServico = (EnumTipoServico)cboTipoServico.EditValue;
            agenda.DetalheServico = txtDetalheServico.Text;
            agenda.Observacao = txtObservacao.Text;
            agenda.Status = EnumStatusRoteiro.EMAGENDA;
            
            return agenda;
        }

        private void BuscaAgendamentoDoPedido(int pedidoId)
        {
            if (pedidoId == 0)
                txtAgendamento.Text = "0";

            var agenda = new ServicoRoteiro().ConsultePorPedido(pedidoId);

            if(agenda != null)
            {
                txtAgendamento.Text = agenda.Id.ToString();
                cboTipoEndereco.EditValue = agenda.TipoEndereco;
                txtDataAgenda.Text = agenda.DataElaboracao.ToString("dd/MM/yyyy");
                cboPeriodo.EditValue = agenda.Periodo;
                cboTipoServico.EditValue = agenda.TipoServico;
                txtDetalheServico.Text = agenda.DetalheServico;
                txtObservacao.Text = agenda.Observacao;
            }
        }

        private void CarregaPeriodos(DateTime dataInicial)
        {
            var roteirosManha = new ServicoRoteiro().ConsulteLista(null, EnumPeriodo.MANHA, null, EnumDataFiltrarRoteiro.ELABORACAO, dataInicial.Date,
                                                                dataInicial.AddDays(5).Date, 0, 0,false);

            var roteirosTarde = new ServicoRoteiro().ConsulteLista(null, EnumPeriodo.TARDE, null, EnumDataFiltrarRoteiro.ELABORACAO, dataInicial.Date,
                                                                dataInicial.AddDays(5).Date, 0,0, false);

            ServicoParametros servParametros = new ServicoParametros();

            var parametrosVenda = servParametros.ConsulteParametros();

            int limiteManha = parametrosVenda.ParametrosVenda.LimiteDiarioManha;
            int limiteTarde = parametrosVenda.ParametrosVenda.LimiteDiarioTarde;

            //*1------>
            lblData1.Text = dataInicial.ToString("dd/MM/yyyy");
            lblDia1.Text = BuscarDescricaoDoDiaSemana((int)dataInicial.DayOfWeek);           

           //Manhã
            btnManhaData1.Text = (limiteManha - roteirosManha.Count(x=>x.DataElaboracao == dataInicial.Date)).ToString();
            btnManhaData1.BackColor = (btnManhaData1.Text.ToInt()) < 0 ? Color.Red : Color.Aqua;

            //Tarde
            btnTardeData1.Text = (limiteTarde - roteirosTarde.Count(x => x.DataElaboracao == dataInicial.Date)).ToString();            
            btnTardeData1.BackColor = (btnTardeData1.Text.ToInt()) < 0 ? Color.Red : Color.LightYellow;


            //**2------>
            lblData2.Text = dataInicial.AddDays(1).ToString("dd/MM/yyyy");
            lblDia2.Text = BuscarDescricaoDoDiaSemana((int)dataInicial.AddDays(1).DayOfWeek);

            //Manhã
            btnManhaData2.Text = (limiteManha - roteirosManha.Count(x => x.DataElaboracao == dataInicial.AddDays(1).Date)).ToString();
            btnManhaData2.BackColor = (btnManhaData2.Text.ToInt()) < 0 ? Color.Red : Color.Aqua;

            //Tarde
            btnTardeData2.Text = (limiteTarde - roteirosTarde.Count(x => x.DataElaboracao == dataInicial.AddDays(1).Date)).ToString();
            btnTardeData2.BackColor = (btnTardeData1.Text.ToInt()) < 0 ? Color.Red : Color.LightYellow;


            //***3----->
            lblData3.Text = dataInicial.AddDays(2).ToString("dd/MM/yyyy");
            lblDia3.Text = BuscarDescricaoDoDiaSemana((int)dataInicial.AddDays(2).DayOfWeek);

            //Manhã
            btnManhaData3.Text = (limiteManha - roteirosManha.Count(x => x.DataElaboracao == dataInicial.AddDays(2).Date)).ToString();
            btnManhaData3.BackColor = (btnManhaData3.Text.ToInt()) < 0 ? Color.Red : Color.Aqua;

            //Tarde
            btnTardeData3.Text = (limiteTarde - roteirosTarde.Count(x => x.DataElaboracao == dataInicial.AddDays(2).Date)).ToString();
            btnTardeData3.BackColor = (btnTardeData3.Text.ToInt()) < 0 ? Color.Red : Color.LightYellow;


            //***4----->
            lblData4.Text = dataInicial.AddDays(3).ToString("dd/MM/yyyy");
            lblDia4.Text = BuscarDescricaoDoDiaSemana((int)dataInicial.AddDays(3).DayOfWeek);

            //Manhã
            btnManhaData4.Text = (limiteManha - roteirosManha.Count(x => x.DataElaboracao == dataInicial.AddDays(3).Date)).ToString();
            btnManhaData4.BackColor = (btnManhaData4.Text.ToInt()) < 0 ? Color.Red : Color.Aqua;

            //Tarde
            btnTardeData4.Text = (limiteTarde - roteirosTarde.Count(x => x.DataElaboracao == dataInicial.AddDays(3).Date)).ToString();
            btnTardeData4.BackColor = (btnTardeData4.Text.ToInt()) < 0 ? Color.Red : Color.LightYellow;


            //***5----->
            lblData5.Text = dataInicial.AddDays(4).ToString("dd/MM/yyyy");
            lblDia5.Text = BuscarDescricaoDoDiaSemana((int)dataInicial.AddDays(4).DayOfWeek);

            //Manhã
            btnManhaData5.Text = (limiteManha - roteirosManha.Count(x => x.DataElaboracao == dataInicial.AddDays(4).Date)).ToString();
            btnManhaData5.BackColor = (btnManhaData5.Text.ToInt()) < 0 ? Color.Red : Color.Aqua;

            //Tarde
            btnTardeData5.Text = (limiteTarde - roteirosTarde.Count(x => x.DataElaboracao == dataInicial.AddDays(4).Date)).ToString();
            btnTardeData5.BackColor = (btnTardeData5.Text.ToInt()) < 0 ? Color.Red : Color.LightYellow;


            //***6----->
            lblData6.Text = dataInicial.AddDays(5).ToString("dd/MM/yyyy");
            lblDia6.Text = BuscarDescricaoDoDiaSemana((int)dataInicial.AddDays(5).DayOfWeek);

            //Manhã
            btnManhaData6.Text = (limiteManha - roteirosManha.Count(x => x.DataElaboracao == dataInicial.AddDays(5).Date)).ToString();
            btnManhaData6.BackColor = (btnManhaData6.Text.ToInt()) < 0 ? Color.Red : Color.Aqua;

            //Tarde
            btnTardeData6.Text = (limiteTarde - roteirosTarde.Count(x => x.DataElaboracao == dataInicial.AddDays(5).Date)).ToString();
            btnTardeData6.BackColor = (btnTardeData5.Text.ToInt()) < 0 ? Color.Red : Color.LightYellow;

        }
        private void validaPeriodos(DateTime dataInicial)
        {
            var roteirosManha = new ServicoRoteiro().ConsulteLista(null, EnumPeriodo.MANHA, null, EnumDataFiltrarRoteiro.ELABORACAO, dataInicial.Date,
                                                                dataInicial.AddDays(5).Date, 0, 0,false);

            var roteirosTarde = new ServicoRoteiro().ConsulteLista(null, EnumPeriodo.TARDE, null, EnumDataFiltrarRoteiro.ELABORACAO, dataInicial.Date,
                                                                dataInicial.AddDays(5).Date, 0, 0,false);

            ServicoParametros servParametros = new ServicoParametros();

            var parametrosVenda = servParametros.ConsulteParametros();

            int limiteManha = parametrosVenda.ParametrosVenda.LimiteDiarioManha;
            int limiteTarde = parametrosVenda.ParametrosVenda.LimiteDiarioTarde;
            _quantidademanha = 0;
            _quantidadetarde = 0;
            //*1------>
            lblData1.Text = dataInicial.ToString("dd/MM/yyyy");
            lblDia1.Text = BuscarDescricaoDoDiaSemana((int)dataInicial.DayOfWeek);

            //Manhã
            _quantidademanha = (limiteManha - roteirosManha.Count(x => x.DataElaboracao == dataInicial.Date)).ToInt();
            _quantidadetarde = (limiteTarde - roteirosTarde.Count(x => x.DataElaboracao == dataInicial.Date)).ToInt();


        }

        private string BuscarDescricaoDoDiaSemana(int dia)
        {
            switch (dia)
            {
                case 1:
                    return "Segunda-Feira";
                case 2:
                    return "Terça-Feira";
                case 3:
                    return "Quarta-Feira";
                case 4:
                    return "Quinta-Feira";
                case 5:
                    return "Sexta-Feira";
                case 6:
                    return "Sábado";
            }

            return "Domingo";
        }

        private void PreenchaDadosClienteEndereco(Pessoa cliente, EnumTipoEndereco tipoEndereco = EnumTipoEndereco.PRINCIPAL)
        {
            if (_clienteSelecionado == null) return;

            cliente = new ServicoPessoa().ConsulteClienteAtivo(_clienteSelecionado.Id);

            if (cliente != null && cliente.ListaDeEnderecos != null && cliente.ListaDeEnderecos.Count > 0)
            {
                var endereco = cliente.ListaDeEnderecos.FirstOrDefault(end => end.TipoEndereco == tipoEndereco);

                if (endereco == null)
                {
                    endereco = cliente.ListaDeEnderecos.First();
                }

                txtEnderecoCliente.Text = endereco.Rua + " - Cep: " + endereco.CEP + " - Bairro: " + endereco.Bairro + 
                                           " - Numero: " + endereco.Numero + " - Complemento: " + endereco.Complemento;
            }
            else
            {
                txtEnderecoCliente.Text = string.Empty;
            }
        }

        private void PreenchaCboTipoEndereco()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoEndereco>();

            lista.Insert(0, null);

            cboTipoEndereco.Properties.DisplayMember = "Descricao";
            cboTipoEndereco.Properties.ValueMember = "Valor";
            cboTipoEndereco.Properties.DataSource = lista;
        }

        private void PreenchaPeriodos()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumPeriodo>();
           
            cboPeriodo.Properties.DataSource = lista;
            cboPeriodo.Properties.ValueMember = "Valor";
            cboPeriodo.Properties.DisplayMember = "Descricao";

            cboPeriodo.EditValue = EnumPeriodo.MANHA;
        }

        private void PreenchaTipoServico()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoServico>();

            lista.Insert(0, null);

            cboTipoServico.Properties.DataSource = lista;
            cboTipoServico.Properties.ValueMember = "Valor";
            cboTipoServico.Properties.DisplayMember = "Descricao";            
        }

        protected virtual ServicoContasPagarReceber RetorneServicoContasPagarOuReceber()
        {
            return null;
        }

        private void LimpeFormulario()
        {
            //Endereço            
            cboTipoEndereco.EditValue = EnumTipoEndereco.PRINCIPAL;
            PreenchaDadosClienteEndereco(_clienteSelecionado);

            _proximasDatas = 0;

            CarregaPeriodos(DateTime.Now);

            txtDataAgenda.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            
            cboPeriodo.EditValue = EnumPeriodo.MANHA;

            cboTipoServico.EditValue = 0;

            txtDetalheServico.Text = string.Empty;

            txtObservacao.Text = string.Empty;
        }

        protected virtual void InformeUsuarioContasAPagarReceber(Pessoa pessoaQueCadastrou)
        {
            _pessoaCadastro = pessoaQueCadastrou;
            txtUsuario.Text = pessoaQueCadastrou.Id + " - " + pessoaQueCadastrou.DadosGerais.Razao;
        }

        private void ImprimaPedidoDeVenda(int idPedidoDeVenda)
        {   
            if (!rdbUmaVia.Checked && !rdbDuasVias.Checked)
            {
                RelatorioPedidoVenda relatorio = new RelatorioPedidoVenda(idPedidoDeVenda, (EnumTipoEndereco)cboTipoEndereco.EditValue);
                relatorio.GereRelatorio();

                using (ReportPrintTool printTool = new ReportPrintTool(relatorio))
                {
                    // Invoke the Ribbon Print Preview form modally, 
                    // and load the report document into it.
                    printTool.ShowRibbonPreviewDialog();

                    // Invoke the Ribbon Print Preview form
                    // with the specified look and feel setting.
                    printTool.ShowRibbonPreview();
                }
            }
            else if (rdbUmaVia.Checked)
            {
                RelatorioPedidoVendaAgenda relatorio = new RelatorioPedidoVendaAgenda(idPedidoDeVenda, (EnumTipoEndereco)cboTipoEndereco.EditValue);
                relatorio.GereRelatorio();

                using (ReportPrintTool printTool = new ReportPrintTool(relatorio))
                {
                    // Invoke the Ribbon Print Preview form modally, 
                    // and load the report document into it.
                    printTool.ShowRibbonPreviewDialog();

                    // Invoke the Ribbon Print Preview form
                    // with the specified look and feel setting.
                    printTool.ShowRibbonPreview();
                }
            }
            else
            {
                RelatorioPedidoVendaDuasViasAgenda relatorioDuasVias = new RelatorioPedidoVendaDuasViasAgenda(idPedidoDeVenda, (EnumTipoEndereco)cboTipoEndereco.EditValue);
                relatorioDuasVias.GereRelatorio();

                using (ReportPrintTool printTool = new ReportPrintTool(relatorioDuasVias))
                {
                    // Invoke the Ribbon Print Preview form modally, 
                    // and load the report document into it.
                    printTool.ShowRibbonPreviewDialog();

                    // Invoke the Ribbon Print Preview form
                    // with the specified look and feel setting.
                    printTool.ShowRibbonPreview();
                }
            }
            
            rdbUmaVia.Checked = false;
            rdbDuasVias.Checked = false;
        }


        #endregion

        #region " CLASSES AUXILIARES "

        #endregion

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("O agendamento será Excluídos.\n\nDeseja continuar?", "Excluir Agendamento", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            var roteiro = new ServicoRoteiro().Consulte(txtAgendamento.Text.ToInt());

            if(roteiro == null)
            {
                MessageBox.Show("O agendamento ainda não foi criado para este pedido!", "Excluir Agendamento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            if(roteiro.Status == EnumStatusRoteiro.CONCLUIDO)
            {
                MessageBox.Show("O Status desse agendamento está Realizado.\n\nNão pode ser excluído!", "Excluir Agendamento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {   
                ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

                servicoRoteiro.AtualizeExclusaoAgenda(roteiro.Id, txtPedido.Text.ToInt());
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                throw ex;
            }

            this.Cursor = Cursors.Default;

            MessageBox.Show("Agendamento excluído com sucesso!", "Exclusão de Agenda", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimaPedidoDeVenda(txtPedido.Text.ToInt());
        }

        private void cboTipoServico_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
