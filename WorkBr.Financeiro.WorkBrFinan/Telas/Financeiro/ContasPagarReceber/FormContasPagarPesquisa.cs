using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.View.Telas.Financeiro.ContasPagarReceber
{
    public partial class FormContasPagarPesquisa : FormContasPagarReceberPesquisa
    {
        public FormContasPagarPesquisa(bool EhMovimentacao=false)
        {
            InitializeComponent();

            gcContasPagarReceber.Views[0].ViewCaption = "Contas Pagar";

            var permissaoManutencao = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.MANUTENCAOCONTASPAGAR);
            var permissaoProrrogacao = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.PRORROGACAOCONTASPAGAR);
            var permissaoContaReceber = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.CONTASPAGAR);

            _cadastroLiberado = permissaoContaReceber.Alterar;
            _manutencaoLiberada = permissaoManutencao.Alterar;
            _prorrogacaoLiberada = permissaoProrrogacao.Alterar;

            if(!EhMovimentacao)
                Pesquise();
        }

        protected override FormCadastroContasPagarReceber RetorneFormCadastroContasPagarReceber()
        {
            return new FormCadastroContasPagar();
        }

        protected override ServicoContasPagarReceber RetorneServicoContasPagarOuReceber()
        {
            return new ServicoContasPagar();
        }

        protected override FormManutencaoContaPagarReceber RetorneFormManutencaoContaPagarReceber()
        {
            return new FormManutencaoContaPagar();
        }

        protected override FormProrrogacaoContaPagarReceber RetorneFormProrrogacaoContaPagarReceber()
        {
            return new FormProrrogacaoContaPagar();
        }
    }
}
