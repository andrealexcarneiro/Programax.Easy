using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Vendas.RoteiroServ
{
    public class ConversorRoteiro : ConversorDeObjetoBasico<Roteiro>, IConversorDeObjeto<Roteiro>
    {
        public Roteiro CopieObjetoParaPersistencia(Roteiro objetoDeNegocio)
        {
            var objetoDeNegocioCopiado = objetoDeNegocio.CloneCompleto();

            var repositorio = FabricaDeRepositorios.Crie<IRepositorioRoteiro>();

            var roteiroBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Roteiro();

            CopieTodasAsPropriedades(objetoDeNegocioCopiado, roteiroBase);

            return roteiroBase;
        }
    }
}
