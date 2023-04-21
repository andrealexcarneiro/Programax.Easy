using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.Repositorio;
using NHibernate;

namespace Programax.Easy.Servico.Cadastros.EmpresaServ
{
    public class RepositorioEmpresa : RepositorioBase<Empresa>, IRepositorioEmpresa
    {
        public RepositorioEmpresa(ISession sessao)
            : base(sessao)
        {
        }

        public Empresa ConsulteUltimaEmpresa()
        {
            return _session.QueryOver<Empresa>().OrderBy(x => x.Id).Desc.SingleOrDefault();
        }
    }
}
