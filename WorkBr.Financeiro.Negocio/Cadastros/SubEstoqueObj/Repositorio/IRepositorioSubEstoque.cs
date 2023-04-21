using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubEstoqueObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.SubEstoqueObj.Repositorio
{
    public interface IRepositorioSubEstoque : IRepositorioBase<SubEstoque>
    {
        List<SubEstoque> ConsulteLista(int? id, string descricao, string status);

        List<SubEstoque> ConsulteListaAtiva();
    }
}
