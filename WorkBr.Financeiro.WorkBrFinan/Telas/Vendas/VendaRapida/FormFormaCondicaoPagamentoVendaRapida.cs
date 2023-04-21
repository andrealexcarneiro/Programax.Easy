using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Financeiro.CrediarioServ;

namespace Programax.Easy.View.Telas.Vendas.VendaRapida
{
    public partial class FormFormaCondicaoPagamentoVendaRapida : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private DialogResult _resultadoOperacao;
        private int _pessoaId;

        #endregion

        #region " PROPRIEDADES "

        public FormaPagamento FormaPagamento { get; set; }

        public CondicaoPagamento CondicaoPagamento { get; set; }

        #endregion

        #region " CONSTRUTOR "

        public FormFormaCondicaoPagamentoVendaRapida()
        {
            InitializeComponent();
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public DialogResult EditeFormaECondicaoPagamento(FormaPagamento formaPagamento, CondicaoPagamento condicaoPagamento, int pessoaId)
        {
            _pessoaId = pessoaId;

            PreenchaCboFormaPagamento();
            PreenchaCboCondicaoPagamento();

            FormaPagamento = formaPagamento;
            CondicaoPagamento = condicaoPagamento;

            cboFormaPagamento.EditValue = formaPagamento != null ? (int?)formaPagamento.Id : null;
            cboCondicaoPagamento.EditValue = condicaoPagamento != null ? (int?)condicaoPagamento.Id : null;

            this.AbrirTelaModal();

            return _resultadoOperacao;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void cboFormaPagamento_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCondicaoPagamento();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            FormaPagamento = cboFormaPagamento.EditValue != null ? new FormaPagamento { Id = cboFormaPagamento.EditValue.ToInt() } : null;
            CondicaoPagamento = cboCondicaoPagamento.EditValue != null ? new CondicaoPagamento { Id = cboCondicaoPagamento.EditValue.ToInt() } : null;

            _resultadoOperacao = DialogResult.OK;

            this.Close();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            _resultadoOperacao = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboFormaPagamento()
        {
            List<FormaPagamento> lista = new List<Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio.FormaPagamento>();

            ServicoCrediario servicoAnaliseCredito = new ServicoCrediario();
            var analiseCredito = servicoAnaliseCredito.Consulte(_pessoaId);

            if (analiseCredito != null && analiseCredito.FormaPagamento != null)
            {
                if (analiseCredito != null)
                {
                    analiseCredito.CondicaoPagamento.CarregueLazyLoad();
                    analiseCredito.FormaPagamento.CarregueLazyLoad();
                }

                lista.Add(analiseCredito.FormaPagamento);
            }
            else
            {
                ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

                lista = servicoFormaPagamento.ConsulteListaAtivos();
            }

            lista.Insert(0, null);

            cboFormaPagamento.Properties.DisplayMember = "Descricao";
            cboFormaPagamento.Properties.ValueMember = "Id";
            cboFormaPagamento.Properties.DataSource = lista;

            if (string.IsNullOrEmpty(cboFormaPagamento.Text))
            {
                cboFormaPagamento.EditValue = null;
            }
        }

        private void PreenchaCboCondicaoPagamento()
        {
            List<CondicaoPagamento> listaCondicoes = new List<CondicaoPagamento>();

            ServicoCrediario servicoAnaliseCredito = new ServicoCrediario();
            var analiseCredito = servicoAnaliseCredito.Consulte(_pessoaId);

            if (analiseCredito != null && analiseCredito.CondicaoPagamento != null)
            {
                listaCondicoes.Add(analiseCredito.CondicaoPagamento);
            }
            else
            {
                ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
                var formaPagamento = servicoFormaPagamento.Consulte(cboFormaPagamento.EditValue.ToInt());

                if (formaPagamento != null &&
                    formaPagamento.ListaCondicoesPagamento != null &&
                    formaPagamento.ListaCondicoesPagamento.Count > 0)
                {
                    foreach (var item in formaPagamento.ListaCondicoesPagamento)
                    {
                        if (item.CondicaoPagamento.Status == "A")
                        {
                            listaCondicoes.Add(item.CondicaoPagamento);
                        }
                    }
                }
            }

            listaCondicoes.Insert(0, null);

            cboCondicaoPagamento.Properties.DisplayMember = "Descricao";
            cboCondicaoPagamento.Properties.ValueMember = "Id";
            cboCondicaoPagamento.Properties.DataSource = listaCondicoes;

            if (string.IsNullOrEmpty(cboCondicaoPagamento.Text))
            {
                cboCondicaoPagamento.EditValue = null;
            }
        }

        #endregion
    }
}
