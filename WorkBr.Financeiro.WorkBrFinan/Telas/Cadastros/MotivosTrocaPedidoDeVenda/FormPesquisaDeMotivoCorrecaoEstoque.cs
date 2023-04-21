using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.MotivoTrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.MotivoTrocaPedidoDeVendaServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Cadastros.MotivosTrocaPedidoDeVenda
{
    public partial class FormPesquisaDeMotivoTrocaPedidoDeVenda: FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private MotivoTrocaPedidoDeVenda _motivoTrocaPedidoDeVendaSelecionada;
        private List<MotivoTrocaPedidoDeVenda> _listaDeMotivosTrocaPedidoDeVenda;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaDeMotivoTrocaPedidoDeVenda()
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

        private void gcMotivosMotivoTrocaPedidoDeVendarecoesEstoque_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcMotivosMotivoTrocaPedidoDeVendarecoesEstoque_EditorKeyDown(object sender, KeyEventArgs e)
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
        
        public MotivoTrocaPedidoDeVenda PesquiseUmaMotivoTrocaPedidoDeVenda()
        {
            this.ShowDialog();

            return _motivoTrocaPedidoDeVendaSelecionada;
        }

        private void Selecione()
        {
            _motivoTrocaPedidoDeVendaSelecionada = null;

            if (_listaDeMotivosTrocaPedidoDeVenda != null && _listaDeMotivosTrocaPedidoDeVenda.Count > 0)
            {
                ServicoMotivoTrocaPedidoDeVenda servicoMotivoTrocaPedidoDeVenda = new ServicoMotivoTrocaPedidoDeVenda();

                _motivoTrocaPedidoDeVendaSelecionada = servicoMotivoTrocaPedidoDeVenda.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void Pesquise()
        {
            ServicoMotivoTrocaPedidoDeVenda servicoMotivoTrocaPedidoDeVenda = new ServicoMotivoTrocaPedidoDeVenda();

            _listaDeMotivosTrocaPedidoDeVenda = servicoMotivoTrocaPedidoDeVenda.ConsulteLista(txtDescricao.Text, cboStatus.EditValue.ToStringEmpty());

            PreenchaGrid();
        }

        private void PreenchaGrid()
        {
            List<MotivoTrocaPedidoDeVendaGrid> listaMotivosCorrecoesEstoqueGrid = new List<MotivoTrocaPedidoDeVendaGrid>();

            foreach (var motivoCorrecao in _listaDeMotivosTrocaPedidoDeVenda)
            {
                MotivoTrocaPedidoDeVendaGrid motivoTrocaPedidoDeVendaGrid = new MotivoTrocaPedidoDeVendaGrid();

                motivoTrocaPedidoDeVendaGrid.Descricao = motivoCorrecao.Descricao;
                motivoTrocaPedidoDeVendaGrid.Id = motivoCorrecao.Id;
                motivoTrocaPedidoDeVendaGrid.Status = motivoCorrecao.Status == "A" ? "ATIVO" : "INATIVO";

                listaMotivosCorrecoesEstoqueGrid.Add(motivoTrocaPedidoDeVendaGrid);
            }

            gcMotivosTrocaPedidoDeVenda.DataSource = listaMotivosCorrecoesEstoqueGrid;
            gcMotivosTrocaPedidoDeVenda.RefreshDataSource();
        }

        #endregion     
   
        #region " CLASSES AUXILIARES "

        private class MotivoTrocaPedidoDeVendaGrid
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
