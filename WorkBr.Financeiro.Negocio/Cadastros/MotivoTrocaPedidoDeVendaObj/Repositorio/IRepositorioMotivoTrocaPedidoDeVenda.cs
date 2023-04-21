using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MotivoTrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.MotivoTrocaPedidoDeVendaObj.Repositorio
{
    public interface IRepositorioMotivoTrocaPedidoDeVenda : IRepositorioBase<MotivoTrocaPedidoDeVenda>
    {
        List<MotivoTrocaPedidoDeVenda> ConsulteLista(string descricao, string status);

        List<MotivoTrocaPedidoDeVenda> ConsulteListaAtiva();
    }
}
