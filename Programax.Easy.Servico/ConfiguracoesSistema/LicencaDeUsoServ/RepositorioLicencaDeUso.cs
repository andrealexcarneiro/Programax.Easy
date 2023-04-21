using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.ConfiguracoesSistema.LicencaDeUsoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.LicencaDeUsoObj.Repositorio;
using NHibernate;

namespace Programax.Easy.Servico.ConfiguracoesSistema.LicencaDeUsoServ
{
    public class RepositorioLicencaDeUso : RepositorioBase<LicencaDeUso>, IRepositorioLicencaDeUso
    {
        public RepositorioLicencaDeUso(ISession sessao)
            : base(sessao)
        {

        }

        public LicencaDeUso ConsulteUltimaLicencaDeUso()
        {
            return _session.QueryOver<LicencaDeUso>().OrderBy(x => x.Id).Desc.SingleOrDefault();
        }
    }
}
