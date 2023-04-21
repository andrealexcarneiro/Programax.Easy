using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.InventarioServ
{
    [Funcionalidade(EnumFuncionalidade.INVENTARIO)]
    public class ServicoItemInventario : ServicoAkilSmallBusiness<ItemInventario, ValidacaoItemInventario, ConversorItemInventario>
    {
        private IRepositorioItemInventario _repositorioItemInventario;

        public ServicoItemInventario()
        {
            RetorneRepositorio();
        }

        public ServicoItemInventario(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<ItemInventario> RetorneRepositorio()
        {
            if (_repositorioItemInventario == null)
            {
                _repositorioItemInventario = FabricaDeRepositorios.Crie<IRepositorioItemInventario>();
            }

            return _repositorioItemInventario;
        }
    }
}
