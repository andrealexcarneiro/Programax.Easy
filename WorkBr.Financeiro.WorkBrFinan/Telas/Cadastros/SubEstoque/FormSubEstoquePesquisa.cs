using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Cadastros.SubEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.SubEstoqueServ;

namespace Programax.Easy.View.Telas.Cadastros.Marcas
{
    public partial class FormSubEstoquePesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<SubEstoque> _listaDeSubestoque;
        private SubEstoque _subSelecionada;

        #endregion

        #region " CONSTRUTOR "

        public FormSubEstoquePesquisa()
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

        public SubEstoque ExibaPesquisaDeMarca()
        {
            this.ShowDialog();

            return _subSelecionada;
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
            ServicoSubEstoque servicoSubEstoque = new ServicoSubEstoque();

            string status = cboStatus.EditValue != null ? cboStatus.EditValue.ToString() : string.Empty;

            _listaDeSubestoque = servicoSubEstoque.ConsulteLista(txtId.Text.ToIntNullabel(), txtDescricao.Text, status);

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<SubEstoqueAuxiliar> listaDeSubAuxiliares = new List<SubEstoqueAuxiliar>();

            foreach (var subestoque in _listaDeSubestoque)
            {
                SubEstoqueAuxiliar subEstoqueAuxiliar = new SubEstoqueAuxiliar();

                subEstoqueAuxiliar.Descricao = subestoque.Descricao;
                subEstoqueAuxiliar.Id = subestoque.Id;
                subEstoqueAuxiliar.Status = subestoque.Ativo == "A" ? "ATIVO" : "INATIVO";

                listaDeSubAuxiliares.Add(subEstoqueAuxiliar);
            }

            gcNcms.DataSource = listaDeSubAuxiliares;
            gcNcms.RefreshDataSource();
        }

        private void Selecione()
        {
            _subSelecionada = null;

            if (_listaDeSubestoque != null && _listaDeSubestoque.Count > 0)
            {
                ServicoSubEstoque servicoSubestoque = new ServicoSubEstoque();

                _subSelecionada = servicoSubestoque.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        #endregion

        #region " CLASSE AUXILIAR "

        private class SubEstoqueAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
