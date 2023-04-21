using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.FabricanteObj.Repositorio
{
    public interface IRepositorioFabricante : IRepositorioBase<Fabricante>
    {
        List<Fabricante> ConsulteLista(int? idFabricante, string descricao, string status);

        List<Fabricante> ConsulteListaAtiva();
    }
}
