using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System.Collections.Generic;

namespace Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio
{
    public interface IRepositorioHistoricoRoteiro : IRepositorioBase<HistoricoRoteiro>
    {
        List<HistoricoRoteiro> ConsulteLista(int idRoteiro);
    }
}
