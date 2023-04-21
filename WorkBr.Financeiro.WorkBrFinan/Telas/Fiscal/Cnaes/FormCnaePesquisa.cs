using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Fiscal.CnaeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Servico.Fiscal.CnaeServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.ClassesAuxiliares;

namespace Programax.Easy.View.Telas.Fiscal.Cnaes
{
    public partial class FormCnaePesquisa : FormularioPadrao
    {
        #region " VARIÁVIES PRIVADAS "

        private List<Cnae> _listaDeCnaes;
        private Cnae _cnaeSelecionado;

        #endregion

        #region " CONSTRUTOR "

        public FormCnaePesquisa()
        {
            InitializeComponent();

            PreenchaOStatus();

            this.ActiveControl = txtCodigoCnae;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbPesquisaCadastro_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void FormCnaePesquisa_Load(object sender, EventArgs e)
        {
            PreenchaCboAtividade();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            SelecioneCnae();
        }

        private void gcCnaes_DoubleClick(object sender, EventArgs e)
        {
            SelecioneCnae();
        }

        private void gcCnaes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelecioneCnae();
            }
        }

        private void CampoEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public Cnae PesquiseCnae()
        {
            this.ShowDialog();

            return _cnaeSelecionado;
        }

        private void PreenchaCboAtividade()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumAtividadeCnae>();
            lista.Insert(0, null);

            cboAtividade.Properties.DataSource = lista;
            cboAtividade.Properties.ValueMember = "Valor";
            cboAtividade.Properties.DisplayMember = "Descricao";

            cboAtividade.EditValue = null;
        }

        private void Pesquise()
        {
            ServicoCnae servicoCnae = new ServicoCnae();

            _listaDeCnaes = servicoCnae.ConsulteLista(txtCodigoCnae.Text, txtDescricao.Text, (EnumAtividadeCnae?)cboAtividade.EditValue, cboStatus.EditValue.ToStringEmpty());

            PreenchaGrid();
        }

        private void SelecioneCnae()
        {
            _cnaeSelecionado = null;

            if (_listaDeCnaes != null && _listaDeCnaes.Count > 0)
            {
                ServicoCnae servicoCnae = new ServicoCnae();

                _cnaeSelecionado = servicoCnae.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void PreenchaGrid()
        {
            List<CnaeAuxiliar> listaDeCnaesAuxiliares = new List<CnaeAuxiliar>();

            foreach (var cnae in _listaDeCnaes)
            {
                CnaeAuxiliar cnaeAuxiliar = new CnaeAuxiliar();
                cnaeAuxiliar.Atividade = cnae.Atividade.Value.Descricao();
                cnaeAuxiliar.Codigo = cnae.Codigo;
                cnaeAuxiliar.Descricao = cnae.Descricao;
                cnaeAuxiliar.Id = cnae.Id;
                cnaeAuxiliar.Status = cnae.Status == "A" ? "ATIVO" : "INATIVO";

                listaDeCnaesAuxiliares.Add(cnaeAuxiliar);
            }

            gcCnaes.DataSource = listaDeCnaesAuxiliares;
            gcCnaes.RefreshDataSource();
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

        class CnaeAuxiliar
        {
            public int Id { get; set; }

            public string Codigo { get; set; }

            public string Descricao { get; set; }

            public string Atividade { get; set; }

            public string Status { get; set; }
        }
    }
}
