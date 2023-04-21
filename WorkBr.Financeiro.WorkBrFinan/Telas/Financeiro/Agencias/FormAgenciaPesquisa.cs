using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.AgenciaServ;
using Programax.Easy.Servico.Financeiro.BancoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Financeiro.Agencias
{
    public partial class FormAgenciaPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Agencia> _listaDeAgencias;
        private Agencia _agenciaSelecionada;

        #endregion

        #region " CONSTRUTOR "

        public FormAgenciaPesquisa()
        {
            InitializeComponent();

            PreenchaOStatus();
            PreenchaCboBanco();

            this.ActiveControl = txtNomeAgencia;
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

        public Agencia ExibaPesquisaDeAgencias(int idBanco)
        {
            cboBancos.EditValue = idBanco;

            this.ShowDialog();

            return _agenciaSelecionada;
        }

        private void Pesquise()
        {
            ServicoAgencia servicoAgencia = new ServicoAgencia();

            Banco banco = cboBancos.EditValue != null ? new Banco { Id = cboBancos.EditValue.ToInt() } : null;

            _listaDeAgencias = servicoAgencia.ConsulteLista(banco, txtNomeAgencia.Text, cboStatus.EditValue.ToString());

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<AgenciaAuxiliar> listaDeFormasPagamentos = new List<AgenciaAuxiliar>();

            foreach (var agencia in _listaDeAgencias)
            {
                AgenciaAuxiliar agenciaAuxiliar = new AgenciaAuxiliar();

                agenciaAuxiliar.Id = agencia.Id;
                agenciaAuxiliar.NomeAgencia = agencia.NomeAgencia;
                agenciaAuxiliar.Status = agencia.Status == "A" ? "ATIVO" : "INATIVO";
                agenciaAuxiliar.Banco = agencia.Banco.Descricao;
                agenciaAuxiliar.NumeroAgencia = agencia.NumeroAgencia;
                agenciaAuxiliar.DigitoAgencia = agencia.DigitoAgencia;

                listaDeFormasPagamentos.Add(agenciaAuxiliar);
            }

            gcAgencias.DataSource = listaDeFormasPagamentos;
            gcAgencias.RefreshDataSource();
        }

        private void Selecione()
        {
            _agenciaSelecionada = null;

            if (_listaDeAgencias != null && _listaDeAgencias.Count > 0)
            {
                ServicoAgencia servicoAgencia = new ServicoAgencia();

                _agenciaSelecionada = servicoAgencia.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
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

        private void PreenchaCboBanco()
        {
            ServicoBanco servicoBanco = new ServicoBanco();

            var listaBancos = servicoBanco.ConsulteLista();

            listaBancos.Insert(0, null);

            cboBancos.Properties.DataSource = listaBancos;
            cboBancos.Properties.DisplayMember = "Descricao";
            cboBancos.Properties.ValueMember = "Id";
        }

        #endregion

        #region " CLASSE AUXILIAR "

        private class AgenciaAuxiliar
        {
            public int Id { get; set; }

            public string NomeAgencia { get; set; }

            public string Status { get; set; }

            public string Banco { get; set; }

            public string NumeroAgencia { get; set; }

            public string DigitoAgencia { get; set; }
        }

        #endregion
    }
}
