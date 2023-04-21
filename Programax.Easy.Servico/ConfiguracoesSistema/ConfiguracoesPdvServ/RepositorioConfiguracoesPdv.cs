using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.ConfiguracoesSistema.ConfiguracoesPdvObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ConfiguracoesPdvObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Repositorios;
using NHibernate;

namespace Programax.Easy.Servico.ConfiguracoesSistema.ConfiguracoesPdvServ
{
    public class RepositorioConfiguracoesPdv : RepositorioBase<ConfiguracoesPdv>, IRepositorioConfiguracoesPdv
    {
        public RepositorioConfiguracoesPdv(ISession sessao)
            : base(sessao)
        {

        }

        public ConfiguracoesPdv ConsulteUltimaConfiguracaoPdv()
        {
            return _session.QueryOver<ConfiguracoesPdv>().SingleOrDefault();
        }
    }
}
