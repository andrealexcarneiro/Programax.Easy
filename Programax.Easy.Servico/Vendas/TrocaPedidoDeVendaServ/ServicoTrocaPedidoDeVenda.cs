using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using System.Transactions;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;

namespace Programax.Easy.Servico.Vendas.TrocaPedidoDeVendaServ
{
    [Funcionalidade(EnumFuncionalidade.TROCAPEDIDOVENDA)]
    public class ServicoTrocaPedidoDeVenda : ServicoAkilSmallBusiness<TrocaPedidoDeVenda, ValidacaoTrocaPedidoDeVenda, ConversorTrocaPedidoDeVenda>
    {
        #region " VARIÁVEIS PRIVADAS "

        private IRepositorioTrocaPedidoDeVenda _repositorioTrocaPedidoDeVenda;

        #endregion

        #region " CONSTRUTOR "

        public ServicoTrocaPedidoDeVenda()
        {
            RetorneRepositorio();
        }

        public ServicoTrocaPedidoDeVenda(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<TrocaPedidoDeVenda> RetorneRepositorio()
        {
            if (_repositorioTrocaPedidoDeVenda == null)
            {
                _repositorioTrocaPedidoDeVenda = FabricaDeRepositorios.Crie<IRepositorioTrocaPedidoDeVenda>();
            }

            return _repositorioTrocaPedidoDeVenda;
        }

        #endregion

        #region " FECHAMENTO E FATURAMENTO "

        public void FecheTroca(TrocaPedidoDeVenda troca)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                if (troca.ListaItens.ToList().Exists(item => item.ItemEstahInconsistente))
                {
                    troca.Status = EnumStatusTrocaPedidoDeVenda.EMLIBERACAO;
                }
                else
                {
                    troca.Status = EnumStatusTrocaPedidoDeVenda.RESERVADO;
                }

                troca.DataFechamento = DateTime.Now.Date;

                if (troca.Id == 0)
                {
                    Cadastre(troca);
                }
                else
                {
                    Atualize(troca);
                }
                
                ServicoParametros servicoParametros = new ServicoParametros(false);
                var parametros = servicoParametros.ConsulteParametros();

                if (parametros.ParametrosVenda.TrabalharComEstoqueReservado)
                {
                    ReserveEstoque(troca);
                }

                scope.Complete();
            }
        }

        public void FatureTroca(TrocaPedidoDeVenda troca)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                troca.Status = EnumStatusTrocaPedidoDeVenda.FATURADO;

                if (troca.Id == 0)
                {
                    Cadastre(troca);
                }
                else
                {
                    Atualize(troca);
                }

                ServicoParametros servicoParametros = new ServicoParametros(false);
                var parametros = servicoParametros.ConsulteParametros();

                if (parametros.ParametrosVenda.TrabalharComEstoqueReservado)
                {
                    RemovaReservaEstoque(troca);
                }

                RemovaProdutosTrocadosDoEstoque(troca);
                
                IncluaProdutosDevolvidosNoEstoque(troca);
                MarqueQuantidadeProdutosDevolvidosNoPedidoDeVenda(troca);

                scope.Complete();
            }
        }

        private void MarqueQuantidadeProdutosDevolvidosNoPedidoDeVenda(TrocaPedidoDeVenda troca)
        {
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda(false, false);
            var pedido = servicoPedidoDeVenda.Consulte(troca.PedidoDeVenda.Id);

            List<ItemPedidoDeVenda> listaItensPedidoAuxiliar = new List<ItemPedidoDeVenda>();

            foreach (var item in troca.ListaItensPedido)
            {
                if (item.QuantidadeTrocar > 0)
                {
                    var itemPedido = pedido.ListaItens.FirstOrDefault(itemDoPedido => !listaItensPedidoAuxiliar.Exists(itemaux => itemaux.Id == itemDoPedido.Id) &&
                                                                                                                       itemDoPedido.Produto.Id == item.Produto.Id &&
                                                                                                                       itemDoPedido.Quantidade == item.Quantidade &&
                                                                                                                       itemDoPedido.ValorUnitario == item.ValorUnitario &&
                                                                                                                       itemDoPedido.ValorTotal == item.ValorTotal);

                    itemPedido.QuantidadeDevolvida += item.QuantidadeTrocar;

                    listaItensPedidoAuxiliar.Add(itemPedido);
                }
            }

            servicoPedidoDeVenda.Atualize(pedido);
        }

        public void CanceleTrocaPedidoDeVenda(int trocaPedidoVendaId)
        {
            var troca = Consulte(trocaPedidoVendaId);

            CanceleOuRecuseTrocaPedidoDeVenda(troca, EnumStatusTrocaPedidoDeVenda.CACNCELADO);
        }

        public void CanceleOuRecuseTrocaPedidoDeVenda(TrocaPedidoDeVenda trocaPedidoDeVenda, EnumStatusTrocaPedidoDeVenda statusTrocaPedidoDeVenda)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                if (trocaPedidoDeVenda.Status == EnumStatusTrocaPedidoDeVenda.EMLIBERACAO || trocaPedidoDeVenda.Status == EnumStatusTrocaPedidoDeVenda.RESERVADO)
                {
                    ServicoParametros servicoParametros = new ServicoParametros();

                    var parametros = servicoParametros.ConsulteParametros();

                    if (parametros.ParametrosVenda.PermiteBaixarEstoqueNaSaida)
                        RemovaReservaEstoque(trocaPedidoDeVenda);
                }
                else if (trocaPedidoDeVenda.Status == EnumStatusTrocaPedidoDeVenda.FATURADO)
                {
                    throw new Exception("Não é permitido cancelar uma troca após ser faturada");
                    //EstorneProdutosParaEstoque(trocaPedidoDeVenda);

                    //InativeContasAReceberDoPedido(trocaPedidoDeVenda);

                    //GereSaidaDeCaixaDoPedido(trocaPedidoDeVenda);
                }

                trocaPedidoDeVenda.Status = statusTrocaPedidoDeVenda;

                this.Atualize(trocaPedidoDeVenda);

                scope.Complete();
            }
        }

        #endregion

        #region " VALIDAÇÕES "

        public void ValideItemTroca(ItemTrocaPedidoDeVenda itemTrocaPedidoVenda)
        {
            ValidacaoItemTrocaPedidoDeVenda validacaoItemTrocaPedidoDeVenda = new ValidacaoItemTrocaPedidoDeVenda();

            validacaoItemTrocaPedidoDeVenda.ValideInclusao();

            validacaoItemTrocaPedidoDeVenda.Valide(itemTrocaPedidoVenda).AssegureSucesso();
        }

        public void ValideItemPedidoTroca(ItemPedidoTrocaPedidoDeVenda itemPedidoTrocaPedidoDeVenda, TrocaPedidoDeVenda trocaPedidoDeVenda)
        {
            ValidacaoItemPedidoTrocaPedidoDeVenda validacaoItemPedidoTrocaPedidoDeVenda = new ValidacaoItemPedidoTrocaPedidoDeVenda();

            validacaoItemPedidoTrocaPedidoDeVenda.TrocaPedidoDeVenda = trocaPedidoDeVenda;

            validacaoItemPedidoTrocaPedidoDeVenda.ValideInclusao();

            validacaoItemPedidoTrocaPedidoDeVenda.Valide(itemPedidoTrocaPedidoDeVenda).AssegureSucesso();
        }

        public void ValideItemTrocaLiberacao(ItemTrocaPedidoDeVenda itemTrocaPedidoVenda)
        {
            ValidacaoItemTrocaPedidoDeVenda validacaoItemTrocaPedidoDeVenda = new ValidacaoItemTrocaPedidoDeVenda();

            validacaoItemTrocaPedidoDeVenda.ValideItemLiberacao();

            validacaoItemTrocaPedidoDeVenda.Valide(itemTrocaPedidoVenda).AssegureSucesso();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void ReserveEstoque(TrocaPedidoDeVenda trocaPedidoDeVenda)
        {
            IRepositorioProduto repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            foreach (var item in trocaPedidoDeVenda.ListaItens)
            {
                if (item != null && item.Produto != null)
                {
                    var produto = repositorioProduto.Consulte(item.Produto.Id);

                    produto.FormacaoPreco.EstoqueReservado += item.Quantidade;

                    repositorioProduto.Atualize(produto);
                }
            }
        }

        private void RemovaReservaEstoque(TrocaPedidoDeVenda trocaPedidoDeVenda)
        {
            IRepositorioProduto repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            foreach (var item in trocaPedidoDeVenda.ListaItens)
            {
                if (item != null && item.Produto != null)
                {
                    var produto = repositorioProduto.Consulte(item.Produto.Id);

                    produto.FormacaoPreco.EstoqueReservado -= item.Quantidade;

                    repositorioProduto.Atualize(produto);
                }
            }
        }

        private void IncluaProdutosDevolvidosNoEstoque(TrocaPedidoDeVenda troca)
        {
            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao(false, false);

            MovimentacaoBase movimentacaoBase = new MovimentacaoBase();

            movimentacaoBase.DataCadastro = DateTime.Now;
            movimentacaoBase.DataMovimentacao = DateTime.Now;
            movimentacaoBase.FornecedorOuCliente = troca.PedidoDeVenda.Cliente;
            movimentacaoBase.OrigemMovimentacao = EnumOrigemMovimentacao.TROCADEMERCADORIA;

            movimentacaoBase.Observacoes = "Troca de Mercadoria Nr. Pedido: " + troca.PedidoDeVenda.Id;
            movimentacaoBase.TipoMovimentacao = EnumTipoMovimentacao.ENTRADA;

            foreach (var item in troca.ListaItensPedido)
            {
                ItemMovimentacao itemMovimentacao = new ItemMovimentacao();

                itemMovimentacao.Produto = item.Produto;
                itemMovimentacao.Quantidade = item.QuantidadeTrocar;

                movimentacaoBase.ListaDeItens.Add(itemMovimentacao);
            }

            servicoMovimentacao.Cadastre(movimentacaoBase);
        }

        private void IncluaProdutosEstornadosParaEstoque(TrocaPedidoDeVenda troca)
        {
            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao(false, false);

            MovimentacaoBase movimentacaoBase = new MovimentacaoBase();

            movimentacaoBase.DataCadastro = DateTime.Now;
            movimentacaoBase.DataMovimentacao = DateTime.Now;
            movimentacaoBase.FornecedorOuCliente = troca.PedidoDeVenda.Cliente;
            movimentacaoBase.OrigemMovimentacao = EnumOrigemMovimentacao.TROCADEMERCADORIA;

            movimentacaoBase.Observacoes = "Estorno de Troca de Mercadoria Nr. Pedido: " + troca.PedidoDeVenda.Id;
            movimentacaoBase.TipoMovimentacao = EnumTipoMovimentacao.ENTRADA;

            foreach (var item in troca.ListaItens)
            {
                ItemMovimentacao itemMovimentacao = new ItemMovimentacao();

                itemMovimentacao.Produto = item.Produto;
                itemMovimentacao.Quantidade = item.Quantidade;

                movimentacaoBase.ListaDeItens.Add(itemMovimentacao);
            }

            servicoMovimentacao.Cadastre(movimentacaoBase);
        }

        private void RemovaProdutosEstornadosDoEstoque(TrocaPedidoDeVenda troca)
        {
            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao(false, false);

            MovimentacaoBase movimentacaoBase = new MovimentacaoBase();

            movimentacaoBase.DataCadastro = DateTime.Now;
            movimentacaoBase.DataMovimentacao = DateTime.Now;
            movimentacaoBase.FornecedorOuCliente = troca.PedidoDeVenda.Cliente;
            movimentacaoBase.OrigemMovimentacao = EnumOrigemMovimentacao.TROCADEMERCADORIA;

            movimentacaoBase.Observacoes = " Estorno de Troca de Mercadoria Nr. Pedido: " + troca.PedidoDeVenda.Id;
            movimentacaoBase.TipoMovimentacao = EnumTipoMovimentacao.SAIDA;

            foreach (var item in troca.ListaItensPedido)
            {
                ItemMovimentacao itemMovimentacao = new ItemMovimentacao();

                itemMovimentacao.Produto = item.Produto;
                itemMovimentacao.Quantidade = item.QuantidadeTrocar;

                movimentacaoBase.ListaDeItens.Add(itemMovimentacao);
            }

            if (movimentacaoBase.ListaDeItens.Count == 0) return;

            servicoMovimentacao.Cadastre(movimentacaoBase);
        }

        private void RemovaQuantidadeTrocada(TrocaPedidoDeVenda troca)
        {
            ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

            var pedido = servicoPedido.Consulte(troca.PedidoDeVenda.Id);

            foreach (var item in pedido.ListaItens)
            {
                if (item.QuantidadeDevolvida > 0)
                    if (troca.ListaItensPedido.FirstOrDefault(x => x.Produto.Id == item.Produto.Id) != null)
                    {
                        item.QuantidadeDevolvida--;
                        servicoPedido.Atualize(pedido);
                    }
            }
        }

        private void RemovaProdutosTrocadosDoEstoque(TrocaPedidoDeVenda troca)
        {
            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao(false, false);

            MovimentacaoBase movimentacaoBase = new MovimentacaoBase();

            movimentacaoBase.DataCadastro = DateTime.Now;
            movimentacaoBase.DataMovimentacao = DateTime.Now;
            movimentacaoBase.FornecedorOuCliente = troca.PedidoDeVenda.Cliente;
            movimentacaoBase.OrigemMovimentacao = EnumOrigemMovimentacao.TROCADEMERCADORIA;

            movimentacaoBase.Observacoes = "Troca de Mercadoria Nr. Pedido: " + troca.PedidoDeVenda.Id;
            movimentacaoBase.TipoMovimentacao = EnumTipoMovimentacao.SAIDA;

            foreach (var item in troca.ListaItens)
            {
                ItemMovimentacao itemMovimentacao = new ItemMovimentacao();

                itemMovimentacao.Produto = item.Produto;
                itemMovimentacao.Quantidade = item.Quantidade;

                movimentacaoBase.ListaDeItens.Add(itemMovimentacao);
            }

            if (movimentacaoBase.ListaDeItens.Count == 0) return;

            servicoMovimentacao.Cadastre(movimentacaoBase);
        }

        public void VolteTrocaDeVendaParaReservado(int trocaId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var troca = Consulte(trocaId);

                if (troca.Status == EnumStatusTrocaPedidoDeVenda.FATURADO)
                {
                    troca.Status = EnumStatusTrocaPedidoDeVenda.RESERVADO;

                    IncluaProdutosEstornadosParaEstoque(troca);
                    RemovaProdutosEstornadosDoEstoque(troca);
                    RemovaQuantidadeTrocada(troca);

                    AtualizeEstornoDeTroca(troca);

                    scope.Complete();                    
                }
            }
        }

        #endregion

        public List<TrocaPedidoDeVenda> ConsulteLista(DateTime? dataInicial, DateTime? dataFinal, PedidoDeVenda pedidoDeVenda, EnumStatusTrocaPedidoDeVenda? statusTrocaPedidoDeVenda)
        {
            return _repositorioTrocaPedidoDeVenda.ConsulteLista(dataInicial, dataFinal, pedidoDeVenda, statusTrocaPedidoDeVenda);
        }

        public void AtualizeEstornoDeTroca(TrocaPedidoDeVenda troca)
        {
            _repositorioTrocaPedidoDeVenda.AtualizeEstornoDeTroca(troca);
        }
    }
}
