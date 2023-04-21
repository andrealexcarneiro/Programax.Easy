using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.TamanhoObj.Repositorio
{
    public interface IRepositorioTamanho : IRepositorioBase<Tamanho>
    {
        List<Tamanho> ConsulteLista(string descricao, string status);

        List<Tamanho> ConsulteListaAtiva();
    }
}
