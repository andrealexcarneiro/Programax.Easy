using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.TransferenciaServ
{
    [Funcionalidade(EnumFuncionalidade.TRANSFERENCIASUBESTOQUE)]
    public class ServicoTransferencia : ServicoAkilSmallBusiness<Transferencia, ValidacaoTransferencia, ConversorTransferencia>
    {
        #region " VARIÁVEIS PRIVADAS "

        private IRepositorioTransferencia _repositorioTransferencia;
        private ServicoMovimentacao _servicoMovimentacao;

        #endregion

        #region " CONSTRUTOR "

        public ServicoTransferencia()
        {
            _servicoMovimentacao = new ServicoMovimentacao(false, false);

            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<Transferencia> RetorneRepositorio()
        {
            if (_repositorioTransferencia == null)
            {
                _repositorioTransferencia = FabricaDeRepositorios.Crie<IRepositorioTransferencia>();
            }

            return _repositorioTransferencia;
        }

        public override int Cadastre(Transferencia objetoDeNegocio)
        {
            objetoDeNegocio.DataInicio = DateTime.Now.Date;

            return base.Cadastre(objetoDeNegocio);
        }

        #endregion

        #region " CONSULTAS "

        public List<Transferencia> ConsulteLista(DateTime? dataInicio, EnumStatusInventario? statusInventario)
        {
            return _repositorioTransferencia.ConsulteLista(dataInicio, statusInventario);
        }

        #endregion


        public void ConsolideInventario(Transferencia transferencia)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                transferencia.Status = EnumStatusInventario.CONSOLIDADO;
                transferencia.DataFinal = DateTime.Now;

                base.Atualize(transferencia);

                DesbloqueieEConsolideQuantidadeItens(transferencia);

                GereMovimentacaoItens(transferencia);

                scope.Complete();
            }
        }

        private void GereMovimentacaoItens(Transferencia Transferencia)
        {
            MovimentacaoBase movimentacao = new MovimentacaoBase();

            movimentacao.DataCadastro = DateTime.Now;
            movimentacao.DataMovimentacao = Transferencia.DataInicio;
            movimentacao.OrigemMovimentacao = EnumOrigemMovimentacao.INVENTARIO;
            movimentacao.TipoMovimentacao = EnumTipoMovimentacao.AMBOS;
            movimentacao.Observacoes = "Código do Inventário: " + Transferencia.Id;

            //foreach (var item in Transferencia.ListaDeItens)
            //{
            //    ItemMovimentacao itemMovimentacao = new ItemMovimentacao();

            //    double quantidadeContagem = RetorneQuantidadeParaConsolidar(item);

            //    var diferencaEstoque = item.Produto.FormacaoPreco.Estoque - quantidadeContagem;

            //    if (diferencaEstoque > 0)
            //    {
            //        itemMovimentacao.TipoMovimentacao = EnumTipoMovimentacao.SAIDA;
            //    }
            //    else
            //    {
            //        itemMovimentacao.TipoMovimentacao = EnumTipoMovimentacao.ENTRADA;
            //    }

            //    itemMovimentacao.Produto = item.Produto;
            //    itemMovimentacao.Quantidade = Math.Abs(diferencaEstoque);

            //    movimentacao.ListaDeItens.Add(itemMovimentacao);
            //}

            _servicoMovimentacao.Cadastre(movimentacao);
        }

        

        #region " ATUALIZE ITEM "

        public void AtualizeItem(ItemTransferencia itemTransferencia)
        {
            ServicoItemTransferencia servicoItemTransferencia = new ServicoItemTransferencia(false, false);
            servicoItemTransferencia.Atualize(itemTransferencia);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void InsiraItensInventario(Transferencia transferencia)
        {
            IRepositorioProduto repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            var listaDeProdutos = repositorioProduto.ConsulteListaProdutosAtivos(transferencia.Marca, transferencia.Categoria, transferencia.Grupo, transferencia.SubGrupo, transferencia.FiltroSituacaoSaldo);

            switch (transferencia.OrdenacaoContagem)
            {
                case EnumFiltroOrdenacaoContagem.PELOCODIGO:

                    listaDeProdutos = listaDeProdutos.OrderBy(x => x.Id).ToList();

                    break;
                case EnumFiltroOrdenacaoContagem.PELADESCRICAODOPRODUTO:

                    listaDeProdutos = listaDeProdutos.OrderBy(x => x.DadosGerais.Descricao).ToList();

                    break;
                case EnumFiltroOrdenacaoContagem.MARCADOPRODUTO:

                    listaDeProdutos = listaDeProdutos.OrderBy(x => x.Principal.Marca != null ? x.Principal.Marca.Descricao : "z").ThenBy(x => x.DadosGerais.Descricao).ToList();

                    break;
                case EnumFiltroOrdenacaoContagem.CATEGORIADOPRODUTO:

                    listaDeProdutos = listaDeProdutos.OrderBy(x => x.Principal.Categoria != null ? x.Principal.Categoria.Descricao : "z").ThenBy(x => x.DadosGerais.Descricao).ToList();

                    break;
            }

            List<ItemTransferencia> listaItensTransferencias = new List<ItemTransferencia>();

            foreach (var produto in listaDeProdutos)
            {
                ItemTransferencia itemTransferencia = new ItemTransferencia();

                //itemTransferencia.Produto = produto;

                itemTransferencia.QuantidadeEstoque = produto.FormacaoPreco.Estoque;

                listaItensTransferencias.Add(itemTransferencia);
            }

           /// transferencia.ListaDeItens = listaItensTransferencias;
        }

        private void InformeQuantidadeEstoque(Transferencia transferencia)
        {
            //foreach (var item in transferencia.ListaDeItens)
            //{
            //    item.QuantidadeEstoque = item.Produto.FormacaoPreco.Estoque;
            //}
        }

      
        private void DesbloqueieEConsolideQuantidadeItens(Transferencia transferencia)
        {
            var repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            var listaProdutos = RetorneDiretoDoRepositorioOsProdutos(transferencia);

            foreach (var produto in listaProdutos)
            {
                produto.DadosGerais.ProdutoEmInventario = false;
                
                //Deve-se atualizar o estoque reservado com zero, para que o estoque disponível fique correto
                produto.FormacaoPreco.EstoqueReservado = 0;
            }

            repositorioProduto.AtualizeLista(listaProdutos);
        }

        private List<Produto> RetorneDiretoDoRepositorioOsProdutos(Transferencia transferencia)
        {
            var repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            List<int> listaIdsProdutos = new List<int>();

            //foreach (var item in transferencia.ListaDeItens)
            //{
            //    listaIdsProdutos.Add(item.Produto.Id);
            //}

            return repositorioProduto.ConsulteLista(listaIdsProdutos);
        }

        //private double RetorneQuantidadeParaConsolidar(ItemTransferencia itemTransferencia)
        //{
        //    //if (itemTransferencia.QuantidadeContagemTres != null)
        //    //{
        //    //    return itemTransferencia.QuantidadeContagemTres.GetValueOrDefault();
        //    //}

        //    //if (itemTransferencia.QuantidadeContagemDois != null)
        //    //{
        //    //    return itemTransferencia.QuantidadeContagemDois.GetValueOrDefault();
        //    //}

        //    //return itemTransferencia.QuantidadeContagemUm.GetValueOrDefault();
        //}

        #endregion
    }
}
