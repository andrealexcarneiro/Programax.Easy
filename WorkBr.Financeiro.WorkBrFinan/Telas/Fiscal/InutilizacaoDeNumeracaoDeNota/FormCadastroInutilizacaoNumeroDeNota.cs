using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ;
using Programax.Easy.Servico.Fiscal.InutilizacaoNumeracaoNotaServ;
using Programax.Easy.Negocio.Fiscal.InutilizacaoNumeracaoNotaObj.ObjetoDeNegocio;
using Programax.Easy.View.ClassesAuxiliares;

namespace Programax.Easy.View.Telas.Fiscal.InutilizacaoDeNumeracaoDeNota
{
    public partial class FormCadastroInutilizacaoNumeroDeNota : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<InutilizacaoNumeracaoNota> _listaInutilizacoes;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroInutilizacaoNumeroDeNota()
        {
            InitializeComponent();

            txtAno.Text = DateTime.Now.ToString("yy");

            PreenchaCboModeloNotaFiscal();

            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe();
            var configuracaoNfe = servicoConfiguracaoNfe.ConsulteConfiguracoesNfe();

            txtSerie.Text = configuracaoNfe.Serie.ToString();

            _listaInutilizacoes = new List<InutilizacaoNumeracaoNota>();

            PesquiseTodasAsInutilizacoes();

            this.ActiveControl = txtAno;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void txtJustificativaCancelamento_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString().IndexOf('\n') > -1)
            {
                txtJustificativaCancelamento.Text = e.NewValue.ToString().Replace("\r\n", ", ");
                txtJustificativaCancelamento.Text = e.NewValue.ToString().Replace("\n", ", ");
            }
        }

        private void txtJustificativaCancelamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnInutilizarNumeracao_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja inutilizar esses números?", "Inutilização de números", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            Action actionSalvar = () =>
            {
                InutilizacaoNumeracaoNota inutilizacaoNumeracaoNota = new InutilizacaoNumeracaoNota();
                inutilizacaoNumeracaoNota.Ano = txtAno.Text.ToString();
                inutilizacaoNumeracaoNota.Justificativa = txtJustificativaCancelamento.Text;
                inutilizacaoNumeracaoNota.ModeloNotaFiscal = (EnumModeloNotaFiscal)cboModeloNotaFiscal.EditValue;
                inutilizacaoNumeracaoNota.NumeroFinal = txtNumeroFinal.Text.ToInt();
                inutilizacaoNumeracaoNota.NumeroInicial = txtNumeroInicial.Text.ToInt();
                inutilizacaoNumeracaoNota.Serie = txtSerie.Text.ToInt();

                ServicoInutilizacaoNumeracaoNota servicoInutilizacaoNumeracaoNota = new ServicoInutilizacaoNumeracaoNota();
                servicoInutilizacaoNumeracaoNota.Cadastre(inutilizacaoNumeracaoNota);

                PesquiseTodasAsInutilizacoes();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, mensagemDeSucesso: "Números inutilizados com sucesso!", tituloMensagemDeErro: "Inutilização com sucesso");
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboModeloNotaFiscal()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumModeloNotaFiscal>();

            cboModeloNotaFiscal.Properties.DisplayMember = "Descricao";
            cboModeloNotaFiscal.Properties.ValueMember = "Valor";
            cboModeloNotaFiscal.Properties.DataSource = lista;

            cboModeloNotaFiscal.EditValue = EnumModeloNotaFiscal.NFE;
        }

        private void PesquiseTodasAsInutilizacoes()
        {
            ServicoInutilizacaoNumeracaoNota servicoInutilizacaoNumeracaoNota = new ServicoInutilizacaoNumeracaoNota();
            _listaInutilizacoes = servicoInutilizacaoNumeracaoNota.ConsulteLista();

            PreenchaGrid();
        }

        private void PreenchaGrid()
        {
            List<InutilizacaoGrid> listaInutilizacaoGrid = new List<InutilizacaoGrid>();

            _listaInutilizacoes = _listaInutilizacoes.OrderByDescending(x => x.Id).ToList();

            foreach (var item in _listaInutilizacoes)
            {
                InutilizacaoGrid inutilizacaoGrid = new InutilizacaoGrid();
                inutilizacaoGrid.Id = item.Id;
                inutilizacaoGrid.Justificativa = item.Justificativa;
                inutilizacaoGrid.Modelo = item.ModeloNotaFiscal.Descricao();
                inutilizacaoGrid.NumeroFinal = item.NumeroFinal;
                inutilizacaoGrid.NumeroInicial = item.NumeroInicial;
                inutilizacaoGrid.Protocolo = item.Protocolo;
                inutilizacaoGrid.Serie = item.Serie;
                inutilizacaoGrid.Ano = item.Ano.ToInt();

                listaInutilizacaoGrid.Add(inutilizacaoGrid);
            }

            gcInutilizacoes.DataSource = listaInutilizacaoGrid;
            gcInutilizacoes.RefreshDataSource();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class InutilizacaoGrid
        {
            public int Id { get; set; }

            public int Ano { get; set; }

            public string Modelo { get; set; }

            public int Serie { get; set; }

            public int NumeroInicial { get; set; }

            public int NumeroFinal { get; set; }

            public string Protocolo { get; set; }

            public string Justificativa { get; set; }
        }

        #endregion
    }
}
