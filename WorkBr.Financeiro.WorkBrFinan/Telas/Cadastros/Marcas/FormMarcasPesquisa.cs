using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Cadastros.Marcas
{
    public partial class FormMarcasPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Marca> _listaDeMarcas;
        private Marca _marcaSelecionada;

        #endregion

        #region " CONSTRUTOR "

        public FormMarcasPesquisa()
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

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public Marca ExibaPesquisaDeMarca()
        {
            this.ShowDialog();

            return _marcaSelecionada;
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
            ServicoMarca servicoMarca = new ServicoMarca();

            string status = cboStatus.EditValue != null ? cboStatus.EditValue.ToString() : string.Empty;

            _listaDeMarcas = servicoMarca.ConsulteLista(txtId.Text.ToIntNullabel(), txtDescricao.Text, status);

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<MarcaAuxiliar> listaDeMarcasAuxiliares = new List<MarcaAuxiliar>();

            foreach (var marca in _listaDeMarcas)
            {
                MarcaAuxiliar marcaAuxiliar = new MarcaAuxiliar();

                marcaAuxiliar.Descricao = marca.Descricao;
                marcaAuxiliar.Id = marca.Id;
                marcaAuxiliar.Status = marca.Ativo == "A" ? "ATIVO" : "INATIVO";

                listaDeMarcasAuxiliares.Add(marcaAuxiliar);
            }

            gcNcms.DataSource = listaDeMarcasAuxiliares;
            gcNcms.RefreshDataSource();
        }

        private void Selecione()
        {
            _marcaSelecionada = null;

            if (_listaDeMarcas != null && _listaDeMarcas.Count > 0)
            {
                ServicoMarca servicoMarca = new ServicoMarca();

                _marcaSelecionada = servicoMarca.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        #endregion

        #region " CLASSE AUXILIAR "

        private class MarcaAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
