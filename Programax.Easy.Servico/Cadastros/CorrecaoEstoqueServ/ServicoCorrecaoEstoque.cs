using System;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;

namespace Programax.Easy.Servico.Cadastros.CorrecaoEstoqueServ
{
    [Funcionalidade(EnumFuncionalidade.CORRECAOESTOQUE)]
    public class ServicoCorrecaoEstoque : ServicoMovimentacao
    {
        public override int Cadastre(MovimentacaoBase objetoDeNegocio)
        {
            objetoDeNegocio.OrigemMovimentacao = EnumOrigemMovimentacao.CORRECAODEESTOQUE;

            return base.Cadastre(objetoDeNegocio);
        }
    }
}
