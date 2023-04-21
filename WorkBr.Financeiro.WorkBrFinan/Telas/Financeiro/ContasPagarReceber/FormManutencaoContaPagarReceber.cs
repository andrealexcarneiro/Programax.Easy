using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;
using Programax.Easy.Servico.Financeiro.CategoriaServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoBancoServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoBancoServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Servico.Financeiro.OperadorasCartaoServ;
using Programax.Easy.Servico.Financeiro.PlanoContasServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Financeiro.BancosParaMovimento;
using Programax.Easy.View.Telas.Financeiro.Categorias;
using Programax.Easy.View.Telas.Financeiro.OperadorasCartoes;
using Programax.Easy.View.Telas.Financeiro.PlanosDeContas;
using Programax.Infraestrutura.Negocio.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Programax.Easy.View.Telas.Financeiro.ContasPagarReceber
{
    public partial class FormManutencaoContaPagarReceber : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private PlanoDeContas _planoDeContas;
        private ContaPagarReceber _contaPagarReceber;
        private bool _ehCalculoManual;
        private Parametros _parametros;
       
        #endregion

        #region " CONSTRUTOR "

        public FormManutencaoContaPagarReceber()
        {
            InitializeComponent();

            PreenchaCboFormaPagamento();
            
            PreenchaOStatus();            
            
            _parametros = new ServicoParametros().ConsulteParametros();

            this.ActiveControl = cboFormaDePagamento;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void cboOperadorasCartao_EditValueChanged(object sender, EventArgs e)
        {
            if (cboOperadorasCartao.EditValue != null)
            {
                var operadora = new ServicoOperadorasCartao().Consulte(cboOperadorasCartao.EditValue.ToInt());

                if (operadora != null)
                    cboBanco.EditValue = operadora.BancoParaMovimento.Id;
            }
        }

        private void btnAdicionarOperadorasCartao_Click(object sender, EventArgs e)
        {
            var retorno = new FormCadastroOperadorasCartao().ShowDialog();

            CarregaComboOperadorasDebitoCredito();
        }

        private void cboFormaDePagamento_EditValueChanged(object sender, EventArgs e)
        {
            if (((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.CARTAOCREDITO ||
             (EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.CARTAODEBITO) &&
             _contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER && 
             _parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
            {
                lblOperadorasCartao.Visible = true;
                cboOperadorasCartao.Visible = true;
                cboCondicaoOperadora.Visible = true;

                btnAdicionarOperadorasCartao.Visible = true;

                CarregaComboOperadorasDebitoCredito();

                PreenchaCboCondicaoPagamento(cboFormaDePagamento.EditValue.ToInt());
            }
            else
            {
                lblOperadorasCartao.Visible = false;
                cboOperadorasCartao.Visible = false;
                cboCondicaoOperadora.Visible = false;

                btnAdicionarOperadorasCartao.Visible = false;
            }
        }

        private void btnAdicionarBanco_Click(object sender, EventArgs e)
        {
            FormCadastroBancoParaMovimento formbanco = new FormCadastroBancoParaMovimento();
            formbanco.ShowDialog();

            PreenchaCboBancos();

            cboBanco.EditValue = null;
        }

        private void btnAdicionarCategoria_Click(object sender, EventArgs e)
        {
            FormCadastroCategoriasFinanceiras formCategoria = new FormCadastroCategoriasFinanceiras();
            formCategoria.ShowDialog();

            PreenchaCboCategorias();

            cboCategoriaFinanceira.EditValue = null;
        }

        private void txtNumeroPlanoDeContas_Leave(object sender, EventArgs e)
        {
            ServicoPlanoDeContas servicoPlanoDeContas = new ServicoPlanoDeContas();

            var planoDeContas = servicoPlanoDeContas.ConsultePlanoDeContasAtivoPeloNumero(txtNumeroPlanoDeContas.Text);

            PreenchaPlanoDeContas(planoDeContas);
        }

        private void btnPesquisaPlanoDeContas_Click(object sender, EventArgs e)
        {
            FormPlanosContasPesquisa formPlanosContasPesquisa = new FormPlanosContasPesquisa();

            var planoDeContas = formPlanosContasPesquisa.ExibaPesquisaDePlanoDeContasAtivos();

            if (planoDeContas != null)
            {
                PreenchaPlanoDeContas(planoDeContas);
            }
        }

        private void txtRecalcularValorTotal_EditValueChanged(object sender, EventArgs e)
        {
            _ehCalculoManual = false;

            RecalcularValorTotal();
        }

        private void rdbMultaPercentual_CheckedChanged(object sender, EventArgs e)
        {
            RecalcularValorTotal();
        }

        private void rdbJurosPercentual_CheckedChanged(object sender, EventArgs e)
        {
            RecalcularValorTotal();
        }

        private bool OCaixaVaiFicarNegativo(EnumTipoFormaPagamento formaPagamento)
        {
            ServicoCaixa servicoCaixa = new ServicoCaixa();

            var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

            if (caixa == null) return false;

            ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();

            var caixaAberto = servicoMovimentacaoCaixa.ConsulteLista(caixa, null, null, null, EnumStatusMovimentacaoCaixa.ABERTO);

            if (caixaAberto == null) return false;

            double dinheiroEntrada = 0;
            double dinheiroSaida = 0;
            double dinheiroSaldoFinal = 0;

            double chequeEntrada = 0;
            double chequeSaida = 0;
            double chequeSaldoFinal = 0;

            var listaItensCaixaAberto = caixaAberto.FirstOrDefault().ListaItensCaixa;

            foreach (var item in listaItensCaixaAberto)
            {                       
                if (item.EstahEstornado)
                {
                    continue;
                }

                if (item.TipoMovimentacao == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
                {
                    if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO)
                    {
                        dinheiroEntrada += item.Valor;
                        dinheiroSaldoFinal += item.Valor;
                    }                   
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                    {
                        chequeEntrada += item.Valor;
                        chequeSaldoFinal += item.Valor;
                    }                    
                }
                else
                {
                    if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO)
                    {
                        dinheiroSaida += item.Valor;
                        dinheiroSaldoFinal -= item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                    {
                        chequeSaida += item.Valor;
                        chequeSaldoFinal -= item.Valor;
                    }
                }
            }

            //Dinheiro
            if (formaPagamento == EnumTipoFormaPagamento.DINHEIRO)
            {
                if (((dinheiroSaldoFinal.ToDouble() - txtValorPago.Text.ToDouble()) < 0))
                {
                    return true;
                }
            }
            else //Cheque
            {
                //if (((chequeSaldoFinal.ToDouble() - txtValorPago.Text.ToDouble()) < 0))
                //{
                //    return true;
                //}
                return false;
            }

            return false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValorPago.Text) && txtValorPago.Text.ToInt() != 0 && !string.IsNullOrEmpty(txtDataPagamento.Text))
            {
                //Valida se tiver usando conciliação bancária é obrigado a informar categoria
                if (_parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria && cboCategoriaFinanceira.EditValue == null)
                {
                    if(cboOperadorasCartao.EditValue.ToInt() != 0)
                    {
                        if(cboCondicaoOperadora.EditValue.ToInt() == 0)
                        {
                            MessageBox.Show("Se estiver utilizando uma operadora. É necessário informa a condição para calcular a taxa.");
                            return;
                        }                       
                    }                    

                    MessageBox.Show("Você está usando a Conciliação Bancária. É obrigatório informar a Categoria Financeira.", "Manutenção de Títulos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //Valida se o caixa vai ficar negativo quando for Cheque ou Dinheiro
                if (((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.DINHEIRO || 
                    (EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.CHEQUE) &&
                    _contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.PAGAR)
                {
                    if (OCaixaVaiFicarNegativo((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue))
                    {
                        MessageBox.Show("Ao baixar este título, seu caixa vai ficar negativo. Por favor, verifique seu caixa!.", "Manutenção de Títulos");
                        return;
                    }
                }

                FazerPagamentoParcial();
                EditarContaPagarReceber(new ServicoContasPagarReceber().Consulte(_contaPagarReceber.Id));
                return;
            }
            
            Action actionSalvar = () =>
            {
                var contaPagarReceber = RetorneContaPagarReceberEmEdicao();

                if (!BancoEstahAberto(contaPagarReceber.BancoParaMovimento) && _parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
                {
                    if (MessageBox.Show("O título selecionado está com o banco fechado.\n\nDeseja continuar mesmo assim?", "Baixar Título", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }

                var servicoContaPagarReceber = RetorneServicoContaPagarOuReceber();

                servicoContaPagarReceber.Atualize(contaPagarReceber);

                if((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.CHEQUE)
                {
                    servicoContaPagarReceber.AtualizarChequesContaPagarReceber(contaPagarReceber);                    
                }
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, this, fecharFormAoConcluirOperacao: true);
        }

        private void btnBaixar_Click(object sender, EventArgs e)
        {
            Action actionSalvar = () =>
            {
                var contaPagarReceber = RetorneContaPagarReceberEmEdicao();

                var servicoContaPagarReceber = RetorneServicoContaPagarOuReceber();
                servicoContaPagarReceber.ValideContaPagarReceber(contaPagarReceber);

                FormInserirPagamentoParcial formInserirPagamentoParcial = new FormInserirPagamentoParcial((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue);

                var resultado = formInserirPagamentoParcial.InserirPagamentoParcial(contaPagarReceber, RetorneServicoContaPagarOuReceber());

                if (resultado == System.Windows.Forms.DialogResult.OK)
                {
                    servicoContaPagarReceber = RetorneServicoContaPagarOuReceber();

                    EditarContaPagarReceber(servicoContaPagarReceber.Consulte(contaPagarReceber.Id));
                }
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, exibirMensagemDeSucesso: false);
        }

        private void btnInativar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja inativar este título?", "Inativação de título", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            var contaPagarReceber = RetorneContaPagarReceberEmEdicao();

            var servicoContaPagarReceber = RetorneServicoContaPagarOuReceber();

            if (contaPagarReceber.ValorPago != 0)
            {
                MessageBox.Show("Este título não pode ser Inativado, pois existe(m) parcela(s) paga(s).", "Inativação de Título");
                return;
            }

            Action actionSalvar = () =>
            {
                //Vai definir se vai reservar o pedido ou não, se for Crediário ou Dinheiro.
                bool reservePedido = contaPagarReceber.FormaPagamento.Id == 6 || contaPagarReceber.FormaPagamento.Id == 1? true : false;

                servicoContaPagarReceber.InativeContaPagarReceber(contaPagarReceber, true, reservePedido);

                EditarContaPagarReceber(contaPagarReceber);

                if ((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.CHEQUE)
                {
                    servicoContaPagarReceber.AtualizarChequesContaPagarReceber(contaPagarReceber);
                }

                //Excluir movimentação de banco
                if (_parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
                    new ServicoItemMovimentacaoBanco().ExcluaParcialOrigemPagarReceber(contaPagarReceber);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, mensagemDeSucesso: "Título inativado com sucesso.");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void gcHistoricoAlteracoes_DoubleClick(object sender, EventArgs e)
        {
            var historico = _contaPagarReceber.ListaHistoricoAlteracoesVencimento.FirstOrDefault(x => x.NumeroAlteracao == colunaNumeroAlteracao.View.GetFocusedRowCellValue(colunaNumeroAlteracao).ToInt());

            MessageBox.Show(historico.Observacoes, "Observações");
        }

        private void btnEstornar_Click(object sender, EventArgs e)
        {
            var pagamentoRealizado = _contaPagarReceber.ListaContasPagarReceberParcial.FirstOrDefault(pagamento => pagamento.Id == colunaIdPagamento.View.GetFocusedRowCellValue(colunaIdPagamento).ToInt());

            if (pagamentoRealizado == null) return;

            string msg = "Data de Pagamento: " + pagamentoRealizado.DataPagamento.ToString("dd/MM/yyyy") +
                                            "\nValor Pagamento: R$ " + pagamentoRealizado.Valor.ToString("#,##0.00");

            if ((EnumTipoFormaPagamento)pagamentoRealizado.FormaPagamento.Id == EnumTipoFormaPagamento.DINHEIRO ||
                (EnumTipoFormaPagamento)pagamentoRealizado.FormaPagamento.Id == EnumTipoFormaPagamento.CHEQUE)
            {
                msg += "\n\nEste título possui uma movimentação de caixa, essa movimentação será estornada.";
            }

            msg += "\n\nDeseja estornar este título?";

            if (MessageBox.Show(msg, "Estornar título", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            Action actionSalvar = () =>
            {
                ServicoContasPagarReceberPagamento servicoContasPagarReceberPagamento = new ServicoContasPagarReceberPagamento();
                servicoContasPagarReceberPagamento.EstorneRegistro(pagamentoRealizado);

                var servicoContaPagarReceber = RetorneServicoContaPagarOuReceber();

                EditarContaPagarReceber(servicoContaPagarReceber.Consulte(pagamentoRealizado.ContaPagarReceber.Id));

                if ((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.CHEQUE)
                {
                    servicoContaPagarReceber.AtualizarChequesContaPagarReceber(pagamentoRealizado.ContaPagarReceber);
                }

                //Se tiver movimentação no banco, vamos excluir
                if (_parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
                    new ServicoItemMovimentacaoBanco().ExcluaParcialOrigemPagarReceber(pagamentoRealizado.ContaPagarReceber, true, false, pagamentoRealizado.Valor);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, mensagemDeSucesso: "Título estornado com sucesso!");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja cancelar este título?", "Cancelamento de título", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            var contaPagarReceber = RetorneContaPagarReceberEmEdicao();

            PedidoDeVenda pedidoVenda = new PedidoDeVenda();

            if (contaPagarReceber.ValorPago != 0)
            {
                MessageBox.Show("Este título não pode ser cancelado, pois existe(m) parcela(s) paga(s).", "Cancelamento de Título");
                return;
            }

            if (contaPagarReceber.OrigemDocumento != EnumOrigemDocumento.DIRETOCONTASARECEBER)
            {
                char delimitador = '-';
                string[] separaParcela = contaPagarReceber.NumeroDocumento.Split(delimitador);
                int numeroDuplicata = int.Parse(separaParcela[0].Trim());

                ServicoPedidoDeVenda servicoPedidoVenda = new ServicoPedidoDeVenda();

                pedidoVenda = servicoPedidoVenda.Consulte(numeroDuplicata);

                if (pedidoVenda == null)
                {
                    MessageBox.Show("Este título não pode ser cancelado, pois não existe pedido vinculado ao mesmo.", "Cancelamento de Título");
                    return;
                }
                else
                {
                    bool emiteMensagem = true;
                    foreach (var parcela in pedidoVenda.ListaParcelasPedidoDeVenda)
                    {
                        if (parcela.NumeroDocumento == contaPagarReceber.NumeroDocumento)
                        {
                            emiteMensagem = false;
                        }
                    }

                    if (emiteMensagem)
                    {
                        MessageBox.Show("Este título não pode ser cancelado, pois não existe pedido vinculado ao mesmo.", "Cancelamento de Título");
                        return;
                    }
                }

                if (pedidoVenda.StatusPedidoVenda == Negocio.Vendas.Enumeradores.EnumStatusPedidoDeVenda.EMITIDONFE)
                {
                    MessageBox.Show("Este título não pode ser cancelado, pois existe nota fiscal emitida para o pedido referente ao mesmo.", "Cancelamento de Título");
                    return;
                }
            }
                Action actionSalvar = () =>
            {
                var servicoContaPagarReceber = RetorneServicoContaPagarOuReceber();

                servicoContaPagarReceber.CanceleContaPagarReceber(contaPagarReceber, pedidoVenda.Id);
                
                EditarContaPagarReceber(contaPagarReceber);

                if ((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.CHEQUE)
                {
                    servicoContaPagarReceber.AtualizarChequesContaPagarReceber(contaPagarReceber);
                }

                //Excluir movimentação de banco
                if (_parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
                    new ServicoItemMovimentacaoBanco().ExcluaParcialOrigemPagarReceber(contaPagarReceber);
            };                        
            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, mensagemDeSucesso: "Título Cancelado com sucesso!");
        }

        private void btnCalculeJurosMultas_Click(object sender, EventArgs e)
        {
            RecalcularValorTotal(true);
            _ehCalculoManual = true;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void CarregaComboOperadorasDebitoCredito()
        {
            ServicoOperadorasCartao servicoOperadorasCartao = new ServicoOperadorasCartao();

            var operadoras = servicoOperadorasCartao.ConsulteLista();

            List<ObjetoParaComboBox> listaDebito = new List<ObjetoParaComboBox>();
            List<ObjetoParaComboBox> listaCredito = new List<ObjetoParaComboBox>();

            foreach (var item in operadoras)
            {
                ObjetoParaComboBox objeto = new ObjetoParaComboBox();

                if (!item.PermiteParcelamento)
                {
                    objeto.Descricao = item.Descricao;
                    objeto.Valor = item.Id;

                    listaDebito.Add(objeto);
                }
                else
                {
                    objeto.Descricao = item.Descricao;
                    objeto.Valor = item.Id;

                    listaCredito.Add(objeto);
                }
            }

            if (cboFormaDePagamento.EditValue == null) return;

            if ((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.CARTAODEBITO)
            {
                cboOperadorasCartao.Properties.DisplayMember = "Descricao";
                cboOperadorasCartao.Properties.ValueMember = "Valor";
                cboOperadorasCartao.Properties.DataSource = listaDebito;
            }
            else if ((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.CARTAOCREDITO)
            {
                cboOperadorasCartao.Properties.DisplayMember = "Descricao";
                cboOperadorasCartao.Properties.ValueMember = "Valor";
                cboOperadorasCartao.Properties.DataSource = listaCredito;
            }
            else
            {
                cboOperadorasCartao.Properties.DisplayMember = "Descricao";
                cboOperadorasCartao.Properties.ValueMember = "Id";
                cboOperadorasCartao.Properties.DataSource = null;
            }
        }

        private void PreenchaCboCondicaoPagamento(int codigoFormaPgto)
        {

            if (codigoFormaPgto != 7 && codigoFormaPgto != 8)
            {
                return;
            }

            List<CondicaoPagamento> listaCondicoes = new List<CondicaoPagamento>();                                                                            

            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
            var formaPagamento = servicoFormaPagamento.Consulte(codigoFormaPgto);

            if (formaPagamento != null &&
                    formaPagamento.ListaCondicoesPagamento != null &&
                    formaPagamento.ListaCondicoesPagamento.Count > 0)
            {
                foreach (var item in formaPagamento.ListaCondicoesPagamento)
                {
                    if (item.CondicaoPagamento.Status == "A")
                    {
                        listaCondicoes.Add(item.CondicaoPagamento);
                    }
                }
            }

            listaCondicoes.Insert(0, null);

            cboCondicaoOperadora.Properties.DisplayMember = "Descricao";
            cboCondicaoOperadora.Properties.ValueMember = "Id";
            cboCondicaoOperadora.Properties.DataSource = listaCondicoes;

            if (string.IsNullOrEmpty(cboCondicaoOperadora.Text))
            {
                cboCondicaoOperadora.EditValue = null;
            }

            if (listaCondicoes.Count == 2)
            {
                cboCondicaoOperadora.EditValue = listaCondicoes[1].Id;
            }

        }

        private void HabiliteControlesConciliacaoBancaria()
        {
            if (_parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
            {   
                cboBanco.Visible = true;
                btnAdicionarBanco.Visible = true;
                lblBanco.Visible = true;
                
                cboCategoriaFinanceira.Visible = true;
                btnAdicionarCategoria.Visible = true;
                lblCategoria.Visible = true;

                if (_contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER)
                {
                    cboOperadorasCartao.Enabled = true;
                    cboCondicaoOperadora.Enabled = true;
                    btnAdicionarOperadorasCartao.Enabled = true;
                }
                else
                {
                    cboOperadorasCartao.Enabled = false;
                    cboCondicaoOperadora.Enabled = false;
                    btnAdicionarOperadorasCartao.Enabled = false;
                }

                cboOperadorasCartao.Enabled = true;
                cboCondicaoOperadora.Enabled = true;
                btnAdicionarOperadorasCartao.Enabled = true;
            }
            else
            {
                gcHistoricoAlteracoes.Height = 160;
                
                cboBanco.Visible = false;
                btnAdicionarBanco.Visible = false;
                lblBanco.Visible = false;

                cboCategoriaFinanceira.Visible = false;
                btnAdicionarCategoria.Visible = false;
                lblCategoria.Visible = false;

                cboOperadorasCartao.Enabled = false;
                cboCondicaoOperadora.Enabled = false;
                btnAdicionarOperadorasCartao.Enabled = false;
               
            }
        }

        private void InsiraMovimentacaoBancaria(ContaPagarReceber contasPagarReceber, bool EhPagamentoParcial, 
                                                EnumTipoOperacaoContasPagarReceber tipoOperacao, DateTime DataPagamento)
        {
            ItemMovimentacaoBanco itemMovimentacaoBanco = new ItemMovimentacaoBanco();

            CategoriaFinanceira categoria = new CategoriaFinanceira();
            categoria.Id = cboCategoriaFinanceira.EditValue.ToInt();
            categoria.Descricao = cboCategoriaFinanceira.Text;

            itemMovimentacaoBanco.EstahEstornado = false;
            itemMovimentacaoBanco.Categoria = categoria;
            itemMovimentacaoBanco.Parceiro = contasPagarReceber.Pessoa;

            string descHistorico = tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? "Recebto." : "Pagto.";

            itemMovimentacaoBanco.DescricaoDaMovimentacao = EhPagamentoParcial? descHistorico+" Parcial. " + contasPagarReceber.Historico:
                                                            descHistorico+" Quitado. " + contasPagarReceber.Historico;

            itemMovimentacaoBanco.DataHoraLancamento = DataPagamento;

            itemMovimentacaoBanco.MovimentacaoBanco = new ServicoMovimentacaoBanco().ConsulteBancoAberto(new BancoParaMovimento {Id=cboBanco.EditValue.ToInt()});

            itemMovimentacaoBanco.TipoMovimentacao = tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER? 
                                                     EnumTipoMovimentacaoBanco.ENTRADA:EnumTipoMovimentacaoBanco.SAIDA;

            itemMovimentacaoBanco.OrigemMovimentacaoBanco = tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER? 
                                                            EnumOrigemMovimentacaoBanco.CONTARECEBER:EnumOrigemMovimentacaoBanco.CONTAPAGAR;

            itemMovimentacaoBanco.Valor = txtValorPago.Text.ToDouble();
            itemMovimentacaoBanco.NumeroDocumentoOrigem = contasPagarReceber.NumeroDocumento;
            itemMovimentacaoBanco.ContaPagarReceber = contasPagarReceber;

            //Usuário e Data de atualização / cadastro
            itemMovimentacaoBanco.DataAtualizacao = DateTime.Now;
            itemMovimentacaoBanco.UsuarioAtualizacao = new Pessoa { Id = Sessao.PessoaLogada.Id };

            ServicoItemMovimentacaoBanco servicoItemMovimentacaoBanco = new ServicoItemMovimentacaoBanco();

            servicoItemMovimentacaoBanco.Cadastre(itemMovimentacaoBanco);
        }

        private void PreenchaCboCategorias()
        {
            var tipoCategoria = _contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER? EnumTipoCategoria.RECEITA : EnumTipoCategoria.DESPESA;

            List <CategoriaFinanceira> categoria = new List<CategoriaFinanceira>();
                      
            categoria = new ServicoCategoria().ConsulteLista(string.Empty, null, "A", tipoCategoria);
                        
            categoria.Insert(0, null);

            cboCategoriaFinanceira.Properties.DisplayMember = "Descricao";
            cboCategoriaFinanceira.Properties.ValueMember = "Id";
            cboCategoriaFinanceira.Properties.DataSource = categoria;

            cboCategoriaFinanceira.EditValue = _contaPagarReceber.CategoriaFinanceira != null? 
                                               _contaPagarReceber.CategoriaFinanceira.Id: _contaPagarReceber.Historico != null && 
                                               _contaPagarReceber.Historico.Contains("Pedido") 
                                               && _contaPagarReceber.TipoOperacao != EnumTipoOperacaoContasPagarReceber.PAGAR? 2:0;

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

            banco = new ServicoBancoParaMovimento().ConsulteLista(string.Empty,"A");

            if (banco.Count == 0) return;

            var idbanco = banco.Find(x => x.TornarPadrao == true).Id;

            banco.Insert(0, null);

            cboBanco.Properties.DisplayMember = "NomeBanco";
            cboBanco.Properties.ValueMember = "Id";
            cboBanco.Properties.DataSource = banco;

            cboBanco.EditValue = _contaPagarReceber.BancoParaMovimento != null ?  _contaPagarReceber.BancoParaMovimento.Id : idbanco;

            if (cboBanco.EditValue != null)
            {
                if (!banco.Exists(banc => banc != null && banc.Id == cboBanco.EditValue.ToInt()))
                {
                    cboBanco.EditValue = null;
                }
            }
        }

        private void PreenchaOStatus()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusContaPagarReceber>();

            lista.RemoveRange(2, 3);

            lista.RemoveRange(3, 2);

            cboStatus.Properties.DataSource = lista;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";
        }

        public void EditarContaPagarReceber(ContaPagarReceber contaPagarReceber)
        {
            contaPagarReceber = new ServicoContasPagarReceber().Consulte(contaPagarReceber.Id);
            
            _contaPagarReceber = contaPagarReceber;
            
            PreenchaCamposContaPagarReceber();

            PreenchaGridPagamentosParciais();
            PreenchaGridAlteracoesVencimento();

            DesativeOuAtiveCamposCasoEstejaQuitadoOuInativo();

            EventoAposCarregarTitulo();

            PreenchaCboCategorias();
            
            PreenchaCboBancos();

            txtValorPago.Text = string.Empty;

            this.Height = 653;

            HabiliteControlesConciliacaoBancaria();

            this.Show();
        }

        private void PreenchaCamposContaPagarReceber()
        {
            txtId.Text = _contaPagarReceber.Id.ToString();

            txtIdPessoa.Text = _contaPagarReceber.Pessoa.Id.ToString();
            txtNomePessoa.Text = _contaPagarReceber.Pessoa.DadosGerais.Razao;

            txtDataEmissao.DateTime = _contaPagarReceber.DataEmissao;
            txtDataVencimento.DateTime = _contaPagarReceber.DataVencimento.GetValueOrDefault();

            txtNumeroDocumento.ReadOnly = _contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.PAGAR ? false : true;

            txtNumeroDocumento.Text = _contaPagarReceber.NumeroDocumento;

            cboOperadorasCartao.EditValue = _contaPagarReceber.OperadorasCartao !=null? _contaPagarReceber.OperadorasCartao.Id:0;

            if (_contaPagarReceber.Status != EnumStatusContaPagarReceber.QUITADO || _contaPagarReceber.Status != EnumStatusContaPagarReceber.CONCILIADOQUITADO)
            {

                if ((EnumTipoFormaPagamento)_contaPagarReceber.FormaPagamento.Id == EnumTipoFormaPagamento.CREDIARIOPROPRIO)
                {
                    cboFormaDePagamento.EditValue = (int)EnumTipoFormaPagamento.DINHEIRO;
                }
                else if ((EnumTipoFormaPagamento)_contaPagarReceber.FormaPagamento.Id == EnumTipoFormaPagamento.DUPLICATA)
                {
                    cboFormaDePagamento.EditValue = (int)EnumTipoFormaPagamento.DINHEIRO;
                }
                else
                {
                    cboFormaDePagamento.EditValue = _contaPagarReceber.FormaPagamento != null ? (int?)_contaPagarReceber.FormaPagamento.Id : null;
                }
            }
            else
            {
                cboFormaDePagamento.EditValue = _contaPagarReceber.FormaPagamento != null ? (int?)_contaPagarReceber.FormaPagamento.Id : null;
            }

            if(_contaPagarReceber.FormaPagamento != null)
            {
                PreenchaCboCondicaoPagamento(_contaPagarReceber.FormaPagamento.Id);
            }

            //Condição da Operadora
            if(_contaPagarReceber.CondicaoPgtoId != 0)
            {
                cboCondicaoOperadora.EditValue = _contaPagarReceber.CondicaoPgtoId;
            }

            PreenchaPlanoDeContas(_contaPagarReceber.PlanoDeContas);

            txtHistorico.Text = _contaPagarReceber.Historico;

            if (_contaPagarReceber.DataPagamento != null)
            {
                txtDataPagamento.Text = _contaPagarReceber.DataPagamento.GetValueOrDefault().ToString("dd/MM/yyyy");
            }
            else
            {
                txtDataPagamento.Text = string.Empty;
            }

            rdbMultaPercentual.Checked = _contaPagarReceber.MultaEhPercentual;
            rdbJurosPercentual.Checked = _contaPagarReceber.JurosEhPercentual;

            txtValorParcela.Text = _contaPagarReceber.ValorParcela.ToString("0.00");
            txtMulta.Text = _contaPagarReceber.Multa.ToString("0.00");
            txtJuros.Text = _contaPagarReceber.Juros.ToString("0.00");
            txtDesconto.Text = _contaPagarReceber.Desconto.ToString("0.00");
            //txtValorPago.Text = _contaPagarReceber.ValorPago.ToString("#,###,##0.00");
                       
            _contaPagarReceber.ehCalculoMultaAutomatica = _parametros.ParametrosFinanceiro.MultaContasReceber > 0? true:false;                        
                        
            txtValorAbertoAReceber.Text = (_contaPagarReceber.ValorTotal - _contaPagarReceber.ValorPago).ToString("#,###,##0.00");
            
            cboStatus.EditValue = (EnumStatusContaPagarReceber)_contaPagarReceber.Status;

            txtUsuario.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.Razao;
        }

        private void DesativeOuAtiveCamposCasoEstejaQuitadoOuInativo()
        {
            if (_contaPagarReceber.Status == EnumStatusContaPagarReceber.INATIVO || _contaPagarReceber.Status == EnumStatusContaPagarReceber.QUITADO
                || _contaPagarReceber.Status == EnumStatusContaPagarReceber.CANCELADO || _contaPagarReceber.Status == EnumStatusContaPagarReceber.CONCILIADOQUITADO)
            {
                cboFormaDePagamento.Enabled = false;
                txtNumeroPlanoDeContas.Properties.ReadOnly = true;

                //Conciliação Bancária
                cboOperadorasCartao.Enabled = false;
                cboCategoriaFinanceira.Enabled = false;
                cboBanco.Enabled = false;

                btnPesquisaPlanoDeContas.Enabled = false;
                txtHistorico.Properties.ReadOnly = true;

                txtDataEmissao.Enabled = false;
                txtDataVencimento.Enabled = false;

                txtValorPago.Enabled = false;

                txtValorParcela.Enabled = false;
                txtMulta.Enabled = false;
                txtJuros.Enabled = false;
                txtDesconto.Enabled = false;
                txtDataPagamento.Enabled = false;
                pnlMultaEhPercentual.Enabled = false;
                pnlJurosEhPercentual.Enabled = false;

                btnSalvar.Visible = false;
                btnBaixar.Visible = false;

                btnEstornar.Visible = true;
                btnInativar.Visible = true;
                btnCancelar.Visible = true;

                if (_contaPagarReceber.Status == EnumStatusContaPagarReceber.INATIVO || _contaPagarReceber.Status == EnumStatusContaPagarReceber.CANCELADO)
                {
                    btnEstornar.Visible = false;
                    btnInativar.Visible = false;
                    btnCancelar.Visible = false;
                }
                
                if (this.NomeDaTela == "Manutenção Conta à Pagar")
                    btnCancelar.Visible = false;
            }
            else
            {
                cboFormaDePagamento.Enabled = true;
                txtNumeroPlanoDeContas.Properties.ReadOnly = false;

                //Conciliação Bancária
                cboOperadorasCartao.Enabled = true;
                cboCategoriaFinanceira.Enabled = true;
                cboBanco.Enabled = true;

                btnPesquisaPlanoDeContas.Enabled = true;
                txtHistorico.Properties.ReadOnly = false;

                txtDataEmissao.Enabled = true;
                txtDataVencimento.Enabled = true;

                txtValorPago.Enabled = true;

                txtValorParcela.Enabled = true;
                txtMulta.Enabled = true;
                txtJuros.Enabled = true;
                txtDesconto.Enabled = true;
                txtDataPagamento.Enabled = true;
                pnlMultaEhPercentual.Enabled = true;
                pnlJurosEhPercentual.Enabled = true;

                btnSalvar.Visible = true;
                btnBaixar.Visible = false;
                btnInativar.Visible = true;
                btnCancelar.Visible = true;
                btnEstornar.Visible = false;

                if (_contaPagarReceber.ListaContasPagarReceberParcial.Count > 0)
                {
                    btnEstornar.Visible = true;
                }
                
                if (this.NomeDaTela == "Manutenção Conta à Pagar")
                    btnCancelar.Visible = false;
            }
        }

        protected virtual void PreenchaPlanoDeContas(PlanoDeContas planoDeContas, bool exibirMensagemDeNaoEncontrado = false)
        {
            _planoDeContas = planoDeContas;

            if (planoDeContas != null)
            {
                txtNumeroPlanoDeContas.Text = planoDeContas.NumeroPlanoDeContas;
                txtDescricaoPlanoContas.Text = planoDeContas.Descricao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Plano de Contas nao encontrado!", "Plano de Contas não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdPessoa.Focus();
                }

                txtNumeroPlanoDeContas.Text = string.Empty;
                txtDescricaoPlanoContas.Text = string.Empty;
            }
        }

        private void PreenchaCboFormaPagamento()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

            var listaFormasPagamentos = servicoFormaPagamento.ConsulteListaAtivos();

            listaFormasPagamentos.Insert(0, null);

            cboFormaDePagamento.Properties.DataSource = listaFormasPagamentos;
            cboFormaDePagamento.Properties.DisplayMember = "Descricao";
            cboFormaDePagamento.Properties.ValueMember = "Id";
        }

        private void PreenchaGridAlteracoesVencimento()
        {
            List<HistoricoAlteracaoGrid> listaHistoricoAlteracoesGrid = new List<HistoricoAlteracaoGrid>();

            foreach (var item in _contaPagarReceber.ListaHistoricoAlteracoesVencimento)
            {
                HistoricoAlteracaoGrid historicoAlteracaoGrid = new HistoricoAlteracaoGrid();

                historicoAlteracaoGrid.DataAlteracao = item.DataAlteracao.ToString("dd/MM/yyyy");
                historicoAlteracaoGrid.DataVencimento = item.DataVencimento.ToString("dd/MM/yyyy");

                historicoAlteracaoGrid.Desconto = item.Desconto.ToString("#,###,##0.00");
                historicoAlteracaoGrid.Juros = item.Juros.ToString("#,###,##0.00");
                historicoAlteracaoGrid.Multa = item.Multa.ToString("#,###,##0.00");
                historicoAlteracaoGrid.NumeroAlteracao = item.NumeroAlteracao.ToString();
                historicoAlteracaoGrid.Usuario = item.Usuario.Id + " - " + item.Usuario.DadosGerais.Razao;
                historicoAlteracaoGrid.ValorParcela = item.Valor.ToString("#,###,##0.00");
                historicoAlteracaoGrid.ValorTotal = item.ValorTotal.ToString("#,###,##0.00");

                listaHistoricoAlteracoesGrid.Add(historicoAlteracaoGrid);
            }

            gcHistoricoAlteracoes.DataSource = listaHistoricoAlteracoesGrid;
            gcHistoricoAlteracoes.RefreshDataSource();
        }

        private void PreenchaGridPagamentosParciais()
        {
            List<PagamentoParcialGrid> listapagamentosParciaisGrid = new List<PagamentoParcialGrid>();

            _contaPagarReceber.ListaContasPagarReceberParcial = _contaPagarReceber.ListaContasPagarReceberParcial.OrderByDescending(x => x.DataPagamento).ToList();

            foreach (var item in _contaPagarReceber.ListaContasPagarReceberParcial)
            {
                PagamentoParcialGrid pagamentoParcialGrid = new PagamentoParcialGrid();

                pagamentoParcialGrid.Id = item.Id;

                pagamentoParcialGrid.DataPagamento = item.DataPagamento.ToString("dd/MM/yyyy");

                pagamentoParcialGrid.FormaPagamento = new ServicoFormaPagamento().Consulte(item.FormaPagamento.Id).Descricao;

                pagamentoParcialGrid.Observacoes = item.Observacoes;
                pagamentoParcialGrid.Valor = item.Valor;
                pagamentoParcialGrid.Responsavel = item.Responsavel.Id + " - " + item.Responsavel.DadosGerais.Razao;
                pagamentoParcialGrid.EstahEstornado = item.EstahEstornado;

                listapagamentosParciaisGrid.Add(pagamentoParcialGrid);
            }

            gcPagamentosParciais.DataSource = listapagamentosParciaisGrid;
            gcPagamentosParciais.RefreshDataSource();
        }

        private void RecalcularValorTotal(bool ehCalculoManual = false)
        {
            var cloneContaPagarReceber = _contaPagarReceber.CloneCompleto();

            cloneContaPagarReceber.ValorParcela = txtValorParcela.Text.ToDouble();
            cloneContaPagarReceber.Multa = txtMulta.Text.ToDouble();
            cloneContaPagarReceber.Juros = txtJuros.Text.ToDouble();
            cloneContaPagarReceber.Desconto = txtDesconto.Text.ToDouble();

            cloneContaPagarReceber.MultaEhPercentual = rdbMultaPercentual.Checked;
            cloneContaPagarReceber.JurosEhPercentual = rdbJurosPercentual.Checked;

            cloneContaPagarReceber.ListaHistoricoAlteracoesVencimento = _contaPagarReceber.ListaHistoricoAlteracoesVencimento;

            if (!string.IsNullOrEmpty(txtDataPagamento.Text))
            {
                cloneContaPagarReceber.Status = EnumStatusContaPagarReceber.QUITADO;

                cloneContaPagarReceber.DataPagamento = txtDataPagamento.Text.ToDate();
            }

            cloneContaPagarReceber.DataVencimento = txtDataVencimento.Text.ToDate();

            var valorTotal = CalculoValorTotalContaPagarReceber.CalculeValorTotalContaPagarReceber(cloneContaPagarReceber, ehCalculoManual);
            
            txtValorPago.Text = _contaPagarReceber.ListaContasPagarReceberParcial.Sum(x => !x.EstahEstornado ? x.Valor : 0).ToString("#,##0.00");
                        
            txtValorAbertoAReceber.Text = (valorTotal - txtValorPago.Text.ToDouble()).ToString("#,##0.00");
            
        }

        private ContaPagarReceber RetorneContaPagarReceberEmEdicao()
        {
            ServicoContasPagarReceber servicoContasPagarReceber = RetorneServicoContaPagarOuReceber();

            var contaPagarReceber = servicoContasPagarReceber.Consulte(_contaPagarReceber.Id);

            contaPagarReceber.Pessoa.CarregueLazyLoad();
            contaPagarReceber.Usuario.CarregueLazyLoad();
            contaPagarReceber.FormaPagamento.CarregueLazyLoad();
            contaPagarReceber.PlanoDeContas.CarregueLazyLoad();

            contaPagarReceber.ListaHistoricoAlteracoesVencimento.CarregueLazyLoad();
            contaPagarReceber.ListaContasPagarReceberParcial.CarregueLazyLoad();

            contaPagarReceber.ListaContasPagarReceberParcial.Clear();

            contaPagarReceber.ValorPago = 0;

            foreach (var item in _contaPagarReceber.ListaContasPagarReceberParcial)
            {
                contaPagarReceber.ListaContasPagarReceberParcial.Add(item);

                if (!item.EstahEstornado)
                {
                    contaPagarReceber.ValorPago += item.Valor;
                }
            }

            contaPagarReceber.ValorParcela = txtValorParcela.Text.ToDouble();

            contaPagarReceber.Multa = txtMulta.Text.ToDouble();
            contaPagarReceber.Juros = txtJuros.Text.ToDouble();
            contaPagarReceber.Desconto = txtDesconto.Text.ToDouble();

            contaPagarReceber.MultaEhPercentual = rdbMultaPercentual.Checked;
            contaPagarReceber.JurosEhPercentual = rdbJurosPercentual.Checked;

            contaPagarReceber.DataEmissao = txtDataEmissao.Text.ToDate();

            contaPagarReceber.DataVencimento = txtDataVencimento.Text.ToDate();

            contaPagarReceber.DataPagamento = txtDataPagamento.Text.ToDateNullabel();

            contaPagarReceber.FormaPagamento = cboFormaDePagamento.EditValue != null ? new FormaPagamento { Id = cboFormaDePagamento.EditValue.ToInt() } : null;

            contaPagarReceber.Status = (EnumStatusContaPagarReceber)cboStatus.EditValue;

            contaPagarReceber.PlanoDeContas = _planoDeContas;

            contaPagarReceber.NumeroDocumento = txtNumeroDocumento.Text;

            //Movimentação Bancária
            contaPagarReceber.BancoParaMovimento = cboBanco.EditValue != null ? new BancoParaMovimento { Id = cboBanco.EditValue.ToInt() } : null;
            contaPagarReceber.CategoriaFinanceira = cboCategoriaFinanceira.EditValue != null ? new CategoriaFinanceira { Id = cboCategoriaFinanceira.EditValue.ToInt() } : null;
            contaPagarReceber.OperadorasCartao = cboOperadorasCartao.EditValue.ToInt() != 0 ? new OperadorasCartao { Id = cboOperadorasCartao.EditValue.ToInt() } : null;
            contaPagarReceber.CondicaoPgtoId = cboCondicaoOperadora.EditValue.ToInt() != 0 ? cboCondicaoOperadora.EditValue.ToInt() : 0;

            contaPagarReceber.Historico = txtHistorico.Text;

            contaPagarReceber.Usuario = Sessao.PessoaLogada;

            return contaPagarReceber;
        }

        protected virtual ServicoContasPagarReceber RetorneServicoContaPagarOuReceber()
        {
            return null;
        }

        protected virtual void EventoAposCarregarTitulo()
        {

        }

        private bool BancoEstahAberto(BancoParaMovimento banco)
        {
            if (banco == null) return false;

            var bancoMov = new ServicoMovimentacaoBanco().ConsulteBancoAberto(banco);

            if (bancoMov == null)
                return false;

            return true;
        }

        private ContaPagarReceberPagamento RetorneContaPagarReceberPagamento()
        {
            ContaPagarReceberPagamento contaPagarReceberPagamento = new ContaPagarReceberPagamento();

            contaPagarReceberPagamento.DataPagamento = txtDataPagamento.Text.ToDate();
            contaPagarReceberPagamento.FormaPagamento = new FormaPagamento { Id = cboFormaDePagamento.EditValue.ToInt(), TipoFormaPagamento = (EnumTipoFormaPagamento)cboFormaDePagamento.EditValue };

            contaPagarReceberPagamento.Observacoes = txtHistorico.Text;
            contaPagarReceberPagamento.Valor = txtValorPago.Text.ToDouble();
            contaPagarReceberPagamento.Responsavel = Sessao.PessoaLogada;

            return contaPagarReceberPagamento;
        }

        private void BusqueContaPagarReceberEmEdicao()
        {
            //Movimentação Bancária
            _contaPagarReceber.BancoParaMovimento = cboBanco.EditValue.ToInt() != 0 ? new BancoParaMovimento { Id = cboBanco.EditValue.ToInt() } : null;
            _contaPagarReceber.CategoriaFinanceira = cboCategoriaFinanceira.EditValue.ToInt() != 0 ? new CategoriaFinanceira { Id = cboCategoriaFinanceira.EditValue.ToInt() } : null;
            _contaPagarReceber.OperadorasCartao = cboOperadorasCartao.EditValue.ToInt() != 0 ? new OperadorasCartao { Id = cboOperadorasCartao.EditValue.ToInt() } : null;
            _contaPagarReceber.CondicaoPgtoId = cboCondicaoOperadora.EditValue.ToInt() != 0 ? cboCondicaoOperadora.EditValue.ToInt() : 0;

            _contaPagarReceber.Multa = txtMulta.Text.ToDouble();
            _contaPagarReceber.MultaEhPercentual = rdbMultaPercentual.Checked;
            _contaPagarReceber.Juros = txtJuros.Text.ToDouble();
            _contaPagarReceber.JurosEhPercentual = rdbJurosPercentual.Checked;
            _contaPagarReceber.Desconto = txtDesconto.Text.ToDouble();
            _contaPagarReceber.ehCalculoDeJurosMultaManual = _ehCalculoManual;
        }

        public void FazerPagamentoParcial()
        {
            Action actionSalvar = () =>
            {
                ServicoContasPagarReceberPagamento servicoContasPagarReceberPagamento = new ServicoContasPagarReceberPagamento();

                ServicoContasReceber servicoContasReceber = new ServicoContasReceber();

                ServicoContasPagar servicoContasPagar = new ServicoContasPagar();

                ServicoItemMovimentacaoBanco servicoItem = new ServicoItemMovimentacaoBanco();

                string mensagemSucesso = string.Empty;

                //Pagamentos realizados no contas a pagar e receber
                var contaPagarReceberPagamento = RetorneContaPagarReceberPagamento();

                //Busca as modificações feitas no Contas a Pagar e Receber
                BusqueContaPagarReceberEmEdicao();

                var total = CalculoValorTotalContaPagarReceber.CalculeValorTotalContaPagarReceber(_contaPagarReceber, _ehCalculoManual);
                
                contaPagarReceberPagamento.ContaPagarReceber = _contaPagarReceber;

                servicoContasPagarReceberPagamento.ValideContaPagarReceberPagamento(contaPagarReceberPagamento);
                
                var contaPagarReceberClonado = _contaPagarReceber.CloneCompleto();

                contaPagarReceberClonado.ValorPago = Math.Round(contaPagarReceberClonado.ListaContasPagarReceberParcial.Sum(x => !x.EstahEstornado ? x.Valor : 0) +
                                                                             contaPagarReceberPagamento.Valor,2);
               
                if (!BancoEstahAberto(contaPagarReceberClonado.BancoParaMovimento) && _parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
                {
                    if (MessageBox.Show("O título selecionado está com o banco fechado.\n\nDeseja continuar mesmo assim?", "Baixar Título", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }
                               
                if (contaPagarReceberClonado.ValorPago.ToString("0.00") == contaPagarReceberClonado.ValorTotal.ToString("0.00"))
                {
                    if (MessageBox.Show("O Valor à Pagar é igual ao Valor à Receber, portanto esse título será baixado.\n\nDeseja continuar?", "Baixar Título", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }

                    contaPagarReceberClonado.Status = EnumStatusContaPagarReceber.QUITADO;
                    contaPagarReceberClonado.DataPagamento = txtDataPagamento.Text.ToDate();                                        
                    
                    if (_contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER)
                    {
                        var categoriaFinanceira = cboCategoriaFinanceira.EditValue.ToInt() == 0 ? null :
                                                                new CategoriaFinanceira { Id = cboCategoriaFinanceira.EditValue.ToInt() };

                        var banco = cboBanco.EditValue.ToInt() == 0 ? null :
                                                                new BancoParaMovimento { Id = cboBanco.EditValue.ToInt() };

                        var usuario = new Pessoa { Id = Sessao.PessoaLogada.Id };

                        var operadoraCartao = cboOperadorasCartao.EditValue.ToInt() == 0 ? null :
                                                               new OperadorasCartao { Id = cboOperadorasCartao.EditValue.ToInt() };

                        servicoContasReceber.Atualize(contaPagarReceberClonado);

                        servicoContasReceber.ConcluaContasReceber(contaPagarReceberClonado, contaPagarReceberPagamento, 
                                                                (EnumTipoFormaPagamento)cboFormaDePagamento.EditValue, 
                                                                _parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria,
                                                                categoriaFinanceira, banco, txtValorPago.Text.ToDouble(), usuario, operadoraCartao);                        
                    }
                    else
                    {
                        servicoContasPagar.Atualize(contaPagarReceberClonado);

                        contaPagarReceberClonado.ListaContasPagarReceberParcial = new List<ContaPagarReceberPagamento>();

                        contaPagarReceberClonado.ListaContasPagarReceberParcial.Add(contaPagarReceberPagamento);

                        servicoContasPagarReceberPagamento.CadastreLista(contaPagarReceberClonado.ListaContasPagarReceberParcial.ToList());
                        servicoContasPagarReceberPagamento.Cadastre(contaPagarReceberPagamento);

                        //Movimentação Bancária
                        if ((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue != EnumTipoFormaPagamento.DINHEIRO &&
                            _parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
                        {
                            servicoItem.InsiraMovimentacaoBancaria(contaPagarReceberClonado, false,
                                                               EnumTipoOperacaoContasPagarReceber.PAGAR, contaPagarReceberClonado.DataPagamento.Value,
                                                               cboCategoriaFinanceira.EditValue.ToInt() == 0? null:
                                                               new CategoriaFinanceira { Id = cboCategoriaFinanceira.EditValue.ToInt() },
                                                               cboBanco.EditValue.ToInt() == 0? null:
                                                               new BancoParaMovimento { Id = cboBanco.EditValue.ToInt() },
                                                               txtValorPago.Text.ToDouble(), new Pessoa { Id = Sessao.PessoaLogada.Id });
                        }
                    }

                    if ((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.CHEQUE)
                    {
                        if (_contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER)
                            servicoContasReceber.AtualizarChequesContaPagarReceber(contaPagarReceberClonado);
                        else
                            servicoContasPagar.AtualizarChequesContaPagarReceber(contaPagarReceberClonado);
                    }

                    mensagemSucesso = "Título quitado com sucesso.";
                }
                else
                {
                    if (MessageBox.Show("Será criado um recebimento parcial.\n\nDeseja Continuar?", "Recebimento Parcial", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                    
                    if (_contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER)
                    {
                        servicoContasReceber.Atualize(contaPagarReceberClonado);

                        contaPagarReceberClonado.ListaContasPagarReceberParcial = new List<ContaPagarReceberPagamento>();

                        contaPagarReceberClonado.ListaContasPagarReceberParcial.Add(contaPagarReceberPagamento);

                        servicoContasPagarReceberPagamento.CadastreLista(contaPagarReceberClonado.ListaContasPagarReceberParcial.ToList());
                        servicoContasPagarReceberPagamento.Cadastre(contaPagarReceberPagamento);

                        //Movimentação Bancária
                        if ((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue != EnumTipoFormaPagamento.DINHEIRO && 
                            _parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
                        {
                            new ServicoItemMovimentacaoBanco().InsiraMovimentacaoBancaria(contaPagarReceberClonado, true,
                                                                EnumTipoOperacaoContasPagarReceber.RECEBER, txtDataPagamento.DateTime,
                                                                cboCategoriaFinanceira.EditValue.ToInt() == 0? null:
                                                                new CategoriaFinanceira { Id = cboCategoriaFinanceira.EditValue.ToInt() },
                                                                cboBanco.EditValue.ToInt() == 0? null:
                                                                new BancoParaMovimento { Id = cboBanco.EditValue.ToInt() },
                                                                txtValorPago.Text.ToDouble(), new Pessoa { Id = Sessao.PessoaLogada.Id });

                            //Calcular despesas de cartões e lançar no banco
                            if ((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue != EnumTipoFormaPagamento.CARTAOCREDITO ||
                                (EnumTipoFormaPagamento)cboFormaDePagamento.EditValue != EnumTipoFormaPagamento.CARTAODEBITO)
                            {
                                new ServicoItemMovimentacaoBanco().CalculeDespesasCartoes(contaPagarReceberClonado, false,
                                                               EnumTipoOperacaoContasPagarReceber.PAGAR, txtDataPagamento.DateTime,
                                                               cboBanco.EditValue.ToInt() == 0? null:
                                                               new BancoParaMovimento { Id = cboBanco.EditValue.ToInt() },
                                                               cboOperadorasCartao.EditValue.ToInt() == 0? null:
                                                               new OperadorasCartao { Id = cboOperadorasCartao.EditValue.ToInt() },
                                                               txtValorPago.Text.ToDouble(), new Pessoa { Id = Sessao.PessoaLogada.Id });
                            }
                        }
                    }
                    else
                    {
                        servicoContasPagar.Atualize(contaPagarReceberClonado);

                        contaPagarReceberClonado.ListaContasPagarReceberParcial = new List<ContaPagarReceberPagamento>();

                        contaPagarReceberClonado.ListaContasPagarReceberParcial.Add(contaPagarReceberPagamento);

                        servicoContasPagarReceberPagamento.CadastreLista(contaPagarReceberClonado.ListaContasPagarReceberParcial.ToList());
                        servicoContasPagarReceberPagamento.Cadastre(contaPagarReceberPagamento);

                        //Movimentação Bancária
                        if ((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue != EnumTipoFormaPagamento.DINHEIRO &&
                            _parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria) 
                            
                        {
                            new ServicoItemMovimentacaoBanco().InsiraMovimentacaoBancaria(contaPagarReceberClonado, true,
                                                              EnumTipoOperacaoContasPagarReceber.PAGAR, txtDataPagamento.DateTime,
                                                              cboCategoriaFinanceira.EditValue.ToInt() == 0? null:
                                                              new CategoriaFinanceira {Id = cboCategoriaFinanceira.EditValue.ToInt()},
                                                              cboBanco.EditValue.ToInt() == 0? null:
                                                              new BancoParaMovimento {Id = cboBanco.EditValue.ToInt()},
                                                              txtValorPago.Text.ToDouble(), new Pessoa {Id = Sessao.PessoaLogada.Id});
                        }  
                    }

                    mensagemSucesso = "Pagamento parcial inserido com sucesso.";
                }

                MessageBox.Show(mensagemSucesso, "Cadastro Salvo", MessageBoxButtons.OK);
                
                //this.Close();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, exibirMensagemDeSucesso: false);
        }
        
        #endregion

        #region " CLASSES AUXILIARES "

        private class HistoricoAlteracaoGrid
        {
            public string NumeroAlteracao { get; set; }

            public string Usuario { get; set; }

            public string DataAlteracao { get; set; }

            public string DataVencimento { get; set; }

            public string ValorParcela { get; set; }

            public string Multa { get; set; }

            public string Juros { get; set; }

            public string Desconto { get; set; }

            public string ValorTotal { get; set; }
        }

        private class PagamentoParcialGrid
        {
            public int Id { get; set; }

            public string DataPagamento { get; set; }

            public double Valor { get; set; }

            public string FormaPagamento { get; set; }

            public string Observacoes { get; set; }

            public string Responsavel { get; set; }

            public bool EstahEstornado { get; set; }
        }

        #endregion
                
    }
}
