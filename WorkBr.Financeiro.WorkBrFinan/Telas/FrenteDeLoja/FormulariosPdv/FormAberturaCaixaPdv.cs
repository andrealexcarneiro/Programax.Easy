using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Cadastros.CaixaServ;

namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    public partial class FormAberturaCaixaPdv : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private MovimentacaoCaixa _movimentacaoCaixa;

        #endregion

        #region " CONSTRUTOR "

        public FormAberturaCaixaPdv()
        {
            InitializeComponent();

            this.ActiveControl = txtValorDinheiro;
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public MovimentacaoCaixa AbraMovimentacaoCaixa(Caixa caixa)
        {
            PreenchaInformacoesCaixa(caixa);

            this.AbrirTelaModal(true);

            return _movimentacaoCaixa;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void FormAberturaCaixaPdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F3)
            {
                AbraCaixa();
            }
        }

        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AbraCaixa();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void AbraCaixa()
        {
            Action actionMovimentacao = () =>
            {
                ServicoCaixa servicoCaixa = new ServicoCaixa();
                var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

                MovimentacaoCaixa movimentacaoCaixa = new MovimentacaoCaixa();

                movimentacaoCaixa.Caixa = caixa;
                //movimentacaoCaixa.SaldoInicial = txtValor.Text.ToDouble();
                movimentacaoCaixa.Status = EnumStatusMovimentacaoCaixa.ABERTO;
                movimentacaoCaixa.UsuarioAbertura = Sessao.PessoaLogada;
                movimentacaoCaixa.DataHoraAbertura = DateTime.Now;

                ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();
                
                //O Registro de abertura é feito em Dinheiro
                movimentacaoCaixa.SaldoInicialDinheiro = txtValorDinheiro.Text.ToDouble();
                movimentacaoCaixa.SaldoInicialCheque = txtValorCheque.Text.ToDouble();
                movimentacaoCaixa.Status = EnumStatusMovimentacaoCaixa.ABERTO;

                servicoMovimentacaoCaixa.Cadastre(movimentacaoCaixa);

                //Depois é lançado o registro em Cheque
                if (txtValorCheque.Text.ToDouble() != 0)
                {
                    servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();

                    movimentacaoCaixa.Caixa = caixa;
                    movimentacaoCaixa.UsuarioAbertura = Sessao.PessoaLogada;
                    movimentacaoCaixa.DataHoraAbertura = DateTime.Now;                    
                    movimentacaoCaixa.SaldoInicialCheque = txtValorCheque.Text.ToDouble();

                    servicoMovimentacaoCaixa.CadastreCheque(movimentacaoCaixa);
                }
                
                servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();

                _movimentacaoCaixa = servicoMovimentacaoCaixa.Consulte(movimentacaoCaixa.Id);
                
                MessageBoxAkil.Show("O Caixa foi aberto com sucesso!", "Caixa aberto");

                this.Close();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionMovimentacao, exibirMensagemDeSucesso: false);
        }

        private void PreenchaInformacoesCaixa(Caixa caixa, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (caixa != null)
            {   
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
                
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Caixa não encontrado", "Caixa não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        #endregion

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AbraCaixa();
        }
    }
}
