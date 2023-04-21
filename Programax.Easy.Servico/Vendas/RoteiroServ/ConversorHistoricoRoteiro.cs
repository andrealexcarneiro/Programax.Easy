using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Vendas.RoteiroServ
{
    public class ConversorHistoricoRoteiro : ConversorDeObjetoBasico<HistoricoRoteiro>, IConversorDeObjeto<HistoricoRoteiro>
    {
        public HistoricoRoteiro CopieObjetoParaPersistencia(HistoricoRoteiro objetoDeNegocio)
        {
            var objetoDeNegocioCopiado = objetoDeNegocio.CloneCompleto();

            var repositorio = FabricaDeRepositorios.Crie<IRepositorioHistoricoRoteiro>();

            var roteiroBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new HistoricoRoteiro();

            CopieTodasAsPropriedades(objetoDeNegocioCopiado, roteiroBase);

            return roteiroBase;
        }
    }
}
