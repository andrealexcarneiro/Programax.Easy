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
    public partial class FormSangriaPdv : FormularioBase
    {
        #region " CONSTRUTOR "

        public FormSangriaPdv()
        {
            InitializeComponent();

            PreenchaCboFormasPagamentos();
            PreenchaCboTipoMovimentacao();

            this.ActiveControl = cboTiposMovimentacoes;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            ConcluaSangria();
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
                ConcluaSangria();
            }
        }

        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConcluaSangria();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboFormasPagamentos()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
            var formaPagamentoDinheiro = servicoFormaPagamento.Consulte(1);
            var formaPagamentoCheque = servicoFormaPagamento.Consulte(4);

            ObjetoDescricaoValor objetoDescricaoValorDinheiro = new ObjetoDescricaoValor();
            objetoDescricaoValorDinheiro.Descricao = formaPagamentoDinheiro.Descricao;
            objetoDescricaoValorDinheiro.Valor = formaPagamentoDinheiro.Id;

            ObjetoDescricaoValor objetoDescricaoValorCheque = new ObjetoDescricaoValor();
            objetoDescricaoValorCheque.Descricao = formaPagamentoCheque.Descricao;
            objetoDescricaoValorCheque.Valor = formaPagamentoCheque.Id;

            List<ObjetoDescricaoValor> lista = new List<ObjetoDescricaoValor>();
            lista.Add(objetoDescricaoValorDinheiro);
            lista.Add(objetoDescricaoValorCheque);

            cboFormasPagamentos.Properties.DisplayMember = "Descricao";
            cboFormasPagamentos.Properties.ValueMember = "Valor";
            cboFormasPagamentos.Properties.DataSource = lista;

            cboFormasPagamentos.EditValue = 1;
        }

        private void PreenchaCboTipoMovimentacao()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoMovimentacaoCaixa>();
            lista.RemoveAt(0);

            cboTiposMovimentacoes.Properties.DisplayMember = "Descricao";
            cboTiposMovimentacoes.Properties.ValueMember = "Valor";
            cboTiposMovimentacoes.Properties.DataSource = lista;

            cboTiposMovimentacoes.EditValue = EnumTipoMovimentacaoCaixa.SAIDASANGRIA;
        }


        private void ConcluaSangria()
        {
            Action actionCadastroItemCaixa = () =>
            {   
                ServicoItemMovimentacaoCaixa servicoItemMovimentacaoCaixa = new ServicoItemMovimentacaoCaixa();

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

                FormaPagamento formaPagamento = new FormaPagamento();
                formaPagamento.Id = cboFormasPagamentos.EditValue.ToInt();
                formaPagamento.Descricao = cboFormasPagamentos.Text;
                formaPagamento.TipoFormaPagamento = cboFormasPagamentos.EditValue.ToInt() == 1 ? EnumTipoFormaPagamento.DINHEIRO : EnumTipoFormaPagamento.CHEQUE;

                itemMovimentacaoCaixa.EstahEstornado = false;
                itemMovimentacaoCaixa.FormaPagamento = formaPagamento;
                itemMovimentacaoCaixa.HistoricoMovimentacoes = "SANGRIA - " + txtHistoricoDaMovimentacao.Text;
                itemMovimentacaoCaixa.MovimentacaoCaixa = movimentacaoCaixa;
                itemMovimentacaoCaixa.TipoMovimentacao = (EnumTipoMovimentacaoCaixa)cboTiposMovimentacoes.EditValue;
                itemMovimentacaoCaixa.Valor = txtValor.Text.ToDouble();
                itemMovimentacaoCaixa.OrigemMovimentacaoCaixa = EnumOrigemMovimentacaoCaixa.DIRETONOCAIXA;

                servicoItemMovimentacaoCaixa.Cadastre(itemMovimentacaoCaixa);

                MessageBoxAkil.Show("Sangria efetuada com sucesso.", "Sangria efetuada");

                this.Close();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionCadastroItemCaixa, exibirMensagemDeSucesso: false);
        }

        #endregion
    }
}
