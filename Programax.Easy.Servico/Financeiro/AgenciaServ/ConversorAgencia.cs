using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Financeiro.AgenciaServ
{
    public class ConversorAgencia : ConversorDeObjetoBasico<Agencia>, IConversorDeObjeto<Agencia>
    {
        public Agencia CopieObjetoParaPersistencia(Agencia objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioAgencia>();

            var agenciaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Agencia();

            CopieTodasAsPropriedades(objetoDeNegocio, agenciaDaBase);

            return agenciaDaBase;
        }
    }
}
