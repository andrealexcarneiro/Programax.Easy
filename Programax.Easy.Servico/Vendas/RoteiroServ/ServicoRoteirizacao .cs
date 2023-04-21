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
    [Funcionalidade(EnumFuncionalidade.ROTEIROS)]
    public class ServicoRoteirizacao : ServicoAkilSmallBusiness<Roteirizacao, ValidacaoRoteirizacao, ConversorRoteirizacao>
    {
        protected IRepositorioRoteirizacao _repositorioRoteirizacao;

        #region " CONSTRUTOR "

        public ServicoRoteirizacao()
        {
            RetorneRepositorio();
        }

        public ServicoRoteirizacao(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<Roteirizacao> RetorneRepositorio()
        {
            if (_repositorioRoteirizacao == null)
            {
                _repositorioRoteirizacao = FabricaDeRepositorios.Crie<IRepositorioRoteirizacao>();
            }

            return _repositorioRoteirizacao;
        }

        #endregion

        #region "Consultas"

        public List<Roteirizacao> ConsulteLista(Pessoa pessoa,
                                                                           EnumStatusRoteiro? statusRoteiro,
                                                                           EnumDataFiltrarRoteiro? tipoDataFiltrar,
                                                                           DateTime? dataInicialPeriodo,
                                                                           DateTime? dataFinalPeriodo)
        {
            return _repositorioRoteirizacao.ConsulteLista(pessoa, statusRoteiro, tipoDataFiltrar, dataInicialPeriodo, dataFinalPeriodo);
        }

        public List<Roteirizacao> ConsulteListaCodigoRoteiro(int? numeroRoteiro)
        {
            return _repositorioRoteirizacao.ConsulteListaCodigoRoteiro(numeroRoteiro);
        }

        #endregion

        #region "Exclusões, Inclusões e Alterações"

        public void ExcluirRoteirizacaoEAtualizarAgendaEPedido(int roteirizacaoId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

                ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

                var roteiro = servicoRoteiro.ConsulteListaPorRoteirizacao(roteirizacaoId);

                foreach (var itemAtualizar in roteiro)
                {
                    //Agendas
                    itemAtualizar.Status = EnumStatusRoteiro.EMAGENDA;
                    itemAtualizar.RoteirizacaoId = null;

                    servicoRoteiro.Atualize(itemAtualizar);

                    //Pedidos                    
                    var pedido = servicoPedido.Consulte(itemAtualizar.PedidoVenda.Id);
                    pedido.StatusRoteiro = EnumStatusRoteiro.EMAGENDA;

                    servicoPedido.Atualize(pedido);
                }

                Exclua(roteirizacaoId);

                scope.Complete();
            }
        }

        public void SalveRoteirizacaoEAtualizeAgendaEPedido(Roteirizacao roteirizacao, List<Roteiro> listaAgendas)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

                ServicoRoteiro servicoRoteiro = new ServicoRoteiro();
                
                if (roteirizacao.Id == 0)
                {
                    Cadastre(roteirizacao);
                }
                else
                {
                    var rotAtualizar = Consulte(roteirizacao.Id);

                    rotAtualizar.Status = EnumStatusRoteiro.EMROTA;
                    rotAtualizar.DataCriacao = roteirizacao.DataCriacao;
                    rotAtualizar.PessoaFuncionario = roteirizacao.PessoaFuncionario;
                    rotAtualizar.Usuario = roteirizacao.Usuario;

                    Atualize(roteirizacao);

                    //Caso escolher outras Agendas no Grid

                    //EstorneAgendasEmRota(roteirizacao.Id, listaAgendas);

                    var roteirosDoBanco = servicoRoteiro.ConsulteListaPorRoteirizacao(roteirizacao.Id);

                    foreach (var itemDoBanco in roteirosDoBanco)
                    {
                        var naoTem = !listaAgendas.Exists(x => x.Id == itemDoBanco.Id);

                        if (naoTem)
                        {
                            itemDoBanco.RoteirizacaoId = null;
                            itemDoBanco.Status = EnumStatusRoteiro.EMAGENDA;
                            servicoRoteiro.Atualize(itemDoBanco);

                            var pedidoAtualizar = servicoPedido.Consulte(itemDoBanco.PedidoVenda.Id);

                            pedidoAtualizar.StatusRoteiro = EnumStatusRoteiro.EMAGENDA;

                            servicoPedido.Atualize(pedidoAtualizar);
                        }
                    }

                }

                foreach (var item in listaAgendas)
                {
                    var itemAtual = servicoRoteiro.Consulte(item.Id);

                    itemAtual.RoteirizacaoId = roteirizacao.Id;
                    itemAtual.Status = EnumStatusRoteiro.EMROTA;
                    servicoRoteiro.Atualize(itemAtual);
                }

                foreach (var item in listaAgendas)
                {
                    var pedidoAtualizar = servicoPedido.Consulte(item.PedidoVenda.Id);

                    pedidoAtualizar.StatusRoteiro = EnumStatusRoteiro.EMROTA;

                    servicoPedido.Atualize(pedidoAtualizar);
                }

                scope.Complete();
            }
        }

        private void EstorneAgendasEmRota(int numeroRoteirizacao, List<Roteiro> listaAgenda)
        {
            ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

            var roteirosDoBanco = servicoRoteiro.ConsulteListaPorRoteirizacao(numeroRoteirizacao);

            ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

            foreach (var itemDoBanco in roteirosDoBanco)
            {
                var naoTem = !listaAgenda.Exists(x => x.Id == itemDoBanco.Id);

                if (naoTem)
                {
                    itemDoBanco.RoteirizacaoId = null;
                    itemDoBanco.Status = EnumStatusRoteiro.EMAGENDA;
                    servicoRoteiro.Atualize(itemDoBanco);

                    var pedidoAtualizar = servicoPedido.Consulte(itemDoBanco.PedidoVenda.Id);

                    pedidoAtualizar.StatusRoteiro = EnumStatusRoteiro.EMAGENDA;

                    servicoPedido.Atualize(pedidoAtualizar);
                }
            }
        }

        public void ConcluaRoteirizacaoEAtualizePedidoEAgenda(List<Roteirizacao> listaRoteirizacao)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();
                ServicoRoteirizacao servicoRoteirizacao = new ServicoRoteirizacao();
                ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

                foreach (var item in listaRoteirizacao)
                {
                    //Roteirização
                    var itemConcluir = servicoRoteirizacao.Consulte(item.Id);

                    itemConcluir.Status = EnumStatusRoteiro.CONCLUIDO;
                    itemConcluir.DataConclusao = DateTime.Now;

                    servicoRoteirizacao.Atualize(itemConcluir);


                    var roteiro = servicoRoteiro.ConsulteListaPorRoteirizacao(itemConcluir.Id);

                    foreach (var itemAtualizar in roteiro)
                    {
                        //Agenda
                        var itemRoteiro = servicoRoteiro.Consulte(itemAtualizar.Id);

                        itemRoteiro.Status = EnumStatusRoteiro.CONCLUIDO;
                        itemRoteiro.DataConclusao = DateTime.Now;

                        servicoRoteiro.Atualize(itemRoteiro);

                        //Pedido
                        var pedido = servicoPedido.Consulte(itemAtualizar.PedidoVenda.Id);

                        pedido.StatusRoteiro = EnumStatusRoteiro.CONCLUIDO;

                        servicoPedido.Atualize(pedido);
                    }
                }

                scope.Complete();
            }
        }

        public void EstorneConclusaoDeRoteirizacao(List<Roteirizacao> listaRoteirizacao)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                ServicoRoteirizacao servicoRoteirizacao = new ServicoRoteirizacao();
                ServicoRoteiro servicoRoteiro = new ServicoRoteiro();
                ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

                foreach (var item in listaRoteirizacao)
                {
                    //Roteirização
                    var itemEstorno = servicoRoteirizacao.Consulte(item.Id);

                    itemEstorno.DataConclusao = null;
                    itemEstorno.Status = EnumStatusRoteiro.EMROTA;

                    servicoRoteirizacao.Atualize(itemEstorno);

                    var roteiro = servicoRoteiro.ConsulteListaPorRoteirizacao(itemEstorno.Id);

                    foreach (var itemAtualizar in roteiro)
                    {
                        //Agendas                       
                        var itemRoteiro = servicoRoteiro.Consulte(itemAtualizar.Id);

                        itemRoteiro.Status = EnumStatusRoteiro.EMROTA;
                        itemRoteiro.DataConclusao = null;

                        servicoRoteiro.Atualize(itemRoteiro);

                        //Pedidos                       
                        var pedido = servicoPedido.Consulte(itemAtualizar.PedidoVenda.Id);

                        pedido.StatusRoteiro = EnumStatusRoteiro.EMROTA;

                        servicoPedido.Atualize(pedido);
                    }
                }

                scope.Complete();
            }
        }

    #endregion
    }
}
