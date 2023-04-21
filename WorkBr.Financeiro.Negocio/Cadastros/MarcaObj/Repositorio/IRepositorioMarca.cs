using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.MarcaObj.Repositorio
{
    public interface IRepositorioMarca : IRepositorioBase<Marca>
    {
        List<Marca> ConsulteLista(int? idMarca, string descricao, string status);

        List<Marca> ConsulteListaAtiva();
    }
}
