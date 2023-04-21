using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.CateogriaObj.Repositorio
{
    public interface IRepositorioCategoria : IRepositorioBase<Categoria>
    {
        List<Categoria> ConsulteLista(string descricao, string status);

        List<Categoria> ConsulteListaAtiva();
    }
}
