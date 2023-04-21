using System;
using System.Collections.Generic;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ComissaoServ;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using System.Windows.Forms;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Cadastros.TabelaPrecoServ;
using Programax.Easy.Servico.Financeiro.CrediarioServ;

namespace Programax.Easy.View.Telas.Vendas.VendaRapida
{
    public partial class FormTabelaPrecoVendaRapida : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private DialogResult _resultadoOperacao;
        private int _pessoaId;

        #endregion

        #region " PROPRIEDADES "

        public TabelaPreco TabelaPrecoSelecionada { get; set; }

        #endregion

        #region " CONSTRUTOR "

        public FormTabelaPrecoVendaRapida()
        {
            InitializeComponent();

            this.ActiveControl = cboTabelaPrecos;
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public DialogResult EditeTabelaPreco(TabelaPreco tabelaPreco, int pessoaId)
        {
            _pessoaId = pessoaId;

            PreenchaCboTabelaPreco();

            TabelaPrecoSelecionada = tabelaPreco;

            cboTabelaPrecos.EditValue = tabelaPreco != null ? (int?)tabelaPreco.Id : null;

            this.AbrirTelaModal();

            return _resultadoOperacao;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            TabelaPrecoSelecionada = cboTabelaPrecos.EditValue != null ? new TabelaPreco { Id = cboTabelaPrecos.EditValue.ToInt() } : null;

            _resultadoOperacao = DialogResult.OK;

            this.Close();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            _resultadoOperacao = DialogResult.Cancel;

            this.Close();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboTabelaPreco()
        {
            List<TabelaPreco> listaTabelaPreco = new List<TabelaPreco>();

            ServicoCrediario servicoAnaliseCredito = new ServicoCrediario();
            var analiseCredito = servicoAnaliseCredito.Consulte(_pessoaId);

            if (analiseCredito != null && analiseCredito.TabelaPreco != null)
            {
                listaTabelaPreco.Add(analiseCredito.TabelaPreco);
            }
            else
            {
                ServicoParametros servicoParametros = new ServicoParametros();
                var parametros = servicoParametros.ConsulteParametros();

                if (parametros.ParametrosVenda.ExibirTodasAsTabelasPrecoVendaRapida)
                {
                    ServicoTabelaPreco servioTabelaPreco = new ServicoTabelaPreco();
                    listaTabelaPreco = servioTabelaPreco.ConsulteListaTabelaPrecosAtivas();
                }
                else
                {

                    ServicoComissao servicoComissao = new ServicoComissao();
                    List<Comissao> listaComissoes = servicoComissao.ConsulteLista(Sessao.PessoaLogada);

                    foreach (var comissao in listaComissoes)
                    {
                        if (comissao.TabelaPreco.Status == "A" && !listaTabelaPreco.Exists(tabelaPreco => tabelaPreco.Id == comissao.TabelaPreco.Id))
                        {
                            listaTabelaPreco.Add(comissao.TabelaPreco);
                        }
                    }
                }
            }

            listaTabelaPreco.Insert(0, null);

            cboTabelaPrecos.Properties.DisplayMember = "NomeTabela";
            cboTabelaPrecos.Properties.ValueMember = "Id";
            cboTabelaPrecos.Properties.DataSource = listaTabelaPreco;

            if (string.IsNullOrEmpty(cboTabelaPrecos.Text))
            {
                cboTabelaPrecos.EditValue = null;
            }
        }

        #endregion
    }
}
