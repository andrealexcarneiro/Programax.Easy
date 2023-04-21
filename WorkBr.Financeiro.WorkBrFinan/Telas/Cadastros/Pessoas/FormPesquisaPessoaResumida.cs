using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Cadastros.Pessoas
{
    public partial class FormPesquisaPessoaResumida : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Pessoa _pessoaSelecionada;
        private List<Pessoa> _listaDePessoas;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaPessoaResumida()
        {
            InitializeComponent();

            PreenchaOStatus();
            PreenchaCboTipoCadastroPessoa();

            this.ActiveControl = txtNomeFantasia;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void pbPesquisaCadastro_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcPessoas_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcPessoas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void ElementoPesquisar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public Pessoa PesquisePessoa()
        {
            this.ShowDialog();

            return _pessoaSelecionada;
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

        private void PreenchaCboTipoCadastroPessoa()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoCadastroParceiro>();

            cboTipoCadastroParceiro.Properties.DataSource = lista;
            cboTipoCadastroParceiro.Properties.ValueMember = "Valor";
            cboTipoCadastroParceiro.Properties.DisplayMember = "Descricao";

            cboTipoCadastroParceiro.EditValue = EnumTipoCadastroParceiro.TODOS;
        }

        private void Pesquise()
        {
            Cursor = Cursors.WaitCursor;
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            bool cliente = ((EnumTipoCadastroParceiro)cboTipoCadastroParceiro.EditValue) == EnumTipoCadastroParceiro.CLIENTES;
            bool funcionario = ((EnumTipoCadastroParceiro)cboTipoCadastroParceiro.EditValue) == EnumTipoCadastroParceiro.FUNCIONARIOS;
            bool fornecedor = ((EnumTipoCadastroParceiro)cboTipoCadastroParceiro.EditValue) == EnumTipoCadastroParceiro.FORNECEDORES;

            _listaDePessoas = servicoPessoa.ConsulteListaPessoaPeloNomeFantasia(txtNomeFantasia.Text, cboStatus.EditValue.ToStringEmpty(), cliente, fornecedor, funcionario);

            PreenchaGrid();
            Cursor = Cursors.Default;
        }

        private void PreenchaGrid()
        {
            List<PessoaParaGrid> listaDePessoasParaGrid = new List<PessoaParaGrid>();

            foreach (var pessoa in _listaDePessoas)
            {
                PessoaParaGrid pessoaParaGrid = new PessoaParaGrid();

                pessoaParaGrid.Id = pessoa.Id;
                pessoaParaGrid.CpfCnpj = pessoa.DadosGerais.CpfCnpj;
                pessoaParaGrid.NomeFantasia = pessoa.DadosGerais.NomeFantasia;
                pessoaParaGrid.RazaoSocial = pessoa.DadosGerais.Razao;

                pessoaParaGrid.UF = pessoa.ListaDeEnderecos != null && pessoa.ListaDeEnderecos.Count > 0 && pessoa.ListaDeEnderecos[0].Cidade !=null?
                                                                             pessoa.ListaDeEnderecos[0].Cidade.Estado.UF :
                                                                             string.Empty;

                pessoaParaGrid.Cidade = pessoa.ListaDeEnderecos != null && pessoa.ListaDeEnderecos.Count > 0 && pessoa.ListaDeEnderecos[0].Cidade != null?
                                                                             pessoa.ListaDeEnderecos[0].Cidade.Descricao :
                                                                             string.Empty;

                pessoaParaGrid.Status = pessoa.DadosGerais.Status == "A" ? "ATIVO" : "INATIVO";

                listaDePessoasParaGrid.Add(pessoaParaGrid);
            }

            gcPessoas.DataSource = listaDePessoasParaGrid;
            gcPessoas.RefreshDataSource();
        }

        public void Selecione()
        {
            _pessoaSelecionada = null;

            if (_listaDePessoas != null && _listaDePessoas.Count > 0)
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                _pessoaSelecionada = servicoPessoa.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        #endregion

        #region " ENUMERADORES "

        private enum EnumTipoCadastroParceiro
        {
            TODOS,
            
            CLIENTES,

            FUNCIONARIOS,

            FORNECEDORES
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class PessoaParaGrid
        {
            public int Id { get; set; }

            public string CpfCnpj { get; set; }

            public string RazaoSocial { get; set; }

            public string NomeFantasia { get; set; }

            public string UF { get; set; }

            public string Cidade { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
