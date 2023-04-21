using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.PaisObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PaisObj.Repositorio;
using NHibernate;

namespace Programax.Easy.Servico.Cadastros.PaisServ
{
    public class RepositorioPais : RepositorioBase<Pais>, IRepositorioPais
    {
        public RepositorioPais(ISession sessao)
            : base(sessao)
        {

        }
    }
}
