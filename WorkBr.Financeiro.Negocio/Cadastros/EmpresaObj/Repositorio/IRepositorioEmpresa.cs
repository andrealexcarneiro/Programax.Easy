using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.EmpresaObj.Repositorio
{
    public interface IRepositorioEmpresa : IRepositorioBase<Empresa>
    {
        Empresa ConsulteUltimaEmpresa();
    }
}
