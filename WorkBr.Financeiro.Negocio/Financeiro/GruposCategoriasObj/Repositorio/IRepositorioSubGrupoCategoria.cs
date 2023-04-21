using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.Repositorio
{
    public interface IRepositorioSubGrupoCategoria : IRepositorioBase <SubGrupoCategoria>
    {
        List<SubGrupoCategoria> ConsulteLista(int? idGrupo, string descricao, string status);

        List<SubGrupoCategoria> ConsulteListaAtiva();
    }
}
