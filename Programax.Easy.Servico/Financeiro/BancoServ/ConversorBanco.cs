using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Financeiro.BancoServ
{
    public class ConversorBanco : ConversorDeObjetoBasico<Banco>, IConversorDeObjeto<Banco>
    {
        public Banco CopieObjetoParaPersistencia(Banco objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioBanco>();

            var bancoDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Banco();

            CopieTodasAsPropriedades(objetoDeNegocio, bancoDaBase);

            return bancoDaBase;
        }
    }
}
