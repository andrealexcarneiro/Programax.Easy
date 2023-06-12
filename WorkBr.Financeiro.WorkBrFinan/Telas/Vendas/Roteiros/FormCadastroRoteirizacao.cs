using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Financeiro.PlanoContasServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.View.Telas.Financeiro.PlanosDeContas;
using Programax.Infraestrutura.Negocio.Utils;
using System.Drawing;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Servico.Vendas.RoteiroServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;

namespace Programax.Easy.View.Telas.Vendas.Roteiros
{
    public partial class FormCadastroRoteirizacao : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Roteiro> _listaDeRoteiros;
        private Roteiro _roteiroSelecionado;
        protected bool _cadastroLiberado = true;
        protected bool _editarPedidoLiberado;
        Parametros _parametros = new ServicoParametros().ConsulteParametros();
        int RoteiroId = 0;
        int IdRoteiro = 0;

        string numeroDocumentoValidacao;

        #endregion


        #region " CONSTRUTOR "

        public FormCadastroRoteirizacao(int CodigoRoteiro, DateTime DataRoteiro, DateTime? DataConclusão, Pessoa Funcionario)
        {
            InitializeComponent();

            txtIdPessoa.Text = Funcionario != null ? Funcionario.Id.ToString() : string.Empty;
            txtNomePessoa.Text = Funcionario != null ? Funcionario.DadosGerais.Razao : string.Empty;

            txtCodigoRoteiro.Text = CodigoRoteiro.ToString();

            txtDataRoteiro.Text = DataRoteiro.ToString("dd/MM/yyyy");

            txtDataConclusao.Text = DataConclusão != null ? DataConclusão.ToStringNull() : string.Empty;

            if(CodigoRoteiro != 0)
            {
                _listaDeRoteiros = new ServicoRoteiro().ConsulteListaPorRoteirizacao(CodigoRoteiro);
                preencherGrid();
                IdRoteiro = CodigoRoteiro;
            }

            _editarPedidoLiberado = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.PEDIDODEVENDAS).Alterar;
           

            this.ActiveControl = txtIdPessoa;
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

            Pesquise();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisaNcm_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void txtIdPessoa_Leave(object sender, EventArgs e)
        {
            pesquiseFuncionarioPorId();
        }

        private void txtNumeroPedidoVendas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AdicionarParaRota(txtNumeroPedidoVendas.Text.ToInt());
            }
        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var pessoa = formPessoaPesquisa.PesquisePessoaFuncionarioAtiva();

            if (pessoa != null)
            {
                PreenchaPessoa(pessoa);
            }
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

            var planoDeContas = servicoPlanoDeContas.ConsultePlanoDeContasAtivoPeloNumero(txtNumeroPedidoVendas.Text);

            //PreenchaPlanoDeContas(planoDeContas);
        }

        private void btnExcluirRoteiro_Click(object sender, EventArgs e)
        {
            if (gridViewRoteiro.SelectedRowsCount == 0)
            {
                MessageBox.Show("Selecione Pelo menos um item para Excluir.",
                                "Exclusão de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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
                    _listaDeRoteiros.Remove(item);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                throw ex;
            }

            this.Cursor = Cursors.Default;

            Pesquise();

            this.Cursor = Cursors.Default;
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
                pesquiseFuncionarioPorId();
            }
        }

        private void cboStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void gcContasPagarReceber_DoubleClick(object sender, EventArgs e)
        {
            var roteiro = RetorneRoteiroSelecionado();

            if (roteiro != null)
            {
                FormCadastroPedidoDeVenda formCadastroPedidoDeVenda = new FormCadastroPedidoDeVenda(roteiro.PedidoVenda.Id);

                formCadastroPedidoDeVenda.Show();
            }
        }

        private void btnEditarPedido_Click(object sender, EventArgs e)
        {
            var roteiro = RetorneRoteiroSelecionado();

            if (roteiro != null)
            {
                FormCadastroPedidoDeVenda formCadastroPedidoDeVenda = new FormCadastroPedidoDeVenda(roteiro.PedidoVenda.Id);

                formCadastroPedidoDeVenda.Show();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void pesquiseFuncionarioPorId()
        {
            if (!string.IsNullOrEmpty(txtIdPessoa.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var vendedor = servicoPessoa.ConsulteFuncionarioAtivo(txtIdPessoa.Text.ToInt());

                PreenchaPessoa(vendedor, true);
                txtDataRoteiro.Text = txtDataRoteiro.Text.ToString();
            }
            else
            {
                PreenchaPessoa(null);
            }
        }

        public void SelecioneLancamento()
        {
            _roteiroSelecionado = null;

            if (_listaDeRoteiros != null && _listaDeRoteiros.Count > 0)
            {
                _roteiroSelecionado = new ServicoRoteiro().Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

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

        public void Pesquise()
        {
            this.Cursor = Cursors.WaitCursor;

            ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

            Pessoa pessoa = !string.IsNullOrEmpty(txtIdPessoa.Text) ? new Pessoa { Id = txtIdPessoa.Text.ToInt() } : null;

            DateTime? dataInicialPeriodo = txtDataRoteiro.Text.ToDateNullabel();
            DateTime? dataFinalPeriodo = txtDataConclusao.Text.ToDateNullabel();

            //_listaDeRoteiros = servicoRoteiro.ConsulteLista(pessoa, periodo, statusRoteiro, tipoDataFiltrar, dataInicialPeriodo, dataFinalPeriodo, txtNumeroPedidoVendas.Text.ToInt());

            preencherGrid();

            //txtQuantidade.Text = _listaDeRoteiros.Count.ToString();

            btnExcluirRoteiro.Visible = false;

            this.Cursor = Cursors.Default;
        }

        private void preencherGrid()
        {
            List<RoteiroAuxiliar> listaDeRoteirosAuxiliar = new List<RoteiroAuxiliar>();

            int totalConcluido = 0;
            int totalEmRota = 0;
            int totalInconcluso = 0;
            int totalEmAgenda = 0;

            if (_listaDeRoteiros == null || _listaDeRoteiros.Count == 0)
            {
                gcRoteiros.DataSource = listaDeRoteirosAuxiliar;
                gcRoteiros.RefreshDataSource();
                return;
            }

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
                    RoteiroAuxiliar.DataConclusao = roteiro.DataConclusao != null ? roteiro.DataConclusao.GetValueOrDefault().ToString("dd/MM/yyyy") : string.Empty;

                    RoteiroAuxiliar.Status = roteiro.Status.Descricao();

                    RoteiroAuxiliar.Observacao = roteiro.DetalheServico;
                    RoteiroAuxiliar.LigarFone = roteiro.Observacao;
                    listaDeRoteirosAuxiliar.Add(RoteiroAuxiliar);

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
                
               

                gcRoteiros.DataSource = listaDeRoteirosAuxiliar;
                gcRoteiros.RefreshDataSource();
            }
        }

        private void PreenchaOStatus()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusRoteiro>();
            lista.Insert(0, null);
        }

        private void PreenchaPessoa(Pessoa pessoa, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (pessoa != null)
            {
                txtIdPessoa.Text = pessoa.Id.ToString();
                txtNomePessoa.Text = pessoa.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Parceiro nao encontrado!", "Parceiro não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdPessoa.Focus();
                }

                txtIdPessoa.Text = string.Empty;
                txtNomePessoa.Text = string.Empty;
            }
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
            txtDataRoteiro.DateTime = primeiroDiaMes;
            txtDataConclusao.DateTime = addDias;
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

            Pesquise();

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

            Pesquise();
        }

        private void LimpeFormulario()
        {
            _listaDeRoteiros.Clear();
            gcRoteiros.DataSource = null;

            PreenchaPessoa(null);

            txtDataRoteiro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtDataConclusao.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            txtIdPessoa.Focus();
        }

        private void AdicionarParaRota(int pedidoId)
        {
            if (!string.IsNullOrEmpty(txtDataConclusao.Text)) return;

            //Consultar se o pedido está agendado
            var agenda = new ServicoRoteiro().ConsultePorPedido(pedidoId);

            if (agenda == null)
            {
                MessageBox.Show("Agendamento não encontrado para o pedido: " + pedidoId +
                                " Pode não ter sido feito o agendamento. Por favor, verifique!",
                                "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (agenda.Status == EnumStatusRoteiro.CONCLUIDO)
                {
                    MessageBox.Show("Roteiro Concluído para o pedido: " + pedidoId,
                                "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (agenda.Status == EnumStatusRoteiro.EMROTA || agenda.Status == EnumStatusRoteiro.INCONCLUSO)
                {
                    MessageBox.Show("Roteiro Criado para o pedido: " + pedidoId,
                                "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (VerifiqueSePedidoEstahNaLista(pedidoId))
            {
                MessageBox.Show("Este pedido consta na lista. Por favor, informe outro para continuar. ",
                                "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _listaDeRoteiros = _listaDeRoteiros ?? new List<Roteiro>();

            //Só vai adicionar se não existir na lista
            if (!_listaDeRoteiros.Exists(x => x.Id == agenda.Id))
            {
                _listaDeRoteiros.Add(agenda);
            }

            preencherGrid();
            txtNumeroPedidoVendas.Text = string.Empty;
        }

        private bool VerifiqueSePedidoEstahNaLista(int IdPedido)
        {
            if (_listaDeRoteiros == null) return false;

            return _listaDeRoteiros.Exists(x => x.PedidoVenda.Id == IdPedido);
        }

        private List<Roteiro> LimpeSelecionados(List<Roteiro> listaSelecionados)
        {
            txtCodigoRoteiro.Text = string.Empty;
            txtIdPessoa.Text = string.Empty;
            txtNomePessoa.Text = string.Empty;

            List<Roteiro> listaRetorno = new List<Roteiro>();

            foreach (var item in _listaDeRoteiros)
            {
                var naoTemNaLista = !listaSelecionados.Exists(x => x.Id == item.Id);

                if (naoTemNaLista)
                {
                    item.Status = EnumStatusRoteiro.EMAGENDA;
                    listaRetorno.Add(item);
                }
            }

            return listaRetorno;

        }

        private void EstorneAgendasEmRota(int numeroRoteirizacao)
        {
            ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

            var roteirosDoBanco = servicoRoteiro.ConsulteListaPorRoteirizacao(numeroRoteirizacao);

            ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

            foreach (var itemDoBanco in roteirosDoBanco)
            {
                var naoTem = !_listaDeRoteiros.Exists(x => x.Id == itemDoBanco.Id);

                if (naoTem)
                {
                    itemDoBanco.RoteirizacaoId = null;
                    itemDoBanco.Status = EnumStatusRoteiro.EMAGENDA;
                    servicoRoteiro.Atualize(itemDoBanco);

                    var pedidoAtualizar = servicoPedido.Consulte(itemDoBanco.PedidoVenda.Id);

                    pedidoAtualizar.StatusRoteiro = EnumStatusRoteiro.EMAGENDA;
                    
                    servicoPedido.Atualize(pedidoAtualizar);
                }
            }
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

            public string DataConclusao { get; set; }

            public string Status { get; set; }

            public string Periodo { get; set; }

            public string LigarFone { get; set; }

            public string Observacao { get; set; }
        }

        #endregion

        private void btnImprimirRelatorio_Click(object sender, EventArgs e)
        {
            bool infopedido = _parametros.ParametrosVenda.ExibirInfoPedido;

            if (infopedido == false)
            {
                gcRoteiros.ExibaRelatorio();
            }
            else
            {
                RelatorioRoteiros relatorio = new RelatorioRoteiros(txtCodigoRoteiro.Text.ToInt(), txtNomePessoa.Text.ToString(), txtDataRoteiro.Text);
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
            //gcRoteiros.view
            

        }

        private void btnSalvarRoteiro_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDataConclusao.Text)) return;

            if (txtIdPessoa.Text == string.Empty)
            {
                MessageBox.Show("Informe o Funcionário para continuar.",
                                "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (gridViewRoteiro.SelectedRowsCount == 0)
            {
                MessageBox.Show("Selecione Pelo menos um item para Salvar.",
                                "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtDataRoteiro.Text == string.Empty)
            {
                MessageBox.Show("Informe a Data do Roteiro para continuar. ",
                                "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Action actionSalvar = () =>
            {
                Roteirizacao roteirizacao = new Roteirizacao();

                roteirizacao.Status = EnumStatusRoteiro.EMROTA;
                roteirizacao.DataCriacao = txtDataRoteiro.Text.ToDate();
                roteirizacao.PessoaFuncionario = new Pessoa { Id = txtIdPessoa.Text.ToInt() };
                roteirizacao.Usuario = Sessao.PessoaLogada;
                roteirizacao.Id = txtCodigoRoteiro.Text.ToInt();

                ServicoRoteirizacao servicoRoteirizacao = new ServicoRoteirizacao();

                servicoRoteirizacao.SalveRoteirizacaoEAtualizeAgendaEPedido(roteirizacao, _listaDeRoteiros);

                _listaDeRoteiros = LimpeSelecionados(_listaDeRoteiros);

                preencherGrid();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }       

        private void btnPesquisaPedidoVendas_Click(object sender, EventArgs e)
        {
            FormPesquisaPedidoDeVenda formPesquisaPedidoDeVenda = new FormPesquisaPedidoDeVenda();

            var pedidoDeVenda = formPesquisaPedidoDeVenda.ExibaPesquisaDePedidosDeVenda();

            if (pedidoDeVenda != null)
            {
                txtNumeroPedidoVendas.Text = pedidoDeVenda.Id.ToString();
            }
        }

        private void btnAddAgendaDoDia_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDataConclusao.Text)) return;

            FormAgendaDoDia formAgendaDoDia = new FormAgendaDoDia();

            var roteirosAAdicionar = formAgendaDoDia.retorneAgendas(txtDataRoteiro.Text.ToDate());

            if(roteirosAAdicionar == null) return;

            _listaDeRoteiros = _listaDeRoteiros ?? new List<Roteiro>();

            foreach (var item in roteirosAAdicionar)
            {
                //Só vai adicionar na lista se não tiver...
                if(!_listaDeRoteiros.Exists(x=>x.Id == item.Id))
                    _listaDeRoteiros.Add(item);
            }

            preencherGrid();
        }

        private void btnAdicionarRoteiro_Click(object sender, EventArgs e)
        {
            AdicionarParaRota(txtNumeroPedidoVendas.Text.ToInt());
        }

        private void gcRoteiros_Click(object sender, EventArgs e)
        {
            ExibicaoBotoesPedidoExcluir();
        }

        private void gcRoteiros_KeyUp(object sender, KeyEventArgs e)
        {
            ExibicaoBotoesPedidoExcluir();
        }

        private void ExibicaoBotoesPedidoExcluir()
        {
            if (gcRoteiros.ContainsFocus)
            {
                var roteiros = RetorneRoteiroSelecionado();

                RoteiroId = roteiros.PedidoVenda.Id;
                IdRoteiro = 0;

                if (roteiros != null)
                {
                    if (roteiros.Status == EnumStatusRoteiro.CONCLUIDO)
                    {   
                        btnExcluirRoteiro.Visible = false;                      
                    }
                    else
                    {   
                        btnExcluirRoteiro.Visible = true;
                    }

                    if (_editarPedidoLiberado)
                    {
                        btnEditarPedido.Visible = true;
                    }
                }
            }
        }

        private void btnDetalhes_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            _listaDeRoteiros.CarregueLazyLoad();

            

            RelatorioListaRoteiros relatorio = new RelatorioListaRoteiros(txtDataRoteiro.Text.ToDate(), txtDataRoteiro.Text.ToDate(), RoteiroId, IdRoteiro);
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

        private void txtIdPessoa_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
    
}
