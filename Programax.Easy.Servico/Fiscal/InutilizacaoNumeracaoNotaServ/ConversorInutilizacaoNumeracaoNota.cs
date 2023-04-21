using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Fiscal.InutilizacaoNumeracaoNotaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Fiscal.InutilizacaoNumeracaoNotaObj.Repositorio;

namespace Programax.Easy.Servico.Fiscal.InutilizacaoNumeracaoNotaServ
{
    public class ConversorInutilizacaoNumeracaoNota : ConversorDeObjetoBasico<InutilizacaoNumeracaoNota>, IConversorDeObjeto<InutilizacaoNumeracaoNota>
    {
        public InutilizacaoNumeracaoNota CopieObjetoParaPersistencia(InutilizacaoNumeracaoNota objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioInutilizacaoNumeracaoNota>();

            var notaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new InutilizacaoNumeracaoNota();

            CopieTodasAsPropriedades(objetoDeNegocio, notaDaBase);

            return notaDaBase;
        }
    }
}
