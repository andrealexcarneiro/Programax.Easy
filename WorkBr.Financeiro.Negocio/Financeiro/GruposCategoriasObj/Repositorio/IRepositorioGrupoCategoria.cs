using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.Repositorio
{
    public interface IRepositorioGrupoCategoria : IRepositorioBase <GrupoCategoria>
    {
        List<GrupoCategoria> ConsulteLista(int? idGrupo, string descricao, string status);

        List<GrupoCategoria> ConsulteListaAtiva();
    }
}
