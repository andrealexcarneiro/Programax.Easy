using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Cadastros.CidadeServ
{
    public class ConversorCidade : ConversorDeObjetoBasico<Cidade>, IConversorDeObjeto<Cidade>
    {
        public Cidade CopieObjetoParaPersistencia(Cidade objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCidade>();

            var cidadeDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Cidade();

            CopieTodasAsPropriedades(objetoDeNegocio, cidadeDaBase);

            return cidadeDaBase;
        }
    }
}
