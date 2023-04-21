using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.ConfiguracoesSistema.ConfiguracoesPdvObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.ConfiguracoesSistema.ConfiguracoesPdvObj.Repositorio;

namespace Programax.Easy.Servico.ConfiguracoesSistema.ConfiguracoesPdvServ
{
    public class ConversorConfiguracoesPdv : ConversorDeObjetoBasico<ConfiguracoesPdv>, IConversorDeObjeto<ConfiguracoesPdv>
    {
        public ConfiguracoesPdv CopieObjetoParaPersistencia(ConfiguracoesPdv objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioConfiguracoesPdv>();

            var configuracoesPdvDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new ConfiguracoesPdv();

            CopieTodasAsPropriedades(objetoDeNegocio, configuracoesPdvDaBase);

            return configuracoesPdvDaBase;
        }
    }
}
