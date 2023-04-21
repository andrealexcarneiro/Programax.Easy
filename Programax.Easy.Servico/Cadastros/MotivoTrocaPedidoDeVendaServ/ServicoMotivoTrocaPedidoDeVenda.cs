using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.MotivoTrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.MotivoTrocaPedidoDeVendaServ;
using Programax.Easy.Negocio.Cadastros.MotivoTrocaPedidoDeVendaObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.MotivoTrocaPedidoDeVendaServ
{
    [Funcionalidade(EnumFuncionalidade.MOTIVOTROCAPEDIDOVENDA)]
    public class ServicoMotivoTrocaPedidoDeVenda : ServicoAkilSmallBusiness<MotivoTrocaPedidoDeVenda, ValidacaoMotivoTrocaPedidoDeVenda, ConversorMotivoTrocaPedidoDeVenda>
    {
        IRepositorioMotivoTrocaPedidoDeVenda _repositorioMotivoTrocaPedidoDeVenda;

        public ServicoMotivoTrocaPedidoDeVenda()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<MotivoTrocaPedidoDeVenda> RetorneRepositorio()
        {
            if (_repositorioMotivoTrocaPedidoDeVenda == null)
            {
                _repositorioMotivoTrocaPedidoDeVenda = FabricaDeRepositorios.Crie<IRepositorioMotivoTrocaPedidoDeVenda>();
            }

            return _repositorioMotivoTrocaPedidoDeVenda;
        }

        public List<MotivoTrocaPedidoDeVenda> ConsulteLista()
        {
            return _repositorioMotivoTrocaPedidoDeVenda.ConsulteLista();
        }

        public List<MotivoTrocaPedidoDeVenda> ConsulteListaAtiva()
        {
            return _repositorioMotivoTrocaPedidoDeVenda.ConsulteListaAtiva();
        }

        public List<MotivoTrocaPedidoDeVenda> ConsulteLista(string descricao, string status)
        {
            return _repositorioMotivoTrocaPedidoDeVenda.ConsulteLista(descricao, status);
        }
    }
}
