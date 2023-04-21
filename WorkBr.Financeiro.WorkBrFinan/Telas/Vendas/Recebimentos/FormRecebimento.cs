using System;
using Programax.Easy.Negocio.Vendas.RecebimentoObj.ObjetoDeNegocio;
using Programax.Easy.Report.RelatoriosDevExpress.Financeiro;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.RecebimentoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Financeiro.Cheques;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using System.Windows.Forms;
using Programax.Easy.Negocio.Vendas.Enumeradores; 
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.View.Telas.Fiscal.NotasFiscais;
using Programax.Easy.Negocio.Integracao.PreVendaDjpdvObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Integracao.PreVendaDjpdvServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;
using System.Collections.Generic;
using Programax.Easy.Servico.Financeiro.ChequeServ;
using System.Linq;
using Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.OperadorasCartaoServ;
using Programax.Easy.View.Telas.Financeiro.OperadorasCartoes;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;
using Programax.Easy.View.Telas.Financeiro.BancosParaMovimento;
using Programax.Easy.View.Telas.Financeiro.Categorias;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.CategoriaServ;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;
using System.Data;
using MySql.Data.MySqlClient;

namespace Programax.Easy.View.Telas.Vendas.Recebimentos
{
    public partial class FormRecebimento : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Recebimento _recebimento;

        private Parametros _parametros;

        private List<Cheque> _listaDeCheques;
        private string ConectionString;
        private DataSet mDataSet;
        private MySqlDataAdapter mAdapter;
        private Boolean habilitado = false;
        private int dias = 0;
        private int validade = 0;
        private double valor = 0;
        private double percentual = 0;
        private double valorCash = 0;
        private int CodigoCash = 0;

        #endregion

        #region " PROPRIEDADES "

        public FormCadastroNotaFiscal FormCadastroNotaFiscal { get; set; }

        #endregion

        #region " CONSTRUTOR "

        public FormRecebimento()
        {
            InitializeComponent();
            LeiaArquivoTextoCheckEmitirNfEPedido();
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public void converteListaRecebimento(RecebimentoNf item)
        {           
            Recebimento itemRecebimento = new Recebimento();

            itemRecebimento.AtendenteId = item.AtendenteId;
            itemRecebimento.AtendenteNomeFantasia = item.AtendenteNomeFantasia;
            itemRecebimento.BancoParaMovimento = item.BancoParaMovimento;
            itemRecebimento.CategoriaFinanceira = item.CategoriaFinanceira;
            itemRecebimento.CidadeDescricao = item.CidadeDescricao;
            itemRecebimento.CidadeId = item.CidadeId;
            itemRecebimento.ClienteCpfCnpj = item.ClienteCpfCnpj;
            itemRecebimento.ClienteDataCadastro = item.ClienteDataCadastro;
            itemRecebimento.ClienteId = item.ClienteId;
            itemRecebimento.ClienteInscricaoEstadual = item.ClienteInscricaoEstadual;
            itemRecebimento.ClienteNomeFantasia = item.ClienteNomeFantasia;
            itemRecebimento.ClienteStatus = item.ClienteStatus;
            itemRecebimento.ClienteTipoPessoa = item.ClienteTipoPessoa;
            itemRecebimento.DataElaboracao = item.DataElaboracao;
            itemRecebimento.DataFechamento = item.DataFechamento;
            itemRecebimento.Desconto = item.Desconto;
            itemRecebimento.DescontoEhPercentual = item.DescontoEhPercentual;
            itemRecebimento.EnderecoCep = item.EnderecoCep;
            itemRecebimento.EstadoNome = item.EstadoNome;
            itemRecebimento.EstadoUf = item.EstadoUf;
            itemRecebimento.Id = item.Id;
            itemRecebimento.ListaParcelasRecebimento = item.ListaParcelasRecebimento;
            itemRecebimento.NaturezaDescricao = item.NaturezaDescricao;
            itemRecebimento.NaturezaId = item.NaturezaId;
            itemRecebimento.StatusDocumento = item.StatusDocumento;
            itemRecebimento.TipoDocumento = item.TipoDocumento;
            itemRecebimento.UsuarioId = item.UsuarioId;
            itemRecebimento.UsuarioNomeFantasia = item.UsuarioNomeFantasia;
            itemRecebimento.ValorTotal = item.ValorTotal;
            itemRecebimento.VendedorId = item.VendedorId;
            itemRecebimento.VendedorNomeFantasia = item.VendedorNomeFantasia;
            itemRecebimento.VendedorNomeRazao = item.ClienteNomeRazao;

            _recebimento = itemRecebimento;            
        }
        
        public void RecebaDocumento(Recebimento recebimento)
        {
            var paramentros = new ServicoParametros().ConsulteParametros();

            ServicoRecebimento servicoRecebimento = new ServicoRecebimento();

            if (paramentros.ParametrosFiscais.EmitirNotaSemReceber)
            {
                var recebimentoNF = servicoRecebimento.ConsulteNf(recebimento.Id, recebimento.TipoDocumento);
                converteListaRecebimento(recebimentoNF);
            }
            else
            {   
                _recebimento = servicoRecebimento.Consulte(recebimento.Id, recebimento.TipoDocumento);
            }           

            PreenchaCabecalho();
            PreenchaTotaisFormaPagamento();
            
            PreenchaTotalVenda();
            AltereNomeBotaoDeAcordoComTipoRecebimento();            
            HabilitaConciliacaoBancaria();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void cboOperadorasDebito_EditValueChanged(object sender, EventArgs e)
        {
            if (cboOperadorasDebito.EditValue != null)
            {
                var operadora = new ServicoOperadorasCartao().Consulte(cboOperadorasDebito.EditValue.ToInt());

                if (operadora != null)
                    cboBanco.EditValue = operadora.BancoParaMovimento.Id;
            }
        }

        private void cboOperadorasCredito_EditValueChanged(object sender, EventArgs e)
        {
            var operadora = new ServicoOperadorasCartao().Consulte(cboOperadorasCredito.EditValue.ToInt());

            var parcela = lblQuantidadeParcelasCartaoCredito.Text.Replace("x", " ").Trim();

            if(operadora != null)
            {
                cboBanco.EditValue = operadora.BancoParaMovimento.Id;

                if (!operadora.PermiteParcelamento && parcela.ToInt() > 1)
                {
                    MessageBox.Show("A operadora selecionada não permite parcelamento. Escolha outra para continuar!",
                                     "Operadoras de Cartão", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cboOperadorasCredito.EditValue = null;
                    return;
                }   
            }
        }

        private void btnAdicionarCategoria_Click(object sender, EventArgs e)
        {
            FormCadastroCategoriasFinanceiras formCategoria = new FormCadastroCategoriasFinanceiras();
            formCategoria.ShowDialog();

            PreenchaCboCategorias(EnumTipoOperacaoContasPagarReceber.RECEBER);

            cboCategoriaFinanceira.EditValue = null;
        }

        private void btnAdicionarBanco_Click(object sender, EventArgs e)
        {
            FormCadastroBancoParaMovimento formbanco = new FormCadastroBancoParaMovimento();
            formbanco.ShowDialog();

            PreenchaCboBancos();

            cboBanco.EditValue = null;
        }

        private void btnAdicionarOperadorasDebito_Click(object sender, EventArgs e)
        {
            var retorno = new FormCadastroOperadorasCartao().ShowDialog();

            CarregaComboOperadorasDebitoCredito();

            cboOperadorasDebito.EditValue = null;
        }

        private void btnAdicionarOperadorasCredito_Click(object sender, EventArgs e)
        {
            var retorno = new FormCadastroOperadorasCartao().ShowDialog();

            CarregaComboOperadorasDebitoCredito();

            cboOperadorasCredito.EditValue = null;
        }
        private void VerificaCashBack()
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
            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();
                string Sql = "Select * from configcashback";
                // string Query = "update student.studentinfo set idStudentInfo='" + this.IdTextBox.Text + "',Name='" + this.NameTextBox.Text + "',Father_Name='" + this.FnameTextBox.Text + "',Age='" + this.AgeTextBox.Text + "',Semester='" + this.SemesterTextBox.Text + "' where idStudentInfo='" + this.IdTextBox.Text + "';";


                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;

               
                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    dias = 0;
                    validade = 0;
                    int habilitar = returnValue["habilitar"].ToInt();
                    valor = returnValue["valor"].ToDouble();
                    dias = returnValue["dias"].ToInt();
                    validade = returnValue["validade"].ToInt();
                    percentual = returnValue["percentual"].ToDouble();
                    habilitado = false;
                    if (habilitar == 1)
                    {
                        if (txtTotalPagar.Text.ToDouble() >= valor.ToDouble())
                        {
                            habilitado = true;
                        }
                    }
                }

            }



        }
        private void GravarCash()
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
            int CodigoCli = txtIdParceiro.Text.ToInt();
            DateTime DataCompra = DateTime.Now;


            int status = 0;
            var value = String.Format("{0:N0}", txtTotalPagar.Text);
            string valor2 = value.Replace(".", "");
            string valor = valor2.Replace(",", ".");
           
            using (var conn = new MySqlConnection(ConectionString))

            {
                conn.Open();

                MySqlCommand command = new MySqlCommand("INSERT INTO cashback (cod_cli,datacompra, valor,status)" +
                "VALUES(" + CodigoCli + ",'" + DataCompra.ToString("yyyy-MM-dd HH:mm:ss") + "','" + valor + "'," + status + ")", conn);

                command.ExecuteNonQuery();
                conn.Close();


            }

        }
        private void AlteraCash()
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
          

            int status = 1;
           
            using (var conn = new MySqlConnection(ConectionString))

            {
                conn.Open();

                string Sql = "update cashback set status= " + status  +

                            " where Codigo=" + CodigoCash + ";";

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();


            }

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
                int statusFaturado = 0;

                if(_parametros.ParametrosVenda.StatusFaturado !=true)
                {
                    statusFaturado = 1;
                }
                else
                {
                    statusFaturado = 2;
                }

                string Sql = "update historicosatendimento set hisat_status= " + statusFaturado +

                            " where hisat_novo_pedido_id=" + _recebimento.Id ;

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();


            }

        }
        private void btnConcluirEntrada_Click(object sender, EventArgs e)
        {
            //Valida se tiver usando conciliação bancária é obrigado a informar categoria
            if (_parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria && cboCategoriaFinanceira.EditValue == null)
            {
                MessageBox.Show("Você está usando a Conciliação Bancária. É obrigatório informar a Categoria Financeira.", "Recebimento");
                return;
            }

            if (txtTotalPagar.Text.ToDouble() != txtValorPago.Text.ToDouble())
            {
                MessageBox.Show("Valor Pago está diferente do total a pagar.", "Valor pago inconsistente", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!ExistemChequesCadastradosParaEsteRecebimento()) return;

            Action actionReceber = () =>
            {
                FatureDocumento();

                

                if (chkImprimirPedido.Checked && _recebimento.TipoDocumento == EnumTipoDocumentoRecebimento.PEDIDODEVENDAS)
                    ImprimaPedidoDeVenda(_recebimento.Id);

                if (chkPromissoria.Checked && txtCrediarioProprio.Text != "0,00")
                    ImprimaPromissoria();

                EmitaNotaFiscal();
                ExporteParaPdv();
                
                this.Close();
            };
            if (txtTotalPagar.Text.ToDouble() >= valor.ToDouble())
            {
                if (habilitado == true)
                {
                    GravarCash();
                }
               
            }
            AlteraPedidoStatus();


            TratamentosDeTela.TrateInclusaoEAtualizacao(actionReceber, exibirMensagemDeSucesso: false);
            if (habilitado == true)
            {
                AlteraCash();
            }
        }

        private void btnCadastrarCheque_Click(object sender, EventArgs e)
        {
            if (txtCheque.Text.ToDouble() != 0)
            {

                FormCadastroCheque formCadastroCheque = new FormCadastroCheque(_recebimento.Id, _recebimento.ListaParcelasRecebimento.ToList(), txtCheque.Text.ToDouble(), txtIdParceiro.Text.ToInt(), _recebimento.ClienteNomeFantasia.ToString());

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                var cliente = servicoPessoa.Consulte(_recebimento.ClienteId);

                formCadastroCheque.CadastreNovoCheque(cliente);
            }
            else
            {
                MessageBox.Show("Para cadastrar cheques é necessário que a forma de pagamento do Pedido seja Cheque", "Recebimento - Cadastrar Cheque");
                return;
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            //Se houver cheques cadastrados, vai excluir, se o usuário sair do formulário de recebimento
            if (txtCheque.Text.ToDouble() != 0)
            {
                ServicoCheque servicoCheque = new ServicoCheque();

                var chequesCadastradosDestePedido = servicoCheque.ConsulteListaChequePorPedido(_recebimento.Id);
                                
                foreach (var item in chequesCadastradosDestePedido)
                {
                    servicoCheque.Exclua(item.Id);
                }                
            }

            this.FecharFormulario();
        }

        private void txtDinheiro_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaValorPago();
        }

        private void txtCartaoDebito_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaValorPago();
        }

        private void txtCartaoCredito_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaValorPago();
        }

        private void txtCheque_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaValorPago();
        }

        private void txtDuplicata_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaValorPago();
        }

        private void txtBoletoBancario_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaValorPago();
        }

        private void txtCreditoInterno_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaValorPago();
        }

        private void txtCrediarioProprio_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaValorPago();
        }

        private void txtDepositoBancario_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaValorPago();
        }

        private void txtPix_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaValorPago();
        }

        private void chkImprimirPedido_CheckedChanged(object sender, EventArgs e)
        {
            GravaArquivoTextoCheckEmitirNfEPedido(chkImprimirPedido.Checked.ToString(), chkEmitirNotaFiscal.Checked.ToString());
        }

        private void chkEmitirNotaFiscal_CheckedChanged(object sender, EventArgs e)
        {
            GravaArquivoTextoCheckEmitirNfEPedido(chkImprimirPedido.Checked.ToString(), chkEmitirNotaFiscal.Checked.ToString());
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboCategorias(EnumTipoOperacaoContasPagarReceber tipoOperacao)
        {
            var tipoCategoria = tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? EnumTipoCategoria.RECEITA : EnumTipoCategoria.DESPESA;

            List<CategoriaFinanceira> categoria = new List<CategoriaFinanceira>();

            categoria = new ServicoCategoria().ConsulteLista(string.Empty, null, "A", tipoCategoria);

            categoria.Insert(0, null);

            cboCategoriaFinanceira.Properties.DisplayMember = "Descricao";
            cboCategoriaFinanceira.Properties.ValueMember = "Id";
            cboCategoriaFinanceira.Properties.DataSource = categoria;

            if (cboCategoriaFinanceira.EditValue != null)
            {
                if (!categoria.Exists(categ => categ != null && categ.Id == cboCategoriaFinanceira.EditValue.ToInt()))
                {
                    cboCategoriaFinanceira.EditValue = null;
                }
            }
        }

        private void PreenchaCboBancos()
        {
            List<BancoParaMovimento> banco = new List<BancoParaMovimento>();

            banco = new ServicoBancoParaMovimento().ConsulteLista(string.Empty, "A");

            if (banco.Count == 0) return;

            var idbanco = banco.Find(x => x.TornarPadrao == true).Id;

            banco.Insert(0, null);

            cboBanco.Properties.DisplayMember = "NomeBanco";
            cboBanco.Properties.ValueMember = "Id";
            cboBanco.Properties.DataSource = banco;

            cboBanco.EditValue = idbanco;

            if (cboBanco.EditValue != null)
            {
                if (!banco.Exists(banc => banc != null && banc.Id == cboBanco.EditValue.ToInt()))
                {
                    cboBanco.EditValue = null;
                }
            }
        }

        private void HabilitaConciliacaoBancaria()
        {
            RetorneParametros();

            if (_parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
            {
                this.Height = 559;
                cboCategoriaFinanceira.Enabled = true;
                cboBanco.Enabled = true;
               
                btnAdicionarCategoria.Visible = true;
                btnAdicionarCategoria.Enabled = false;

                if (txtCartaoDebito.Text.ToDouble() > 0)
                {
                    btnAdicionarOperadorasDebito.Visible = true;
                    btnAdicionarOperadorasDebito.Enabled = true;

                    lblDebito.Visible = true;
                    cboOperadorasDebito.Visible = true;
                    cboOperadorasDebito.Enabled = true;

                    CarregaComboOperadorasDebitoCredito();
                }
                    
                if (txtCartaoCredito.Text.ToDouble() > 0)
                {
                    btnAdicionarOperadorasCredito.Visible = true;
                    btnAdicionarOperadorasCredito.Enabled = true;

                    lblCredito.Visible = true;
                    cboOperadorasCredito.Visible = true;
                    cboOperadorasCredito.Enabled = true;

                    CarregaComboOperadorasDebitoCredito();
                }

                PreenchaCboBancos();
                PreenchaCboCategorias(EnumTipoOperacaoContasPagarReceber.RECEBER);
                cboCategoriaFinanceira.EditValue = 2; //Recebimento Operacionais - > Padrão. 15/08/2021
            }
            else
            {
                cboCategoriaFinanceira.Enabled = false;
                cboBanco.Enabled = false;
                lblCredito.Visible = false;
                lblDebito.Visible = false;
                cboOperadorasCredito.Visible = false;
                cboOperadorasDebito.Visible = false;
                this.Height = 500;

                btnAdicionarOperadorasCredito.Visible = false;
                btnAdicionarOperadorasDebito.Visible = false;
                btnAdicionarBanco.Visible = false;
                btnAdicionarCategoria.Visible = false;
            }
                
        }

        private void CarregaComboOperadorasDebitoCredito()
        {
            ServicoOperadorasCartao servicoOperadorasCartao = new ServicoOperadorasCartao();

            var operadoras = servicoOperadorasCartao.ConsulteLista();

            List<ObjetoParaComboBox> listaDebito = new List<ObjetoParaComboBox>();
            
            foreach (var item in operadoras)
            {
                ObjetoParaComboBox objeto = new ObjetoParaComboBox();

                if (!item.PermiteParcelamento)
                {
                    objeto.Descricao = item.Descricao;
                    objeto.Valor = item.Id;

                    listaDebito.Add(objeto);
                }
                   
            }

            cboOperadorasDebito.Properties.DisplayMember = "Descricao";
            cboOperadorasDebito.Properties.ValueMember = "Valor";
            cboOperadorasDebito.Properties.DataSource = listaDebito;

            cboOperadorasCredito.Properties.DisplayMember = "Descricao";
            cboOperadorasCredito.Properties.ValueMember = "Id";
            cboOperadorasCredito.Properties.DataSource = operadoras;
        }

        private void PreenchaCabecalho()
        {
            txtIdParceiro.Text = _recebimento.ClienteId.ToString();
            txtNomeFantasia.Text = _recebimento.ClienteNomeFantasia;
            txtValorAPagar.Text = _recebimento.ValorTotal.ToString("#,###,##0.00");
        }

        private void PreenchaTotaisFormaPagamento()
        {
            double totalDinheiro = 0;
            double totalCheque = 0;
            double totalCreditoInterno = 0;

            double totalBoletoBancario = 0;
            double totalDuplicata = 0;
            double totalCrediarioProprio = 0;

            double totalCartaoCredito = 0;
            double totalCartaoDebito = 0;
            double totalDepositoBancario = 0;

            double totalPix = 0;

            int quantasVezesCartaoCredito = 0;

            foreach (var parcela in _recebimento.ListaParcelasRecebimento)
            {
                switch ((EnumTipoFormaPagamento)parcela.FormaPagamentoId)
                {
                    case EnumTipoFormaPagamento.CREDITOINTERNO:
                        totalCreditoInterno += parcela.Valor;


                        break;
                    case EnumTipoFormaPagamento.DINHEIRO:
                        totalDinheiro += parcela.Valor;

                        break;
                    case EnumTipoFormaPagamento.BOLETOBANCARIO:
                        totalBoletoBancario += parcela.Valor;

                        break;
                    case EnumTipoFormaPagamento.DEPOSITOBANCARIO:
                        totalDepositoBancario += parcela.Valor;

                        break;
                    case EnumTipoFormaPagamento.CHEQUE:
                        totalCheque += parcela.Valor;

                        break;
                    case EnumTipoFormaPagamento.DUPLICATA:
                        totalDuplicata += parcela.Valor;

                        break;
                    case EnumTipoFormaPagamento.CREDIARIOPROPRIO:
                        totalCrediarioProprio += parcela.Valor;

                        break;
                    case EnumTipoFormaPagamento.CARTAOCREDITO:
                        totalCartaoCredito += parcela.Valor;
                        quantasVezesCartaoCredito++;
                        cboOperadorasCredito.EditValue = parcela.OperadorasCartao != null ? parcela.OperadorasCartao.Id : 0;

                        break;
                    case EnumTipoFormaPagamento.CARTAODEBITO:
                        totalCartaoDebito += parcela.Valor;
                        cboOperadorasDebito.EditValue = parcela.OperadorasCartao != null ? parcela.OperadorasCartao.Id : 0;

                        break;

                    case EnumTipoFormaPagamento.PIX:
                        totalPix += parcela.Valor;                        

                        break;
                }

                txtDinheiro.Text = totalDinheiro.ToString("#,###,##0.00");
                txtCheque.Text = totalCheque.ToString("#,###,##0.00");
                txtCreditoInterno.Text = totalCreditoInterno.ToString("#,###,##0.00");

                txtBoletoBancario.Text = totalBoletoBancario.ToString("#,###,##0.00");
                txtDuplicata.Text = totalDuplicata.ToString("#,###,##0.00");
                txtCrediarioProprio.Text = totalCrediarioProprio.ToString("#,###,##0.00");

                txtCartaoCredito.Text = totalCartaoCredito.ToString("#,###,##0.00");
                txtCartaoDebito.Text = totalCartaoDebito.ToString("#,###,##0.00");
                txtDepositoBancario.Text = totalDepositoBancario.ToString("#,###,##0.00");

                txtPix.Text = totalPix.ToString("#,###,##0.00");

                lblQuantidadeParcelasCartaoCredito.Text = "x " + quantasVezesCartaoCredito;
            }
        }
        private void VerificaDesconto()
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
            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();
                string Sql = "Select * from cashback Where cod_cli = " + txtIdParceiro.Text + " And status = 0";

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    CodigoCash = returnValue["Codigo"].ToInt();
                    DateTime DataValidade = new DateTime();
                    string DataV = returnValue["datacompra"].ToString();
                    DataValidade = DataV.ToDate();
                   
                    DateTime data2 = DataValidade.AddDays(validade);

                    DateTime DataValida = new DateTime();
                    string Data = returnValue["datacompra"].ToString();
                    DataValida = Data.ToDate();
                    
      
                    DateTime data3 = DataValida.AddDays(dias);

                    double ValorCompra = returnValue["valor"].ToDouble();
                    if (DateTime.Now >= data3)
                    {
                        if (DateTime.Now <= data2)
                        {
                            valorCash = (ValorCompra.ToDouble()) * percentual.ToDouble()/100 ;
                            txtcashback.Text = valorCash.ToString("#,###,##0.00");

                        }
                    }
                    else
                    {
                        txtcashback.Text = "0,00";
                    }    
                
                }

            }


        }
        private void PreenchaTotalVenda()
        {
            
            
            double valorTotalBruto = _recebimento.Desconto;
            double valorDesconto = 0;

           

            if (_recebimento.DescontoEhPercentual)
            {
                valorDesconto = Math.Round(_recebimento.ValorTotal * _recebimento.Desconto / (double)100, 2);

            }

            valorTotalBruto = _recebimento.ValorTotal - valorDesconto ;

            txtDesconto.Text = valorDesconto.ToString("#,###,##0.00");
            txtTotal.Text = valorTotalBruto.ToString("#,###,##0.00");
            txtTotalPagar.Text = _recebimento.ValorTotal.ToString("#,###,##0.00");
            VerificaCashBack();
            if (habilitado == true)
            {
                VerificaDesconto();
            }
            valorTotalBruto = _recebimento.ValorTotal - valorDesconto - valorCash;
            if (habilitado == true)
            {
                txtDesconto.Text = valorDesconto.ToString("#,###,##0.00");
               
               
                txtTotalPagar.Text = valorTotalBruto.ToString("#,###,##0.00");
                txtValorPago.Text = valorTotalBruto.ToString("#,###,##0.00");
            }
        }

        private void PreenchaValorPago()
        {
            var somaDeTodasAsFormasPagamento = txtDinheiro.Text.ToDouble() + txtCartaoDebito.Text.ToDouble() + txtCartaoCredito.Text.ToDouble() +
                                                                        txtCheque.Text.ToDouble() + txtDuplicata.Text.ToDouble() + txtBoletoBancario.Text.ToDouble() +
                                                                        txtCreditoInterno.Text.ToDouble() + txtCrediarioProprio.Text.ToDouble() 
                                                                        + txtDepositoBancario.Text.ToDouble() + txtPix.Text.ToDouble();


            txtValorPago.Text = somaDeTodasAsFormasPagamento.ToString("#,###,##0.00");
        }

        private void AltereNomeBotaoDeAcordoComTipoRecebimento()
        {
            if (_recebimento.TipoDocumento == EnumTipoDocumentoRecebimento.PEDIDODEVENDAS)
            {
                btnConcluirRecebimento.Text = " Concluir Venda";
            }
            else if (_recebimento.TipoDocumento == EnumTipoDocumentoRecebimento.TROCAPEDIDODEVENDAS)
            {
                btnConcluirRecebimento.Text = " Concluir Troca";
            }
        }

        private void FatureDocumento()
        {
            double pix = txtPix.Text.ToDouble() ;
            double dinheiro = txtDinheiro.Text.ToDouble() ;
            double cartaoDebito = txtCartaoDebito.Text.ToDouble();
            double cartaoCredito = txtCartaoCredito.Text.ToDouble();
            double cheque = txtCheque.Text.ToDouble();
            double cashback = txtcashback.Text.ToDouble();

            InsiraParametroConciliacaoBancaria();

            ServicoRecebimento servicoRecebimento = new ServicoRecebimento();
            servicoRecebimento.FatureRecebimento(_recebimento,cashback, pix,dinheiro, cartaoDebito, cartaoCredito, cheque, _listaDeCheques);

            MessageBox.Show("Documento Recebido Com Sucesso!", "Documento Recebido");
        }

        private void InsiraParametroConciliacaoBancaria()
        {
            if (_parametros == null) return;

            if (_parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
            {
                _recebimento.CategoriaFinanceira = cboCategoriaFinanceira.EditValue != null ? new CategoriaFinanceira { Id = cboCategoriaFinanceira.EditValue.ToInt() } : null;
                _recebimento.BancoParaMovimento = cboBanco.EditValue != null ? new BancoParaMovimento { Id = cboBanco.EditValue.ToInt() } : null;

                foreach (var item in _recebimento.ListaParcelasRecebimento)
                {

                    item.OperadorasCartao = item.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO ? cboOperadorasDebito.EditValue != null ?
                                                                        new OperadorasCartao { Id = cboOperadorasDebito.EditValue.ToInt() } : null :
                                                                        cboOperadorasCredito.EditValue != null ?
                                                                        new OperadorasCartao { Id = cboOperadorasCredito.EditValue.ToInt() } : null;


                }
            }
        }

        private void EmitaNotaFiscal()
        {
            try
            {
                if (_recebimento.TipoDocumento == EnumTipoDocumentoRecebimento.PEDIDODEVENDAS)
                {
                    var parametros = RetorneParametros();

                    if (parametros.ParametrosFinanceiro.QuestionarSeDesejaEmitirNotaAoReceberPedido)
                    {
                        if (chkEmitirNotaFiscal.Checked)//(MessageBox.Show("Deseja emitir nota deste documento?", "Emissão de nota", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            if (_recebimento.TipoDocumento == EnumTipoDocumentoRecebimento.PEDIDODEVENDAS)
                            {
                                FormCadastroNotaFiscal = new FormCadastroNotaFiscal();
                                FormCadastroNotaFiscal.EditePedidoDeVendaId(_recebimento.Id);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void ExporteParaPdv()
        {
            if (_recebimento.TipoDocumento == EnumTipoDocumentoRecebimento.PEDIDODEVENDAS)
            {
                var parametros = RetorneParametros();

                if (parametros.ParametrosFinanceiro.QuestionarSeDesejaExportarVendaParaPdv)
                {
                    if (MessageBox.Show("Deseja exportar esta venda para o pdv?", "Exportar venda para pdv", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Action actionExportacao = () =>
                        {
                            PreVendaDjpdv preVenda = new PreVendaDjpdv();
                            preVenda.PedidoDeVendaId = _recebimento.Id;

                            ServicoPreVendaDjpdv servicoPreVendaDjpdv = new ServicoPreVendaDjpdv();
                            servicoPreVendaDjpdv.Cadastre(preVenda);
                        };

                        TratamentosDeTela.TrateInclusaoEAtualizacao(actionExportacao,
                                                                                            mensagemDeSucesso: "Venda exportada para PDV.",
                                                                                            tituloMensagemDeErro: "Erro ao exportar venda",
                                                                                            tituloMensagemDeSucesso: "Venda exportada com sucesso");

                    }
                }
            }
        }

        private Parametros RetorneParametros()
        {
            if (_parametros == null)
            {
                ServicoParametros servicoParametros = new ServicoParametros();
                _parametros = servicoParametros.ConsulteParametros();
            }

            return _parametros;
        }

        private void ImprimaPedidoDeVenda(int idPedidoDeVenda)
        {
            RelatorioPedidoVenda relatorio = new RelatorioPedidoVenda(idPedidoDeVenda, EnumTipoEndereco.PRINCIPAL);
            relatorio.GereRelatorio();

            using (ReportPrintTool printTool = new ReportPrintTool(relatorio))
            {
                // Invoke the Ribbon Print Preview form modally, 
                // and load the report document into it.
                printTool.ShowRibbonPreviewDialog();

                // Invoke the Ribbon Print Preview form
                // with the specified look and feel setting.
                printTool.ShowRibbonPreview(UserLookAndFeel.Default);
            }
        }
        
        private void ImprimaPromissoria()
        {
            ContaPagarReceber contaPagarReceber = new ContaPagarReceber();
            List<ContaPagarReceber> listaContaPagarReceber = new List<ContaPagarReceber>();

            contaPagarReceber.Pessoa = new Negocio.Cadastros.PessoaObj.ObjetoDeNegocio.Pessoa { Id = _recebimento.ClienteId };

            contaPagarReceber.DataEmissao = _recebimento.ListaParcelasRecebimento.Count > 0 ? _recebimento.DataFechamento:DateTime.Now;

            listaContaPagarReceber.Add(contaPagarReceber);

            //***Promissória
            RelatorioNotaPromissoria relatorioNotaPromissoria = new RelatorioNotaPromissoria(listaContaPagarReceber, _recebimento.Id);
            TratamentosDeTela.ExibirRelatorio(relatorioNotaPromissoria);

            //***Carnê
            RelatorioCarnePagamento relatorioCarne = new RelatorioCarnePagamento(listaContaPagarReceber, _recebimento.Id);
            TratamentosDeTela.ExibirRelatorio(relatorioCarne);

        }

        private void GravaArquivoTextoCheckEmitirNfEPedido(string imprirmirPedido, string emitirNF)
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            var parametros = servicoParametros.ConsulteParametros();
            string caminhoACBR = "C:\\Programax";

            if (!string.IsNullOrEmpty(parametros.ParametrosCadastros.CaminhoACBR))
            {
                caminhoACBR = parametros.ParametrosCadastros.CaminhoACBR;
            }

            string lines = imprirmirPedido + "," + emitirNF;
            System.IO.File.WriteAllText(@caminhoACBR+"\\CheckEmitirNfEPedidoRecebimento.txt", lines);
        }

        private void LeiaArquivoTextoCheckEmitirNfEPedido()
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            var parametros = servicoParametros.ConsulteParametros();
            string caminhoACBR = "C:\\Programax";

            if (!string.IsNullOrEmpty( parametros.ParametrosCadastros.CaminhoACBR))
            {
                caminhoACBR = parametros.ParametrosCadastros.CaminhoACBR;
            }

            string textoArquivo;

            if (System.IO.File.Exists(@caminhoACBR+"\\CheckEmitirNfEPedidoRecebimento.txt"))
            {
                textoArquivo = System.IO.File.ReadAllText(@caminhoACBR+"\\CheckEmitirNfEPedidoRecebimento.txt");

                if (textoArquivo != string.Empty)
                {
                    string[] nFPedido = textoArquivo.Split(',');

                    chkImprimirPedido.Checked = nFPedido[0] == "True" ? true : false;
                    chkEmitirNotaFiscal.Checked = nFPedido[1] == "True" ? true : false;
                }
            }
        }

        private bool ExistemChequesCadastradosParaEsteRecebimento()
        {
            if (txtCheque.Text.ToDouble() != 0)
            {
                ServicoCheque servicoCheque = new ServicoCheque();

                var cheques = servicoCheque.ConsulteListaChequePorPedido(_recebimento.Id);

                if(cheques.Count == 0)
                {   
                    MessageBox.Show("Para fazer o recebimento deste pedido. Você deve obrigatoriamente cadastrar todos os cheques.", "Recebimento");                    
                    return false;
                }

                _listaDeCheques = cheques;
            }

            return true;
        }

        #endregion

        private void txtValorPago_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
