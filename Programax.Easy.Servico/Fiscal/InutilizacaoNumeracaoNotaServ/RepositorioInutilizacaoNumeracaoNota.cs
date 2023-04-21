using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Fiscal.InutilizacaoNumeracaoNotaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.InutilizacaoNumeracaoNotaObj.Repositorio;
using NHibernate;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using System.Linq.Expressions;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Fiscal.InutilizacaoNumeracaoNotaServ
{
    public class RepositorioInutilizacaoNumeracaoNota : RepositorioBase<InutilizacaoNumeracaoNota>, IRepositorioInutilizacaoNumeracaoNota
    {
        public RepositorioInutilizacaoNumeracaoNota(ISession sessao)
            : base(sessao)
        {

        }
    }
}
