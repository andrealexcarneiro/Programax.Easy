using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.PautaIcmsObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PautaIcmsObj.Repositorio;
using NHibernate;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using System.Linq.Expressions;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Cadastros.PautaIcmsServ
{
    public class RepositorioPautaIcms : RepositorioBase<PautaIcms>, IRepositorioPautaIcms
    {
        public RepositorioPautaIcms(ISession sessao)
            : base(sessao)
        {

        }

        public PautaIcms Consulte(Produto produto, Estado estado, Cidade cidade)
        {
            if (produto == null || estado == null)
            {
                return null;
            }

            produto = produto ?? new Produto();

            Expression<Func<PautaIcms, bool>> expressaoParaConsulta = pessoa => pessoa.Produto.Id == produto.Id;

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(pauta => pauta.Estado.UF == estado.UF);

            if (cidade != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pauta => pauta.Cidade.Id == cidade.Id);
            }
            else
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pauta => pauta.Cidade == null);
            }

            return _session.QueryOver<PautaIcms>().Where(expressaoParaConsulta).Take(1).SingleOrDefault();
        }
    }
}
