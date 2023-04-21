using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.Repositorio;
using NHibernate;
using System.Linq.Expressions;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;

namespace Programax.Easy.Servico.Movimentacao.MovimentacaoServ
{
    public class RepositorioMovimentacao : RepositorioBase<MovimentacaoBase>, IRepositorioMovimentacao
    {
        public RepositorioMovimentacao(ISession sessao)
            : base(sessao)
        {

        }

        public List<VwMovimentacaoProduto> ConsulteVwMovimentacoesProdutos(int? produtoId, DateTime? dataInicial, DateTime? dataFinal, EnumTipoMovimentacao? tipoMovimentacao, EnumOrigemMovimentacao? origemMovimentacao)
        {
            Expression<Func<VwMovimentacaoProduto, bool>> expressaoParaConsulta = vwMovimentacao => vwMovimentacao.Id > 0;

            if (produtoId != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwMovimentacao => vwMovimentacao.ProdutoId == produtoId);
            }
            if (dataInicial != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwMovimentacao => vwMovimentacao.DataMovimentacao >= dataInicial);
            }
            if (dataFinal != null)
            {
                dataFinal = dataFinal.Value.AddHours(23);
                dataFinal = dataFinal.Value.AddMinutes(59);
                dataFinal = dataFinal.Value.AddSeconds(59);

                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwMovimentacao => vwMovimentacao.DataMovimentacao <= dataFinal);
            }

            if (tipoMovimentacao != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwMovimentacao => vwMovimentacao.TipoMovimentacao == tipoMovimentacao);
            }

            if (origemMovimentacao != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwMovimentacao => vwMovimentacao.OrigemMovimentacao == origemMovimentacao);
            }

            return _session.QueryOver<VwMovimentacaoProduto>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<VwMovimentacaoSaidaItens> ConsulteVwMovimentacoesSaidaItens(int? produtoId, DateTime? dataInicial, DateTime? dataFinal)
        {
            Expression<Func<VwMovimentacaoSaidaItens, bool>> expressaoParaConsulta = vwMovimentacao => vwMovimentacao.Id > 0;

            if (produtoId != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwMovimentacao => vwMovimentacao.Id == produtoId);
            }

            if (dataInicial != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwMovimentacao => vwMovimentacao.DataMovimentacao >= dataInicial);
            }
            if (dataFinal != null)
            {
                dataFinal = dataFinal.Value.AddHours(23);
                dataFinal = dataFinal.Value.AddMinutes(59);
                dataFinal = dataFinal.Value.AddSeconds(59);

                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwMovimentacao => vwMovimentacao.DataMovimentacao <= dataFinal);
            }
            
            return _session.QueryOver<VwMovimentacaoSaidaItens>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<ItemMovimentacao> ConsulteListaItensSaidaPorPedido(int numeroPedido)
        {
            Expression<Func<ItemMovimentacao, bool>> expressaoParaConsulta = itemMovimentacao => itemMovimentacao.Id > 0;
                        
            expressaoParaConsulta = expressaoParaConsulta.AndAlso(itemMovimentacao => itemMovimentacao.PedidoVenda_Id == numeroPedido);
            
            return _session.QueryOver<ItemMovimentacao>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<BaixaItens> ConsulteListaItensBaixaPorPedido(int numeroPedido)
        {
            Expression<Func<BaixaItens, bool>> expressaoParaConsulta = itemMovimentacao => itemMovimentacao.Id > 0;

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(itemMovimentacao => itemMovimentacao.PedidoVenda_Id == numeroPedido);

            return _session.QueryOver<BaixaItens>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<ItemMovimentacao> ConsulteListaItensSaidaPorPedidoEItem(int numeroPedido, int Item)
        {
            Expression<Func<ItemMovimentacao, bool>> expressaoParaConsulta = itemMovimentacao => itemMovimentacao.Id > 0;

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(itemMovimentacao => itemMovimentacao.PedidoVenda_Id == numeroPedido &&
                                                                                                                itemMovimentacao.Produto.Id == Item);

            //return _session.QueryOver<EntradaMercadoria>().Where(entrada => entrada.NumeroNota == numeroNota &&
            //                                                                                                 entrada.Serie == serie &&
            //                                                                                                 entrada.Fornecedor.Id == fornecedor.Id &&
            //                                                                                                 entrada.StatusEntrada != EnumStatusEntrada.CANCELADA).Take(1).SingleOrDefault();

            return _session.QueryOver<ItemMovimentacao>().Where(expressaoParaConsulta).List().ToList();
        }
    }
}
