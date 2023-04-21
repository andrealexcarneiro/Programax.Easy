using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.RoteiroObj;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using System.Transactions;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;

namespace Programax.Easy.Servico.Vendas.RoteiroServ
{
    [Funcionalidade(EnumFuncionalidade.AGENDA)]    
    public class ServicoRoteiro : ServicoAkilSmallBusiness<Roteiro, ValidacaoRoteiro, ConversorRoteiro>
    {
        protected IRepositorioRoteiro _repositorioRoteiro;

        #region " CONSTRUTOR "

        public ServicoRoteiro()
        {
            RetorneRepositorio();
        }

        public ServicoRoteiro(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<Roteiro> RetorneRepositorio()
        {
            if (_repositorioRoteiro == null)
            {
                _repositorioRoteiro = FabricaDeRepositorios.Crie<IRepositorioRoteiro>();
            }

            return _repositorioRoteiro;
        }

        #endregion

        #region "Consultas"

        public List<Roteiro> ConsulteLista(Pessoa pessoa,
                                                                           EnumPeriodo? periodo,
                                                                           EnumStatusRoteiro? statusRoteiro,
                                                                           EnumDataFiltrarRoteiro? tipoDataFiltrar,
                                                                           DateTime? dataInicialPeriodo,
                                                                           DateTime? dataFinalPeriodo,
                                                                           int? idPedido,
                                                                           bool buscarConcluidos = true)
        {
            
            return _repositorioRoteiro.ConsulteLista(pessoa, periodo, statusRoteiro, tipoDataFiltrar, dataInicialPeriodo, dataFinalPeriodo, idPedido);
        }

        public Roteiro ConsultePorPedido(int idPedido)
        {
            return _repositorioRoteiro.ConsultePorPedido(idPedido);
        }

        public List<Roteiro> ConsulteListaPorRoteirizacao(int roteirizacaoId)
        {
            return _repositorioRoteiro.ConsulteListaPorRoteirizacao(roteirizacaoId);
        }

        public void AtualizeExclusaoAgenda(int agendaId, int pedidoId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda(false, true);

                var pedido = servicoPedido.Consulte(pedidoId);

                pedido.StatusRoteiro = null;

                servicoPedido.Atualize(pedido);

                Exclua(agendaId);

                scope.Complete();
            }
        }

        public void SalveAgenda(Roteiro agenda, int pedidoId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda(false, true);

                var pedido = servicoPedido.Consulte(pedidoId);

                pedido.StatusRoteiro = EnumStatusRoteiro.EMAGENDA;

                servicoPedido.Atualize(pedido);

                if (agenda.Id == 0)
                    Cadastre(agenda);
                else
                    Atualize(agenda);

                scope.Complete();
            }
        }

        #endregion
    }
}
