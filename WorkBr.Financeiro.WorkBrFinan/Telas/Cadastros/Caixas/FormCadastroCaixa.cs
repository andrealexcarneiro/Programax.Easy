using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.View.Telas.Cadastros.Caixas
{
    public partial class FormCadastroCaixa : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroCaixa()
        {
            InitializeComponent();

            PreenchaCboFuncionarios();
            PreenchaOStatus();
            PreenchaCboPerfilCaixa();

            this.NomeDaTela = "Cadastro de Caixa";

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Caixa caixa = new Caixa();

            caixa.Id = txtId.Text.ToInt();
            caixa.NomeCaixa = txtNomeCaixa.Text;
            caixa.DataCadastro = txtDataCadastro.Text.ToDate();
            caixa.Status = cboStatus.EditValue.ToString();
            caixa.PerfilCaixa = (EnumPerfilCaixa?)cboPerfilCaixa.EditValue;

            caixa.Funcionario = cboFuncionarios.EditValue != null ? new Pessoa { Id = cboFuncionarios.EditValue.ToInt() } : null;

            Action actionSalvar = () =>
            {
                ServicoCaixa servicoCaixa = new ServicoCaixa();

                if (caixa.Id == 0)
                {
                    servicoCaixa.Cadastre(caixa);
                }
                else
                {
                    servicoCaixa.Atualize(caixa);
                }

                txtId.Text = caixa.Id.ToString();

                PesquisePeloId();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPesquisaCaixa formPesquisaDeCaixaes = new FormPesquisaCaixa();

            var caregoria = formPesquisaDeCaixaes.PesquiseUmCaixa();

            if (caregoria != null)
            {
                EditeCaixa(caregoria);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePeloId();
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

        private void PreenchaCboPerfilCaixa()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumPerfilCaixa>();

            lista.Insert(0, null);

            cboPerfilCaixa.Properties.DataSource = lista;
            cboPerfilCaixa.Properties.DisplayMember = "Descricao";
            cboPerfilCaixa.Properties.ValueMember = "Valor";
        }

        private void LimpeFormulario()
        {
            EditeCaixa(null);
        }

        private void EditeCaixa(Caixa caixa, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (caixa != null)
            {
                txtId.Text = caixa.Id.ToString();
                txtNomeCaixa.Text = caixa.NomeCaixa;
                txtDataCadastro.Text = caixa.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = caixa.Status;
                cboPerfilCaixa.EditValue = caixa.PerfilCaixa;

                cboFuncionarios.EditValue = caixa.Funcionario != null ? (int?)caixa.Funcionario.Id : null;

                txtId.Enabled = false;

                txtNomeCaixa.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtNomeCaixa.Text = string.Empty;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                cboStatus.EditValue = "A";

                cboFuncionarios.EditValue = null;

                txtId.Enabled = true;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Caixa não encontrado", "Caixa não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                txtId.Focus();
            }
        }

        private void PreenchaCboFuncionarios()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaFuncionariosAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboFuncionarios.Properties.DisplayMember = "Descricao";
            cboFuncionarios.Properties.ValueMember = "Valor";
            cboFuncionarios.Properties.DataSource = listaObjetoValor;
        }

        private void PesquisePeloId()
        {
            ServicoCaixa servicoCaixa = new ServicoCaixa();
            var caregoria = servicoCaixa.Consulte(txtId.Text.ToInt());

            EditeCaixa(caregoria, exibirMensagemDeNaoEncontrado: true);
        }

        #endregion
    }
}
