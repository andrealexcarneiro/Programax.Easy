using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using System.Linq.Expressions;
using System;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Cadastros.RamoAtividadeServ
{
    public class RepositorioRamoAtividade : RepositorioBase<RamoAtividade>, IRepositorioRamoAtividade
    {
        public RepositorioRamoAtividade(ISession sessao)
            : base(sessao)
        {
        }

        public List<RamoAtividade> ConsulteLista(string descricao, string status)
        {
            Expression<Func<RamoAtividade, bool>> expressaoParaConsulta = ramoAtividade => ramoAtividade.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrWhiteSpace(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(ramoAtividade => ramoAtividade.Status == status);
            }

            return _session.QueryOver<RamoAtividade>().Where(expressaoParaConsulta).List<RamoAtividade>().ToList();
        }


        public List<RamoAtividade> ConsulteListaAtiva()
        {
            return _session.QueryOver<RamoAtividade>().Where(RamoAtividade => RamoAtividade.Status == "A").List<RamoAtividade>().ToList();
        }
    }
}
