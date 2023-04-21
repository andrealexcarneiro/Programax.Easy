using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CorServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Cadastros.Cores
{
    public partial class FormPesquisaCores : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Cor _corSelecionada;
        private List<Cor> _listaDeCores;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaCores()
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

        private void gcCores_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcCores_EditorKeyDown(object sender, KeyEventArgs e)
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

        public Cor PesquiseUmaCor()
        {
            this.ShowDialog();

            return _corSelecionada;
        }

        private void Selecione()
        {
            _corSelecionada = null;

            if (_listaDeCores != null && _listaDeCores.Count > 0)
            {
                ServicoCor servicoCor = new ServicoCor();

                _corSelecionada = servicoCor.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void Pesquise()
        {
            ServicoCor servicoCor = new ServicoCor();

            _listaDeCores = servicoCor.ConsulteLista(txtDescricao.Text, cboStatus.EditValue.ToStringEmpty());

            PreenchaGrid();
        }

        private void PreenchaGrid()
        {
            List<CorGrid> listaCoresGrid = new List<CorGrid>();

            foreach (var cor in _listaDeCores)
            {
                CorGrid corGrid = new CorGrid();

                corGrid.Descricao = cor.Descricao;
                corGrid.Id = cor.Id;
                corGrid.Status = cor.Status == "A" ? "ATIVO" : "INATIVO";

                listaCoresGrid.Add(corGrid);
            }

            gcCores.DataSource = listaCoresGrid;
            gcCores.RefreshDataSource();
        }

        #endregion        

        #region " CLASSES AUXILIARES "

        private class CorGrid
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
