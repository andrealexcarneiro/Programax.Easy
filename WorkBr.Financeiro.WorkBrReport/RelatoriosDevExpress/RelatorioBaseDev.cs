using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Infraestrutura.Negocio.Utils;
//using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Report.RelatoriosDevExpress
{
    public partial class RelatorioBaseDev : DevExpress.XtraReports.UI.XtraReport
    {
        #region " VARIÁVEIS "

        protected string _tituloRelatorio;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioBaseDev()
        {
            InitializeComponent();
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public void GereRelatorio()
        {
            CarregueCabecalho();
            CarregueRodape();

            CarregueDadosRelatorio();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void lblPagina_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            lblPagina.Text = "Página: " + (e.PageIndex + 1) + " de " + e.PageCount;
        }

        #endregion

        #region " CABEÇALHO "

        private void CarregueCabecalho()
        {
            CarregueDadosEmpresa();
            CarregueTituloRelatorio();
        }

        private void CarregueTituloRelatorio()
        {
            lblTituloRelatorio.Text = _tituloRelatorio;
        }

        private void CarregueDadosEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            lblNomeEmpresa.Text = empresa.DadosEmpresa.NomeFantasia;
            lblEndereco.Text = empresa.DadosEmpresa.Endereco.Rua + ", " + empresa.DadosEmpresa.Endereco.Numero + " - " + empresa.DadosEmpresa.Endereco.Bairro;
            lblCidade.Text = empresa.DadosEmpresa.Endereco.Cidade.Descricao + " " + empresa.DadosEmpresa.Endereco.CEP + " " + empresa.DadosEmpresa.Endereco.Cidade.Estado.UF;
            lblTelefone.Text = "Fone: " + empresa.DadosEmpresa.Telefone;
            
            pctLogo.Image = empresa.DadosEmpresa.Foto.ToImage();

            empresa.DadosEmpresa.Foto.ToImage().Dispose();
        }

        #endregion

        #region " DADOS RELATORIO "

        protected virtual void CarregueDadosRelatorio()
        {
            throw new Exception("É necessário sobrescrever o método CarregueDadosRelatorio");
        }

        #endregion

        #region " RODAPÉ "

        private void CarregueRodape()
        {
            lblUsuario.Text = "Usuário: " + Sessao.PessoaLogada.DadosGerais.NomeFantasia;
            lblDataEmissao.Text = "Emitido em: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        #endregion
    }
}
