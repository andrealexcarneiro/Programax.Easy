using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.MotivoCorrecaoEstoqueServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Cadastros.MotivosCorrecoesEstoque
{
    public partial class FormPesquisaDeMotivoCorrecaoEstoque : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private MotivoCorrecaoEstoque _motivoCorrecaoEstoqueSelecionada;
        private List<MotivoCorrecaoEstoque> _listaDeMotivosCorrecoesEstoque;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaDeMotivoCorrecaoEstoque()
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

        private void gcMotivosMotivoCorrecaoEstoquerecoesEstoque_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcMotivosMotivoCorrecaoEstoquerecoesEstoque_EditorKeyDown(object sender, KeyEventArgs e)
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
        
        public MotivoCorrecaoEstoque PesquiseUmaMotivoCorrecaoEstoque()
        {
            this.ShowDialog();

            return _motivoCorrecaoEstoqueSelecionada;
        }

        private void Selecione()
        {
            _motivoCorrecaoEstoqueSelecionada = null;

            if (_listaDeMotivosCorrecoesEstoque != null && _listaDeMotivosCorrecoesEstoque.Count > 0)
            {
                ServicoMotivoCorrecaoEstoque servicoMotivoCorrecaoEstoque = new ServicoMotivoCorrecaoEstoque();

                _motivoCorrecaoEstoqueSelecionada = servicoMotivoCorrecaoEstoque.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void Pesquise()
        {
            ServicoMotivoCorrecaoEstoque servicoMotivoCorrecaoEstoque = new ServicoMotivoCorrecaoEstoque();

            _listaDeMotivosCorrecoesEstoque = servicoMotivoCorrecaoEstoque.ConsulteLista(txtDescricao.Text, cboStatus.EditValue.ToStringEmpty());

            PreenchaGrid();
        }

        private void PreenchaGrid()
        {
            List<MotivoCorrecaoEstoqueGrid> listaMotivosCorrecoesEstoqueGrid = new List<MotivoCorrecaoEstoqueGrid>();

            foreach (var motivoCorrecao in _listaDeMotivosCorrecoesEstoque)
            {
                MotivoCorrecaoEstoqueGrid motivoCorrecaoEstoqueGrid = new MotivoCorrecaoEstoqueGrid();

                motivoCorrecaoEstoqueGrid.Descricao = motivoCorrecao.Descricao;
                motivoCorrecaoEstoqueGrid.Id = motivoCorrecao.Id;
                motivoCorrecaoEstoqueGrid.Status = motivoCorrecao.Status == "A" ? "ATIVO" : "INATIVO";

                listaMotivosCorrecoesEstoqueGrid.Add(motivoCorrecaoEstoqueGrid);
            }

            gcMotivosCorrecoesEstoque.DataSource = listaMotivosCorrecoesEstoqueGrid;
            gcMotivosCorrecoesEstoque.RefreshDataSource();
        }

        #endregion     
   
        #region " CLASSES AUXILIARES "

        private class MotivoCorrecaoEstoqueGrid
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
