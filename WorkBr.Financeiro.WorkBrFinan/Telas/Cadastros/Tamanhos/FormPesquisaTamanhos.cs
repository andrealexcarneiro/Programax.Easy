using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.TamanhoServ;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.ClassesAuxiliares;

namespace Programax.Easy.View.Telas.Cadastros.Tamanhos
{
    public partial class FormPesquisaTamanhos : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Tamanho _tamanhoSelecionada;
        private List<Tamanho> _listaDeTamanhos;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaTamanhos()
        {
            InitializeComponent();

            PreenchaOStatus();

            this.ActiveControl = txtDescricao;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
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

        private void gcTamanhos_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcTamanhos_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
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

        public Tamanho PesquiseUmaTamanho()
        {
            this.ShowDialog();

            return _tamanhoSelecionada;
        }

        private void Selecione()
        {
            _tamanhoSelecionada = null;

            if (_listaDeTamanhos != null && _listaDeTamanhos.Count > 0)
            {
                ServicoTamanho servicoTamanho = new ServicoTamanho();

                _tamanhoSelecionada = servicoTamanho.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void Pesquise()
        {
            ServicoTamanho servicoTamanho = new ServicoTamanho();

            _listaDeTamanhos = servicoTamanho.ConsulteLista(txtDescricao.Text,cboStatus.EditValue.ToStringEmpty());

            PreenchaGrid();
        }

        private void PreenchaGrid()
        {
            List<TamanhoGrid> listaTamanhosGrid = new List<TamanhoGrid>();

            foreach (var tamanho in _listaDeTamanhos)
            {
                TamanhoGrid tamanhoGrid = new TamanhoGrid();

                tamanhoGrid.Descricao = tamanho.Descricao;
                tamanhoGrid.Id = tamanho.Id;
                tamanhoGrid.Status = tamanho.Status == "A" ? "ATIVO" : "INATIVO";

                listaTamanhosGrid.Add(tamanhoGrid);
            }

            gcTamanhos.DataSource = listaTamanhosGrid;
            gcTamanhos.RefreshDataSource();
        }

        #endregion   
     
        #region " CLASSES AUXILIARES "

        private class TamanhoGrid
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
