using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.LicencaDeUsoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.LicencaDeUsoObj.Repositorio
{
    public interface IRepositorioLicencaDeUso : IRepositorioBase<LicencaDeUso>
    {
        LicencaDeUso ConsulteUltimaLicencaDeUso();
    }
}
