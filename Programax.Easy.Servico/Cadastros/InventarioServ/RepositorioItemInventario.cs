using NHibernate;
using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Cadastros.InventarioServ
{
    public class RepositorioItemInventario : RepositorioBase<ItemInventario>, IRepositorioItemInventario
    {
        public RepositorioItemInventario(ISession sessao)
            : base(sessao)
        {

        }
    }
}
