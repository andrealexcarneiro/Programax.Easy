using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Financeiro.ContasPagarReceber
{
    public partial class FormContasReceberPesquisa : FormContasPagarReceberPesquisa
    {
        public FormContasReceberPesquisa(bool EhMovimentacao=false)
        {   
            InitializeComponent();

            gcContasPagarReceber.Views[0].ViewCaption = "Contas Receber";

            var permissaoManutencao = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.MANUTENCAOCONTASRECEBER);
            var permissaoProrrogacao = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.PRORROGACAOCONTASRECEBER);
            var permissaoContaReceber = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.CONTASRECEBER);

            _cadastroLiberado = permissaoContaReceber.Alterar;
            _manutencaoLiberada = permissaoManutencao.Alterar;
            _prorrogacaoLiberada = permissaoProrrogacao.Alterar;

            if(!EhMovimentacao)
                Pesquise();            
        }

        protected override ServicoContasPagarReceber RetorneServicoContasPagarOuReceber()
        {
            return new ServicoContasReceber();
        }

        protected override FormCadastroContasPagarReceber RetorneFormCadastroContasPagarReceber()
        {
            return new FormCadastroContasReceber();
        }

        protected override FormManutencaoContaPagarReceber RetorneFormManutencaoContaPagarReceber()
        {
            return new FormManutencaoContaReceber();
        }

        protected override FormProrrogacaoContaPagarReceber RetorneFormProrrogacaoContaPagarReceber()
        {
            return new FormProrrogacaoContaReceber();
        }
    }
}
