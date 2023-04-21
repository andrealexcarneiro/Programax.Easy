using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Cadastros.Pessoas;

namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    public partial class FormSelecioneVendedor : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private DialogResult _dialogResult;

        #endregion

        #region " PROPRIEDADES "

        public Pessoa Vendedor { get; set; }

        #endregion

        #region " CONSTRUTOR "

        public FormSelecioneVendedor()
        {
            InitializeComponent();

            this.ActiveControl = txtIdVendedor;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void txtIdVendedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtIdVendedor.Text))
                {
                    ServicoPessoa servicoPessoa = new ServicoPessoa();
                    var vendedor = servicoPessoa.ConsulteVendedorAtivo(txtIdVendedor.Text.ToInt());

                    if (vendedor == null)
                    {
                        MessageBoxAkil.Show("Vendedor não encontrado!", "Aviso");

                        return;
                    }

                    txtNomeVendedor.Text = vendedor.DadosGerais.Razao;
                }
                else
                {
                    txtIdVendedor.Text = string.Empty;
                    txtNomeVendedor.Text = string.Empty;
                }
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair();
        }

        private void FormSelecioneVendedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                Selecione();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Sair();
            }
        }

        private void btnPesquisaVendedor_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            var vendedor = formPessoaPesquisa.PesquisePessoaVendedoraAtiva();

            if (vendedor != null)
            {
                txtIdVendedor.Text = vendedor.Id.ToString();
                txtNomeVendedor.Text = vendedor.DadosGerais.Razao;
            }
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public DialogResult PesquiseVendedor(Pessoa vendedor)
        {
            if (vendedor != null)
            {
                Vendedor = vendedor;

                txtIdVendedor.Text = vendedor.Id.ToString();
                txtNomeVendedor.Text = vendedor.DadosGerais.Razao;
            }

            this.AbrirTelaModal(true);

            return _dialogResult;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void Selecione()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            Vendedor = servicoPessoa.Consulte(txtIdVendedor.Text.ToInt());

            _dialogResult = DialogResult.OK;

            this.Close();
        }

        private void Sair()
        {
            _dialogResult = DialogResult.Cancel;

            this.Close();
        }

        #endregion
    }
}
