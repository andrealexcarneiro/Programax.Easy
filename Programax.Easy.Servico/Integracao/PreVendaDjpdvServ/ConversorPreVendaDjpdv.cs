using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Integracao.TabelasAtualizadasIntegracaoDJObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Integracao.TabelasAtualizadasIntegracaoDJObj.Repositorio;
using Programax.Easy.Negocio.Integracao.PreVendaDjpdvObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Integracao.PreVendaDjpdvObj.Repositorio;

namespace Programax.Easy.Servico.Integracao.PreVendaDjpdvServ
{
    public class ConversorPreVendaDjpdv : ConversorDeObjetoBasico<PreVendaDjpdv>, IConversorDeObjeto<PreVendaDjpdv>
    {
        public PreVendaDjpdv CopieObjetoParaPersistencia(PreVendaDjpdv objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioPreVendaDjpdv>();

            var preVendaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new PreVendaDjpdv();

            CopieTodasAsPropriedades(objetoDeNegocio, preVendaBase);

            return preVendaBase;
        }
    }
}
