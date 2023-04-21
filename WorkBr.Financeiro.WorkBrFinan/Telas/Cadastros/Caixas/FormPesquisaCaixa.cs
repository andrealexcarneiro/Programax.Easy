using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.View.Telas.Cadastros.Caixas
{
    public partial class FormPesquisaCaixa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Caixa _caixaSelecionada;
        private List<Caixa> _listaDeCaixas;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaCaixa()
        {
            InitializeComponent();

            PreenchaOStatus();
            PreenchaCboFuncionarios();

            this.ActiveControl = txtNomeCaixa;
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

        private void gcCaixas_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcCaixas_EditorKeyDown(object sender, KeyEventArgs e)
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

        private void txtNomeCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void cboFuncionarios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public Caixa PesquiseUmCaixa()
        {
            this.ShowDialog();

            return _caixaSelecionada;
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
            _caixaSelecionada = null;

            if (_listaDeCaixas != null && _listaDeCaixas.Count > 0)
            {
                ServicoCaixa servicoCaixa = new ServicoCaixa();

                _caixaSelecionada = servicoCaixa.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void Pesquise()
        {
            Pessoa pessoa = cboFuncionarios.EditValue != null ? new Pessoa { Id = cboFuncionarios.EditValue.ToInt() } : null;

            ServicoCaixa servicoCaixa = new ServicoCaixa();

            _listaDeCaixas = servicoCaixa.ConsulteLista(txtNomeCaixa.Text, cboStatus.EditValue.ToStringEmpty(), pessoa);

            PreencherGrid();
        }

        private void PreencherGrid()
        {
            List<CaixaAuxiliar> listaDeCaixasParaGrid = new List<CaixaAuxiliar>();

            foreach (var caixa in _listaDeCaixas)
            {
                CaixaAuxiliar caixaAuxiliar = new CaixaAuxiliar();
                caixaAuxiliar.NomeCaixa = caixa.NomeCaixa;
                caixaAuxiliar.Id = caixa.Id;
                caixaAuxiliar.Status = caixa.Status == "A" ? "ATIVO" : "INATIVO";

                caixaAuxiliar.Funcionario = caixa.Funcionario.Id + " - " + caixa.Funcionario.DadosGerais.Razao;

                listaDeCaixasParaGrid.Add(caixaAuxiliar);
            }

            gcCaixas.DataSource = listaDeCaixasParaGrid;
            gcCaixas.RefreshDataSource();
        }

        private void PreenchaCboFuncionarios()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaFuncionariosAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboFuncionarios.Properties.DisplayMember = "Descricao";
            cboFuncionarios.Properties.ValueMember = "Valor";
            cboFuncionarios.Properties.DataSource = listaObjetoValor;
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class CaixaAuxiliar
        {
            public int Id { get; set; }

            public string NomeCaixa { get; set; }

            public string Status { get; set; }

            public string Funcionario { get; set; }
        }

        #endregion
    }
}
