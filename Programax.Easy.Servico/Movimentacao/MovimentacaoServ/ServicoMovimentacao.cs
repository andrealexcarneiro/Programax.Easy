using System.Collections.Generic;
using System.Transactions;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;

namespace Programax.Easy.Servico.Movimentacao.MovimentacaoServ
{
    [Funcionalidade(EnumFuncionalidade.SEMVERIFICACAODEPERMISSAO)]
    public class ServicoMovimentacao : ServicoAkilSmallBusiness<MovimentacaoBase, ValidacaoMovimentacaoBase<MovimentacaoBase>, ConversorMovimentacao>
    {
        protected IRepositorioMovimentacao _repositorioMovimentacao;

        public ServicoMovimentacao()
        {
            RetorneRepositorio();
        }

        public ServicoMovimentacao(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<MovimentacaoBase> RetorneRepositorio()
        {
            if (_repositorioMovimentacao == null)
            {
                _repositorioMovimentacao = FabricaDeRepositorios.Crie<IRepositorioMovimentacao>();
            }

            return _repositorioMovimentacao;
        }

        public override int Cadastre(MovimentacaoBase objetoDeNegocio)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                objetoDeNegocio.PessoaCadastro = Sessao.PessoaLogada;

                InsiraTipoMovimentacaoNoItem(objetoDeNegocio);

                var idMovimentacao = base.Cadastre(objetoDeNegocio);

                AtualizeEstoque(objetoDeNegocio);

                scope.Complete();

                return idMovimentacao;
            }
        }

        public override void Atualize(MovimentacaoBase objetoDeNegocio)
        {
            objetoDeNegocio.PessoaCadastro = Sessao.PessoaLogada;

            InsiraTipoMovimentacaoNoItem(objetoDeNegocio);

            base.Atualize(objetoDeNegocio);
        }

        protected static void InsiraTipoMovimentacaoNoItem(MovimentacaoBase movimentacaoBase)
        {
            if (movimentacaoBase.TipoMovimentacao != EnumTipoMovimentacao.AMBOS)
            {
                foreach (var item in movimentacaoBase.ListaDeItens)
                {
                    item.TipoMovimentacao = movimentacaoBase.TipoMovimentacao;
                }
            }
        }

        protected void AtualizeEstoque(MovimentacaoBase movimentacaoBase)
        {
            var repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            List<Produto> listaProdutos = new List<Produto>();

            foreach (var item in movimentacaoBase.ListaDeItens)
            {
                var produto = repositorioProduto.Consulte(item.Produto.Id);

                produto.FormacaoPreco = produto.FormacaoPreco ?? new FormacaoPrecoProduto();

                if (item.TipoMovimentacao == EnumTipoMovimentacao.ENTRADA)
                {
                    produto.FormacaoPreco.Estoque = produto.FormacaoPreco.Estoque + item.Quantidade;
                }
                else
                {
                    produto.FormacaoPreco.Estoque = produto.FormacaoPreco.Estoque - item.Quantidade;
                }

                listaProdutos.Add(produto);
            }

            repositorioProduto.AtualizeLista(listaProdutos);
        }

        public List<MovimentacaoBase> ConsulteLista()
        {
            return _repositorioMovimentacao.ConsulteLista();
        }

        public List<ItemMovimentacao> ConsulteListaItensSaidaPorPedido(int numeroPedido)
        {
            return _repositorioMovimentacao.ConsulteListaItensSaidaPorPedido(numeroPedido);
        }
        public List<ItemMovimentacao> ConsulteListaItensSaidaPorPedidoEItem(int numeroPedido, int Item)
        {
            return _repositorioMovimentacao.ConsulteListaItensSaidaPorPedidoEItem(numeroPedido, Item);
        }

        public List<BaixaItens> ConsulteListaItensBaixaPorPedido(int numeroPedido)
        {
            return _repositorioMovimentacao.ConsulteListaItensBaixaPorPedido(numeroPedido);
        }

        public List<VwMovimentacaoProduto> ConsulteVwMovimentacoesProdutos(int? produtoId, DateTime? dataInicial, DateTime? dataFinal, EnumTipoMovimentacao? tipoMovimentacao, EnumOrigemMovimentacao? origemMovimentacao)
        {
            return _repositorioMovimentacao.ConsulteVwMovimentacoesProdutos(produtoId,dataInicial, dataFinal, tipoMovimentacao, origemMovimentacao);
        }

        public List<VwMovimentacaoSaidaItens> ConsulteVwMovimentacoesSaidaItens(int? produtoId, DateTime? dataInicial, DateTime? dataFinal)
        {
            return _repositorioMovimentacao.ConsulteVwMovimentacoesSaidaItens(produtoId, dataInicial, dataFinal);
        }
    }
}
