using System.Collections.Generic;
using Programax.Easy.Negocio.Integracao.PreVendaDjpdvObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Integracao.PreVendaDjpdvObj.Repositorio
{
    public interface IRepositorioPreVendaDjpdv : IRepositorioBase<PreVendaDjpdv>
    {
        void ExcluaLista(List<PreVendaDjpdv> atualizacoes);
    }
}
