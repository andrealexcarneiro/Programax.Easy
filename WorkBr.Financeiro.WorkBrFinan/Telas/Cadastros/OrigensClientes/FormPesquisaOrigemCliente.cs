using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.OrigemClienteObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.OrigemClienteServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Cadastros.OrigensClientes
{
    public partial class FormPesquisaOrigemCliente : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private OrigemCliente _origemClienteSelecionada;
        private List<OrigemCliente> _listaDeOrigensClientes;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaOrigemCliente()
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

        private void gcOrigensClientes_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void gcOrigensClientes_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void FormPesquisaOrigemCliente_Load(object sender, EventArgs e)
        {
            PreenchaOStatus();
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

        public OrigemCliente PesquiseUmOrigemCliente()
        {
            this.ShowDialog();

            return _origemClienteSelecionada;
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
            _origemClienteSelecionada = null;

            if (_listaDeOrigensClientes != null && _listaDeOrigensClientes.Count > 0)
            {
                ServicoOrigemCliente servicoOrigemCliente = new ServicoOrigemCliente();

                _origemClienteSelecionada = servicoOrigemCliente.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void Pesquise()
        {
            ServicoOrigemCliente servicoOrigemCliente = new ServicoOrigemCliente();

            _listaDeOrigensClientes = servicoOrigemCliente.ConsulteLista(txtDescricao.Text, cboStatus.EditValue.ToString());

            PreencherGrid();
        }

        private void PreencherGrid()
        {
            List<OrigemClienteAuxiliar> listaDeOrigensClientesParaGrid = new List<OrigemClienteAuxiliar>();

            foreach (var origemCliente in _listaDeOrigensClientes)
            {
                OrigemClienteAuxiliar origemClienteAuxiliar = new OrigemClienteAuxiliar();
                origemClienteAuxiliar.Descricao = origemCliente.Descricao;
                origemClienteAuxiliar.Id = origemCliente.Id;
                origemClienteAuxiliar.Status = origemCliente.Status == "A" ? "ATIVO" : "INATIVO";

                listaDeOrigensClientesParaGrid.Add(origemClienteAuxiliar);
            }

            gcOrigensClientes.DataSource = listaDeOrigensClientesParaGrid;
            gcOrigensClientes.RefreshDataSource();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class OrigemClienteAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }
        }

        #endregion        
    }
}
