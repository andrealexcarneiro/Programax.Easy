using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.Repositorio;
using NHibernate;

namespace Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ
{
    public class RepositorioParametros : RepositorioBase<Parametros>, IRepositorioParametros
    {
        public RepositorioParametros(ISession sessao)
            : base(sessao)
        {

        }

        public Parametros ConsulteParametros()
        {
            return ConsulteLista().FirstOrDefault();
        }
    }
}
