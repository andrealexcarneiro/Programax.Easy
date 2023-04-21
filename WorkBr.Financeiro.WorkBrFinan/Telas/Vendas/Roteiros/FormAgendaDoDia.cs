using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Servico.Financeiro.PlanoContasServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Financeiro.PlanosDeContas;
using Programax.Infraestrutura.Negocio.Utils;
using System.Drawing;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Servico.Vendas.RoteiroServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;

namespace Programax.Easy.View.Telas.Vendas.Roteiros
{
    public partial class FormAgendaDoDia : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Roteiro> _listaDeRoteiros;   
        private Roteiro _roteiroSelecionado;
        private List<Roteiro> _listaDeRoteirosSelecionados;
        protected bool _cadastroLiberado=true;
        private int intPedido = 0;

        string numeroDocumentoValidacao;

        #endregion

        #region " CONSTRUTOR "

        public FormAgendaDoDia()
        {
            InitializeComponent();

            PreenchaPeriodos();                       
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void FormRoteiroPesquisa_Load(object sender, EventArgs e)
        {
           
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            FormCadastroRoteiros formCadastroRoteiro = new FormCadastroRoteiros();
            formCadastroRoteiro.ShowDialog();

            Pesquise(null);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            _listaDeRoteirosSelecionados = new List<Roteiro>();

            this.Close();
        }

        private void btnPesquisaNcm_Click(object sender, EventArgs e)
        {
            Pesquise(null);
        }

        private void btnPesquisaPlanoDeContas_Click(object sender, EventArgs e)
        {
            FormPlanosContasPesquisa formPlanosContasPesquisa = new FormPlanosContasPesquisa();

            var planoDeContas = formPlanosContasPesquisa.ExibaPesquisaDePlanoDeContasAtivos();

            if (planoDeContas != null)
            {
                //PreenchaPlanoDeContas(planoDeContas);
            }
        }

        private void txtNumeroPlanoDeContas_Leave(object sender, EventArgs e)
        {
            ServicoPlanoDeContas servicoPlanoDeContas = new ServicoPlanoDeContas();

            //PreenchaPlanoDeContas(planoDeContas);
        }

        private void btnExcluirRoteiro_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("O(s) Roteiro(s) selecionado(s) será(ão) Excluídos(s).\n\nDeseja continuar?", "Excluir Roteiro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            var listaParaExcluir = carregaListaParaConcluirRoteiro();

            if (listaParaExcluir.Count() == 0) return;

            if (!valideSeListaRoteiroEstahConcluida(listaParaExcluir)) return;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                foreach (var item in listaParaExcluir)
                {
                    ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

                    var itemExcluir = servicoRoteiro.Consulte(item.Id);

                    servicoRoteiro.Exclua(itemExcluir.Id);

                    ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

                    var pedido = servicoPedido.Consulte(item.PedidoVenda.Id);

                    pedido.StatusRoteiro = null;

                    servicoPedido.Atualize(pedido);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                throw ex;
            }

            this.Cursor = Cursors.Default;

            MessageBox.Show("Roteiro(s) Excluído(s) com sucesso!", "Conclusão de Roteiro(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Pesquise(null);
        }
        
        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void txtIdPessoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise(null);
            }
        }

        private void cboStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise(null);
            }
        }

        private void gcContasPagarReceber_Click(object sender, EventArgs e)
        {
            
        }

        private void gcContasPagarReceber_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void gcContasPagarReceber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelecioneLancamento();
            }
        }

        private void gcContasPagarReceber_DoubleClick(object sender, EventArgs e)
        {
            SelecioneLancamento();            
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public List<Roteiro> retorneAgendas(DateTime DataDoDia)
        {
            txtDia.Text = DataDoDia.ToString("dd/MM/yyyy");

            Pesquise(DataDoDia);

            this.ShowDialog();

            return _listaDeRoteirosSelecionados;
        }

        public void SelecioneLancamento()
        {            
            _roteiroSelecionado = null;

            if (_listaDeRoteiros != null && _listaDeRoteiros.Count > 0)
            {                
                _roteiroSelecionado = new ServicoRoteiro().Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
                //intPedido = _roteiroSelecionado.PedidoVenda.Id;


                if (_roteiroSelecionado != null)
                {
                    //if(_roteiroSelecionado.Status != EnumStatusRoteiro.EMROTA)
                    //{
                    //    MessageBox.Show("O Lançamento deve estar com o Status: Em Rota!","Roteiros",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //    return;
                    //}
                }
            }  
        }
       
        private bool valideSeListaRoteiroEstahConcluida(List<Roteiro> listaRoteiro)
        {
            numeroDocumentoValidacao = string.Empty;

            for (int i = 0; i < listaRoteiro.Count; i++)
            {
                if (listaRoteiro[i].Status == EnumStatusRoteiro.CONCLUIDO)
                    numeroDocumentoValidacao += listaRoteiro[i].Id + "; ";
            }

            if (numeroDocumentoValidacao != string.Empty)
            {
                MessageBox.Show("Os Roteiros(s) de numero: " + numeroDocumentoValidacao + "Está(ão) Concluído(s). Somente Roteiro(s) Em Rota pode(m) ser Concluído(s)/Excluido(s).", "Conclusão ou Exclusão de Roteiros(s)", MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private bool valideSeListaRoteirosEstahEmRota(List<Roteiro> listaRoteiro)
        {
            numeroDocumentoValidacao = string.Empty;

            for (int i = 0; i < listaRoteiro.Count; i++)
            {
                if (listaRoteiro[i].Status == EnumStatusRoteiro.EMROTA)
                    numeroDocumentoValidacao += listaRoteiro[i].Id + "; ";
            }

            if (numeroDocumentoValidacao != string.Empty)
            {
                MessageBox.Show("O(s) Roteiros(s) de numero: " + numeroDocumentoValidacao + "Está(ão) Em Rota. Somente Roteiro(s) Concluído(s) pode(m) ser Estornados(s).", "Estorno de Roteiro(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        
        public void Pesquise(DateTime? DataDia)
        {
            this.Cursor = Cursors.WaitCursor;

            ServicoRoteiro servicoRoteiro = new ServicoRoteiro();
            
            EnumPeriodo? periodo = (EnumPeriodo?)cboPeriodo.EditValue;

            DateTime? dataInicialPeriodo = DataDia == null? txtDia.Text.ToDateNullabel():DataDia;
            
            _listaDeRoteiros = servicoRoteiro.ConsulteLista(null, periodo, EnumStatusRoteiro.EMAGENDA, EnumDataFiltrarRoteiro.ELABORACAO, dataInicialPeriodo, dataInicialPeriodo, 0);

            preencherGrid();

            txtQuantidade.Text = _listaDeRoteiros.Count.ToString();
           
            this.Cursor = Cursors.Default;
        }

        private void preencherGrid()
        {
            List<RoteiroAuxiliar> listaDeRoteirosAuxiliar = new List<RoteiroAuxiliar>();

            int totalConcluido = 0;
            int totalEmRota = 0;
            int totalInconcluso = 0;
            int totalEmAgenda = 0;

            for (int i = 0; i < _listaDeRoteiros.Count; i++)
            {
                var roteiro = _listaDeRoteiros[i];

                RoteiroAuxiliar RoteiroAuxiliar = new RoteiroAuxiliar();

                RoteiroAuxiliar.Id = roteiro.Id;
                RoteiroAuxiliar.Funcionario = roteiro.PessoaFuncionario != null ? roteiro.PessoaFuncionario.Id + " - " + roteiro.PessoaFuncionario.DadosGerais.Razao : string.Empty;
                RoteiroAuxiliar.Pedido = roteiro.PedidoVenda.Id.ToString();
                RoteiroAuxiliar.Cliente = roteiro.PedidoVenda.Cliente != null ? roteiro.PedidoVenda.Cliente.DadosGerais.Razao : string.Empty;

                RoteiroAuxiliar.Periodo = roteiro.Periodo.Descricao();

                RoteiroAuxiliar.Endereco = roteiro.PedidoVenda.EnderecoPedidoDeVenda != null ? "Rua: " +
                                           roteiro.PedidoVenda.EnderecoPedidoDeVenda.Rua + " - " + "N.: " +
                                           roteiro.PedidoVenda.EnderecoPedidoDeVenda.Numero + " - " + "Bairro: " +
                                           roteiro.PedidoVenda.EnderecoPedidoDeVenda.Bairro + " - " + "Cidade: " +
                                           roteiro.PedidoVenda.EnderecoPedidoDeVenda.Cidade.Descricao : string.Empty;

                RoteiroAuxiliar.DataElaboracao = roteiro.DataElaboracao.ToString("dd/MM/yyyy");
                if (roteiro.PedidoVenda.StatusPedidoVenda !=  EnumStatusPedidoDeVenda.EMLIBERACAO)
                {
                    listaDeRoteirosAuxiliar.Add(RoteiroAuxiliar);
                }
                

                if (roteiro.Status == EnumStatusRoteiro.EMROTA)
                {
                    RoteiroAuxiliar.Imagem = Properties.Resources.icone_verde;

                    totalEmRota += 1;
                }
                else if (roteiro.Status == EnumStatusRoteiro.CONCLUIDO)
                {
                    RoteiroAuxiliar.Imagem = Properties.Resources.icone_azul;

                    totalConcluido += 1;
                }
                else if (roteiro.Status == EnumStatusRoteiro.INCONCLUSO)
                {
                    RoteiroAuxiliar.Imagem = Properties.Resources.icone_vermelho;

                    totalInconcluso += 1;
                }
                else if (roteiro.Status == EnumStatusRoteiro.EMAGENDA)
                {
                    RoteiroAuxiliar.Imagem = Properties.Resources.icons8_linha_vertical_30;
                    totalEmAgenda += 1;
                }
                else
                {
                    RoteiroAuxiliar.Imagem = null;
                }

                gcAgendas.DataSource = listaDeRoteirosAuxiliar;
                gcAgendas.RefreshDataSource();
            }
        }

        private void PreenchaPeriodos()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumPeriodo>();
            lista.Insert(0, null);

            cboPeriodo.Properties.DataSource = lista;
            cboPeriodo.Properties.ValueMember = "Valor";
            cboPeriodo.Properties.DisplayMember = "Descricao";
          
        }
        
        protected virtual Roteiro RetorneRoteiroSelecionado()
        {
            Roteiro roteiro = null;

            if (_listaDeRoteiros != null && _listaDeRoteiros.Count > 0)
            {
                ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

                roteiro = servicoRoteiro.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            return roteiro;
        }
       
        private void PreenchaPrimeiroEUltimoDiaMes()
        {   
            var primeiroDiaMes = DateTime.Now;
            var addDias = DateTime.Now;
            txtDia.DateTime = primeiroDiaMes;
           
            cboPeriodo.EditValue = EnumPeriodo.MANHA;                  
        }

        private List<Roteiro> carregaListaParaConcluirRoteiro()
        {
            if (gridViewRoteiro.SelectedRowsCount == 0) return null;

            Roteiro itemParaEmissao = new Roteiro();
            List<Roteiro> listaItemParaEmissao = new List<Roteiro>();

            foreach (var item in gridViewRoteiro.GetSelectedRows())
            {
                var lancamento = colunaId.View.GetRowCellValue(item, colunaId);

                itemParaEmissao = _listaDeRoteiros.FirstOrDefault(x => x.Id == lancamento.ToInt());

                if (itemParaEmissao != null)
                    listaItemParaEmissao.Add(itemParaEmissao);
            }

            return listaItemParaEmissao;
        }

        private void ConcluirRoteiro()
        {   
            if (MessageBox.Show("O(s) Roteiro(s) selecionado(s) será(ão) Concluído(s) na <data atual>.\n\nDeseja continuar?", "Concluir Roteiro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            var listaParaQuitacaoDeTitulos = carregaListaParaConcluirRoteiro();

            if (listaParaQuitacaoDeTitulos.Count() == 0) return;

            if (!valideSeListaRoteiroEstahConcluida(listaParaQuitacaoDeTitulos)) return;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                foreach (var item in listaParaQuitacaoDeTitulos)
                {
                    var roteiroSelecionado = RetorneRoteiroSelecionado();
                                        
                    ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

                    var itemConcluir = servicoRoteiro.Consulte(item.Id);

                    itemConcluir.Status = EnumStatusRoteiro.CONCLUIDO;                    
                    itemConcluir.DataConclusao = DateTime.Now;

                    servicoRoteiro.Atualize(itemConcluir);

                    ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

                    var pedido = servicoPedido.Consulte(itemConcluir.PedidoVenda.Id);

                    pedido.StatusRoteiro = EnumStatusRoteiro.CONCLUIDO;

                    servicoPedido.Atualize(pedido);

                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                throw ex;
            }

            this.Cursor = Cursors.Default;

            MessageBox.Show("Roteiro(s) concluido(s) com sucesso!", "Conclusão de Roteiro(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Pesquise(null);
        
        }

        private void EstornarTituloDiretoDaPesquisa()
        {
            if (MessageBox.Show("O(s) Roteiro(s) selecionado(s) será(ão) Estornado(s).\n\nDeseja continuar?", "Estornar Roteiro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            var listaParaEstorno = carregaListaParaConcluirRoteiro();

            if (!valideSeListaRoteirosEstahEmRota(listaParaEstorno)) return;

            try
            {
                foreach (var item in listaParaEstorno)
                {
                    var tituloSelecionado = RetorneRoteiroSelecionado();
                                       
                    ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

                    var itemEstorno = servicoRoteiro.Consulte(item.Id);

                    itemEstorno.DataConclusao = null;
                    itemEstorno.Status = EnumStatusRoteiro.EMROTA;

                    servicoRoteiro.Atualize(itemEstorno);

                    ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

                    var pedido = servicoPedido.Consulte(itemEstorno.PedidoVenda.Id);

                    pedido.StatusRoteiro = EnumStatusRoteiro.EMROTA;

                    servicoPedido.Atualize(pedido);

                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                throw ex;               
            }

            this.Cursor = Cursors.Default;

            MessageBox.Show("Roteiro(s) Estornados(s) com sucesso!", "Estorno de Roteiro(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Pesquise(null);
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class RoteiroAuxiliar
        {
            public Image Imagem { get; set; }

            public int Id { get; set; }

            public string DataElaboracao { get; set; }

            public string Funcionario { get; set; }

            public string Pedido { get; set; }

            public string Cliente { get; set; }

            public string Endereco { get; set; }            
            
            public string Periodo { get; set; }
        }

        #endregion

        private void btnImprimirRelatorio_Click(object sender, EventArgs e)
        {
            //gcRoteiros.view
            gcAgendas.ExibaRelatorio();
        }

        private void btnPesquisaPedidoVendas_Click(object sender, EventArgs e)
        {
            FormPesquisaPedidoDeVenda formPesquisaPedidoDeVenda = new FormPesquisaPedidoDeVenda();

            var pedidoDeVenda = formPesquisaPedidoDeVenda.ExibaPesquisaDePedidosDeVenda();

            if (pedidoDeVenda != null)
            {
                if (pedidoDeVenda.StatusPedidoVenda != EnumStatusPedidoDeVenda.FATURADO ||
                    pedidoDeVenda.StatusPedidoVenda != EnumStatusPedidoDeVenda.EMITIDONFE)
                {
                    MessageBox.Show("Para pesquisar, o pedido de venda deve estar FATURADO OU EMITIDO NFE",
                                    "Pesquisa de Roteiro", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void btnAdicionarRoteiro_Click(object sender, EventArgs e)
        {
            _listaDeRoteirosSelecionados = carregaListaParaConcluirRoteiro();
                       
            this.Close();
        }

        private void cboPeriodo_EditValueChanged(object sender, EventArgs e)
        {
            Pesquise(txtDia.Text.ToDate());
        }

        private void btnAdicionarSelecao_Click(object sender, EventArgs e)
        {
            _listaDeRoteirosSelecionados = carregaListaParaConcluirRoteiro();

            this.Close();
        }

        private void btnDetalhes_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            //_listaDeRoteiros.CarregueLazyLoad();

            RelatorioListaRoteiros relatorio = new RelatorioListaRoteiros(txtDia.Text.ToDate(), txtDia.Text.ToDate());
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
            this.Cursor = Cursors.Default;
        }
    }
    
}
