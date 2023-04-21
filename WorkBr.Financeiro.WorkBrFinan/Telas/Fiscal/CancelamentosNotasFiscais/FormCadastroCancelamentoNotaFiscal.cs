using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using NFe.Servicos;
using Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ;
using System.Threading;
using NFe.Servicos.Retorno;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.View.Telas.Fiscal.CancelamentosNotasFiscais
{
    public partial class FormCadastroCancelamentoNotaFiscal : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private NotaFiscal _notaFiscal;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroCancelamentoNotaFiscal()
        {
            InitializeComponent();

            PreenchaCboModeloNotaFiscal();

            this.ActiveControl = txtSerie;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void txtNumeroNfe_Leave(object sender, EventArgs e)
        {
            ConsulteNotaPeloNumeroESerie();
        }

        private void txtSerie_Leave(object sender, EventArgs e)
        {
            ConsulteNotaPeloNumeroESerie();
        }

        private void btnCancelarNfe_Click(object sender, EventArgs e)
        {
            if (_notaFiscal == null || _notaFiscal.InformacoesGeraisNotaFiscal.Status != EnumStatusNotaFiscal.AUTORIZADA)
            {
                MessageBox.Show("Informe uma nota fiscal autorizada.", "Nota autorizada");

                return;
            }

            string mensagemAlerta = "Ao cancelar esta nota, as seguintes ações serão executadas automáticamente:\n\n" +
                                                  "  01 - As contas a receber serão inativadas indepedente de sua quitação;\n\n" +
                                                  "  02 - Os produtos retornarão para o estoque;\n\n" +
                                                  "  03 - Será feito uma saída do caixa, caso tenha recebido por cartão de crédito, cartão de débito, dinheiro ou cheque;\n\n" +
                                                  "  04 - O documento vinculado a nota será cancelado(Ex.: Pedido de Venda, Outras Saídas, e demais tipos de documentos);\n\n" +
                                                  "  05 - ESTA É UMA AÇÃO QUE NÃO SERÁ POSSÍVEL SER DESFEITA.\n\n" +

                                                  "Deseja continuar?";

            if (MessageBox.Show(mensagemAlerta, "Cancelar Nota", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            Action actionSalvar = () =>
            {
                this.Cursor = Cursors.WaitCursor;
                ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
                servicoNotaFiscal.CanceleNotaFiscal(_notaFiscal.Id, txtJustificativaCancelamento.Text);

                _notaFiscal = servicoNotaFiscal.Consulte(_notaFiscal.Id);

                PreenchaCamposNota();
            };

            this.Cursor = Cursors.Default;
            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, mensagemDeSucesso: "Nota Fiscal cancelada com sucesso.");
        }

        private void txtJustificativaCancelamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
            }
        }

        private void txtJustificativaCancelamento_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString().IndexOf('\n') > -1)
            {
                txtJustificativaCancelamento.Text = e.NewValue.ToString().Replace("\r\n", ", ");
                txtJustificativaCancelamento.Text = e.NewValue.ToString().Replace("\n", ", ");
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnPesquiseNotaFiscal_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            FormPesquisaNotasFiscaisResumido formPesquisaNotasFiscaisResumido = new FormPesquisaNotasFiscaisResumido();
            var notaFiscal = formPesquisaNotasFiscaisResumido.PesquiseNotaFiscal();

            if (notaFiscal != null)
            {
                _notaFiscal = notaFiscal;

                PreenchaCamposNota();
            }

            Cursor = Cursors.Default;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void ConsulteNotaPeloNumeroESerie()
        {
            int serie = txtSerie.Text.ToInt();
            int numero = txtNumeroNfe.Text.ToInt();
            EnumModeloNotaFiscal modelo = (EnumModeloNotaFiscal)cboModeloNotaFiscal.EditValue;

            if (serie == 0 || numero == 0)
            {
                return;
            }

            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
            _notaFiscal = servicoNotaFiscal.Consulte(serie, numero, EnumStatusNotaFiscal.AUTORIZADA, modelo);

            PreenchaCamposNota();
        }

        private void PreenchaCamposNota()
        {
            if (_notaFiscal != null)
            {
                txtSerie.Text = _notaFiscal.IdentificacaoNotaFiscal.Serie.ToString();
                txtNumeroNfe.Text = _notaFiscal.IdentificacaoNotaFiscal.NumeroNota.ToString();

                txtChaveAcesso.Text = _notaFiscal.InformacoesGeraisNotaFiscal.ChaveDeAcesso;
                txtDataHoraEmissao.DateTime = _notaFiscal.IdentificacaoNotaFiscal.DataHoraEmissao;
                txtDataHoraSaida.DateTime = _notaFiscal.IdentificacaoNotaFiscal.DataHoraSaida.GetValueOrDefault();
                txtStatusNota.Text = _notaFiscal.InformacoesGeraisNotaFiscal.Status.Descricao();

                txtOrigem.Text = _notaFiscal.InformacoesDocumentoOrigemNotaFiscal.Origem.Descricao();
                txtNumeroDocumento.Text = _notaFiscal.InformacoesDocumentoOrigemNotaFiscal.DocumentoId.ToString();

                cboModeloNotaFiscal.EditValue = (EnumModeloNotaFiscal)_notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal;

                if (_notaFiscal.Destinatario != null)
                {
                    txtParceiroId.Text = _notaFiscal.Destinatario.Pessoa.Id.ToString();
                    txtRazaoSocialParceiro.Text = _notaFiscal.Destinatario.RazaoSocialOuNomeDestinatario;
                }
            }
            else
            {
                txtChaveAcesso.Text = string.Empty;
                txtDataHoraEmissao.Text = string.Empty;
                txtDataHoraSaida.Text = string.Empty;
                txtStatusNota.Text = string.Empty;

                txtOrigem.Text = string.Empty;
                txtNumeroDocumento.Text = string.Empty;
                txtParceiroId.Text = string.Empty;
                txtRazaoSocialParceiro.Text = string.Empty;
            }
        }

        private void PreenchaCboModeloNotaFiscal()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumModeloNotaFiscal>();

            cboModeloNotaFiscal.Properties.DisplayMember = "Descricao";
            cboModeloNotaFiscal.Properties.ValueMember = "Valor";
            cboModeloNotaFiscal.Properties.DataSource = lista;

            cboModeloNotaFiscal.EditValue = EnumModeloNotaFiscal.NFE;
        }

        #endregion
    }
}
