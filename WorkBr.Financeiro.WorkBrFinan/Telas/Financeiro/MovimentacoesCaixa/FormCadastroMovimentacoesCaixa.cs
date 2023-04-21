using System;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Infraestrutura.Negocio.Utils;
using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using System.Linq;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Servico;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Negocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Report.RelatoriosDevExpress.Financeiro;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.CategoriaServ;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.View.Telas.Financeiro.Categorias;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Programax.Easy.View.Telas.Financeiro.MovimentacoesCaixa
{
    public partial class FormCadastroMovimentacoesCaixa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private MovimentacaoCaixa _movimentacaoCaixa;
        private List<ItemMovimentacaoCaixa> _listaItensMovimentacoes;
        private Caixa _caixa;
        private Parametros _parametros;
        private string ConectionString;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroMovimentacoesCaixa()
        {
            InitializeComponent();
            OrganizarControlesConciliacaoDesabilitada();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnAdicionarCategoria_Click(object sender, EventArgs e)
        {
            FormCadastroCategoriasFinanceiras formCategoria = new FormCadastroCategoriasFinanceiras();
            formCategoria.ShowDialog();

            PreenchaCboCategorias();

            cboCategoriaFinanceira.EditValue = null;
        }

        private void FormCadastroMovimentacoesCaixa_Load(object sender, EventArgs e)
        {
            _listaItensMovimentacoes = new List<ItemMovimentacaoCaixa>();

            PreenchaCboTipoMovimentacao();
            PreenchaCboFormasPagamentos();

            PesquiseEPreenchaCaixaAberto();

            this.ActiveControl = cboTiposMovimentacoes;
        }

        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InsiraItem();
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
            InsiraItem();
        }

        private void btnEstornarItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja estornar este item?", "Estornar item", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            Action actionEstornoItemCaixa = () =>
            {
                var itemMovimentacaoCaixa = RetorneItemMovimentacaoCaixaSelecionado();
                
                if (itemMovimentacaoCaixa.HistoricoMovimentacoes == "Saldo inicial dinheiro na abertura." || itemMovimentacaoCaixa.HistoricoMovimentacoes == "Saldo inicial cheque na abertura.")
                {
                    MessageBox.Show("Não é possível estornar o Saldo Inicial de abertura!", "Estornando item de caixa", MessageBoxButtons.OK, MessageBoxIcon.Stop);                    
                    return;
                }

                if ((itemMovimentacaoCaixa.OrigemMovimentacaoCaixa == EnumOrigemMovimentacaoCaixa.CONTAPAGAR ||
                    itemMovimentacaoCaixa.OrigemMovimentacaoCaixa == EnumOrigemMovimentacaoCaixa.CONTARECEBER) &&
                    itemMovimentacaoCaixa.NumeroDocumentoOrigem != null)
                {
                    if (MessageBox.Show("Existe um pagamento registrado para esta movimentação, ele também será estornado.\n\nDeseja Continuar?", "Estornar Título", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }
                                
                ServicoItemMovimentacaoCaixa servicoItemMovimentacaoCaixa = new ServicoItemMovimentacaoCaixa();

                servicoItemMovimentacaoCaixa.EstorneItemMovimentacaoCaixa(itemMovimentacaoCaixa);

                _listaItensMovimentacoes
                    .FindAll(item => item.NumeroDocumentoOrigem != null &&
                                            item.NumeroDocumentoOrigem == itemMovimentacaoCaixa.NumeroDocumentoOrigem &&
                                            item.OrigemMovimentacaoCaixa == itemMovimentacaoCaixa.OrigemMovimentacaoCaixa)
                    .ForEach(item => item.EstahEstornado = true);

                PreenchaGrid();
                PreenchaResumo();
                AlteraPedidoStatus(itemMovimentacaoCaixa.NumeroDocumentoOrigem.ToInt());
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionEstornoItemCaixa);
            
        }
        private void AlteraPedidoStatus(int idPedidoDeVenda)
        {
            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

            var item = conexoes.Conexoes[IndiceBancoDados];

            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
            }
            else
            {
                ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverSecundarioOnline)
                {
                    StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }


            }




            using (var conn = new MySqlConnection(ConectionString))

            {
                conn.Open();

                string Sql = "update historicosatendimento set hisat_status= " + 2 +

                            " where hisat_novo_pedido_id=" + idPedidoDeVenda;

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                conn.Close();

                conn.Open();

                string SqlII = "update pedidosvendas set PEDIDO_ESTAH_PAGO= " + 0 +

                          " where pedido_id=" + idPedidoDeVenda;

                MySqlCommand MyCommandII = new MySqlCommand(SqlII, conn);
                MySqlDataReader MyReader3;


                var returnValueII = MyCommandII.ExecuteReader();


            }

        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnAbrirCaixa_Click(object sender, EventArgs e)
        {
            FormAberturaCaixa formAberturaCaixa = new FormAberturaCaixa();

            var movimentacaoAbertura = formAberturaCaixa.AbrirCaixa(_caixa);

            if (movimentacaoAbertura != null)
            {
                PreenchaCamposMovimentacaoCaixa(movimentacaoAbertura);
            }
        }

        private void btnFecharCaixa_Click(object sender, EventArgs e)
        {
            FormFechamentoCaixa formFechamentoCaixa = new FormFechamentoCaixa();

            _movimentacaoCaixa.SaldoFinalDinheiro = txtDinheiroSaldoFinal.Text.ToDouble();
            _movimentacaoCaixa.SaldoFinalCheque = txtChequeSaldoFinal.Text.ToDouble();

            var movimentacaoCaixa = formFechamentoCaixa.FecharCaixa(_movimentacaoCaixa, txtDinheiroSaldoFinal.Text.ToDouble());

            if (movimentacaoCaixa != null)
            {
                PreenchaCamposMovimentacaoCaixa(movimentacaoCaixa);
            }
        }

        private void btnCancelarItem_Click(object sender, EventArgs e)
        {
            LimpeCamposItem();

            cboTiposMovimentacoes.Focus();
        }

        private void btnPesquisarCaixa_Click(object sender, EventArgs e)
        {
            var formPequisaMovimentacoesCaixa = new FormPesquisaMovimentacoesCaixa();

            var movimentacaoCaixa = formPequisaMovimentacoesCaixa.PesquiseCaixa(_caixa);

            if (movimentacaoCaixa != null)
            {
                PreenchaCamposCaixa(movimentacaoCaixa.Caixa);
                PreenchaCamposMovimentacaoCaixa(movimentacaoCaixa);
            }
        }

        private void AtualizaCaixaAberto()
        {
            this.Cursor = Cursors.WaitCursor;

            ServicoMovimentacaoCaixa servicoBancos = new ServicoMovimentacaoCaixa();

            var movimentacaoCaixa = servicoBancos.Consulte(txtId.Text.ToInt());

            if (movimentacaoCaixa != null)
            {
                PreenchaCamposCaixa(movimentacaoCaixa.Caixa);
                PreenchaCamposMovimentacaoCaixa(movimentacaoCaixa);
            }

            this.Cursor = Cursors.Default;
            
        }

        private void btnRecibo_Click(object sender, EventArgs e)
        {
            int idItemMovimentacao = colunaId.View.GetFocusedRowCellValue(colunaId).ToInt();

            var itemMovimentacao = _listaItensMovimentacoes.FirstOrDefault(item => item.Id == idItemMovimentacao);

            if (itemMovimentacao.TipoMovimentacao == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
            {
                MessageBox.Show("Só é possível emitir o recibo para movimentações de saída.");
                return;
            }

            int? idParceiro = null;

            if (itemMovimentacao.Parceiro == null)
            {
                FormSelecaoClienteReciboMovimentacaoCaixa formSelecaoClienteReciboMovimentacaoCaixa = new FormSelecaoClienteReciboMovimentacaoCaixa();
                var resultado = formSelecaoClienteReciboMovimentacaoCaixa.SelecioneParceiro();

                if (resultado == DialogResult.Cancel)
                {
                    return;
                }

                if (formSelecaoClienteReciboMovimentacaoCaixa.Parceiro != null)
                {
                    idParceiro = formSelecaoClienteReciboMovimentacaoCaixa.Parceiro.Id;
                }
                else
                {
                    MessageBox.Show("É necessário informar um parceiro para emitir o recibo.");
                    return;
                }
            }

            RelatorioReciboCaixa relatorio = new RelatorioReciboCaixa(idItemMovimentacao, idParceiro);
            TratamentosDeTela.ExibirRelatorio(relatorio);
        }

        private void btnImprimirMovimentacao_Click(object sender, EventArgs e)
        {
            RelatorioMovimentacaoCaixa relatorioMovimentacaoCaixa = new RelatorioMovimentacaoCaixa(_movimentacaoCaixa.Id);
            TratamentosDeTela.ExibirRelatorio(relatorioMovimentacaoCaixa);
        }

        private void cboTiposMovimentacoes_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCategorias();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboCategorias()
        {   
            List<CategoriaFinanceira> categoria = new List<CategoriaFinanceira>();

            if ((EnumTipoMovimentacaoCaixa)cboTiposMovimentacoes.EditValue == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
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

        private void InsiraItem()
        {
            //Valida se tiver usando conciliação bancária é obrigado a informar categoria
            if (_parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria && cboCategoriaFinanceira.EditValue == null)
            {
                MessageBox.Show("Você está usando a Conciliação Bancária. É obrigatório informar a Categoria Financeira.","Inserindo Item na Movimentação de Caixa");
                return;
            }
             
            //Ao informar a saída verifica se a o saldo é menor que zero
            if ((EnumTipoMovimentacaoCaixa)cboTiposMovimentacoes.EditValue != EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
            if (ValideSaidaDinheiroChequeMenorQueZero(cboFormasPagamentos.EditValue.ToInt()))
                return;

            Action actionCadastroItemCaixa = () =>
            {
                ItemMovimentacaoCaixa itemMovimentacaoCaixa = new ItemMovimentacaoCaixa();
               
                FormaPagamento formaPagamento = new FormaPagamento();
                formaPagamento.Id = cboFormasPagamentos.EditValue.ToInt();
                formaPagamento.Descricao = cboFormasPagamentos.Text;
                formaPagamento.TipoFormaPagamento = cboFormasPagamentos.EditValue.ToInt() == 1 ? EnumTipoFormaPagamento.DINHEIRO : EnumTipoFormaPagamento.CHEQUE;

                itemMovimentacaoCaixa.CategoriaFinaceira = cboCategoriaFinanceira.EditValue.ToInt() != 0? new CategoriaFinanceira { Id = cboCategoriaFinanceira.EditValue.ToInt() }:null;

                itemMovimentacaoCaixa.EstahEstornado = false;
                itemMovimentacaoCaixa.FormaPagamento = formaPagamento;
                itemMovimentacaoCaixa.HistoricoMovimentacoes = txtHistoricoDaMovimentacao.Text;
                itemMovimentacaoCaixa.MovimentacaoCaixa = _movimentacaoCaixa;
                itemMovimentacaoCaixa.TipoMovimentacao = (EnumTipoMovimentacaoCaixa)cboTiposMovimentacoes.EditValue;
                itemMovimentacaoCaixa.Valor = txtValor.Text.ToDouble();
                itemMovimentacaoCaixa.OrigemMovimentacaoCaixa = EnumOrigemMovimentacaoCaixa.DIRETONOCAIXA;
                
                ServicoItemMovimentacaoCaixa servicoItemMovimentacaoCaixa = new ServicoItemMovimentacaoCaixa();

                servicoItemMovimentacaoCaixa.Cadastre(itemMovimentacaoCaixa);

                _listaItensMovimentacoes.Add(itemMovimentacaoCaixa);

                PreenchaGrid();
                PreenchaResumo();

                LimpeCamposItem();

                cboTiposMovimentacoes.Focus();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionCadastroItemCaixa);
        }

        private bool ValideSaidaDinheiroChequeMenorQueZero(int formaPagamento)
        {
            if (formaPagamento == 1) //Dinheiro
            {
                if ((txtDinheiroSaldoFinal.Text.ToDouble() - txtValor.Text.ToDouble()) < 0)
                {
                    MessageBox.Show("O valor de Entrada menos(-) Saída não pode ser menor (<) que zero (0)!", "Valor informado inconsistente!");
                    return true;
                }
            }
            else //Cheque
            {
                if ((txtChequeSaldoFinal.Text.ToDouble() - txtValor.Text.ToDouble()) < 0)
                {
                    MessageBox.Show("O valor de Entrada menos(-) Saída não pode ser menor (<) que zero (0)!", "Valor informado inconsistente!");
                    return true;
                }
            }
            return false;
        }

        private void PesquiseEPreenchaCaixaAberto()
        {
            ServicoCaixa servicoCaixa = new ServicoCaixa();

            var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

            PreenchaCamposCaixa(caixa);

            if (caixa != null)
            {
                ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();

                MovimentacaoCaixa movimentacaoCaixa = servicoMovimentacaoCaixa.ConsulteCaixaAberto(caixa);

                PreenchaCamposMovimentacaoCaixa(movimentacaoCaixa);
            }
        }

        private void PreenchaCamposCaixa(Caixa caixa)
        {
            _caixa = caixa;

            if (caixa != null)
            {
                txtIdCaixa.Text = caixa.Id.ToString();
                txtNomeCaixa.Text = caixa.NomeCaixa;
                txtUsuarioCaixa.Text = caixa.Funcionario.Id + " - " + caixa.Funcionario.DadosGerais.Razao;
            }
            else
            {
                MessageBox.Show("Este usuário não contém um caixa cadastrado!", "Não existe caixa para este usuário");

                this.FecharFormulario();
            }
        }

        private void PreenchaCamposMovimentacaoCaixa(MovimentacaoCaixa movimentacaoCaixa)
        {
            if (movimentacaoCaixa != null)
            {
                _movimentacaoCaixa = movimentacaoCaixa;
                _listaItensMovimentacoes = movimentacaoCaixa.ListaItensCaixa.ToList();

                txtId.Text = movimentacaoCaixa.Id.ToString();
                txtDataCaixa.Text = movimentacaoCaixa.DataHoraAbertura.GetValueOrDefault().ToString("dd/MM/yyyy");
                txtStatusCaixa.Text = movimentacaoCaixa.Status.Descricao();

                if (movimentacaoCaixa.Status == EnumStatusMovimentacaoCaixa.FECHADO && !Sessao.GrupoAcesso.Tesoureiro)
                {
                    pnlInserirEstornarItemMovimentacao.Enabled = false;
                }
                else
                {
                    pnlInserirEstornarItemMovimentacao.Enabled = true;
                }

                if (movimentacaoCaixa.Status == EnumStatusMovimentacaoCaixa.ABERTO)
                {
                    btnFecharCaixa.Visible = true;
                    btnAbrirCaixa.Visible = false;
                    btnImprimirMovimentacao.Visible = false;
                }
                else
                {
                    btnAbrirCaixa.Visible = true;
                    btnFecharCaixa.Visible = false;
                    btnImprimirMovimentacao.Visible = true;
                }

                PreenchaGrid();
                PreenchaResumo();
            }
        }

        private void PreenchaCboTipoMovimentacao()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoMovimentacaoCaixa>();

            cboTiposMovimentacoes.Properties.DisplayMember = "Descricao";
            cboTiposMovimentacoes.Properties.ValueMember = "Valor";
            cboTiposMovimentacoes.Properties.DataSource = lista;

            cboTiposMovimentacoes.EditValue = EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO;

            PreenchaCboCategorias();
        }

        private void PreenchaCboFormasPagamentos()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
            var formaPagamentoDinheiro = servicoFormaPagamento.Consulte(1);
            var formaPagamentoCheque = servicoFormaPagamento.Consulte(4);
            var formaPagamentoPix = servicoFormaPagamento.Consulte(10);

            ObjetoDescricaoValor objetoDescricaoValorDinheiro = new ObjetoDescricaoValor();
            objetoDescricaoValorDinheiro.Descricao = formaPagamentoDinheiro.Descricao;
            objetoDescricaoValorDinheiro.Valor = formaPagamentoDinheiro.Id;

            ObjetoDescricaoValor objetoDescricaoValorCheque = new ObjetoDescricaoValor();
            objetoDescricaoValorCheque.Descricao = formaPagamentoCheque.Descricao;
            objetoDescricaoValorCheque.Valor = formaPagamentoCheque.Id;

            ObjetoDescricaoValor objetoDescricaoValorPIX = new ObjetoDescricaoValor();
            objetoDescricaoValorPIX.Descricao = formaPagamentoPix.Descricao;
            objetoDescricaoValorPIX.Valor = formaPagamentoPix.Id;

            List<ObjetoDescricaoValor> lista = new List<ObjetoDescricaoValor>();
            lista.Add(objetoDescricaoValorDinheiro);
            lista.Add(objetoDescricaoValorCheque);
            lista.Add(objetoDescricaoValorPIX);

            cboFormasPagamentos.Properties.DisplayMember = "Descricao";
            cboFormasPagamentos.Properties.ValueMember = "Valor";
            cboFormasPagamentos.Properties.DataSource = lista;

            cboFormasPagamentos.EditValue = 1;
        }

        private void PreenchaGrid()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa(false, false);

            _listaItensMovimentacoes = _listaItensMovimentacoes.OrderBy(x => x.DataHora).ToList();

            List<ItemMovimentacaoCaixaGrid> listaItemMovimentacaoCaixaGrid = new List<ItemMovimentacaoCaixaGrid>();

            double saldo = 0;

            foreach (var item in _listaItensMovimentacoes)
            {
                ItemMovimentacaoCaixaGrid itemMovimentacaoGrid = new ItemMovimentacaoCaixaGrid();

                itemMovimentacaoGrid.DataHora = item.DataHora.ToString("dd/MM/yyyy HH:mm");
                itemMovimentacaoGrid.FormaPagamento = new ServicoFormaPagamento().Consulte(item.FormaPagamento.Id).Descricao;
                itemMovimentacaoGrid.HistoricoMovimentacao = item.HistoricoMovimentacoes;
                itemMovimentacaoGrid.Id = item.Id;

                if (item.Parceiro != null)
                {
                    var pessoa = servicoPessoa.Consulte(item.Parceiro.Id);

                    itemMovimentacaoGrid.Parceiro = pessoa != null ? pessoa.Id + " - " + pessoa.DadosGerais.Razao : string.Empty;
                }

                if(item.CategoriaFinaceira != null)
                {
                    var categoria = new ServicoCategoria().Consulte(item.CategoriaFinaceira.Id);

                    itemMovimentacaoGrid.Categoria = categoria != null ? categoria.Descricao : string.Empty;
                }

                itemMovimentacaoGrid.EstahEstornado = item.EstahEstornado;
                itemMovimentacaoGrid.OrigemMovimentacao = item.OrigemMovimentacaoCaixa.Descricao();

                if (item.TipoMovimentacao == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
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

                listaItemMovimentacaoCaixaGrid.Add(itemMovimentacaoGrid);
            }

            gcItens.DataSource = listaItemMovimentacaoCaixaGrid;
            gcItens.RefreshDataSource();
            
            if (_listaItensMovimentacoes.Count > 0)
            {
                btnRecibo.Visible = true;
            }
            else
            {
                btnRecibo.Visible = false;
            }
        }

        private void PreenchaResumo()
        {
            double dinheiroEntrada = 0;
            double cartaoDebitoEntrada = 0;
            double cartaoCreditoEntrada = 0;
            double chequeEntrada = 0;
            double carteiraEntrada = 0;

            double dinheiroSaida = 0;
            double chequeSaida = 0;

            double dinheiroSaldoFinal = 0;
            double chequeSaldoFinal = 0;

            foreach (var item in _listaItensMovimentacoes)
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
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO)
                    {
                        cartaoDebitoEntrada += item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO)
                    {
                        cartaoCreditoEntrada += item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                    {
                        chequeEntrada += item.Valor;
                        chequeSaldoFinal += item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDIARIOPROPRIO)
                    {
                        carteiraEntrada += item.Valor;
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

            txtDinheiroSaldoInicial.Text = _movimentacaoCaixa.SaldoInicial.ToDouble() == 0? _movimentacaoCaixa.SaldoInicialDinheiro.ToString("0.00"): _movimentacaoCaixa.SaldoInicial.ToString("0.00");
            txtChequeSaldoInicial.Text = _movimentacaoCaixa.SaldoInicialCheque.ToString("0.00");
            txtDinheiroEntrada.Text = dinheiroEntrada.ToString("0.00");
            txtCartaoDebitoEntrada.Text = cartaoDebitoEntrada.ToString("0.00");
            txtCartaoCreditoEntrada.Text = cartaoCreditoEntrada.ToString("0.00");
            txtChequeEntrada.Text = chequeEntrada.ToString("0.00");
            txtCarteiraEntrada.Text = carteiraEntrada.ToString("0.00");

            txtDinheiroSaida.Text = dinheiroSaida.ToString("0.00");
            txtChequeSaida.Text = chequeSaida.ToString("0.00");

            txtDinheiroSaldoFinal.Text = dinheiroSaldoFinal.ToString("0.00");
            txtChequeSaldoFinal.Text = chequeSaldoFinal.ToString("0.00");
        }

        private ItemMovimentacaoCaixa RetorneItemMovimentacaoCaixaSelecionado()
        {
            ItemMovimentacaoCaixa itemMovimentacaoCaixa = null;

            if (_listaItensMovimentacoes != null && _listaItensMovimentacoes.Count > 0)
            {
                itemMovimentacaoCaixa = _listaItensMovimentacoes.FirstOrDefault(x => x.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());
            }

            return itemMovimentacaoCaixa;
        }

        private void LimpeCamposItem()
        {
            cboTiposMovimentacoes.EditValue = EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO;
            txtHistoricoDaMovimentacao.Text = string.Empty;
            cboFormasPagamentos.EditValue = 1;
            cboCategoriaFinanceira.EditValue = null;
            txtValor.Text = string.Empty;
        }

        private void OrganizarControlesConciliacaoDesabilitada()
        {
            _parametros = new ServicoParametros().ConsulteParametros();

            if (_parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
                return;

            gridControl2.Columns.ColumnByName("colunaCategoria").Visible = false;

            lblCategoria.Visible = false;
            cboCategoriaFinanceira.Visible = false;
            btnAdicionarCategoria.Visible = false;

            //cboTiposMovimentacoes.Width = 176;
            //lblHistorico.Location = new System.Drawing.Point(183,1);
            //txtHistoricoDaMovimentacao.Location = new System.Drawing.Point(183,18);
            //txtHistoricoDaMovimentacao.Width = 383;
            //lblFormaPagamento.Location = new System.Drawing.Point(572,3);
            //cboFormasPagamentos.Location = new System.Drawing.Point(569,18);
            //cboFormasPagamentos.Width = 307;            
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ItemMovimentacaoCaixaGrid
        {
            public int Id { get; set; }

            public bool EstahEstornado { get; set; }

            public string DataHora { get; set; }

            public string Parceiro { get; set; }

            public string HistoricoMovimentacao { get; set; }

            public string FormaPagamento { get; set; }

            public string Categoria { get; set; }

            public string Entrada { get; set; }

            public string Saida { get; set; }

            public string Saldo { get; set; }

            public string OrigemMovimentacao { get; set; }
        }

        #endregion

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizaCaixaAberto();
        }
    }
}
