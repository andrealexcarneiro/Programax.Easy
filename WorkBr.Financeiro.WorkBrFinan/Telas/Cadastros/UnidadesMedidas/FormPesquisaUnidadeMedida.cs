using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.UnidadeMedidaServ;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.ClassesAuxiliares;

namespace Programax.Easy.View.Telas.Cadastros.UnidadesMedidas
{
    public partial class FormPesquisaUnidadeMedida : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private UnidadeMedida _unidadeMedidaSelecionada;
        private List<UnidadeMedida> _listaDeUnidadesMedidas;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaUnidadeMedida()
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

        private void txtAbreviacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void gcUnidadeMedidas_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcUnidadeMedidas_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
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

        public UnidadeMedida PesquiseUmaUnidadeMedida()
        {
            this.ShowDialog();

            return _unidadeMedidaSelecionada;
        }

        private void Selecione()
        {
            _unidadeMedidaSelecionada = null;

            if (_listaDeUnidadesMedidas != null && _listaDeUnidadesMedidas.Count > 0)
            {
                ServicoUnidadeMedida servicoUnidadeMedida = new ServicoUnidadeMedida();

                _unidadeMedidaSelecionada = servicoUnidadeMedida.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void Pesquise()
        {
            ServicoUnidadeMedida servicoUnidadeMedida = new ServicoUnidadeMedida();

            _listaDeUnidadesMedidas = servicoUnidadeMedida.ConsulteLista(txtDescricao.Text, txtAbreviacao.Text, cboStatus.EditValue.ToStringEmpty());

            PreenchaGrid();
        }

        private void PreenchaGrid()
        {
            List<UnidadeMedidaGrid> listaUnidadesMedidaGrid = new List<UnidadeMedidaGrid>();

            foreach (var unidadeMedida in _listaDeUnidadesMedidas)
            {
                UnidadeMedidaGrid unidadeMedidaGrid = new UnidadeMedidaGrid();

                unidadeMedidaGrid.Descricao = unidadeMedida.Descricao;
                unidadeMedidaGrid.Id = unidadeMedida.Id;
                unidadeMedidaGrid.Abreviacao = unidadeMedida.Abreviacao;
                unidadeMedidaGrid.Status = unidadeMedida.Status == "A" ? "ATIVO" : "INATIVO";

                listaUnidadesMedidaGrid.Add(unidadeMedidaGrid);
            }

            gcUnidadeMedidas.DataSource = listaUnidadesMedidaGrid;
            gcUnidadeMedidas.RefreshDataSource();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class UnidadeMedidaGrid
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Abreviacao { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
