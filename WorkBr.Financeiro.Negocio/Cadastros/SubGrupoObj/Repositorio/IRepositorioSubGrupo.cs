using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.SubGrupoObj.Repositorio
{
    public interface IRepositorioSubGrupo : IRepositorioBase<SubGrupo>
    {
        List<SubGrupo> ConsulteLista(string descricao, Grupo grupo, string status);

        List<SubGrupo> ConsulteListaAtiva(Grupo grupo);
    }
}
