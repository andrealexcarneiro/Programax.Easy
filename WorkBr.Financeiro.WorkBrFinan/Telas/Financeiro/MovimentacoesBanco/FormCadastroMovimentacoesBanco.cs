using OFXParser.Entities;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Report.RelatoriosDevExpress.Financeiro;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;
using Programax.Easy.Servico.Financeiro.CategoriaServ;
using Programax.Easy.Servico.Financeiro.ContaBancariaServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoBancoServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoBancoServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.View.Telas.Financeiro.Categorias;
using Programax.Easy.View.Telas.Financeiro.ContasPagarReceber;
using Programax.Infraestrutura.Negocio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transaction = OFXParser.Entities.Transaction;

namespace Programax.Easy.View.Telas.Financeiro.MovimentacoesBanco
{
    public partial class FormCadastroMovimentacoesBanco : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private MovimentacaoBanco _movimentacaoBanco;
        private List<ItemMovimentacaoBanco> _listaItensMovimentacoes;
        private BancoParaMovimento _banco;
        private int _IdItemMovimento;
        private ItemMovimentacaoBanco _itemMovimentoEmEdicao;
        string caminhoDoArquivoOFX;

        List<ConciliacaoBancaria> _listaConcilicao = new List<ConciliacaoBancaria>();
        List<ConciliacaoBancaria> _listaConcilicaoImportacao = new List<ConciliacaoBancaria>();
        List<BancoParaMovimento> _listaBancos = new List<BancoParaMovimento>();
        List<string> _listaBancosRelatorio = new List<string>();
        List<int> _listaIdMovimentacoesBancos = new List<int>();

        private DateTime? _dataInicialPesquisa = null;
        private DateTime? _dataFinalPesquisa = null;

        private Parametros _parametros;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroMovimentacoesBanco()
        {
            InitializeComponent();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnLimparSelecao_Click(object sender, EventArgs e)
        {
            LimparSelecaoBancos();
        }

        private void btnEscolherBancos_Click(object sender, EventArgs e)
        {
            EscolherBancos();
        }

        private void txtIdBanco_Leave(object sender, EventArgs e)
        {
            ServicoBancoParaMovimento servicoBanco = new ServicoBancoParaMovimento();

            BancoParaMovimento banco = servicoBanco.Consulte(txtIdBanco.Text.ToInt());

            if (banco == null)
            {
                banco = servicoBanco.ConsulteBanco();
            }

            PreenchaCamposBanco(banco);

            if (banco != null)
            {
                ServicoMovimentacaoBanco servicoMovimentacaoBanco = new ServicoMovimentacaoBanco();

                MovimentacaoBanco movimentacaoBanco = servicoMovimentacaoBanco.ConsulteBancoAberto(banco);

                if (movimentacaoBanco != null)
                    PreenchaCamposMovimentacaoBanco(movimentacaoBanco);
                else
                    PreparaBancoNovoMovimento();
            }
        }

        private void EscolherBancos()
        {
            var formSelecionarBancos = new FormSelecaoBancos();

            formSelecionarBancos.ShowDialog();

            _listaBancos = formSelecionarBancos._listaBancos;

            if (_listaBancos.Count > 0)
            {
                AtivarSelecaoBancos();
            }
            else
            {
                LimparSelecaoBancos();
            }
        }

        private void PreparaBancoNovoMovimento()
        {
            _movimentacaoBanco = new MovimentacaoBanco();
            _listaItensMovimentacoes = new List<ItemMovimentacaoBanco>();

            txtId.Text = string.Empty;
            txtDataBanco.Text = string.Empty;
            txtStatusBanco.Text = string.Empty;

            btnAbrirBanco.Visible = true;
            btnFecharBanco.Visible = false;

            btnStatusFechado.Visible = true;
            btnStatusAberto.Visible = false;

            PreenchaGrid();
            PreenchaResumo();
        }

        private void btnDadosCruzados_Click(object sender, EventArgs e)
        {
            if (!UsuarioTemCaixa()) return;

            ImportacoesAssociadas();
        }

        private void pbPesquisaAvancada_Click(object sender, EventArgs e)
        {
            PesquisarTodos();
        }

        private void FormCadastroMovimentacoesBanco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                var sairForm = MessageBoxAkil.Show("Deseja sair da movimentação de bancos?", "Sair", MessageBoxButtons.OKCancel);

                if (sairForm == DialogResult.Cancel)
                {
                    return;
                }

                this.Close();
            }
            else if (e.KeyCode == Keys.F2)
            {
                Pesquise();
            }
            else if (e.KeyCode == Keys.F3)
            {
                PesquisarTodos();
            }
            else if (e.KeyCode == Keys.F4)
            {
                LimpeCamposItem();
            }
            else if (e.KeyCode == Keys.F5)
            {
                if (btnAbrirBanco.Visible == true)
                    AbrirBanco();

                if (btnFecharBanco.Visible == true)
                    FecharBanco();
            }
            else if (e.KeyCode == Keys.F6)
            {
                ImprimeMovimentacao();
            }
            else if (e.KeyCode == Keys.F7)
            {
                if (!UsuarioTemCaixa()) return;

                ImportarEConciliarExtratoOfx();
            }
            else if (e.KeyCode == Keys.F8)
            {
                if (!UsuarioTemCaixa()) return;

                ConciliarComPagarReceber();
            }
            else if (e.KeyCode == Keys.F9)
            {
                if (!UsuarioTemCaixa()) return;

                ImportacoesAssociadas();
            }
        }

        private void FormCadastroMovimentacoesBanco_Load(object sender, EventArgs e)
        {
            _listaItensMovimentacoes = new List<ItemMovimentacaoBanco>();

            PreenchaCboTipoMovimentacao();
            PreenchaCboOrigemMovimentacao();

            txtDataHoraMovimento.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDataInicialPeriodo.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDataFinalPeriodo.Text = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy");

            PesquiseEPreenchaBancoAberto();

            this.ActiveControl = cboTiposMovimentacoes;
        }

        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InsiraEAtualizeItem();
            }
        }

        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void btnInserirAtualizarItem_Click(object sender, EventArgs e)
        {
            InsiraEAtualizeItem();
        }

        private void btnEstornarItem_Click(object sender, EventArgs e)
        {
            if (!BancoEstahAberto())
            {
                MessageBox.Show("O Banco escolhido não está aberto. Faça a abertura do mesmo para continuar!", "Movimentações Bancárias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Deseja Excluir este item?", "Excluir item", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            Action actionEstornoItemBanco = () =>
            {
                var itemMovimentacaoBanco = RetorneItemMovimentacaoBancoSelecionado();

                if (itemMovimentacaoBanco.DescricaoDaMovimentacao == "Saldo inicial na abertura.")
                {
                    MessageBox.Show("Não é possível excluir o Saldo Inicial de abertura!", "Excluindo item do banco", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if ((itemMovimentacaoBanco.OrigemMovimentacaoBanco == EnumOrigemMovimentacaoBanco.CONTAPAGAR ||
                    itemMovimentacaoBanco.OrigemMovimentacaoBanco == EnumOrigemMovimentacaoBanco.CONTARECEBER) &&
                    itemMovimentacaoBanco.NumeroDocumentoOrigem != null)
                {
                    if (MessageBox.Show("Existe um pagamento/recebimento registrado para esta movimentação, ele será estornado.\n\nDeseja Continuar?", "Excluindo Item", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }

                ServicoItemMovimentacaoBanco servicoItemMovimentacaoBanco = new ServicoItemMovimentacaoBanco();

                //Se tiver conta Pagar ou Receber, vamos estornar também...
                servicoItemMovimentacaoBanco.EstornarContasPagarReceber(itemMovimentacaoBanco);

                servicoItemMovimentacaoBanco.Exclua(itemMovimentacaoBanco.Id);

                PesquisarTodos();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionEstornoItemBanco);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnAbrirBanco_Click(object sender, EventArgs e)
        {
            AbrirBanco();
        }

        private void btnFecharBanco_Click(object sender, EventArgs e)
        {
            FecharBanco();
        }

        private void btnCancelarItem_Click(object sender, EventArgs e)
        {
            LimpeCamposItem();
        }

        private void btnPesquisarBanco_Click(object sender, EventArgs e)
        {
            var formPequisaMovimentacoesBanco = new FormPesquisaMovimentacoesBanco();

            var movimentacaoBanco = formPequisaMovimentacoesBanco.PesquiseBanco(_banco);

            if (movimentacaoBanco != null)
            {
                PreenchaCamposBanco(movimentacaoBanco.Banco);
                PreenchaCamposMovimentacaoBanco(movimentacaoBanco);
            }
        }

        private void btnImprimirMovimentacao_Click(object sender, EventArgs e)
        {
            ImprimeMovimentacao();
        }

        private void btnAdicionarCategoria_Click(object sender, EventArgs e)
        {
            FormCadastroCategoriasFinanceiras formCategoria = new FormCadastroCategoriasFinanceiras();
            formCategoria.ShowDialog();

            PreenchaCboCategorias();

            cboCategoriaFinanceira.EditValue = null;
        }

        private void btnStatusFechado_Click(object sender, EventArgs e)
        {
            FormAberturaBanco formAberturBanco = new FormAberturaBanco();

            var movimentacaoAbertura = formAberturBanco.AbrirBanco(_banco);

            if (movimentacaoAbertura != null)
            {
                PreenchaCamposMovimentacaoBanco(movimentacaoAbertura);
            }
        }

        private void btnStatusAberto_Click(object sender, EventArgs e)
        {
            FormFechamentoBanco formFechamentoBanco = new FormFechamentoBanco();

            _movimentacaoBanco.SaldoFinal = txtSaldoFinal.Text.ToDouble();

            var movimentacaoBanco = formFechamentoBanco.FecharBanco(_movimentacaoBanco);

            if (movimentacaoBanco != null)
            {
                PreenchaCamposMovimentacaoBanco(movimentacaoBanco);
            }
        }

        private void cboTiposMovimentacoes_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCategorias();
        }

        private void gcItens_DoubleClick(object sender, EventArgs e)
        {
            SelecioneItensMovimentacoes();
        }

        private void gcItens_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelecioneItensMovimentacoes();
            }
        }

        private void btnAdicionarParceiro_Click(object sender, EventArgs e)
        {
            FormCadastroPessoa formParceiros = new FormCadastroPessoa();
            formParceiros.ShowDialog();

            cboParceiro.EditValue = null;
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

        private void pbPesquisa_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (!UsuarioTemCaixa()) return;

            ImportarEConciliarExtratoOfx();
        }

        private void btnPagarReceber_Click(object sender, EventArgs e)
        {
            if (!UsuarioTemCaixa()) return;

            ConciliarComPagarReceber();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void ConciliarComPagarReceber()
        {
            ContaPagarReceber contasPagarReceber = new ContaPagarReceber();

            Pessoa parceiro = cboParceiro.EditValue.ToInt() != 0 ? new Pessoa { Id = cboParceiro.EditValue.ToInt() } : null;

            if ((EnumTipoMovimentacaoBanco)cboTiposMovimentacoes.EditValue == EnumTipoMovimentacaoBanco.ENTRADA)
            {
                FormContasReceberPesquisa formPesquisaReceber = new FormContasReceberPesquisa(true);

                contasPagarReceber = formPesquisaReceber.BuscarConciliacaoPagarReceber(parceiro, txtDataHoraMovimento.DateTime.Date);
            }
            else if ((EnumTipoMovimentacaoBanco)cboTiposMovimentacoes.EditValue == EnumTipoMovimentacaoBanco.SAIDA)
            {
                FormContasPagarPesquisa formPesquisaPagar = new FormContasPagarPesquisa(true);

                contasPagarReceber = formPesquisaPagar.BuscarConciliacaoPagarReceber(parceiro, txtDataHoraMovimento.DateTime);
            }
            else
            {
                MessageBox.Show("Para acessar a pesquisa Contas Pagar ou Receber, você precisa informar se é ENTRADA OU SAÍDA.",
                                "Pagar / Receber", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (contasPagarReceber == null) return;

            var itemMov = ConvertaContaPagarReceberParaItmMovimentacao(contasPagarReceber);

            EditeItensMovimentacoes(itemMov);
        }

        private void ImprimeMovimentacao()
        {
            _movimentacaoBanco.ListaItensBanco = _listaItensMovimentacoes;

            RelatorioMovimentacaoBanco relatorioMovimentacaoBanco = new RelatorioMovimentacaoBanco(_movimentacaoBanco,
                                                                                                    _listaBancosRelatorio,
                                                                                                    _dataInicialPesquisa,
                                                                                                    _dataFinalPesquisa);
            TratamentosDeTela.ExibirRelatorio(relatorioMovimentacaoBanco);
        }

        private void AbrirBanco()
        {
            FormAberturaBanco formAberturBanco = new FormAberturaBanco();

            var movimentacaoAbertura = formAberturBanco.AbrirBanco(_banco);

            if (movimentacaoAbertura != null)
            {
                PreenchaCamposMovimentacaoBanco(movimentacaoAbertura);
            }
        }

        private void FecharBanco()
        {
            FormFechamentoBanco formFechamentoBanco = new FormFechamentoBanco();

            _movimentacaoBanco.SaldoFinal = txtSaldoFinal.Text.ToDouble();

            var movimentacaoBanco = formFechamentoBanco.FecharBanco(_movimentacaoBanco);

            if (movimentacaoBanco != null)
            {
                PreenchaCamposMovimentacaoBanco(movimentacaoBanco);
            }

            _listaItensMovimentacoes = new List<ItemMovimentacaoBanco>();
            PreenchaGrid();
        }

        //Vai buscar todos os movimentos em relação ao bancos selecionados
        private void BusqueIdMovimentacoesBanco()
        {
            _listaIdMovimentacoesBancos = new List<int>();

            if (_listaBancos.Count == 0) return;

            foreach (var itemBanco in _listaBancos)
            {
                var movimentacao = new ServicoMovimentacaoBanco().ConsulteLista(itemBanco, null, null, null, null);

                foreach (var itemMov in movimentacao)
                {
                    _listaIdMovimentacoesBancos.Add(itemMov.Id);
                }
            }
        }

        private void Pesquise()
        {
            int iDParceiro = cboParceiro.EditValue.ToInt();
            int iDCategoria = cboCategoriaFinanceira.EditValue.ToInt();

            DateTime dataInicial = txtDataInicialPeriodo.DateTime;
            DateTime dataFinal = txtDataFinalPeriodo.DateTime;

            _dataFinalPesquisa = dataFinal;
            _dataInicialPesquisa = dataInicial;

            //Vai buscar todos os movimentos em relação ao bancos selecionados
            BusqueIdMovimentacoesBanco();

            var movimentacaoItensBanco = new ServicoItemMovimentacaoBanco().ConsulteListaItens(new MovimentacaoBanco { Id = txtId.Text.ToInt() },
                                                                                               dataInicial, dataFinal,
                                                                                                (EnumOrigemMovimentacaoBanco)cboOrigemMovimentacao.EditValue,
                                                                                                txtDescricaoDaMovimentacao.Text,
                                                                                                (EnumTipoMovimentacaoBanco)cboTiposMovimentacoes.EditValue,
                                                                                                txtNumeroDocumento.Text,
                                                                                                new Pessoa { Id = iDParceiro },
                                                                                                new CategoriaFinanceira { Id = iDCategoria }, _listaIdMovimentacoesBancos);

            if (movimentacaoItensBanco != null)
            {
                _listaItensMovimentacoes = movimentacaoItensBanco;

                PreenchaGrid();
                PreenchaResumo();
            }
        }

        private void PesquisarTodos()
        {
            //Vai buscar todos os movimentos em relação ao bancos selecionados
            BusqueIdMovimentacoesBanco();

            _dataFinalPesquisa = null;
            _dataInicialPesquisa = null;

            var movimentacaoItensBanco = new ServicoItemMovimentacaoBanco().ConsulteListaItens(new MovimentacaoBanco { Id = txtId.Text.ToInt() },
                                                                                                null, null,
                                                                                                EnumOrigemMovimentacaoBanco.TODOS,
                                                                                                string.Empty,
                                                                                                EnumTipoMovimentacaoBanco.TODAS,
                                                                                                string.Empty, null, null, _listaIdMovimentacoesBancos);

            if (movimentacaoItensBanco != null)
            {
                _listaItensMovimentacoes = movimentacaoItensBanco;

                PreenchaGrid();
                PreenchaResumo();
            }
        }

        private void InsiraEAtualizeItem()
        {
            if (!ValidouCadastroEAtualizao()) return;

            Action actionCadastroItemBanco = () =>
            {
                if (_IdItemMovimento == 0)
                {
                    CadastreItem();
                }
                else
                {
                    AtualizeItem();
                }

                PreenchaGrid();
                PreenchaResumo();

                LimpeCamposItem();

                cboTiposMovimentacoes.Focus();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionCadastroItemBanco);
        }

        private bool BancoEstahAberto()
        {
            if (_movimentacaoBanco == null) return false;

            if (_movimentacaoBanco.Status == EnumStatusMovimentacaoCaixa.ABERTO)
                return true;

            return false;
        }

        private bool ValidouCadastroEAtualizao()
        {
            if (!BancoEstahAberto())
            {
                MessageBox.Show("O Banco escolhido não está aberto. Faça a abertura do mesmo para continuar!", "Movimentações Bancárias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (txtDataHoraMovimento.Text == string.Empty)
            {
                MessageBox.Show("Informe uma Data para o Movimento.", "Movimentações Bancárias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cboOrigemMovimentacao.EditValue == null)
            {
                MessageBox.Show("Informe a Origem/Status do Movimento.", "Movimentações Bancárias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if ((EnumOrigemMovimentacaoBanco)cboOrigemMovimentacao.EditValue == EnumOrigemMovimentacaoBanco.NENHUM ||
                (EnumOrigemMovimentacaoBanco)cboOrigemMovimentacao.EditValue == EnumOrigemMovimentacaoBanco.TODOS ||
                (EnumOrigemMovimentacaoBanco)cboOrigemMovimentacao.EditValue == EnumOrigemMovimentacaoBanco.IGNORADO)

            {
                MessageBox.Show("Informe outra Origem/Status do Movimento, pois esta, está fora do contexto.", "Movimentações Bancárias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cboTiposMovimentacoes.EditValue == null)
            {
                MessageBox.Show("Informe o Tipo de Movimento.", "Movimentações Bancárias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if ((EnumTipoMovimentacaoBanco)cboTiposMovimentacoes.EditValue == EnumTipoMovimentacaoBanco.TODAS)
            {
                MessageBox.Show("Informe o tipo de movimento como: <Entrada> ou <Saída>.", "Movimentações Bancárias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (txtValor.Text.ToDouble() == 0)
            {
                MessageBox.Show("Informe o Valor do Movimento.", "Movimentações Bancárias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cboCategoriaFinanceira.EditValue == null)
            {
                MessageBox.Show("Informe a Categoria Financeira para continuar.", "Movimentações Bancárias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void AtualizeContaPagarReceber(ItemMovimentacaoBanco itemMov)
        {
            if (itemMov.ContaPagarReceber == null) return;

            ServicoContasPagarReceber servicoContaPagarReceber = new ServicoContasPagarReceber();

            var contaPagarReceber = servicoContaPagarReceber.Consulte(itemMov.ContaPagarReceber.Id);

            if (contaPagarReceber == null) return;

            EnumStatusContaPagarReceber statusAntesDeAtualizar = contaPagarReceber.Status;

            contaPagarReceber.Status = EnumStatusContaPagarReceber.CONCILIADOQUITADO;
            contaPagarReceber.CategoriaFinanceira = itemMov.Categoria;
            contaPagarReceber.BancoParaMovimento = itemMov.MovimentacaoBanco.Banco;
            contaPagarReceber.DataPagamento = itemMov.DataHoraLancamento;
            contaPagarReceber.ValorPago = txtValor.Text.ToDouble();

            if (contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER)
            {
                new ServicoContasReceber().Atualize(contaPagarReceber);

                //Caso a conciliação for de Cartão, o sistema vai calcular o valor da taxa de adm e criar um lançamento
                if (contaPagarReceber.FormaPagamento.TipoFormaPagamento != EnumTipoFormaPagamento.CARTAOCREDITO ||
                    contaPagarReceber.FormaPagamento.TipoFormaPagamento != EnumTipoFormaPagamento.CARTAODEBITO)
                {
                    new ServicoItemMovimentacaoBanco().CalculeDespesasCartoes(contaPagarReceber, false,
                                                   EnumTipoOperacaoContasPagarReceber.PAGAR, contaPagarReceber.DataPagamento.Value,
                                                   _banco,
                                                   contaPagarReceber.OperadorasCartao,
                                                   contaPagarReceber.ValorPago, new Pessoa { Id = Sessao.PessoaLogada.Id });
                }

            }
            else
                new ServicoContasPagar().Atualize(contaPagarReceber);

            if (statusAntesDeAtualizar != EnumStatusContaPagarReceber.CONCILIADOQUITADO)
            {
                var objetoContasPagarReceberParcial = retorneListaHistoricoDePagamentos(contaPagarReceber.DataPagamento.Value,
                                                                   txtValor.Text.ToDouble(),
                                                                   contaPagarReceber.FormaPagamento, contaPagarReceber, false);

                contaPagarReceber.ListaContasPagarReceberParcial.Add(objetoContasPagarReceberParcial);

                ServicoContasPagarReceberPagamento servicoContasPagarReceberPagamento = new ServicoContasPagarReceberPagamento();

                servicoContasPagarReceberPagamento.CadastreLista(contaPagarReceber.ListaContasPagarReceberParcial.ToList());
                servicoContasPagarReceberPagamento.Cadastre(objetoContasPagarReceberParcial);
            }
        }

        private ContaPagarReceberPagamento retorneListaHistoricoDePagamentos(DateTime DataPagamento, double Valor, FormaPagamento
                                                                             formaDePagamento, ContaPagarReceber ContaPagarReceber, bool EstahEstornado)
        {
            ContaPagarReceberPagamento item = new ContaPagarReceberPagamento();

            List<ContaPagarReceberPagamento> Lista = new List<ContaPagarReceberPagamento>();

            item.DataPagamento = DataPagamento;
            item.Valor = Valor;
            item.Observacoes = "Título Quitado/Conciliado através da movimentação de bancos";
            item.Responsavel = new ServicoPessoa().Consulte(Sessao.PessoaLogada.Id);
            item.FormaPagamento = formaDePagamento;
            item.ContaPagarReceber = ContaPagarReceber;
            item.EstahEstornado = EstahEstornado;

            return item;
        }

        private void CadastreItem()
        {
            ItemMovimentacaoBanco itemMovimentacaoBanco = new ItemMovimentacaoBanco();

            CategoriaFinanceira categoria = new CategoriaFinanceira();
            categoria.Id = cboCategoriaFinanceira.EditValue.ToInt();
            categoria.Descricao = cboCategoriaFinanceira.Text;

            Pessoa parceiro = new Pessoa();
            parceiro.Id = cboParceiro.EditValue.ToInt();

            itemMovimentacaoBanco.EstahEstornado = false;
            itemMovimentacaoBanco.Categoria = categoria.Id == 0 ? null : categoria;
            itemMovimentacaoBanco.Parceiro = parceiro.Id == 0 ? null : parceiro;
            itemMovimentacaoBanco.DescricaoDaMovimentacao = txtDescricaoDaMovimentacao.Text;
            itemMovimentacaoBanco.DataHoraLancamento = txtDataHoraMovimento.DateTime;
            itemMovimentacaoBanco.MovimentacaoBanco = _movimentacaoBanco;
            itemMovimentacaoBanco.TipoMovimentacao = (EnumTipoMovimentacaoBanco)cboTiposMovimentacoes.EditValue;
            itemMovimentacaoBanco.OrigemMovimentacaoBanco = (EnumOrigemMovimentacaoBanco)cboOrigemMovimentacao.EditValue;
            itemMovimentacaoBanco.Valor = txtValor.Text.ToDouble();
            itemMovimentacaoBanco.NumeroDocumentoOrigem = txtNumeroDocumento.Text != string.Empty ? txtNumeroDocumento.Text : null;

            //Usuário e Data de atualização / cadastro
            itemMovimentacaoBanco.DataAtualizacao = DateTime.Now;
            itemMovimentacaoBanco.UsuarioAtualizacao = new Pessoa { Id = Sessao.PessoaLogada.Id };

            if (_itemMovimentoEmEdicao != null && _itemMovimentoEmEdicao.ContaPagarReceber != null &&
                _itemMovimentoEmEdicao.ContaPagarReceber.Id != 0)
                itemMovimentacaoBanco.ContaPagarReceber = _itemMovimentoEmEdicao.ContaPagarReceber;

            //Atualiza contas Pagar/Receber****
            AtualizeContaPagarReceber(itemMovimentacaoBanco);

            ServicoItemMovimentacaoBanco servicoItemMovimentacaoBanco = new ServicoItemMovimentacaoBanco();
            servicoItemMovimentacaoBanco.Cadastre(itemMovimentacaoBanco);

            _listaItensMovimentacoes.Add(itemMovimentacaoBanco);
        }

        private void AtualizeItem()
        {
            ServicoItemMovimentacaoBanco servicoItemMovimentacaoBanco = new ServicoItemMovimentacaoBanco();

            var itemMovimentacaoBanco = servicoItemMovimentacaoBanco.Consulte(_IdItemMovimento);

            CategoriaFinanceira categoria = new CategoriaFinanceira();
            categoria.Id = cboCategoriaFinanceira.EditValue.ToInt();
            categoria.Descricao = cboCategoriaFinanceira.Text;

            Pessoa parceiro = new Pessoa();
            parceiro.Id = cboParceiro.EditValue.ToInt();

            itemMovimentacaoBanco.EstahEstornado = false;
            itemMovimentacaoBanco.Categoria = categoria.Id == 0 ? null : categoria;
            itemMovimentacaoBanco.Parceiro = parceiro.Id == 0 ? null : parceiro;
            itemMovimentacaoBanco.DescricaoDaMovimentacao = txtDescricaoDaMovimentacao.Text;

            itemMovimentacaoBanco.DataHoraLancamento = (EnumOrigemMovimentacaoBanco)cboOrigemMovimentacao.EditValue ==
                                                        EnumOrigemMovimentacaoBanco.INFORMACAOMANUAL ?
                                                        txtDataHoraMovimento.DateTime : itemMovimentacaoBanco.DataHoraLancamento;

            itemMovimentacaoBanco.MovimentacaoBanco = _movimentacaoBanco;
            itemMovimentacaoBanco.TipoMovimentacao = (EnumTipoMovimentacaoBanco)cboTiposMovimentacoes.EditValue;
            itemMovimentacaoBanco.OrigemMovimentacaoBanco = (EnumOrigemMovimentacaoBanco)cboOrigemMovimentacao.EditValue;
            itemMovimentacaoBanco.Valor = txtValor.Text.ToDouble();
            itemMovimentacaoBanco.NumeroDocumentoOrigem = txtNumeroDocumento.Text != string.Empty ? txtNumeroDocumento.Text : null;

            //Usuário e Data de atualização / cadastro
            itemMovimentacaoBanco.DataAtualizacao = DateTime.Now;
            itemMovimentacaoBanco.UsuarioAtualizacao = new Pessoa { Id = Sessao.PessoaLogada.Id };

            itemMovimentacaoBanco.Id = _IdItemMovimento;

            if (_itemMovimentoEmEdicao != null && _itemMovimentoEmEdicao.ContaPagarReceber != null &&
                _itemMovimentoEmEdicao.ContaPagarReceber.Id > 0)
                itemMovimentacaoBanco.ContaPagarReceber = _itemMovimentoEmEdicao.ContaPagarReceber;

            //Atualiza contas Pagar/Receber****
            if (!_itemMovimentoEmEdicao.DescricaoDaMovimentacao.ToLower().Contains("taxa de administração de cartão"))
                AtualizeContaPagarReceber(itemMovimentacaoBanco);

            servicoItemMovimentacaoBanco.Atualize(itemMovimentacaoBanco);

            AtualizaListaMovimentacaoParaOGrid();
        }

        private void AtualizaListaMovimentacaoParaOGrid()
        {
            //Atualiza lista de movimentação para o Grid
            ServicoMovimentacaoBanco servicoMovimentacaoBanco = new ServicoMovimentacaoBanco();
            MovimentacaoBanco movimentacaoBanco = servicoMovimentacaoBanco.ConsulteBancoAberto(_banco);
            _listaItensMovimentacoes = movimentacaoBanco.ListaItensBanco.ToList();
        }

        private void PesquiseEPreenchaBancoAberto()
        {
            ServicoBancoParaMovimento servicoBanco = new ServicoBancoParaMovimento();

            BancoParaMovimento banco = servicoBanco.ConsulteBanco(true);

            if (banco == null)
            {
                banco = servicoBanco.ConsulteBanco();
            }

            PreenchaCamposBanco(banco);

            if (banco != null)
            {
                ServicoMovimentacaoBanco servicoMovimentacaoBanco = new ServicoMovimentacaoBanco();

                MovimentacaoBanco movimentacaoBanco = servicoMovimentacaoBanco.ConsulteBancoAberto(banco);

                PreenchaCamposMovimentacaoBanco(movimentacaoBanco);
            }
        }

        private void PreenchaCamposBanco(BancoParaMovimento banco)
        {
            _banco = banco;

            if (banco != null)
            {
                txtIdBanco.Text = banco.Id.ToString();
                txtDescricaoBanco.Text = banco.NomeBanco;
                txtUsuarioBanco.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.Razao;
            }
            else
            {
                MessageBox.Show("Este usuário não contém um banco cadastrado!", "Não existe banco este usuário");

                this.FecharFormulario();
            }
        }

        private void PreenchaCamposMovimentacaoBanco(MovimentacaoBanco movimentacaoBanco)
        {
            if (movimentacaoBanco != null)
            {
                _movimentacaoBanco = movimentacaoBanco;
                _listaItensMovimentacoes = movimentacaoBanco.ListaItensBanco.ToList();

                txtId.Text = movimentacaoBanco.Id.ToString();
                txtDataBanco.Text = movimentacaoBanco.DataHoraAbertura.GetValueOrDefault().ToString("dd/MM/yyyy");
                txtStatusBanco.Text = movimentacaoBanco.Status.Descricao();

                if (movimentacaoBanco.Status == EnumStatusMovimentacaoCaixa.FECHADO && !Sessao.GrupoAcesso.Tesoureiro)
                {
                    pnlInserirEstornarItemMovimentacao.Enabled = false;
                }
                else
                {
                    pnlInserirEstornarItemMovimentacao.Enabled = true;
                }

                if (movimentacaoBanco.Status == EnumStatusMovimentacaoCaixa.ABERTO)
                {
                    btnFecharBanco.Visible = true;
                    btnAbrirBanco.Visible = false;
                }
                else
                {
                    btnAbrirBanco.Visible = true;
                    btnFecharBanco.Visible = false;
                }

                btnStatusFechado.Visible = _movimentacaoBanco.Status == EnumStatusMovimentacaoCaixa.FECHADO ? true : false;
                btnStatusAberto.Visible = _movimentacaoBanco.Status == EnumStatusMovimentacaoCaixa.ABERTO ? true : false;

                PreenchaGrid();
                PreenchaResumo();
            }
        }

        private void PreenchaCboTipoMovimentacao()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoMovimentacaoBanco>();

            cboTiposMovimentacoes.Properties.DisplayMember = "Descricao";
            cboTiposMovimentacoes.Properties.ValueMember = "Valor";
            cboTiposMovimentacoes.Properties.DataSource = lista;

            cboTiposMovimentacoes.EditValue = EnumTipoMovimentacaoBanco.ENTRADA;

            PreenchaCboCategorias();
        }

        private void PreenchaCboOrigemMovimentacao()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumOrigemMovimentacaoBanco>();

            cboOrigemMovimentacao.Properties.DisplayMember = "Descricao";
            cboOrigemMovimentacao.Properties.ValueMember = "Valor";
            cboOrigemMovimentacao.Properties.DataSource = lista;

            cboOrigemMovimentacao.EditValue = EnumOrigemMovimentacaoBanco.INFORMACAOMANUAL;
        }

        private void PreenchaCboCategorias()
        {
            if ((EnumTipoMovimentacaoBanco)cboTiposMovimentacoes.EditValue == EnumTipoMovimentacaoBanco.TODAS) return;

            List<CategoriaFinanceira> categoria = new List<CategoriaFinanceira>();

            if ((EnumTipoMovimentacaoBanco)cboTiposMovimentacoes.EditValue == EnumTipoMovimentacaoBanco.ENTRADA)
            {
                categoria = new ServicoCategoria().ConsulteLista(string.Empty, null, "A", EnumTipoCategoria.RECEITA);
            }
            else
            {
                categoria = new ServicoCategoria().ConsulteLista(string.Empty, null, "A", EnumTipoCategoria.DESPESA);
            }

            categoria.Insert(0, null);

            cboCategoriaFinanceira.Properties.DisplayMember = "Descricao";
            cboCategoriaFinanceira.Properties.ValueMember = "Id";
            cboCategoriaFinanceira.Properties.DataSource = categoria;

            if (cboCategoriaFinanceira.EditValue != null)
            {
                if (!categoria.Exists(categ => categ != null && categ.Id == cboCategoriaFinanceira.EditValue.ToInt()))
                {
                    cboCategoriaFinanceira.EditValue = null;
                }
            }
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

            if (cboCategoriaFinanceira.EditValue != null)
            {
                if (!listaCombo.Exists(parc => parc != null && parc.Valor == cboParceiro.EditValue))
                {
                    cboParceiro.EditValue = null;
                }
            }
        }

        private void PreenchaGrid()
        {
            _listaItensMovimentacoes = _listaItensMovimentacoes.OrderBy(x => x.DataHoraLancamento).ToList();

            List<ItemMovimentacaoBancoGrid> listaItemMovimentacaoBancoGrid = new List<ItemMovimentacaoBancoGrid>();

            double saldo = 0;

            foreach (var item in _listaItensMovimentacoes)
            {
                ItemMovimentacaoBancoGrid itemMovimentacaoGrid = new ItemMovimentacaoBancoGrid();

                itemMovimentacaoGrid.DataHora = item.DataHoraLancamento.ToString("dd/MM/yyyy");
                itemMovimentacaoGrid.Categoria = item.Categoria != null ? item.Categoria.Descricao : null;
                itemMovimentacaoGrid.DescricaoMovimentacao = item.DescricaoDaMovimentacao;
                itemMovimentacaoGrid.Id = item.Id;

                if (item.Parceiro != null)
                {
                    if (item.Parceiro.DadosGerais.Razao == null)
                    {
                        var pessoa = new ServicoPessoa().Consulte(item.Parceiro.Id);

                        itemMovimentacaoGrid.Parceiro = pessoa != null ? pessoa.Id + " - " + pessoa.DadosGerais.Razao : string.Empty;
                    }
                    else
                    {
                        itemMovimentacaoGrid.Parceiro = item.Parceiro.DadosGerais.Razao != null ? item.Parceiro.Id + " - " + item.Parceiro.DadosGerais.Razao : string.Empty;
                    }
                }

                itemMovimentacaoGrid.EstahEstornado = item.EstahEstornado;
                itemMovimentacaoGrid.OrigemMovimentacao = item.OrigemMovimentacaoBanco.Descricao();
                itemMovimentacaoGrid.NumeroDocumento = item.NumeroDocumentoOrigem;

                if (item.TipoMovimentacao == EnumTipoMovimentacaoBanco.ENTRADA)
                {
                    itemMovimentacaoGrid.Entrada = item.Valor.ToString("#,###,##0.00");

                    if (!item.EstahEstornado)
                    {
                        saldo += item.Valor;
                    }
                }
                else
                {
                    itemMovimentacaoGrid.Saida = item.Valor.ToString("#,###,##0.00");

                    if (!item.EstahEstornado)
                    {
                        saldo -= item.Valor;
                    }
                }

                itemMovimentacaoGrid.Saldo = saldo.ToString("#,###,##0.00");

                listaItemMovimentacaoBancoGrid.Add(itemMovimentacaoGrid);
            }

            gcItens.DataSource = listaItemMovimentacaoBancoGrid;
            gcItens.RefreshDataSource();


            //if (_listaItensMovimentacoes.Count > 0)
            //{
            //    btnImportar.Visible = true;
            //}
            //else
            //{
            //    btnImportar.Visible = false;
            //}
        }

        private void PreenchaResumo()
        {
            double dinheiroEntrada = 0;

            double dinheiroSaida = 0;

            double dinheiroSaldoFinal = 0;

            foreach (var item in _listaItensMovimentacoes)
            {
                if (item.EstahEstornado)
                {
                    continue;
                }

                if (item.TipoMovimentacao == EnumTipoMovimentacaoBanco.ENTRADA)
                {
                    dinheiroEntrada += item.Valor;
                    dinheiroSaldoFinal += item.Valor;
                }
                else
                {
                    dinheiroSaida += item.Valor;
                    dinheiroSaldoFinal -= item.Valor;
                }
            }


            //txtSaldoInicial.Text = _movimentacaoBanco!=null? _movimentacaoBanco.SaldoInicial.ToDouble() == 0 ? _movimentacaoBanco.SaldoInicial.ToString("0.00") : _movimentacaoBanco.SaldoInicial.ToString("0.00"):"0.00";

            txtEntradas.Text = dinheiroEntrada.ToString("0.00");

            txtSaidas.Text = dinheiroSaida.ToString("0.00");

            txtSaldoFinal.Text = dinheiroSaldoFinal.ToString("0.00");

            //****Foi retirado, pois o saldo inicial deve ser discriminado na planilha ******
            //
            //if (_movimentacaoBanco != null && dinheiroEntrada == 0 && _movimentacaoBanco.SaldoInicial.ToDouble() != 0)
            //{
            //    txtSaldoFinal.Text = (_movimentacaoBanco.SaldoInicial.ToDouble() - dinheiroSaida).ToString("0.00");
            //}
        }

        private ItemMovimentacaoBanco RetorneItemMovimentacaoBancoSelecionado()
        {
            ItemMovimentacaoBanco itemMovimentacaoBanco = null;

            if (_listaItensMovimentacoes != null && _listaItensMovimentacoes.Count > 0)
            {
                itemMovimentacaoBanco = _listaItensMovimentacoes.FirstOrDefault(x => x.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());
            }

            return itemMovimentacaoBanco;
        }

        private void LimpeCamposItem()
        {
            _IdItemMovimento = 0;
            txtDataHoraMovimento.Text = DateTime.Now.ToString("dd/MM/yyyy");
            cboOrigemMovimentacao.EditValue = EnumOrigemMovimentacaoBanco.INFORMACAOMANUAL;
            cboTiposMovimentacoes.EditValue = EnumTipoMovimentacaoBanco.ENTRADA;
            txtDescricaoDaMovimentacao.Text = string.Empty;
            cboCategoriaFinanceira.EditValue = null;
            cboParceiro.EditValue = null;
            txtNumeroDocumento.Text = string.Empty;

            txtValor.Text = string.Empty;

            //Muda o ícone para Inserir
            btnInserirAtualizarItem.Image = Properties.Resources.icones2_19;

            txtDescricaoDaMovimentacao.Focus();
        }

        private void SelecioneItensMovimentacoes()
        {
            if (_listaItensMovimentacoes != null && _listaItensMovimentacoes.Count > 0)
            {
                ServicoItemMovimentacaoBanco servicoItensMovimentacoes = new ServicoItemMovimentacaoBanco();

                var itensMovimentacoes = servicoItensMovimentacoes.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

                if (itensMovimentacoes != null && itensMovimentacoes.Parceiro != null)
                    PreenchaCboParceiro(itensMovimentacoes.Parceiro.DadosGerais.Razao);

                EditeItensMovimentacoes(itensMovimentacoes);
            }
        }

        private void EditeItensMovimentacoes(ItemMovimentacaoBanco itemMovimento)
        {
            _itemMovimentoEmEdicao = itemMovimento;

            if (itemMovimento != null)
            {
                _IdItemMovimento = itemMovimento.Id;
                txtDataHoraMovimento.Text = itemMovimento.DataHoraLancamento.ToString("dd/MM/yyy");
                cboOrigemMovimentacao.EditValue = itemMovimento.OrigemMovimentacaoBanco;
                cboTiposMovimentacoes.EditValue = itemMovimento.TipoMovimentacao;
                txtDescricaoDaMovimentacao.Text = itemMovimento.DescricaoDaMovimentacao;
                txtNumeroDocumento.Text = itemMovimento.NumeroDocumentoOrigem;
                cboCategoriaFinanceira.EditValue = itemMovimento.Categoria != null ? itemMovimento.Categoria.Id : 0;
                cboParceiro.EditValue = itemMovimento.Parceiro != null ? itemMovimento.Parceiro.Id : 0;
                txtValor.Text = itemMovimento.Valor.ToString("0.00");

                //Muda o ícone para atualizar
                btnInserirAtualizarItem.Image = Properties.Resources.icones2_07;
                btnInserirAtualizarItem.Image = Properties.Resources.icon_atualizar;

                txtDescricaoDaMovimentacao.Focus();
            }
            else
            {
                _IdItemMovimento = 0;
                txtDataHoraMovimento.Text = DateTime.Now.ToString("dd/MM/yyyy");
                cboOrigemMovimentacao.EditValue = EnumOrigemMovimentacaoBanco.INFORMACAOMANUAL;
                cboTiposMovimentacoes.EditValue = EnumTipoMovimentacaoBanco.ENTRADA;
                txtDescricaoDaMovimentacao.Text = string.Empty;
                txtNumeroDocumento.Text = string.Empty;
                cboCategoriaFinanceira.EditValue = null;
                cboParceiro.EditValue = null;
                txtValor.Text = string.Empty;

                //Muda o ícone para Inserir
                btnInserirAtualizarItem.Image = Properties.Resources.icones2_19;

                txtDescricaoDaMovimentacao.Focus();
            }
        }

        private ItemMovimentacaoBanco ConvertaContaPagarReceberParaItmMovimentacao(ContaPagarReceber contasPagarReceber)
        {
            ItemMovimentacaoBanco itemMovimento = new ItemMovimentacaoBanco();

            itemMovimento.Id = _IdItemMovimento != 0 ? _IdItemMovimento : 0;

            itemMovimento.DataHoraLancamento = contasPagarReceber.DataVencimento.Value;

            itemMovimento.OrigemMovimentacaoBanco = EnumOrigemMovimentacaoBanco.CONCILIADO;

            itemMovimento.TipoMovimentacao = contasPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ?
                                                    EnumTipoMovimentacaoBanco.ENTRADA : EnumTipoMovimentacaoBanco.SAIDA;

            itemMovimento.DescricaoDaMovimentacao = contasPagarReceber.Historico;
            itemMovimento.NumeroDocumentoOrigem = contasPagarReceber.NumeroDocumento;
            itemMovimento.Categoria = contasPagarReceber.CategoriaFinanceira;
            itemMovimento.Parceiro = contasPagarReceber.Pessoa;

            if (itemMovimento.Parceiro != null)
                PreenchaCboParceiro(itemMovimento.Parceiro.DadosGerais.Razao);

            itemMovimento.Valor = contasPagarReceber.ValorTotal;
            itemMovimento.ContaPagarReceber = contasPagarReceber;

            return itemMovimento;
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ItemMovimentacaoBancoGrid
        {
            public int Id { get; set; }

            public bool EstahEstornado { get; set; }

            public string DataHora { get; set; }

            public string Parceiro { get; set; }

            public string DescricaoMovimentacao { get; set; }

            public string Categoria { get; set; }

            public string Entrada { get; set; }

            public string Saida { get; set; }

            public string Saldo { get; set; }

            public string OrigemMovimentacao { get; set; }

            public string NumeroDocumento { get; set; }
        }

        #endregion

        #region "Metodos Para Conciliacao Bancária"

        private Extract abrirECarregarExtratoOfx()
        {
            if (AbrirOfx.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                caminhoDoArquivoOFX = AbrirOfx.FileName;
            }

            return caminhoDoArquivoOFX != null ? OFXParser.Parser.GenerateExtract(caminhoDoArquivoOFX) : null;
        }

        private void conciliarContasReceber(Extract extratoBancario)
        {
            //Contas a Receber
            foreach (var transacao in extratoBancario.Transactions)
            {
                //Crédito
                if (transacao.TransactionValue > 0)
                    preencheListaConciliacaoContasPagarReceber(transacao.Date.Date, transacao.TransactionValue,
                                                                transacao.Description, transacao.Id, EnumTipoOperacaoContasPagarReceber.RECEBER);
            }
        }

        private void conciliarContasPagar(Extract extratoBancario)
        {
            //Contas a Pagar
            foreach (var transacao in extratoBancario.Transactions)
            {
                //Débito
                if (transacao.TransactionValue < 0)
                    preencheListaConciliacaoContasPagarReceber(transacao.Date.Date, Math.Abs(transacao.TransactionValue),
                                                                transacao.Description, transacao.Id, EnumTipoOperacaoContasPagarReceber.PAGAR);
            }
        }

        private void conciliarMovimentacaoEntrada(Extract extratoBancario)
        {
            //Entrada (Crédito)
            foreach (var transacao in extratoBancario.Transactions)
            {
                if (transacao.TransactionValue > 0)
                    preencheListaConciliacaoMovimentacaoBancaria(transacao.Date.Date, transacao.TransactionValue,
                                                                transacao.Description, transacao.Id, EnumTipoMovimentacaoBanco.ENTRADA);
            }
        }

        private void conciliarMovimentacaoSaida(Extract extratoBancario)
        {
            //Saída (Débito)
            foreach (var transacao in extratoBancario.Transactions)
            {
                if (transacao.TransactionValue < 0)
                    preencheListaConciliacaoMovimentacaoBancaria(transacao.Date.Date, Math.Abs(transacao.TransactionValue),
                                                                transacao.Description, transacao.Id, EnumTipoMovimentacaoBanco.SAIDA);
            }
        }

        private bool UsuarioTemCaixa()
        {
            ServicoCaixa servicoCaixa = new ServicoCaixa();
            var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

            if (caixa == null)
            {
                MessageBox.Show("Seu usuário não contém um caixa.");

                return false;
            }

            ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();
            var movimentacaoCaixa = servicoMovimentacaoCaixa.ConsulteCaixaAberto(caixa);

            if (movimentacaoCaixa == null)
            {
                MessageBox.Show("Você não possui um caixa aberto.");

                return false;
            }

            return true;
        }

        private void ImportarEConciliarExtratoOfx()
        {
            if (!BancoEstahAberto())
            {
                MessageBox.Show("O Banco escolhido não está aberto. Faça a abertura do mesmo para continuar!", "Movimentações Bancárias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                _parametros = new ServicoParametros().ConsulteParametros();

                var extratoBancario = abrirECarregarExtratoOfx();

                if (extratoBancario == null) return;

                if (MessageBox.Show(this, "Você deseja importar todos os " + extratoBancario.Transactions.Count() + " itens do extrato bancário para o Akil?",
                                    "Importação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                if (extratoBancario != null)
                {
                    if (!EhBancoEscolhido(extratoBancario.BankAccount.Bank.Code)) return;

                    //Aciona o cursor... 
                    this.Cursor = Cursors.WaitCursor;

                    _listaConcilicao = new List<ConciliacaoBancaria>();

                    conciliarContasReceber(extratoBancario);

                    conciliarContasPagar(extratoBancario);

                    conciliarMovimentacaoEntrada(extratoBancario);

                    conciliarMovimentacaoSaida(extratoBancario);

                    if (!_parametros.ParametrosFinanceiro.ImportacaoAutomaticaExtrato)
                    {
                        CarregaDadosEncontradosNoAkil();

                        CarregaDadosParaImportacaoEConciliacao(extratoBancario);

                        FormConciliacaoBancaria formConciliacao = new FormConciliacaoBancaria(_listaConcilicaoImportacao, _movimentacaoBanco);

                        var retorno = formConciliacao.ShowDialog();
                    }
                    else
                    {
                        ImporteExtratoParaMovimentacao(extratoBancario);

                        if (_listaConcilicao.Count() > 0)
                        {
                            MessageBox.Show("Foram encontrados lançamentos semelhantes no Akil. Estes não foram Importados! " +
                               "Faça a <Importação> ou <Conciliação> no formulário a seguir...",
                               "Importação de Extrato Bancário", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            FormConciliacaoBancaria formConciliacao = new FormConciliacaoBancaria(_listaConcilicao, _movimentacaoBanco);

                            var retorno = formConciliacao.ShowDialog();
                        }
                    }

                    AtualizaListaMovimentacaoParaOGrid();
                    PreenchaGrid();
                    PreenchaResumo();

                    LimpeCamposItem();

                    cboTiposMovimentacoes.Focus();

                    //MessageBox.Show("Extrato importado com sucesso!", "Importação de Extrato Bancário", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Para o cursor...
                this.Cursor = Cursors.Default;

                return;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                throw ex;
            }
        }

        private void CarregaDadosEncontradosNoAkil()
        {
            if (_listaConcilicao.Count != 0)
            {
                MessageBox.Show("Foram encontrados lançamentos semelhantes no Akil. " +
                                "Faça a importação ou conciliação no formulário a seguir...",
                                "Importação de Extrato Bancário", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                return;

            _listaConcilicaoImportacao = _listaConcilicao;
        }

        private string ConverteParaUTF8(string DescricaoExtrato)
        {
            byte[] utfBytes = Encoding.UTF8.GetBytes(DescricaoExtrato);

            string str = Encoding.UTF8.GetString(utfBytes);
            byte[] iso88591data = Encoding.GetEncoding("ISO-8859-1").GetBytes(str);

            string isocontent = Encoding.GetEncoding("ISO-8859-1").GetString(iso88591data, 0, iso88591data.Length);
            byte[] isobytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(isocontent);
            byte[] ubytes = Encoding.Convert(Encoding.GetEncoding("ISO-8859-1"), Encoding.Unicode, isobytes);

            return Encoding.Unicode.GetString(ubytes, 0, ubytes.Length);
        }

        private void CarregaDadosParaImportacaoEConciliacao(Extract extrato)
        {
            foreach (var item in extrato.Transactions)
            {
                if (!ExisteLancamentoNoExtratoParaConciliacao(item))
                {
                    ConciliacaoBancaria conciliacao = new ConciliacaoBancaria();

                    //Origem_1
                    conciliacao.ChaveOrigem1 = null;
                    conciliacao.Origem1 = null;
                    conciliacao.NumDoc = string.Empty;
                    conciliacao.DescricaoDoc = string.Empty;
                    conciliacao.DataVencimento = null;
                    conciliacao.ValorDoc = null;

                    //Origem_2
                    conciliacao.Origem2 = item.TransactionValue > 0 ? EnumOrigemConciliacaoBancaria.EXTRATOBANCARIOCR : EnumOrigemConciliacaoBancaria.EXTRATOBANCARIODB;
                    conciliacao.NumLancto = item.Id;
                    conciliacao.DescricaoLancto = ConverteParaUTF8(item.Description);
                    conciliacao.DataLancto = item.Date;
                    conciliacao.ValorLancto = Math.Abs(item.TransactionValue);

                    conciliacao.StatusConciliacao = EnumOrigemMovimentacaoBanco.NENHUM;

                    conciliacao.MovimentacaoBanco = _movimentacaoBanco;

                    _listaConcilicaoImportacao.Add(conciliacao);
                }
            }
        }

        private bool ExisteLancamentoNoExtratoParaConciliacao(Transaction itemTransacao)
        {
            if (_listaConcilicao.Exists(x => x.NumLancto == itemTransacao.Id))
            {
                return true;
            }

            return false;
        }

        private void ImporteExtratoParaMovimentacao(Extract extrato)
        {
            foreach (var item in extrato.Transactions)
            {
                if (!ExisteLancamentoNoExtratoParaConciliacao(item))
                {
                    ItemMovimentacaoBanco itemMovimentacaoBanco = new ItemMovimentacaoBanco();

                    itemMovimentacaoBanco.EstahEstornado = false;
                    itemMovimentacaoBanco.Categoria = null;
                    itemMovimentacaoBanco.Parceiro = null;
                    itemMovimentacaoBanco.DescricaoDaMovimentacao = ConverteParaUTF8(item.Description);
                    itemMovimentacaoBanco.MovimentacaoBanco = _movimentacaoBanco;
                    itemMovimentacaoBanco.TipoMovimentacao = item.TransactionValue > 0 ? EnumTipoMovimentacaoBanco.ENTRADA : EnumTipoMovimentacaoBanco.SAIDA;
                    itemMovimentacaoBanco.OrigemMovimentacaoBanco = EnumOrigemMovimentacaoBanco.IMPORTADO;
                    itemMovimentacaoBanco.DataHoraLancamento = item.Date;
                    itemMovimentacaoBanco.Valor = Math.Abs(item.TransactionValue); //Importar com sinal positivo, 
                                                                                   //pois o sistema identifica quando é entrada ou saída.
                    itemMovimentacaoBanco.NumeroDocumentoOrigem = item.Id;

                    //Usuário e Data de atualização / cadastro
                    itemMovimentacaoBanco.DataAtualizacao = DateTime.Now;
                    itemMovimentacaoBanco.UsuarioAtualizacao = new Pessoa { Id = Sessao.PessoaLogada.Id };

                    ServicoItemMovimentacaoBanco servicoItemMovimentacaoBanco = new ServicoItemMovimentacaoBanco();

                    servicoItemMovimentacaoBanco.Cadastre(itemMovimentacaoBanco);
                    _listaItensMovimentacoes.Add(itemMovimentacaoBanco);
                }
            }

            ServicoMovimentacaoBanco servicoMovimentacaoBanco = new ServicoMovimentacaoBanco();

            MovimentacaoBanco movimentacaoBanco = servicoMovimentacaoBanco.ConsulteBancoAberto(_banco);

            _listaItensMovimentacoes = movimentacaoBanco.ListaItensBanco.ToList();

            PreenchaGrid();
            PreenchaResumo();

            LimpeCamposItem();

            cboTiposMovimentacoes.Focus();
        }

        private void preencheListaConciliacaoContasPagarReceber(DateTime dataBuscar, double valorTransacao, string descricaoTransacao,
                                                                string iDTransacao, EnumTipoOperacaoContasPagarReceber tipoOperacao)
        {
            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var listaContaParaBaixar = servicoContasPagarReceber.ConsulteLista(null,
                                        tipoOperacao,
                                        EnumStatusContaPagarReceber.ABERTO, null, null,
                                        EnumDataFiltrarContasPagarReceber.VENCIMENTO,
                                        dataBuscar, dataBuscar);

            foreach (var item in listaContaParaBaixar)
            {
                if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO) continue;

                if (item.ValorTotal >= item.ValorParcela)
                {
                    if ((item.ValorTotal - item.ValorParcela) == 0)
                    {
                        if (item.ValorTotal == valorTransacao)
                        {
                            PreencheItemConciliacao(tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? EnumOrigemConciliacaoBancaria.ARECEBER :
                                                    EnumOrigemConciliacaoBancaria.APAGAR, item.NumeroDocumento.ToString(), item.Historico, item.DataVencimento.Value,
                                                    item.ValorTotal,
                                                    tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? EnumOrigemConciliacaoBancaria.EXTRATOBANCARIOCR :
                                                    EnumOrigemConciliacaoBancaria.EXTRATOBANCARIODB,
                                                    iDTransacao, descricaoTransacao,
                                                    dataBuscar, valorTransacao, EnumOrigemMovimentacaoBanco.NENHUM, item.Id);
                            continue;
                        }

                    }
                    else if ((item.ValorTotal - item.ValorParcela) == valorTransacao)
                    {
                        if (item.ValorTotal == valorTransacao)
                        {
                            PreencheItemConciliacao(tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? EnumOrigemConciliacaoBancaria.ARECEBER :
                                                    EnumOrigemConciliacaoBancaria.APAGAR, item.NumeroDocumento.ToString(), item.Historico, item.DataVencimento.Value,
                                                    item.ValorTotal,
                                                    tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? EnumOrigemConciliacaoBancaria.EXTRATOBANCARIOCR :
                                                    EnumOrigemConciliacaoBancaria.EXTRATOBANCARIODB,
                                                    iDTransacao, descricaoTransacao,
                                                    dataBuscar, valorTransacao, EnumOrigemMovimentacaoBanco.NENHUM, item.Id);
                            continue;
                        }

                    }
                }
                else
                {
                    if ((item.ValorParcela - item.ValorTotal) == 0)
                    {
                        if (item.ValorParcela == valorTransacao)
                        {
                            PreencheItemConciliacao(tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? EnumOrigemConciliacaoBancaria.ARECEBER :
                                                    EnumOrigemConciliacaoBancaria.APAGAR, item.NumeroDocumento.ToString(), item.Historico, item.DataVencimento.Value,
                                                    item.ValorParcela,
                                                    tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? EnumOrigemConciliacaoBancaria.EXTRATOBANCARIOCR :
                                                    EnumOrigemConciliacaoBancaria.EXTRATOBANCARIODB,
                                                    iDTransacao, descricaoTransacao,
                                                    dataBuscar, valorTransacao, EnumOrigemMovimentacaoBanco.NENHUM, item.Id);
                            continue;
                        }
                    }
                    else if ((item.ValorParcela - item.ValorTotal) == valorTransacao)
                    {
                        if (item.ValorTotal == valorTransacao)
                        {
                            PreencheItemConciliacao(tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? EnumOrigemConciliacaoBancaria.ARECEBER :
                                                    EnumOrigemConciliacaoBancaria.APAGAR, item.NumeroDocumento.ToString(), item.Historico, item.DataVencimento.Value,
                                                    item.ValorTotal,
                                                    tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? EnumOrigemConciliacaoBancaria.EXTRATOBANCARIOCR :
                                                    EnumOrigemConciliacaoBancaria.EXTRATOBANCARIODB,
                                                    iDTransacao, descricaoTransacao,
                                                    dataBuscar, valorTransacao, EnumOrigemMovimentacaoBanco.NENHUM, item.Id);
                            continue;
                        }
                    }
                }

                FazerBuscaApenasPorValorPagarReceber(valorTransacao, tipoOperacao, iDTransacao, descricaoTransacao, dataBuscar);
            }
        }

        private void FazerBuscaApenasPorValorPagarReceber(double valorTransacao, EnumTipoOperacaoContasPagarReceber tipoOperacao,
                                                            string iDTransacao, string descricaoTransacao, DateTime dataBuscar)
        {
            var diasAntes = _parametros.ParametrosFinanceiro.DiasAntes != 0 ? _parametros.ParametrosFinanceiro.DiasAntes : 7;
            var diasDepois = _parametros.ParametrosFinanceiro.DiasDepois != 0 ? _parametros.ParametrosFinanceiro.DiasDepois : 40;

            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();
            var dataInicial = dataBuscar.AddDays(-diasAntes);
            var dataFinal = dataBuscar.AddDays(diasDepois);

            var listaContaParaBaixar = servicoContasPagarReceber.ConsulteLista(null,
                                        tipoOperacao,
                                        EnumStatusContaPagarReceber.ABERTO, null, null,
                                        EnumDataFiltrarContasPagarReceber.VENCIMENTO, dataInicial, dataFinal, valorTransacao);

            foreach (var item in listaContaParaBaixar)
            {
                PreencheItemConciliacao(tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? EnumOrigemConciliacaoBancaria.ARECEBER :
                                                   EnumOrigemConciliacaoBancaria.APAGAR, item.NumeroDocumento.ToString(), item.Historico, item.DataVencimento.Value,
                                                   item.ValorTotal,
                                                   tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? EnumOrigemConciliacaoBancaria.EXTRATOBANCARIOCR :
                                                    EnumOrigemConciliacaoBancaria.EXTRATOBANCARIODB,
                                                   iDTransacao, descricaoTransacao,
                                                   dataBuscar, valorTransacao, EnumOrigemMovimentacaoBanco.NENHUM, item.Id);
            }
        }

        private void preencheListaConciliacaoMovimentacaoBancaria(DateTime dataBuscar, double valorTransacao, string descricaoTransacao,
                                                               string iDTransacao, EnumTipoMovimentacaoBanco tipoOperacao)
        {

            ServicoItemMovimentacaoBanco servicoItemMovimentacao = new ServicoItemMovimentacaoBanco();

            var listaMovimentoComparar = servicoItemMovimentacao.ConsulteListaItens(_movimentacaoBanco, dataBuscar, dataBuscar, EnumOrigemMovimentacaoBanco.TODOS, string.Empty, tipoOperacao,
                                                                                    string.Empty, null, null, _listaIdMovimentacoesBancos);

            foreach (var item in listaMovimentoComparar)
            {
                if (item.EstahEstornado == true) continue;

                if (item.Valor == valorTransacao)
                {
                    PreencheItemConciliacao(tipoOperacao == EnumTipoMovimentacaoBanco.ENTRADA ? EnumOrigemConciliacaoBancaria.MOVIMENTACAOBANCARIACR :
                                            EnumOrigemConciliacaoBancaria.MOVIMENTACAOBANCARIADB, item.NumeroDocumentoOrigem.ToString(), item.DescricaoDaMovimentacao,
                                            item.DataHoraLancamento, valorTransacao,
                                            tipoOperacao == EnumTipoMovimentacaoBanco.ENTRADA ? EnumOrigemConciliacaoBancaria.EXTRATOBANCARIOCR :
                                                    EnumOrigemConciliacaoBancaria.EXTRATOBANCARIODB,
                                            iDTransacao, descricaoTransacao,
                                            item.DataHoraLancamento, valorTransacao, EnumOrigemMovimentacaoBanco.NENHUM, item.Id);
                }
            }
        }

        private void PreencheItemConciliacao(EnumOrigemConciliacaoBancaria origem1, string NumDoc, string descricaoDoc, DateTime dataVencimento,
                                                double valorDoc,
                                                EnumOrigemConciliacaoBancaria origem2, string NumLancto, string descricaoLancto, DateTime dataLancto,
                                                double valorLancto, EnumOrigemMovimentacaoBanco status, int ChaveOrigem1)

        {
            ConciliacaoBancaria conciliacao = new ConciliacaoBancaria();

            //Origem_1
            conciliacao.ChaveOrigem1 = ChaveOrigem1;
            conciliacao.Origem1 = origem1;
            conciliacao.NumDoc = NumDoc;
            conciliacao.DescricaoDoc = descricaoDoc;
            conciliacao.DataVencimento = dataVencimento;
            conciliacao.ValorDoc = valorDoc;

            //Origem_2
            conciliacao.Origem2 = origem2;
            conciliacao.NumLancto = NumLancto;
            conciliacao.DescricaoLancto = descricaoLancto;
            conciliacao.DataLancto = dataLancto;
            conciliacao.ValorLancto = valorLancto;

            conciliacao.StatusConciliacao = EnumOrigemMovimentacaoBanco.NENHUM;

            conciliacao.MovimentacaoBanco = _movimentacaoBanco;

            if (!_listaConcilicao.Exists(x => x.NumDoc.Equals(NumDoc)) && !_listaConcilicao.Exists(x => x.ValorDoc.Equals(valorDoc)))
                _listaConcilicao.Add(conciliacao);
        }

        private bool EhBancoEscolhido(int numeroBanco)
        {

            var conta = new ServicoContaBancaria().Consulte(_banco.ContaBancaria.Id);

            if (numeroBanco != conta.Agencia.Banco.Codigo.ToInt())
            {
                MessageBox.Show("O Extrato Bancário que está sendo importado não é do Banco Escolhido. Banco Escolhido: "
                    + conta.Agencia.Banco.Codigo + ". " + "Banco a ser Importado: " + numeroBanco + ". ", "Selecione o Banco Correto!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void ImportacoesAssociadas()
        {
            if (!BancoEstahAberto())
            {
                MessageBox.Show("O Banco escolhido não está aberto. Faça a abertura do mesmo para continuar!", "Movimentações Bancárias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Cursor = Cursors.WaitCursor;

            _listaConcilicao = new List<ConciliacaoBancaria>();

            FormConciliacaoBancaria formConciliacao = new FormConciliacaoBancaria(_listaConcilicao, _movimentacaoBanco);

            var retorno = formConciliacao.ShowDialog();

            AtualizaListaMovimentacaoParaOGrid();
            PreenchaGrid();
            PreenchaResumo();

            Cursor = Cursors.Default;

            LimpeCamposItem();

            cboTiposMovimentacoes.Focus();
        }

        private void LimparSelecaoBancos()
        {
            btnInserirAtualizarItem.Enabled = true;
            btnEstornarItem.Enabled = true;
            btnImportar.Enabled = true;
            btnPagarReceber.Enabled = true;
            btnAbrirBanco.Enabled = true;
            btnFecharBanco.Enabled = true;
            btnDadosCruzados.Enabled = true;
            btnStatusAberto.Enabled = true;
            btnStatusFechado.Enabled = true;
            //btnPesquisarBanco.Enabled = true;

            btnEscolherBancos.Text = "Escolher Bancos ...";

            btnLimparSelecao.Enabled = false;

            _listaIdMovimentacoesBancos = new List<int>();
            _listaBancos = new List<BancoParaMovimento>();
            _listaBancosRelatorio = new List<string>();
        }

        private void AtivarSelecaoBancos()
        {
            foreach (var item in _listaBancos)
            {
                var descricaoBanco = new ServicoBancoParaMovimento().Consulte(item.Id).NomeBanco;
                _listaBancosRelatorio.Add(descricaoBanco);
            }

            btnInserirAtualizarItem.Enabled = false;
            btnEstornarItem.Enabled = false;
            btnImportar.Enabled = false;
            btnPagarReceber.Enabled = false;
            btnAbrirBanco.Enabled = false;
            btnFecharBanco.Enabled = false;
            btnDadosCruzados.Enabled = false;
            btnStatusAberto.Enabled = false;
            btnStatusFechado.Enabled = false;
            //btnPesquisarBanco.Enabled = false;

            btnEscolherBancos.Text = "SELECIONADO ...";

            btnLimparSelecao.Enabled = true;
        }


        #endregion

        private void cboParceiro_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtValor_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}