using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Vendas.RoteiroServ
{
    public class ConversorRoteirizacao : ConversorDeObjetoBasico<Roteirizacao>, IConversorDeObjeto<Roteirizacao>
    {
        public Roteirizacao CopieObjetoParaPersistencia(Roteirizacao objetoDeNegocio)
        {
            var objetoDeNegocioCopiado = objetoDeNegocio.CloneCompleto();

            var repositorio = FabricaDeRepositorios.Crie<IRepositorioRoteirizacao>();

            var roteiroBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Roteirizacao();

            CopieTodasAsPropriedades(objetoDeNegocioCopiado, roteiroBase);

            return roteiroBase;
        }
    }
}
