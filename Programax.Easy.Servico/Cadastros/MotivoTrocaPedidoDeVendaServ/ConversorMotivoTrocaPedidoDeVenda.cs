using Programax.Easy.Negocio.Cadastros.MotivoTrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MotivoTrocaPedidoDeVendaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Cadastros.MotivoTrocaPedidoDeVendaServ
{
    public class ConversorMotivoTrocaPedidoDeVenda : ConversorDeObjetoBasico<MotivoTrocaPedidoDeVenda>, IConversorDeObjeto<MotivoTrocaPedidoDeVenda>
    {
        public MotivoTrocaPedidoDeVenda CopieObjetoParaPersistencia(MotivoTrocaPedidoDeVenda objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioMotivoTrocaPedidoDeVenda>();

            var motivoTrocaPedidoDeVendaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new MotivoTrocaPedidoDeVenda();

            CopieTodasAsPropriedades(objetoDeNegocio, motivoTrocaPedidoDeVendaDaBase);

            return motivoTrocaPedidoDeVendaDaBase;
        }
    }
}
