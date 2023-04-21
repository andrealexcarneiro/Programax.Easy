using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Infraestrutura.Negocio.Utils;
using System.Drawing;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Servico.Vendas.RoteiroServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;

namespace Programax.Easy.View.Telas.Vendas.Roteiros
{
    public partial class FormRoteirizacaoPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Roteirizacao> _listaDeRoteiros;   
        private Roteirizacao _roteiroSelecionado;
        protected bool _cadastroLiberado=true;
        protected bool _editarPedidoLiberado;

        string numeroDocumentoValidacao;

        #endregion

        #region " CONSTRUTOR "

        public FormRoteirizacaoPesquisa()
        {
            InitializeComponent();

            PreenchaOStatus();
            PreenchaCboDataFiltrar();
                            
            PreenchaPrimeiroEUltimoDiaMes();

            _editarPedidoLiberado = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.PEDIDODEVENDAS).Alterar;

            this.ActiveControl = txtIdPessoa;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void FormRoteiroPesquisa_Load(object sender, EventArgs e)
        {
            if (_cadastroLiberado)
            {
                btnNovo.Visible = true;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            FormCadastroRoteirizacao formCadastroRoteiro = new FormCadastroRoteirizacao(0, DateTime.Now, null, null);
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

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var pessoa = formPessoaPesquisa.PesquisePessoaFuncionarioAtiva();

            if (pessoa != null)
            {
                PreenchaPessoa(pessoa);
            }
        }

        private void cboDataFiltrar_EditValueChanged(object sender, EventArgs e)
        {
            if ((EnumDataFiltrarRoteiro?)cboDataFiltrar.EditValue == null)
            {
                txtDataInicialPeriodo.Enabled = false;
                txtDataFinalPeriodo.Enabled = false;

                txtDataInicialPeriodo.Text = string.Empty;
                txtDataFinalPeriodo.Text = string.Empty;
            }
            else
            {
                txtDataInicialPeriodo.Enabled = true;
                txtDataFinalPeriodo.Enabled = true;
            }
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
                ServicoRoteirizacao servicoRoteirizacao = new ServicoRoteirizacao();

                foreach (var item in listaParaExcluir)
                {

                    servicoRoteirizacao.ExcluirRoteirizacaoEAtualizarAgendaEPedido(item.Id);

                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                throw ex;
            }

            this.Cursor = Cursors.Default;

            MessageBox.Show("Roteiro(s) Excluído(s) com sucesso!", "Conclusão de Roteiro(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Pesquise();
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

        private void gcRoteiros_Click(object sender, EventArgs e)
        {
            ExibicaoBotoesConcluirEstornarExcluir();
        }

        private void gcRoteiros_KeyUp(object sender, KeyEventArgs e)
        {
            ExibicaoBotoesConcluirEstornarExcluir();
        }

        private void gcRoteiros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelecioneLancamento();
            }
        }

        private void gcRoteiros_DoubleClick(object sender, EventArgs e)
        {
            SelecioneLancamento();

            if (_roteiroSelecionado == null) return;

            if (_roteiroSelecionado.Status != EnumStatusRoteiro.CONCLUIDO)
            {
                FormCadastroRoteirizacao formCadastroRoteiros = new FormCadastroRoteirizacao(_roteiroSelecionado.Id, _roteiroSelecionado.DataCriacao,
                                                                                  _roteiroSelecionado.DataConclusao, _roteiroSelecionado.PessoaFuncionario);
                formCadastroRoteiros.ShowDialog();
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
                _roteiroSelecionado = new ServicoRoteirizacao().Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

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
       
        private bool valideSeListaRoteiroEstahConcluida(List<Roteirizacao> listaRoteiro)
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

        private bool valideSeListaRoteirosEstahEmRota(List<Roteirizacao> listaRoteiro)
        {
            numeroDocumentoValidacao = string.Empty;

            for (int i = 0; i < listaRoteiro.Count; i++)
            {
                if (listaRoteiro[i].Status == EnumStatusRoteiro.EMROTA)
                    numeroDocumentoValidacao += listaRoteiro[i].Id + "; ";
            }

            if (numeroDocumentoValidacao != string.Empty)
            {               
                return false;
            }

            return true;
        }
        
        public void Pesquise()
        {
            this.Cursor = Cursors.WaitCursor;

            ServicoRoteirizacao servicoRoteirizacao = new ServicoRoteirizacao();

            Pessoa pessoa = !string.IsNullOrEmpty(txtIdPessoa.Text) ? new Pessoa { Id = txtIdPessoa.Text.ToInt() } : null;

            EnumStatusRoteiro? statusRoteiro = (EnumStatusRoteiro?)cboStatus.EditValue;

            EnumDataFiltrarRoteiro? tipoDataFiltrar = (EnumDataFiltrarRoteiro?)cboDataFiltrar.EditValue;

            DateTime? dataInicialPeriodo = txtDataInicialPeriodo.Text.ToDateNullabel();
            DateTime? dataFinalPeriodo = txtDataFinalPeriodo.Text.ToDateNullabel();

            if (string.IsNullOrEmpty(txtNumeroPedidoVendas.Text) && string.IsNullOrEmpty(txtCodigoRoteiro.Text))
            {
                _listaDeRoteiros = servicoRoteirizacao.ConsulteLista(pessoa, statusRoteiro, tipoDataFiltrar, dataInicialPeriodo, dataFinalPeriodo);
            }                
            else
            {
                if (!string.IsNullOrEmpty(txtNumeroPedidoVendas.Text))
                {
                    var codigoRoteirizacao = new ServicoRoteiro().ConsultePorPedido(txtNumeroPedidoVendas.Text.ToInt());

                    if (codigoRoteirizacao == null)
                    {
                        MessageBox.Show("O pedido numero: " + txtNumeroPedidoVendas.Text + " Não foi encontrado Em Agenda!", "Pesquisa de Roteiro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    _listaDeRoteiros = servicoRoteirizacao.ConsulteListaCodigoRoteiro(codigoRoteirizacao.RoteirizacaoId);


                    if (_listaDeRoteiros.Count == 0)
                    {
                        MessageBox.Show("O pedido numero: " + txtNumeroPedidoVendas.Text + " Não foi encontrado Em Rota!", "Pesquisa de Roteiro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                }                   
                else if (!string.IsNullOrEmpty(txtCodigoRoteiro.Text))
                {
                    _listaDeRoteiros = servicoRoteirizacao.ConsulteListaCodigoRoteiro(txtCodigoRoteiro.Text.ToInt());

                    if (_listaDeRoteiros.Count == 0)
                    {
                        MessageBox.Show("O Roteiro de número: " + txtCodigoRoteiro.Text + " Não foi encontrado!", "Pesquisa de Roteiro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }   
            }
               
            preencherGrid();

            txtQuantidade.Text = _listaDeRoteiros.Count.ToString();

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

            for (int i = 0; i < _listaDeRoteiros.Count; i++)
            {
                var roteiro = _listaDeRoteiros[i];

                RoteiroAuxiliar RoteiroAuxiliar = new RoteiroAuxiliar();

                RoteiroAuxiliar.Id = roteiro.Id;
                RoteiroAuxiliar.Funcionario = roteiro.PessoaFuncionario != null ? roteiro.PessoaFuncionario.Id + " - " + roteiro.PessoaFuncionario.DadosGerais.Razao : string.Empty;

                RoteiroAuxiliar.Usuario = roteiro.Usuario.Id + "-" + roteiro.Usuario.DadosGerais.Razao;

                RoteiroAuxiliar.DataCriacao = roteiro.DataCriacao.ToString("dd/MM/yyyy");
                RoteiroAuxiliar.DataConclusao = roteiro.DataConclusao != null ? roteiro.DataConclusao.GetValueOrDefault().ToString("dd/MM/yyyy") : string.Empty;

                RoteiroAuxiliar.Status = roteiro.Status.Descricao();

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

                txtTotalConcluido.Text = totalConcluido.ToString();
                txtTotalEmRota.Text = totalEmRota.ToString();
                txtInconclusos.Text = totalInconcluso.ToString();
               
                gcRoteiros.DataSource = listaDeRoteirosAuxiliar;
                gcRoteiros.RefreshDataSource();
            }

            if(_listaDeRoteiros.Count==0)
                gcRoteiros.DataSource = listaDeRoteirosAuxiliar;
                gcRoteiros.RefreshDataSource();

        }

        private void PreenchaOStatus()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusRoteiro>();
            lista.Insert(0, null);

            lista.RemoveAt(4);

            cboStatus.Properties.DataSource = lista;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";
        }

        private void PreenchaCboDataFiltrar()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumDataFiltrarRoteiro>();

            lista.Insert(0, null);

            cboDataFiltrar.Properties.DataSource = lista;
            cboDataFiltrar.Properties.ValueMember = "Valor";
            cboDataFiltrar.Properties.DisplayMember = "Descricao";

            cboDataFiltrar.EditValue = EnumDataFiltrarRoteiro.ELABORACAO;
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
        
        protected virtual Roteirizacao RetorneRoteiroSelecionado()
        {
            Roteirizacao roteiro = null;

            if (_listaDeRoteiros != null && _listaDeRoteiros.Count > 0)
            {
                ServicoRoteirizacao servicoRoteiro = new ServicoRoteirizacao();

                roteiro = servicoRoteiro.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            return roteiro;
        }
       
        private void PreenchaPrimeiroEUltimoDiaMes()
        {   
            var primeiroDiaMes = DateTime.Now;
            var addDias = DateTime.Now;
            txtDataInicialPeriodo.DateTime = primeiroDiaMes;
            txtDataFinalPeriodo.DateTime = addDias;

            cboStatus.EditValue = EnumStatusRoteiro.EMROTA;            
        }

        private List<Roteirizacao> carregaListaParaConcluirRoteiro()
        {
            if (gridViewRoteiro.SelectedRowsCount == 0) return null;

            Roteirizacao itemParaEmissao = new Roteirizacao();
            List<Roteirizacao> listaItemParaEmissao = new List<Roteirizacao>();

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
            if (gridViewRoteiro.SelectedRowsCount == 0) return;

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
                new ServicoRoteirizacao().ConcluaRoteirizacaoEAtualizePedidoEAgenda(listaParaQuitacaoDeTitulos);
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
                ServicoRoteirizacao servicoRoteirizacao = new ServicoRoteirizacao();

                servicoRoteirizacao.EstorneConclusaoDeRoteirizacao(listaParaEstorno);
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

        private void ExibicaoBotoesConcluirEstornarExcluir()
        {
            if (gcRoteiros.ContainsFocus)
            {
                var roteiros = RetorneRoteiroSelecionado();

                if (roteiros != null)
                {
                    if (roteiros.Status == EnumStatusRoteiro.CONCLUIDO)
                    {
                        btnConcluirRoteiro.Visible = true;
                        btnConcluirRoteiro.Text = "Estornar";
                        btnConcluirRoteiro.Image = Properties.Resources.icone_estornar;
                        btnExcluirRoteiro.Visible = false;
                    }
                    else
                    {
                        btnConcluirRoteiro.Visible = true;
                        btnConcluirRoteiro.Text = "Concluir";
                        btnConcluirRoteiro.Image = Properties.Resources.icone_baixar;
                        btnExcluirRoteiro.Visible = true;
                    }

                    if(_editarPedidoLiberado)
                    {                        
                        btnEditar.Visible = true;                        
                    }
                }
            }
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class RoteiroAuxiliar
        {
            public Image Imagem { get; set; }

            public int Id { get; set; }

            public string DataCriacao { get; set; }

            public string Funcionario { get; set; }

            public string Pedido { get; set; }

            public string Usuario { get; set; }

            public string DataConclusao { get; set; }

            public string Status { get; set; }
        }

        #endregion

        private void btnImprimirRelatorio_Click(object sender, EventArgs e)
        {
            //gcRoteiros.view
            gcRoteiros.ExibaRelatorio();
        }

        private void btnConcluirRoteiro_Click(object sender, EventArgs e)
        {
            if(btnConcluirRoteiro.Text.RemovaEspacosEmBrancoDoInicioEFim() == "Concluir")
            {
                ConcluirRoteiro();
            }
            else if (btnConcluirRoteiro.Text.RemovaEspacosEmBrancoDoInicioEFim() == "Estornar")
            {
                EstornarTituloDiretoDaPesquisa();
            }
            else if(btnConcluirRoteiro.Text.RemovaEspacosEmBrancoDoInicioEFim() == "Selecionar")
            {
                SelecioneLancamento();
            }   
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

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            var roteiroSelecionado = RetorneRoteiroSelecionado();

            FormHistoricoRoteiro formHistorico = new FormHistoricoRoteiro(roteiroSelecionado, Sessao.PessoaLogada);

            formHistorico.Show();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var roteiro = RetorneRoteiroSelecionado();

            if (roteiro != null)
            {
                FormCadastroRoteirizacao formCadastroRoteiros = new FormCadastroRoteirizacao(roteiro.Id, roteiro.DataCriacao,
                                                                                                roteiro.DataConclusao, roteiro.PessoaFuncionario);
                formCadastroRoteiros.ShowDialog();

                Pesquise();
            }
        }
    }
    
}
