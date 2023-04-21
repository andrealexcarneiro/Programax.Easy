using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.OrigemClienteObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.OrigemClienteObj.Repositorio
{
    public interface IRepositorioOrigemCliente : IRepositorioBase<OrigemCliente>
    {
        List<OrigemCliente> ConsulteLista(string descricao, string status);

        List<OrigemCliente> ConsulteListaAtiva();
    }
}
