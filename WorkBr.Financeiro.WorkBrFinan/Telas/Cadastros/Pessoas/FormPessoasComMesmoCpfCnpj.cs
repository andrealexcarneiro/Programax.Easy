using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Cadastros.Pessoas
{
    public partial class FormPessoasComMesmoCpfCnpj : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Pessoa> _listaPessoas;
        private Pessoa _pessoaSelecionada;

        #endregion

        #region " CONSTRUTOR "

        public FormPessoasComMesmoCpfCnpj()
        {
            InitializeComponent();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelecionarParceiro_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcPessoas_DoubleClick(object sender, EventArgs e)
        {
            if (HouveUmClickOuDuploClickNaLinha(gridView5))
            {
                Selecione();
            }
        }

        private void gcPessoas_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public bool ExistePessoasComMesmoCpfOuCnpj(string cpfCnpj, int idPessoaDesconsiderar)
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();

            return servicoPessoa.ExistePessoasComMesmoCpfOuCnpj(cpfCnpj, idPessoaDesconsiderar);
        }

        public Pessoa ListePessoasComMesmoCpfCnpj(string cpfCnpj)
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();

            _listaPessoas = servicoPessoa.ConsulteListaPessoaPeloCpfCnpj(cpfCnpj);

            PreencherGrid();

            this.AbrirTelaModal();

            return _pessoaSelecionada;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreencherGrid()
        {
            List<PessoaParaGrid> listaDePessoasParaGrid = new List<PessoaParaGrid>();

            foreach (var pessoa in _listaPessoas)
            {
                PessoaParaGrid pessoaParaGrid = new PessoaParaGrid();

                pessoaParaGrid.Id = pessoa.Id;
                pessoaParaGrid.CpfCnpj = pessoa.DadosGerais.CpfCnpj;
                pessoaParaGrid.NomeFantasia = pessoa.DadosGerais.NomeFantasia;
                pessoaParaGrid.RazaoSocial = pessoa.DadosGerais.Razao;

                pessoaParaGrid.UF = pessoa.ListaDeEnderecos != null && pessoa.ListaDeEnderecos.Count > 0 ?
                                                                             pessoa.ListaDeEnderecos[0].Cidade.Estado.UF :
                                                                             string.Empty;

                pessoaParaGrid.Cidade = pessoa.ListaDeEnderecos != null && pessoa.ListaDeEnderecos.Count > 0 ?
                                                                             pessoa.ListaDeEnderecos[0].Cidade.Descricao :
                                                                             string.Empty;

                pessoaParaGrid.Status = pessoa.DadosGerais.Status == "A" ? "ATIVO" : "INATIVO";

                listaDePessoasParaGrid.Add(pessoaParaGrid);
            }

            gcPessoas.DataSource = listaDePessoasParaGrid;
            gcPessoas.RefreshDataSource();
        }

        private void Selecione()
        {
            _pessoaSelecionada = null;

            if (_listaPessoas != null && _listaPessoas.Count > 0)
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                _pessoaSelecionada = servicoPessoa.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
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
