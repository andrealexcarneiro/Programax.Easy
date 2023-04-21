using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.Repositorio;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.GrupoTributacaoFederalServ
{
    public class RepositorioGrupoTributacaoFederal : RepositorioBase<GrupoTributacaoFederal>, IRepositorioGrupoTributacaoFederal
    {
        public RepositorioGrupoTributacaoFederal(ISession sessao)
            : base(sessao)
        {

        }

        public List<GrupoTributacaoFederal> ConsulteLista(string descricao)
        {
            return _session.QueryOver<GrupoTributacaoFederal>().Where(grupoTributacaoIcms => grupoTributacaoIcms.Descricao.IsLike("%" + descricao + "%")).List().ToList();
        }

        public GrupoTributacaoFederal ConsulteTributacaoTerceirosId(int id)
        {
            return _session.QueryOver<GrupoTributacaoFederal>().Where(grupo => grupo.Id == id && grupo.NaturezaProduto == EnumNaturezaProduto.TERCEIROS).Take(1).SingleOrDefault();
        }

        public GrupoTributacaoFederal ConsulteTributacaoProducaoPropriaId(int id)
        {
            return _session.QueryOver<GrupoTributacaoFederal>().Where(grupo => grupo.Id == id && grupo.NaturezaProduto == EnumNaturezaProduto.FABRICACAOPROPRIA).Take(1).SingleOrDefault();
        }
    }
}
