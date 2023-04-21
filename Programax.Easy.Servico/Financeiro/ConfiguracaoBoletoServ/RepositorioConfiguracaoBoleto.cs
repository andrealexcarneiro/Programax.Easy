using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using System.Linq.Expressions;
using System;
using Programax.Infraestrutura.Negocio.Utils;
using NHibernate.Transform;

namespace Programax.Easy.Servico.Financeiro.PerfilEmissaoBoletoSev
{
    public class RepositorioConfiguracaoBoleto : RepositorioBase<ConfiguracaoBoleto>, IRepositorioConfiguracaoBoleto
    {
        public RepositorioConfiguracaoBoleto(ISession sessao)
            : base(sessao)
        {
        }
        
        public ConfiguracaoBoleto ConsultePeloPerfil(int IdPerfil)
        {
            Expression<Func<ConfiguracaoBoleto, bool>> expressaoParaConsulta = configuracao => configuracao.Perfil.Id == IdPerfil;

            return _session.QueryOver<ConfiguracaoBoleto>().Where(expressaoParaConsulta).Take(1).SingleOrDefault();
        }

        public override ConfiguracaoBoleto Consulte(int Id)
        {
            Expression<Func<ConfiguracaoBoleto, bool>> expressaoParaConsulta = configuracao => configuracao.Id == Id;

            return _session.QueryOver<ConfiguracaoBoleto>().Where(expressaoParaConsulta).Take(1).SingleOrDefault();
        }

        public List<ConfiguracaoBoleto> ConsulteLista(int idPerfil)
        {
            return _session.QueryOver<ConfiguracaoBoleto>().Where(configuracao =>configuracao.Perfil.Id == idPerfil).List().ToList();
        }
    }
}
