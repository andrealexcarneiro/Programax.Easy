using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Fiscal.ConfiguracaoNfeObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Fiscal.ConfiguracaoNfeObj.Repositorio;

namespace Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ
{
    public class ConversorConfiguracaoNfe : ConversorDeObjetoBasico<ConfiguracaoNfe>, IConversorDeObjeto<ConfiguracaoNfe>
    {
        public ConfiguracaoNfe CopieObjetoParaPersistencia(ConfiguracaoNfe objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioConfiguracaoNfe>();

            var configuracaoNfeBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new ConfiguracaoNfe();

            CopieTodasAsPropriedades(objetoDeNegocio, configuracaoNfeBase);

            return configuracaoNfeBase;
        }
    }
}
