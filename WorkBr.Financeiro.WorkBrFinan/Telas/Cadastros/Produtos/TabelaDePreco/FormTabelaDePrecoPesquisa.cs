using System;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Produtos.TabelaDePreco
{
    public partial class FormTabelaDePrecoPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private TabelaPreco _tabelaPreco;

        #endregion

        #region " CONSTRUTOR "

        public FormTabelaDePrecoPesquisa()
        {
            InitializeComponent();

            ucTabelaDePrecoPesquisa.InformarMetodoDeRetornoDoRegistro(AposSelecionarRegistro);

            this.ActiveControl = ucTabelaDePrecoPesquisa;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ucTabelaDePrecoPesquisa.Selecione();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public TabelaPreco PesquiseTabelaDePreco()
        {
            this.ShowDialog();

            return _tabelaPreco;
        }

        private void AposSelecionarRegistro(TabelaPreco tabelaPreco)
        {
            _tabelaPreco = tabelaPreco;

            if (tabelaPreco != null)
            {
                this.Close();
            }
        }

        #endregion
    }
}
