using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.PlanoContasServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Financeiro.PlanosDeContas
{
    public partial class FormPlanosContasPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<PlanoDeContas> _listaDePlanosDeContas;
        private PlanoDeContas _PlanoDeContasSelecionada;

        #endregion

        #region " CONSTRUTOR "

        public FormPlanosContasPesquisa()
        {
            InitializeComponent();

            PreenchaOStatus();

            this.ActiveControl = txtNumeroPlanoDeContas;
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

        private void cboTipoPlanoDeContas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void txtNumeroPlanoContasContador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public PlanoDeContas ExibaPesquisaDePlanoDeContas()
        {
            this.ShowDialog();

            return _PlanoDeContasSelecionada;
        }

        public PlanoDeContas ExibaPesquisaDePlanoDeContasAtivos()
        {
            this.cboStatus.EditValue = "A";
            this.cboStatus.Enabled = false;

            this.ShowDialog();

            return _PlanoDeContasSelecionada;
        }

        private void Pesquise()
        {
            string descricao = txtDescricao.Text;
            string numeroPlanoContas = txtNumeroPlanoDeContas.Text;
            string status = cboStatus.EditValue.ToStringEmpty();
            string numeroPlanoContasContador = txtNumeroPlanoContasContador.Text;

            ServicoPlanoDeContas servicoPlanoDeContas = new ServicoPlanoDeContas();

            _listaDePlanosDeContas = servicoPlanoDeContas.ConsulteLista(numeroPlanoContas, 
                                                                                                      descricao, 
                                                                                                      status, 
                                                                                                      null, 
                                                                                                      null,
                                                                                                      numeroPlanoContasContador);

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<PlanoDeContasAuxiliar> listaDePlanosDeContasAuxiliares = new List<PlanoDeContasAuxiliar>();

            foreach (var planoDeContas in _listaDePlanosDeContas)
            {
                PlanoDeContasAuxiliar planoDeContasAuxiliar = new PlanoDeContasAuxiliar();

                planoDeContasAuxiliar.Descricao = planoDeContas.Descricao;
                planoDeContasAuxiliar.Id = planoDeContas.Id;
                planoDeContasAuxiliar.Natureza = planoDeContas.NaturezaPlanoContas != null ? planoDeContas.NaturezaPlanoContas.GetValueOrDefault().Descricao() : string.Empty;
                planoDeContasAuxiliar.NumeroPlanoDeContas = planoDeContas.NumeroPlanoDeContas;
                planoDeContasAuxiliar.Tipo = planoDeContas.TipoPlanoContas != null ? planoDeContas.TipoPlanoContas.GetValueOrDefault().Descricao() : string.Empty;
                planoDeContasAuxiliar.Status = planoDeContas.Status == "A" ? "ATIVO" : "INATIVO";
                planoDeContasAuxiliar.NumeroPlanoDeContasContador = planoDeContas.NumeroPlanoContasContador;

                listaDePlanosDeContasAuxiliares.Add(planoDeContasAuxiliar);
            }

            gcPlanoDeContas.DataSource = listaDePlanosDeContasAuxiliares;
            gcPlanoDeContas.RefreshDataSource();
        }

        private void Selecione()
        {
            _PlanoDeContasSelecionada = null;

            if (_listaDePlanosDeContas != null && _listaDePlanosDeContas.Count > 0)
            {
                ServicoPlanoDeContas servicoPlanoDeContas = new ServicoPlanoDeContas();

                _PlanoDeContasSelecionada = servicoPlanoDeContas.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
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

    class PlanoDeContasAuxiliar
    {
        public int Id { get; set; }

        public string NumeroPlanoDeContas { get; set; }

        public string Descricao { get; set; }

        public string Status { get; set; }

        public string Natureza { get; set; }

        public string Tipo { get; set; }

        public string NumeroPlanoDeContasContador { get; set; }

        public int Grau { get; set; }

        public string Valor { get; set; }
    }

    #endregion
}
