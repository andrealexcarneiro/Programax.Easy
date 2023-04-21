using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;

namespace Programax.Easy.View.Telas.Cadastros.Pessoas
{
    public partial class FormPesquisaPessoa : Form
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Pessoa> _listaDePessoas;
        private Pessoa _pessoaSelecionada;
        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaPessoa()
        {
            InitializeComponent();

            this.ActiveControl = txtChave;
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public Pessoa ExibaPesquisaPessoaDoTipoCliente()
        {
            this.ShowDialog();

            return _pessoaSelecionada;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void pbPesquisaCadastro_Click(object sender, EventArgs e)
        {
            PesquisePessoas();
        }

        private void cbbTipoPesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tipoDaPesquisa = cbbTipoPesquisa.Text;

            if (tipoDaPesquisa == "Cpf")
            {
                txtChave.Mask = "999,999,999-99";
            }
            else if (tipoDaPesquisa == "Cnpj")
            {
                txtChave.Mask = "99,999,999/9999-99";
            }
            else
            {
                txtChave.Mask = string.Empty;
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            SelecionePessoa();
            this.Close();
        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gcPessoas_DoubleClick(object sender, EventArgs e)
        {
            SelecionePessoa();
            this.Close();
        }

        private void txtChave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PesquisePessoas();
            }
        }

        private void gcPessoas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelecionePessoa();
                this.Close();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private List<Pessoa> ConsultePessoasPeloFiltro(string tipoDaPesquisa, string chave)
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();

            if (tipoDaPesquisa == "Nome/Razão Social")
            {
                _listaDePessoas = servicoPessoa.ConsulteListaClientePelaRazaoSocial(chave);
            }
            else if (tipoDaPesquisa == "Nome Fantasia")
            {
                _listaDePessoas = servicoPessoa.ConsulteListaClientePeloNomeFantasia(chave);
            }
            else if (tipoDaPesquisa == "Cpf" || tipoDaPesquisa == "Cnpj")
            {
                _listaDePessoas = servicoPessoa.ConsulteListaClientePeloCpfCnpj(chave);
            }

            return _listaDePessoas;
        }

        private void SelecionePessoa()
        {
            if (_listaDePessoas != null && _listaDePessoas.Count > 0)
            {
                _pessoaSelecionada = _listaDePessoas.FirstOrDefault(pessoa => pessoa.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }
        }

        private void PesquisePessoas()
        {
            this.Cursor = Cursors.WaitCursor;

            var chaveParaConsulta = txtChave.Text;
            var tipoDaPesquisa = cbbTipoPesquisa.Text;

            gcPessoas.DataSource = ConsultePessoasPeloFiltro(tipoDaPesquisa, chaveParaConsulta);

            this.Cursor = Cursors.Default;
        }

        #endregion
        
    }
}
