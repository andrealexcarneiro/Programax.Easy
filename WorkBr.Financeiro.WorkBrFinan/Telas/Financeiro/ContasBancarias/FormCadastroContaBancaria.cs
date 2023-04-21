using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContaBancariaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Financeiro.AgenciaServ;
using Programax.Easy.Servico.Financeiro.BancoServ;
using Programax.Easy.Servico.Financeiro.ContaBancariaServ;
using Programax.Easy.Servico.Financeiro.PlanoContasServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.View.Telas.Financeiro.Bancos;
using Programax.Easy.View.Telas.Financeiro.PlanosDeContas;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Financeiro.Agencias;

namespace Programax.Easy.View.Telas.Financeiro.ContasBancarias
{
    public partial class FormCadastroContaBancaria : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private PlanoDeContas _planoDeContas;
        private int _idContaBancaria;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroContaBancaria()
        {
            InitializeComponent();

            PreenchaOStatus();
            PreenchaCboBanco();
            PreenchaCboTipoConta();

            this.NomeDaTela = "Cadastro de Conta Bancária";

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            TrateUsuarioNaoTemPermissaoCadastroBanco();
            TrateUsuarioNaoTemPermissaoCadastroAgencia();

            this.ActiveControl = txtNumeroConta;
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            ContaBancaria contaBancaria = RetorneContaBancariaEmEdicao();

            Action actionSalvar = () =>
            {
                ServicoContaBancaria servicoContaBancaria = new ServicoContaBancaria();

                if (contaBancaria.Id == 0)
                {
                    servicoContaBancaria.Cadastre(contaBancaria);
                }
                else
                {
                    servicoContaBancaria.Atualize(contaBancaria);
                }

                PesquiseContaPeloNumero();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void pbPesquisaFormaPagamento_Click(object sender, EventArgs e)
        {
            FormContaBancariaPesquisa formContaBancariaPesquisa = new FormContaBancariaPesquisa();

            var contaBancaria = formContaBancariaPesquisa.ExibaPesquisaDeContasBancarias();

            if (contaBancaria != null)
            {
                EditeContaBancaria(contaBancaria);
            }
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
            var pessoa = formPessoaPesquisa.PesquisePessoa();

            if (pessoa != null)
            {
                PreenchaPessoa(pessoa);
            }
        }

        private void cboBancos_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboAgencias();
        }

        private void txtNumeroPlanoDeContas_Leave(object sender, EventArgs e)
        {
            ServicoPlanoDeContas servicoPlanoDeContas = new ServicoPlanoDeContas();

            var planoDeContas = servicoPlanoDeContas.ConsultePlanoDeContasAtivoPeloNumero(txtNumeroPlanoDeContas.Text);

            PreenchaPlanoDeContas(planoDeContas);
        }

        private void btnPesquisaPlanoDeContas_Click(object sender, EventArgs e)
        {
            FormPlanosContasPesquisa formPlanosContasPesquisa = new FormPlanosContasPesquisa();

            var planoDeContas = formPlanosContasPesquisa.ExibaPesquisaDePlanoDeContasAtivos();

            if (planoDeContas != null)
            {
                PreenchaPlanoDeContas(planoDeContas);
            }
        }

        private void btnAtalhoBanco_Click(object sender, EventArgs e)
        {
            ExibaCadastroBanco();
        }

        private void btnAtalhoAgencia_Click(object sender, EventArgs e)
        {
            ExibaCadastroAgencia();
        }

        private void txtNumeroConta_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNumeroConta.Text))
            {
                PesquiseContaPeloNumero();
            }
            else
            {
                EditeContaBancaria(null);
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

        private void PreenchaCboBanco()
        {
            ServicoBanco servicoBanco = new ServicoBanco();

            var listaBancos = servicoBanco.ConsulteListaAtiva();

            listaBancos.Insert(0, null);

            cboBancos.Properties.DataSource = listaBancos;
            cboBancos.Properties.DisplayMember = "Descricao";
            cboBancos.Properties.ValueMember = "Id";

            if (cboBancos.EditValue != null)
            {
                if (!listaBancos.Exists(banco => banco != null && banco.Id == cboBancos.EditValue.ToInt()))
                {
                    cboBancos.EditValue = null;
                }
            }
        }

        private void PreenchaCboAgencias()
        {
            if (cboBancos.EditValue != null)
            {
                cboAgencias.Enabled = true;
                btnAtalhoAgencia.Enabled = true;

                ServicoAgencia servicoAgencia = new ServicoAgencia();

                var listaAgencias = servicoAgencia.ConsulteLista(new Banco { Id = cboBancos.EditValue.ToInt() }, string.Empty, "A");

                List<AgenciaAuxiliarComboBox> listaAgenciasAuxiliares = new List<AgenciaAuxiliarComboBox>();

                listaAgenciasAuxiliares.Add(null);

                foreach (var agencia in listaAgencias)
                {
                    AgenciaAuxiliarComboBox contaBancariaAuxiliar = new AgenciaAuxiliarComboBox();

                    contaBancariaAuxiliar.Id = agencia.Id;

                    contaBancariaAuxiliar.Descricao = agencia.NumeroAgencia + " - " + agencia.DigitoAgencia + " - " + agencia.NomeAgencia;

                    listaAgenciasAuxiliares.Add(contaBancariaAuxiliar);
                }

                cboAgencias.Properties.DataSource = listaAgenciasAuxiliares;
                cboAgencias.Properties.DisplayMember = "Descricao";
                cboAgencias.Properties.ValueMember = "Id";

                if (cboAgencias.EditValue != null)
                {
                    if (!listaAgencias.Exists(agencia => agencia != null && agencia.Id == cboAgencias.EditValue.ToInt()))
                    {
                        cboAgencias.EditValue = null;
                    }
                }
            }
            else
            {
                cboAgencias.EditValue = null;
                cboAgencias.Enabled = false;
                btnAtalhoAgencia.Enabled = false;
            }
        }

        private void PreenchaCboTipoConta()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoContaBancaria>();

            lista.Insert(0, null);

            cboTipoConta.Properties.DataSource = lista;
            cboTipoConta.Properties.DisplayMember = "Descricao";
            cboTipoConta.Properties.ValueMember = "Valor";
        }

        private void LimpeFormulario()
        {
            EditeContaBancaria(null, limparNumeroConta: true, focoNumeroConta: true);
        }

        private void EditeContaBancaria(ContaBancaria contaBancaria, bool limparNumeroConta = false, bool focoNumeroConta = false)
        {
            if (contaBancaria != null)
            {
                _idContaBancaria = contaBancaria.Id;

                contaBancaria.PlanoDeContas.CarregueLazyLoad();

                PreenchaPessoa(contaBancaria.Pessoa);

                cboStatus.EditValue = contaBancaria.Status;

                cboBancos.EditValue = contaBancaria.Agencia != null ? (int?)contaBancaria.Agencia.Banco.Id : null;
                cboAgencias.EditValue = contaBancaria.Agencia != null ? (int?)contaBancaria.Agencia.Id : null;

                cboTipoConta.EditValue = contaBancaria.TipoContaBancaria;

                txtNumeroConta.Text = contaBancaria.NumeroConta;

                PreenchaPlanoDeContas(contaBancaria.PlanoDeContas);

                txtDataCadastro.Text = contaBancaria.DataCadastro.ToString("dd/MM/yyyy");

                cboTipoConta.Focus();
            }
            else
            {
                _idContaBancaria = 0;

                cboStatus.EditValue = "A";

                cboBancos.EditValue = null;
                cboAgencias.EditValue = null;

                PreenchaPessoa(null);

                cboTipoConta.EditValue = null;

                if (limparNumeroConta)
                {
                    txtNumeroConta.Text = string.Empty;
                }

                PreenchaPlanoDeContas(null);

                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                if (focoNumeroConta)
                {
                    txtNumeroConta.Focus();
                }
            }
        }

        private ContaBancaria RetorneContaBancariaEmEdicao()
        {
            ContaBancaria contaBancaria = new ContaBancaria();

            contaBancaria.Id = _idContaBancaria;

            contaBancaria.Status = cboStatus.EditValue.ToString();
            contaBancaria.Agencia = cboAgencias.EditValue != null ? new Agencia { Id = cboAgencias.EditValue.ToInt() } : null;

            contaBancaria.Pessoa = !string.IsNullOrEmpty(txtIdPessoa.Text) ? new Pessoa { Id = txtIdPessoa.Text.ToInt() } : null;
            contaBancaria.TipoContaBancaria = (EnumTipoContaBancaria?)cboTipoConta.EditValue.ToIntNullabel();

            contaBancaria.NumeroConta = txtNumeroConta.Text;

            contaBancaria.PlanoDeContas = _planoDeContas;
            contaBancaria.DataCadastro = txtDataCadastro.Text.ToDate();

            return contaBancaria;
        }

        private void PreenchaPessoa(Pessoa pessoa, bool exibirMensagemDeNaoEncontrado = false)
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

        private void PreenchaPlanoDeContas(PlanoDeContas planoDeContas, bool exibirMensagemDeNaoEncontrado = false)
        {
            _planoDeContas = planoDeContas;

            if (planoDeContas != null)
            {
                txtNumeroPlanoDeContas.Text = planoDeContas.NumeroPlanoDeContas;
                txtDescricaoPlanoContas.Text = planoDeContas.Descricao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Plano de Contas nao encontrado!", "Plano de Contas não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdPessoa.Focus();
                }

                txtNumeroPlanoDeContas.Text = string.Empty;
                txtDescricaoPlanoContas.Text = string.Empty;
            }
        }

        private void ExibaCadastroBanco()
        {
            FormCadastroBanco formCadastroBanco = new FormCadastroBanco();
            formCadastroBanco.ShowDialog();

            PreenchaCboBanco();
        }

        private void ExibaCadastroAgencia()
        {
            Banco banco = new Banco { Id = cboBancos.EditValue.ToInt() };

            FormCadastroAgencia formCadastroAgencia = new FormCadastroAgencia();
            formCadastroAgencia.ExibaCadastroAgencia(banco);

            PreenchaCboAgencias();
        }

        private void TrateUsuarioNaoTemPermissaoCadastroBanco()
        {
            var permissao = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.BANCOS);

            if (!permissao.Alterar)
            {
                cboBancos.Size = new Size(pnlBanco.Width, cboBancos.Size.Height);

                btnAtalhoBanco.Visible = false;
            }
        }

        private void TrateUsuarioNaoTemPermissaoCadastroAgencia()
        {
            var permissao = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.AGENCIAS);

            if (!permissao.Alterar)
            {
                cboAgencias.Size = new Size(pnlAgencia.Width, cboAgencias.Size.Height);

                btnAtalhoAgencia.Visible = false;
            }
        }

        private void PesquiseContaPeloNumero()
        {
            ServicoContaBancaria servicoContaBancaria = new ServicoContaBancaria();
            ContaBancaria contaBancaria = servicoContaBancaria.ConsultePeloNumeroConta(txtNumeroConta.Text);

            EditeContaBancaria(contaBancaria);
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class AgenciaAuxiliarComboBox
        {
            public int Id { get; set; }

            public string Descricao { get; set; }
        }

        #endregion

        private void cboAgencias_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
