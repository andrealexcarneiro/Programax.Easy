using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.NaturezaOperacaoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Cadastros.NaturezasOperacoes
{
    public partial class FormNaturezasOperacoesPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<NaturezaOperacao> _listaDeNaturezasOperacoes;
        private NaturezaOperacao _naturezaOperacaoSelecionada;

        #endregion

        #region " CONSTRUTOR "

        public FormNaturezasOperacoesPesquisa()
        {
            InitializeComponent();

            PreenchaOStatus();

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcNcms_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcNcms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void btnPesquisaNcm_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void cboStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public NaturezaOperacao ExibaPesquisaDeNaturezaOperacao()
        {
            this.ShowDialog();

            return _naturezaOperacaoSelecionada;
        }

        private void PreenchaOStatus()
        {
            ObjetoParaComboBox objetoComboBoxAtivoOuInativo = new ObjetoParaComboBox();
            objetoComboBoxAtivoOuInativo.Valor = string.Empty;
            objetoComboBoxAtivoOuInativo.Descricao = "Ativo ou Inativo";

            ObjetoParaComboBox objetoComboBoxAtivo = new ObjetoParaComboBox();
            objetoComboBoxAtivo.Valor = "A";
            objetoComboBoxAtivo.Descricao = "Ativo";

            ObjetoParaComboBox objetoComboBoxInativo = new ObjetoParaComboBox();
            objetoComboBoxInativo.Valor = "I";
            objetoComboBoxInativo.Descricao = "Inativo";

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivoOuInativo);
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivo);
            listaDeItensParaOComboBox.Add(objetoComboBoxInativo);

            cboStatus.Properties.DataSource = listaDeItensParaOComboBox;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";

            cboStatus.EditValue = string.Empty;
        }

        private void Pesquise()
        {
            ServicoNaturezaOperacao servicoNaturezaOperacao = new ServicoNaturezaOperacao();

            string status = cboStatus.EditValue != null ? cboStatus.EditValue.ToString() : string.Empty;

            _listaDeNaturezasOperacoes = servicoNaturezaOperacao.ConsulteLista(txtId.Text.ToIntNullabel(), txtDescricao.Text, status);

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<NaturezaOperacaoAuxiliar> listaDeNaturezasOperacoesAuxiliares = new List<NaturezaOperacaoAuxiliar>();

            foreach (var naturezaOperacao in _listaDeNaturezasOperacoes)
            {
                NaturezaOperacaoAuxiliar naturezaOperacaoAuxiliar = new NaturezaOperacaoAuxiliar();

                naturezaOperacaoAuxiliar.Descricao = naturezaOperacao.Descricao;
                naturezaOperacaoAuxiliar.Id = naturezaOperacao.Id;
                naturezaOperacaoAuxiliar.Status = naturezaOperacao.Status == "A" ? "ATIVO" : "INATIVO";
                naturezaOperacaoAuxiliar.TipoMovimentacao = naturezaOperacao.TipoMovimentacao.Descricao();
                naturezaOperacaoAuxiliar.OrigemDestino = naturezaOperacao.OrigemDestino.Descricao();

                listaDeNaturezasOperacoesAuxiliares.Add(naturezaOperacaoAuxiliar);
            }

            gcNaturezasOperacoes.DataSource = listaDeNaturezasOperacoesAuxiliares;
            gcNaturezasOperacoes.RefreshDataSource();
        }

        private void Selecione()
        {
            _naturezaOperacaoSelecionada = null;

            if (_listaDeNaturezasOperacoes != null && _listaDeNaturezasOperacoes.Count > 0)
            {
                ServicoNaturezaOperacao servicoNaturezaOperacao = new ServicoNaturezaOperacao();

                _naturezaOperacaoSelecionada = servicoNaturezaOperacao.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        #endregion

        #region " CLASSE AUXILIAR "

        private class NaturezaOperacaoAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }

            public string OrigemDestino { get; set; }

            public string TipoMovimentacao { get; set; }
        }

        #endregion
    }
}
