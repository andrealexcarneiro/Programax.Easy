using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.Repositorio
{
    public interface IRepositorioMotivoCorrecaoEstoque : IRepositorioBase<MotivoCorrecaoEstoque>
    {
        List<MotivoCorrecaoEstoque> ConsulteLista(string descricao, string status);
    }
}
