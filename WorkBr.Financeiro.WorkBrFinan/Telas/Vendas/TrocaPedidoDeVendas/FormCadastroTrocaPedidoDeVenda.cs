using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;
using Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Cadastros.Produtos;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Servico.Vendas.TrocaPedidoDeVendaServ;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Cadastros.MotivoTrocaPedidoDeVendaServ;
using Programax.Easy.Negocio.Cadastros.MotivoTrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio;
using DevExpress.XtraReports.UI;
using DevExpress.DXCore.Controls.LookAndFeel;

namespace Programax.Easy.View.Telas.Vendas.TrocaPedidoDeVendas
{
    public partial class FormCadastroTrocaPedidoDeVenda : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private PedidoDeVenda _pedidoDeVenda;
        private List<ItemPedidoTrocaPedidoDeVenda> _listaItensPedido;
        private List<ItemTrocaPedidoDeVenda> _listaItensTroca;
        private List<FormaPagamento> _listaFormasPagamento;

        private Produto _produtoTrocaEmEdicao;
        private ItemTrocaPedidoDeVenda _itemTrocaPedidoDeVendaEmEdicao;
        private ItemPedidoTrocaPedidoDeVenda _itemPedidoTrocaPedidoDeVendaEmEdicao;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroTrocaPedidoDeVenda()
        {
            InitializeComponent();

            _listaItensPedido = new List<ItemPedidoTrocaPedidoDeVenda>();
            _listaItensTroca = new List<ItemTrocaPedidoDeVenda>();

            PreenchaCboStatus();
            PreenchaCboFormaPagamento();
            PreenchaCboMotivosTroca();

            txtDataVencimento.DateTime = DateTime.Now.Date;
            txtDataElaboracaoTroca.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtUsuarioTroca.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.Razao;

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        #region " PESQUISA DE TROCAS "

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                ServicoTrocaPedidoDeVenda servicoTrocaPedidoDeVenda = new ServicoTrocaPedidoDeVenda();

                var troca = servicoTrocaPedidoDeVenda.Consulte(txtId.Text.ToInt());

                EditeTroca(troca);
            }
        }

        private void btnPesquisaTroca_Click(object sender, EventArgs e)
        {
            FormPesquisaTrocaPedidoDeVenda formPesquisaTrocaPedidoDeVenda = new FormPesquisaTrocaPedidoDeVenda();
            var troca = formPesquisaTrocaPedidoDeVenda.ExibaPesquisaDeTrocaPedidosDeVenda();

            if (troca != null)
            {
                EditeTroca(troca);
            }
        }

        #endregion

        #region " PEDIDO DE VENDA "

        private void txtPedidoId_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPedidoId.Text))
            {
                if (_pedidoDeVenda == null || _pedidoDeVenda.Id != txtPedidoId.Text.ToInt())
                {
                    ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                    var pedidoDeVenda = servicoPedidoDeVenda.ConsultePedidoFaturadoOuEmitidoNfe(txtPedidoId.Text.ToInt());

                    PreenchaPedidoDeVenda(pedidoDeVenda, exibirMensagemDeNaoEncontrado: true);
                }
            }
            else
            {
                PreenchaPedidoDeVenda(null);
            }
        }

        private void btnPesquisaPedido_Click(object sender, EventArgs e)
        {
            FormPesquisaPedidoDeVenda formPesquisaPedidoDeVenda = new FormPesquisaPedidoDeVenda();
            var pedido = formPesquisaPedidoDeVenda.ExibaPesquisaDePedidosDeVendaFaturadosEEmitdosNfe();

            if (pedido != null)
            {
                PreenchaPedidoDeVenda(pedido, exibirMensagemDeNaoEncontrado: true);
            }
        }

        #endregion

        #region " ITENS PEDIDO "

        private void gcItensPedidoVenda_DoubleClick(object sender, EventArgs e)
        {
            EditeCamposItemPedido();
        }

        private void gcItensPedidoVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditeCamposItemPedido();
            }
        }

        private void btnCancelarItemPedidoDeVenda_Click(object sender, EventArgs e)
        {
            LimpeCamposItemPedido();
        }

        private void btnAtualizarItemPedidoDeVenda_Click(object sender, EventArgs e)
        {
            AltereQuantidadeTroca();
        }

        #endregion

        #region " ITENS TROCA "

        private void txtCodigoBarrasItemTroca_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoBarrasItemTroca.Text))
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                var produto = servicoProduto.ConsulteProdutoAtivoPeloCodigoDeBarras(txtCodigoBarrasItemTroca.Text);

                PreenchaProduto(produto, exibirMensagemDeNaoEncontrado: true);
            }
            else
            {
                PreenchaProduto(null);
            }
        }

        private void btnPesquisaItemTroca_Click(object sender, EventArgs e)
        {
            FormPesquisaProduto formPesquisaProduto = new FormPesquisaProduto();
            var produto = formPesquisaProduto.ExibaPesquisaDeProdutoAtivo();

            if (produto != null)
            {
                PreenchaProduto(produto);
            }
        }

        private void txtIdItemTroca_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdItemTroca.Text))
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                var produto = servicoProduto.ConsulteProdutoAtivo(txtIdItemTroca.Text.ToInt());

                PreenchaProduto(produto, exibirMensagemDeNaoEncontrado: true);
            }
            else
            {
                PreenchaProduto(null);
            }
        }

        private void btnCancelarItemTroca_Click(object sender, EventArgs e)
        {
            LimpeCamposItemTroca();
        }

        private void btnInserirAtualizarItemTroca_Click(object sender, EventArgs e)
        {
            InsiraOuAtualizeItemTroca();
        }

        private void txtQuantidadeItemTroca_EditValueChanged(object sender, EventArgs e)
        {
            CalculeValorTotalItem();
        }

        private void txtValorUnitarioItemTroca_EditValueChanged(object sender, EventArgs e)
        {
            CalculeValorTotalItem();
        }

        private void txtDescontoItemTroca_EditValueChanged(object sender, EventArgs e)
        {
            CalculeValorTotalItem();
        }

        private void rdbDescontoEhPercentualItemTroca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDescontoItemTroca.Focus();
            }
        }

        private void gcItensTroca_DoubleClick(object sender, EventArgs e)
        {
            EditeItemTroca();
        }

        private void gcItensTroca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditeItemTroca();
            }
        }

        private void btnExcluirItemTroca_Click(object sender, EventArgs e)
        {
            if (_listaItensTroca.Count > 0)
            {
                var itemASerExcluido = _listaItensTroca.FirstOrDefault(item => item.Id == colunaItemTrocaId.View.GetFocusedRowCellValue(colunaItemPedidoId).ToInt());

                var mensagemConfirmacaoExclusao = "Deseja excluir o item " + itemASerExcluido.Produto.Id + " - " + itemASerExcluido.Produto.DadosGerais.Descricao + " ?";

                if (MessageBox.Show(mensagemConfirmacaoExclusao, "Deseja excluir este item ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    _listaItensTroca.Remove(itemASerExcluido);

                    PreenchaGridItensTroca();
                }
            }
        }

        #endregion

        #region " BARRA DE BOTÕES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Action actionSalvar = () =>
            {
                var troca = RetorneTrocaPedidoDeVendaEmEdicao();

                ServicoTrocaPedidoDeVenda servicoTrocaPedidoDeVenda = new ServicoTrocaPedidoDeVenda();

                if (troca.Id == 0)
                {
                    servicoTrocaPedidoDeVenda.Cadastre(troca);
                }
                else
                {
                    servicoTrocaPedidoDeVenda.Atualize(troca);
                }

                MessageBox.Show("Nr. da Troca " + troca.Id + ".");

                LimpeTela();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, mensagemDeSucesso: "A troca foi salva com sucesso.");
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeTela();
        }

        private void btnCancelarTroca_Click(object sender, EventArgs e)
        {
            Action actionSalvar = () =>
            {
                ServicoTrocaPedidoDeVenda servicoTrocaPedidoDeVenda = new ServicoTrocaPedidoDeVenda();

                servicoTrocaPedidoDeVenda.CanceleTrocaPedidoDeVenda(txtId.Text.ToInt());

                LimpeTela();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, mensagemDeSucesso: "A troca foi cancelada com sucesso.");
        }

        private void btnFecharTroca_Click(object sender, EventArgs e)
        {
            Action actionSalvar = () =>
            {
                var troca = RetorneTrocaPedidoDeVendaEmEdicao();
                bool precisaLiberacao = troca.ListaItens.ToList().Exists(item => item.ItemEstahInconsistente);

                if (precisaLiberacao)
                {
                    if (MessageBox.Show("A troca precisa passar pela liberação, Deseja envia-la?", "Enviar troca para liberação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                
                ServicoTrocaPedidoDeVenda servicoTrocaPedidoDeVenda = new ServicoTrocaPedidoDeVenda();

                servicoTrocaPedidoDeVenda.FecheTroca(troca);

                MessageBox.Show("Nr. da Troca " + troca.Id + ".");

                if (precisaLiberacao)
                {
                    MessageBox.Show("A troca foi enviada para liberação.");
                }

                LimpeTela();

                MessageBox.Show("A troca foi salva com sucesso", "Troca Salva");

                ImprimaTrocaPedidoDeVenda(troca.Id);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, exibirMensagemDeSucesso: false);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimaTrocaPedidoDeVenda(txtId.Text.ToInt());
        }

        #endregion

        #region " FECHAMENTO "

        private void cboFormaPagamentoFinanceiro_EditValueChanged(object sender, EventArgs e)
        {
            var formaPagamento = _listaFormasPagamento.FirstOrDefault(forma => forma.Id == cboFormaPagamentoFinanceiro.EditValue.ToInt());

            if (formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.BOLETOBANCARIO ||
                formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDIARIOPROPRIO ||
                formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDITOINTERNO ||
                formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DEPOSITOBANCARIO ||
                formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DUPLICATA)
            {
                if (txtValorFinanceiro.Text.ToDouble() > 0)
                {
                    rdbGeraContasReceber.Checked = true;
                }
                else
                {
                    rdbGeraContasPagar.Checked = true;
                }

                txtNumeroDocumentoFinanceiro.Text = "TROCA PED VENDA " + _pedidoDeVenda.Id + " - " + DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                rdbGeraMovimentacaoCaixa.Checked = true;
            }
        }

        #endregion

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " EDITAR TROCA "

        public void EditeTroca(TrocaPedidoDeVenda trocaPedidoDeVenda)
        {
            btnSalvar.Visible = false;
            btnCancelarTroca.Visible = false;
            btnFecharTroca.Visible = false;
            btnImprimir.Visible = true;

            if (trocaPedidoDeVenda != null)
            {
                txtId.Text = trocaPedidoDeVenda.Id.ToString();

                PreenchaPedidoDeVenda(trocaPedidoDeVenda.PedidoDeVenda);

                txtUsuarioTroca.Text = trocaPedidoDeVenda.UsuarioRealizouTroca != null ? trocaPedidoDeVenda.UsuarioRealizouTroca.Id + " - " + trocaPedidoDeVenda.UsuarioRealizouTroca.DadosGerais.Razao : string.Empty;

                _listaItensPedido = trocaPedidoDeVenda.ListaItensPedido.ToList();
                _listaItensTroca = trocaPedidoDeVenda.ListaItens.ToList();

                PreenchaGridItensPedido();
                PreenchaGridItensTroca();

                cboStatusTroca.EditValue = trocaPedidoDeVenda.Status;
                grpTipoMovimentacaoFinanceira.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Tag.ToInt() == (int)trocaPedidoDeVenda.TipoMovimentacaoFinanceira).Checked = true;

                cboMotivoTroca.EditValue = trocaPedidoDeVenda.MotivoTroca != null ? (int?)trocaPedidoDeVenda.MotivoTroca.Id : null;

                cboFormaPagamentoFinanceiro.EditValue = trocaPedidoDeVenda.FormaPagamento != null ? trocaPedidoDeVenda.FormaPagamento.Id : 1;
                txtNumeroDocumentoFinanceiro.Text = trocaPedidoDeVenda.NumeroDocumento;
                txtDataVencimento.DateTime = trocaPedidoDeVenda.DataVencimento;
                txtValorFinanceiro.Text = trocaPedidoDeVenda.ValorTotalTroca.ToString("#,##0.00");
                txtDataElaboracaoTroca.Text = trocaPedidoDeVenda.DataElaboracao.ToString("dd/MM/yyyy");

                txtId.Properties.ReadOnly = true;

                if (trocaPedidoDeVenda.Status == EnumStatusTrocaPedidoDeVenda.ABERTO)
                {
                    btnFecharTroca.Visible = true;
                    btnSalvar.Visible = true;
                }

                if (trocaPedidoDeVenda.Status == EnumStatusTrocaPedidoDeVenda.ABERTO ||
                    trocaPedidoDeVenda.Status == EnumStatusTrocaPedidoDeVenda.EMLIBERACAO ||
                    trocaPedidoDeVenda.Status == EnumStatusTrocaPedidoDeVenda.RESERVADO)
                {
                    btnCancelarTroca.Visible = true;
                    btnSalvar.Visible = true;
                }
            }
            else
            {
                txtId.Text = string.Empty;

                PreenchaPedidoDeVenda(null);

                txtUsuarioTroca.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.Razao;

                _listaItensPedido.Clear();
                _listaItensTroca.Clear();

                PreenchaGridItensPedido();
                PreenchaGridItensTroca();

                cboMotivoTroca.EditValue = null;

                cboFormaPagamentoFinanceiro.EditValue = 1;
                txtNumeroDocumentoFinanceiro.Text = string.Empty;
                txtDataVencimento.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtValorFinanceiro.Text = string.Empty;
                txtDataElaboracaoTroca.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txtId.Properties.ReadOnly = false;

                btnSalvar.Visible = true;
                btnFecharTroca.Visible = true;

                txtId.Focus();
            }
        }

        private void LimpeTela()
        {
            EditeTroca(null);
        }

        #endregion

        #region " PREENCHIMENTO DE CAMPOS "

        private void PreenchaPedidoDeVenda(PedidoDeVenda pedidoDeVenda, bool exibirMensagemDeNaoEncontrado = false)
        {
            _pedidoDeVenda = pedidoDeVenda;

            if (pedidoDeVenda != null)
            {
                pedidoDeVenda.Cliente.CarregueLazyLoad();
                pedidoDeVenda.Usuario.CarregueLazyLoad();
                pedidoDeVenda.Vendedor.CarregueLazyLoad();
                pedidoDeVenda.Indicador.CarregueLazyLoad();
                pedidoDeVenda.Atendente.CarregueLazyLoad();
                pedidoDeVenda.Supervisor.CarregueLazyLoad();

                pedidoDeVenda.Transportadora.CarregueLazyLoad();

                pedidoDeVenda.ListaItens.CarregueLazyLoad();
                pedidoDeVenda.ListaParcelasPedidoDeVenda.CarregueLazyLoad();

                foreach (var item in pedidoDeVenda.ListaItens)
                {
                    item.Produto.CarregueLazyLoad();
                }

                txtPedidoId.Text = pedidoDeVenda.Id.ToString();
                txtClienteId.Text = pedidoDeVenda.Cliente.Id.ToString();
                txtNomeCliente.Text = pedidoDeVenda.Cliente.DadosGerais.NomeFantasia;
                txtClienteCpfCnpj.Text = pedidoDeVenda.Cliente.DadosGerais.CpfCnpj;
                txtClienteRazaoSocial.Text = pedidoDeVenda.Cliente.DadosGerais.Razao;

                txtDataElaboracao.Text = pedidoDeVenda.DataElaboracao.ToString("dd/MM/yyyy");
                txtUsuarioPedido.Text = pedidoDeVenda.Usuario.Id + " - " + pedidoDeVenda.Usuario.DadosGerais.Razao;

                txtTabelaPrecoPedido.Text = pedidoDeVenda.TabelaPreco.Id + " - " + pedidoDeVenda.TabelaPreco.NomeTabela;
                txtNomeVendedorPedido.Text = pedidoDeVenda.Vendedor != null ? pedidoDeVenda.Vendedor.Id + " - " + pedidoDeVenda.Vendedor.DadosGerais.Razao : string.Empty;

                ConvertaItensPedidoParaItensTrocaPedido();
                PreenchaGridItensPedido();
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Pedido de Venda nao encontrado!", "Pedido de Venda não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtPedidoId.Focus();
                }

                txtPedidoId.Text = string.Empty;
                txtClienteId.Text = string.Empty;
                txtNomeCliente.Text = string.Empty;
                txtClienteCpfCnpj.Text = string.Empty;
                txtClienteRazaoSocial.Text = string.Empty;

                txtDataElaboracao.Text = string.Empty;
                txtUsuarioPedido.Text = string.Empty;

                txtTabelaPrecoPedido.Text = string.Empty;
                txtNomeVendedorPedido.Text = string.Empty;

                _listaItensPedido.Clear();
                PreenchaGridItensPedido();
            }
        }

        private void PreenchaCboStatus()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusTrocaPedidoDeVenda>();

            cboStatusTroca.Properties.DataSource = lista;
            cboStatusTroca.Properties.DisplayMember = "Descricao";
            cboStatusTroca.Properties.ValueMember = "Valor";

            cboStatusTroca.EditValue = EnumStatusTrocaPedidoDeVenda.ABERTO;
        }

        private void PreenchaCboFormaPagamento()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
            _listaFormasPagamento = servicoFormaPagamento.ConsulteListaFormasDePagamentoAtivas();

            cboFormaPagamentoFinanceiro.Properties.DataSource = _listaFormasPagamento;
            cboFormaPagamentoFinanceiro.Properties.DisplayMember = "Descricao";
            cboFormaPagamentoFinanceiro.Properties.ValueMember = "Id";

            cboFormaPagamentoFinanceiro.EditValue = 1;
        }

        private void PreenchaCboMotivosTroca()
        {
            ServicoMotivoTrocaPedidoDeVenda servicoMotivoTrocaPedidoDeVenda = new ServicoMotivoTrocaPedidoDeVenda();
            var lista = servicoMotivoTrocaPedidoDeVenda.ConsulteListaAtiva();

            lista.Insert(0, null);

            cboMotivoTroca.Properties.DataSource = lista;
            cboMotivoTroca.Properties.DisplayMember = "Descricao";
            cboMotivoTroca.Properties.ValueMember = "Id";
        }

        #endregion

        #region " ITENS DO PEDIDO "

        private void ConvertaItensPedidoParaItensTrocaPedido()
        {
            _listaItensPedido.Clear();

            foreach (var item in _pedidoDeVenda.ListaItens)
            {
                ItemPedidoTrocaPedidoDeVenda itemPedidoTrocaPedidoDeVenda = new ItemPedidoTrocaPedidoDeVenda();
                itemPedidoTrocaPedidoDeVenda.Desconto = Math.Round(item.Quantidade * item.ValorUnitario, 2) - item.ValorTotal;
                itemPedidoTrocaPedidoDeVenda.Id = item.Id;
                itemPedidoTrocaPedidoDeVenda.Produto = item.Produto;
                itemPedidoTrocaPedidoDeVenda.Quantidade = item.Quantidade;
                itemPedidoTrocaPedidoDeVenda.QuantidadeTrocar = 0;
                itemPedidoTrocaPedidoDeVenda.ValorTotal = item.ValorTotal;
                itemPedidoTrocaPedidoDeVenda.ValorUnitario = item.ValorUnitario;

                _listaItensPedido.Add(itemPedidoTrocaPedidoDeVenda);
            }
        }

        private void PreenchaGridItensPedido()
        {
            List<ItemPedidoGridTroca> listaItensPedidoGridTroca = new List<ItemPedidoGridTroca>();

            foreach (var item in _listaItensPedido)
            {
                ItemPedidoGridTroca itemPedidoGridTroca = new ItemPedidoGridTroca();

                itemPedidoGridTroca.Desconto = item.Desconto.ToString("#0.00");
                itemPedidoGridTroca.Descricao = item.Produto.DadosGerais.Descricao;
                itemPedidoGridTroca.Id = item.Id;
                itemPedidoGridTroca.IdProduto = item.Produto.Id;
                itemPedidoGridTroca.MarcaFabricante = item.Produto.Principal.Fabricante != null ? item.Produto.Principal.Fabricante.Descricao : string.Empty;
                itemPedidoGridTroca.Quantidade = item.Quantidade;
                itemPedidoGridTroca.QuantidadeTroca = item.QuantidadeTrocar;
                itemPedidoGridTroca.Tamanho = item.Produto.Vestuario != null && item.Produto.Vestuario.Tamanho != null ? item.Produto.Vestuario.Tamanho.Descricao : string.Empty;
                itemPedidoGridTroca.Unidade = item.Produto.DadosGerais.Unidade != null ? item.Produto.DadosGerais.Unidade.Descricao : string.Empty;
                itemPedidoGridTroca.ValorTotal = item.ValorTotal.ToString("#0.00");
                itemPedidoGridTroca.ValorUnitario = item.ValorUnitario.ToString("#0.00");

                listaItensPedidoGridTroca.Add(itemPedidoGridTroca);
            }

            gcItensPedidoVenda.DataSource = listaItensPedidoGridTroca;
            gcItensPedidoVenda.RefreshDataSource();

            PreenchaGridFechamento();
        }

        private void EditeItemPedido()
        {
            if (_listaItensPedido != null && _listaItensPedido.Count > 0)
            {
                var itemPedido = _listaItensPedido.FirstOrDefault(item => item.Id == colunaItemPedidoId.View.GetFocusedRowCellValue(colunaItemPedidoId).ToInt());

                PreenchaCamposItemPedido(itemPedido);
            }
        }

        private void PreenchaCamposItemPedido(ItemPedidoTrocaPedidoDeVenda itemPedidoTrocaPedidoVenda)
        {
            _itemPedidoTrocaPedidoDeVendaEmEdicao = itemPedidoTrocaPedidoVenda;

            if (itemPedidoTrocaPedidoVenda != null)
            {
                txtCodigoDeBarrasProduto.Text = itemPedidoTrocaPedidoVenda.Produto.DadosGerais.CodigoDeBarras;
                txtIdProduto.Text = itemPedidoTrocaPedidoVenda.Produto.Id.ToString();
                txtQuantidadeProduto.Text = itemPedidoTrocaPedidoVenda.Quantidade.ToString();
                txtQuantidadeTroca.Text = itemPedidoTrocaPedidoVenda.QuantidadeTrocar.ToString();
                txtValorTotalProduto.Text = itemPedidoTrocaPedidoVenda.ValorTotal.ToString("0.00");

                txtDescricaoProduto.Text = itemPedidoTrocaPedidoVenda.Produto.DadosGerais.Descricao;

                AltereMascaraQuantidadeItemPedido();

                txtQuantidadeTroca.Focus();
            }
            else
            {
                txtCodigoDeBarrasProduto.Text = string.Empty;
                txtIdProduto.Text = string.Empty;
                txtQuantidadeProduto.Text = string.Empty;
                txtQuantidadeTroca.Text = string.Empty;
                txtValorTotalProduto.Text = string.Empty;
                txtDescricaoProduto.Text = string.Empty;
            }
        }

        private void AltereMascaraQuantidadeItemPedido()
        {
            if (_itemPedidoTrocaPedidoDeVendaEmEdicao.Produto.DadosGerais.PermiteVendaFracionada)
            {
                txtQuantidadeTroca.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,4})?";
            }
            else
            {
                txtQuantidadeTroca.Properties.Mask.EditMask = @"[0-9]{1,11}";
            }
        }

        private void EditeCamposItemPedido()
        {
            if (_listaItensPedido != null && _listaItensPedido.Count > 0)
            {
                var itemPedido = _listaItensPedido.FirstOrDefault(item => item.Id == colunaItemPedidoId.View.GetFocusedRowCellValue(colunaItemPedidoId).ToInt());

                PreenchaCamposItemPedido(itemPedido);
            }
        }

        private void LimpeCamposItemPedido()
        {
            PreenchaCamposItemPedido(null);
        }

        private void AltereQuantidadeTroca()
        {
            Action actionAlterarProduto = () =>
            {
                if (_itemPedidoTrocaPedidoDeVendaEmEdicao != null)
                {
                    if (_itemPedidoTrocaPedidoDeVendaEmEdicao.Quantidade >= txtQuantidadeTroca.Text.ToDouble())
                    {
                        _itemPedidoTrocaPedidoDeVendaEmEdicao.QuantidadeTrocar = txtQuantidadeTroca.Text.ToDouble();
                    }
                    else
                    {
                        MessageBox.Show("Quantidade de troca não pode ser maior que a quantidade do produto.");

                        return;
                    }

                    var troca = RetorneTrocaPedidoDeVendaEmEdicao();

                    ServicoTrocaPedidoDeVenda servicoTrocaPedidoDeVenda = new ServicoTrocaPedidoDeVenda();
                    servicoTrocaPedidoDeVenda.ValideItemPedidoTroca(_itemPedidoTrocaPedidoDeVendaEmEdicao, troca);
                }

                LimpeCamposItemPedido();
                PreenchaGridItensPedido();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionAlterarProduto, exibirMensagemDeSucesso: false);
        }

        #endregion

        #region " ITENS DA TROCA "

        private void PreenchaProduto(Produto produto, bool exibirMensagemDeNaoEncontrado = false)
        {
            _produtoTrocaEmEdicao = produto;

            if (produto != null)
            {
                if (produto.Principal != null)
                {
                    produto.Principal.Marca.CarregueLazyLoad();
                }

                if (produto.Vestuario != null)
                {
                    produto.Vestuario.Tamanho.CarregueLazyLoad();
                    produto.Vestuario.Cor.CarregueLazyLoad();
                }

                produto.DadosGerais.Unidade.CarregueLazyLoad();

                txtIdItemTroca.Text = produto.Id.ToString();
                txtCodigoBarrasItemTroca.Text = produto.DadosGerais.CodigoDeBarras;
                txtDescricaoItemTroca.Text = produto.DadosGerais.Descricao;

                var valorUnitario = ServicoPedidoDeVenda.CalculePrecoUnitarioProduto(_pedidoDeVenda.TabelaPreco, produto);

                txtValorUnitarioItemTroca.Text = valorUnitario.ToString("0.00");

                AltereMascaraQuantidadeItemTroca();

                txtValorUnitarioItemTroca.Focus();
            }
            else
            {
                txtIdProduto.Text = string.Empty;
                txtCodigoDeBarrasProduto.Text = string.Empty;
                txtDescricaoProduto.Text = string.Empty;
                txtValorUnitarioItemTroca.Text = string.Empty;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Produto nao encontrado!", "Produto não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigoDeBarrasProduto.Focus();
                }
            }
        }

        private void AltereMascaraQuantidadeItemTroca()
        {
            if (_produtoTrocaEmEdicao.DadosGerais.PermiteVendaFracionada)
            {
                txtQuantidadeItemTroca.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,4})?";
            }
            else
            {
                txtQuantidadeItemTroca.Properties.Mask.EditMask = @"[0-9]{1,11}";
            }
        }

        private void InsiraOuAtualizeItemTroca()
        {
            if (VerificaitensEstoqueNegativoTrocaPedido()) return;

            Action actionInserirItem = () =>
            {
                ItemTrocaPedidoDeVenda itemTrocaPedidoVenda = RetorneItemTrocaPedidoDeVendaEmEdicao();
                TrocaPedidoDeVenda trocaPedidoDeVenda = RetorneTrocaPedidoDeVendaEmEdicao();

                itemTrocaPedidoVenda.TrocaPedidoDeVenda = trocaPedidoDeVenda;

                _listaItensTroca.Remove(_itemTrocaPedidoDeVendaEmEdicao);

                ServicoTrocaPedidoDeVenda servicoTrocaPedidoDeVenda = new ServicoTrocaPedidoDeVenda();

                servicoTrocaPedidoDeVenda.ValideItemTroca(itemTrocaPedidoVenda);

                try
                {
                    servicoTrocaPedidoDeVenda.ValideItemTrocaLiberacao(itemTrocaPedidoVenda);
                    itemTrocaPedidoVenda.ItemEstahInconsistente = false;
                }
                catch
                {
                    itemTrocaPedidoVenda.ItemEstahInconsistente = true;
                }

                if (itemTrocaPedidoVenda.Id == 0)
                {
                    _listaItensTroca.Add(itemTrocaPedidoVenda);
                }
                else
                {
                    int posicaoItem = itemTrocaPedidoVenda.Id - 1;

                    _listaItensTroca.Insert(posicaoItem, itemTrocaPedidoVenda);
                }

                LimpeCamposItemTroca();

                PreenchaGridItensTroca();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionInserirItem, exibirMensagemDeSucesso: false);
        }

        private bool VerificaitensEstoqueNegativoTrocaPedido()
        {
            ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

            ServicoProduto servicoProduto = new ServicoProduto();

            var produto = servicoProduto.Consulte(txtIdItemTroca.Text.ToInt());

            double quantidade = _listaItensTroca.Sum(x => x.Id == txtIdItemTroca.Text.ToInt() ? x.Quantidade : 0) + txtQuantidadeItemTroca.Text.ToInt();

            if (servicoPedido.VerifiqueItemQuantidadeEstoqueNegativo(quantidade, produto, false))
            {
                MessageBox.Show("O estoque do seguinte item: " + produto.Id + " - " + produto.DadosGerais.Descricao + ". Pode estar zerado ou a quantidade requerida não está disponível!", "Verifique o estoque!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            return false;
        }

        private ItemTrocaPedidoDeVenda RetorneItemTrocaPedidoDeVendaEmEdicao()
        {
            _itemTrocaPedidoDeVendaEmEdicao = _itemTrocaPedidoDeVendaEmEdicao ?? new ItemTrocaPedidoDeVenda();

            var item = _itemTrocaPedidoDeVendaEmEdicao.CloneCompleto();

            item.Desconto = txtDescontoItemTroca.Text.ToDouble();
            item.DescontoEhPercentual = rdbDescontoEhPercentualItemTroca.Checked;
            item.Produto = _produtoTrocaEmEdicao;
            item.Quantidade = txtQuantidadeItemTroca.Text.ToDouble();
            item.ValorUnitario = txtValorUnitarioItemTroca.Text.ToDouble();
            item.ValorTotal = txtValorTotalItemTroca.Text.ToDouble();

            return item;
        }

        private void PreenchaGridItensTroca()
        {
            GereIdFalsoParaOsItensTroca();

            List<ItemGridTroca> listaItemGrid = new List<ItemGridTroca>();

            foreach (var item in _listaItensTroca)
            {
                ItemGridTroca itemGridTroca = new ItemGridTroca();

                itemGridTroca.Id = item.Id;
                itemGridTroca.CodigoDeBarras = item.Produto.DadosGerais.CodigoDeBarras;
                itemGridTroca.IdProduto = item.Produto.Id;
                itemGridTroca.Cor = item.Produto.Vestuario != null && item.Produto.Vestuario.Cor != null ? item.Produto.Vestuario.Cor.Descricao : string.Empty;
                itemGridTroca.Descricao = item.Produto.DadosGerais.Descricao;

                if (item.DescontoEhPercentual)
                {
                    itemGridTroca.Desconto = item.Desconto.ToString("0.00") + "%";
                }
                else
                {
                    itemGridTroca.Desconto = "R$" + item.Desconto.ToString("0.00");
                }

                itemGridTroca.MarcaFabricante = item.Produto.Principal != null && item.Produto.Principal.Marca != null ? item.Produto.Principal.Marca.Descricao : string.Empty;
                itemGridTroca.Modelo = item.Produto.Vestuario != null ? item.Produto.Vestuario.Modelo : string.Empty;
                itemGridTroca.Quantidade = item.Quantidade.ToDouble();
                itemGridTroca.Sexo = item.Produto.Vestuario != null && item.Produto.Vestuario.SexoProduto != null ? item.Produto.Vestuario.SexoProduto.Value.Descricao() : string.Empty;
                itemGridTroca.Tamanho = item.Produto.Vestuario != null && item.Produto.Vestuario.Tamanho != null ? item.Produto.Vestuario.Tamanho.Descricao : string.Empty;
                itemGridTroca.Unidade = item.Produto.DadosGerais.Unidade != null ? item.Produto.DadosGerais.Unidade.Descricao : string.Empty;
                itemGridTroca.ValorTotal = item.ValorTotal.ToString("0.00");
                itemGridTroca.ValorUnitario = item.ValorUnitario.ToString("0.00");
                itemGridTroca.ItemEstahInconsistente = item.ItemEstahInconsistente;

                listaItemGrid.Add(itemGridTroca);
            }

            gcItensTroca.DataSource = listaItemGrid;
            gcItensTroca.RefreshDataSource();

            PreenchaGridFechamento();
        }

        private void GereIdFalsoParaOsItensTroca()
        {
            for (int i = 0; i < _listaItensTroca.Count; i++)
            {
                _listaItensTroca[i].Id = i + 1;
            }
        }

        private void CalculeValorTotalItem()
        {
            double valorTotalItem = RetorneValorTotalItem(txtValorUnitarioItemTroca.Text.ToDouble(),
                                                                               txtQuantidadeItemTroca.Text.ToDouble(),
                                                                               rdbDescontoEhPercentualItemTroca.Checked,
                                                                               txtDescontoItemTroca.Text.ToDouble());

            txtValorTotalItemTroca.Text = valorTotalItem.ToString("0.00");
        }

        private double RetorneValorTotalItem(double valorUnitario, double quantidade, bool descontoEhPercentual, double desconto)
        {
            var totalSemDesconto = valorUnitario * quantidade;
            double total = totalSemDesconto;

            if (desconto > 0)
            {
                if (descontoEhPercentual)
                {
                    total = totalSemDesconto - totalSemDesconto * desconto / (double)100;
                }
                else
                {
                    total = totalSemDesconto - desconto;
                }
            }

            return total;
        }

        private void LimpeCamposItemTroca()
        {
            PreenchaCamposItemTroca(null);
        }

        private void EditeItemTroca()
        {
            if (_listaItensTroca != null && _listaItensTroca.Count > 0)
            {
                var itemPedido = _listaItensTroca.FirstOrDefault(item => item.Id == colunaItemTrocaId.View.GetFocusedRowCellValue(colunaItemTrocaId).ToInt());

                PreenchaCamposItemTroca(itemPedido);
            }
        }

        private void PreenchaCamposItemTroca(ItemTrocaPedidoDeVenda itemTrocaPedidoVenda)
        {
            _itemTrocaPedidoDeVendaEmEdicao = itemTrocaPedidoVenda;

            if (itemTrocaPedidoVenda != null)
            {
                _produtoTrocaEmEdicao = itemTrocaPedidoVenda.Produto;

                rdbDescontoEhValorItemTroca.Checked = true;
                rdbDescontoEhPercentualItemTroca.Checked = itemTrocaPedidoVenda.DescontoEhPercentual;

                txtCodigoBarrasItemTroca.Text = itemTrocaPedidoVenda.Produto.DadosGerais.CodigoDeBarras;
                txtDescontoItemTroca.Text = itemTrocaPedidoVenda.Desconto.ToString("0.00");
                txtIdItemTroca.Text = itemTrocaPedidoVenda.Produto.Id.ToString();
                txtQuantidadeItemTroca.Text = itemTrocaPedidoVenda.Quantidade.ToString();
                txtValorUnitarioItemTroca.Text = itemTrocaPedidoVenda.ValorUnitario.ToString("0.00");
                txtValorTotalItemTroca.Text = itemTrocaPedidoVenda.ValorTotal.ToString("0.00");

                txtDescricaoItemTroca.Text = itemTrocaPedidoVenda.Produto.DadosGerais.Descricao;

                AltereMascaraQuantidadeItemTroca();

                txtValorUnitarioItemTroca.Focus();

                btnInserirAtualizarItemTroca.Image = Properties.Resources.icon_atualizar;
            }
            else
            {
                _produtoTrocaEmEdicao = null;

                txtCodigoBarrasItemTroca.Text = string.Empty;
                txtDescontoItemTroca.Text = string.Empty;
                rdbDescontoEhPercentualItemTroca.Checked = true;
                txtIdItemTroca.Text = string.Empty;
                txtQuantidadeItemTroca.Text = string.Empty;
                txtValorUnitarioItemTroca.Text = string.Empty;
                txtValorTotalItemTroca.Text = string.Empty;

                txtDescricaoItemTroca.Text = string.Empty;

                txtCodigoBarrasItemTroca.Focus();

                btnInserirAtualizarItemTroca.Image = Properties.Resources.icones2_19;
            }
        }

        #endregion

        #region " FECHAMENTO "

        private void PreenchaGridFechamento()
        {
            List<ItemGridFechamento> listaGridFechamento = new List<ItemGridFechamento>();

            ItemGridFechamento itemGridFechamentoDevolvidos = new ItemGridFechamento();
            ItemGridFechamento itemGridFechamentoTroca = new ItemGridFechamento();

            double valorTotalDevolvido = 0;
            double valorItensDevolvidosSemDescontos = 0;
            double quantidadeDevolvida = 0;

            foreach (var item in _listaItensPedido)
            {
                if (item.QuantidadeTrocar > 0)
                {
                    quantidadeDevolvida += item.QuantidadeTrocar;
                    double valorTotalSemDesconto = item.QuantidadeTrocar * item.ValorUnitario;

                    valorItensDevolvidosSemDescontos += valorTotalSemDesconto;

                    valorTotalDevolvido += item.QuantidadeTrocar * item.ValorTotal / item.Quantidade;
                }
            }

            itemGridFechamentoDevolvidos.Item = "Itens Devolvidos";
            itemGridFechamentoDevolvidos.ValorTotalSemDesconto = valorItensDevolvidosSemDescontos.ToString("#,##0.00");
            itemGridFechamentoDevolvidos.QuantidadeTotal = quantidadeDevolvida.ToString();
            itemGridFechamentoDevolvidos.ValorTotalGeral = valorItensDevolvidosSemDescontos.ToString("#,##0.00");
            itemGridFechamentoDevolvidos.DescontoTotal = (valorItensDevolvidosSemDescontos - valorTotalDevolvido).ToString("#,##0.00");

            double valorTotalTroca = 0;
            double valorItensTrocaSemDescontos = 0;
            double quantidadeItensTroca = 0;

            foreach (var item in _listaItensTroca)
            {
                quantidadeItensTroca += item.Quantidade;
                valorTotalTroca += item.ValorTotal;
                valorItensTrocaSemDescontos += item.Quantidade * item.ValorUnitario;
            }

            itemGridFechamentoTroca.Item = "Itens da Troca";
            itemGridFechamentoTroca.ValorTotalSemDesconto = valorItensTrocaSemDescontos.ToString("#,##0.00");
            itemGridFechamentoTroca.QuantidadeTotal = quantidadeItensTroca.ToString();
            itemGridFechamentoTroca.ValorTotalGeral = valorTotalTroca.ToString("#,##0.00");
            itemGridFechamentoTroca.DescontoTotal = (valorItensTrocaSemDescontos - valorTotalTroca).ToString("#,##0.00");

            double diferenca = valorTotalTroca - valorTotalDevolvido;

            itemGridFechamentoTroca.Diferenca = diferenca.ToString("#,##0.00");
            itemGridFechamentoDevolvidos.Diferenca = diferenca.ToString("#,##0.00");

            listaGridFechamento.Add(itemGridFechamentoDevolvidos);
            listaGridFechamento.Add(itemGridFechamentoTroca);

            gcFechamento.DataSource = listaGridFechamento;
            gcFechamento.RefreshDataSource();

            txtValorFinanceiro.Text = diferenca.ToString("#,##0.00");
        }

        #endregion

        #region " SALVAR E FECHAR TROCA "

        private TrocaPedidoDeVenda RetorneTrocaPedidoDeVendaEmEdicao()
        {
            TrocaPedidoDeVenda trocaPedidoDeVenda = new TrocaPedidoDeVenda();

            trocaPedidoDeVenda.Id = txtId.Text.ToInt();
            trocaPedidoDeVenda.PedidoDeVenda = _pedidoDeVenda;

            trocaPedidoDeVenda.UsuarioRealizouTroca = Sessao.PessoaLogada;

            trocaPedidoDeVenda.ListaItens = _listaItensTroca;
            trocaPedidoDeVenda.ListaItensPedido = _listaItensPedido;

            trocaPedidoDeVenda.TipoMovimentacaoFinanceira = (EnumTipoMovimentacaoFinanceiraTrocaPedidoDeVenda)grpTipoMovimentacaoFinanceira.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();
            trocaPedidoDeVenda.Status = (EnumStatusTrocaPedidoDeVenda)cboStatusTroca.EditValue;

            trocaPedidoDeVenda.MotivoTroca = cboMotivoTroca.EditValue != null ? new MotivoTrocaPedidoDeVenda { Id = cboMotivoTroca.EditValue.ToInt() } : null;

            trocaPedidoDeVenda.FormaPagamento = _listaFormasPagamento.FirstOrDefault(forma => forma.Id == cboFormaPagamentoFinanceiro.EditValue.ToInt());
            trocaPedidoDeVenda.NumeroDocumento = txtNumeroDocumentoFinanceiro.Text;
            trocaPedidoDeVenda.ValorTotalTroca = txtValorFinanceiro.Text.ToDouble();
            trocaPedidoDeVenda.DataVencimento = txtDataVencimento.Text.ToDate();
            trocaPedidoDeVenda.DataElaboracao = txtDataElaboracaoTroca.Text.ToDate();

            return trocaPedidoDeVenda;
        }

        private void ImprimaTrocaPedidoDeVenda(int idPedidoDeVenda)
        {
            RelatorioTrocaDeVendas relatorio = new RelatorioTrocaDeVendas(idPedidoDeVenda);
                relatorio.GereRelatorio();

                using (ReportPrintTool printTool = new ReportPrintTool(relatorio))
                {
                    // Invoke the Ribbon Print Preview form modally, 
                    // and load the report document into it.
                    printTool.ShowRibbonPreviewDialog();

                    // Invoke the Ribbon Print Preview form
                    // with the specified look and feel setting.
                    //printTool.ShowRibbonPreview(UserLookAndFeel.Default);
                }
        }


        #endregion

        #endregion

        #region " CLASSES AUXILIARES "

        private class ItemGridTroca
        {
            public int Id { get; set; }

            public int IdProduto { get; set; }

            public string CodigoDeBarras { get; set; }

            public string Descricao { get; set; }

            public string Unidade { get; set; }

            public string MarcaFabricante { get; set; }

            public string Tamanho { get; set; }

            public string Cor { get; set; }

            public string Sexo { get; set; }

            public string Modelo { get; set; }

            public double Quantidade { get; set; }

            public string ValorUnitario { get; set; }

            public string Desconto { get; set; }

            public string ValorTotal { get; set; }

            public bool ItemEstahInconsistente { get; set; }
        }

        private class ItemPedidoGridTroca
        {
            public int Id { get; set; }

            public int IdProduto { get; set; }

            public string Descricao { get; set; }

            public string Unidade { get; set; }

            public string MarcaFabricante { get; set; }

            public string Tamanho { get; set; }

            public double Quantidade { get; set; }

            public string ValorUnitario { get; set; }

            public string Desconto { get; set; }

            public string ValorTotal { get; set; }

            public double QuantidadeTroca { get; set; }
        }

        private class ItemGridFechamento
        {
            public string Item { get; set; }

            public string ValorTotalSemDesconto { get; set; }

            public string QuantidadeTotal { get; set; }

            public string DescontoTotal { get; set; }

            public string ValorTotalGeral { get; set; }

            public string Diferenca { get; set; }
        }

        #endregion
           
    }
}
