using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.OrigemClienteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.OrigemClienteObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using System.Linq.Expressions;
using System;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Cadastros.OrigemClienteServ
{
    public class RepositorioOrigemCliente : RepositorioBase<OrigemCliente>, IRepositorioOrigemCliente
    {
        public RepositorioOrigemCliente(ISession sessao)
            : base(sessao)
        {
        }

        public List<OrigemCliente> ConsulteLista(string descricao, string status)
        {
            Expression<Func<OrigemCliente, bool>> expressaoParaConsulta = origemCliente => origemCliente.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrWhiteSpace(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(origemCliente => origemCliente.Status == status);
            }

            return _session.QueryOver<OrigemCliente>().Where(expressaoParaConsulta).List<OrigemCliente>().ToList();
        }


        public List<OrigemCliente> ConsulteListaAtiva()
        {
            return _session.QueryOver<OrigemCliente>().Where(OrigemCliente => OrigemCliente.Status == "A").List<OrigemCliente>().ToList();
        }
    }
}
