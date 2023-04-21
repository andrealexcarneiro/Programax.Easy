using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Cadastros.Pessoas;

namespace Programax.Easy.View.Telas.Financeiro.MovimentacoesCaixa
{
    public partial class FormSelecaoClienteReciboMovimentacaoCaixa : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private DialogResult _resultadoOperacao;

        #endregion

        #region " PROPRIEDADES "

        public Pessoa Parceiro { get; set; }

        #endregion

        #region " CONSTRUTOR "

        public FormSelecaoClienteReciboMovimentacaoCaixa()
        {
            InitializeComponent();

            this.ActiveControl = txtIdPessoa;
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public DialogResult SelecioneParceiro()
        {
            this.AbrirTelaModal();

            return _resultadoOperacao;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            _resultadoOperacao = DialogResult.Cancel;
            this.Close();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Parceiro = !txtIdPessoa.Text.EstahVazioOuZerado() ? new Pessoa { Id = txtIdPessoa.Text.ToInt() } : null;

            _resultadoOperacao = DialogResult.OK;

            this.Close();
        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var pessoa = formPessoaPesquisa.PesquisePessoaAtiva();

            if (pessoa != null)
            {
                PreenchaPessoa(pessoa);
            }
        }

        private void txtIdPessoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PesquisePeloId();
            }
        }

        private void txtIdPessoa_Leave(object sender, EventArgs e)
        {
            PesquisePeloId();
        }

        #endregion

        #region " MÉTODOS PRIVADOS "

        private void PesquisePeloId()
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

        #endregion        
    }
}
