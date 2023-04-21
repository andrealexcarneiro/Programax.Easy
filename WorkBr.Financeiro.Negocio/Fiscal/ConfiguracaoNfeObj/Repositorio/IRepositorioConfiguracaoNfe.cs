using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.ConfiguracaoNfeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.ConfiguracaoNfeObj.Repositorio
{
    public interface IRepositorioConfiguracaoNfe : IRepositorioBase<ConfiguracaoNfe>
    {
        ConfiguracaoNfe ConsulteConfiguracoesNfe(EnumModeloNotaFiscal ModeloNF = EnumModeloNotaFiscal.NFE);
    }
}
