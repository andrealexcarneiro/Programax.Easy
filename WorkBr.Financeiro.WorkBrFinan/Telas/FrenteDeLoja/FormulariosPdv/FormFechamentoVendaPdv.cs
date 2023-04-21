using DevExpress.LookAndFeel;
using DevExpress.XtraReports.UI;
using NFe.Classes;
using NFe.Impressao.NFCe.FastReports;
using NFe.Servicos;
using NFe.Servicos.Retorno;
using NFe.Utils;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.RecebimentoObj.ObjetoDeNegocio;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Servico.Vendas.RecebimentoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Fiscal.NotasFiscais;
using Programax.Infraestrutura.Negocio.Utils;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NFe.Utils.NFe;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ.ClassesAuxiliares;
using System.Collections.Generic;
using System.IO;
using Programax.Easy.Servico.ConfiguracoesSistema.ConfiguracoesPdvServ;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using Programax.Easy.View.Telas.Vendas.VendaRapida;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using System.Drawing;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    public partial class FormFechamentoVendaPdv : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private PedidoDeVenda _pedidoDeVenda;
        private Parametros _parametros;
        private DialogResult _resultado;

        private Thread _threadFormAviso;
        private FormAvisoGerandoEEnviandoNfe _formAvisoGerandoEEnviandoNfe;
        private bool _exibaFormAviso;
        private Recebimento _recebimento;
        private bool _variavelControleAlterandoTipoDesconto;
        private bool _editandoDescontoFechamento;
        private NotaFiscal _notaFiscalEmEdicao;
        private FormaPagamento _formaPagamentoSelecionada;
        private CondicaoPagamento _condicaoPagamentoSelecionada;
        private List<ParcelaPedidoDeVenda> _listaParcelasPedidoDeVenda;
        private IList<NotaFiscalReferenciada> _notasFiscaisReferenciadas;

        #endregion

        #region " PROPRIEDADES "

        public FormCadastroNotaFiscal FormCadastroNotaFiscal { get; set; }

        #endregion

        #region " CONSTRUTOR "

        public FormFechamentoVendaPdv(PedidoDeVenda pedidoDeVenda)
        {
            InitializeComponent();

            _pedidoDeVenda = pedidoDeVenda;

            _parametros = new ServicoParametros().ConsulteParametros();

            lblValorTotalVenda.Text = "R$ " + _pedidoDeVenda.ValorTotal.ToString("#,###,##0.00");
            txtDescontoFechamento.Text = _pedidoDeVenda.Desconto.ToString("0.00");
            rdbDescontoTotalValor.TabStop = false;

            //LeiaArquivoTextoCheckEmitirNfEPedido();

            RedimensioneFormConformeParametros();

            this.ActiveControl = txtDinheiro;
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        private bool TemFormasDePagamentosDuplicadas()
        {
            if(_formaPagamentoSelecionada != null)
            {
                if (!string.IsNullOrEmpty(txtDinheiro.Text))
                {
                    if ((EnumTipoFormaPagamento)_formaPagamentoSelecionada.Id == EnumTipoFormaPagamento.DINHEIRO)
                    {
                        return false;
                    }
                    else
                        return true;
                         
                }
                else if (!string.IsNullOrEmpty(txtPix.Text))
                {
                    if ((EnumTipoFormaPagamento)_formaPagamentoSelecionada.Id == EnumTipoFormaPagamento.PIX)
                    {
                        return false;
                    }
                    else
                        return true;

                }
                else if (!string.IsNullOrEmpty(txtCartaoCredito.Text))
                {
                    if ((EnumTipoFormaPagamento)_formaPagamentoSelecionada.Id == EnumTipoFormaPagamento.CARTAOCREDITO)
                    {
                        return false;
                    }
                    else
                        return true;

                }
                else if (!string.IsNullOrEmpty(txtCartaoDebito.Text))
                {
                    if ((EnumTipoFormaPagamento)_formaPagamentoSelecionada.Id == EnumTipoFormaPagamento.CARTAODEBITO)
                    {
                        return false;
                    }
                    else
                        return true;
                }
                else if(!string.IsNullOrEmpty(txtCheque.Text))
                {
                    if ((EnumTipoFormaPagamento)_formaPagamentoSelecionada.Id == EnumTipoFormaPagamento.CHEQUE)
                    {
                        return false;
                    }
                    else
                        return true;
                }
            }

            return false;
        }

        public void RedimensioneFormConformeParametros()
        {
            var configPDV = new ServicoConfiguracoesPdv().ConsulteUltimaConfiguracaoPdv();

            if(configPDV.PermitirFormaECondicaoPagamentoNoPDV)
            {
                btnFormaECondicaoPagamento.Visible = true;
                btnFinanceiro.Visible = true;

                this.Width = 608;
                this.Height = 700;

                pictureBox6.Location = new Point(-44, 457);
                labelControl2.Location = new Point(37, 478);
                lblValorTotalVenda.Location = new Point(358, 478);
                labelControl4.Location = new Point(39, 526);
                lblValorTroco.Location = new Point(358, 526);

                lblDescResto.Location = new Point(39, 566);
                lblResto.Location = new Point(358, 566);
                                
                pictureBox2.Location = new Point(-73, 620);

                button2.Location = new Point(39, 640);
                chkImprimirPedido.Location = new Point(79, 650);

                button1.Location = new Point(119, 640);
                chkEmitirNotaFiscal.Location = new Point(160, 650);

                btnAtualizar.Location = new Point(335, 640);
                
                btnSair.Location = new Point(485, 640);
            }
            else
            {
                btnFormaECondicaoPagamento.Visible = false;
                btnFinanceiro.Visible = false;

                this.Width = 608;
                this.Height = 610;

                pictureBox6.Location = new Point(-44, 383);
                labelControl2.Location = new Point(37, 404);
                lblValorTotalVenda.Location = new Point(358, 404);
                labelControl4.Location = new Point(39, 452);
                lblValorTroco.Location = new Point(358, 452);

                lblDescResto.Location = new Point(40,500);
                lblResto.Location = new Point(358, 500);
                                
                pictureBox2.Location = new Point(-73, 550);

                button2.Location = new Point(40, 560);
                chkImprimirPedido.Location = new Point(80, 570);                

                button1.Location = new Point(130, 560);
                chkEmitirNotaFiscal.Location = new Point(170, 570);

                btnAtualizar.Location = new Point(310, 560);

                btnSair.Location = new Point(470, 560);
            }
            
        }

        public DialogResult RetorneVendaFechadaComSucesso()
        {
            this.AbrirTelaModal(true);

            return _resultado;
        }

        public bool ValidaDescontoFechamento(double desconto, double totalSemDesconto, bool ehpercentual)
        {
            //Validação do desconto
            ServicoPessoa servicoPessoa = new ServicoPessoa();

            var Comissao = servicoPessoa.Consulte(Sessao.PessoaLogada.Id).ListaDeComissoes;

            foreach (var maxdesconto in Comissao)
            {
                if (maxdesconto.DescontoMaximoEhPercentual)
                {
                    if (ehpercentual)
                    {
                        if (maxdesconto.DescontoMaximo < desconto) return true;
                    }
                    else
                    {
                        if (((totalSemDesconto * maxdesconto.DescontoMaximo) / 100) < desconto) return true;
                    }
                }
                else
                {
                    if (!ehpercentual)
                    {
                        if (maxdesconto.DescontoMaximo < desconto) return true;
                    }
                    else
                    {
                        if (((desconto * 100) / (double)totalSemDesconto) < desconto) return true;
                    }
                }

            }

            return false;
        }

        private void GereParcelasFinanceiro()
        {
            if (_listaParcelasPedidoDeVenda != null)
                _listaParcelasPedidoDeVenda.Clear();
            else
                _listaParcelasPedidoDeVenda = new List<ParcelaPedidoDeVenda>();

            if (_pedidoDeVenda.ListaItens.Count > 0 && _condicaoPagamentoSelecionada != null)
            {
                ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
                ServicoCondicaoPagamento servicoCondicaoPagamento = new ServicoCondicaoPagamento();

                var condicaoPagamento = servicoCondicaoPagamento.Consulte(_condicaoPagamentoSelecionada.Id);
                var formaPagamento = servicoFormaPagamento.Consulte(_formaPagamentoSelecionada.Id);

                int contador = 0;
                int quantidadeDeParcelas = condicaoPagamento.ListaDeParcelas.Count;

                string valortotalvenda = lblValorTotalVenda.Text;
                double totalvenda = valortotalvenda.Remove(0, 3).ToDouble(); //Remove o "R$ " 

                double totalVendaFechamento = totalvenda;
                double totalSomaParcelas = 0;

                foreach (var parcelaCondicao in condicaoPagamento.ListaDeParcelas)
                {
                    ParcelaPedidoDeVenda parcelaPedidoDeVenda = new ParcelaPedidoDeVenda();

                    parcelaPedidoDeVenda.CondicaoPagamento = condicaoPagamento;
                    parcelaPedidoDeVenda.DataVencimento = DateTime.Now.Date.Date.AddDays(parcelaCondicao.Dias);
                    parcelaPedidoDeVenda.FormaPagamento = formaPagamento;
                    parcelaPedidoDeVenda.Id = contador + 1;
                    parcelaPedidoDeVenda.Parcela = (contador + 1).ToString() + "/" + quantidadeDeParcelas;
                    parcelaPedidoDeVenda.Valor = Math.Round(totalVendaFechamento * parcelaCondicao.PercentualRateio / (double)100, 2);

                    totalSomaParcelas += Math.Round(parcelaPedidoDeVenda.Valor, 2);
                    contador++;

                    _listaParcelasPedidoDeVenda.Add(parcelaPedidoDeVenda);
                }

                var diferenca = Math.Round(totalVendaFechamento - totalSomaParcelas, 2);

                _listaParcelasPedidoDeVenda.Last().Valor += Math.Round(diferenca, 2);
            }
            else
            {
                _listaParcelasPedidoDeVenda.Clear();
            }
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormFechamentoVendaPdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F3)
            {
                FecheVenda();
            }
            else if(e.KeyCode == Keys.F4)
            {
                chkEmitirNotaFiscal.Checked = true;
            }
            else if (e.KeyCode == Keys.F5)
            {
                chkImprimirPedido.Checked = true;
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            FecheVenda();
        }

        private void txtCheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FecheVenda();
            }
        }


        private void txtDinheiro_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaLblTroco();
            CalculeResto();
        }
        //private void txtDinheiro_KeyDown(object sender, EventArgs e)
        //{
           
        //}
        private void txtPix_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaLblTroco();
            CalculeResto();
        }
        private void txtCartaoCredito_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaLblTroco();
            CalculeResto();
        }

        private void txtCartaoDebito_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaLblTroco();
            CalculeResto();
        }

        private void txtCheque_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaLblTroco();
            CalculeResto();
        }

        private void txtDescontoFechamento_EditValueChanged(object sender, EventArgs e)
        {
            if (_editandoDescontoFechamento)
            {
                PreenchaTotais();
                PreenchaLblTroco();
                _editandoDescontoFechamento = false;
            }
        }

        private void txtDescontoFechamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtDescontoFechamento.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtDescontoFechamento_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoDescontoFechamento = true;
        }

        private void txtDescontoFechamento_LostFocus(object sender, EventArgs e)
        {
            _editandoDescontoFechamento = false;
        }

        private void rdbDescontoTotalValor_CheckedChanged(object sender, EventArgs e)
        {
            if (_variavelControleAlterandoTipoDesconto)
            {
                return;
            }

            double desconto = txtDescontoFechamento.Text.ToDouble();

            if (rdbDescontoTotalValor.Checked)
            {
                txtDescontoFechamento.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,2})?";
                ConvertaDescontoParaDinheiro(desconto);
            }
            else
            {
                txtDescontoFechamento.Properties.Mask.EditMask = @"[0-9]{1,2}([\.\,][0-9]{0,2})?";
                ConvertaDescontoParaPercentual(desconto);
            }
        }

        private void chkImprimirPedido_CheckedChanged(object sender, EventArgs e)
        {
            //GravaArquivoTextoCheckEmitirNfEPedido(chkImprimirPedido.Checked.ToString(), chkEmitirNotaFiscal.Checked.ToString());
        }

        private void chkEmitirNotaFiscal_CheckedChanged(object sender, EventArgs e)
        {
            //GravaArquivoTextoCheckEmitirNfEPedido(chkImprimirPedido.Checked.ToString(), chkEmitirNotaFiscal.Checked.ToString());
        }

        private void btnFormaECondicaoPagamento_Click(object sender, EventArgs e)
        {
            FormFormaCondicaoPagamentoVendaRapida formFormaCondicaoPagamentoVendaRapida = new FormFormaCondicaoPagamentoVendaRapida();
            var resultado = formFormaCondicaoPagamentoVendaRapida.EditeFormaECondicaoPagamento(_formaPagamentoSelecionada, _condicaoPagamentoSelecionada, _pedidoDeVenda.Cliente.Id);

            if (resultado == DialogResult.OK)
            {
                _formaPagamentoSelecionada = formFormaCondicaoPagamentoVendaRapida.FormaPagamento;
                _condicaoPagamentoSelecionada = formFormaCondicaoPagamentoVendaRapida.CondicaoPagamento;

                PreenchaTotais();
            }
        }

        private void btnFinanceiro_Click(object sender, EventArgs e)
        {
            if (_listaParcelasPedidoDeVenda == null) return;

            FormFinanceiroVendaRapida formFinanceiroVendaRapida = new FormFinanceiroVendaRapida();
            _listaParcelasPedidoDeVenda = formFinanceiroVendaRapida.EditeFinanceiro(_listaParcelasPedidoDeVenda, _pedidoDeVenda.Cliente.Id);
        }

        #endregion

        #region " MÉTODOS PRIVADOS "
        //Este método vai 1- cadastrar a venda, 2-gerar movimento no caixa e 3- emitir a nota fiscal
        private void FecheVenda()
        {
            if (TemFormasDePagamentosDuplicadas())
            {
                MessageBox.Show("Você informou duas formas de pagamentos diferentes. " +
                                "Para continuar, informe apenas uma, ou, iguais.", "Fechamento de Venda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Action actionCadastrePedidoDeVenda = () =>
            {
                try
                {
                    //Estes dois métodos carrega o objeto: _pedidoDeVenda para processamento
                    
                    InsiraTotais();

                    if (_listaParcelasPedidoDeVenda == null || _listaParcelasPedidoDeVenda.Count ==0)
                        InsiraParcelasNoPedidoDeVenda();
                    else
                       _pedidoDeVenda.ListaParcelasPedidoDeVenda = _listaParcelasPedidoDeVenda;

                    if (_formaPagamentoSelecionada == null)
                        InsiraFormaECondicaoPagamentoNoPedidoDeVenda();
                    else
                    {
                        ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
                        _pedidoDeVenda.FormaPagamento = servicoFormaPagamento.Consulte(_formaPagamentoSelecionada.Id);
                        _pedidoDeVenda.CondicaoPagamento = _condicaoPagamentoSelecionada;                        
                    }
                        

                    //1-Cadastra automaticamente o pedido de vendas
                    ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
                    if (_pedidoDeVenda.Cliente == null)
                    {
                        MessageBox.Show("Para fazer o recebimento é necessário ter um cliente cadastrado em <Menu: Adm Sistema-> Configurações PDV.", "Recebimento", MessageBoxButtons.OK);
                        return;
                    }

                    if (_pedidoDeVenda.Id == 0)
                    {
                        var tipoMensagem = servicoPedidoDeVenda.VerificaEstoqueNegativo(_pedidoDeVenda);

                        //Mensagem informando o parâmetro: "Não aceitar estoque negativo". Não deixar fechar o pedido.
                        if (tipoMensagem.SegundaResposta)
                        {
                            MessageBox.Show("O Estoque do(s) seguinte(s) item(s): " + tipoMensagem.PrimeiroConteudo + " . Está ou ficará menor que zero!", "Não é permitido fechar a venda!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        //Mensagem quando preencherem o estoque mínimo. Caso atingir este estoque, o sistema avisa.
                        if (tipoMensagem.TerceiraResposta)
                        {
                            MessageBox.Show("Verifique os seguintes itens: " + tipoMensagem.SegundoConteudo + ".", "O estoque mínimo foi atingido!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                                               
                       CarregarEnderecoPedidoDeVenda();
                       
                        _pedidoDeVenda.StatusPedidoVenda = EnumStatusPedidoDeVenda.RESERVADO;
                        servicoPedidoDeVenda.FechePedidoDeVenda(_pedidoDeVenda);
                    }

                    //2-Gera automaticamente o movimento de caixa, conforme o pedido cadastrado no bloco acima
                    FatureDocumento();

                    if (chkImprimirPedido.Checked)
                    {
                        if(!_parametros.ParametrosVenda.PedidoEmImpressoraTermica)
                            ImprimaPedidoDeVenda(_recebimento.Id);
                        else
                            ImprimaPedidoDeVendaReduzido(_recebimento.Id);
                    }
                    
                    //3-Abrir o formulário de cadastro para emitir nota fiscal
                    if (chkEmitirNotaFiscal.Checked)//(MessageBox.Show("Deseja emitir nota deste documento?", "Emissão de nota", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        ServicoConfiguracoesPdv servicoConfiguracao = new ServicoConfiguracoesPdv();

                        var configPDV = servicoConfiguracao.ConsulteUltimaConfiguracaoPdv();

                        if (configPDV.EmitirNotaFiscalDiretamenteNoPDV)
                        {
                            try
                            {
                                EnvieNotaFiscalPDV();

                                _pedidoDeVenda = null;
                                Application.OpenForms["FormPdv"].Close();
                                this.Close();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message + " do Pedido Número: " + _pedidoDeVenda.Id+".", "Emissão da Nota Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _pedidoDeVenda = null;
                                Application.OpenForms["FormPdv"].Close();
                                this.Close();
                            }
                            
                        }
                        else
                        {
                            FormCadastroNotaFiscal = new FormCadastroNotaFiscal();
                            FormCadastroNotaFiscal.EditePedidoDeVendaId(_recebimento.Id);

                            Application.OpenForms["FormPdv"].Close();

                            FormCadastroNotaFiscal.KeyPreview = true;

                            FormCadastroNotaFiscal.TopLevel = true;
                            FormCadastroNotaFiscal.TopMost = true;
                            FormCadastroNotaFiscal.BringToFront();
                            FormCadastroNotaFiscal.Focus();

                            this.Close();
                        }
                    }
                    //Caso o usuário não queira emitir a Nota Fiscal. Vai limpar e fechar os formulários Fechamento de venda e PDV
                    else
                    {
                        _pedidoDeVenda = null;
                        Application.OpenForms["FormPdv"].Close();
                        this.Close();
                    }
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionCadastrePedidoDeVenda, exibirMensagemDeSucesso: false);
        }

        private void CarregarEnderecoPedidoDeVenda()
        {
            if (_pedidoDeVenda.Cliente.ListaDeEnderecos != null && _pedidoDeVenda.Cliente.ListaDeEnderecos.Count > 0)
            {
                ServicoClienteRapido servicoCliente = new ServicoClienteRapido();

                var cliente = servicoCliente.Consulte(_pedidoDeVenda.Cliente.Id);

                var enderecoCliente = cliente.ListaDeEnderecos.FirstOrDefault(endereco => endereco.TipoEndereco == EnumTipoEndereco.PRINCIPAL);

                if (enderecoCliente == null)
                {
                    enderecoCliente = _pedidoDeVenda.Cliente.ListaDeEnderecos.First();
                }

                _pedidoDeVenda.EnderecoPedidoDeVenda.CEP = enderecoCliente.CEP;
                _pedidoDeVenda.EnderecoPedidoDeVenda.Bairro = enderecoCliente.Bairro;
                _pedidoDeVenda.EnderecoPedidoDeVenda.Rua = enderecoCliente.Rua;
                _pedidoDeVenda.EnderecoPedidoDeVenda.Cidade = enderecoCliente.Cidade;

                _pedidoDeVenda.EnderecoPedidoDeVenda.Complemento = enderecoCliente.Complemento;
                _pedidoDeVenda.EnderecoPedidoDeVenda.Numero = enderecoCliente.Numero;
                _pedidoDeVenda.EnderecoPedidoDeVenda.TipoEndereco = enderecoCliente.TipoEndereco;
            }
        }

        private void FatureDocumento()
        {
            
            double dinheiro = txtDinheiro.Text.ToDouble() == 0 ? InsiraTotalFormaPagamento(EnumTipoFormaPagamento.DINHEIRO) : txtDinheiro.Text.ToDouble();
            double pix = txtPix.Text.ToDouble() == 0 ? InsiraTotalFormaPagamento(EnumTipoFormaPagamento.PIX) : txtPix.Text.ToDouble();
            double cartaoDebito = txtCartaoDebito.Text.ToDouble() == 0 ? InsiraTotalFormaPagamento(EnumTipoFormaPagamento.CARTAODEBITO) : txtCartaoDebito.Text.ToDouble();
            double cartaoCredito = txtCartaoCredito.Text.ToDouble() == 0 ? InsiraTotalFormaPagamento(EnumTipoFormaPagamento.CARTAOCREDITO) : txtCartaoCredito.Text.ToDouble();
            double cheque = txtCheque.Text.ToDouble() == 0 ? InsiraTotalFormaPagamento(EnumTipoFormaPagamento.CHEQUE) : txtCartaoCredito.Text.ToDouble();

            //Valor recebido (Entrada)
            ServicoRecebimento servicoRecebimento = new ServicoRecebimento();

            _recebimento = servicoRecebimento.Consulte(_pedidoDeVenda.Id, EnumTipoDocumentoRecebimento.PEDIDODEVENDAS);

            _recebimento.CategoriaFinanceira = new CategoriaFinanceira {Id = 2}; // Categoria Fincanceira fixa: 2 - Receita com Vendas

            servicoRecebimento.FatureRecebimento(_recebimento, 0,pix, dinheiro, cartaoDebito, cartaoCredito, cheque);

            //.

            //Saída ou troco
            if (lblValorTroco.Text.Remove(0, 3).ToDouble() > 0)
                if (GeraTrocoNaSaida()) return;

            MessageBox.Show("Documento Recebido Com Sucesso!", "Documento Recebido");
        }

        private double InsiraTotalFormaPagamento( EnumTipoFormaPagamento TipoFormaPagamento)
        {
            double somaPagamentos = 0;

            if (_pedidoDeVenda.ListaParcelasPedidoDeVenda != null && _pedidoDeVenda.ListaParcelasPedidoDeVenda.Count >0)
            {
                foreach (var item in _pedidoDeVenda.ListaParcelasPedidoDeVenda)
                {
                    if((EnumTipoFormaPagamento)item.FormaPagamento.Id == TipoFormaPagamento)
                    {
                        somaPagamentos = somaPagamentos + item.Valor;
                    }
                }

                return Math.Round(somaPagamentos, 2);
            }

            return 0;
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

        private void ImprimaPedidoDeVendaReduzido(int idPedidoDeVenda)
        {
            RelatorioPedidoVendaReduzido relatorio = new RelatorioPedidoVendaReduzido(idPedidoDeVenda);
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

        private bool GeraTrocoNaSaida()
        {
            ServicoItemMovimentacaoCaixa servicoItemMovimentacaoCaixa = new ServicoItemMovimentacaoCaixa();

            ServicoCaixa servicoCaixa = new ServicoCaixa();
            var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

            if (caixa == null)
            {
                MessageBoxAkil.Show("Usuário logado não contém caixa");
                return true;
            }

            ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa(false, false);
            var movimentacaoCaixa = servicoMovimentacaoCaixa.ConsulteCaixaAberto(caixa);

            ItemMovimentacaoCaixa itemMovimentacaoCaixa = new ItemMovimentacaoCaixa();

            FormaPagamento formaPagamento = new FormaPagamento();
            formaPagamento.Id = 1;
            formaPagamento.Descricao = "DINHEIRO";
            formaPagamento.TipoFormaPagamento = EnumTipoFormaPagamento.DINHEIRO;

            itemMovimentacaoCaixa.EstahEstornado = false;
            itemMovimentacaoCaixa.FormaPagamento = formaPagamento;
            itemMovimentacaoCaixa.HistoricoMovimentacoes = "Troco PDV - do Pedido nº." + _pedidoDeVenda.Id;
            itemMovimentacaoCaixa.MovimentacaoCaixa = movimentacaoCaixa;
            itemMovimentacaoCaixa.TipoMovimentacao = EnumTipoMovimentacaoCaixa.SAIDATROCO;
            itemMovimentacaoCaixa.Valor = lblValorTroco.Text.Remove(0, 3).ToDouble();
            itemMovimentacaoCaixa.OrigemMovimentacaoCaixa = EnumOrigemMovimentacaoCaixa.PEDIDODEVENDA;
            itemMovimentacaoCaixa.Parceiro = Sessao.PessoaLogada;

            servicoItemMovimentacaoCaixa.Cadastre(itemMovimentacaoCaixa);
            return false;
        }

        private void InsiraParcelasNoPedidoDeVenda()
        {
            int totalParcelas = _pedidoDeVenda.ListaParcelasPedidoDeVenda.Count;

            for (int i = 0; i < totalParcelas; i++)
            {
                var parcela = _pedidoDeVenda.ListaParcelasPedidoDeVenda[i];

                if (parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.PIX ||
                    parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO ||
                    parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO ||
                    parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO ||
                    parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                {
                    _pedidoDeVenda.ListaParcelasPedidoDeVenda.Remove(parcela);

                    i--;
                    totalParcelas--;
                }
            }

            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
            ServicoCondicaoPagamento servicoCondicaoPagamento = new ServicoCondicaoPagamento();

            var condicaoPagamento = servicoCondicaoPagamento.ConsulteCondicaoPagamentoAVistaPadrao();

            if (txtDinheiro.Text.ToDouble() > 0)
            {
                ParcelaPedidoDeVenda parcelaPedidoDeVenda = new ParcelaPedidoDeVenda();
                parcelaPedidoDeVenda.Valor = txtDinheiro.Text.ToDouble() - RetorneValorTroco();
                parcelaPedidoDeVenda.DataVencimento = DateTime.Now.Date;
                parcelaPedidoDeVenda.FormaPagamento = servicoFormaPagamento.ConsultePeloTipo(EnumTipoFormaPagamento.DINHEIRO);
                parcelaPedidoDeVenda.CondicaoPagamento = condicaoPagamento;

                _pedidoDeVenda.ListaParcelasPedidoDeVenda.Add(parcelaPedidoDeVenda);
            }

            if (txtCartaoCredito.Text.ToDouble() > 0)
            {
                ParcelaPedidoDeVenda parcelaPedidoDeVenda = new ParcelaPedidoDeVenda();
                parcelaPedidoDeVenda.Valor = txtCartaoCredito.Text.ToDouble();
                parcelaPedidoDeVenda.DataVencimento = DateTime.Now.Date;
                parcelaPedidoDeVenda.FormaPagamento = servicoFormaPagamento.ConsultePeloTipo(EnumTipoFormaPagamento.CARTAOCREDITO);
                parcelaPedidoDeVenda.CondicaoPagamento = condicaoPagamento;

                _pedidoDeVenda.ListaParcelasPedidoDeVenda.Add(parcelaPedidoDeVenda);
            }

            if (txtCartaoDebito.Text.ToDouble() > 0)
            {
                ParcelaPedidoDeVenda parcelaPedidoDeVenda = new ParcelaPedidoDeVenda();
                parcelaPedidoDeVenda.Valor = txtCartaoDebito.Text.ToDouble();
                parcelaPedidoDeVenda.DataVencimento = DateTime.Now.Date;
                parcelaPedidoDeVenda.FormaPagamento = servicoFormaPagamento.ConsultePeloTipo(EnumTipoFormaPagamento.CARTAODEBITO);
                parcelaPedidoDeVenda.CondicaoPagamento = condicaoPagamento;

                _pedidoDeVenda.ListaParcelasPedidoDeVenda.Add(parcelaPedidoDeVenda);
            }

            if (txtCheque.Text.ToDouble() > 0)
            {
                ParcelaPedidoDeVenda parcelaPedidoDeVenda = new ParcelaPedidoDeVenda();
                parcelaPedidoDeVenda.Valor = txtCheque.Text.ToDouble();
                parcelaPedidoDeVenda.DataVencimento = DateTime.Now.Date;
                parcelaPedidoDeVenda.FormaPagamento = servicoFormaPagamento.ConsultePeloTipo(EnumTipoFormaPagamento.CHEQUE);
                parcelaPedidoDeVenda.CondicaoPagamento = condicaoPagamento;

                _pedidoDeVenda.ListaParcelasPedidoDeVenda.Add(parcelaPedidoDeVenda);
            }

            int numeroParcela = 1;

            foreach (var parcela in _pedidoDeVenda.ListaParcelasPedidoDeVenda)
            {
                parcela.Parcela = numeroParcela + "/" + totalParcelas;
                numeroParcela++;
            }
        }

        private void InsiraFormaECondicaoPagamentoNoPedidoDeVenda()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
            ServicoCondicaoPagamento servicoCondicaoPagamento = new ServicoCondicaoPagamento();

            _pedidoDeVenda.CondicaoPagamento = servicoCondicaoPagamento.ConsulteCondicaoPagamentoAVistaPadrao();

            if (txtDinheiro.Text.ToDouble() > 0)
            {
                _pedidoDeVenda.FormaPagamento = servicoFormaPagamento.ConsultePeloTipo(EnumTipoFormaPagamento.DINHEIRO);
            }
            else if (txtPix.Text.ToDouble() > 0)
            {
                _pedidoDeVenda.FormaPagamento = servicoFormaPagamento.ConsultePeloTipo(EnumTipoFormaPagamento.PIX);
            }
            else if (txtCartaoCredito.Text.ToDouble() > 0)
            {
                _pedidoDeVenda.FormaPagamento = servicoFormaPagamento.ConsultePeloTipo(EnumTipoFormaPagamento.CARTAOCREDITO);
            }
            else if (txtCartaoDebito.Text.ToDouble() > 0)
            {
                _pedidoDeVenda.FormaPagamento = servicoFormaPagamento.ConsultePeloTipo(EnumTipoFormaPagamento.CARTAODEBITO);
            }
            else if (txtCheque.Text.ToDouble() > 0)
            {
                _pedidoDeVenda.FormaPagamento = servicoFormaPagamento.ConsultePeloTipo(EnumTipoFormaPagamento.CHEQUE);
            }
        }

        private void InsiraTotais()
        {
            string valortotalvenda = lblValorTotalVenda.Text;
            double totalvenda = valortotalvenda.Remove(0, 3).ToDouble(); //Remove o "R$ " 

            //Total de descontos itens = "total real" menos o "total de desconto do fechamento" que pode estar...
            //com o desconto maior do que o que foi proporcionado nos itens
            double diferencadescontositens = Math.Round(_pedidoDeVenda.ValorTotal - totalvenda,2);
            
            //Quando o total de descontos dos itens for menor do que total fechamento entra aqui                
            if (diferencadescontositens > 0)
            {
                //***21/12/2018 - Foi alterado para rateiar o desconto de maneira que não fique negativo***

                double totalBruto = _pedidoDeVenda.ListaItens.Sum(i => i.ValorUnitario * i.Quantidade);

                double totaldescontositens = (_pedidoDeVenda.ValorTotal + _pedidoDeVenda.Desconto) - totalvenda;
                int totalitens = _pedidoDeVenda.ListaItens.Count; 

                //Insere o desconto para cada item
                foreach (var descontounitario in _pedidoDeVenda.ListaItens)
                {
                    descontounitario.TotalDesconto = Math.Round((descontounitario.Quantidade * descontounitario.ValorUnitario * totaldescontositens) / totalBruto, 2);
                    descontounitario.ValorTotal = (descontounitario.ValorUnitario * descontounitario.Quantidade) - descontounitario.TotalDesconto;//valorrateioitens;
                }
            }

            if (totalvenda > 0)
                _pedidoDeVenda.ValorTotal = totalvenda;

            if (rdbDescontoTotalValor.Checked)
            {
                _pedidoDeVenda.Desconto = txtDescontoFechamento.Text.ToDouble();
            }
            else
            {
                _pedidoDeVenda.Desconto = txtDescontoFechamento.Text.ToDouble();
                _pedidoDeVenda.DescontoEhPercentual = true;
            }
        }

        private double RetorneValorTroco()
        {
            double troco;
            double valoremdinheiro;
            double valoremPIX;
            valoremdinheiro = txtDinheiro.Text.ToDouble();
            valoremPIX = txtPix.Text.ToDouble();

            if (valoremdinheiro <= 0)
            {
                if(valoremPIX <=0)
                {
                    return 0;
                }
                
            }
            if (valoremPIX <= 0)
            {
                if (valoremdinheiro <= 0)
                {
                    return 0;
                }
                    
            }

            string totalparatroco = lblValorTotalVenda.Text;
            troco = totalparatroco.Remove(0, 3).ToDouble(); //Remove o "R$ "

            troco -= txtCartaoCredito.Text.ToDouble();
            troco -= txtCartaoDebito.Text.ToDouble();
            troco -= txtCheque.Text.ToDouble();
            troco -= valoremdinheiro;
            troco -= valoremPIX;

            if (troco < 0)
            {
                return Math.Abs(troco);
            }
            else
            {
                return 0;
            }
        }

        private double RetorneValorResto()
        {
            double resto;
            double valoremdinheiro;
            double valorempix;
            valoremdinheiro = txtDinheiro.Text.ToDouble();
            valorempix = txtDinheiro.Text.ToDouble();

            if (valoremdinheiro <= 0)
            {
                return 0;
            }

            if (valorempix <= 0)
            {
                return 0;
            }

            string totalparatroco = lblValorTotalVenda.Text;
            resto = totalparatroco.Remove(0, 3).ToDouble(); //Remove o "R$ "

            resto -= txtCartaoCredito.Text.ToDouble();
            resto -= txtCartaoDebito.Text.ToDouble();
            resto -= txtCheque.Text.ToDouble();
            resto -= valoremdinheiro;
            resto -= valorempix;

            if (resto > 0)
            {
                return Math.Abs(resto);
            }
            else
            {
                return 0;
            }
        }

        private void PreenchaTotais()
        {
            double desconto;
            double valortotal;
            double resultado;

            desconto = txtDescontoFechamento.Text.ToDouble();
            valortotal = _pedidoDeVenda.ValorTotal + _pedidoDeVenda.Desconto;

            if (rdbDescontoTotalPercentual.Checked)
            {
                if (ValidaDescontoFechamento(desconto, valortotal, true))
                {
                    MessageBoxAkil.Show("Desconto acima do Permitido.", "Fechamento Venda PDV");
                    lblValorTotalVenda.Text = "R$ " + (_pedidoDeVenda.ValorTotal + _pedidoDeVenda.Desconto).ToString("#,###,##0.00");
                    return;
                }
                desconto = (desconto * valortotal) / 100;
                resultado = valortotal - desconto;
                lblValorTotalVenda.Text = "R$ " + resultado.ToString("#,###,##0.00");
            }
            else
            {
                resultado = valortotal - desconto;

                if (ValidaDescontoFechamento(desconto, valortotal, false))
                {
                    MessageBoxAkil.Show("Desconto acima do Permitido.", "Fechamento Venda PDV");
                    lblValorTotalVenda.Text = "R$ " + (_pedidoDeVenda.ValorTotal + _pedidoDeVenda.Desconto).ToString("#,###,##0.00");
                    return;
                }
                lblValorTotalVenda.Text = "R$ " + resultado.ToString("#,###,##0.00");
            }

            GereParcelasFinanceiro();
        }

        private void PreenchaLblTroco()
        {
            lblValorTroco.Text = "R$ " + RetorneValorTroco().ToString("0.00");
        }

        private void CalculeResto()
        {
            lblResto.Text = "R$ " + RetorneValorResto().ToString("0.00");
        }


        private void ValideNotaFiscal()
        {
            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe();
            var configuracoesZeus = servicoConfiguracaoNfe.RetorneConfiguracaoServicoZeus((EnumModeloNotaFiscal)_notaFiscalEmEdicao.IdentificacaoNotaFiscal.ModeloDocumentoFiscal);

            if (_notaFiscalEmEdicao.IdentificacaoNotaFiscal.TipoEmissaoDanfe != EnumTipoEmissaoDanfe.EMISSAONORMAL)
            {
                configuracoesZeus.tpEmis = (NFe.Classes.Informacoes.Identificacao.Tipos.TipoEmissao)_notaFiscalEmEdicao.IdentificacaoNotaFiscal.TipoEmissaoDanfe;
            }

            var servicoNFe = new ServicosNFe(configuracoesZeus);

            RetornoNFeRetAutorizacao retornoConsulta = null;

            while (retornoConsulta == null || retornoConsulta.Retorno.cStat == 105)
            {
                Thread.Sleep(200);

                retornoConsulta = servicoNFe.NFeRetAutorizacao(_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.NumeroReciboLote);
            }

            if (retornoConsulta.Retorno.cStat == 104)
            {
                if (retornoConsulta.Retorno.protNFe[0].infProt.cStat != 100)
                {
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDevolvida = retornoConsulta.Retorno.cStat + " - " + retornoConsulta.Retorno.xMotivo;
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDeErro = retornoConsulta.Retorno.protNFe[0].infProt.cStat + " - " + retornoConsulta.Retorno.protNFe[0].infProt.xMotivo;

                    if (retornoConsulta.Retorno.protNFe[0].infProt.cStat == 301 ||
                        retornoConsulta.Retorno.protNFe[0].infProt.cStat == 302 ||
                        retornoConsulta.Retorno.protNFe[0].infProt.cStat == 303)
                    {
                        _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.DENEGADA;
                    }
                    else
                    {
                        _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.REJEITADA;
                    }
                }
                else
                {
                    var informacoesProtocolo = retornoConsulta.Retorno.protNFe[0].infProt;

                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDevolvida = informacoesProtocolo.cStat + " - " + retornoConsulta.Retorno.protNFe[0].infProt.xMotivo;
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDeErro = string.Empty;
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.AUTORIZADA;

                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal = _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal ?? new InformacoesProtocoloAutorizacaoNotaFiscal();

                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.Id = informacoesProtocolo.Id;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.ChaveNfe = informacoesProtocolo.chNFe;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.DataHoraRecibo = informacoesProtocolo.dhRecbto.Date;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.DigestValue = informacoesProtocolo.digVal;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.Motivo = informacoesProtocolo.xMotivo;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.NumeroProtocolo = informacoesProtocolo.nProt;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.Status = informacoesProtocolo.cStat;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.TipoAmbiente = (int)informacoesProtocolo.tpAmb;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.VersaoAplicativo = informacoesProtocolo.verAplic;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.VersaoNota = "4.00";

                    //ConversorNotaFiscalAkilParaZeus conversorNotaFiscalAkilParaZeus = new ConversorNotaFiscalAkilParaZeus();
                    //var notaFiscalZeus = conversorNotaFiscalAkilParaZeus.ConvertaNotaAutorizadaAkilParaZeus(_notaFiscalEmEdicao);

                    //notaFiscalZeus.NFe.Assina();

                    //_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Xml = notaFiscalZeus.ObterXmlString();
                }
            }
            else
            {
                _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDevolvida = retornoConsulta.Retorno.cStat + " - " + retornoConsulta.Retorno.xMotivo;

                if (retornoConsulta.Retorno.protNFe.Count > 0)
                {
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDeErro = retornoConsulta.Retorno.protNFe[0].infProt.cStat + " - " + retornoConsulta.Retorno.protNFe[0].infProt.xMotivo;
                }

                _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.REJEITADA;
            }

            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
            servicoNotaFiscal.Atualize(_notaFiscalEmEdicao);

            //PreenchaNotaFiscalEmEdicao();

            if (_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status != EnumStatusNotaFiscal.AUTORIZADA)
            {
                throw new Exception("Ocorreu o seguinte erro ao validar a nota: " + _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDeErro);
            }

            if (_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem == EnumTipoDocumento.PEDIDODEVENDAS)
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                var pedido = servicoPedidoDeVenda.Consulte(_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DocumentoId);

                pedido.StatusPedidoVenda = EnumStatusPedidoDeVenda.EMITIDONFE;

                servicoPedidoDeVenda.Atualize(pedido);
            }

            servicoNotaFiscal.Atualize(_notaFiscalEmEdicao);

            //PreenchaNotaFiscalEmEdicao();
        }
    
        private void ImprimirNota(NotaFiscal notaFiscal)
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            var parametros = servicoParametros.ConsulteParametros();
            string caminhoACBR = "C:\\Programax";

            if (!string.IsNullOrEmpty(parametros.ParametrosCadastros.CaminhoACBR))
            {
                caminhoACBR = parametros.ParametrosCadastros.CaminhoACBR;
            }

            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe();
            servicoConfiguracaoNfe.RetorneConfiguracaoServicoZeus((EnumModeloNotaFiscal)_notaFiscalEmEdicao.IdentificacaoNotaFiscal.ModeloDocumentoFiscal);

            ConversorNotaFiscalAkilParaZeus conversorNotaFiscalAkilParaZeus = new ConversorNotaFiscalAkilParaZeus();
            var notaFiscalZeus = conversorNotaFiscalAkilParaZeus.ConvertaNotaAutorizadaAkilParaZeus(_notaFiscalEmEdicao);

            notaFiscalZeus.NFe.Assina();

            string nomeNotaFiscal = _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.ChaveDeAcesso + ".xml";

            if (!Directory.Exists(@caminhoACBR + "\\NotaFiscal"))
            {
                Directory.CreateDirectory(@caminhoACBR + "\\NotaFiscal");
            }

            if (!Directory.Exists(@caminhoACBR + "\\NotaFiscal\\XML\\"))
            {
                Directory.CreateDirectory(@caminhoACBR + "\\NotaFiscal\\XML\\");
            }

            if (!Directory.Exists(@caminhoACBR + "\\NotaFiscal\\ACBR\\"))
            {
                Directory.CreateDirectory(@caminhoACBR + "\\NotaFiscal\\ACBR\\");
            }

            if (!Directory.Exists(@caminhoACBR + "\\NotaFiscal\\ACBR\\SAIDA\\"))
            {
                Directory.CreateDirectory(@caminhoACBR + "\\NotaFiscal\\ACBR\\SAIDA\\");
            }

            if (!Directory.Exists(@caminhoACBR + "\\NotaFiscal\\ACBR\\ENTRADA\\"))
            {
                Directory.CreateDirectory(@caminhoACBR + "\\NotaFiscal\\ACBR\\ENTRADA\\");
            }

            notaFiscalZeus.SalvarArquivoXml(@caminhoACBR + "\\NotaFiscal\\XML\\" + nomeNotaFiscal);

            string textoArquivo;

            if (System.IO.File.Exists(@caminhoACBR + "\\NotaFiscal\\XML\\" + nomeNotaFiscal))
            {
                textoArquivo = System.IO.File.ReadAllText(@caminhoACBR + "\\NotaFiscal\\XML\\" + nomeNotaFiscal);

                if (textoArquivo != string.Empty)
                {
                    string lines = "<?xml version=" + '"' + "1.0" + '"' + "encoding=" + '"' + "UTF-8" + '"' + "?>" + textoArquivo.ToString();

                    System.IO.File.WriteAllText(@caminhoACBR + "\\NotaFiscal\\XML\\" + nomeNotaFiscal, lines);
                }
            }

            string comandoAcbrImprimirNota = "NFE.IMPRIMIRDANFE(\"" + @caminhoACBR + "\\NotaFiscal\\XML\\" + nomeNotaFiscal + "\")";
            
            System.IO.File.WriteAllText(@caminhoACBR + "\\NotaFiscal\\ACBR\\ENTRADA\\IMPRIMIR_NOTA.txt", comandoAcbrImprimirNota);
        }

        private void ConvertaDescontoParaPercentual(double valor)
        {
            _variavelControleAlterandoTipoDesconto = true;

            double totalSemDesconto = 0;


            totalSemDesconto += _pedidoDeVenda.ValorTotal + _pedidoDeVenda.Desconto;// * item.Quantidade;


            txtDescontoFechamento.Text = ((valor * 100) / (double)totalSemDesconto).ToString("0.00");

            _variavelControleAlterandoTipoDesconto = false;
        }

        private void ConvertaDescontoParaDinheiro(double percentual)
        {
            double ValorTotal = _pedidoDeVenda.ValorTotal + _pedidoDeVenda.Desconto;

            txtDescontoFechamento.Text = ((ValorTotal * percentual) / 100).ToString("0.00");
        }

        private void GravaArquivoTextoCheckEmitirNfEPedido(string imprirmirPedido, string emitirNF)
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            var parametros = servicoParametros.ConsulteParametros();
            string caminhoACBR = "C:\\Programax";

            if (!string.IsNullOrEmpty( parametros.ParametrosCadastros.CaminhoACBR))
            {
                caminhoACBR = parametros.ParametrosCadastros.CaminhoACBR;
            }

            string lines = imprirmirPedido + "," + emitirNF;
            System.IO.File.WriteAllText(@caminhoACBR+"\\CheckEmitirNfEPedidoPDV.txt", lines);
        }

        private void LeiaArquivoTextoCheckEmitirNfEPedido()
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            var parametros = servicoParametros.ConsulteParametros();
            string caminhoACBR = "C:\\Programax";

            if (!string.IsNullOrEmpty(parametros.ParametrosCadastros.CaminhoACBR))
            {
                caminhoACBR = parametros.ParametrosCadastros.CaminhoACBR;
            }

            string textoArquivo;

            if (System.IO.File.Exists(@caminhoACBR+"\\CheckEmitirNfEPedidoPDV.txt"))
            {
                textoArquivo = System.IO.File.ReadAllText(@caminhoACBR+"\\CheckEmitirNfEPedidoPDV.txt");

                if (textoArquivo != string.Empty)
                {
                    string[] nFPedido = textoArquivo.Split(',');
                    
                    chkImprimirPedido.Checked = nFPedido[0] == "True" ? true : false;
                    chkEmitirNotaFiscal.Checked = nFPedido[1] == "True"? true : false;
                }
            }                    
        }

        private void EditePedidoDeVenda()
        {
            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
            var nota = servicoNotaFiscal.RetorneNotaAPartirDePedido(_pedidoDeVenda.Id, BusqueDestinatarioDoPedido(_pedidoDeVenda));

            _notaFiscalEmEdicao = nota;
            _notaFiscalEmEdicao.ListaNotasReferenciadas = _notaFiscalEmEdicao.ListaNotasReferenciadas.Count == 0 ? _notasFiscaisReferenciadas : _notaFiscalEmEdicao.ListaNotasReferenciadas;
            
            //PreenchaNotaFiscalEmEdicao();
        }

        private DestinatarioAuxiliarNotaFiscal BusqueDestinatarioDoPedido(PedidoDeVenda pedido)
        {
            DestinatarioAuxiliarNotaFiscal Destinatario = new DestinatarioAuxiliarNotaFiscal();

            Destinatario.TipoPessoa = (EnumTipoPessoa)pedido.Cliente.DadosGerais.TipoPessoa;
            Destinatario.Nome = pedido.Cliente.DadosGerais.Razao;
            Destinatario.CpfCnpj = pedido.Cliente.DadosGerais.CpfCnpj;
            Destinatario.TipoCliente = EnumTipoCliente.CONSUMIDOR;

            ServicoPessoa servicoPessoa = new ServicoPessoa();
            
            var pessoa = servicoPessoa.Consulte(pedido.Cliente.Id);

            pedido.Cliente.ListaDeEnderecos = pessoa.ListaDeEnderecos;

            if (pedido.Cliente.ListaDeEnderecos.Count > 0)
            {
                Destinatario.Cep = pedido.Cliente.ListaDeEnderecos.First().CEP;
                Destinatario.UF = pedido.Cliente.ListaDeEnderecos.First().Cidade.Estado.UF;
                
                Destinatario.CodigoCidade = pedido.Cliente.ListaDeEnderecos.First().Cidade.CodigoIbge.ToLong();
                Destinatario.NomeCidade = pedido.Cliente.ListaDeEnderecos.First().Cidade.Descricao;
                
                Destinatario.Bairro = pedido.Cliente.ListaDeEnderecos.First().Bairro;
                Destinatario.Logradouro = pedido.Cliente.ListaDeEnderecos.First().Rua;
                Destinatario.Complemento = pedido.Cliente.ListaDeEnderecos.First().Complemento;

                //Se o numero do endereço for nulo vamos colocar um ponto "."
                if (pedido.Cliente.ListaDeEnderecos.First().Numero == null || pedido.Cliente.ListaDeEnderecos.First().Numero == "")
                {
                    Destinatario.Numero = ".";
                }
                else
                {
                    Destinatario.Numero = pedido.Cliente.ListaDeEnderecos.First().Numero;
                }
            }

            return Destinatario;
        }

        private void EnvieNotaFiscalPDV()
        {
            try
            {
                ExibaFormAviso();
                                
                EnvieNotaFiscal();

                if (_notaFiscalEmEdicao.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == 55)
                    ValideNotaFiscal();
                
                FecheFormAvisoThread();

                ImprimirNota(_notaFiscalEmEdicao);                
            }
            catch (Exception e)
            {
                FecheFormAvisoThread();
                if (e.Message.Contains("enderDest"))
                 {
                    MessageBox.Show("Não foi possível gerar a nota, porque você não informou o endereço. " +
                                    "Informe e tente novamente pelo cadastro de notas fiscais.", 
                                    "Nota Fiscal PDV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }
                 else
                    throw e;
            }
        }

        private void EnvieNotaFiscal()
        {
            EditePedidoDeVenda();

            PreenchaInformacoesPagamentosNotaVersao4_00();
            
            _notaFiscalEmEdicao.IdentificacaoNotaFiscal.IdentificacaoOperacaoNotaFiscal = EnumIdenficacaoOperacaoNotaFiscal.OPERACAOINTERNA;
            _notaFiscalEmEdicao.IdentificacaoNotaFiscal.TipoEmissaoDanfe = EnumTipoEmissaoDanfe.EMISSAONORMAL;
            _notaFiscalEmEdicao.IdentificacaoNotaFiscal.DataHoraSaida = DateTime.Now;
            _notaFiscalEmEdicao.IdentificacaoNotaFiscal.DataHoraEmissao = DateTime.Now;

            //Se o cpf/cnpj no modelo NFCe(65) for nulo então limpa o destinatário
            //Na emissão da nota sairá "CONSUMIDOR NÃO IDENTIFICADO"
            if (string.IsNullOrEmpty(_pedidoDeVenda.Cliente.DadosGerais.CpfCnpj))
            {
                _notaFiscalEmEdicao.Destinatario.CnpjCpf = null;
            }
            else
            {//Caso tiver o cpf/cnpj tem que validar
                if (!ValidaCpfCnpjNFCe(_pedidoDeVenda.Cliente.DadosGerais.CpfCnpj))
                    return;
            }

            //Envia a NFCe
            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
            _notaFiscalEmEdicao = servicoNotaFiscal.EnviaNFCe(_notaFiscalEmEdicao);

            }

        private void PreenchaInformacoesPagamentosNotaVersao4_00()
        {
            var pedidoDeVenda = new ServicoPedidoDeVenda().Consulte(_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DocumentoId);

            if (pedidoDeVenda == null) return;

            _notaFiscalEmEdicao.DadosCobranca.ListaDeParcelasVendas = pedidoDeVenda.ListaParcelasPedidoDeVenda.ToList();

            var formaPgtoPesquisa = pedidoDeVenda.ListaParcelasPedidoDeVenda.Select(x => x.FormaPagamento.TipoFormaPagamento).FirstOrDefault();

            _notaFiscalEmEdicao.DadosCobranca.TotalDePagamento = pedidoDeVenda.ListaParcelasPedidoDeVenda.Sum(x => x.Valor);
            _notaFiscalEmEdicao.DadosCobranca.FormaPagamentoNF = new ServicoNotaFiscal().RetorneFormaPagamentoParaNF(formaPgtoPesquisa);
            _notaFiscalEmEdicao.DadosCobranca.CondicaoVistaPrazo = retorneCondicaoPagamentoVistaPrazo(pedidoDeVenda.ListaParcelasPedidoDeVenda.ToList());
        }

        private EnumCondicaoVistaPrazo retorneCondicaoPagamentoVistaPrazo(List<ParcelaPedidoDeVenda> ListaParcelas)
        {
            var idCondicao = ListaParcelas.Select(x => x.CondicaoPagamento.Id).FirstOrDefault();

            var condicao = new ServicoCondicaoPagamento().Consulte(idCondicao);

            var nDias = condicao.ListaDeParcelas.Select(x => x.Dias).FirstOrDefault();

            if (nDias > 1)
            {
                return EnumCondicaoVistaPrazo.APrazo;
            }
            else
            {
                return EnumCondicaoVistaPrazo.AVista;
            }
        }

        //Método que retorna se está configurado para NFCe para não validar o CPF/CNPJ
        private bool ValidaCpfCnpjNFCe(String CpfCnpj)
        {
            if (_pedidoDeVenda.Cliente.DadosGerais.TipoPessoa == EnumTipoPessoa.PESSOAFISICA)
            {
                if (!ValidacoesGerais.CpfEstahValido(CpfCnpj))
                {
                    MessageBox.Show("CPF inconsistente.");

                    return false;
                }
            }
            else
            {
                if (!ValidacoesGerais.CnpjEstahValido(CpfCnpj))
                {
                    MessageBox.Show("CNPJ inconsistente.");

                    return false;
                }
            }

            return true;
        }

    #region " MENSAGEM ENVIANDO NOTA "

    private void ExibaFormAviso()
        {
            _exibaFormAviso = true;

            _threadFormAviso = new Thread(new ThreadStart(this.ExibaFormAvisoThread));
            _threadFormAviso.Start();
        }

        private void ExibaFormAvisoThread()
        {
            Thread.Sleep(150);

            if (_exibaFormAviso)
            {
                _formAvisoGerandoEEnviandoNfe = new FormAvisoGerandoEEnviandoNfe();
                _formAvisoGerandoEEnviandoNfe.AbrirTelaModal(true);
            }
        }

        private void FecheFormAvisoThread()
        {
            _exibaFormAviso = false;

            if (_formAvisoGerandoEEnviandoNfe != null)
            {
                _formAvisoGerandoEEnviandoNfe.Close();
                _formAvisoGerandoEEnviandoNfe.Dispose();
                _formAvisoGerandoEEnviandoNfe = null;
            }

            this.Focus();
        }

        #endregion

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void FormFechamentoVendaPdv_Load(object sender, EventArgs e)
        {

        }

       
    }
}
