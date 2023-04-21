using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Integracao.TabelasAtualizadasIntegracaoDJObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Integracao.TabelasAtualizadasIntegracaoDJObj.Repositorio;

namespace Programax.Easy.Servico.Integracao.TabelasAtualizadasIntegracaoDJServ
{
    public class ConversorTabelasAtualizadasIntegracaoDJ : ConversorDeObjetoBasico<TabelasAtualizadasIntegracaoDJ>, IConversorDeObjeto<TabelasAtualizadasIntegracaoDJ>
    {
        public TabelasAtualizadasIntegracaoDJ CopieObjetoParaPersistencia(TabelasAtualizadasIntegracaoDJ objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioTabelasAtualizadasIntegracaoDJ>();

            var tabelaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new TabelasAtualizadasIntegracaoDJ();

            CopieTodasAsPropriedades(objetoDeNegocio, tabelaBase);

            return tabelaBase;
        }
    }
}
