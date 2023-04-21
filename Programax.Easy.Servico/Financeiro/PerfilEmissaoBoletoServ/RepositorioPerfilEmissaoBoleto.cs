using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using System.Linq.Expressions;
using System;
using Programax.Infraestrutura.Negocio.Utils;
using NHibernate.Transform;

namespace Programax.Easy.Servico.Financeiro.PerfilEmissaoBoletoSev
{
    public class RepositorioPerfilEmissaoBoleto : RepositorioBase<PerfilEmissaoBoleto>, IRepositorioPerfilEmissaoBoleto
    {
        public RepositorioPerfilEmissaoBoleto(ISession sessao)
            : base(sessao)
        {
        }

        public override List<PerfilEmissaoBoleto> ConsulteLista()
        {
            return _session.QueryOver<PerfilEmissaoBoleto>().OrderBy(perfil => perfil.Descricao).Asc.List().ToList();            
        }

        public List<PerfilEmissaoBoleto> ConsulteLista(string descricao)
        {
            Expression<Func<PerfilEmissaoBoleto, bool>> expressaoParaConsulta = perfil => perfil.Descricao.IsLike("%" + descricao + "%");
            
            return _session.QueryOver<PerfilEmissaoBoleto>().Where(expressaoParaConsulta).List().ToList();
        }

        public override PerfilEmissaoBoleto Consulte(int Id)
        {
            Expression<Func<PerfilEmissaoBoleto, bool>> expressaoParaConsulta = perfil => perfil.Id == Id;

            return _session.QueryOver<PerfilEmissaoBoleto>().Where(expressaoParaConsulta).Take(1).SingleOrDefault();
        }
    }
}
