using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.GrupoObj.Repositorio
{
    public interface IRepositorioGrupo : IRepositorioBase<Grupo>
    {
        System.Collections.Generic.List<Grupo> ConsulteLista(string descricao, Categoria categoria, string status);

        System.Collections.Generic.List<Grupo> ConsulteListaAtivos(Categoria categoria);

        System.Collections.Generic.List<Grupo> ConsulteListaAtivos();
    }
}
