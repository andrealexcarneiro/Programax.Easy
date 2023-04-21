using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Estoque.EntradaMercadoriaServ;
using Programax.Easy.Negocio.Estoque.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.View.Telas.Cadastros.EntradaMercadorias
{
    public partial class FormPesquisaDeEntradaDeMercadorias : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<EntradaMercadoria> _listaEntradasMercadorias;
        private EntradaMercadoria _entradaMercadoriaSelecionada;
        private int _entradatipo;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaDeEntradaDeMercadorias(int entradamercadoria)
        {
            InitializeComponent();
            _entradatipo = entradamercadoria;

            _listaEntradasMercadorias = new List<EntradaMercadoria>();

            PreenchaCboStatus();
            PreenchaDatasInicialEFinal();

            Pesquise();

            this.ActiveControl = gcNotasEntrada;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcNotasEntrada_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcNotasEntrada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void txtRazaoSocialFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public EntradaMercadoria ExibaPesquisaDeEntradas(int tipo)
        {
            _entradatipo = tipo;
            this.ShowDialog();
         
            return _entradaMercadoriaSelecionada;
        }

        private void Pesquise()
        {
            EnumStatusEntrada? status = (EnumStatusEntrada?)cboStatus.EditValue;

            ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

            _listaEntradasMercadorias = servicoEntradaMercadoria.ConsulteLista(txtDataInicialEmissao.Text.ToDateNullabel(),
                                                                                                              txtDataFinalEmissao.Text.ToDateNullabel(),
                                                                                                              txtDataInicialEntrada.Text.ToDateNullabel(),
                                                                                                              txtDataFinalEntrada.Text.ToDateNullabel(),
                                                                                                              txtNumeroNfe.Text,
                                                                                                              txtRazaoSocialFornecedor.Text,
                                                                                                              status, _entradatipo);

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<EntradaAuxiliar> listaDeEntradasAuxiliares = new List<EntradaAuxiliar>();

            foreach (var entrada in _listaEntradasMercadorias)
            {
                EntradaAuxiliar entradaAuxiliar = new EntradaAuxiliar();

                entradaAuxiliar.Id = entrada.Id;
                entradaAuxiliar.DataEmissao = entrada.DataEmissao.GetValueOrDefault();
                entradaAuxiliar.DataEntrada = entrada.DataMovimentacao.GetValueOrDefault();
                entradaAuxiliar.NumeroNota = entrada.NumeroNota;
                entradaAuxiliar.RazaoSocialFornecedor = entrada.Fornecedor != null ? entrada.Fornecedor.DadosGerais.Razao : string.Empty;
                entradaAuxiliar.Status = entrada.StatusEntrada.Descricao();

                listaDeEntradasAuxiliares.Add(entradaAuxiliar);
            }

            gcNotasEntrada.DataSource = listaDeEntradasAuxiliares;
            gcNotasEntrada.RefreshDataSource();
        }

        private void Selecione()
        {
            _entradaMercadoriaSelecionada = null;

            if (_listaEntradasMercadorias != null && _listaEntradasMercadorias.Count > 0)
            {
                ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

                _entradaMercadoriaSelecionada = servicoEntradaMercadoria.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void PreenchaDatasInicialEFinal()
        {
            txtDataFinalEntrada.DateTime = DateTime.Now;
            txtDataInicialEntrada.DateTime = DateTime.Now.AddDays(-7);
        }

        private void PreenchaCboStatus()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusEntrada>();

            lista.Insert(0, null);

            cboStatus.Properties.DataSource = lista;
            cboStatus.Properties.DisplayMember = "Descricao";
            cboStatus.Properties.ValueMember = "Valor";

            cboStatus.EditValue = 0;
            cboStatus.EditValue = null;
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class EntradaAuxiliar
        {
            public int Id { get; set; }

            public DateTime DataEntrada { get; set; }

            public DateTime DataEmissao { get; set; }

            public string NumeroNota { get; set; }

            public string RazaoSocialFornecedor { get; set; }

            public string Status { get; set; }
        }

        #endregion
                
    }
}
