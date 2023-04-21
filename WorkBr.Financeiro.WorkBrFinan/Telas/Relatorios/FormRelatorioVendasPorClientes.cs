using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormRelatorioVendasPorClientes : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private Pessoa _clienteSelecionado;
        private List<FormaPagamento> _listaFormasPagamentos;
        private List<Caixa> _listaCaixas;
        private Parametros _parametros;

        #endregion

        #region " CONSTRUTOR "

        public FormRelatorioVendasPorClientes()
        {
            InitializeComponent();

            PreenchaCboTipoCliente();
            PreenchaCboTipoPedido();
            PreenchaPrimeiroEUltimoDiaMes();
            PreenchalstFormasPagamento();
            //PreenchaCboFormaPagamento();
            PreenchaCboCaixa();
            CarregueParametros();
        }

        #endregion

        #region " EVENTOS CONTROLES "
        private void CarregueParametros()
        {
            ServicoParametros servicoParametros = new ServicoParametros();

            _parametros = servicoParametros.ConsulteParametros();

            
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            EnumTipoPessoa? tipoPessoa = (EnumTipoPessoa?)cboTipoCliente.EditValue;
                        
            bool statusAberto = chkStatusAberto.Checked;
            bool statusOrcamento = chkStatusOrcamento.Checked;
            bool statusCancelado = chkStatusCancelado.Checked;
            bool statusEmLiberacao = chkStatusEmLiberacao.Checked;
            bool statusRecusado = chkStatusRecusado.Checked;
            bool statusReservado = chkStatusReservado.Checked;
            bool statusFaturado = chkStatusFaturado.Checked;
            bool statusEmitidoNFe = chkStatusEmitidoNFe.Checked;

            List<FormaPagamento> listaFormasPagamento = new List<FormaPagamento>();

            for (int i = 0; i < lstFormasPagamentos.SelectedItems.Count; i++)
            {
                string textoFormaPagamento = lstFormasPagamentos.SelectedItems[i].ToString();
                string[] codFormaPagamento;
                
                codFormaPagamento = textoFormaPagamento.Split('-');                

                FormaPagamento formaPagamento = lstFormasPagamentos.SelectedItem != null ? new FormaPagamento { Id = codFormaPagamento[0].Trim().ToInt(), Descricao = codFormaPagamento[1].Trim() } : null;

                listaFormasPagamento.Add(formaPagamento);
            }

            //string formaPagamento = cboFormaPagamentoFinanceiro.Text != string.Empty? cboFormaPagamentoFinanceiro.Text:null;

            Caixa caixa = cboCaixa.EditValue != null && cboCaixa.Enabled==true? new Caixa { Id = cboCaixa.ItemIndex } : null;

            EnumTipoPedidoDeVenda? tipoPedidoVenda = (EnumTipoPedidoDeVenda?)cboTipoPedido.EditValue;

            DateTime dataInicial = rdbPeriodoFaturamento.Checked ? txtDataInicialFaturamento.Text.ToDate() : txtDataInicialEmissao.Text.ToDate();
            DateTime dataFinal = rdbPeriodoFaturamento.Checked ? txtDataFinalFaturamento.Text.ToDate() : txtDataFinalEmissao.Text.ToDate();

            string inconsistencias = string.Empty;

            inconsistencias += dataInicial == DateTime.MinValue ? "Informe a data inicial do período.\n\n" : string.Empty;
            inconsistencias += dataFinal == DateTime.MinValue ? "Informe a data final do período.\n\n" : string.Empty;

            if (!string.IsNullOrEmpty(inconsistencias))
            {
                MessageBox.Show(inconsistencias);

                return;
            }

            EnumOrdenacaoPesquisaVwVendas ordenacao = (EnumOrdenacaoPesquisaVwVendas)pnlOrdenacao.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();
            if (_parametros.ParametrosVenda.ExibirInfoPedido == false)
            {
                RelatorioVendasClientes relatorioVendas = new RelatorioVendasClientes(_clienteSelecionado,
                                                                                                                      tipoPessoa,
                                                                                                                      tipoPedidoVenda,
                                                                                                                      rdbPeriodoFaturamento.Checked,
                                                                                                                      dataInicial,
                                                                                                                      dataFinal,
                                                                                                                      statusAberto,
                                                                                                                      statusOrcamento,
                                                                                                                      statusCancelado,
                                                                                                                      statusEmLiberacao,
                                                                                                                      statusRecusado,
                                                                                                                      statusReservado,
                                                                                                                      statusFaturado,
                                                                                                                      statusEmitidoNFe,
                                                                                                                      ordenacao,
                                                                                                                      listaFormasPagamento,
                                                                                                                      caixa);

                TratamentosDeTela.ExibirRelatorio(relatorioVendas);
            }
            else
            { 
            RelatorioVendasClientesII relatorioVendas = new RelatorioVendasClientesII(_clienteSelecionado,
                                                                                                                     tipoPessoa,
                                                                                                                     tipoPedidoVenda,
                                                                                                                     rdbPeriodoFaturamento.Checked,
                                                                                                                     dataInicial,
                                                                                                                     dataFinal,
                                                                                                                     statusAberto,
                                                                                                                     statusOrcamento,
                                                                                                                     statusCancelado,
                                                                                                                     statusEmLiberacao,
                                                                                                                     statusRecusado,
                                                                                                                     statusReservado,
                                                                                                                     statusFaturado,
                                                                                                                     statusEmitidoNFe,
                                                                                                                     ordenacao,
                                                                                                                     listaFormasPagamento,
                                                                                                                     caixa);

            TratamentosDeTela.ExibirRelatorio(relatorioVendas);
            }

            this.Cursor = Cursors.Default;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdbPeriodoFaturamento_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPeriodoFaturamento.Checked)
            {
                txtDataInicialEmissao.Enabled = false;
                txtDataInicialEmissao.Text = string.Empty;

                txtDataFinalEmissao.Enabled = false;
                txtDataFinalEmissao.Text = string.Empty;

                txtDataInicialFaturamento.Enabled = true;
                txtDataFinalFaturamento.Enabled = true;
            }
            else
            {
                txtDataInicialEmissao.Enabled = true;
                txtDataFinalEmissao.Enabled = true;

                txtDataInicialFaturamento.Enabled = false;
                txtDataInicialFaturamento.Text = string.Empty;

                txtDataFinalFaturamento.Enabled = false;
                txtDataFinalFaturamento.Text = string.Empty;
            }
        }

        private void txtIdCliente_Leave(object sender, EventArgs e)
        {
            if (_clienteSelecionado == null || txtIdCliente.Text.ToInt() != _clienteSelecionado.Id)
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

        private void cboFormaPagamentoFinanceiro_EditValueChanged(object sender, EventArgs e)
        {
            HabilitaDesabilitaCaixa();
        }

        private void chkStatusFaturado_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaDesabilitaCaixa();
        }

        private void cboTipoPedido_EditValueChanged(object sender, EventArgs e)
        {
            HabilitaDesabilitaCaixa();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void HabilitaDesabilitaCaixa()
        {
            if (cboFormaPagamentoFinanceiro.EditValue == null || cboTipoPedido.EditValue == null )
            { 
                cboCaixa.Enabled = false;
                return;
            }

            if (chkStatusFaturado.Checked &&
               ((EnumTipoFormaPagamento)cboFormaPagamentoFinanceiro.EditValue == EnumTipoFormaPagamento.DINHEIRO ||
               (EnumTipoFormaPagamento)cboFormaPagamentoFinanceiro.EditValue == EnumTipoFormaPagamento.CHEQUE) &&
               (EnumTipoPedidoDeVenda)cboTipoPedido.EditValue == EnumTipoPedidoDeVenda.PEDIDOVENDA)
                cboCaixa.Enabled = true;
            else
                cboCaixa.Enabled = false;
        }

        private void PreenchaCboTipoPedido()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoPedidoDeVenda>();

            lista.Insert(0, null);

            cboTipoPedido.Properties.DataSource = lista;
            cboTipoPedido.Properties.ValueMember = "Valor";
            cboTipoPedido.Properties.DisplayMember = "Descricao";
        }

        private void PreenchaCboTipoCliente()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoPessoa>();

            lista.Insert(0, null);

            cboTipoCliente.Properties.DataSource = lista;
            cboTipoCliente.Properties.ValueMember = "Valor";
            cboTipoCliente.Properties.DisplayMember = "Descricao";
        }

        private void PreenchaCliente(Pessoa cliente, bool exibirMensagemDeNaoEncontrado = false)
        {
            _clienteSelecionado = cliente;

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

        private void PreenchaPrimeiroEUltimoDiaMes()
        {
            var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

            txtDataInicialEmissao.DateTime = primeiroDiaMes;
            txtDataFinalEmissao.DateTime = ultimoDiaMes;
        }

        private void PreenchaCboFormaPagamento()
        {
            List<FormaPagamento> lista = new List<FormaPagamento>();
                        
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

            lista = servicoFormaPagamento.ConsulteListaAtivos();
            
            _listaFormasPagamentos = lista.CloneCompleto();

            cboFormaPagamentoFinanceiro.Properties.DisplayMember = "Descricao";
            cboFormaPagamentoFinanceiro.Properties.ValueMember = "Id";
            cboFormaPagamentoFinanceiro.Properties.DataSource = _listaFormasPagamentos;

            lista.Insert(0, null);

            cboFormaPagamentoFinanceiro.Properties.DisplayMember = "Descricao";
            cboFormaPagamentoFinanceiro.Properties.ValueMember = "Id";
            cboFormaPagamentoFinanceiro.Properties.DataSource = lista;

            if (string.IsNullOrEmpty(cboFormaPagamentoFinanceiro.Text))
            {
                cboFormaPagamentoFinanceiro.EditValue = null;
            }

            if (lista.Count == 2)
            {
                cboFormaPagamentoFinanceiro.EditValue = lista[1].Id;
            }
        }

        private void PreenchalstFormasPagamento()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

            List<FormaPagamento> lista = new List<FormaPagamento>();

            lista = servicoFormaPagamento.ConsulteListaAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(forma =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = forma.Id + " - " + forma.Descricao, Valor = forma.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            foreach (var itens in listaObjetoValor)
            {
                int i = 0;
                lstFormasPagamentos.Items.Add(itens.Descricao);
                i++;
            }
        }

        private void PreenchaCboCaixa()
        {   
            ServicoCaixa servicoCaixa = new ServicoCaixa();
            
            _listaCaixas = servicoCaixa.ConsulteLista(string.Empty,string.Empty,null);

            var lista = _listaCaixas.CloneCompleto();
            lista.Insert(0, null);

            cboCaixa.Properties.DataSource = lista;
            cboCaixa.Properties.DisplayMember = "NomeCaixa";
            cboCaixa.Properties.ValueMember = "Id";
            
        }
        #endregion        
    }
}
