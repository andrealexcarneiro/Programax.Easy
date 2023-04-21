using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Negocio;

namespace Programax.Easy.View.Telas.Vendas.PedidosDeVendas
{
    public partial class FormPesquisaPedidoDeVenda : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<PedidoDeVenda> _listaPedidosDeVenda;
        private PedidoDeVenda _pedidoDeVendaSelecionado;
        private bool _somenteImpressao;
        private Parametros _parametros;
        private double TotaisVendas;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaPedidoDeVenda(bool somenteImpressao = false)
        {
            InitializeComponent();

            _listaPedidosDeVenda = new List<PedidoDeVenda>();

            PreenchaCboAtendentes();
            PreenchaCboVendedores();
            PreenchaCboTipoDocumento();
            PreenchaCboSituacao();

            _somenteImpressao = somenteImpressao;

            _parametros = new ServicoParametros().ConsulteParametros(); 

            if (somenteImpressao)
            {
                this.NomeDaTela = "Impressão de Pedido De Venda";
                btnSelecionar.Visible = false;
            }

            txtDataInicial.DateTime = DateTime.Now.Date;
            txtDataFinal.DateTime = DateTime.Now.Date;

            this.ActiveControl = txtDataInicial;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
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
            if (_listaPedidosDeVenda.Count > 0)
            {
                btnImprimir.Visible = true;

                if (_parametros.ParametrosVenda.LimiteDiarioManha > 0)
                {
                    tblImpressao.Visible = true;
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void gcPedidosDeVenda_Click(object sender, EventArgs e)
        {
            if (_listaPedidosDeVenda.Count > 0)
            {
                btnImprimir.Visible = true;

                if (_parametros.ParametrosVenda.LimiteDiarioManha > 0)
                {
                    tblImpressao.Visible = true;
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            int pedidoId = colunaId.View.GetFocusedRowCellValue(colunaId).ToInt();

            if (_parametros.ParametrosVenda.PedidoEmDuasVias)
            {
                RelatorioPedidoVendaDuasVias relatorioDuasVias = new RelatorioPedidoVendaDuasVias(pedidoId);
                relatorioDuasVias.GereRelatorio();

                using (ReportPrintTool printTool = new ReportPrintTool(relatorioDuasVias))
                {
                    // Invoke the Ribbon Print Preview form modally, 
                    // and load the report document into it.
                    printTool.ShowRibbonPreviewDialog();

                    // Invoke the Ribbon Print Preview form
                    // with the specified look and feel setting.
                    printTool.ShowRibbonPreview(UserLookAndFeel.Default);
                }
            }
            else if (_parametros.ParametrosVenda.PedidoEmImpressoraTermica)
            {
                RelatorioPedidoVendaReduzido relatorioReduzido = new RelatorioPedidoVendaReduzido(pedidoId);
                relatorioReduzido.GereRelatorio();

                using (ReportPrintTool printTool = new ReportPrintTool(relatorioReduzido))
                {
                    // Invoke the Ribbon Print Preview form modally, 
                    // and load the report document into it.
                    printTool.ShowRibbonPreviewDialog();

                    // Invoke the Ribbon Print Preview form
                    // with the specified look and feel setting.
                    printTool.ShowRibbonPreview(UserLookAndFeel.Default);
                }
            }
            else
            {
                if (!rdbUmaVia.Checked && !rdbDuasVias.Checked)
                {
                    RelatorioPedidoVenda relatorio = new RelatorioPedidoVenda(pedidoId, Negocio.Cadastros.Enumeradores.EnumTipoEndereco.PRINCIPAL);
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
                else if (rdbUmaVia.Checked)
                {
                    RelatorioPedidoVendaAgenda relatorio = new RelatorioPedidoVendaAgenda(pedidoId);
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
                else
                {
                    RelatorioPedidoVendaDuasViasAgenda relatorioDuasVias = new RelatorioPedidoVendaDuasViasAgenda(pedidoId);
                    relatorioDuasVias.GereRelatorio();

                    using (ReportPrintTool printTool = new ReportPrintTool(relatorioDuasVias))
                    {
                        // Invoke the Ribbon Print Preview form modally, 
                        // and load the report document into it.
                        printTool.ShowRibbonPreviewDialog();

                        // Invoke the Ribbon Print Preview form
                        // with the specified look and feel setting.
                        printTool.ShowRibbonPreview(UserLookAndFeel.Default);
                    }
                }

                rdbDuasVias.Checked = false;
                rdbUmaVia.Checked = false;
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

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

            cboSituacao.Properties.DataSource = lista;
            cboSituacao.EditValue = EnumStatusPedidoDeVenda.FATURADO;

            this.ShowDialog();

            return _pedidoDeVendaSelecionado;
        }

        private void Pesquise()
        {
            this.Cursor = Cursors.WaitCursor;
            DateTime? dataInicial = txtDataInicial.Text.ToDateNullabel();
            DateTime? dataFinal = txtDataFinal.Text.ToDateNullabel();

            Pessoa atendente = cboAtendentes.EditValue != null ? new Pessoa { Id = cboAtendentes.EditValue.ToInt() } : null;
            Pessoa vendedor = cboVendedores.EditValue != null ? new Pessoa { Id = cboVendedores.EditValue.ToInt() } : null;
            Pessoa cliente = !string.IsNullOrEmpty(txtIdCliente.Text) ? new Pessoa { Id = txtIdCliente.Text.ToInt() } : null;

            EnumTipoPedidoDeVenda? tipoPedidoDeVenda = (EnumTipoPedidoDeVenda?)cboTipoDocumento.EditValue;
            EnumStatusPedidoDeVenda? statusPedidoDeVenda = (EnumStatusPedidoDeVenda?)cboSituacao.EditValue;

            Pessoa usuarioAtual = new Pessoa { Id = Sessao.PessoaLogada.Id };

            if (!_parametros.ParametrosVenda.PedidosPorVendedor || Sessao.GrupoAcesso.Id == 1 || Sessao.GrupoAcesso.Id == 2 || Sessao.GrupoAcesso.Id == 3 ||  Sessao.GrupoAcesso.Id == 6)
            {
                usuarioAtual = null;
            }

            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
            _listaPedidosDeVenda = servicoPedidoDeVenda.ConsulteLista(dataInicial, dataFinal, atendente, vendedor, cliente, tipoPedidoDeVenda, statusPedidoDeVenda, usuarioAtual);

            btnImprimir.Visible = false;

            tblImpressao.Visible = false;

            PreenchaGrid();
            this.Cursor = Cursors.WaitCursor;
        }

        private void PreenchaCboAtendentes()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaAtendentesAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboAtendentes.Properties.DisplayMember = "Descricao";
            cboAtendentes.Properties.ValueMember = "Valor";
            cboAtendentes.Properties.DataSource = listaObjetoValor;
        }

        private void PreenchaCboVendedores()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaVendedoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboVendedores.Properties.DisplayMember = "Descricao";
            cboVendedores.Properties.ValueMember = "Valor";
            cboVendedores.Properties.DataSource = listaObjetoValor;
        }

        private void PreenchaCboTipoDocumento()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoPedidoDeVenda>();

            lista.Insert(0, null);

            cboTipoDocumento.Properties.DisplayMember = "Descricao";
            cboTipoDocumento.Properties.ValueMember = "Valor";
            cboTipoDocumento.Properties.DataSource = lista;
        }

        private void PreenchaCboSituacao()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusPedidoDeVenda>();

            lista.Insert(0, null);

            cboSituacao.Properties.DisplayMember = "Descricao";
            cboSituacao.Properties.ValueMember = "Valor";
            cboSituacao.Properties.DataSource = lista;
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
            List<PedidoDeVendaGrid> listaPedidosDeVendaGrid = new List<PedidoDeVendaGrid>();
            TotaisVendas = 0;

            foreach (var pedido in _listaPedidosDeVenda)
            {
                PedidoDeVendaGrid pedidoGrid = new PedidoDeVendaGrid();

                pedidoGrid.Id = pedido.Id;

                pedidoGrid.DataElaboracao = pedido.DataElaboracao.ToString("dd/MM/yyyy");

                pedidoGrid.Atendente = pedido.Atendente != null ? pedido.Atendente.Id + " - " + pedido.Atendente.DadosGerais.Razao : null;
                pedidoGrid.Vendedor = pedido.Vendedor != null ? pedido.Vendedor.Id + " - " + pedido.Vendedor.DadosGerais.Razao : null;
                pedidoGrid.Cliente = pedido.Cliente != null ? pedido.Cliente.Id + " - " + pedido.Cliente.DadosGerais.Razao : null;

                pedidoGrid.TipoDocumento = pedido.TipoPedidoVenda != null ? pedido.TipoPedidoVenda.Value.Descricao() : string.Empty;
                pedidoGrid.Situacao = pedido.StatusPedidoVenda.Descricao();

                pedidoGrid.ValorTotal = pedido.ValorTotal;
                TotaisVendas += pedido.ValorTotal;

                listaPedidosDeVendaGrid.Add(pedidoGrid);
            }
            txttotal.Text = TotaisVendas.ToString("#,###,##0.00");

            gcPedidosDeVenda.DataSource = listaPedidosDeVendaGrid;
            gcPedidosDeVenda.RefreshDataSource();
        }

        private void Selecione()
        {
            if (_somenteImpressao)
            {
                return;
            }

            _pedidoDeVendaSelecionado = null;

            if (_listaPedidosDeVenda != null && _listaPedidosDeVenda.Count > 0)
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                _pedidoDeVendaSelecionado = servicoPedidoDeVenda.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class PedidoDeVendaGrid
        {
            public int Id { get; set; }

            public string DataElaboracao { get; set; }

            public string Cliente { get; set; }

            public string Atendente { get; set; }

            public string Vendedor { get; set; }

            public string TipoDocumento { get; set; }

            public string Situacao { get; set; }

            public double ValorTotal { get; set; }
        }

        #endregion
    }
}
