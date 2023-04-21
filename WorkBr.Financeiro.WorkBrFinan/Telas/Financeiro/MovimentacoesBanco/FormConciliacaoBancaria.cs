using Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.ObjetoDeNegocio;
using Programax.Easy.View.Componentes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Financeiro.ConciliacaoBancariaServ;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoBancoServ;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Financeiro.ContasPagarReceber;
using System.Transactions;

namespace Programax.Easy.View.Telas.Financeiro.MovimentacoesBanco
{
    public partial class FormConciliacaoBancaria : FormularioPadrao
    {
        private List<ConciliacaoBancaria> _listaDadosConciliacao;
        List<ConciliacaoBancariaGrid> listaDeDadosConciliacaoGrid = new List<ConciliacaoBancariaGrid>();
        private MovimentacaoBanco _movimentacaoBanco;
        private bool _ehAcaoDePesquisa;
        public List<ConciliacaoBancaria> _listaItensConciliacaoSalvarAtualizar = new List<ConciliacaoBancaria>();

        public FormConciliacaoBancaria(List<ConciliacaoBancaria> listaDadosConciliacao, MovimentacaoBanco movimentacaoBanco)
        {
            InitializeComponent();

            if(listaDadosConciliacao != null && listaDadosConciliacao.Count !=0)
                _listaDadosConciliacao = listaDadosConciliacao;
            else
            {
                _ehAcaoDePesquisa=true;
                _listaDadosConciliacao = new ServicoConciliacaoBancaria().ConsulteLista(null, null, null, null, movimentacaoBanco.Id);

                cboStatusFiltrar.Enabled = true;
                cboDataFiltrar.Enabled = true;
                txtDataInicialPeriodo.Enabled = true;
                txtDataFinalPeriodo.Enabled = true;
                
                PreenchaCboStatusAFiltrar();
                PreenchaCboDataFiltrar();
            }
            
            _movimentacaoBanco = movimentacaoBanco;

            CarregaGrid();
        }

        #region "CONTROLES"

        private void cboDataFiltrar_EditValueChanged(object sender, EventArgs e)
        {
            if (cboDataFiltrar.EditValue == null)
            {
                txtDataFinalPeriodo.Text = string.Empty;
                txtDataInicialPeriodo.Text = string.Empty;
            }
        }

        private void btnPesquisaConciliacao_Click(object sender, EventArgs e)
        {
            if (_ehAcaoDePesquisa)
            {
                EnumOrigemMovimentacaoBanco? statusFiltrar;
                EnumDataFiltrarConciliacaoBancaria? dataFiltrar;
                DateTime? dataInicial;
                DateTime? dataFinal;

                //Status Filtrar
                if (cboStatusFiltrar.EditValue == null)
                    statusFiltrar = null;
                else
                    statusFiltrar = (EnumOrigemMovimentacaoBanco)cboStatusFiltrar.EditValue;

                //Data Filtrar
                if (cboDataFiltrar.EditValue == null)
                    dataFiltrar = null;
                else
                    dataFiltrar = (EnumDataFiltrarConciliacaoBancaria)cboDataFiltrar.EditValue;

                //Data Inicial
                if (txtDataInicialPeriodo.Text == string.Empty)
                    dataInicial = null;
                else
                    dataInicial = txtDataInicialPeriodo.DateTime;

                //Data Final
                if (txtDataFinalPeriodo.Text == string.Empty)
                    dataFinal = null;
                else
                    dataFinal = txtDataFinalPeriodo.DateTime;

                listaDeDadosConciliacaoGrid = new List<ConciliacaoBancariaGrid>();

                _listaDadosConciliacao = new ServicoConciliacaoBancaria().ConsulteLista(statusFiltrar, dataFiltrar, dataInicial, dataFinal, _movimentacaoBanco.Id);

                CarregaGrid();
            }
        }

        private void btnPagarReceber_Click(object sender, EventArgs e)
        {
            ConciliarComPagarReceber();

            //var pagarReceber = listaDeDadosConciliacaoGrid.Find(x => x.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

            //if (pagarReceber.Origem1 == EnumOrigemConciliacaoBancaria.ARECEBER.Descricao())
            //{
            //    FormContasReceberPesquisa formPesquisaReceber = new FormContasReceberPesquisa(true);

            //    formPesquisaReceber.ConsulteItemContaPagarReber(pagarReceber.ChaveOrigem1);
            //}
            //else
            //{
            //    FormContasPagarPesquisa formPesquisaPagar = new FormContasPagarPesquisa(true);

            //    formPesquisaPagar.ConsulteItemContaPagarReber(pagarReceber.ChaveOrigem1);
            //}
        }

        private void gcConciliacaoBancaria_Click(object sender, EventArgs e)
        {
            //ExibirBotoesAtravesGrid();
        }

        private void gcConciliacaoBancaria_KeyUp(object sender, KeyEventArgs e)
        {
            //ExibirBotoesAtravesGrid();
        }

        private void lnkInstrucoes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new AboutInstrucoes().Show();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            FinalizeConciliacaoBancaria();
        }

        private void FormConciliacaoBancaria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                SairDoSistema();
            }
            else if (e.KeyCode == Keys.F2)
            {
                //Pesquise();
            }
            else if (e.KeyCode == Keys.F3)
            {
                Ignorar();
            }
            else if (e.KeyCode == Keys.F4)
            {
                Importar();
            }
            else if (e.KeyCode == Keys.F5)
            {
                Conciliar();
            }
            else if (e.KeyCode == Keys.F6)
            {
                Desfazer();
            }
            else if (e.KeyCode == Keys.F7)
            {
                
            }
            else if (e.KeyCode == Keys.F8)
            {
                return;
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            SairDoSistema();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            Importar();
        }

        private void btnIgnorar_Click(object sender, EventArgs e)
        {
            Ignorar();
        }

        private void btnConciliar_Click(object sender, EventArgs e)
        {
            Conciliar();
        }

        private void btnDesfazer_Click(object sender, EventArgs e)
        {
            Desfazer();
        }

        #endregion

        #region "Metodos Auxiliares"

        private void ConciliarComPagarReceber()
        {
            ContaPagarReceber contasPagarReceber = new ContaPagarReceber();

            Pessoa parceiro = null;

            var itemGrid = listaDeDadosConciliacaoGrid.Find(x => x.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

            if (itemGrid == null) return;

            if (itemGrid.Origem2 == EnumOrigemConciliacaoBancaria.EXTRATOBANCARIOCR.Descricao())
            {
                FormContasReceberPesquisa formPesquisaReceber = new FormContasReceberPesquisa(true);

                contasPagarReceber = formPesquisaReceber.BuscarConciliacaoPagarReceber(parceiro, DateTime.Parse(itemGrid.DataLancto));
            }
            else
            {
                FormContasPagarPesquisa formPesquisaPagar = new FormContasPagarPesquisa(true);

                contasPagarReceber = formPesquisaPagar.BuscarConciliacaoPagarReceber(parceiro, DateTime.Parse(itemGrid.DataLancto));
            }

            if (contasPagarReceber == null) return;

            itemGrid.ChaveOrigem1 = contasPagarReceber.Id;

            itemGrid.Origem1 = contasPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER? 
                                                            EnumOrigemConciliacaoBancaria.ARECEBER.Descricao(): EnumOrigemConciliacaoBancaria.APAGAR.Descricao();
            itemGrid.NumDoc = contasPagarReceber.NumeroDocumento;
            itemGrid.DescricaoDoc = contasPagarReceber.Historico;
            itemGrid.DataVencimento = string.Format(contasPagarReceber.DataVencimento.ToStringNull(),"dd/MM/yyyy");
            itemGrid.ValorDoc = contasPagarReceber.ValorTotal;

            AtualizeGrid();
        }
        
        private void PreenchaCboStatusAFiltrar()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumOrigemMovimentacaoBanco>();
            lista.Insert(0, null);

            lista.RemoveRange(0, 8);
            
            cboStatusFiltrar.Properties.DisplayMember = "Descricao";
            cboStatusFiltrar.Properties.ValueMember = "Valor";
            cboStatusFiltrar.Properties.DataSource = lista;
        }

        private void PreenchaCboDataFiltrar()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumDataFiltrarConciliacaoBancaria>();
            lista.Insert(0, null);

            cboDataFiltrar.Properties.DisplayMember = "Descricao";
            cboDataFiltrar.Properties.ValueMember = "Valor";
            cboDataFiltrar.Properties.DataSource = lista;
        }

        private void FinalizeConciliacaoBancaria()
        {
            if (MessageBox.Show("Você tem certeza que deseja Finalizar? Ao Finalizar, o sistema irá salvar tudo, " +
                                "e irá dar baixa no Contas Pagar/Receber e irá importar para Movimentação Bancária.", "Conciliação", 
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {               
                if (!_ehAcaoDePesquisa)
                {   
                    SalvarItensConciliacao();

                    ConciliarLancamentos();
                }
                else
                {
                    DesfazerConciliacao();
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                throw ex;
            }

            this.Cursor = Cursors.Default;
        }

        private void DesfazerConciliacao()
        {
            foreach (var item in listaDeDadosConciliacaoGrid)
            {
                var itemConciliacaoAtualGrid = retorneItemConciliacaoBancariaApartirListaGrid(item);

                var itemAdesfazer = new ServicoConciliacaoBancaria().Consulte(itemConciliacaoAtualGrid.Id);

                if(itemConciliacaoAtualGrid.StatusConciliacao != itemAdesfazer.StatusConciliacao)
                {
                    if (itemConciliacaoAtualGrid.StatusConciliacao == EnumOrigemMovimentacaoBanco.CONCILIADO)
                    {   
                        AtualizeApenasOsConciliados(itemAdesfazer);
                    }
                    else if (itemConciliacaoAtualGrid.StatusConciliacao == EnumOrigemMovimentacaoBanco.IMPORTADO)
                    {
                        ImportarParaMovimentacaoBancaria(itemAdesfazer);
                    }
                    else
                    {
                        DesfazerItensImportadosConciliados(itemAdesfazer);
                    }

                    itemAdesfazer.StatusConciliacao = retorneStatusConciliacao(item.StatusConciliacao);

                    new ServicoConciliacaoBancaria().Atualize(itemAdesfazer);
                }
            }
        }

        private void DesfazerItensImportadosConciliados(ConciliacaoBancaria itemParaDesfazer)
        {
            ServicoItemMovimentacaoBanco servicoItemMovimentacaoBanco = new ServicoItemMovimentacaoBanco();

            var itemMovimentacao =  servicoItemMovimentacaoBanco.ConsulteItemConciliadoImportado(itemParaDesfazer.Id);

            if (itemMovimentacao == null) return;

            var itemMovimentacaoAtual = servicoItemMovimentacaoBanco.Consulte(itemMovimentacao.Id);

            if (itemMovimentacao.ContaPagarReceber == null)
            {
                servicoItemMovimentacaoBanco.Exclua(itemMovimentacao.Id);
            }
            else
            {
                servicoItemMovimentacaoBanco.EstornarContasPagarReceber(itemMovimentacao);
                servicoItemMovimentacaoBanco.ExcluaParcialOrigemPagarReceber(itemMovimentacao.ContaPagarReceber, false, true);
            }
        }

        private void AtualizeApenasOsConciliados(ConciliacaoBancaria itemParaAtualizar)
        {
            if (itemParaAtualizar.Origem1 == EnumOrigemConciliacaoBancaria.APAGAR || itemParaAtualizar.Origem1 == EnumOrigemConciliacaoBancaria.ARECEBER)
            {
                AtualizarContasPagarReceberDaConciliacao(itemParaAtualizar);
            }

            if (itemParaAtualizar.Origem1 == EnumOrigemConciliacaoBancaria.MOVIMENTACAOBANCARIACR || itemParaAtualizar.Origem1 == EnumOrigemConciliacaoBancaria.MOVIMENTACAOBANCARIADB)
            {
                AtualizarMovimentacaoBancaria(itemParaAtualizar);
            }

            new ServicoConciliacaoBancaria().Atualize(itemParaAtualizar);
        }
        
        private void SalvarItensConciliacao()
        {
            _listaItensConciliacaoSalvarAtualizar = new List<ConciliacaoBancaria>();

            foreach (var item in listaDeDadosConciliacaoGrid)
            {
                var itemConciliacao = retorneItemConciliacaoBancariaApartirListaGrid(item);

                var idConciliacao = new ServicoConciliacaoBancaria().Cadastre(itemConciliacao);

                itemConciliacao.Id = idConciliacao;

                _listaItensConciliacaoSalvarAtualizar.Add(itemConciliacao);
            }
        }

        private void ConciliarLancamentos()
        {
            foreach (var item in _listaItensConciliacaoSalvarAtualizar)
            {
                if (item.StatusConciliacao == EnumOrigemMovimentacaoBanco.CONCILIADO)
                {
                    if (item.Origem1 == EnumOrigemConciliacaoBancaria.APAGAR || item.Origem1 == EnumOrigemConciliacaoBancaria.ARECEBER)
                        AtualizarContasPagarReceberDaConciliacao(item);

                    if (item.Origem1 == EnumOrigemConciliacaoBancaria.MOVIMENTACAOBANCARIACR || item.Origem1 == EnumOrigemConciliacaoBancaria.MOVIMENTACAOBANCARIADB)
                        AtualizarMovimentacaoBancaria(item);
                }
                else if(item.StatusConciliacao == EnumOrigemMovimentacaoBanco.IMPORTADO)
                {
                    ImportarParaMovimentacaoBancaria(item);
                }                
            }

            MessageBox.Show("Conciliação realizada com sucesso!", "Importação de Extrato Bancário", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void AtualizarContasPagarReceberDaConciliacao(ConciliacaoBancaria itemConciliacao)
        {
            if (itemConciliacao == null) return;

            ServicoContasPagarReceber servicoContaPagarReceber = new ServicoContasPagarReceber();

            var contaPagarReceber = servicoContaPagarReceber.Consulte(itemConciliacao.ChaveOrigem1.ToInt());

            if (contaPagarReceber == null) return;

            EnumStatusContaPagarReceber statusAntesDeAtualizar = contaPagarReceber.Status;

            contaPagarReceber.Status = EnumStatusContaPagarReceber.CONCILIADOQUITADO;
            contaPagarReceber.DataPagamento = itemConciliacao.DataLancto;
            contaPagarReceber.ValorPago = itemConciliacao.ValorLancto;
            if (contaPagarReceber.DataEmissao > contaPagarReceber.DataVencimento)
            {
                contaPagarReceber.DataEmissao = (DateTime)contaPagarReceber.DataVencimento;
            }

            if (contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER)
            {
                new ServicoContasReceber().Atualize(contaPagarReceber);

                //Caso a conciliação for de Cartão, o sistema vai calcular o valor da taxa de adm e criar um lançamento
                if (contaPagarReceber.FormaPagamento.TipoFormaPagamento != EnumTipoFormaPagamento.CARTAOCREDITO ||
                    contaPagarReceber.FormaPagamento.TipoFormaPagamento != EnumTipoFormaPagamento.CARTAODEBITO)
                {
                    new ServicoItemMovimentacaoBanco().CalculeDespesasCartoes(contaPagarReceber, false,
                                                   EnumTipoOperacaoContasPagarReceber.PAGAR, contaPagarReceber.DataPagamento.Value,
                                                   _movimentacaoBanco.Banco,
                                                   contaPagarReceber.OperadorasCartao,
                                                   contaPagarReceber.ValorPago, new Pessoa { Id = Sessao.PessoaLogada.Id });
                }
            }
                
            else
                new ServicoContasPagar().Atualize(contaPagarReceber);

            if (statusAntesDeAtualizar != EnumStatusContaPagarReceber.CONCILIADOQUITADO)
            {
                var objetoContasPagarReceberParcial = retorneListaHistoricoDePagamentos(contaPagarReceber.DataPagamento.Value,
                                                                   itemConciliacao.ValorLancto,
                                                                   contaPagarReceber.FormaPagamento, contaPagarReceber, false);

                contaPagarReceber.ListaContasPagarReceberParcial.Add(objetoContasPagarReceberParcial);

                ServicoContasPagarReceberPagamento servicoContasPagarReceberPagamento = new ServicoContasPagarReceberPagamento();

                servicoContasPagarReceberPagamento.CadastreLista(contaPagarReceber.ListaContasPagarReceberParcial.ToList());
                servicoContasPagarReceberPagamento.Cadastre(objetoContasPagarReceberParcial);
            }

            //Vamos Incluir na movimentação do banco
            ImportarContaPagarReceberParaMovimentacaoBancaria(contaPagarReceber, itemConciliacao.Id);
        }

        private ContaPagarReceberPagamento retorneListaHistoricoDePagamentos(DateTime DataPagamento, double Valor, FormaPagamento
                                                                             formaDePagamento, ContaPagarReceber ContaPagarReceber, bool EstahEstornado)
        {
            ContaPagarReceberPagamento item = new ContaPagarReceberPagamento();

            List<ContaPagarReceberPagamento> Lista = new List<ContaPagarReceberPagamento>();

            item.DataPagamento = DataPagamento;
            item.Valor = Valor;
            item.Observacoes = "Título Quitado/Conciliado através da importação de Extrato Bancário";
            item.Responsavel = new ServicoPessoa().Consulte(Sessao.PessoaLogada.Id);
            item.FormaPagamento = formaDePagamento;
            item.ContaPagarReceber = ContaPagarReceber;
            item.EstahEstornado = EstahEstornado;

            return item;
        }

        private void AtualizarMovimentacaoBancaria(ConciliacaoBancaria itemConciliacao)
        {
            ServicoItemMovimentacaoBanco servicoItemMovimentacaoBanco = new ServicoItemMovimentacaoBanco();

            if (itemConciliacao == null) return;

            var itemMovimentacaoBanco = servicoItemMovimentacaoBanco.Consulte(itemConciliacao.ChaveOrigem1.ToInt());

            itemMovimentacaoBanco.DataHoraLancamento = itemConciliacao.DataLancto;
            itemMovimentacaoBanco.Valor = itemConciliacao.ValorLancto;
            itemMovimentacaoBanco.OrigemMovimentacaoBanco = EnumOrigemMovimentacaoBanco.CONCILIADO;

            //Usuário e Data de atualização / cadastro
            itemMovimentacaoBanco.DataAtualizacao = DateTime.Now;
            itemMovimentacaoBanco.UsuarioAtualizacao = new Pessoa { Id = Sessao.PessoaLogada.Id };

            //Atualiza contas Pagar/Receber****
            AtualizeContaPagarReceberDaMovimentacao(itemMovimentacaoBanco);

            servicoItemMovimentacaoBanco.Atualize(itemMovimentacaoBanco);
        }

        private void AtualizeContaPagarReceberDaMovimentacao(ItemMovimentacaoBanco itemMov)
        {
            if (itemMov.ContaPagarReceber == null) return;

            ServicoContasPagarReceber servicoContaPagarReceber = new ServicoContasPagarReceber();

            var contaPagarReceber = servicoContaPagarReceber.Consulte(itemMov.ContaPagarReceber.Id);

            if (contaPagarReceber == null) return;

            EnumStatusContaPagarReceber statusAntesDeAtualizar = contaPagarReceber.Status;

            contaPagarReceber.Status = EnumStatusContaPagarReceber.CONCILIADOQUITADO;           
            contaPagarReceber.DataPagamento = itemMov.DataHoraLancamento;

            if (contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER)
                new ServicoContasReceber().Atualize(contaPagarReceber);
            else
                new ServicoContasPagar().Atualize(contaPagarReceber);

            if (statusAntesDeAtualizar != EnumStatusContaPagarReceber.CONCILIADOQUITADO)
            {
                var objetoContasPagarReceberParcial = retorneListaHistoricoDePagamentos(contaPagarReceber.DataPagamento.Value,
                                                                   itemMov.Valor,
                                                                   contaPagarReceber.FormaPagamento, contaPagarReceber, false);

                contaPagarReceber.ListaContasPagarReceberParcial.Add(objetoContasPagarReceberParcial);

                ServicoContasPagarReceberPagamento servicoContasPagarReceberPagamento = new ServicoContasPagarReceberPagamento();

                servicoContasPagarReceberPagamento.CadastreLista(contaPagarReceber.ListaContasPagarReceberParcial.ToList());
                servicoContasPagarReceberPagamento.Cadastre(objetoContasPagarReceberParcial);
            }
        }

        private void ImportarParaMovimentacaoBancaria(ConciliacaoBancaria itemConciliacao)
        {
            ItemMovimentacaoBanco itemMovimentacaoBanco = new ItemMovimentacaoBanco();

            itemMovimentacaoBanco.EstahEstornado = false;
            itemMovimentacaoBanco.Categoria = null;
            itemMovimentacaoBanco.Parceiro = null;
            itemMovimentacaoBanco.DescricaoDaMovimentacao = itemConciliacao.DescricaoLancto;
            itemMovimentacaoBanco.MovimentacaoBanco = _movimentacaoBanco;
            itemMovimentacaoBanco.TipoMovimentacao = itemConciliacao.Origem1 == EnumOrigemConciliacaoBancaria.MOVIMENTACAOBANCARIACR ||
                                                     itemConciliacao.Origem1 == EnumOrigemConciliacaoBancaria.ARECEBER ? EnumTipoMovimentacaoBanco.ENTRADA :
                                                     EnumTipoMovimentacaoBanco.SAIDA;
                                                        
            itemMovimentacaoBanco.OrigemMovimentacaoBanco = EnumOrigemMovimentacaoBanco.IMPORTADO;
            itemMovimentacaoBanco.DataHoraLancamento = itemConciliacao.DataLancto;
            itemMovimentacaoBanco.Valor = Math.Abs(itemConciliacao.ValorLancto); //Importar com sinal positivo, 
                                                                           //pois o sistema identifica quando é entrada ou saída.
            itemMovimentacaoBanco.NumeroDocumentoOrigem = itemConciliacao.NumLancto;

            //Usuário e Data de atualização / cadastro
            itemMovimentacaoBanco.DataAtualizacao = DateTime.Now;
            itemMovimentacaoBanco.UsuarioAtualizacao = new Pessoa { Id = Sessao.PessoaLogada.Id };
            itemMovimentacaoBanco.ConciliacaoImportacaoId = itemConciliacao.Id;

            ServicoItemMovimentacaoBanco servicoItemMovimentacaoBanco = new ServicoItemMovimentacaoBanco();

            servicoItemMovimentacaoBanco.Cadastre(itemMovimentacaoBanco);
        }

        private void ImportarContaPagarReceberParaMovimentacaoBancaria(ContaPagarReceber itemPagarReceber, int conciliacaoImpotacaoId)
        {
            ItemMovimentacaoBanco itemMovimentacaoBanco = new ItemMovimentacaoBanco();

            itemMovimentacaoBanco.EstahEstornado = false;
            itemMovimentacaoBanco.Categoria = itemPagarReceber.CategoriaFinanceira;
            itemMovimentacaoBanco.Parceiro = itemPagarReceber.Pessoa;
            itemMovimentacaoBanco.DescricaoDaMovimentacao = itemPagarReceber.Historico;
            itemMovimentacaoBanco.MovimentacaoBanco = _movimentacaoBanco;
            itemMovimentacaoBanco.TipoMovimentacao = itemPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER? EnumTipoMovimentacaoBanco.ENTRADA :
                                                     EnumTipoMovimentacaoBanco.SAIDA;

            itemMovimentacaoBanco.OrigemMovimentacaoBanco =EnumOrigemMovimentacaoBanco.CONCILIADO;
            itemMovimentacaoBanco.DataHoraLancamento = itemPagarReceber.DataPagamento.Value;
            itemMovimentacaoBanco.Valor = itemPagarReceber.ValorPago;
            itemMovimentacaoBanco.NumeroDocumentoOrigem = itemPagarReceber.NumeroDocumento;
            itemMovimentacaoBanco.ContaPagarReceber = itemPagarReceber;

            //Usuário e Data de atualização / cadastro
            itemMovimentacaoBanco.DataAtualizacao = DateTime.Now;
            itemMovimentacaoBanco.UsuarioAtualizacao = new Pessoa { Id = Sessao.PessoaLogada.Id };
            itemMovimentacaoBanco.ConciliacaoImportacaoId = conciliacaoImpotacaoId;

            ServicoItemMovimentacaoBanco servicoItemMovimentacaoBanco = new ServicoItemMovimentacaoBanco();

            servicoItemMovimentacaoBanco.Cadastre(itemMovimentacaoBanco);
        }

        private ConciliacaoBancaria retorneItemConciliacaoBancariaApartirListaGrid(ConciliacaoBancariaGrid itemGrid)
        {
            ConciliacaoBancaria itemConciliacao = new ConciliacaoBancaria();



            itemConciliacao.Id = itemGrid.IdConciliacao;
            itemConciliacao.ChaveOrigem1 = itemGrid.ChaveOrigem1;
            itemConciliacao.Origem1 = retorneStatusOrigem(itemGrid.Origem1);
            itemConciliacao.NumDoc = itemGrid.NumDoc;
            itemConciliacao.DescricaoDoc = itemGrid.DescricaoDoc;
            itemConciliacao.DataVencimento = itemGrid.DataVencimento.ToString()==string.Empty? null: itemGrid.DataVencimento.ToDateNullabel();
            itemConciliacao.ValorDoc = itemGrid.ValorDoc;
            itemConciliacao.Origem2 = retorneStatusOrigem(itemGrid.Origem2);
            itemConciliacao.NumLancto = itemGrid.NumLancto;
            itemConciliacao.DescricaoLancto = itemGrid.DescricaoLancto;
            itemConciliacao.DataLancto = DateTime.Parse(itemGrid.DataLancto);
            itemConciliacao.ValorLancto = itemGrid.ValorLancto;
            itemConciliacao.MovimentacaoBanco = _movimentacaoBanco;
            itemConciliacao.StatusConciliacao = retorneStatusConciliacao(itemGrid.StatusConciliacao);

            return itemConciliacao;
        }

        private EnumOrigemMovimentacaoBanco retorneStatusConciliacao(string DescricaoOrigem)
        {
            return DescricaoOrigem == EnumOrigemMovimentacaoBanco.CONCILIADO.Descricao() ?
                                                  EnumOrigemMovimentacaoBanco.CONCILIADO :
                                                  DescricaoOrigem == EnumOrigemMovimentacaoBanco.IMPORTADO.Descricao() ?
                                                  EnumOrigemMovimentacaoBanco.IMPORTADO :
                                                  DescricaoOrigem == EnumOrigemMovimentacaoBanco.IGNORADO.Descricao() ?
                                                  EnumOrigemMovimentacaoBanco.IGNORADO :
                                                  EnumOrigemMovimentacaoBanco.NENHUM;
        }

        private void Ignorar()
        {
            var listaConciliacao = retorneListaConciliacaoParaEdicao();

            if (listaConciliacao == null) return;

            if (listaConciliacao == null) return;

            foreach (var item in listaConciliacao)
            {
                listaDeDadosConciliacaoGrid.FirstOrDefault(x => x.Id == item.Id).StatusConciliacao = EnumOrigemMovimentacaoBanco.IGNORADO.Descricao();
                listaDeDadosConciliacaoGrid.FirstOrDefault(x => x.Id == item.Id).Imagem = Properties.Resources.icone_vermelho;
            }

            AtualizeGrid();
        }

        private void Conciliar()
        {
            var listaConciliacao = retorneListaConciliacaoParaEdicao();

            if (listaConciliacao == null) return;

            foreach (var item in listaConciliacao)
            {
                if (item.Origem1 != null)
                {
                    listaDeDadosConciliacaoGrid.FirstOrDefault(x => x.Id == item.Id).StatusConciliacao = EnumOrigemMovimentacaoBanco.CONCILIADO.Descricao();
                    listaDeDadosConciliacaoGrid.FirstOrDefault(x => x.Id == item.Id).Imagem = Properties.Resources.icone_verde;
                }
                else
                {
                    MessageBox.Show("Para marcar o status como: <CONCILIADO>, a Origem_1 tem que estar preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            AtualizeGrid();
        }

        private void Importar()
        {
            var listaConciliacao = retorneListaConciliacaoParaEdicao();
            
            if (listaConciliacao == null) return;

            foreach (var item in listaConciliacao)
            {
                listaDeDadosConciliacaoGrid.FirstOrDefault(x => x.Id == item.Id).StatusConciliacao = EnumOrigemMovimentacaoBanco.IMPORTADO.Descricao();
                listaDeDadosConciliacaoGrid.FirstOrDefault(x => x.Id == item.Id).Imagem = Properties.Resources.icone_azul;               
            }

            AtualizeGrid();

        }

        private void Desfazer()
        {
            var listaConciliacao = retorneListaConciliacaoParaEdicao();

            if (listaConciliacao == null) return;

            foreach (var item in listaConciliacao)
            {
                listaDeDadosConciliacaoGrid.FirstOrDefault(x => x.Id == item.Id).StatusConciliacao = EnumOrigemMovimentacaoBanco.NENHUM.Descricao();
                listaDeDadosConciliacaoGrid.FirstOrDefault(x => x.Id == item.Id).Imagem = Properties.Resources.none_icon;
            }

            AtualizeGrid();
        }

        private void SairDoSistema()
        {
            var mudaBotao = listaDeDadosConciliacaoGrid.Find(x => x.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

            if(mudaBotao==null || _ehAcaoDePesquisa)
            {
                this.Close();
                return;
            }

            if (MessageBox.Show("Você tem certeza que deseja sair? Se Sair, o sistema irá ignorar tudo, e você perderá todas as informações carregadas.","Conciliação",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.Cancel)
            {
                return;
            }

            this.Close();
        }

        private EnumOrigemConciliacaoBancaria? retorneStatusOrigem(string DescricaoOrigem)
        {

            if (string.IsNullOrEmpty(DescricaoOrigem)) return null;

            return DescricaoOrigem == EnumOrigemConciliacaoBancaria.APAGAR.Descricao() ?
                                                  EnumOrigemConciliacaoBancaria.APAGAR :
                                                  DescricaoOrigem == EnumOrigemConciliacaoBancaria.ARECEBER.Descricao() ?
                                                  EnumOrigemConciliacaoBancaria.ARECEBER : 
                                                  DescricaoOrigem == EnumOrigemConciliacaoBancaria.EXTRATOBANCARIOCR.Descricao() ?
                                                  EnumOrigemConciliacaoBancaria.EXTRATOBANCARIOCR :
                                                   DescricaoOrigem == EnumOrigemConciliacaoBancaria.EXTRATOBANCARIODB.Descricao() ?
                                                  EnumOrigemConciliacaoBancaria.EXTRATOBANCARIODB :
                                                  DescricaoOrigem == EnumOrigemConciliacaoBancaria.MOVIMENTACAOBANCARIACR.Descricao() ?
                                                  EnumOrigemConciliacaoBancaria.MOVIMENTACAOBANCARIACR :
                                                  EnumOrigemConciliacaoBancaria.MOVIMENTACAOBANCARIADB;
        }

        private List<DadosConciliacao> retorneListaConciliacaoParaEdicao()
         {
            if (gridViewConciliacaoBancaria.SelectedRowsCount == 0) return null;

            List<DadosConciliacao> listaConciliacaoParaEdicao = new List<DadosConciliacao>();

            if (gridViewConciliacaoBancaria.SelectedRowsCount > 0)
            {
                var linhasSelecionadas = gridViewConciliacaoBancaria.GetSelectedRows();

                foreach (var item in gridViewConciliacaoBancaria.GetSelectedRows())
                {
                    DadosConciliacao dadosConciliacao = new DadosConciliacao();

                    var lancamento = colunaId.View.GetRowCellValue(item, colunaId);

                    var itemDoGrid = listaDeDadosConciliacaoGrid.FirstOrDefault(x => x.Id == lancamento.ToInt());

                    if (itemDoGrid != null)
                    {
                        dadosConciliacao.Id = itemDoGrid.Id;
                        dadosConciliacao.ChaveOrigem1 = itemDoGrid.ChaveOrigem1;
                        dadosConciliacao.Origem1 = retorneStatusOrigem(itemDoGrid.Origem1);
                        dadosConciliacao.NumDoc = itemDoGrid.NumDoc;
                        dadosConciliacao.DescricaoDoc = itemDoGrid.DescricaoDoc;
                        dadosConciliacao.DataVencimento = string.IsNullOrEmpty(itemDoGrid.DataVencimento)? dadosConciliacao.DataVencimento: DateTime.Parse(itemDoGrid.DataVencimento);
                        dadosConciliacao.ValorDoc = itemDoGrid.ValorDoc;
                        dadosConciliacao.Origem2 = retorneStatusOrigem(itemDoGrid.Origem2);
                        dadosConciliacao.NumLancto = itemDoGrid.NumLancto;
                        dadosConciliacao.DescricaoLancto = itemDoGrid.DescricaoLancto;
                        dadosConciliacao.DataLancto = DateTime.Parse(itemDoGrid.DataLancto);
                        dadosConciliacao.ValorLancto = itemDoGrid.ValorLancto;

                        listaConciliacaoParaEdicao.Add(dadosConciliacao);
                    }
                }
            }
            else
            {
                foreach (var item in listaDeDadosConciliacaoGrid)
                {
                    DadosConciliacao dadosConciliacao = new DadosConciliacao();

                    dadosConciliacao.Id = item.Id;
                    dadosConciliacao.ChaveOrigem1 = item.ChaveOrigem1;
                    dadosConciliacao.Origem1 = retorneStatusOrigem(item.Origem1);
                    dadosConciliacao.NumDoc = item.NumDoc;
                    dadosConciliacao.DescricaoDoc = item.DescricaoDoc;
                    dadosConciliacao.DataVencimento = string.IsNullOrEmpty(item.DataVencimento) ? dadosConciliacao.DataVencimento : DateTime.Parse(item.DataVencimento);
                    dadosConciliacao.ValorDoc = item.ValorDoc;
                    dadosConciliacao.Origem2 = retorneStatusOrigem(item.Origem2);
                    dadosConciliacao.NumLancto = item.NumLancto;
                    dadosConciliacao.DescricaoLancto = item.DescricaoLancto;
                    dadosConciliacao.DataLancto = DateTime.Parse(item.DataLancto);
                    dadosConciliacao.ValorLancto = item.ValorLancto;

                    listaConciliacaoParaEdicao.Add(dadosConciliacao);
                }
            }

            return listaConciliacaoParaEdicao;
        }

        private void GereIdParaGrid()
        {
            for (int i = 0; i < listaDeDadosConciliacaoGrid.Count; i++)
            {
                listaDeDadosConciliacaoGrid[i].Id = i + 1;
            }
        }

        private void AtualizeGrid()
        {
            GereIdParaGrid();

            gcConciliacaoBancaria.DataSource = listaDeDadosConciliacaoGrid;
            gcConciliacaoBancaria.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.False;
            gcConciliacaoBancaria.RefreshDataSource();
        }

        private void CarregaGrid()
        {
            foreach (var dados in _listaDadosConciliacao)
            {
                ConciliacaoBancariaGrid conciliar = new ConciliacaoBancariaGrid();

                conciliar.IdConciliacao = dados.Id;
                conciliar.ChaveOrigem1 = dados.ChaveOrigem1.ToInt();
                conciliar.Id = dados.Id;
                conciliar.Origem1 = dados.Origem1 == null? null: dados.Origem1.Descricao();
                conciliar.NumDoc = dados.NumDoc;
                conciliar.DescricaoDoc = dados.DescricaoDoc;

                if (dados.DataVencimento == null)
                    conciliar.DataVencimento = string.Empty;
                else
                    conciliar.DataVencimento = dados.DataVencimento.Value.ToString("dd/MM/yyyy");
                
                conciliar.ValorDoc = dados.ValorDoc.ToDouble();
                conciliar.Origem2 = dados.Origem2.Descricao();
                conciliar.NumLancto = dados.NumLancto;
                conciliar.DescricaoLancto = dados.DescricaoLancto;
                conciliar.DataLancto = dados.DataLancto.ToString("dd/MM/yyyy");
                conciliar.ValorLancto = dados.ValorLancto;
                
                conciliar.StatusConciliacao = dados.StatusConciliacao.Descricao();

                if (dados.StatusConciliacao==EnumOrigemMovimentacaoBanco.CONCILIADO)
                {
                    conciliar.Imagem = Properties.Resources.icone_verde;
                }
                else if (dados.StatusConciliacao == EnumOrigemMovimentacaoBanco.IMPORTADO)
                {
                    conciliar.Imagem = Properties.Resources.icone_azul;
                }
                else if (dados.StatusConciliacao == EnumOrigemMovimentacaoBanco.IGNORADO)
                {
                    conciliar.Imagem = Properties.Resources.icone_vermelho;
                }
                else
                {
                    conciliar.Imagem = Properties.Resources.none_icon;
                }
                                 
                listaDeDadosConciliacaoGrid.Add(conciliar);
            }

            AtualizeGrid();
        }

        private void ExibirBotoesAtravesGrid()
        {
            var mudaBotao = listaDeDadosConciliacaoGrid.Find(x => x.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

            if (mudaBotao == null) return; 

            if (mudaBotao.Origem1==EnumOrigemConciliacaoBancaria.APAGAR.Descricao() || mudaBotao.Origem1 == EnumOrigemConciliacaoBancaria.ARECEBER.Descricao())
            {
                btnPagarReceber.Visible = true;
            }
            else
            {
                btnPagarReceber.Visible = false;
            }
        }
        #endregion

        #region "Classes Auxiliares"

        public class DadosConciliacao
        {   
            public int ChaveOrigem1 { get; set; }
            public int Id { get; set; }
            public EnumOrigemConciliacaoBancaria? Origem1 { get; set; }
            public string NumDoc { get; set; }
            public string DescricaoDoc { get; set; }
            public DateTime DataVencimento { get; set; }
            public double ValorDoc { get; set; }
            public EnumOrigemConciliacaoBancaria? Origem2 { get; set; }
            public string NumLancto { get; set; }
            public string DescricaoLancto { get; set; }
            public DateTime DataLancto { get; set; }
            public double ValorLancto { get; set; }

            public EnumOrigemMovimentacaoBanco StatusConcialiacao { get; set; }
            public Image Imagem { get; set; }
        }

        public class ConciliacaoBancariaGrid
        {
            public int IdConciliacao { get; set; }

            public int ChaveOrigem1 { get; set; }

            public int Id { get; set; }

            public string Origem1 { get; set; }

            public string NumDoc { get; set; }

            public string DescricaoDoc { get; set; }

            public string DataVencimento { get; set; }

            public double ValorDoc { get; set; }

            public string Origem2 { get; set; }

            public string NumLancto { get; set; }

            public string DescricaoLancto { get; set; }

            public string DataLancto { get; set; }

            public double ValorLancto { get; set; }
                       
            public string StatusConciliacao { get; set; }

            public Image Imagem { get; set; }
        }

        #endregion

    }
}
