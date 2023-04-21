using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.TabelaPrecoServ;
using Programax.Easy.View.ClassesAuxiliares;

namespace Programax.Easy.View.Telas.Produtos.TabelaDePreco
{
    public partial class UcTabelaDePrecoPesquisa : UserControl
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<TabelaPreco> _listaTabelaPreco;
        private TabelaPreco _tabelaPrecoSelecionada;
        private Action<TabelaPreco> _metodoAposASelecaoDoRegistro;

        #endregion

        #region " CONSTRUTOR "

        public UcTabelaDePrecoPesquisa()
        {
            InitializeComponent();

            PreenchaOpcoesDeStatus();

            this.ActiveControl = txtChave;

            _listaTabelaPreco = new List<TabelaPreco>();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void pbPesquisaCadastro_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void gcTabelasPrecos_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcTabelasPrecos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void txtChave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void txtChave_KeyDown_1(object sender, KeyEventArgs e)
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

        private void txtDataValidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public void InformarMetodoDeRetornoDoRegistro(Action<TabelaPreco> metodoAposASelecaoDoRegistro)
        {
            _metodoAposASelecaoDoRegistro = metodoAposASelecaoDoRegistro;
        }

        public void Selecione()
        {
            _tabelaPrecoSelecionada = null;

            if (_listaTabelaPreco != null && _listaTabelaPreco.Count > 0)
            {
                ServicoTabelaPreco servicoTabelaPreco = new ServicoTabelaPreco();

                _tabelaPrecoSelecionada= servicoTabelaPreco.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            _metodoAposASelecaoDoRegistro(_tabelaPrecoSelecionada);
        }

        private void Pesquise()
        {
            var servicoTabelaPreco = new ServicoTabelaPreco();

            string descricao = txtChave.Text;

            DateTime? dataDeValidade = null;

            if (!string.IsNullOrEmpty(txtDataValidade.Text))
            {
                dataDeValidade = Convert.ToDateTime(txtDataValidade.Text);
            }

            string status = cboStatus.EditValue == null ? string.Empty : cboStatus.EditValue.ToString();

            _listaTabelaPreco = servicoTabelaPreco.ConsulteLista(descricao, status, dataDeValidade);

            gcTabelasPrecos.DataSource = _listaTabelaPreco;
            gcTabelasPrecos.RefreshDataSource();
        }

        private void PreenchaOpcoesDeStatus()
        {
            ObjetoParaComboBox objetoVazio = new ObjetoParaComboBox();
            ObjetoParaComboBox objetoAtivo = new ObjetoParaComboBox();
            ObjetoParaComboBox objetoInativo = new ObjetoParaComboBox();

            objetoVazio.Descricao = "Ativo ou Inativo";
            objetoVazio.Valor = string.Empty;

            objetoAtivo.Descricao = "Ativo";
            objetoAtivo.Valor = "A";

            objetoInativo.Descricao = "Inativo";
            objetoInativo.Valor = "I";

            List<ObjetoParaComboBox> listaDeObjetosParaComboBox = new List<ObjetoParaComboBox>();

            listaDeObjetosParaComboBox.Add(objetoVazio);
            listaDeObjetosParaComboBox.Add(objetoAtivo);
            listaDeObjetosParaComboBox.Add(objetoInativo);

            cboStatus.Properties.DataSource = listaDeObjetosParaComboBox;
            cboStatus.Properties.DisplayMember = "Descricao";
            cboStatus.Properties.ValueMember = "Valor";

            cboStatus.EditValue = objetoVazio.Valor;
        }


        #endregion
    }
}
