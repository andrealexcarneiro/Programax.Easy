using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Cadastros.TamanhoServ
{
    public class ConversorTamanho : ConversorDeObjetoBasico<Tamanho>, IConversorDeObjeto<Tamanho>
    {
        public Tamanho CopieObjetoParaPersistencia(Tamanho objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioTamanho>();

            var tamanhoDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Tamanho();

            CopieTodasAsPropriedades(objetoDeNegocio, tamanhoDaBase);

            return tamanhoDaBase;
        }
    }
}
