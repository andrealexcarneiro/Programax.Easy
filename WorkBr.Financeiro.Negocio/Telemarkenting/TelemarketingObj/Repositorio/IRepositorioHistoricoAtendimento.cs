using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System.Collections.Generic;

namespace Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio
{
    public interface IRepositorioHistoricoAtendimento : IRepositorioBase<HistoricoAtendimento>
    {
        List<HistoricoAtendimento> ConsulteLista(int idPedido);
    }
}
