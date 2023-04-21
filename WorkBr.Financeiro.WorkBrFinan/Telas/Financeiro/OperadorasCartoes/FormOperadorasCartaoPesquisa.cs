using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.OperadorasCartaoServ;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;

namespace Programax.Easy.View.Telas.Financeiro.OperadorasCartoes
{
    public partial class FormOperadorasCartaoPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<OperadorasCartao> _listaDeOperadorasCartao;
        private OperadorasCartao _OperadoraSelecionada;

        #endregion

        #region " CONSTRUTOR "

        public FormOperadorasCartaoPesquisa()
        {
            InitializeComponent();

            PreenchaOStatus();

            this.ActiveControl = txtCodigoOperadora;
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

        private void btnPesquisaNcm_Click(object sender, EventArgs e)
        {
            Pesquise();
        }
        
        #endregion

        #region " MÉTODOS AUXILIARES "

        public OperadorasCartao ExibaPesquisaDeOperadoras()
        {
            this.ShowDialog();

            return _OperadoraSelecionada;
        }

        public OperadorasCartao ExibaPesquisaDeOperadorasCartaoAtivos()
        {
            this.cboStatus.EditValue = "A";
            this.cboStatus.Enabled = false;

            this.ShowDialog();

            return _OperadoraSelecionada;
        }

        private void Pesquise()
        {
            string descricao = txtDescricao.Text;           
            string status = cboStatus.EditValue.ToStringEmpty();
           
            ServicoOperadorasCartao servicoOperadorasCartao = new ServicoOperadorasCartao();

            _listaDeOperadorasCartao = servicoOperadorasCartao.ConsulteLista(descricao, status);

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<OperadorasAuxiliar> listaDeOperadorasAuxiliares = new List<OperadorasAuxiliar>();

            foreach (var operadoras in _listaDeOperadorasCartao)
            {
                OperadorasAuxiliar operadorasAuxiliar = new OperadorasAuxiliar();

                operadorasAuxiliar.Descricao = operadoras.Descricao;
                operadorasAuxiliar.Id = operadoras.Id;                               
                operadorasAuxiliar.Status = operadoras.Status == "A" ? "ATIVO" : "INATIVO";
                operadorasAuxiliar.Banco = new ServicoBancoParaMovimento().Consulte(operadoras.BancoParaMovimento.Id).NomeBanco;
                
                listaDeOperadorasAuxiliares.Add(operadorasAuxiliar);
            }

            gcOperadoras.DataSource = listaDeOperadorasAuxiliares;
            gcOperadoras.RefreshDataSource();
        }

        private void Selecione()
        {
            _OperadoraSelecionada = null;

            if (_listaDeOperadorasCartao != null && _listaDeOperadorasCartao.Count > 0)
            {
                ServicoOperadorasCartao servicoOperadorasCartao = new ServicoOperadorasCartao();

                _OperadoraSelecionada = servicoOperadorasCartao.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
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

        #endregion 
    }

    #region " CLASSE AUXILIAR "

    class OperadorasAuxiliar
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Status { get; set; }
               
        public string Banco { get; set; }

    }

    #endregion
}
