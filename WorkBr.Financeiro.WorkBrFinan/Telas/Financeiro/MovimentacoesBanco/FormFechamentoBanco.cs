using System;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.MovimentacaoBancoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Report.RelatoriosDevExpress.Financeiro;
using System.Collections.Generic;

namespace Programax.Easy.View.Telas.Financeiro.MovimentacoesBanco
{
    public partial class FormFechamentoBanco : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private MovimentacaoBanco _movimentacaoBanco;

        #endregion

        #region " CONSTRUTOR "

        public FormFechamentoBanco()
        {
            InitializeComponent();

            txtUsuarioFechamento.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.Razao;

        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnFecharCaixa_Click(object sender, EventArgs e)
        {
            Action actionMovimentacao = () =>
            {
                BancoParaMovimento banco = new BancoParaMovimento { Id = txtIdBanco.Text.ToInt() };

                var movimentacaoBancoClone = _movimentacaoBanco.CloneCompleto();

                movimentacaoBancoClone.Banco = banco;
                movimentacaoBancoClone.ObservacoesFechamento = txtObs.Text;
                movimentacaoBancoClone.Status = EnumStatusMovimentacaoCaixa.FECHADO;
                movimentacaoBancoClone.UsuarioFechamento = Sessao.PessoaLogada;
                movimentacaoBancoClone.DataHoraFechamento = txtDataHoraFechamento.Text.ToDate();

                         
                ServicoMovimentacaoBanco servicoMovimentacaoBanco = new ServicoMovimentacaoBanco();

                servicoMovimentacaoBanco.Atualize(movimentacaoBancoClone);

                _movimentacaoBanco = servicoMovimentacaoBanco.Consulte(movimentacaoBancoClone.Id);

                if (MessageBox.Show("Deseja emitir o relatório de movimentação de caixa?", "Emitir relatório de movimentação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    RelatorioMovimentacaoBanco relatorioMovimentacaoBanco = new RelatorioMovimentacaoBanco(_movimentacaoBanco, new List<string>(), null, null);
                    TratamentosDeTela.ExibirRelatorio(relatorioMovimentacaoBanco);
                }
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionMovimentacao, this, fecharFormAoConcluirOperacao: true, mensagemDeSucesso: "O Caixa foi fechado com sucesso!");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }
        
        #endregion

        #region " MÉTODOS PÚBLICOS "

        public MovimentacaoBanco FecharBanco(MovimentacaoBanco movimentacaoBanco)
        {
            PreenchaDadosMovimentacaoBanco(movimentacaoBanco);

            this.ShowDialog();

            return _movimentacaoBanco;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaDadosMovimentacaoBanco(MovimentacaoBanco movimentacaoBanco)
        {
            if (movimentacaoBanco == null)
            {
                return;
            }

            ServicoMovimentacaoBanco servicoMovimentacaoBanco = new ServicoMovimentacaoBanco();
            var movimentacao = servicoMovimentacaoBanco.Consulte(movimentacaoBanco.Id);

            _movimentacaoBanco = movimentacao;

            _movimentacaoBanco.SaldoFinal = movimentacaoBanco.SaldoFinal;
           
            txtIdBanco.Text = _movimentacaoBanco.Banco.Id.ToString();
            txtDescricaoBanco.Text = _movimentacaoBanco.Banco.NomeBanco;
            txtNrRegistroBanco.Text = _movimentacaoBanco.Id.ToString();

            txtDataHoraFechamento.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            txtUsuarioFechamento.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.Razao;
                       
            double totalDinheiro = 0;

            foreach (var item in _movimentacaoBanco.ListaItensBanco)
            {               
                    if (!item.EstahEstornado)
                    if (item.TipoMovimentacao == EnumTipoMovimentacaoBanco.ENTRADA)
                    {
                        totalDinheiro += item.Valor;
                    }
                    else
                    {
                        totalDinheiro -= item.Valor;
                    }
            }

            txtSaldoFinal.Text = totalDinheiro.ToString("0.00");
        }

        #endregion
    }
}
