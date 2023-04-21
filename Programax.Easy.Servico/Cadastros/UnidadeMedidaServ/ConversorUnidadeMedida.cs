using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Cadastros.UnidadeMedidaServ
{
    public class ConversorUnidadeMedida : ConversorDeObjetoBasico<UnidadeMedida>, IConversorDeObjeto<UnidadeMedida>
    {
        public UnidadeMedida CopieObjetoParaPersistencia(UnidadeMedida objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioUnidadeMedida>();

            var unidadeMedidaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new UnidadeMedida();

            CopieTodasAsPropriedades(objetoDeNegocio, unidadeMedidaDaBase);

            return unidadeMedidaDaBase;
        }
    }
}
