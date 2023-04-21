using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using System.Linq.Expressions;
using System;
using Programax.Infraestrutura.Negocio.Utils;
using NHibernate.Transform;

namespace Programax.Easy.Servico.Financeiro.BancoServ
{
    public class RepositorioBanco : RepositorioBase<Banco>, IRepositorioBanco
    {
        public RepositorioBanco(ISession sesao)
            : base(sesao)
        {
        }

        public override List<Banco> ConsulteLista()
        {
            return _session.QueryOver<Banco>().OrderBy(banco => banco.Descricao).Asc.List().ToList();
            //return _registros.OrderBy(banco => banco.Descricao).ToList();
        }

        public List<Banco> ConsulteLista(string descricao, string status)
        {
            Expression<Func<Banco, bool>> expressaoParaConsulta = banco => banco.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(banco => banco.Status == status);
            }

            return _session.QueryOver<Banco>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<Banco> ConsulteListaAtiva()
        {
            return _session.QueryOver<Banco>().Where(banco => banco.Status == "A").OrderBy(banco => banco.Descricao).Asc.List().ToList();
        }

        public Banco ConsultePeloCodigoBanco(string codigoBanco)
        {
            return _session.QueryOver<Banco>().Where(banco => banco.Codigo == codigoBanco).Take(1).SingleOrDefault();
        }
    }
}
