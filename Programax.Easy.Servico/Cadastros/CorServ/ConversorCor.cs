using Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CorObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Cadastros.CorServ
{
    public class ConversorCor : ConversorDeObjetoBasico<Cor>, IConversorDeObjeto<Cor>
    {
        public Cor CopieObjetoParaPersistencia(Cor objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCor>();

            var corDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Cor();

            CopieTodasAsPropriedades(objetoDeNegocio, corDaBase);

            return corDaBase;
        }
    }
}
