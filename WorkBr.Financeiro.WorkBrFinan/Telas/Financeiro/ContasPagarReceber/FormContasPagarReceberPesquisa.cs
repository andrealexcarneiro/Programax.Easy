using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Servico.Financeiro.PlanoContasServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.View.Telas.Financeiro.PlanosDeContas;
using Programax.Infraestrutura.Negocio.Utils;
using System.Drawing;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Report.RelatoriosDevExpress.Financeiro;
using BoletoNet.Arquivo;
using Programax.Easy.View.Telas.Financeiro.Cheques;
using Programax.Easy.Servico.Financeiro.ChequeServ;
using Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoBancoServ;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.MovimentacaoBancoServ;

namespace Programax.Easy.View.Telas.Financeiro.ContasPagarReceber
{
    public partial class FormContasPagarReceberPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<ContaPagarReceber> _listaDeContasPagarReceber;   
        private ContaPagarReceber _contasPagarReceberSelecionada;
        private PlanoDeContas _planoDeContas;
        
        private Parametros _parametros;
        
        protected bool _manutencaoLiberada;
        protected bool _prorrogacaoLiberada;
        protected bool _cadastroLiberado;
        private bool _EhMovimentacao = false;
        
        string numeroDocumentoValidacao;

        #endregion

        #region " CONSTRUTOR "

        public FormContasPagarReceberPesquisa(bool EhMovimentacao=false)
        {
            InitializeComponent();

            PreenchaOStatus();
            PreenchaCboDataFiltrar();
            PreenchaCboFormaPagamento();

            PreenchaPrimeiroEUltimoDiaMes();
                       
            _parametros = new ServicoParametros().ConsulteParametros();
                        
            //Pesquise();

            this.ActiveControl = txtIdPessoa;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void FormContasPagarReceberPesquisa_Load(object sender, EventArgs e)
        {
            if (_cadastroLiberado && !_EhMovimentacao)
            {
                btnNovo.Visible = true;
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            var formNovaContasPagarReceber = RetorneFormCadastroContasPagarReceber();           
            formNovaContasPagarReceber.Show();
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
            if (!string.IsNullOrEmpty(txtIdPessoa.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var vendedor = servicoPessoa.ConsultePessoaAtiva(txtIdPessoa.Text.ToInt());

                PreenchaPessoa(vendedor, true);
            }
            else
            {
                PreenchaPessoa(null);
            }
        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var pessoa = formPessoaPesquisa.PesquisePessoa();

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
                PreenchaPlanoDeContas(planoDeContas);
            }
        }

        private void txtNumeroPlanoDeContas_Leave(object sender, EventArgs e)
        {
            ServicoPlanoDeContas servicoPlanoDeContas = new ServicoPlanoDeContas();

            var planoDeContas = servicoPlanoDeContas.ConsultePlanoDeContasAtivoPeloNumero(txtNumeroPlanoDeContas.Text);

            PreenchaPlanoDeContas(planoDeContas);
        }

        private void cboDataFiltrar_EditValueChanged(object sender, EventArgs e)
        {
            if ((EnumDataFiltrarContasPagarReceber?)cboDataFiltrar.EditValue == null)
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

        private void btnManutencaoTitulo_Click(object sender, EventArgs e)
        {
            var formManutencaoTitulo = RetorneFormManutencaoContaPagarReceber();

            var tituloSelecionado = RetorneTituloSelecionado();

            formManutencaoTitulo.EditarContaPagarReceber(tituloSelecionado);
        }

        private void btnProrrogarTitulo_Click(object sender, EventArgs e)
        {
            var formProrrogacaoTitulo = RetorneFormProrrogacaoContaPagarReceber();

            var tituloSelecionado = RetorneTituloSelecionado();

            formProrrogacaoTitulo.EditarContaPagarReceber(tituloSelecionado);
        }

       

        private void txtIdPessoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void cboStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void gcContasPagarReceber_Click(object sender, EventArgs e)
        {
            ExibicaoBotoesManutencaoEProrrogacao();
        }

        private void gcContasPagarReceber_KeyUp(object sender, KeyEventArgs e)
        {
            ExibicaoBotoesManutencaoEProrrogacao();
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

        public void SelecioneLancamento()
        {
            if (!_EhMovimentacao) return;

            _contasPagarReceberSelecionada = null;

            if (_listaDeContasPagarReceber != null && _listaDeContasPagarReceber.Count > 0)
            {                
                _contasPagarReceberSelecionada = new ServicoContasPagarReceber().Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

                if (_contasPagarReceberSelecionada != null)
                {
                    if(_contasPagarReceberSelecionada.Status != EnumStatusContaPagarReceber.ABERTO & _contasPagarReceberSelecionada.Status != EnumStatusContaPagarReceber.ABERTOPRORROGADO 
                        & _contasPagarReceberSelecionada.Status != EnumStatusContaPagarReceber.ABERTOPROTESTADO)
                    {
                        MessageBox.Show("O Lançamento deve estar com o Status: Aberto!","Pagar/Receber",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return;
                    }

                    if (_contasPagarReceberSelecionada.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO)
                    {
                        MessageBox.Show("O Lançamento deve ter a Forma de Pagamento diferente de Dinheiro.","Pagar/Receber", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            this.Close();           
        }

        public ContaPagarReceber BuscarConciliacaoPagarReceber(Pessoa parceiro, DateTime dataBusca)
        {
            PesquisePagarOuReceberMovimentacoes(parceiro, dataBusca);

            this.ShowDialog();

            return _contasPagarReceberSelecionada;
        }

        public void ConsulteItemContaPagarReber(int Id)
        {
            ServicoContasPagarReceber servicoContaPagarReceber = RetorneServicoContasPagarOuReceber();

            _listaDeContasPagarReceber= new List<ContaPagarReceber>();

            _listaDeContasPagarReceber.Add(servicoContaPagarReceber.Consulte(Id));

            preencherGrid();

            btnManutencaoTitulo.Visible = false;
            btnProrrogarTitulo.Visible = false;
            btnBoleto.Visible = false;
            btnCarne.Visible = false;

            this.ShowDialog();
        }

        private List<ContaPagarReceber> carregaListaParaEmissaoBoleto()
        {
            if (gridViewContasPagarReceber.SelectedRowsCount == 0) return null;

            ContaPagarReceber itemParaEmissao = new ContaPagarReceber();
            List<ContaPagarReceber> listaItemParaEmissao = new List<ContaPagarReceber>();
            
            foreach (var item in gridViewContasPagarReceber.GetSelectedRows())
            {
                var lancamento = colunaId.View.GetRowCellValue(item, colunaId);
               
                itemParaEmissao = _listaDeContasPagarReceber.FirstOrDefault(x => x.Id == lancamento.ToInt());
                
                if (itemParaEmissao != null)
                    listaItemParaEmissao.Add(itemParaEmissao);
            }
            
            return listaItemParaEmissao;
        }

        private bool valideListaContasReceberEhBoleto(List<ContaPagarReceber> listaPagarReceber)
        {
            numeroDocumentoValidacao = string.Empty;

            for (int i = 0; i < listaPagarReceber.Count; i++)
            {   
                if (listaPagarReceber[i].FormaPagamento.TipoFormaPagamento != EnumTipoFormaPagamento.BOLETOBANCARIO)
                    numeroDocumentoValidacao += listaPagarReceber[i].NumeroDocumento + "; ";
            }

            if (numeroDocumentoValidacao != string.Empty)
            {
                MessageBoxAkil.Show("Os documento(s) de numero: " + numeroDocumentoValidacao + "não possue(m) a forma de pagamento correta para emissão de Boleto. Altere para BOLETO BANCARIO.","Validação de Emissão de Boleto", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private bool valideSeListaContasPagarReceberEhQuitado(List<ContaPagarReceber> listaPagarReceber)
        {
            numeroDocumentoValidacao = string.Empty;

            for (int i = 0; i < listaPagarReceber.Count; i++)
            {
                if (listaPagarReceber[i].Status == EnumStatusContaPagarReceber.QUITADO ||
                    listaPagarReceber[i].Status == EnumStatusContaPagarReceber.CONCILIADOQUITADO ||
                    listaPagarReceber[i].Status == EnumStatusContaPagarReceber.INATIVO ||
                    listaPagarReceber[i].Status == EnumStatusContaPagarReceber.CANCELADO)
                    numeroDocumentoValidacao += listaPagarReceber[i].NumeroDocumento + "; ";
            }

            if (numeroDocumentoValidacao != string.Empty)
            {
                MessageBox.Show("Os documento(s) de numero: " + numeroDocumentoValidacao + "Pode(m) estar Quitado(s), Inativo(s) ou Cancelado(s). Somente títulos abertos pode(m) ser Quitado(s).", "Quitação de Título(s)", MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private bool valideSeListaContasPagarReceberEhParaDinheiro(List<ContaPagarReceber> listaPagarReceber)
        {
            numeroDocumentoValidacao = string.Empty;

            for (int i = 0; i < listaPagarReceber.Count; i++)
            {
                if ((EnumTipoFormaPagamento)listaPagarReceber[i].FormaPagamento.Id == EnumTipoFormaPagamento.CREDIARIOPROPRIO ||
                    (EnumTipoFormaPagamento)listaPagarReceber[i].FormaPagamento.Id == EnumTipoFormaPagamento.DUPLICATA)
                    numeroDocumentoValidacao += listaPagarReceber[i].NumeroDocumento + "; ";
            }

            if (numeroDocumentoValidacao != string.Empty)
            {
                if (MessageBox.Show("O(s) documento(s) de número: " + numeroDocumentoValidacao + " será(ão) gerado(s) como <DINHEIRO> por <PADRÃO> e entrará no <CAIXA>.\n\nDeseja continuar ? ", "Baixar Título", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    return false;
                else
                    return true;
            }

            return true;
        }

        private bool valideSeListaContasPagarReceberEstaoAbertos(List<ContaPagarReceber> listaPagarReceber)
        {
            numeroDocumentoValidacao = string.Empty;

            for (int i = 0; i < listaPagarReceber.Count; i++)
            {
                if (listaPagarReceber[i].Status == EnumStatusContaPagarReceber.ABERTO ||
                    listaPagarReceber[i].Status == EnumStatusContaPagarReceber.ABERTOPRORROGADO ||
                    listaPagarReceber[i].Status == EnumStatusContaPagarReceber.ABERTOPROTESTADO ||
                    listaPagarReceber[i].Status == EnumStatusContaPagarReceber.INATIVO ||
                    listaPagarReceber[i].Status == EnumStatusContaPagarReceber.CANCELADO)
                    numeroDocumentoValidacao += listaPagarReceber[i].NumeroDocumento + "; ";
            }

            if (numeroDocumentoValidacao != string.Empty)
            {
                MessageBox.Show("O(s) documento(s) de numero: " + numeroDocumentoValidacao + "Pode(m) estar Aberto(s), Inativo(s) ou Cancelado(s). Somente título(s) Quitado(s) pode(m) ser Estornados(s).", "Estorno de Título(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private bool valideSeListaContasPagarReceberTemOutrosPagamentos(List<ContaPagarReceber> listaPagarReceber)
        {
            numeroDocumentoValidacao = string.Empty;

            foreach (var item in listaPagarReceber)
            {
                if (item.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER)
                {
                    var contaPagarReceber = new ServicoContasReceber().Consulte(item.Id);


                    if (contaPagarReceber.ListaContasPagarReceberParcial.Count(x => x.EstahEstornado == false) > 1)
                    {
                        numeroDocumentoValidacao += item.NumeroDocumento + "; ";
                    }
                }
                else
                {
                    var contaPagar = new ServicoContasPagar().Consulte(item.Id);
                    
                    if (contaPagar.ListaContasPagarReceberParcial.Count(x => x.EstahEstornado == false) > 1)
                    {
                        numeroDocumentoValidacao += item.NumeroDocumento + "; ";
                    }
                }

            }

            if (numeroDocumentoValidacao != string.Empty)
            {
                MessageBox.Show("O(s) documento(s) de numero: " + numeroDocumentoValidacao + "Possue(m) mais de um pagamento aberto, portanto, utilize a Manutenção de Títulos para realizar o estorno.", "Estorno de Título(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        public void Pesquise()
        {
            this.Cursor = Cursors.WaitCursor;

            ServicoContasPagarReceber servicoContaPagarReceber = RetorneServicoContasPagarOuReceber();

            Pessoa pessoa = !string.IsNullOrEmpty(txtIdPessoa.Text) ? new Pessoa { Id = txtIdPessoa.Text.ToInt() } : null;

            EnumStatusContaPagarReceber? statusContaPagarReceber = (EnumStatusContaPagarReceber?)cboStatus.EditValue;

            FormaPagamento formaPagamento = cboFormaDePagamento.EditValue != null ? new FormaPagamento { Id = cboFormaDePagamento.EditValue.ToInt() } : null;

            EnumDataFiltrarContasPagarReceber? tipoDataFiltrar = (EnumDataFiltrarContasPagarReceber?)cboDataFiltrar.EditValue;
            
            DateTime? dataInicialPeriodo = txtDataInicialPeriodo.Text.ToDateNullabel();
            DateTime? dataFinalPeriodo = txtDataFinalPeriodo.Text.ToDateNullabel();

            double valor = txtValor.Text != string.Empty ? txtValor.Text.ToDouble() : 0; 

            _listaDeContasPagarReceber = servicoContaPagarReceber.ConsulteLista(pessoa, EnumTipoOperacaoContasPagarReceber.RECEBER, statusContaPagarReceber, 
                                        formaPagamento, _planoDeContas, tipoDataFiltrar, dataInicialPeriodo, dataFinalPeriodo, valor);

            preencherGrid();

            txtQuantidade.Text = _listaDeContasPagarReceber.Count.ToString();

            btnManutencaoTitulo.Visible = false;
            btnProrrogarTitulo.Visible = false;
            btnBoleto.Visible = false;
            btnCarne.Visible = false;

            this.Cursor = Cursors.Default;
        }

        private void PesquisePagarOuReceberMovimentacoes(Pessoa Parceiro, DateTime dataBusca, bool EhMovimentacao=true)
        {
            _EhMovimentacao = EhMovimentacao;

            this.Cursor = Cursors.WaitCursor;

            ServicoContasPagarReceber servicoContaPagarReceber = RetorneServicoContasPagarOuReceber();

            Pessoa pessoa = Parceiro;

            EnumStatusContaPagarReceber? statusContaPagarReceber = (EnumStatusContaPagarReceber?)cboStatus.EditValue;

            FormaPagamento formaPagamento = cboFormaDePagamento.EditValue != null ? new FormaPagamento { Id = cboFormaDePagamento.EditValue.ToInt() } : null;

            EnumDataFiltrarContasPagarReceber? tipoDataFiltrar = (EnumDataFiltrarContasPagarReceber?)cboDataFiltrar.EditValue;

            DateTime? dataInicialPeriodo = dataBusca;
            DateTime? dataFinalPeriodo = dataBusca;
                        

            _listaDeContasPagarReceber = servicoContaPagarReceber.ConsulteLista(pessoa, null, statusContaPagarReceber, formaPagamento, _planoDeContas, tipoDataFiltrar, dataInicialPeriodo, dataFinalPeriodo);

            if (_listaDeContasPagarReceber.Count == 0)
            {
                tipoDataFiltrar = EnumDataFiltrarContasPagarReceber.PAGAMENTO;
                _listaDeContasPagarReceber = servicoContaPagarReceber.ConsulteLista(pessoa, null, statusContaPagarReceber, formaPagamento, _planoDeContas, tipoDataFiltrar, dataInicialPeriodo, dataFinalPeriodo);
            }

            if (_listaDeContasPagarReceber.Count == 0)
            {
                tipoDataFiltrar = EnumDataFiltrarContasPagarReceber.EMISSAO;
                _listaDeContasPagarReceber = servicoContaPagarReceber.ConsulteLista(pessoa, null, statusContaPagarReceber, formaPagamento, _planoDeContas, tipoDataFiltrar, dataInicialPeriodo, dataFinalPeriodo);
            }
           
            preencherGrid();

            txtQuantidade.Text = _listaDeContasPagarReceber.Count.ToString();
            txtIdPessoa.Text = Parceiro != null? Parceiro.Id.ToString() : string.Empty;
            txtNomePessoa.Text = Parceiro != null? new ServicoPessoa().Consulte(Parceiro.Id).DadosGerais.Razao : string.Empty;

            txtDataInicialPeriodo.Text = dataInicialPeriodo.ToString();
            txtDataFinalPeriodo.Text = dataFinalPeriodo.ToString();

            btnManutencaoTitulo.Visible = false;
            btnProrrogarTitulo.Visible = false;
            btnBoleto.Visible = false;
            btnCarne.Visible = false;
            btnImprimirRelatorio.Visible = false;
            btnNovo.Visible = false;
            btnQuitarTitulo.Visible = false;

            this.Cursor = Cursors.Default;
        }

        private void preencherGrid()
        {
            List<ContaPagarReceberAuxiliar> listaDeContasPagarReceberAuxiliar = new List<ContaPagarReceberAuxiliar>();

            double totalAbertoAVencer = 0;
            double totalAbertoVencido = 0;
            double totalGeralAPagar = 0;
            double totalQuitado = 0;

            for (int i = 0; i < _listaDeContasPagarReceber.Count; i++)
            {
                var contaPagarReceber = _listaDeContasPagarReceber[i];

                ContaPagarReceberAuxiliar contaPagarReceberAuxiliar = new ContaPagarReceberAuxiliar();

                contaPagarReceberAuxiliar.Id = contaPagarReceber.Id;
                contaPagarReceberAuxiliar.Pessoa = contaPagarReceber.Pessoa != null ? contaPagarReceber.Pessoa.Id + " - " + contaPagarReceber.Pessoa.DadosGerais.Razao : string.Empty;
                contaPagarReceberAuxiliar.NumeroDocumento = contaPagarReceber.NumeroDocumento;
                contaPagarReceberAuxiliar.FormaDePagamento = contaPagarReceber.FormaPagamento != null ? contaPagarReceber.FormaPagamento.Descricao : string.Empty;

                contaPagarReceberAuxiliar.ValorParcela = contaPagarReceber.ValorParcela;
                contaPagarReceberAuxiliar.Multa = contaPagarReceber.Multa;
                contaPagarReceberAuxiliar.Juros = contaPagarReceber.Juros;
                contaPagarReceberAuxiliar.Desconto = contaPagarReceber.Desconto;
                contaPagarReceber.ehCalculoMultaAutomatica = _parametros.ParametrosFinanceiro.MultaContasReceber > 0? true:false;
                contaPagarReceberAuxiliar.ValorTotal = contaPagarReceber.ValorTotal;
                contaPagarReceberAuxiliar.ValorPago = contaPagarReceber.ValorPago;
                contaPagarReceberAuxiliar.ValorAPagar = contaPagarReceber.ValorTotal - contaPagarReceber.ValorPago;

                contaPagarReceberAuxiliar.DataPagamento = contaPagarReceber.DataPagamento != null ? contaPagarReceber.DataPagamento.GetValueOrDefault().ToString("dd/MM/yyyy") : string.Empty;
                contaPagarReceberAuxiliar.DataVencimento = contaPagarReceber.DataVencimento != null ? contaPagarReceber.DataVencimento.GetValueOrDefault().ToString("dd/MM/yyyy") : string.Empty;
                contaPagarReceberAuxiliar.DataEmissao = contaPagarReceber.DataEmissao.ToString("dd/MM/yyyy");

                contaPagarReceberAuxiliar.Status = contaPagarReceber.Status.Descricao();

                listaDeContasPagarReceberAuxiliar.Add(contaPagarReceberAuxiliar);

                if (contaPagarReceber.Status == EnumStatusContaPagarReceber.ABERTOPRORROGADO ||
                    contaPagarReceber.Status == EnumStatusContaPagarReceber.ABERTO)
                {
                    if (contaPagarReceber.DataVencimento >= DateTime.Now.Date)
                    {
                        totalAbertoAVencer += contaPagarReceber.ValorTotal;
                        totalAbertoAVencer -= contaPagarReceber.ValorPago;

                        contaPagarReceberAuxiliar.Imagem = Properties.Resources.icone_verde;
                    }
                    else
                    {
                        totalAbertoVencido += contaPagarReceber.ValorTotal;
                        totalAbertoVencido -= contaPagarReceber.ValorPago;

                        contaPagarReceberAuxiliar.Imagem = Properties.Resources.icone_vermelho;
                    }

                    totalGeralAPagar += contaPagarReceber.ValorTotal;
                    totalGeralAPagar -= contaPagarReceber.ValorPago;
                    totalQuitado += contaPagarReceber.ValorPago;
                }
                else if (contaPagarReceber.Status == EnumStatusContaPagarReceber.QUITADO || contaPagarReceber.Status == EnumStatusContaPagarReceber.CONCILIADOQUITADO)
                {
                    contaPagarReceberAuxiliar.Imagem = Properties.Resources.icone_azul;

                    totalQuitado += contaPagarReceber.ValorTotal;
                }
                else if (contaPagarReceber.Status == EnumStatusContaPagarReceber.INATIVO)
                {
                    contaPagarReceberAuxiliar.Imagem = Properties.Resources.icone_inativar;
                }
                else if (contaPagarReceber.Status == EnumStatusContaPagarReceber.CANCELADO)
                {
                    contaPagarReceberAuxiliar.Imagem = Properties.Resources.icone_cancelar1;
                }
                else
                {
                    contaPagarReceberAuxiliar.Imagem = null;
                }
            }

            txtTotalAbertoVencer.Text = totalAbertoAVencer.ToString("###,###,###0.00");
            txtTotalAbertoVencido.Text = totalAbertoVencido.ToString("###,###,###0.00");
            txtTotalGeralPagarReceber.Text = totalGeralAPagar.ToString("###,###,###0.00");

            txtTotalQuitado.Text = totalQuitado.ToString("###,###,###0.00");

            gcContasPagarReceber.DataSource = listaDeContasPagarReceberAuxiliar;
            gcContasPagarReceber.RefreshDataSource();             
        }

        private void PreenchaCboFormaPagamento()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

            var listaFormasPagamentos = servicoFormaPagamento.ConsulteListaAtivos();

            listaFormasPagamentos.Insert(0, null);

            cboFormaDePagamento.Properties.DataSource = listaFormasPagamentos;
            cboFormaDePagamento.Properties.DisplayMember = "Descricao";
            cboFormaDePagamento.Properties.ValueMember = "Id";
        }

        private void PreenchaOStatus()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusContaPagarReceber>();
            lista.Insert(0, null);

            cboStatus.Properties.DataSource = lista;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";
        }

        private void PreenchaCboDataFiltrar()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumDataFiltrarContasPagarReceber>();

            lista.Insert(0, null);

            cboDataFiltrar.Properties.DataSource = lista;
            cboDataFiltrar.Properties.ValueMember = "Valor";
            cboDataFiltrar.Properties.DisplayMember = "Descricao";

            cboDataFiltrar.EditValue = EnumDataFiltrarContasPagarReceber.VENCIMENTO;
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

        private void PreenchaPlanoDeContas(PlanoDeContas planoDeContas, bool exibirMensagemDeNaoEncontrado = false)
        {
            _planoDeContas = planoDeContas;

            if (planoDeContas != null)
            {
                txtNumeroPlanoDeContas.Text = planoDeContas.NumeroPlanoDeContas;
                txtDescricaoPlanoContas.Text = planoDeContas.Descricao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Plano de Contas nao encontrado!", "Plano de Contas não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdPessoa.Focus();
                }

                txtNumeroPlanoDeContas.Text = string.Empty;
                txtDescricaoPlanoContas.Text = string.Empty;
            }
        }

        protected virtual ServicoContasPagarReceber RetorneServicoContasPagarOuReceber()
        {
            return null;
        }

        protected virtual ContaPagarReceber RetorneTituloSelecionado()
        {
            ContaPagarReceber contaPagarReceber = null;

            if (_listaDeContasPagarReceber != null && _listaDeContasPagarReceber.Count > 0)
            {
                ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

                contaPagarReceber = _listaDeContasPagarReceber.Find(x => x.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));//servicoContasPagarReceber.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            return contaPagarReceber;
        }
        
        protected virtual FormCadastroContasPagarReceber RetorneFormCadastroContasPagarReceber()
        {
            return null;
        }

        protected virtual FormManutencaoContaPagarReceber RetorneFormManutencaoContaPagarReceber()
        {
            return null;
        }

        protected virtual FormProrrogacaoContaPagarReceber RetorneFormProrrogacaoContaPagarReceber()
        {
            return null;
        }

        private void ExibicaoBotoesManutencaoEProrrogacao()
        {
            if (gcContasPagarReceber.ContainsFocus)
            {
                var contaPagarReceber = RetorneTituloSelecionado();

                if (contaPagarReceber != null && !_EhMovimentacao)
                {
                    if ((EnumTipoFormaPagamento)contaPagarReceber.FormaPagamento.Id == EnumTipoFormaPagamento.CREDIARIOPROPRIO ||
                        contaPagarReceber.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.BOLETOBANCARIO ||
                        contaPagarReceber.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE ||
                        contaPagarReceber.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO ||
                        contaPagarReceber.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO ||
                        contaPagarReceber.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO ||
                        contaPagarReceber.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DUPLICATA ||
                        contaPagarReceber.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DEPOSITOBANCARIO)
                    {
                        if ((EnumTipoFormaPagamento)contaPagarReceber.FormaPagamento.Id == EnumTipoFormaPagamento.CREDIARIOPROPRIO)
                        {
                            btnCarne.Visible = true;
                            btnBoleto.Visible = true;
                            btnBoleto.Location = new Point(420, 0);
                            btnBoleto.Width = 125;
                            btnBoleto.Text = "Promissoria";
                            //gridViewContasPagarReceber.OptionsSelection.MultiSelect = false;
                        }
                        else if(contaPagarReceber.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.BOLETOBANCARIO)
                        {
                            btnBoleto.Visible = true;
                            btnBoleto.Location = new Point(459, 0);
                            btnBoleto.Width = 90;
                            btnBoleto.Text = "Boleto";
                            //gridViewContasPagarReceber.OptionsSelection.MultiSelect = true;
                        }
                        else if(contaPagarReceber.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                        {
                            btnBoleto.Visible = true;
                            btnBoleto.Location = new Point(459, 0);
                            btnBoleto.Width = 90;
                            btnBoleto.Text = "Cheque";
                        }

                        if (contaPagarReceber.Status == EnumStatusContaPagarReceber.QUITADO || contaPagarReceber.Status == EnumStatusContaPagarReceber.CONCILIADOQUITADO ||
                            contaPagarReceber.Status == EnumStatusContaPagarReceber.INATIVO || contaPagarReceber.Status == EnumStatusContaPagarReceber.CANCELADO)
                        {
                            btnBoleto.Visible = false;
                            btnQuitarTitulo.Visible = false;                           
                        }
                        else 
                        {   
                            btnQuitarTitulo.Visible = true;
                            btnQuitarTitulo.Text = "Quitar Título";
                            btnQuitarTitulo.Image = Properties.Resources.icone_baixar;
                        }

                        if (contaPagarReceber.Status == EnumStatusContaPagarReceber.QUITADO || contaPagarReceber.Status == EnumStatusContaPagarReceber.CONCILIADOQUITADO)
                        {   
                            btnQuitarTitulo.Visible = true;
                            btnQuitarTitulo.Text = "Estornar";
                            btnQuitarTitulo.Image = Properties.Resources.icone_estornar;
                        }
                    }
                    else
                    {
                        btnBoleto.Visible = false;
                        btnCarne.Visible = false;
                    }
                    
                    if (_manutencaoLiberada && !_EhMovimentacao)
                    {
                        btnManutencaoTitulo.Visible = true;
                    }

                    if (_prorrogacaoLiberada)
                    {
                        if (contaPagarReceber.Status == EnumStatusContaPagarReceber.QUITADO || contaPagarReceber.Status == EnumStatusContaPagarReceber.CONCILIADOQUITADO ||
                            contaPagarReceber.Status == EnumStatusContaPagarReceber.INATIVO || contaPagarReceber.Status == EnumStatusContaPagarReceber.CANCELADO)
                        {   
                            btnProrrogarTitulo.Visible = false;
                        }
                        else
                        {   
                            btnProrrogarTitulo.Visible = false;
                        }
                    }
                }
                else if (_EhMovimentacao)
                     {
                        btnQuitarTitulo.Visible = true;
                        btnQuitarTitulo.Text = "Selecionar";
                        btnQuitarTitulo.Image = Properties.Resources.icone_selecionar1;
                     }  
            }
            else
            {
                btnManutencaoTitulo.Visible = false;
                btnProrrogarTitulo.Visible = false;
            }
        }

        private void PreenchaPrimeiroEUltimoDiaMes()
        {
            //var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

            var primeiroDiaMes = DateTime.Now;
            var addDias = primeiroDiaMes.AddDays(5);
            txtDataInicialPeriodo.DateTime = primeiroDiaMes;
            txtDataFinalPeriodo.DateTime = addDias;
        }

        private void ImprimirPromissoria(List<ContaPagarReceber> listaClientesPromissoria)
        {   
            RelatorioNotaPromissoria relatorioNotaPromissoria = new RelatorioNotaPromissoria(listaClientesPromissoria,null);

            TratamentosDeTela.ExibirRelatorio(relatorioNotaPromissoria);
        }

        private void ImprimirCarne(List<ContaPagarReceber> listaClientesPromissoria)
        {
            RelatorioCarnePagamento relatorioCarnePagamento = new RelatorioCarnePagamento(listaClientesPromissoria, null);

            TratamentosDeTela.ExibirRelatorio(relatorioCarnePagamento);
        }

        private ContaPagarReceberPagamento retorneListaHistoricoDePagamentos(DateTime DataPagamento, double Valor, FormaPagamento
                                                                                  formaDePagamento, ContaPagarReceber ContaPagarReceber, bool EstahEstornado)
        {
            ContaPagarReceberPagamento item = new ContaPagarReceberPagamento();

            List<ContaPagarReceberPagamento> Lista = new List<ContaPagarReceberPagamento>();

            item.DataPagamento = DataPagamento;
            item.Valor = Valor;
            item.Observacoes = "Título quitado de forma direta, no formulário de pesquisa de Contas Pagar Receber, através o botão <Quitar Título>";
            item.Responsavel = new ServicoPessoa().Consulte(Sessao.PessoaLogada.Id);
            item.FormaPagamento = formaDePagamento;
            item.ContaPagarReceber = ContaPagarReceber;
            item.EstahEstornado = EstahEstornado;
            
            return item;
        }

        private bool UsuarioTemCaixaAberto()
        {
            ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();

            var caixa = new ServicoCaixa().ConsultePeloFuncionario(Sessao.PessoaLogada);

            if (caixa == null)
            {
                return false;
            }

            var movimentacaoCaixa = servicoMovimentacaoCaixa.ConsulteCaixaAberto(caixa);

            if (movimentacaoCaixa == null)
            {
                return false;
            }

            return true;

        }

        private bool BancoEstahAberto(BancoParaMovimento banco)
        {
            if (banco == null) return false;

            var bancoMov = new ServicoMovimentacaoBanco().ConsulteBancoAberto(banco);

            if (bancoMov == null)               
                return false;

            return true;
        }

        private bool OCaixaVaiFicarNegativo(EnumTipoFormaPagamento formaPagamento, double valorPago)
        {
            ServicoCaixa servicoCaixa = new ServicoCaixa();

            var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

            if (caixa == null) return false;

            ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();

            var caixaAberto = servicoMovimentacaoCaixa.ConsulteLista(caixa, null, null, null, EnumStatusMovimentacaoCaixa.ABERTO);

            if (caixaAberto == null) return false;

            double dinheiroEntrada = 0;
            double dinheiroSaida = 0;
            double dinheiroSaldoFinal = 0;

            double chequeEntrada = 0;
            double chequeSaida = 0;
            double chequeSaldoFinal = 0;

            var listaItensCaixaAberto = caixaAberto.FirstOrDefault().ListaItensCaixa;

            foreach (var item in listaItensCaixaAberto)
            {
                if (item.EstahEstornado)
                {
                    continue;
                }

                if (item.TipoMovimentacao == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
                {
                    if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO)
                    {
                        dinheiroEntrada += item.Valor;
                        dinheiroSaldoFinal += item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                    {
                        chequeEntrada += item.Valor;
                        chequeSaldoFinal += item.Valor;
                    }
                }
                else
                {
                    if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO)
                    {
                        dinheiroSaida += item.Valor;
                        dinheiroSaldoFinal -= item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                    {
                        chequeSaida += item.Valor;
                        chequeSaldoFinal -= item.Valor;
                    }
                }
            }

            //Dinheiro
            if (formaPagamento == EnumTipoFormaPagamento.DINHEIRO)
            {
                if (((dinheiroSaldoFinal.ToDouble() - valorPago) < 0))
                {
                    return true;
                }
            }
            else //Cheque
            {
                if (((chequeSaldoFinal.ToDouble() - valorPago) < 0))
                {
                    return true;
                }
            }

            return false;
        }

        private void QuitarTituloDiretoDaPesquisa()
        {   
            if (MessageBox.Show("O(s) título(s) selecionado(s) será(ão) baixado(s) na <data atual>.\n\nDeseja continuar?", "Baixar Título", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            if (!UsuarioTemCaixaAberto())
            {
                MessageBox.Show("Você não possui um caixa. Para continuar cadastre ou entre com um usuário que tenha caixa, e que esteja, aberto.","Baixar Titulo",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var listaParaQuitacaoDeTitulos = carregaListaParaEmissaoBoleto();

            if (!valideSeListaContasPagarReceberEhQuitado(listaParaQuitacaoDeTitulos)) return;

            if (!valideSeListaContasPagarReceberEhParaDinheiro(listaParaQuitacaoDeTitulos)) return;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                foreach (var item in listaParaQuitacaoDeTitulos)
                {
                    var tituloSelecionado = RetorneTituloSelecionado();

                    if (!BancoEstahAberto(tituloSelecionado.BancoParaMovimento) && _parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
                    {
                        if (MessageBox.Show("O(s) título(s) selecionado(s) está(ão) com o banco fechado.\n\nDeseja continuar?", "Baixar Título", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                        {
                            return;
                        }
                    }

                    //Valida se o caixa vai ficar negativo quando for Cheque ou Dinheiro
                    if ((tituloSelecionado.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO ||
                        tituloSelecionado.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE) &&
                        tituloSelecionado.TipoOperacao == EnumTipoOperacaoContasPagarReceber.PAGAR)
                    {
                        if (OCaixaVaiFicarNegativo(tituloSelecionado.FormaPagamento.TipoFormaPagamento, tituloSelecionado.ValorTotal))
                        {
                            MessageBox.Show("Este título: "+ tituloSelecionado.NumeroDocumento + " - não poderá ser baixado, pois seu caixa vai ficar negativo. Por favor, verifique seu caixa!.", "Manutenção de Títulos");
                            continue;
                        }
                    }

                    if (tituloSelecionado.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER)
                    {
                        ServicoContasReceber servicoContasReceber = new ServicoContasReceber();

                        var itemBaixarReceber = servicoContasReceber.Consulte(item.Id);
                        
                        var total = CalculoValorTotalContaPagarReceber.CalculeValorTotalContaPagarReceber(itemBaixarReceber);

                        itemBaixarReceber.Status = EnumStatusContaPagarReceber.QUITADO;
                        itemBaixarReceber.ValorPago = itemBaixarReceber.ValorPago == 0 ? total : Math.Round(itemBaixarReceber.ValorTotal - itemBaixarReceber.ValorPago,2);
                        itemBaixarReceber.DataPagamento = DateTime.Now;

                        servicoContasReceber.Atualize(itemBaixarReceber);

                        itemBaixarReceber.ListaContasPagarReceberParcial = new List<ContaPagarReceberPagamento>();

                        if ((EnumTipoFormaPagamento)itemBaixarReceber.FormaPagamento.Id == EnumTipoFormaPagamento.CREDIARIOPROPRIO)
                        {  
                            itemBaixarReceber.FormaPagamento = new FormaPagamento { Id=(int)EnumTipoFormaPagamento.DINHEIRO, TipoFormaPagamento = EnumTipoFormaPagamento.DINHEIRO} ;
                        }
                        else if (itemBaixarReceber.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DUPLICATA)
                        {
                            itemBaixarReceber.FormaPagamento = new FormaPagamento { Id = (int)EnumTipoFormaPagamento.DINHEIRO, TipoFormaPagamento = EnumTipoFormaPagamento.DINHEIRO };
                        }

                        var objetoContasPagarReceberParcial = retorneListaHistoricoDePagamentos(DateTime.Now,
                                                                itemBaixarReceber.ValorPago,
                                                                itemBaixarReceber.FormaPagamento, itemBaixarReceber, false);

                        itemBaixarReceber.ListaContasPagarReceberParcial.Add(objetoContasPagarReceberParcial);

                        ServicoContasPagarReceberPagamento servicoContasPagarReceberPagamento = new ServicoContasPagarReceberPagamento();

                        servicoContasPagarReceberPagamento.CadastreLista(itemBaixarReceber.ListaContasPagarReceberParcial.ToList());
                        servicoContasPagarReceberPagamento.Cadastre(objetoContasPagarReceberParcial);

                        if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                        {
                            servicoContasReceber.AtualizarChequesContaPagarReceber(itemBaixarReceber);
                        }

                        //Movimentação Bancária
                        if (itemBaixarReceber.FormaPagamento.TipoFormaPagamento != EnumTipoFormaPagamento.DINHEIRO && 
                            _parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
                        {
                            FacaMovimentacaoBancaria(itemBaixarReceber, EnumTipoOperacaoContasPagarReceber.RECEBER);
                        }
                    }
                    else
                    {
                        ServicoContasPagar servicoContasPagar = new ServicoContasPagar();

                        var itemBaixarPagar = servicoContasPagar.Consulte(item.Id);
                        
                        itemBaixarPagar.Status = EnumStatusContaPagarReceber.QUITADO;
                        itemBaixarPagar.ValorPago = itemBaixarPagar.ValorPago == 0 ? itemBaixarPagar.ValorTotal : itemBaixarPagar.ValorTotal - itemBaixarPagar.ValorPago;
                        itemBaixarPagar.DataPagamento = DateTime.Now;

                        servicoContasPagar.Atualize(itemBaixarPagar);

                        itemBaixarPagar.ListaContasPagarReceberParcial = new List<ContaPagarReceberPagamento>();

                        if ((EnumTipoFormaPagamento)itemBaixarPagar.FormaPagamento.Id == EnumTipoFormaPagamento.CREDIARIOPROPRIO)
                        {
                            itemBaixarPagar.FormaPagamento = new FormaPagamento { Id = (int)EnumTipoFormaPagamento.DINHEIRO, TipoFormaPagamento = EnumTipoFormaPagamento.DINHEIRO };
                        }
                        else if (itemBaixarPagar.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DUPLICATA)
                        {
                            itemBaixarPagar.FormaPagamento = new FormaPagamento { Id = (int)EnumTipoFormaPagamento.DINHEIRO, TipoFormaPagamento = EnumTipoFormaPagamento.DINHEIRO };
                        }

                        var objetoContasPagarReceberParcial = retorneListaHistoricoDePagamentos(DateTime.Now,
                                                                itemBaixarPagar.ValorPago,
                                                                itemBaixarPagar.FormaPagamento, itemBaixarPagar, false);

                        itemBaixarPagar.ListaContasPagarReceberParcial.Add(objetoContasPagarReceberParcial);

                        ServicoContasPagarReceberPagamento servicoContasPagarReceberPagamento = new ServicoContasPagarReceberPagamento();

                        servicoContasPagarReceberPagamento.CadastreLista(itemBaixarPagar.ListaContasPagarReceberParcial.ToList());
                        servicoContasPagarReceberPagamento.Cadastre(objetoContasPagarReceberParcial);
                        
                        if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                        {
                            new ServicoContasPagar().AtualizarChequesContaPagarReceber(itemBaixarPagar);
                        }

                        //Movimentação Bancária
                        if (itemBaixarPagar.FormaPagamento.TipoFormaPagamento != EnumTipoFormaPagamento.DINHEIRO &&
                            _parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
                        {
                            FacaMovimentacaoBancaria(itemBaixarPagar,EnumTipoOperacaoContasPagarReceber.PAGAR);
                        }                           
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                throw ex;
            }

            this.Cursor = Cursors.Default;

            MessageBox.Show("Título(s) quitado(s) com sucesso!", "Quitação de Título(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Pesquise();
        
        }

        private void FacaMovimentacaoBancaria(ContaPagarReceber contaPagarReceber, EnumTipoOperacaoContasPagarReceber tipoOperacao)
        {
            CategoriaFinanceira categoria = contaPagarReceber.CategoriaFinanceira != null ?
                                              contaPagarReceber.CategoriaFinanceira : contaPagarReceber.Historico != null &&
                                              contaPagarReceber.Historico.Contains("Pedido")
                                              && contaPagarReceber.TipoOperacao != EnumTipoOperacaoContasPagarReceber.PAGAR ?
                                              new CategoriaFinanceira { Id = 2 } : null;

            var banco = new ServicoBancoParaMovimento().ConsulteLista(string.Empty, "A");

            var idbanco = banco.Find(x => x.TornarPadrao == true).Id;

            var bancoMov = contaPagarReceber.BancoParaMovimento != null ?
                           contaPagarReceber.BancoParaMovimento : new BancoParaMovimento { Id = idbanco };

            new ServicoItemMovimentacaoBanco().InsiraMovimentacaoBancaria(contaPagarReceber, false, tipoOperacao,
                                                                          contaPagarReceber.DataPagamento.Value, categoria, bancoMov,
                                                                          contaPagarReceber.ValorPago, new Pessoa { Id = Sessao.PessoaLogada.Id });

            //Calcular despesas de cartões e lançar no banco(Somente Contas Receber)
            if (tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER)
                if (contaPagarReceber.FormaPagamento.TipoFormaPagamento != EnumTipoFormaPagamento.CARTAOCREDITO ||
                    contaPagarReceber.FormaPagamento.TipoFormaPagamento != EnumTipoFormaPagamento.CARTAODEBITO)
                {
                    new ServicoItemMovimentacaoBanco().CalculeDespesasCartoes(contaPagarReceber, false,
                                                   EnumTipoOperacaoContasPagarReceber.PAGAR, contaPagarReceber.DataPagamento.Value,
                                                   bancoMov,
                                                   contaPagarReceber.OperadorasCartao,
                                                   contaPagarReceber.ValorPago, new Pessoa { Id = Sessao.PessoaLogada.Id });

                }
        }

        private void EstornarTituloDiretoDaPesquisa()
        {
            if (MessageBox.Show("O(s) título(s) selecionado(s) será(ão) Estornado(s).\n\nDeseja continuar?", "Estornar Título", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            var listaParaEstornoDeTitulos = carregaListaParaEmissaoBoleto();

            if (!valideSeListaContasPagarReceberEstaoAbertos(listaParaEstornoDeTitulos)) return;

            if (!valideSeListaContasPagarReceberTemOutrosPagamentos(listaParaEstornoDeTitulos)) return;

            try
            {
                foreach (var item in listaParaEstornoDeTitulos)
                {
                    var tituloSelecionado = RetorneTituloSelecionado();

                    if (tituloSelecionado.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER)
                    {
                        ServicoContasReceber servicoContasReceber = new ServicoContasReceber();

                        var itemBaixarReceber = servicoContasReceber.Consulte(item.Id);
                        
                        if(itemBaixarReceber.ListaContasPagarReceberParcial.Count != 0)
                        {
                            var itemEstornar = itemBaixarReceber.ListaContasPagarReceberParcial.FirstOrDefault(x=>x.EstahEstornado==false);

                            if(itemEstornar != null)
                                new ServicoContasPagarReceberPagamento().EstorneRegistro(itemEstornar);
                        }

                        servicoContasReceber.EstornarContaPagarReceber(itemBaixarReceber);                        
                        
                        if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                        {
                            servicoContasReceber.AtualizarChequesContaPagarReceber(itemBaixarReceber);
                        }

                        //Se tiver movimentação no Banco, vai excluir...
                        if(_parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
                            new ServicoItemMovimentacaoBanco().ExcluaParcialOrigemPagarReceber(itemBaixarReceber, false);


                    }
                    else
                    {
                        ServicoContasPagar servicoContasPagar = new ServicoContasPagar();

                        var itemBaixarPagar = servicoContasPagar.Consulte(item.Id);

                        if (itemBaixarPagar.ListaContasPagarReceberParcial != null)
                        {
                            var itemEstornar = itemBaixarPagar.ListaContasPagarReceberParcial.FirstOrDefault(x => x.EstahEstornado == false);

                            if (itemEstornar != null)
                                new ServicoContasPagarReceberPagamento().EstorneRegistro(itemEstornar);
                        }

                        servicoContasPagar.EstornarContaPagarReceber(itemBaixarPagar);
                        
                        if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                        {
                            new ServicoContasPagar().AtualizarChequesContaPagarReceber(itemBaixarPagar);
                        }

                        //Se tiver movimentação no Banco, vai excluir...
                        if (_parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
                            new ServicoItemMovimentacaoBanco().ExcluaParcialOrigemPagarReceber(itemBaixarPagar, false);
                    }

                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                throw ex;               
            }

            this.Cursor = Cursors.Default;

            MessageBox.Show("Título(s) Estornados(s) com sucesso!", "Estorno de Título(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Pesquise();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ContaPagarReceberAuxiliar
        {
            public int Id { get; set; }

            public string Pessoa { get; set; }

            public string NumeroDocumento { get; set; }

            public string FormaDePagamento { get; set; }

            public double ValorParcela { get; set; }

            public double Multa { get; set; }

            public double Juros { get; set; }

            public double Desconto { get; set; }

            public double ValorTotal { get; set; }

            public double ValorPago { get; set; }

            public double ValorAPagar { get; set; }

            public string DataPagamento { get; set; }

            public string DataEmissao { get; set; }

            public string DataVencimento { get; set; }

            public string Status { get; set; }

            public Image Imagem { get; set; }
        }

        #endregion

        private void btnImprimirRelatorio_Click(object sender, EventArgs e)
        {
            //gcContasPagarReceber.view
            gcContasPagarReceber.ExibaRelatorio();
            
        }

        private void btnBoleto_Click(object sender, EventArgs e)
        {
            if (btnBoleto.Text == "Promissoria")
            {
                var listaDeClientesParaEmissaoDeBoleto = carregaListaParaEmissaoBoleto();
                ImprimirPromissoria(listaDeClientesParaEmissaoDeBoleto);
            }
            else if(btnBoleto.Text == "Boleto")
            {
                NBoleto formBoleto = new NBoleto();

                formBoleto._QuantidadeBoletos = gridViewContasPagarReceber.SelectedRowsCount;

                var listaDeClientesParaEmissaoDeBoleto = carregaListaParaEmissaoBoleto();

                if (!valideListaContasReceberEhBoleto(listaDeClientesParaEmissaoDeBoleto)) return;

                formBoleto._listaClientesEmissao = listaDeClientesParaEmissaoDeBoleto;

                formBoleto.ShowDialog();
            }
            else if(btnBoleto.Text == "Cheque")
            {
                var tituloSelecionado = RetorneTituloSelecionado();
               
                ServicoCheque servicoCheque = new ServicoCheque();

                FormCadastroCheque formCadastroCheque = new FormCadastroCheque(0,null,0,0,null, (bool)(tituloSelecionado.TipoOperacao == EnumTipoOperacaoContasPagarReceber.PAGAR));
                                
                Cheque chequeDoc = new Cheque();

                var ListaDeCheques = servicoCheque.ConsulteLista(tituloSelecionado.Pessoa, null, null, null, null, null, true, true, true, true, true, true, false);

                foreach (var item in ListaDeCheques)
                {
                    chequeDoc = servicoCheque.ConsulteJoinComItens(item.Id);

                    var numeroPedido = tituloSelecionado.NumeroDocumento.Split('-');

                    int nPedido = numeroPedido[0].Trim().TrimEnd().ToInt();

                    if((chequeDoc.ListaVinculosDePedidos.FirstOrDefault(x=>x.NumeroPedidos == nPedido) != null))
                    {
                        chequeDoc.Pessoa = tituloSelecionado.Pessoa;
                        formCadastroCheque.EditeCheque(chequeDoc, tituloSelecionado);
                        return;
                    }
                }

                if (tituloSelecionado.ChequeId == null || tituloSelecionado.ChequeId == 0)
                {
                    if (chequeDoc.ListaVinculosDePedidos.Count == 0)
                        chequeDoc = servicoCheque.ConsulteChequePeloNumeroDocumento(tituloSelecionado.NumeroDocumento);

                    if (chequeDoc != null)
                    {
                        chequeDoc.Pessoa = tituloSelecionado.Pessoa;
                        formCadastroCheque.EditeCheque(chequeDoc, tituloSelecionado);
                        return;
                    }
                    else
                    {
                        formCadastroCheque.CadastreNovoCheque(tituloSelecionado.Pessoa, tituloSelecionado);
                        return;
                    }
                }

                var cheque = servicoCheque.Consulte(tituloSelecionado.ChequeId.GetValueOrDefault());
                    
                cheque.Pessoa = tituloSelecionado.Pessoa;

                formCadastroCheque.EditeCheque(cheque, tituloSelecionado);
            }
        }

        private void btnCarne_Click(object sender, EventArgs e)
        {
            var listaDeClientesParaEmissaoDeBoleto = carregaListaParaEmissaoBoleto();
            ImprimirCarne(listaDeClientesParaEmissaoDeBoleto);
        }

        private void btnQuitarTitulo_Click(object sender, EventArgs e)
        {
            if(btnQuitarTitulo.Text.RemovaEspacosEmBrancoDoInicioEFim() == "Quitar Título")
            {
                QuitarTituloDiretoDaPesquisa();
            }
            else if (btnQuitarTitulo.Text.RemovaEspacosEmBrancoDoInicioEFim() == "Estornar")
            {
                EstornarTituloDiretoDaPesquisa();
            }
            else if(btnQuitarTitulo.Text.RemovaEspacosEmBrancoDoInicioEFim() == "Selecionar")
            {
                SelecioneLancamento();
            }   
        }
    }
    
}
