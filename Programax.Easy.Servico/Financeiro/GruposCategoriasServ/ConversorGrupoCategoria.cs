using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.Repositorio;

namespace Programax.Easy.Servico.Financeiro.GruposCategoriasServ
{
    public class ConversorGrupoCategoria:ConversorDeObjetoBasico<GrupoCategoria>, IConversorDeObjeto<GrupoCategoria>
    {
        public GrupoCategoria CopieObjetoParaPersistencia(GrupoCategoria objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioGrupoCategoria>();

            var grupoDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new GrupoCategoria();

            CopieTodasAsPropriedades(objetoDeNegocio, grupoDaBase);

            return grupoDaBase;
        }
    }
}
