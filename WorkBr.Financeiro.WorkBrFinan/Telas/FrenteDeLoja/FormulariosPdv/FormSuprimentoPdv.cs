using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Negocio;

namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    public partial class FormSuprimentoPdv : FormularioBase
    {
        #region " CONSTRUTOR "

        public FormSuprimentoPdv()
        {
            InitializeComponent();

            this.ActiveControl = txtHistoricoDaMovimentacao;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            ConcluaSuprimento();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSangriaPdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F3)
            {
                ConcluaSuprimento();
            }
        }

        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConcluaSuprimento();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void ConcluaSuprimento()
        {
            Action actionCadastroItemCaixa = () =>
            {
                ServicoItemMovimentacaoCaixa servicoItemMovimentacaoCaixa = new ServicoItemMovimentacaoCaixa();
                ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
                ServicoCaixa servicoCaixa = new ServicoCaixa();

                var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

                if (caixa == null)
                {
                    MessageBoxAkil.Show("Usuário logado não contém caixa");

                    return;
                }

                ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa(false, false);
                var movimentacaoCaixa = servicoMovimentacaoCaixa.ConsulteCaixaAberto(caixa);

                ItemMovimentacaoCaixa itemMovimentacaoCaixa = new ItemMovimentacaoCaixa();

                itemMovimentacaoCaixa.EstahEstornado = false;
                itemMovimentacaoCaixa.FormaPagamento = servicoFormaPagamento.ConsultePeloTipo(EnumTipoFormaPagamento.DINHEIRO);
                itemMovimentacaoCaixa.HistoricoMovimentacoes = "SUPRIMENTO - " + txtHistoricoDaMovimentacao.Text;
                itemMovimentacaoCaixa.MovimentacaoCaixa = movimentacaoCaixa;
                itemMovimentacaoCaixa.TipoMovimentacao = EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO;
                itemMovimentacaoCaixa.Valor = txtValor.Text.ToDouble();
                itemMovimentacaoCaixa.OrigemMovimentacaoCaixa = EnumOrigemMovimentacaoCaixa.DIRETONOCAIXA;

                servicoItemMovimentacaoCaixa.Cadastre(itemMovimentacaoCaixa);

                MessageBoxAkil.Show("Suprimento efetuado com sucesso.", "Suprimento efetuado");

                this.Close();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionCadastroItemCaixa, exibirMensagemDeSucesso: false);
        }

        #endregion
    }
}
