using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Negocio.TeleMarketing.Enumeradores;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Telemarketing.TmkServ;
using Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio;
using Programax.Easy.View.ClassesAuxiliares;
using System.Linq;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using System.Drawing;

namespace Programax.Easy.View.Telas.TeleMarketing
{
    public partial class FormGerenciarAtendimentos : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<GerenciarTmk> _listaTmk;
        private PedidoDeVenda _pedidoDeVendaSelecionado;

        #endregion

        #region " CONSTRUTOR "

        public FormGerenciarAtendimentos(bool somenteImpressao = false)
        {
            InitializeComponent();

            _listaTmk = new List<GerenciarTmk>();

            PreenchaCboStatusAtendimento();
            PreenchaCboVendedores();

            if (somenteImpressao)
            {
                this.NomeDaTela = "Gerenciar Atendimentos";
                btnImprimir.Visible = false;
            }

            txtDataInicial.DateTime = DateTime.Now.Date;
            txtDataFinal.DateTime = DateTime.Now.Date;

            this.ActiveControl = txtDataInicial;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void pbPesquisa_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void cboParceiro_KeyDown(object sender, KeyEventArgs e)
        {
            if (cboParceiro.Text.Length <= 2)
            {
                if (e.KeyCode != Keys.Enter && e.KeyCode != Keys.Down && e.KeyCode != Keys.Up
                    && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
                {
                    PreenchaCboParceiro(cboParceiro.Text);
                }
            }
        }

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
               
        #endregion

        #region " MÉTODOS AUXILIARES "

        private void ImprimirGridEGrafico()
        {
            // Create objects and define event handlers. 
            CompositeLink composLink = new CompositeLink(new PrintingSystem());

            composLink.CreateMarginalHeaderArea +=
                new CreateAreaEventHandler(composLink_CreateMarginalHeaderArea);

            PrintableComponentLink pcLink1 = new PrintableComponentLink();
            PrintableComponentLink pcLink2 = new PrintableComponentLink();
            Link linkMainReport = new Link();
            linkMainReport.CreateDetailArea +=
                new CreateAreaEventHandler(linkMainReport_CreateDetailArea);
            Link linkGrid1Report = new Link();
            linkGrid1Report.CreateDetailArea +=
                new CreateAreaEventHandler(linkGrid1Report_CreateDetailArea);
            Link linkGrid2Report = new Link();
            linkGrid2Report.CreateDetailArea +=
                new CreateAreaEventHandler(linkGrid2Report_CreateDetailArea);

            // Assign the controls to the printing links. 
            pcLink1.Component = this.gcAtendimentos;
            pcLink2.Component = this.chtFluxoCaixa;

            // Populate the collection of links in the composite link. 
            // The order of operations corresponds to the document structure. 
            composLink.Links.Add(linkGrid1Report);
            composLink.Links.Add(pcLink1);
            composLink.Links.Add(linkMainReport);
            composLink.Links.Add(linkGrid2Report);
            composLink.Links.Add(pcLink2);

            // Create the report and show the preview window.
            composLink.Landscape = true;
            composLink.ShowPreviewDialog();
        }

        // Inserts a PageInfoBrick into the top margin to display the time. 
        void composLink_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            e.Graph.DrawPageInfo(PageInfo.DateTime, "{0:hhhh:mmmm:ssss}", Color.Black,
                new RectangleF(0, 0, 200, 50), BorderSide.None);
        }

        // Creates a text header for the first grid. 
        void linkGrid1Report_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            TextBrick tb = new TextBrick();
            tb.Text = "Relatório de Atendimentos";
            tb.Font = new Font("Arial", 15);
            tb.Rect = new RectangleF(10, 10, 300, 30);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            e.Graph.DrawBrick(tb);
        }

        // Creates an interval between the grids and fills it with color. 
        void linkMainReport_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {

            TextBrick tb = new TextBrick();
            tb.Rect = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
            tb.BackColor = Color.Gray;
            e.Graph.DrawBrick(tb);
        }

        // Creates a text header for the second grid. 
        void linkGrid2Report_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            TextBrick tb = new TextBrick();
            tb.Text = "Gráfico Finalizado x Inconcluso";
            tb.Font = new Font("Arial", 15);
            tb.Rect = new RectangleF(10, 10, 200, 10);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            e.Graph.DrawBrick(tb);
        }

        public void CarregaGrafico(List<GerenciarTmk> listaDados)
        {
            var percFinalizado = listaDados.Count(x => x.Status.ToInt() == EnumStatusAtendimento.CONCLUIDO.GetHashCode());
            var percInconcluso = listaDados.Count(x => x.Status.ToInt() == EnumStatusAtendimento.AGENDADO.GetHashCode());

            List<DadosGrafico> dadosGrafico = new List<DadosGrafico>();

            DadosGrafico itemdadosGrafico = new DadosGrafico();

            itemdadosGrafico.DataRealizado = DateTime.Now;

            itemdadosGrafico.Finalizado = percFinalizado;//Math.Round(((percFinalizado + percInconcluso) / percFinalizado).ToDouble(), 2);

            itemdadosGrafico.Inconcluso = percInconcluso;//Math.Round(((percFinalizado + percInconcluso) / percInconcluso).ToDouble(), 2);

            dadosGrafico.Add(itemdadosGrafico);

            chtFluxoCaixa.DataSource = dadosGrafico;
        }

        private void PreenchaCboParceiro(string parametro)
        {
            List<VWPessoasSelecao> parceiro = new List<VWPessoasSelecao>();

            parceiro = new ServicoPessoa().ConsulteListaPessoaPelaRazaoSocialLetras(parametro);

            List<ObjetoParaComboBox> listaCombo = new List<ObjetoParaComboBox>();
            foreach (var item in parceiro)
            {
                ObjetoParaComboBox objetoComboBox = new ObjetoParaComboBox();
                objetoComboBox.Valor = item.Id;
                objetoComboBox.Descricao = item.Razao;

                listaCombo.Add(objetoComboBox);
            }

            listaCombo.Insert(0, null);

            cboParceiro.Properties.DisplayMember = "Descricao";
            cboParceiro.Properties.ValueMember = "Valor";
            cboParceiro.Properties.DataSource = listaCombo;
        }

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

            cboStatusAtendimento.Properties.DataSource = lista;
            cboStatusAtendimento.EditValue = EnumStatusPedidoDeVenda.FATURADO;

            this.ShowDialog();

            return _pedidoDeVendaSelecionado;
        }

        private void Pesquise()
        {
            this.Cursor = Cursors.WaitCursor;

            DateTime? dataInicial = txtDataInicial.Text.ToDateNullabel();
            DateTime? dataFinal = txtDataFinal.Text.ToDateNullabel();

            Pessoa vendedor = cboParceiro.EditValue.ToInt() != 0 ? new Pessoa { Id = cboParceiro.EditValue.ToInt() } : null;

            EnumStatusAtendimento? statusTmk = (EnumStatusAtendimento?)cboStatusAtendimento.EditValue;

            ServicoTmk servicoTmk = new ServicoTmk();
            _listaTmk = servicoTmk.ConsulteListaParaGerenciarTMK(vendedor, statusTmk, dataInicial, dataFinal);

            PreenchaGrid();

            CarregaGrafico(_listaTmk);

            this.Cursor = Cursors.Default;
        }

        private void PreenchaCboStatusAtendimento()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusAtendimento>();

            lista.RemoveAt(4);

            lista.Insert(0, null);

            cboStatusAtendimento.Properties.DisplayMember = "Descricao";
            cboStatusAtendimento.Properties.ValueMember = "Valor";
            cboStatusAtendimento.Properties.DataSource = lista;
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

            cboParceiro.Properties.DisplayMember = "Descricao";
            cboParceiro.Properties.ValueMember = "Valor";
            cboParceiro.Properties.DataSource = listaObjetoValor;

            if (string.IsNullOrWhiteSpace(cboParceiro.Text))
            {
                cboParceiro.EditValue = null;
            }
        }

        private void PreenchaGrid()
        {
            List<TmkGrid> listaTmkGrid = new List<TmkGrid>();

            foreach (var itemTmk in _listaTmk)
            {
                TmkGrid tmkGrid = new TmkGrid();

                tmkGrid.Status = ((EnumStatusAtendimento)itemTmk.Status.ToInt()).Descricao();

                tmkGrid.Vendedor = itemTmk.VendedorId != 0 ? itemTmk.VendedorId.ToString() : null;

                tmkGrid.Data = itemTmk.Data.ToString("dd/MM/yyyy HH:mm:ss");

       //         tmkGrid.Duracao = itemTmk.Duracao.Substring(0, 8);

                tmkGrid.ValorVenda = itemTmk.ValorVenda.ToString("0.00");

                tmkGrid.Id = itemTmk.IdAtendimento.ToInt();

                tmkGrid.Vendedor = new ServicoPessoa().Consulte(itemTmk.VendedorId.ToInt()).DadosGerais.Razao;

                listaTmkGrid.Add(tmkGrid);
            }

            TmkGrid tmkGrid2 = new TmkGrid();            

            var vendedorAgrupado = _listaTmk.GroupBy(x => x.VendedorId);

            foreach (var item2 in vendedorAgrupado)
            {
                RodapeGrid item = new RodapeGrid();

                item.Vendedor = new ServicoPessoa().Consulte(item2.Key.ToInt()).DadosGerais.Razao;

                item.TotalVenda = _listaTmk.Where(x=>x.VendedorId == item2.Key.ToInt()).Sum(x => x.ValorVenda).ToString("0.00");

                tmkGrid2.listaVendedores.Add(item);                
            }

            listaTmkGrid.Add(tmkGrid2);

            gcAtendimentos.DataSource = listaTmkGrid;
            gcAtendimentos.RefreshDataSource();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class TmkGrid
        {
            public TmkGrid()
            {
                listaVendedores = new List<RodapeGrid>();
            }

            public int Id { get; set; }

            public string Data { get; set; }

            public string Status { get; set; }

            public string Vendedor { get; set; }

            public string Duracao { get; set; }

            public string ValorVenda { get; set; }

            public List<RodapeGrid> listaVendedores { get; set; }
        }

        public class RodapeGrid
        {
            public string Vendedor { get; set; }

            public string TotalVenda { get; set; }
        }

        public class DadosGrafico
        {
            public DateTime DataRealizado { get; set; }
            public double Finalizado { get; set; }
            public double Inconcluso { get; set; }
        }

        #endregion

        private void btnImprimir_Click_1(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            //gcGridFluxoCaixa.

            ImprimirGridEGrafico();

            this.Cursor = Cursors.Default;
        }
    }
}
