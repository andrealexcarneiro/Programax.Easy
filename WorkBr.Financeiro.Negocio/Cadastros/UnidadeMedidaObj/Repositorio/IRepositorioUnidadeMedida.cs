using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.Repositorio
{
    public interface IRepositorioUnidadeMedida : IRepositorioBase<UnidadeMedida>
    {
        List<UnidadeMedida> ConsulteLista(string descricao, string abreviacao, string status);

        List<UnidadeMedida> ConsulteListaAtiva();

        UnidadeMedida ConsultePelaAbreviacao(string abreviacao, int idDesconsiderar);
    }
}
