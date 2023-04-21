using Programax.Easy.Negocio.Financeiro.GruposDreObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.GruposDreObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.GruposDreServ
{
    public class ConversorGrupoDre : ConversorDeObjetoBasico<GrupoDre>, IConversorDeObjeto<GrupoDre>
    {
        public GrupoDre CopieObjetoParaPersistencia(GrupoDre objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioGrupoDre>();

            var grupoDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new GrupoDre();

            CopieTodasAsPropriedades(objetoDeNegocio, grupoDaBase);

            return grupoDaBase;
        }
    }
}
