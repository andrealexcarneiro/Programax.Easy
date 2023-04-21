using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Programax.Easy.Negocio.Fiscal.ConfiguracaoNfeObj.Repositorio;
using Programax.Easy.Negocio.Fiscal.ConfiguracaoNfeObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using NHibernate;

namespace Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ
{
    public class RepositorioConfiguracaoNfe : RepositorioBase<ConfiguracaoNfe>, IRepositorioConfiguracaoNfe
    {
        public RepositorioConfiguracaoNfe(ISession sessao)
            : base(sessao)
        {

        }

        public ConfiguracaoNfe ConsulteConfiguracoesNfe(EnumModeloNotaFiscal ModeloNF = EnumModeloNotaFiscal.NFE )
        {
            Expression<Func<ConfiguracaoNfe, bool>> expressaoParaConsulta = (x=>x.ModeloNF == ModeloNF);
            
            return _session.QueryOver<ConfiguracaoNfe>().Where(expressaoParaConsulta).SingleOrDefault();
        }
    }
}
