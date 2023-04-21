using System;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Easy.View.Componentes;
using System.Windows.Forms;

namespace Programax.Easy.View.Telas.Cadastros.Enderecos
{
    public partial class FormEnderecoPesquisa : FormularioPadrao
    {
        private Endereco _enderecoSelecionado;

        public FormEnderecoPesquisa()
        {
            InitializeComponent();

            ucEnderecoPesquisa.InformarMetodoDeRetornoDoRegistro(AposSelecionarRegistro);

            this.ActiveControl = ucEnderecoPesquisa;
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            ucEnderecoPesquisa.Selecione();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Endereco PesquiseEndereco()
        {
            this.ShowDialog();

            return _enderecoSelecionado;
        }

        public Endereco PesquiseEnderecoAtivo()
        {
            this.ucEnderecoPesquisa.cboStatus.EditValue = "A";
            this.ucEnderecoPesquisa.cboStatus.Enabled = false;

            this.ShowDialog();

            return _enderecoSelecionado;
        }

        private void AposSelecionarRegistro(Endereco endereco)
        {
            _enderecoSelecionado = endereco;

            this.Close();
        }
    }
}
