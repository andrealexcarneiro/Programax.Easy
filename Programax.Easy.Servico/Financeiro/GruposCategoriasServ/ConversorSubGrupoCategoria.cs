using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.Repositorio;

namespace Programax.Easy.Servico.Financeiro.GruposCategoriasServ
{
    public class ConversorSubGrupoCategoria:ConversorDeObjetoBasico<SubGrupoCategoria>, IConversorDeObjeto<SubGrupoCategoria>
    {
        public SubGrupoCategoria CopieObjetoParaPersistencia(SubGrupoCategoria objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioSubGrupoCategoria>();

            var grupoDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new SubGrupoCategoria();

            CopieTodasAsPropriedades(objetoDeNegocio, grupoDaBase);

            return grupoDaBase;
        }
    }
}
