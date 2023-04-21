using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.Repositorio;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.GrupoTributacaoIcmsServ
{
    public class RepositorioGrupoTributacaoIcms : RepositorioBase<GrupoTributacaoIcms>, IRepositorioGrupoTributacaoIcms
    {
        public RepositorioGrupoTributacaoIcms(ISession sessao)
            : base(sessao)
        {

        }

        public List<GrupoTributacaoIcms> ConsulteLista(string descricao)
        {
            return _session.QueryOver<GrupoTributacaoIcms>().Where(grupoTributacaoIcms => grupoTributacaoIcms.Descricao.IsLike("%" + descricao + "%")).List().ToList();
        }

        public GrupoTributacaoIcms ConsulteTributacaoTerceirosId(int id)
        {
            return _session.QueryOver<GrupoTributacaoIcms>().Where(grupo => grupo.Id == id && grupo.NaturezaProduto == EnumNaturezaProduto.TERCEIROS).Take(1).SingleOrDefault();
        }

        public GrupoTributacaoIcms ConsulteTributacaoProducaoPropriaId(int id)
        {
            return _session.QueryOver<GrupoTributacaoIcms>().Where(grupo => grupo.Id == id && grupo.NaturezaProduto == EnumNaturezaProduto.FABRICACAOPROPRIA).Take(1).SingleOrDefault();
        }
    }
}
