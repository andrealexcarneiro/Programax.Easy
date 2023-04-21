using Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System.Collections.Generic;

namespace Programax.Easy.Negocio.Cadastros.CorObj.Repositorio
{
    public interface IRepositorioCor : IRepositorioBase<Cor>
    {
        List<Cor> ConsulteLista(string descricao, string status);

        List<Cor> ConsulteListaAtiva();
    }
}
