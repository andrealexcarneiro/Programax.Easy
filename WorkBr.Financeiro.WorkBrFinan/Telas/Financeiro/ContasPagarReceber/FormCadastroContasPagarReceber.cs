using System;
using System.Collections.Generic;
using System.Linq;
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
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.View.Telas.Financeiro.PlanosDeContas;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.CategoriaServ;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;
using Programax.Easy.View.Telas.Financeiro.BancosParaMovimento;
using Programax.Easy.View.Telas.Financeiro.Categorias;
using Programax.Easy.Servico.Financeiro.OperadorasCartaoServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.View.Telas.Financeiro.OperadorasCartoes;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Financeiro.ContasPagarReceber
{
    public partial class FormCadastroContasPagarReceber : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private PlanoDeContas _planoDeContas;
        private Pessoa _pessoaCadastro;
        private List<ContaPagarReceber> _listaDeContasPagarReceber;
        private ContaPagarReceber _contaPagarReceberAtualizando;
        public EnumTipoOperacaoContasPagarReceber _tipoOperacao;
        private bool _ConciliacaoBancariaEstahHabilitada;
        private Parametros _parametros;
        
        #endregion

        #region " CONSTRUTOR "

        public FormCadastroContasPagarReceber()
        {
            InitializeComponent();

            _listaDeContasPagarReceber = new List<ContaPagarReceber>();

            PreenchaCboFormaPagamento();
            PreenchaCboPeriodicidade();

            InformeUsuarioContasAPagarReceber(Sessao.PessoaLogada);

            txtDataEmissao.DateTime = DateTime.Now.Date;
            txtDataVencimento.DateTime = DateTime.Now.Date;

            SetarVariavelPagarOuReceber();

            HabiliteControlesConciliacaoBancaria();

            this.ActiveControl = txtIdPessoa;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void cboOperadorasCartao_EditValueChanged(object sender, EventArgs e)
        {
            if (cboOperadorasCartao.EditValue != null)
            {
                var operadora = new ServicoOperadorasCartao().Consulte(cboOperadorasCartao.EditValue.ToInt());

                if (operadora != null)
                    cboBanco.EditValue = operadora.BancoParaMovimento.Id;
            }
        }

        private void btnAdicionarOperadorasCartao_Click(object sender, EventArgs e)
        {
            var retorno = new FormCadastroOperadorasCartao().ShowDialog();

            CarregaComboOperadorasDebitoCredito();

            cboOperadorasCartao.EditValue = null;
        }

        private void cboFormaDePagamento_EditValueChanged(object sender, EventArgs e)
        {

            if (cboFormaDePagamento.EditValue == null) return;

            if(((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.CARTAOCREDITO || 
              (EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.CARTAODEBITO) && 
              _tipoOperacao==EnumTipoOperacaoContasPagarReceber.RECEBER && _ConciliacaoBancariaEstahHabilitada)
            {   
                lblOperadorasCartao.Visible = true;
                cboOperadorasCartao.Visible = true;

                btnAdicionarOperadorasCartao.Visible = true;

                CarregaComboOperadorasDebitoCredito();
            }
            else
            {
                lblOperadorasCartao.Visible = false;
                cboOperadorasCartao.Visible = false;
                btnAdicionarOperadorasCartao.Visible = false;
            }
        }

        private void btnAdicionarCategoria_Click(object sender, EventArgs e)
        {
            FormCadastroCategoriasFinanceiras formCategoria = new FormCadastroCategoriasFinanceiras();
            formCategoria.ShowDialog();

            PreenchaCboCategorias(_tipoOperacao);

            cboCategoriaFinanceira.EditValue = null;
        }

        private void btnAdicionarBanco_Click(object sender, EventArgs e)
        {
            FormCadastroBancoParaMovimento formbanco = new FormCadastroBancoParaMovimento();
            formbanco.ShowDialog();

            PreenchaCboBancos();

            cboBanco.EditValue = null;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (_listaDeContasPagarReceber == null || _listaDeContasPagarReceber.Count == 0)
            {
                MessageBox.Show("Não foi gerado nenhuma parcela.\n\nPor favor gere pelo menos uma parcela.", "Nenhuma Parcela Gerada", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            Action actionSalvar = () =>
            {
                ValideTotalParcelasComTotalInformado();

                ServicoContasPagarReceber servicoContasPagarReceber = RetorneServicoContasPagarOuReceber();

                servicoContasPagarReceber.CadastreLista(_listaDeContasPagarReceber);

                LimpeFormulario();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
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
            var pessoa = formPessoaPesquisa.PesquisePessoaAtiva();

            if (pessoa != null)
            {
                PreenchaPessoa(pessoa);
            }
        }

        private void txtPlanoDeContas_Leave(object sender, EventArgs e)
        {
            ServicoPlanoDeContas servicoPlanoDeContas = new ServicoPlanoDeContas();

            var planoDeContas = servicoPlanoDeContas.ConsultePlanoDeContasAtivoPeloNumero(txtNumeroPlanoDeContas.Text);

            PreenchaPlanoDeContas(planoDeContas);
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

        private void btnGerarTitulos_Click(object sender, EventArgs e)
        {
            //Valida se tiver usando conciliação bancária é obrigado a informar categoria
            if (_parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria && cboCategoriaFinanceira.EditValue == null)
            {
                MessageBox.Show("Você está usando a Conciliação Bancária. É obrigatório informar a Categoria Financeira.", "Manuntenção de Títulos");
                return;
            }

            Action actionGerarTitulos = () =>
            {
                ServicoContasPagarReceber servicoContasPagarReceber = RetorneServicoContasPagarOuReceber();

                ContaPagarReceber contaPagarReceber = new ContaPagarReceber();

                contaPagarReceber.DataEmissao = txtDataEmissao.Text.ToDate();
                contaPagarReceber.DataVencimento = txtDataVencimento.Text.ToDateNullabel();
                contaPagarReceber.FormaPagamento = cboFormaDePagamento.EditValue != null ? new FormaPagamento { Id = cboFormaDePagamento.EditValue.ToInt(), Descricao = cboFormaDePagamento.Text } : null;

                contaPagarReceber.OperadorasCartao = cboOperadorasCartao.EditValue.ToInt() != 0 ? new OperadorasCartao {Id=cboOperadorasCartao.EditValue.ToInt()}:null;

                contaPagarReceber.Historico = txtHistorico.Text;
                contaPagarReceber.Juros = txtJuros.Text.ToDouble();
                contaPagarReceber.JurosEhPercentual = rdbJurosPercentual.Checked;
                contaPagarReceber.Multa = txtMulta.Text.ToDouble();
                contaPagarReceber.MultaEhPercentual = rdbMultaPercentual.Checked;
                contaPagarReceber.NumeroDocumento = txtNumeroDocumento.Text;
                contaPagarReceber.Pessoa = !string.IsNullOrEmpty(txtIdPessoa.Text) ? new Pessoa { Id = txtIdPessoa.Text.ToInt() } : null;
                contaPagarReceber.PlanoDeContas = _planoDeContas;

                //Movimentação Bancos
                contaPagarReceber.BancoParaMovimento = cboBanco.EditValue != null ? new BancoParaMovimento { Id = cboBanco.EditValue.ToInt()} : null;
                contaPagarReceber.CategoriaFinanceira = cboCategoriaFinanceira.EditValue != null ? new CategoriaFinanceira { Id = cboCategoriaFinanceira.EditValue.ToInt() } : null;

                contaPagarReceber.Usuario = _pessoaCadastro;
                contaPagarReceber.ValorParcela = txtValorTotal.Text.ToDouble();
                contaPagarReceber.OrigemDocumento = EnumOrigemDocumento.DIRETOCONTASARECEBER;

                servicoContasPagarReceber.ValideGeracaoParcelasContasPagarReceber(contaPagarReceber, txtQuantidadeParcelas.Text.ToInt());

                var listaDeContaspagarReceber = servicoContasPagarReceber.GereContasPagarReceber(contaPagarReceber.Pessoa,
                                                                                                                                         contaPagarReceber.DataEmissao,
                                                                                                                                         contaPagarReceber.DataVencimento.GetValueOrDefault(),
                                                                                                                                         contaPagarReceber.NumeroDocumento,
                                                                                                                                         contaPagarReceber.OrigemDocumento,
                                                                                                                                         contaPagarReceber.FormaPagamento,
                                                                                                                                         contaPagarReceber.PlanoDeContas,
                                                                                                                                         contaPagarReceber.BancoParaMovimento,
                                                                                                                                         contaPagarReceber.CategoriaFinanceira,
                                                                                                                                         contaPagarReceber.OperadorasCartao,
                                                                                                                                         contaPagarReceber.Historico,
                                                                                                                                         contaPagarReceber.Usuario,
                                                                                                                                         (EnumPeriodicidade)cboPeriodicidade.EditValue,
                                                                                                                                         contaPagarReceber.ValorParcela,
                                                                                                                                         contaPagarReceber.Multa,
                                                                                                                                         contaPagarReceber.Juros,
                                                                                                                                         contaPagarReceber.MultaEhPercentual,
                                                                                                                                         contaPagarReceber.JurosEhPercentual,
                                                                                                                                         txtQuantidadeParcelas.Text.ToInt(),
                                                                                                                                         txtValorEntrada.Text.ToDouble());

                servicoContasPagarReceber.ValideParcelas(listaDeContaspagarReceber);

                _listaDeContasPagarReceber = listaDeContaspagarReceber;

                PreenchaGridContasPagarReceber();

                //pnlInformacoesContaPagarReceber.Enabled = false;
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionGerarTitulos, exibirMensagemDeSucesso: false, tituloMensagemDeErro: "Erro ao gerar parcelas");
        }

        private void rdbMultaValor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMultaValor.Checked)
            {
                txtMulta.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,2})?";
            }
            else
            {
                txtMulta.Properties.Mask.EditMask = @"[0-9]{1,2}([\.\,][0-9]{0,2})?";
            }

            txtMulta.Focus();

            txtMulta.Text = txtMulta.Text;
        }

        private void rdbJurosValor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbJurosValor.Checked)
            {
                txtJuros.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,2})?";
            }
            else
            {
                txtJuros.Properties.Mask.EditMask = @"[0-9]{1,2}([\.\,][0-9]{0,2})?";
            }

            txtJuros.Focus();

            txtJuros.Text = txtJuros.Text;
        }

        private void rdbMultaTituloValor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMultaTituloValor.Checked)
            {
                txtMultaTitulo.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,2})?";
            }
            else
            {
                txtMultaTitulo.Properties.Mask.EditMask = @"[0-9]{1,2}([\.\,][0-9]{0,2})?";
            }

            txtMultaTitulo.Focus();

            txtMultaTitulo.Text = txtMultaTitulo.Text;
        }

        private void rdbJurosTituloValor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbJurosTituloValor.Checked)
            {
                txtJurosTitulo.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,2})?";
            }
            else
            {
                txtJurosTitulo.Properties.Mask.EditMask = @"[0-9]{1,2}([\.\,][0-9]{0,2})?";
            }

            txtJurosTitulo.Focus();

            txtJurosTitulo.Text = txtJurosTitulo.Text;
        }

        private void gcParcelas_DoubleClick(object sender, EventArgs e)
        {
            int numeroParcela = colunaParcela.View.GetFocusedRowCellValue(colunaParcela).ToInt();

            int numeroLinhasSelecionadas = colunaParcela.View.GetSelectedRows().Count();

            if (numeroLinhasSelecionadas > 1)
            {
                MessageBox.Show("Selecione apenas uma linha para editar a parcela.", "Apenas 1 registro pode ser atualizado por vez.");

                return;
            }
            else if (numeroLinhasSelecionadas == 1)
            {
                if (colunaParcela.View.GetSelectedRows()[0] != numeroParcela - 1)
                {
                    MessageBox.Show("Selecione apenas uma linha para editar a parcela.", "Apenas 1 registro pode ser atualizado por vez.");

                    return;
                }
            }

            rdbMultaTituloValor.Checked = true;
            rdbJurosTituloValor.Checked = true;

            _contaPagarReceberAtualizando = _listaDeContasPagarReceber[numeroParcela - 1];

            txtNumeroParcela.Text = numeroParcela.ToString();
            txtFormaPagamentoTitulo.Text = _contaPagarReceberAtualizando.FormaPagamento != null ? _contaPagarReceberAtualizando.FormaPagamento.Descricao : string.Empty;
            txtValorParcelaTitulo.Text = _contaPagarReceberAtualizando.ValorParcela.ToString("0.00");
            txtMultaTitulo.Text = _contaPagarReceberAtualizando.Multa.ToString("0.00");
            txtJurosTitulo.Text = _contaPagarReceberAtualizando.Juros.ToString("0.00");
            rdbMultaTituloPercentual.Checked = _contaPagarReceberAtualizando.MultaEhPercentual;
            rdbJurosTituloPercentual.Checked = _contaPagarReceberAtualizando.JurosEhPercentual;
            txtDataVencimentoTitulo.Text = _contaPagarReceberAtualizando.DataVencimento.GetValueOrDefault().ToString("dd/MM/yyyy");
            txtNumeroDocumentoTitulo.Text = _contaPagarReceberAtualizando.NumeroDocumento;
            txtSituacaoTitulo.Text = _contaPagarReceberAtualizando.Status.Descricao();

            txtValorParcelaTitulo.Focus();
        }

        private void btnAlterarTitulo_Click(object sender, EventArgs e)
        {
            Action actionAlterarParcela = () =>
            {

                if (_contaPagarReceberAtualizando == null)
                {
                    MessageBox.Show("É necessário selecionar uma parcela para poder atualizar.", "Selecione uma parcela");

                    return;
                }

                var cloneContaPagarReceber = _contaPagarReceberAtualizando.CloneCompleto();

                cloneContaPagarReceber.ValorParcela = txtValorParcelaTitulo.Text.ToDouble();
                cloneContaPagarReceber.Multa = txtMultaTitulo.Text.ToDouble();
                cloneContaPagarReceber.Juros = txtJurosTitulo.Text.ToDouble();

                cloneContaPagarReceber.MultaEhPercentual = rdbMultaTituloPercentual.Checked;
                cloneContaPagarReceber.JurosEhPercentual = rdbJurosTituloPercentual.Checked;

                cloneContaPagarReceber.DataVencimento = txtDataVencimentoTitulo.Text.ToDateNullabel();

                ServicoContasPagar servicoContaPagarReceber = new ServicoContasPagar();
                servicoContaPagarReceber.ValideContaPagarReceber(cloneContaPagarReceber);

                _contaPagarReceberAtualizando.ValorParcela = txtValorParcelaTitulo.Text.ToDouble();
                _contaPagarReceberAtualizando.Multa = txtMultaTitulo.Text.ToDouble();
                _contaPagarReceberAtualizando.Juros = txtJurosTitulo.Text.ToDouble();

                _contaPagarReceberAtualizando.MultaEhPercentual = rdbMultaTituloPercentual.Checked;
                _contaPagarReceberAtualizando.JurosEhPercentual = rdbJurosTituloPercentual.Checked;

                _contaPagarReceberAtualizando.DataVencimento = txtDataVencimentoTitulo.Text.ToDateNullabel();

                PreenchaGridContasPagarReceber();

                LimpeTituloSelecionado();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionAlterarParcela, exibirMensagemDeSucesso: false, tituloMensagemDeErro: "Erro ao atualizar parcela");
        }

        private void btnCancelarTitulo_Click(object sender, EventArgs e)
        {
            LimpeTituloSelecionado();
        }
        
        private void cboPeriodicidade_EditValueChanged(object sender, EventArgs e)
        {
            if (((EnumPeriodicidade)cboPeriodicidade.EditValue) == EnumPeriodicidade.UNICA)
            {
                txtQuantidadeParcelas.Text = "1";
                txtQuantidadeParcelas.Enabled = false;

                txtValorEntrada.Text = string.Empty;
                txtValorEntrada.Enabled = false;
            }
            else
            {
                txtQuantidadeParcelas.Enabled = true;
                txtValorEntrada.Enabled = true;
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void SetarVariavelPagarOuReceber()
        {
            var servico = RetorneServicoContasPagarOuReceber().ToString();

            if (servico.Contains("ServicoContasReceber"))
            {
                _tipoOperacao = EnumTipoOperacaoContasPagarReceber.RECEBER;
            }
            else
            {
                _tipoOperacao = EnumTipoOperacaoContasPagarReceber.PAGAR;
            }
        }

        private void HabiliteControlesConciliacaoBancaria()
        {
            _parametros = new ServicoParametros().ConsulteParametros();
            
            if(_parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
            {
                PreenchaCboCategorias(_tipoOperacao);
                PreenchaCboBancos();

                _ConciliacaoBancariaEstahHabilitada = true;

                cboBanco.Visible = true;
                btnAdicionarBanco.Visible = true;
                lblBanco.Visible = true;

                linha2.Visible = false;

                cboCategoriaFinanceira.Visible = true;
                btnAdicionarCategoria.Visible = true;
                lblCategoria.Visible = true;

                if (_tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER)
                {
                    cboOperadorasCartao.Enabled = true;
                    btnAdicionarOperadorasCartao.Enabled = true;
                }
                else
                {
                    cboOperadorasCartao.Enabled = false;
                    btnAdicionarOperadorasCartao.Enabled = false;
                }

                cboOperadorasCartao.Enabled = true;
                btnAdicionarOperadorasCartao.Enabled = true;
            }
            else
            {
                cboBanco.Visible = false;
                btnAdicionarBanco.Visible = false;
                lblBanco.Visible = false;

                linha2.Visible = true;

                cboCategoriaFinanceira.Visible = false;
                btnAdicionarCategoria.Visible = false;
                lblCategoria.Visible = false;

                cboOperadorasCartao.Enabled = false;
                btnAdicionarOperadorasCartao.Enabled = false;
            }
        }

        private void CarregaComboOperadorasDebitoCredito()
        {
            ServicoOperadorasCartao servicoOperadorasCartao = new ServicoOperadorasCartao();

            var operadoras = servicoOperadorasCartao.ConsulteLista();

            if (operadoras == null) return;

            List<ObjetoParaComboBox> lista = new List<ObjetoParaComboBox>();

            foreach (var item in operadoras)
            {
                ObjetoParaComboBox objeto = new ObjetoParaComboBox();

                if (!item.PermiteParcelamento && (EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.CARTAODEBITO)
                {
                    objeto.Descricao = item.Descricao;
                    objeto.Valor = item.Id;

                    lista.Add(objeto);
                }
                else if((EnumTipoFormaPagamento)cboFormaDePagamento.EditValue == EnumTipoFormaPagamento.CARTAOCREDITO)
                {
                    objeto.Descricao = item.Descricao;
                    objeto.Valor = item.Id;

                    lista.Add(objeto);
                }
            }

            cboOperadorasCartao.Properties.DisplayMember = "Descricao";
            cboOperadorasCartao.Properties.ValueMember = "Valor";
            cboOperadorasCartao.Properties.DataSource = lista;
        }

        private void PreenchaCboBancos()
        {
            List<BancoParaMovimento> banco = new List<BancoParaMovimento>();

            banco = new ServicoBancoParaMovimento().ConsulteLista(string.Empty, "A");

            if (banco.Count == 0) return;

            var idbanco = banco.Find(x => x.TornarPadrao == true).Id;

            banco.Insert(0, null);

            cboBanco.Properties.DisplayMember = "NomeBanco";
            cboBanco.Properties.ValueMember = "Id";
            cboBanco.Properties.DataSource = banco;

            cboBanco.EditValue = idbanco;

            if (cboBanco.EditValue != null)
            {
                if (!banco.Exists(banc => banc != null && banc.Id == cboBanco.EditValue.ToInt()))
                {
                    cboBanco.EditValue = null;
                }
            }
        }

        private void PreenchaCboCategorias(EnumTipoOperacaoContasPagarReceber tipoOperacao)
        {             
            var tipoCategoria = tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER? EnumTipoCategoria.RECEITA : EnumTipoCategoria.DESPESA;

            List<CategoriaFinanceira> categoria = new List<CategoriaFinanceira>();

            categoria = new ServicoCategoria().ConsulteLista(string.Empty, null, "A", tipoCategoria);

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

        protected virtual ServicoContasPagarReceber RetorneServicoContasPagarOuReceber()
        {
            return null;
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

        private void PreenchaCboPeriodicidade()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumPeriodicidade>();

            cboPeriodicidade.Properties.DataSource = lista;
            cboPeriodicidade.Properties.DisplayMember = "Descricao";
            cboPeriodicidade.Properties.ValueMember = "Valor";

            cboPeriodicidade.EditValue = EnumPeriodicidade.UNICA;
        }

        private void LimpeFormulario()
        {
            pnlInformacoesContaPagarReceber.Enabled = true;
            LimpeTituloSelecionado();
            _listaDeContasPagarReceber.Clear();
            PreenchaGridContasPagarReceber();

            PreenchaPessoa(null);
            PreenchaPlanoDeContas(null);
            txtDataEmissao.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtDataVencimento.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            txtNumeroDocumento.Text = string.Empty;
            cboFormaDePagamento.EditValue = 0;

            cboOperadorasCartao.EditValue = 0;

            txtHistorico.Text = string.Empty;
            txtValorTotal.Text = string.Empty;
            txtMulta.Text = string.Empty;
            txtJuros.Text = string.Empty;

            rdbMultaPercentual.Checked = true;
            rdbJurosPercentual.Checked = true;

            cboPeriodicidade.EditValue = EnumPeriodicidade.UNICA;

            txtIdPessoa.Focus();
        }

        protected virtual void PreenchaPessoa(Pessoa pessoa, bool exibirMensagemDeNaoEncontrado = false)
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

        protected virtual void PreenchaPlanoDeContas(PlanoDeContas planoDeContas, bool exibirMensagemDeNaoEncontrado = false)
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

        protected virtual void InformeUsuarioContasAPagarReceber(Pessoa pessoaQueCadastrou)
        {
            _pessoaCadastro = pessoaQueCadastrou;
            txtUsuario.Text = pessoaQueCadastrou.Id + " - " + pessoaQueCadastrou.DadosGerais.Razao;
        }

        private void PreenchaGridContasPagarReceber()
        {
            List<ContaPagarReceberGrid> listaContaPagarReceberGrid = new List<ContaPagarReceberGrid>();

            int numeroParcela = 1;

            foreach (var titulo in _listaDeContasPagarReceber)
            {
                ContaPagarReceberGrid tituloGrid = new ContaPagarReceberGrid();

                tituloGrid.Id = tituloGrid.Id;
                tituloGrid.Parcela = numeroParcela;
                tituloGrid.FormaPagamento = titulo.FormaPagamento != null ? titulo.FormaPagamento.Descricao : string.Empty;

                tituloGrid.Juros = titulo.Juros.ToString("#0.00");
                tituloGrid.Multa = titulo.Multa.ToString("#0.00");
                tituloGrid.NumeroDocumento = titulo.NumeroDocumento;
                tituloGrid.Status = titulo.Status.Descricao();
                tituloGrid.ValorParcela = titulo.ValorParcela.ToString("#0.00");
                tituloGrid.Vencimento = titulo.DataVencimento.GetValueOrDefault().ToString("dd/MM/yyyy");

                numeroParcela++;

                listaContaPagarReceberGrid.Add(tituloGrid);
            }

            gcParcelas.DataSource = listaContaPagarReceberGrid;
            gcParcelas.RefreshDataSource();
        }

        private void LimpeTituloSelecionado()
        {
            _contaPagarReceberAtualizando = null;

            txtNumeroParcela.Text = string.Empty;
            txtFormaPagamentoTitulo.Text = string.Empty;
            txtValorParcelaTitulo.Text = string.Empty;
            txtMultaTitulo.Text = string.Empty;
            txtJurosTitulo.Text = string.Empty;
            rdbMultaTituloPercentual.Checked = true;
            rdbJurosTituloPercentual.Checked = true;
            txtDataVencimentoTitulo.Text = string.Empty;
            txtNumeroDocumentoTitulo.Text = string.Empty;
            txtSituacaoTitulo.Text = string.Empty;

            gcParcelas.Focus();
        }

        private void ValideTotalParcelasComTotalInformado()
        {
            double totalParcelas = Math.Round(_listaDeContasPagarReceber.Sum(contaPagarReceber => contaPagarReceber.ValorParcela), 2);

            double totalInformado = Math.Round(txtValorTotal.Text.ToDouble(), 2);

            if (totalParcelas != totalInformado)
            {
                Inconsistencia inconsistencia = new Inconsistencia();
                inconsistencia.Mensagem = "Total das parcelas está diferente do total informado.";

                InconsistenciasDeValidacao inconsistenciasDeValidacao = new InconsistenciasDeValidacao();
                inconsistenciasDeValidacao.ListaDeInconsistencias.Add(inconsistencia);
                inconsistenciasDeValidacao.AssegureSucesso();
            }
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ContaPagarReceberGrid
        {
            public int Id { get; set; }

            public int Parcela { get; set; }

            public string ValorParcela { get; set; }

            public string FormaPagamento { get; set; }

            public string Multa { get; set; }

            public string Juros { get; set; }

            public string Vencimento { get; set; }

            public string NumeroDocumento { get; set; }

            public string Status { get; set; }
        }

        #endregion

    }
}
