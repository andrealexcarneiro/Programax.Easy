using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Cadastros.CategoriaServ
{
    public class ConversorCategoria : ConversorDeObjetoBasico<Categoria>, IConversorDeObjeto<Categoria>
    {
        public Categoria CopieObjetoParaPersistencia(Categoria objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCategoria>();

            var linhaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Categoria();

            CopieTodasAsPropriedades(objetoDeNegocio, linhaDaBase);

            return linhaDaBase;
        }
    }
}
