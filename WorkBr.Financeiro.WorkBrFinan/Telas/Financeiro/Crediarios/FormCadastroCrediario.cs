using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Financeiro.CrediarioObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Servico.Financeiro.CrediarioServ;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.TabelaPrecoServ;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using System.Globalization;

namespace Programax.Easy.View.Telas.Financeiro.Crediarios
{
    public partial class FormCadastroCrediario : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private bool _estahIncluindo;
        private List<FormaPagamento> _listaFormasPagamentos;
        private bool _formularioLimpo;
        private EnumTipoPessoa? _tipoPessoaFisicaOuJuridica;
        private EnumTipoPedidoDeVenda? _tipoPedidoDeVenda;
        private List<FormaPagamento> _listaFormasPagamento;
        private List<ItemPedidoDeVenda> _listaItensPedidosVenda;
        private Pessoa _cliente;
        private bool _periodoFaturamento;
        private DateTime _dataInicialPeriodo;
        private DateTime _dataFinalPeriodo;
        private EnumOrdenacaoPesquisaVwVendas _ordenacao;
        private bool _statusFaturado;
        private bool _statusEmitidoNFe;

        #endregion
       
        #region " CONSTRUTOR "

        public FormCadastroCrediario()
        {
            InitializeComponent();

            _formularioLimpo = true;

            PreenchaCboFormaPagamento();
            PreenchaCboTabelaPreco();

            this.ActiveControl = txtIdCliente;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnAtalhoCliente_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Action actionSalvar = () =>
            {
                var analiseCredito = RetorneAnaliseCreditoEmEdicao();

                ServicoCrediario servicoAnaliseCredito = new ServicoCrediario();

                if (_estahIncluindo)
                {
                    servicoAnaliseCredito.Cadastre(analiseCredito);
                }
                else
                {
                    servicoAnaliseCredito.Atualize(analiseCredito);
                }

                PesquiseEEditeAnaliseCredito(analiseCredito.Id);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void cboFormaPagamento_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCondicaoPagamento();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            EditeAnaliseCredito(null);
        }

        private void txtIdCliente_Leave(object sender, EventArgs e)
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();

            var cliente = servicoPessoa.ConsulteClienteAtivo(txtIdCliente.Text.ToInt());

            if (cliente != null)
            {
                PesquiseEEditeAnaliseCredito(cliente.Id);
            }
            else
            {
                if (!_formularioLimpo || !string.IsNullOrEmpty(txtIdCliente.Text))
                {
                    EditeAnaliseCredito(null);

                    MessageBox.Show("Cliente não encontrado!", "Cliente não encontrado");

                    txtIdCliente.Focus();
                }
            }

        }


        private void btnPesquisaPessoa_Click_1(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var pessoa = formPessoaPesquisa.PesquisePessoaClienteAtiva();

            if (pessoa != null)
            {
                PesquiseEEditeAnaliseCredito(pessoa.Id);
                CarregaItensPedidos(pessoa);
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " PREENCHIMENTO DE CBOS "

        private void PreenchaCboTabelaPreco()
        {
            List<TabelaPreco> listaTabelaPreco = new List<TabelaPreco>();

            ServicoTabelaPreco servioTabelaPreco = new ServicoTabelaPreco();
            listaTabelaPreco = servioTabelaPreco.ConsulteListaTabelaPrecosAtivas();

            listaTabelaPreco.Insert(0, null);

            cboTabelaPreco.Properties.DisplayMember = "NomeTabela";
            cboTabelaPreco.Properties.ValueMember = "Id";
            cboTabelaPreco.Properties.DataSource = listaTabelaPreco;
        }

        private void PreenchaCboFormaPagamento()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

            var lista = servicoFormaPagamento.ConsulteListaAtivos();

            _listaFormasPagamentos = lista.CloneCompleto();

            cboFormaPagamento.Properties.DisplayMember = "Descricao";
            cboFormaPagamento.Properties.ValueMember = "Id";
            cboFormaPagamento.Properties.DataSource = _listaFormasPagamentos;

            lista.Insert(0, null);

            cboFormaPagamento.Properties.DisplayMember = "Descricao";
            cboFormaPagamento.Properties.ValueMember = "Id";
            cboFormaPagamento.Properties.DataSource = lista;
        }

        private void PreenchaCboCondicaoPagamento()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
            var formaPagamento = servicoFormaPagamento.Consulte(cboFormaPagamento.EditValue.ToInt());

            List<CondicaoPagamento> listaCondicoes = new List<CondicaoPagamento>();

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

            cboCondicaoPagamento.Properties.DisplayMember = "Descricao";
            cboCondicaoPagamento.Properties.ValueMember = "Id";
            cboCondicaoPagamento.Properties.DataSource = listaCondicoes;
        }

        #endregion
        private void CarregaItensPedidos(Pessoa _cliente)

        {

            //    var PedidoVenda = new ServicoPedidoDeVenda().Consulte(Cliente);

            //    if (PedidoVenda != null)
            //        PedidoVenda = new ServicoPedidoDeVenda().Consulte(Cliente);

            //    //PreenchaGridBaixas(baixa);

            //    List<PedidoDeVenda> movimentacaoItensGrid = new List<PedidoDeVenda>();

            //    foreach (var baixaitem in PedidoVenda)
            //    {
            //        BaixaItens itemBaixaGrid = new BaixaItens();


            //        itemBaixaGrid.IdProduto = baixaitem.Produto.Id;
            //        itemBaixaGrid.Nome = baixaitem.Produto.DadosGerais.Descricao;
            //        itemBaixaGrid.DataBaixa = baixaitem.MovimentacaoBase.DataMovimentacao.ToString();

            //        movimentacaoItensGrid.Add(itemBaixaGrid);
            //    }

            //    gcBaixas.DataSource = movimentacaoItensGrid;
            //    gcBaixas.RefreshDataSource();
            //}
            _dataFinalPeriodo = DateTime.Now;

             ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
            List<PedidoDeVenda> listaVWVenda = servicoPedidoDeVenda.ConsulteLista(_dataInicialPeriodo,
                                                                                                      _dataFinalPeriodo,null, null,_cliente,null,null,null,false);
           

            List<PedidoDeVenda> movimentacaoItensGrid = new List<PedidoDeVenda>();
            foreach (var venda in listaVWVenda)
                //foreach (var pedidovenda in pedidosdevenda)
            {
                PedidoDeVenda itemPedidoGrid = new PedidoDeVenda();

                CultureInfo CI = CultureInfo.InvariantCulture;

                itemPedidoGrid.Id = venda.Id;
                double Valor;
                Valor   = venda.ValorTotal;
                //item.ValorTotal.ToString("0.00");
                itemPedidoGrid.ObservacoesNotaFiscal = venda.ValorTotal.ToString("0.00");
                itemPedidoGrid.ObservacoesGeraisVenda = venda.FormaPagamento.Descricao.ToString();
                itemPedidoGrid.DataElaboracao = venda.DataElaboracao;
                //itemPedidoGrid.Cliente.Atendimento.CondicaoDePagamento.DataCadastro = venda.

                movimentacaoItensGrid.Add(itemPedidoGrid);
            }

            gcPedidos.DataSource = movimentacaoItensGrid;
            gcPedidos.RefreshDataSource();
        }

        private Crediario RetorneAnaliseCreditoEmEdicao()
        {
            Crediario analiseCredito = new Crediario();
            analiseCredito.Id = txtIdCliente.Text.ToInt();
            analiseCredito.Pessoa = new Pessoa { Id = txtIdCliente.Text.ToInt() };

            analiseCredito.CondicaoPagamento = cboCondicaoPagamento.EditValue != null ? new CondicaoPagamento { Id = cboCondicaoPagamento.EditValue.ToInt() } : null;
            analiseCredito.FormaPagamento = cboFormaPagamento.EditValue != null ? new FormaPagamento { Id = cboFormaPagamento.EditValue.ToInt() } : null;
            analiseCredito.TabelaPreco = cboTabelaPreco.EditValue != null ? new TabelaPreco { Id = cboTabelaPreco.EditValue.ToInt() } : null;

            analiseCredito.PodeAlterarMultaEJuros = rdbAlterarMultaEJuros.Checked;
            analiseCredito.StatusAnaliseCredito = rdbLiberado.Checked ? EnumStatusCrediario.LIBERADO :
                                                                    rdbLiberacaoGerente.Checked ? EnumStatusCrediario.LIBERACAOGERENTE :
                                                                                                                     EnumStatusCrediario.BLOQUEADO;

            analiseCredito.ValorLimiteCredito = txtValorLimiteCredito.Text.ToDouble();

            analiseCredito.DataValidade = txtDataValidade.Text.ToDate();
            return analiseCredito;
        }

        private void EditeAnaliseCredito(Crediario analiseCredito)
        {
            _estahIncluindo = true;
            _formularioLimpo = false;

            if (analiseCredito != null)
            {
                _estahIncluindo = false;

                txtDataUltimaAlteracao.Text = analiseCredito.DataUltimaAlteracao.ToString("dd/MM/yyyy");
                txtUltimoUsuarioAlteracao.Text = analiseCredito.UsuarioUltimaAlteracao.Id + " - " + analiseCredito.UsuarioUltimaAlteracao.DadosGerais.Razao;

                txtIdCliente.Text = analiseCredito.Id.ToString();
                txtCpfCnpj.Text = analiseCredito.Pessoa.DadosGerais.CpfCnpj;
                txtStatusCliente.Text = analiseCredito.Pessoa.DadosGerais.Status == "A" ? "ATIVO" : "INATIVO";
                txtNomeCliente.Text = analiseCredito.Pessoa.DadosGerais.NomeFantasia;

                cboFormaPagamento.EditValue = analiseCredito.FormaPagamento != null ? (int?)analiseCredito.FormaPagamento.Id : null;
                cboCondicaoPagamento.EditValue = analiseCredito.CondicaoPagamento != null ? (int?)analiseCredito.CondicaoPagamento.Id : null;
                cboTabelaPreco.EditValue = analiseCredito.TabelaPreco != null ? (int?)analiseCredito.TabelaPreco.Id : null;

                rdbBloqueado.Checked = true;
                rdbLiberacaoGerente.Checked = analiseCredito.StatusAnaliseCredito == EnumStatusCrediario.LIBERACAOGERENTE;
                rdbLiberado.Checked = analiseCredito.StatusAnaliseCredito == EnumStatusCrediario.LIBERADO;

                txtValorLimiteCredito.Text = analiseCredito.ValorLimiteCredito.ToString("0.00");

                rdbNaoAlterarMultaEJuros.Checked = true;
                rdbAlterarMultaEJuros.Checked = analiseCredito.PodeAlterarMultaEJuros;
                txtDataValidade.Text = analiseCredito.DataValidade.ToString("dd/MM/yyyy");
            }
            else
            {
                _formularioLimpo = true;

                txtIdCliente.Text = string.Empty;
                txtCpfCnpj.Text = string.Empty;
                txtStatusCliente.Text = string.Empty;
                txtNomeCliente.Text = string.Empty;

                cboFormaPagamento.EditValue = null;
                cboCondicaoPagamento.EditValue = null;
                cboTabelaPreco.EditValue = null;

                rdbLiberacaoGerente.Checked = true;

                txtValorLimiteCredito.Text = string.Empty;

                rdbAlterarMultaEJuros.Checked = true;

                txtDataUltimaAlteracao.Text = string.Empty;
                txtUltimoUsuarioAlteracao.Text = string.Empty;
                txtDataValidade.Text = string.Empty;

                txtIdCliente.Focus();
            }
        }

        private void PesquiseEEditeAnaliseCredito(int id)
        {
            ServicoCrediario servicoAnaliseCredito = new ServicoCrediario();
            var analiseCredito = servicoAnaliseCredito.Consulte(id);

            EditeAnaliseCredito(analiseCredito);
        }

        #endregion

        private void rdbRevenda_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnAtalhoSupervisor_Click(object sender, EventArgs e)
        {

        }

        private void btnAtalhoAtendente_Click(object sender, EventArgs e)
        {

        }

        private void btnAtalhoVendedor_Click(object sender, EventArgs e)
        {

        }

        private void btnAtalhoIndicador_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisaEndereco_Click(object sender, EventArgs e)
        {

        }
    }
}
