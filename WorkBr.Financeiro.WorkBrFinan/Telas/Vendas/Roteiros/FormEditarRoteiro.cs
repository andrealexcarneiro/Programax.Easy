using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.RoteiroServ;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Vendas.Enumeradores;

namespace Programax.Easy.View.Telas.Vendas.Roteiros
{
    public partial class FormEditarRoteiro : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Pessoa _pessoaCadastro;
        private List<Roteiro> _listaDeRoteiros;

        #endregion

        #region " CONSTRUTOR "

        public FormEditarRoteiro(int numeroRoteiro)
        {
            InitializeComponent();

            PreenchaPeriodos();
            PreenchaOStatus();

            InformeUsuarioContasAPagarReceber(Sessao.PessoaLogada);

            carregaRoteiroEdicao(numeroRoteiro);

            this.ActiveControl = txtIdPessoa;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtDataAgenda.Text == string.Empty)
            {
                MessageBox.Show("Para continuar, você precisa informar a Data da Agenda.\n\nPor favor informe para continuarmos.",
                                "Alterar Roteiro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            if (txtNomePessoa.Text == string.Empty)
            {
                MessageBox.Show("Para continuar, você precisa informar o Funcionário.\n\nPor favor informe para continuarmos.",
                                "Alterar Roteiro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            if ((EnumStatusRoteiro)cboStatus.EditValue == EnumStatusRoteiro.CONCLUIDO)
            {
                MessageBox.Show("Você não pode concluir o Roteiro / Agenda por aqui. Escolhar outro Status para continuar.",
                            "Alterar Roteiro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            Action actionSalvar = () =>
            {
                ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

                var roteiro = servicoRoteiro.Consulte(txtNumeroAgendaRoteiro.Text.ToInt());

                roteiro.PessoaFuncionario = new Pessoa { Id = txtIdPessoa.Text.ToInt() };
                roteiro.DataElaboracao = txtDataAgenda.DateTime;
                roteiro.Periodo = (EnumPeriodo)cboPeriodo.EditValue;
                roteiro.Status = (EnumStatusRoteiro)cboStatus.EditValue;

                servicoRoteiro.Atualize(roteiro);

                ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

                var pedido = servicoPedido.Consulte(roteiro.PedidoVenda.Id);

                pedido.StatusRoteiro = roteiro.Status;

                servicoPedido.Atualize(pedido);
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
            var pessoa = formPessoaPesquisa.PesquisePessoaFuncionarioAtiva();

            if (pessoa != null)
            {
                PreenchaPessoa(pessoa);
            }
        }

        private void btnPesquisaPedidoVendas_Click(object sender, EventArgs e)
        {
            FormPesquisaPedidoDeVenda formPesquisaPedidoDeVenda = new FormPesquisaPedidoDeVenda();

            var pedidoDeVenda = formPesquisaPedidoDeVenda.ExibaPesquisaDePedidosDeVenda();

            if (pedidoDeVenda != null)
            {
                txtNumeroPedidoVendas.Text = pedidoDeVenda.Id.ToString();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaPeriodos()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumPeriodo>();

            cboPeriodo.Properties.DataSource = lista;
            cboPeriodo.Properties.ValueMember = "Valor";
            cboPeriodo.Properties.DisplayMember = "Descricao";

            cboPeriodo.EditValue = EnumPeriodo.MANHA;
        }

        protected virtual ServicoContasPagarReceber RetorneServicoContasPagarOuReceber()
        {
            return null;
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

        protected virtual void InformeUsuarioContasAPagarReceber(Pessoa pessoaQueCadastrou)
        {
            _pessoaCadastro = pessoaQueCadastrou;
            txtUsuario.Text = pessoaQueCadastrou.Id + " - " + pessoaQueCadastrou.DadosGerais.Razao;
        }

        private void carregaRoteiroEdicao(int roteiroId)
        {
            var roteiro = new ServicoRoteiro().Consulte(roteiroId);

            txtIdPessoa.Text = roteiro.PessoaFuncionario != null? roteiro.PessoaFuncionario.Id.ToString():string.Empty;
            txtNomePessoa.Text = roteiro.PessoaFuncionario != null ? roteiro.PessoaFuncionario.DadosGerais.Razao:string.Empty;
            txtDataAgenda.Text = roteiro.DataElaboracao.Date.ToString("dd/MM/yyyy");
            txtNumeroPedidoVendas.Text = roteiro.PedidoVenda.Id.ToString();
            txtNumeroAgendaRoteiro.Text = roteiro.Id.ToString();
            cboPeriodo.EditValue = roteiro.Periodo;
            cboStatus.EditValue = roteiro.Status;
        }

        private void PreenchaOStatus()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusRoteiro>();
           
            cboStatus.Properties.DataSource = lista;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";
        }

        #endregion

        private void pnlInformacoesContaPagarReceber_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
