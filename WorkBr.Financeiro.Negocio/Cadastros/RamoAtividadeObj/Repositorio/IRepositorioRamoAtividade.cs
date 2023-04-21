using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.Repositorio
{
    public interface IRepositorioRamoAtividade : IRepositorioBase<RamoAtividade>
    {
        List<RamoAtividade> ConsulteLista(string descricao, string status);

        List<RamoAtividade> ConsulteListaAtiva();
    }
}
