using Programax.Easy.Negocio.Cadastros.OrigemClienteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.OrigemClienteObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Cadastros.OrigemClienteObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.OrigemClienteServ
{
    public class ConversorOrigemCliente : ConversorDeObjetoBasico<OrigemCliente>, IConversorDeObjeto<OrigemCliente>
    {
        public OrigemCliente CopieObjetoParaPersistencia(OrigemCliente objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioOrigemCliente>();

            var OrigemClienteBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new OrigemCliente();

            CopieTodasAsPropriedades(objetoDeNegocio, OrigemClienteBase);

            return OrigemClienteBase;
        }
    }
}
