using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.ClassesAuxiliares;

namespace Programax.Easy.View.Telas.Cadastros.Cidades
{
    public partial class FormCidadePesquisa : FormularioPadrao
    {
        private Cidade _cidadeSelecionada;
        private List<Cidade> _listaDeCidades;

        public FormCidadePesquisa()
        {
            InitializeComponent();

            _listaDeCidades = new List<Cidade>();
            PreenchaOsEstados();
            PreenchaOStatus();

            this.ActiveControl = txtNomeCidade;
        }

        public Cidade PesquiseCidade()
        {
            this.ShowDialog();

            return _cidadeSelecionada;
        }

        public Cidade PesquiseCidadeAtiva()
        {
            cboStatus.EditValue = "A";
            cboStatus.Enabled = false;

            this.ShowDialog();

            return _cidadeSelecionada;
        }

        private void txtNomeCidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtNomeCidade.Text))
                {
                    PesquiseCidades();
                }
                else
                {
                    cboEstado.Focus();
                }
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            SelecioneCidade();
        }

        private void SelecioneCidade()
        {
            _cidadeSelecionada = null;

            if (_listaDeCidades != null && _listaDeCidades.Count > 0)
            {
                ServicoCidade servicoCidade = new ServicoCidade();

                _cidadeSelecionada = servicoCidade.Consulte(colunaId.View.GetFocusedRowCellValue(colunaId).ToString().ToInt());
            }

            this.Close();
        }

        private void PreenchaOsEstados()
        {
            ServicoEstado servicoEstado = new ServicoEstado();

            var listaDeEstados = servicoEstado.ConsulteListaEstados();

            listaDeEstados.Insert(0, null);

            cboEstado.Properties.DataSource = listaDeEstados;
            cboEstado.Properties.DisplayMember = "Nome";
            cboEstado.Properties.ValueMember = "UF";
        }

        private void pbPesquisaCadastro_Click(object sender, EventArgs e)
        {
            PesquiseCidades();
        }

        private void PesquiseCidades()
        {
            string uf = string.Empty;

            if (cboEstado.EditValue != null)
            {
                uf = cboEstado.EditValue.ToString();
            }

            ServicoCidade servicoCidade = new ServicoCidade();
            _listaDeCidades = servicoCidade.ConsulteListaCidades(txtNomeCidade.Text, uf, cboStatus.EditValue.ToStringEmpty());

            PreenchaGrid();
        }

        private void PreenchaGrid()
        {
            List<CidadeGrid> listaDeCidadesParaGrid = new List<CidadeGrid>();

            foreach (Cidade cidade in _listaDeCidades)
            {
                CidadeGrid cidadeGrid = new CidadeGrid();

                cidadeGrid.Id = cidade.Id;
                cidadeGrid.CodigoIbge = cidade.CodigoIbge;
                cidadeGrid.NomeCidade = cidade.Descricao;
                cidadeGrid.Uf = cidade.Estado != null ? cidade.Estado.UF : string.Empty;
                cidadeGrid.Status = cidade.Status == "A" ? "ATIVO" : "INATIVO";

                listaDeCidadesParaGrid.Add(cidadeGrid);
            }

            gcCidades.DataSource = listaDeCidadesParaGrid;
            gcCidades.RefreshDataSource();
        }

        private void cboEstado_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PesquiseCidades();
            }
        }

        private void gcCidades_DoubleClick(object sender, EventArgs e)
        {
            SelecioneCidade();
        }

        private void gcCidades_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelecioneCidade();
            }
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

        private class CidadeGrid
        {
            public int Id { get; set; }

            public string CodigoIbge { get; set; }

            public string NomeCidade { get; set; }

            public string Uf { get; set; }

            public string Status { get; set; }
        }
    }
}
