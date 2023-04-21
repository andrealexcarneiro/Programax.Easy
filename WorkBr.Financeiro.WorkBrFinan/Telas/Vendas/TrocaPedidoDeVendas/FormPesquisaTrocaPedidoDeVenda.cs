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
using Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.TrocaPedidoDeVendaServ;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;

namespace Programax.Easy.View.Telas.Vendas.TrocaPedidoDeVendas
{
    public partial class FormPesquisaTrocaPedidoDeVenda : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<TrocaPedidoDeVenda> _listaTrocaPedidosDeVenda;
        private TrocaPedidoDeVenda _trocaPedidoDeVendaSelecionado;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaTrocaPedidoDeVenda()
        {
            InitializeComponent();

            _listaTrocaPedidosDeVenda = new List<TrocaPedidoDeVenda>();

            PreenchaCboSituacao();

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

        private void btnPesquisaPedido_Click(object sender, EventArgs e)
        {
            FormPesquisaPedidoDeVenda formPesquisaPedidoDeVenda = new FormPesquisaPedidoDeVenda();
            var pedido = formPesquisaPedidoDeVenda.ExibaPesquisaDePedidosDeVendaFaturadosEEmitdosNfe();

            if (pedido != null)
            {
                PreenchaPedidoDeVenda(pedido, exibirMensagemDeNaoEncontrado: true);
            }
        }

        private void txtPedidoId_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPedidoId.Text))
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                var pedidoDeVenda = servicoPedidoDeVenda.ConsultePedidoFaturadoOuEmitidoNfe(txtPedidoId.Text.ToInt());

                PreenchaPedidoDeVenda(pedidoDeVenda, exibirMensagemDeNaoEncontrado: true);
            }
            else
            {
                PreenchaPedidoDeVenda(null);
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public TrocaPedidoDeVenda ExibaPesquisaDeTrocaPedidosDeVenda()
        {
            this.ShowDialog();

            return _trocaPedidoDeVendaSelecionado;
        }

        private void Pesquise()
        {
            DateTime? dataInicial = txtDataInicial.Text.ToDateNullabel();
            DateTime? dataFinal = txtDataFinal.Text.ToDateNullabel();

            EnumStatusTrocaPedidoDeVenda? statusTrocaPedidoDeVenda = (EnumStatusTrocaPedidoDeVenda?)cboSituacao.EditValue;

            PedidoDeVenda pedidoDeVenda = !string.IsNullOrEmpty(txtPedidoId.Text) ? new PedidoDeVenda { Id = txtPedidoId.Text.ToInt() } : null;

            ServicoTrocaPedidoDeVenda servicoTrocaPedidoDeVenda = new ServicoTrocaPedidoDeVenda();
            _listaTrocaPedidosDeVenda = servicoTrocaPedidoDeVenda.ConsulteLista(dataInicial, dataFinal, pedidoDeVenda, statusTrocaPedidoDeVenda);

            PreenchaGrid();
        }

        private void PreenchaCboSituacao()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusTrocaPedidoDeVenda>();

            lista.Insert(0, null);

            cboSituacao.Properties.DisplayMember = "Descricao";
            cboSituacao.Properties.ValueMember = "Valor";
            cboSituacao.Properties.DataSource = lista;
        }

        private void PreenchaGrid()
        {
            List<TrocaPedidoDeVendaGrid> listaTrocaPedidosDeVendaGrid = new List<TrocaPedidoDeVendaGrid>();

            foreach (var troca in _listaTrocaPedidosDeVenda)
            {
                TrocaPedidoDeVendaGrid trocaPedidoDeVendaGrid = new TrocaPedidoDeVendaGrid();

                trocaPedidoDeVendaGrid.Id = troca.Id;

                trocaPedidoDeVendaGrid.PedidoId = troca.PedidoDeVenda.Id;

                trocaPedidoDeVendaGrid.DataElaboracaoTroca = troca.DataElaboracao.ToString("dd/MM/yyyy");
                trocaPedidoDeVendaGrid.DataElaboracaoPedido = troca.PedidoDeVenda.DataElaboracao.ToString("dd/MM/yyyy");

                trocaPedidoDeVendaGrid.Cliente = troca.PedidoDeVenda.Cliente.Id + " - " + troca.PedidoDeVenda.Cliente.DadosGerais.NomeFantasia;

                trocaPedidoDeVendaGrid.Situacao = troca.Status.Descricao();

                trocaPedidoDeVendaGrid.ValorDiferenca = troca.ValorTotalTroca.ToString("#,##0.00");

                listaTrocaPedidosDeVendaGrid.Add(trocaPedidoDeVendaGrid);
            }

            gcTrocaPedidosDeVenda.DataSource = listaTrocaPedidosDeVendaGrid;
            gcTrocaPedidosDeVenda.RefreshDataSource();
        }

        private void Selecione()
        {
            _trocaPedidoDeVendaSelecionado = null;

            if (_listaTrocaPedidosDeVenda != null && _listaTrocaPedidosDeVenda.Count > 0)
            {
                ServicoTrocaPedidoDeVenda servicoTrocaPedidoDeVenda = new ServicoTrocaPedidoDeVenda();

                _trocaPedidoDeVendaSelecionado = servicoTrocaPedidoDeVenda.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        public void PreenchaPedidoDeVenda(PedidoDeVenda pedidoDeVenda, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (pedidoDeVenda != null)
            {
                txtPedidoId.Text = pedidoDeVenda.Id.ToString();
                txtClienteId.Text = pedidoDeVenda.Cliente.Id.ToString();
                txtNomeCliente.Text = pedidoDeVenda.Cliente.DadosGerais.NomeFantasia;
                txtClienteCpfCnpj.Text = pedidoDeVenda.Cliente.DadosGerais.CpfCnpj;
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
            }
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class TrocaPedidoDeVendaGrid
        {
            public int Id { get; set; }

            public int PedidoId { get; set; }

            public string DataElaboracaoPedido { get; set; }

            public string DataElaboracaoTroca { get; set; }

            public string Cliente { get; set; }

            public string Situacao { get; set; }

            public string ValorDiferenca { get; set; }
        }

        #endregion

    }
}
