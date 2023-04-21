using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ;

namespace Programax.Easy.View.Telas.Financeiro.FormasPagamento
{
    public partial class FormCadastroFormaPagamento : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<CondicaoDePagamentoDaForma> _listaCondicoesPagamento;
        private List<CondicaoPagamento> _listaCondicoesPagamentoDoBanco;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroFormaPagamento()
        {
            InitializeComponent();

            PreenchaOStatus();

            this.NomeDaTela = "Forma de Pagamento";

            _listaCondicoesPagamento = new List<CondicaoDePagamentoDaForma>();

            PreenchaCboCondicaoPagamento();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePeloId();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            FormaPagamento formaPagamento = new FormaPagamento();

            formaPagamento.Id = txtId.Text.ToInt();
            formaPagamento.Descricao = txtDescricao.Text;
            formaPagamento.Status = cboStatus.EditValue.ToString();
            formaPagamento.DataCadastro = txtDataCadastro.Text.ToDate();

            formaPagamento.DisponivelParaPdv = chkDisponivelParaPdv.Checked;
            formaPagamento.DisponivelParaContasPagar = chkDisponivelParaContasAPagar.Checked;
            formaPagamento.DisponivelParaContasReceber = chkDisponivelParaContasAReceber.Checked;
            formaPagamento.DisponivelParaPedidoVenda = chkDisponivelPedidoVenda.Checked;

            formaPagamento.ListaCondicoesPagamento = _listaCondicoesPagamento;

            Action actionSalvar = () =>
            {
                ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

                if (formaPagamento.Id == 0)
                {
                    servicoFormaPagamento.Cadastre(formaPagamento);
                }
                else
                {
                    servicoFormaPagamento.Atualize(formaPagamento);
                }

                txtId.Text = formaPagamento.Id.ToString();

                PesquisePeloId();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void pbPesquisaFormaPagamento_Click(object sender, EventArgs e)
        {
            FormFormaPagamentoPesquisa formFormaPagamentoPesquisa = new FormFormaPagamentoPesquisa();

            var formaDePagamento = formFormaPagamentoPesquisa.ExibaPesquisaDeFormaPagamento();

            if (formaDePagamento != null)
            {
                EditeFormaPagamento(formaDePagamento);
            }
        }

        private void btnInserirCondicao_Click(object sender, EventArgs e)
        {
            if (cboCondicaoPagamento.EditValue != null)
            {
                if (cboCondicaoPagamento.EditValue.ToInt() == -1)
                {
                    _listaCondicoesPagamento.Clear();

                    foreach (var condicaoPagamento in _listaCondicoesPagamentoDoBanco)
                    {
                        CondicaoDePagamentoDaForma condicaoFormaPagamento = new CondicaoDePagamentoDaForma { CondicaoPagamento = condicaoPagamento };

                        _listaCondicoesPagamento.Add(condicaoFormaPagamento);
                    }
                }
                else
                {
                    if (_listaCondicoesPagamento.Exists(x => x.CondicaoPagamento.Id == cboCondicaoPagamento.EditValue.ToInt()))
                    {
                        MessageBox.Show("Esta condição já está adicionada.");

                        return;
                    }

                    CondicaoDePagamentoDaForma condicaoFormaPagamento = new CondicaoDePagamentoDaForma();

                    CondicaoPagamento condicaoPagamento = new CondicaoPagamento { Id = cboCondicaoPagamento.EditValue.ToInt(), Descricao = cboCondicaoPagamento.Text };

                    condicaoFormaPagamento.CondicaoPagamento = condicaoPagamento;

                    _listaCondicoesPagamento.Add(condicaoFormaPagamento);
                }

                cboCondicaoPagamento.Focus();

                PreenchaGrid();
            }
        }

        private void btnExcluirCondicao_Click(object sender, EventArgs e)
        {
            if (_listaCondicoesPagamento.Count > 0)
            {
                var idItem = colunaId.View.GetFocusedRowCellValue(colunaId).ToInt();

                var itemASerExcluido = _listaCondicoesPagamento.FirstOrDefault(x => x.CondicaoPagamento.Id == idItem);

                var mensagemConfirmacaoExclusao = "Deseja excluir a condição de pagamento " + itemASerExcluido.CondicaoPagamento.Descricao + " ?";

                if (MessageBox.Show(mensagemConfirmacaoExclusao, "Deseja excluir esssa condição de pagamento?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    _listaCondicoesPagamento.Remove(itemASerExcluido);

                    PreenchaGrid();
                }
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaOStatus()
        {
            ObjetoParaComboBox objetoComboBoxAtivo = new ObjetoParaComboBox();
            objetoComboBoxAtivo.Valor = "A";
            objetoComboBoxAtivo.Descricao = "Ativo";

            ObjetoParaComboBox objetoComboBoxInativo = new ObjetoParaComboBox();
            objetoComboBoxInativo.Valor = "I";
            objetoComboBoxInativo.Descricao = "Inativo";

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivo);
            listaDeItensParaOComboBox.Add(objetoComboBoxInativo);

            cboStatus.Properties.DataSource = listaDeItensParaOComboBox;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";

            cboStatus.EditValue = "A";
        }

        private void LimpeFormulario()
        {
            EditeFormaPagamento(null);
        }

        private void EditeFormaPagamento(FormaPagamento formaPagamento)
        {
            _listaCondicoesPagamento.Clear();

            if (formaPagamento != null)
            {
                pnlInformacoesCadastrais1.Enabled = true;
                pnlInformacoesCadastrais2.Enabled = true;

                txtDescricao.Text = formaPagamento.Descricao;
                txtId.Text = formaPagamento.Id.ToString();

                cboStatus.EditValue = formaPagamento.Status;

                txtDataCadastro.Text = formaPagamento.DataCadastro.ToString("dd/MM/yyyy");

                chkDisponivelParaPdv.Checked = formaPagamento.DisponivelParaPdv;
                chkDisponivelParaContasAPagar.Checked = formaPagamento.DisponivelParaContasPagar;
                chkDisponivelParaContasAReceber.Checked = formaPagamento.DisponivelParaContasReceber;
                chkDisponivelPedidoVenda.Checked = formaPagamento.DisponivelParaPedidoVenda;

                _listaCondicoesPagamento = formaPagamento.ListaCondicoesPagamento.ToList();

                if (formaPagamento.TipoFormaPagamento != EnumTipoFormaPagamento.OUTROS)
                {
                    txtDescricao.Enabled = false;
                    cboStatus.Enabled = false;
                }
                else
                {
                    txtDescricao.Enabled = true;
                    cboStatus.Enabled = true;
                }

                txtId.Enabled = false;

                txtDescricao.Focus();
            }
            else
            {
                pnlInformacoesCadastrais1.Enabled = false;
                pnlInformacoesCadastrais2.Enabled = false;

                txtDescricao.Text = string.Empty;
                txtId.Text = string.Empty;

                cboStatus.EditValue = "A";
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                chkDisponivelParaPdv.Checked = false;
                chkDisponivelParaContasAPagar.Checked = false;
                chkDisponivelParaContasAReceber.Checked = false;
                chkDisponivelPedidoVenda.Checked = false;

                txtDescricao.Enabled = true;
                cboStatus.Enabled = true;
                txtId.Enabled = true;

                txtId.Focus();
            }

            PreenchaGrid();
        }

        private void PreenchaGrid()
        {
            List<CondicaoPagamento> listaCondicoesPagamento = new List<CondicaoPagamento>();

            foreach (var condicaoForma in _listaCondicoesPagamento)
            {
                listaCondicoesPagamento.Add(condicaoForma.CondicaoPagamento);
            }

            gcItens.DataSource = listaCondicoesPagamento;
            gcItens.RefreshDataSource();
        }

        private void PreenchaCboCondicaoPagamento()
        {
            ServicoCondicaoPagamento servicoCondicaoPagamento = new ServicoCondicaoPagamento();

            _listaCondicoesPagamentoDoBanco = servicoCondicaoPagamento.ConsulteListaCondicoesPagamentoAtivas();

            var lista = _listaCondicoesPagamentoDoBanco.CloneCompleto();

            lista.Insert(0, null);

            var condicaoTodos = new CondicaoPagamento { Id = -1, Descricao = "INSERIR TODAS AS CONDIÇÕES" };

            lista.Insert(1, condicaoTodos);

            cboCondicaoPagamento.Properties.DisplayMember = "Descricao";
            cboCondicaoPagamento.Properties.ValueMember = "Id";
            cboCondicaoPagamento.Properties.DataSource = lista;
        }

        private void PesquisePeloId()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
            var formaPagamento = servicoFormaPagamento.Consulte(txtId.Text.ToInt());

            EditeFormaPagamento(formaPagamento);
        }

        #endregion
    }
}
