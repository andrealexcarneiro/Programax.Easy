using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EstadoObj.Repositorio;
using NHibernate;

namespace Programax.Easy.Servico.Cadastros.EstadoServ
{
    public class RepositorioEstado : RepositorioBase<Estado>, IRepositorioEstado
    {
        public RepositorioEstado(ISession sessao)
            : base(sessao)
        {

        }
    }
}
