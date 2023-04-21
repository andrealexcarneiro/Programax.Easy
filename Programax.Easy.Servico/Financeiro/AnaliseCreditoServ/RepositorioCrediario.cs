using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Financeiro.CrediarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CrediarioObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using NHibernate;

namespace Programax.Easy.Servico.Financeiro.CrediarioServ
{
    public class RepositorioCrediario : RepositorioBase<Crediario>, IRepositorioCrediario
    {
        public RepositorioCrediario(ISession sessao)
            : base(sessao)
        {

        }
    }
}
