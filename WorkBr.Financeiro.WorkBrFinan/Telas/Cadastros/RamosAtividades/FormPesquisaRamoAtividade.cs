using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.RamoAtividadeServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Cadastros.RamosAtividades
{
    public partial class FormPesquisaRamoAtividade : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private RamoAtividade _ramoAtividadeSelecionada;
        private List<RamoAtividade> _listaDeRamosAtividades;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaRamoAtividade()
        {
            InitializeComponent();

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

        private void gcRamosAtividades_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcRamosAtividades_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void FormPesquisaRamoAtividade_Load(object sender, EventArgs e)
        {
            PreenchaOStatus();
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

        public RamoAtividade PesquiseUmRamoAtividade()
        {
            this.ShowDialog();

            return _ramoAtividadeSelecionada;
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

        private void Selecione()
        {
            _ramoAtividadeSelecionada = null;

            if (_listaDeRamosAtividades != null && _listaDeRamosAtividades.Count > 0)
            {
                ServicoRamoAtividade servicoRamoAtividade = new ServicoRamoAtividade();

                _ramoAtividadeSelecionada = servicoRamoAtividade.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void Pesquise()
        {
            ServicoRamoAtividade servicoRamoAtividade = new ServicoRamoAtividade();

            _listaDeRamosAtividades = servicoRamoAtividade.ConsulteLista(txtDescricao.Text, cboStatus.EditValue.ToString());

            PreencherGrid();
        }

        private void PreencherGrid()
        {
            List<RamoAtividadeAuxiliar> listaDeRamosAtividadesParaGrid = new List<RamoAtividadeAuxiliar>();

            foreach (var ramoAtividade in _listaDeRamosAtividades)
            {
                RamoAtividadeAuxiliar ramoAtividadeAuxiliar = new RamoAtividadeAuxiliar();
                ramoAtividadeAuxiliar.Descricao = ramoAtividade.Descricao;
                ramoAtividadeAuxiliar.Id = ramoAtividade.Id;
                ramoAtividadeAuxiliar.Status = ramoAtividade.Status == "A" ? "ATIVO" : "INATIVO";

                listaDeRamosAtividadesParaGrid.Add(ramoAtividadeAuxiliar);
            }

            gcRamosAtividades.DataSource = listaDeRamosAtividadesParaGrid;
            gcRamosAtividades.RefreshDataSource();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class RamoAtividadeAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
