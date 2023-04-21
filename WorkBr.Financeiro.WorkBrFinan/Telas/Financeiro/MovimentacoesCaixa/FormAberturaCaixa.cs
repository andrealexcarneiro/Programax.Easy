using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Caixas;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Servico;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Financeiro.CategoriaServ;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.View.Telas.Financeiro.Categorias;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;

namespace Programax.Easy.View.Telas.Financeiro.MovimentacoesCaixa
{
    public partial class FormAberturaCaixa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private MovimentacaoCaixa _movimentacaoCaixa;

        #endregion

        #region " CONSTRUTOR "

        public FormAberturaCaixa()
        {
            InitializeComponent();

            if (!Sessao.GrupoAcesso.Tesoureiro)
            {
                txtIdCaixa.Properties.ReadOnly = true;
                btnPesquisarCaixa.Visible = false;
            }

            MostreCategoriaFinanceira();
            PreenchaCboCategorias();
            cboCategoriaFinanceira.EditValue = 24; //Saldo Inicial - > Padrão. 15/08/2021
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormPesquisaCaixa formPesquisaDeCaixaes = new FormPesquisaCaixa();

            var caregoria = formPesquisaDeCaixaes.PesquiseUmCaixa();

            if (caregoria != null)
            {
                PreenchaInformacoesCaixa(caregoria);
            }
        }

        private void txtIdCaixa_Leave(object sender, EventArgs e)
        {
            if (txtIdCaixa.Enabled && !string.IsNullOrEmpty(txtIdCaixa.Text))
            {
                ServicoCaixa servicoCaixa = new ServicoCaixa();
                var caregoria = servicoCaixa.Consulte(txtIdCaixa.Text.ToInt());

                PreenchaInformacoesCaixa(caregoria, exibirMensagemDeNaoEncontrado: true);
            }
        }

        private void btnAbrirCaixa_Click(object sender, EventArgs e)
        {
            //Valida se tiver usando conciliação bancária é obrigado a informar categoria
            var parametros = new ServicoParametros().ConsulteParametros();
            if (parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria && cboCategoriaFinanceira.EditValue == null)
            {
                MessageBox.Show("Você está usando a Conciliação Bancária. É obrigatório informar a Categoria para Dinheiro ao Abrir o Caixa.", "Abertura de Caixa");
                return;
            }

            Action actionMovimentacao = () =>
            {
                Caixa caixa = new Caixa { Id = txtIdCaixa.Text.ToInt() };

                MovimentacaoCaixa movimentacaoCaixa = new MovimentacaoCaixa();

                movimentacaoCaixa.Caixa = caixa;
                movimentacaoCaixa.ObservacoesAbertura = txtObs.Text;
                movimentacaoCaixa.DataHoraAbertura = txtDataHoraAbertura.Text.ToDate();
                movimentacaoCaixa.UsuarioAbertura = Sessao.PessoaLogada;

                ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();
                
                //O Registro de abertura é feito em Dinheiro
                movimentacaoCaixa.SaldoInicialDinheiro = txtValorDinheiro.Text.ToDouble();
                movimentacaoCaixa.SaldoInicialCheque = txtValorCheque.Text.ToDouble();
                movimentacaoCaixa.Status = EnumStatusMovimentacaoCaixa.ABERTO;

                //Categoria Financeira DINHEIRO
                movimentacaoCaixa.CategoriaFinanceira = cboCategoriaFinanceira.EditValue.ToInt() == 0 ? null : 
                                                        new CategoriaFinanceira { Id = cboCategoriaFinanceira.EditValue.ToInt() };

                servicoMovimentacaoCaixa.Cadastre(movimentacaoCaixa);

                //Depois é lançado o registro em Cheque
                if (txtValorCheque.Text.ToDouble() != 0)
                {                    
                    servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();

                    movimentacaoCaixa.Caixa = caixa;
                    movimentacaoCaixa.UsuarioAbertura = Sessao.PessoaLogada;
                    movimentacaoCaixa.DataHoraAbertura = txtDataHoraAbertura.Text.ToDate();
                    movimentacaoCaixa.ObservacoesAbertura = txtObs.Text;
                    movimentacaoCaixa.SaldoInicialCheque = txtValorCheque.Text.ToDouble();

                    //Categoria Financeira CHEQUE
                    movimentacaoCaixa.CategoriaFinanceira = cboCategoriaCheque.EditValue.ToInt() == 0 ? null :
                                                            new CategoriaFinanceira { Id = cboCategoriaCheque.EditValue.ToInt() };

                    servicoMovimentacaoCaixa.CadastreCheque(movimentacaoCaixa);
                }
                
                servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();
                _movimentacaoCaixa = servicoMovimentacaoCaixa.Consulte(movimentacaoCaixa.Id);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionMovimentacao, this, fecharFormAoConcluirOperacao: true, mensagemDeSucesso: "O Caixa foi aberto com sucesso!");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        

        public MovimentacaoCaixa AbrirCaixa(Caixa caixa)
        {
            PreenchaInformacoesCaixa(caixa);

            this.ShowDialog();

            return _movimentacaoCaixa;
        }

        #endregion

        #region " MÉTODOS PRIVADOS "

        private void MostreCategoriaFinanceira()
        {
            var parametros = new ServicoParametros().ConsulteParametros();

            if(parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria)
            {
                lblCategoria.Visible = true;
                cboCategoriaFinanceira.Visible = true;
                btnAdicionarCategoria.Visible = true;

                lblCategoriaCheque.Visible = true;
                cboCategoriaCheque.Visible = true;
                btnAdicionarCategoriaCheque.Visible = true;
            }
            else
            {
                this.Height = 300;
            }
        }

        private void PreenchaCboCategorias()
        {
            var categoria = new ServicoCategoria().ConsulteLista(string.Empty, null, "A", EnumTipoCategoria.RECEITA);

            categoria.Insert(0, null);

            cboCategoriaFinanceira.Properties.DisplayMember = "Descricao";
            cboCategoriaFinanceira.Properties.ValueMember = "Id";
            cboCategoriaFinanceira.Properties.DataSource = categoria;

            cboCategoriaCheque.Properties.DisplayMember = "Descricao";
            cboCategoriaCheque.Properties.ValueMember = "Id";
            cboCategoriaCheque.Properties.DataSource = categoria;

            if (cboCategoriaFinanceira.EditValue != null)
            {
                if (!categoria.Exists(categ => categ != null && categ.Id == cboCategoriaFinanceira.EditValue.ToInt()))
                {
                    cboCategoriaFinanceira.EditValue = null;
                }
            }

            if (cboCategoriaCheque.EditValue != null)
            {
                if (!categoria.Exists(categ => categ != null && categ.Id == cboCategoriaCheque.EditValue.ToInt()))
                {
                    cboCategoriaCheque.EditValue = null;
                }
            }

        }

        private void PreenchaInformacoesCaixa(Caixa caixa, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (caixa != null)
            {
                txtIdCaixa.Text = caixa.Id.ToString();
                txtNomeCaixa.Text = caixa.NomeCaixa;

                txtDataHoraAbertura.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                txtUsuarioAbertura.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.Razao;                

                ServicoMovimentacaoCaixa servicoCaixaMovimentacao = new ServicoMovimentacaoCaixa();

                var caixamovimentacao = servicoCaixaMovimentacao.ConsulteLista(caixa, EnumDataFiltrarMovimentacaoCaixa.DATAFECHAMENTO, DateTime.Now.AddMonths(-1), DateTime.Now, EnumStatusMovimentacaoCaixa.FECHADO);

                //Verifica se tem caixa dentro do primeiro mês
                if (caixamovimentacao.Count != 0)
                {
                    if (caixamovimentacao != null)
                    {
                        txtValorDinheiro.Text = caixamovimentacao.LastOrDefault().SaldoFinalDinheiro != 0 ? caixamovimentacao.LastOrDefault().SaldoFinalDinheiro.ToString("0.00") : "0.00";
                        txtValorCheque.Text = caixamovimentacao.LastOrDefault().SaldoFinalCheque != 0 ? caixamovimentacao.LastOrDefault().SaldoFinalCheque.ToString("0.00") : "0.00";
                    }
                    else
                    {
                        caixamovimentacao = servicoCaixaMovimentacao.ConsulteLista(caixa, EnumDataFiltrarMovimentacaoCaixa.DATAFECHAMENTO, DateTime.Now.AddMonths(-7), DateTime.Now, EnumStatusMovimentacaoCaixa.FECHADO);

                        //Senão tiver caixa no primeiro mês cai aqui para buscar até o sexto mês anterior
                        if (caixamovimentacao != null)
                        {
                            txtValorDinheiro.Text = caixamovimentacao.LastOrDefault().SaldoFinalDinheiro != 0 ? caixamovimentacao.LastOrDefault().SaldoFinalDinheiro.ToString("0.00") : "0.00";
                            txtValorCheque.Text = caixamovimentacao.LastOrDefault().SaldoFinalCheque != 0 ? caixamovimentacao.LastOrDefault().SaldoFinalCheque.ToString("0.00") : "0.00";
                        }
                        else
                        {
                            //Caso usuário não tiver nenhum caixa dentro dos perídos acima
                            txtValorDinheiro.Text = "0.00";
                            txtValorCheque.Text = "0.00";
                        }
                    }
                }
                else
                {
                    //Caso usuário nunca abriu um caixa
                    txtValorDinheiro.Text = "0.00";
                    txtValorCheque.Text = "0.00";
                }
            }
            else
            {
                txtIdCaixa.Text = string.Empty;
                txtNomeCaixa.Text = string.Empty;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Caixa não encontrado", "Caixa não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                this.ActiveControl = txtIdCaixa;
                txtIdCaixa.Focus();
            }
        }

        #endregion

        private void btnAdicionarCategoriaCheque_Click(object sender, EventArgs e)
        {
            FormCadastroCategoriasFinanceiras formCategoria = new FormCadastroCategoriasFinanceiras();
            formCategoria.ShowDialog();

            PreenchaCboCategorias();            
            cboCategoriaFinanceira.EditValue = null;
        }
    }
}
