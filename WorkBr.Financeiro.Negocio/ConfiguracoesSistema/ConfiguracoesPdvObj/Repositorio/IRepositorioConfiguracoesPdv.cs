using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ConfiguracoesPdvObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.ConfiguracoesPdvObj.Repositorio
{
    public interface IRepositorioConfiguracoesPdv : IRepositorioBase<ConfiguracoesPdv>
    {
        ConfiguracoesPdv ConsulteUltimaConfiguracaoPdv();
    }
}
