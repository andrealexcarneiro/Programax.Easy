using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.Repositorio;
using System.Linq.Expressions;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Cadastros.FabricanteServ
{
    public class RepositorioFabricante : RepositorioBase<Fabricante>, IRepositorioFabricante
    {
        public RepositorioFabricante(ISession sessao)
            : base(sessao)
        {

        }

        public List<Fabricante> ConsulteLista(int? idFabricante, string descricao, string status)
        {
            Expression<Func<Fabricante, bool>> expressaoParaConsulta = fabricante => fabricante.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(fabricante => fabricante.Ativo == status);
            }

            if (idFabricante != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(ncm => ncm.Id == idFabricante.Value);
            }

            return _session.QueryOver<Fabricante>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<Fabricante> ConsulteListaAtiva()
        {
            return _session.QueryOver<Fabricante>().Where(fabricante => fabricante.Ativo == "A").List().ToList();
        }
    }
}
