using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Cadastros.TabelaPrecoServ;
using Programax.Easy.Servico.Cadastros.ComissaoServ;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    public partial class FormAlterarTabelaPrecoPdv : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private DialogResult _resultadoOperacao;

        #endregion

        #region " PROPRIEDADES "

        public TabelaPreco TabelaPrecoSelecionada { get; set; }

        #endregion

        #region " CONSTRUTOR "

        public FormAlterarTabelaPrecoPdv()
        {
            InitializeComponent();

            PreenchaCboTabelaPreco();

            this.ActiveControl = cboTabelaPrecos;
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public DialogResult EditeTabelaPreco(TabelaPreco tabelaPreco)
        {
            TabelaPrecoSelecionada = tabelaPreco;

            cboTabelaPrecos.EditValue = tabelaPreco != null ? (int?)tabelaPreco.Id : null;

            this.AbrirTelaModal(true);

            return _resultadoOperacao;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            SelecioneTabelaPreco();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            _resultadoOperacao = DialogResult.Cancel;

            this.Close();
        }

        private void FormAlterarTabelaPrecoPdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F3)
            {
                SelecioneTabelaPreco();
            }
        }

        private void cboTabelaPrecos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelecioneTabelaPreco();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboTabelaPreco()
        {
            List<TabelaPreco> listaTabelaPreco = new List<TabelaPreco>();

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

            cboTabelaPrecos.Properties.DisplayMember = "NomeTabela";
            cboTabelaPrecos.Properties.ValueMember = "Id";
            cboTabelaPrecos.Properties.DataSource = listaTabelaPreco;
        }

        private void SelecioneTabelaPreco()
        {
            ServicoTabelaPreco servicoTabelaPreco = new ServicoTabelaPreco();
            TabelaPrecoSelecionada = servicoTabelaPreco.Consulte(cboTabelaPrecos.EditValue.ToInt());

            _resultadoOperacao = DialogResult.OK;

            this.Close();
        }

        #endregion
    }
}
