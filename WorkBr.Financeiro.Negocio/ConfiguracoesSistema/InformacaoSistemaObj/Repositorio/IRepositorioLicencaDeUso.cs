using Programax.Easy.Negocio.ConfiguracoesSistema.InformacaoSistemaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.LicencaDeUsoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.InformacaoSistemaObj.Repositorio
{
    public interface IRepositorioInformacaoSistema : IRepositorioBase<InformacaoSistema>
    {
        InformacaoSistema ConsulteUltimaInformacaoSistema();
    }
}
