using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Fiscal.CartaCorrecaoObj.Repositorio;
using Programax.Easy.Negocio.Fiscal.CartaCorrecaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Repositorios;
using NHibernate;

namespace Programax.Easy.Servico.Fiscal.CartaCorrecaoServ
{
    public class RepositorioCartaCorrecao : RepositorioBase<CartaCorrecao>, IRepositorioCartaCorrecao
    {
        public RepositorioCartaCorrecao(ISession sessao)
            : base(sessao)
        {

        }
    }
}
