using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using System.Collections.Generic;
using Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj;
using System.Transactions;
using System;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.TeleMarketing.Enumeradores;

namespace Programax.Easy.Servico.Telemarketing.TmkServ
{
    [Funcionalidade(EnumFuncionalidade.TELEMARKETING)]
    public class ServicoHistoricoAtendimento : ServicoAkilSmallBusiness<HistoricoAtendimento, ValidacaoHistoricoAtendimento, ConversorHistoricoAtendimento>
    {
        protected IRepositorioHistoricoAtendimento _repositorioAtendimento;        

        #region " CONSTRUTOR "

        public ServicoHistoricoAtendimento()
        {
            RetorneRepositorio();
        }

        public ServicoHistoricoAtendimento(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<HistoricoAtendimento> RetorneRepositorio()
        {
            if (_repositorioAtendimento == null)
            {
                _repositorioAtendimento = FabricaDeRepositorios.Crie<IRepositorioHistoricoAtendimento>();
            }

            return _repositorioAtendimento;
        }

        #endregion


        #region "Consultas"

        public List<HistoricoAtendimento> ConsulteLista(int idPedido)
        {
            return _repositorioAtendimento.ConsulteLista(idPedido);
        }

        #endregion

        public void ConcluaPostergueAtendimentoEAtualizePedido(bool ehConclusao, HistoricoAtendimento historico)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                
                Cadastre(historico);

                ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

                var pedidoAtualizar = servicoPedido.Consulte(historico.Pedido.Id);

                //if (ehConclusao)
                //{
                //    pedidoAtualizar.StatusAtendimento = EnumStatusAtendimento.FINALIZADO;
                //}
                //else
                //{
                //    pedidoAtualizar.StatusAtendimento = EnumStatusAtendimento.AGENDADO;
                //}               

                //servicoPedido.Atualize(pedidoAtualizar);
                    
                scope.Complete();
            }
        }
    }
}
