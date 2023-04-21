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
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Servico.Integracao.PreVendaDjpdvServ;
using Programax.Easy.Negocio.Integracao.PreVendaDjpdvObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.FrenteDeLoja.ExportaPedidoVendaPdvEcf
{
    public partial class FormExportaPedidoDeVendaPdvEcf : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<VWVenda> _listaVwVendas = new List<VWVenda>();

        #endregion

        #region " CONSTRUTOR "

        public FormExportaPedidoDeVendaPdvEcf()
        {
            InitializeComponent();

            PreenchaCboStatus();
            PreenchaCboTipoPesquisa();
            PreenchaCboVendaJahExportada();

            txtDataInicial.DateTime = DateTime.Now.AddMonths(-1);
            txtDataFinal.DateTime = DateTime.Now;

            Pesquise();

            this.ActiveControl = txtPedidoDeVendaId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnPesquisaVendas_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void gcVendas_KeyUp(object sender, KeyEventArgs e)
        {
            SelecioneVwVenda();
        }

        private void gcVendas_Click(object sender, EventArgs e)
        {
            SelecioneVwVenda();
        }

        private void gcVendas_DoubleClick(object sender, EventArgs e)
        {
            ExportarPedidoDeVenda();
        }

        private void btnExportarVendas_Click(object sender, EventArgs e)
        {
            ExportarPedidoDeVenda();
        }

        private void txtPedidoDeVendaId_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPedidoDeVendaId.Text))
            {
                cboTipoPesquisa.Enabled = false;
                txtDataInicial.Enabled = false;
                txtDataFinal.Enabled = false;
                cboStatus.Enabled = false;
                cboVendaJahExportada.Enabled = false;
            }
            else
            {
                cboTipoPesquisa.Enabled = true;
                txtDataInicial.Enabled = true;
                txtDataFinal.Enabled = true;
                cboStatus.Enabled = true;
                cboVendaJahExportada.Enabled = true;
            }
        }

        private void txtPedidoDeVendaId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboStatus()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusPedidoDeVenda>();

            lista.Insert(0, null);

            cboStatus.Properties.DisplayMember = "Descricao";
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DataSource = lista;

            cboStatus.EditValue = EnumStatusPedidoDeVenda.FATURADO;
        }

        private void PreenchaCboVendaJahExportada()
        {
            ObjetoDescricaoValor objetoDescricaoValorSim = new ObjetoDescricaoValor();
            objetoDescricaoValorSim.Descricao = "SIM";
            objetoDescricaoValorSim.Valor = true;

            ObjetoDescricaoValor objetoDescricaoValorNao = new ObjetoDescricaoValor();
            objetoDescricaoValorNao.Descricao = "NÃO";
            objetoDescricaoValorNao.Valor = false;


            var lista = new List<ObjetoDescricaoValor>();
            lista.Add(null);
            lista.Add(objetoDescricaoValorSim);
            lista.Add(objetoDescricaoValorNao);

            cboVendaJahExportada.Properties.DisplayMember = "Descricao";
            cboVendaJahExportada.Properties.ValueMember = "Valor";
            cboVendaJahExportada.Properties.DataSource = lista;

            cboVendaJahExportada.EditValue = false;
        }

        private void PreenchaCboTipoPesquisa()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoPesquisa>();

            cboTipoPesquisa.Properties.DisplayMember = "Descricao";
            cboTipoPesquisa.Properties.ValueMember = "Valor";
            cboTipoPesquisa.Properties.DataSource = lista;

            cboTipoPesquisa.EditValue = EnumTipoPesquisa.ELABORACAO;
        }

        private void PreenchaGrid()
        {
            List<ExportacaoPedidoVendaGrid> listaItensGrid = new List<ExportacaoPedidoVendaGrid>();

            foreach (var vwVenda in _listaVwVendas)
            {
                ExportacaoPedidoVendaGrid exportacaoPedidoVendaGrid = new ExportacaoPedidoVendaGrid();

                exportacaoPedidoVendaGrid.ClienteCpfCnpj = vwVenda.ClienteCpfCnpj;
                exportacaoPedidoVendaGrid.ClienteId = vwVenda.ClienteId;
                exportacaoPedidoVendaGrid.ClienteNomeFantasia = vwVenda.ClienteNome;
                exportacaoPedidoVendaGrid.DataElaboracao = vwVenda.DataElaboracao;
                exportacaoPedidoVendaGrid.VendaJahExportada = vwVenda.VendaJahExportadaPdvEcf ? "SIM" : "NÃO";
                exportacaoPedidoVendaGrid.Id = vwVenda.Id;
                exportacaoPedidoVendaGrid.StatusDocumento = vwVenda.Status.Descricao();
                exportacaoPedidoVendaGrid.ValorTotal = vwVenda.ValorTotal;
                exportacaoPedidoVendaGrid.VendedorNomeFantasia = vwVenda.VendedorId > 0 ? vwVenda.VendedorId + " - " + vwVenda.VendedorNome : string.Empty;

                listaItensGrid.Add(exportacaoPedidoVendaGrid);
            }

            gcVendas.DataSource = listaItensGrid;
            gcVendas.RefreshDataSource();
        }

        private void Pesquise()
        {
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

            int? numeroDocumento = txtPedidoDeVendaId.Text.ToIntNullabel();

            bool pedidoFaturamento = (EnumTipoPesquisa)cboTipoPesquisa.EditValue == EnumTipoPesquisa.FECHAMENTO;

            DateTime dataInicialElaboracao = txtDataInicial.Text.ToDate();
            DateTime dataFinalElaboracao = txtDataFinal.Text.ToDate();

            if (dataFinalElaboracao == DateTime.MinValue)
            {
                dataFinalElaboracao = DateTime.MaxValue;
            }

            EnumStatusPedidoDeVenda? statusPedidoDeVenda = (EnumStatusPedidoDeVenda?)cboStatus.EditValue;
            bool? vendaJahExportada = (bool?)cboVendaJahExportada.EditValue;

            _listaVwVendas = servicoPedidoDeVenda.ConsulteListaVWVendas(numeroDocumento,
                                                                                                             pedidoFaturamento,
                                                                                                             dataInicialElaboracao,
                                                                                                             dataFinalElaboracao,
                                                                                                             statusPedidoDeVenda,
                                                                                                             vendaJahExportada);

            ExibaOuEscondaBotaoExportacao();

            PreenchaGrid();
        }

        private void ExibaOuEscondaBotaoExportacao()
        {
            if (_listaVwVendas.Count > 0)
            {
                btnExportarVendas.Visible = true;
            }
            else
            {
                btnExportarVendas.Visible = false;
            }
        }

        private void SelecioneVwVenda()
        {
            var venda = RetorneVwVendaSelecionada();

            if (venda != null)
            {
                if (venda.VendaJahExportadaPdvEcf)
                {
                    btnExportarVendas.Text = "Re-exportar Venda";
                }
                else
                {
                    btnExportarVendas.Text = "Exportar Venda";
                }
            }
        }

        private VWVenda RetorneVwVendaSelecionada()
        {
            return _listaVwVendas.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt()); ;
        }

        private void ExportarPedidoDeVenda()
        {
            var vwVenda = RetorneVwVendaSelecionada();

            if (MessageBox.Show("Deseja Exportar o Pedido: " + vwVenda.Id, "Exportação para Pdv", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            Action actionExportacao = () =>
            {
                PreVendaDjpdv preVenda = new PreVendaDjpdv();
                preVenda.PedidoDeVendaId = vwVenda.Id;

                ServicoPreVendaDjpdv servicoPreVendaDjpdv = new ServicoPreVendaDjpdv();
                servicoPreVendaDjpdv.Cadastre(preVenda);

                Pesquise();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionExportacao,
                                                                                mensagemDeSucesso: "Venda exportada para PDV.",
                                                                                tituloMensagemDeErro: "Erro ao exportar venda",
                                                                                tituloMensagemDeSucesso: "Venda exportada com sucesso");
        }

        #endregion

        #region " CLASSES E ENUMERADORES AUXILIARES "

        private class ExportacaoPedidoVendaGrid
        {
            public int Id { get; set; }

            public DateTime DataElaboracao { get; set; }

            public int ClienteId { get; set; }

            public string ClienteNomeFantasia { get; set; }

            public string ClienteCpfCnpj { get; set; }

            public double ValorTotal { get; set; }

            public string VendedorNomeFantasia { get; set; }

            public string StatusDocumento { get; set; }

            public string VendaJahExportada { get; set; }
        }

        private enum EnumTipoPesquisa
        {
            [Description("ELABORAÇÃO")]
            ELABORACAO,

            [Description("FECHAMENTO")]
            FECHAMENTO
        }

        #endregion
    }
}
