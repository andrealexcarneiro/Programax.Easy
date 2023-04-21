using System;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Cadastros.Pessoas
{
    public partial class FormPessoaPesquisarefiltek : FormularioPadrao
    {
        private Pessoa _pessoaSelecionada;

       


        public FormPessoaPesquisarefiltek()
        {
            InitializeComponent();

            ucPessoaPesquisa.InformarMetodoDeRetornoDoRegistro(AposSelecionarRegistro);

            this.ActiveControl = ucPessoaPesquisa;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ucPessoaPesquisa.Selecione();
        }

        public Pessoa PesquisePessoa()
        {
            this.ShowDialog();

            return _pessoaSelecionada;
        }

        public Pessoa PesquisePessoaAtiva()
        {
            this.ucPessoaPesquisa.cboStatus.EditValue = "A";
            this.ucPessoaPesquisa.cboStatus.Enabled = false;

            this.ShowDialog();

            return _pessoaSelecionada;
        }

        public Pessoa PesquisePessoaSupervisora()
        {
            DesabiliteEDesmarqueTodosOsTiposDePessoa();
            
            ucPessoaPesquisa.chkEhSupervisor.Checked = true;

            this.ShowDialog();

            return _pessoaSelecionada;
        }

        public Pessoa PesquisePessoaIndicadora()
        {
            DesabiliteEDesmarqueTodosOsTiposDePessoa();

           ucPessoaPesquisa.chkEhIndicador.Checked = true;

            this.ShowDialog();

            return _pessoaSelecionada;
        }

        public Pessoa PesquisePessoaVendedora()
        {
            DesabiliteEDesmarqueTodosOsTiposDePessoa();

            ucPessoaPesquisa.chkEhVendedor.Checked = true;

            this.ShowDialog();

            return _pessoaSelecionada;
        }

        public Pessoa PesquisePessoaVendedoraAtiva()
        {
            DesabiliteEDesmarqueTodosOsTiposDePessoa();

            ucPessoaPesquisa.chkEhVendedor.Checked = true;

            this.ucPessoaPesquisa.cboStatus.EditValue = "A";
            this.ucPessoaPesquisa.cboStatus.Enabled = false;

            this.ShowDialog();

            return _pessoaSelecionada;
        }

        public Pessoa PesquisePessoaAtendente()
        {
            DesabiliteEDesmarqueTodosOsTiposDePessoa();

            ucPessoaPesquisa.chkEhAtendente.Checked = true;

            this.ShowDialog();

            return _pessoaSelecionada;
        }

        public Pessoa PesquisePessoaAtendenteAtiva()
        {
            DesabiliteEDesmarqueTodosOsTiposDePessoa();

            ucPessoaPesquisa.chkEhAtendente.Checked = true;

            this.ucPessoaPesquisa.cboStatus.EditValue = "A";
            this.ucPessoaPesquisa.cboStatus.Enabled = false;

            this.ShowDialog();

            return _pessoaSelecionada;
        }

        public Pessoa PesquisePessoaFornecedoraAtiva()
        {
            DesabiliteEDesmarqueTodosOsTiposDePessoa();

            ucPessoaPesquisa.chkEhFornecedor.Checked = true;

            this.ucPessoaPesquisa.cboStatus.EditValue = "A";
            this.ucPessoaPesquisa.cboStatus.Enabled = false;

            this.ShowDialog();

            return _pessoaSelecionada;
        }

        public Pessoa PesquisePessoaTransportadora()
        {
            DesabiliteEDesmarqueTodosOsTiposDePessoa();

            ucPessoaPesquisa.chkEhTransportadora.Checked = true;

            this.ShowDialog();

            return _pessoaSelecionada;
        }

        public Pessoa PesquisePessoaClienteAtiva()
        {
            DesabiliteEDesmarqueTodosOsTiposDePessoa();

            ucPessoaPesquisa.chkEhCliente.Checked = true;

            this.ucPessoaPesquisa.cboStatus.EditValue = "A";
            this.ucPessoaPesquisa.cboStatus.Enabled = false;

            this.ShowDialog();
           

            return _pessoaSelecionada;
        }

        public Pessoa PesquisePessoaFuncionarioAtiva()
        {
            DesabiliteEDesmarqueTodosOsTiposDePessoa();

            ucPessoaPesquisa.chkEhFuncionario.Checked = true;

           this.ucPessoaPesquisa.cboStatus.EditValue = "A";
            this.ucPessoaPesquisa.cboStatus.Enabled = false;

            this.ShowDialog();

            return _pessoaSelecionada;
        }

        private void DesabiliteEDesmarqueTodosOsTiposDePessoa()
        {
            ucPessoaPesquisa.chkEhAtendente.Checked = false;
            ucPessoaPesquisa.chkEhAtendente.Enabled = false;

            ucPessoaPesquisa.chkEhCliente.Checked = false;
            ucPessoaPesquisa.chkEhCliente.Enabled = false;

            ucPessoaPesquisa.chkEhFornecedor.Checked = false;
            ucPessoaPesquisa.chkEhFornecedor.Enabled = false;

            ucPessoaPesquisa.chkEhFuncionario.Checked = false;
            ucPessoaPesquisa.chkEhFuncionario.Enabled = false;

            ucPessoaPesquisa.chkEhIndicador.Checked = false;
            ucPessoaPesquisa.chkEhIndicador.Enabled = false;

            ucPessoaPesquisa.chkEhSupervisor.Checked = false;
            ucPessoaPesquisa.chkEhSupervisor.Enabled = false;

            ucPessoaPesquisa.chkEhTransportadora.Checked = false;
            ucPessoaPesquisa.chkEhTransportadora.Enabled = false;

            ucPessoaPesquisa.chkEhVendedor.Checked = false;
            ucPessoaPesquisa.chkEhVendedor.Enabled = false;
        }

        private void AposSelecionarRegistro(Pessoa pessoa)
        {
            _pessoaSelecionada = pessoa;

            this.Close();
        }
    }
}
