using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.Repositorio;
using Programax.Easy.Negocio.Estoque.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Estoque.EntradaMercadoriaServ
{
    public class RepositorioEntradaMercadoria : RepositorioBase<EntradaMercadoria>, IRepositorioEntradaMercadoria
    {
        private Pessoa _fornecedor = null;

        public RepositorioEntradaMercadoria(ISession sessao)
            : base(sessao)
        {

        }

        public EntradaMercadoria Consulte(string numeroNota, string serie, Pessoa fornecedor)
        {
            return _session.QueryOver<EntradaMercadoria>().Where(entrada => entrada.NumeroNota == numeroNota &&
                                                                                                                entrada.Serie == serie &&
                                                                                                                entrada.Fornecedor.Id == fornecedor.Id &&
                                                                                                                entrada.StatusEntrada != EnumStatusEntrada.CANCELADA).Take(1).SingleOrDefault();
        }

        public EntradaMercadoria ConsulteNotaEntrada(string numeroNota)
        {
            return _session.QueryOver<EntradaMercadoria>().Where(entrada => entrada.NumeroNota == numeroNota).Take(1).SingleOrDefault();
        }

        public List<EntradaMercadoria> ConsulteLista(DateTime? dataInicialEmissao,
                                                                           DateTime? dataFinalEmissao,
                                                                           DateTime? dataInicialEntrada,
                                                                           DateTime? dataFinalEntrada,
                                                                           string numeroNota,
                                                                           string razaoSocialFornecedor,
                                                                           EnumStatusEntrada? status, 
                                                                           int tipo)
        {
            Expression<Func<EntradaMercadoria, bool>> expressaoParaConsulta = entrada => entrada.Id > 0;

            if (dataInicialEmissao != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(entrada => entrada.DataEmissao >= dataInicialEmissao.GetValueOrDefault());
            }

            if (dataFinalEmissao != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(entrada => entrada.DataEmissao <= dataFinalEmissao.GetValueOrDefault());
            }

            if (dataInicialEntrada != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(entrada => entrada.DataMovimentacao >= dataInicialEntrada.GetValueOrDefault());
            }

            if (dataFinalEntrada != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(entrada => entrada.DataMovimentacao <= dataFinalEntrada.GetValueOrDefault());
            }

            if (!string.IsNullOrEmpty(numeroNota))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(entrada => entrada.NumeroNota == numeroNota);
            }

            if (tipo != 2)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(entrada =>  entrada.Tipo == tipo);
            }

            if (!string.IsNullOrEmpty(razaoSocialFornecedor))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(entrada => _fornecedor.DadosGerais.Razao.IsLike("%" + razaoSocialFornecedor + "%"));
            }

            if (status != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(entrada => entrada.StatusEntrada == status.Value);
            }

            return _session.QueryOver<EntradaMercadoria>()
                .Left.JoinAlias(entrada => entrada.Fornecedor, () => _fornecedor)
                .TransformUsing(Transformers.DistinctRootEntity)
                .Where(expressaoParaConsulta)
                .List<EntradaMercadoria>().ToList();
        }


        public ItemEntrada ConsulteUltimaEntradaProduto(Produto produto)
        {
            return _session.QueryOver<ItemEntrada>().Where(item => item.Produto.Id == produto.Id)
                                        .Where(item => item.Produto.Id == produto.Id)
                                        .JoinQueryOver<EntradaMercadoria>(x => (EntradaMercadoria)x.EntradaMercadoria)
                                                .OrderBy(x => x.DataCadastro).Desc
                                                .TransformUsing(Transformers.DistinctRootEntity)
                                                .Take(1).SingleOrDefault();
        }

        public List<EntradaMercadoria> ConsulteListaNumero(string numeroNota, int tipo)
        {
            Expression<Func<EntradaMercadoria, bool>> expressaoParaConsulta = entrada => entrada.Id > 0 ;

            if (!string.IsNullOrEmpty(numeroNota))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(entrada => entrada.NumeroNota == numeroNota && entrada.Tipo == tipo);
            }

            return _session.QueryOver<EntradaMercadoria>()
                .Left.JoinAlias(entrada => entrada.Fornecedor, () => _fornecedor)
                .TransformUsing(Transformers.DistinctRootEntity)
                .Where(expressaoParaConsulta)
                .List<EntradaMercadoria>().ToList();
        }

       
    }
}
