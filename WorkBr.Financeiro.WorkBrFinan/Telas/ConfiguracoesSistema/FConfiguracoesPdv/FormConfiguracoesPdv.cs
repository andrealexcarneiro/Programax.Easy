using System;
using Programax.Easy.Negocio.ConfiguracoesSistema.ConfiguracoesPdvObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ConfiguracoesSistema.ConfiguracoesPdvServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using System.Windows.Forms;
using Programax.Easy.Servico.Cadastros.PessoaServ;

namespace Programax.Easy.View.Telas.ConfiguracoesSistema.FConfiguracoesPdv
{
    public partial class FormConfiguracoesPdv : FormularioPadrao
    {
        private int _idConfiguracoesPdv;

        public FormConfiguracoesPdv()
        {
            InitializeComponent();

            PreenchaCboTiposCartoes();

            ServicoConfiguracoesPdv servicoConfiguracoesPdv = new ServicoConfiguracoesPdv();
            var configuracaoPdv = servicoConfiguracoesPdv.ConsulteUltimaConfiguracaoPdv();

            _idConfiguracoesPdv = configuracaoPdv.Id;

            cboTipoCartaoCreditoEDebito.EditValue = configuracaoPdv.TipoCartaoCreditoDebito;
            chkVendaDjpdvGeraVendaAkil.Checked = configuracaoPdv.GereVendaAPartirDoPdv;
            txtDescontoMaximoCaixa.Text = configuracaoPdv.DescontoMaximoCaixa.ToString("0.00");
            chkFormaPagamentoEntradaIgualPedidoVenda.Checked = configuracaoPdv.FormaPagamentoEntradaIgualPedidoVenda;

            chkNotaFiscalPDV.Checked = configuracaoPdv.EmitirNotaFiscalDiretamenteNoPDV;
            chkPermitirFormasCondicaoPagamento.Checked = configuracaoPdv.PermitirFormaECondicaoPagamentoNoPDV;

            PreenchaCliente(configuracaoPdv.Cliente);
            PreenchaClienteTemporario(configuracaoPdv.ClienteTemporario);

            cboTipoCartaoCreditoEDebito.Focus();
        }

        private void PreenchaCboTiposCartoes()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoCartaoCreditoEDebito>();

            lista.Insert(0, null);

            cboTipoCartaoCreditoEDebito.Properties.DataSource = lista;
            cboTipoCartaoCreditoEDebito.Properties.ValueMember = "Valor";
            cboTipoCartaoCreditoEDebito.Properties.DisplayMember = "Descricao";
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            ConfiguracoesPdv configuracoesPdv = new ConfiguracoesPdv();

            configuracoesPdv.Id = _idConfiguracoesPdv;
            configuracoesPdv.TipoCartaoCreditoDebito = (EnumTipoCartaoCreditoEDebito?)cboTipoCartaoCreditoEDebito.EditValue;
            configuracoesPdv.GereVendaAPartirDoPdv = chkVendaDjpdvGeraVendaAkil.Checked;
            configuracoesPdv.DescontoMaximoCaixa = txtDescontoMaximoCaixa.Text.ToDouble();
            configuracoesPdv.FormaPagamentoEntradaIgualPedidoVenda = chkFormaPagamentoEntradaIgualPedidoVenda.Checked;

            configuracoesPdv.EmitirNotaFiscalDiretamenteNoPDV = chkNotaFiscalPDV.Checked;
            configuracoesPdv.PermitirFormaECondicaoPagamentoNoPDV = chkPermitirFormasCondicaoPagamento.Checked;

            configuracoesPdv.Cliente = !string.IsNullOrEmpty(txtIdCliente.Text) ? new Pessoa { Id = txtIdCliente.Text.ToInt() } : null;
            configuracoesPdv.ClienteTemporario = !string.IsNullOrEmpty(txtIdClienteTemporario.Text) ? new Pessoa { Id = txtIdClienteTemporario.Text.ToInt() } : null;

            Action actionSalvar = () =>
            {
                ServicoConfiguracoesPdv servicoConfiguracoesPdv = new ServicoConfiguracoesPdv();

                if (configuracoesPdv.Id == 0)
                {
                    servicoConfiguracoesPdv.Cadastre(configuracoesPdv);
                }
                else
                {
                    servicoConfiguracoesPdv.Atualize(configuracoesPdv);
                }
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            var cliente = formPessoaPesquisa.PesquisePessoaClienteAtiva();

            if (cliente != null)
            {
                PreenchaCliente(cliente);
            }
        }

        private void txtIdCliente_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdCliente.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();
                var cliente = servicoPessoa.ConsultePessoaAtiva(txtIdCliente.Text.ToInt());

                PreenchaCliente(cliente, true);
            }
            else
            {
                PreenchaCliente(null);
            }
        }

        private void txtIdClienteTemporario_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdClienteTemporario.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();
                var cliente = servicoPessoa.ConsultePessoaAtiva(txtIdClienteTemporario.Text.ToInt());

                PreenchaClienteTemporario(cliente, true);
            }
            else
            {
                PreenchaClienteTemporario(null);
            }
        }

        private void PreenchaCliente(Pessoa cliente, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (cliente != null)
            {
                txtIdCliente.Text = cliente.Id.ToString();
                txtNomeCliente.Text = cliente.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Cliente nao encontrado!", "Cliente não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdCliente.Focus();
                }

                txtIdCliente.Text = string.Empty;
                txtNomeCliente.Text = string.Empty;
            }
        }

        private void PreenchaClienteTemporario(Pessoa cliente, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (cliente != null)
            {
                txtIdClienteTemporario.Text = cliente.Id.ToString();
                txtNomeClienteTemporario.Text = cliente.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Cliente nao encontrado!", "Cliente não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdClienteTemporario.Focus();
                }

                txtIdClienteTemporario.Text = string.Empty;
                txtNomeClienteTemporario.Text = string.Empty;
            }
        }

        private void btnPesquisaPessoaTemp_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            var cliente = formPessoaPesquisa.PesquisePessoaClienteAtiva();

            if (cliente != null)
            {
                PreenchaClienteTemporario(cliente);
            }
        }
    }
}
