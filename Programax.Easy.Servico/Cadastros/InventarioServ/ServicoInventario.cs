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
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.InventarioServ
{
    [Funcionalidade(EnumFuncionalidade.INVENTARIO)]
    public class ServicoInventario : ServicoAkilSmallBusiness<Inventario, ValidacaoInventario, ConversorInventario>
    {
        #region " VARIÁVEIS PRIVADAS "

        private IRepositorioInventario _repositorioInventario;
        private ServicoMovimentacao _servicoMovimentacao;

        #endregion

        #region " CONSTRUTOR "

        public ServicoInventario()
        {
            _servicoMovimentacao = new ServicoMovimentacao(false, false);

            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<Inventario> RetorneRepositorio()
        {
            if (_repositorioInventario == null)
            {
                _repositorioInventario = FabricaDeRepositorios.Crie<IRepositorioInventario>();
            }

            return _repositorioInventario;
        }

        public override int Cadastre(Inventario objetoDeNegocio)
        {
            objetoDeNegocio.DataInicio = DateTime.Now.Date;

            return base.Cadastre(objetoDeNegocio);
        }

        #endregion

        #region " CONSULTAS "

        public List<Inventario> ConsulteLista(DateTime? dataInicio, EnumStatusInventario? statusInventario, int? contagem, Marca marca, Categoria categoria, Grupo grupo, SubGrupo subGrupo)
        {
            return _repositorioInventario.ConsulteLista(dataInicio, statusInventario, contagem, marca, categoria, grupo, subGrupo);
        }

        #endregion

        #region " EMISSÃO DE CONTAGEM E CONSOLIDAÇÃO "

        public void EmitaPrimeiraContagem(Inventario inventario)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                inventario.ContagemAtual = 1;
                inventario.DataInicioPrimeiraContagem = DateTime.Now;

                InsiraItensInventario(inventario);

                this.Cadastre(inventario);

                if (inventario.BloquearProdutosMovimentacao || inventario.UtilizarSaldoPrimeiraContagem)
                {
                    BloqueieEstoqueOuUtilizeEstoqueNaPrimeiraContagemDosItens(inventario);
                }

                scope.Complete();
            }
        }

        public void EmitaSegundaContagem(Inventario inventario)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                inventario.ContagemAtual = 2;

                if (inventario.UtilizarSaldoPrimeiraContagem && inventario.Id == 0)
                {
                    inventario.DataInicioPrimeiraContagem = DateTime.Now;
                    InsiraItensInventario(inventario);

                    InformeQuantidadeEstoque(inventario);

                    if (inventario.BloquearProdutosMovimentacao || inventario.UtilizarSaldoPrimeiraContagem)
                    {
                        BloqueieEstoqueOuUtilizeEstoqueNaPrimeiraContagemDosItens(inventario);
                    }
                }

                inventario.DataInicioSegundaContagem = DateTime.Now;

                if (inventario.Id == 0)
                {
                    base.Cadastre(inventario);
                }
                else
                {
                    base.Atualize(inventario);
                }

                scope.Complete();
            }
        }

        public void EmitaTerceiraContagem(Inventario inventario)
        {
            inventario.ContagemAtual = 3;
            inventario.DataInicioTerceiraContagem = DateTime.Now;

            base.Atualize(inventario);
        }

        public void ConsolideInventario(Inventario inventario)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                inventario.Status = EnumStatusInventario.CONSOLIDADO;
                inventario.DataFinal = DateTime.Now;

                base.Atualize(inventario);

                DesbloqueieEConsolideQuantidadeItens(inventario);

                GereMovimentacaoItens(inventario);

                scope.Complete();
            }
        }

        private void GereMovimentacaoItens(Inventario inventario)
        {
            MovimentacaoBase movimentacao = new MovimentacaoBase();

            movimentacao.DataCadastro = DateTime.Now;
            movimentacao.DataMovimentacao = inventario.DataInicio;
            movimentacao.OrigemMovimentacao = EnumOrigemMovimentacao.INVENTARIO;
            movimentacao.TipoMovimentacao = EnumTipoMovimentacao.AMBOS;
            movimentacao.Observacoes = "Código do Inventário: " + inventario.Id;

            foreach (var item in inventario.ListaDeItens)
            {
                ItemMovimentacao itemMovimentacao = new ItemMovimentacao();

                double quantidadeContagem = RetorneQuantidadeParaConsolidar(item);

                var diferencaEstoque = item.Produto.FormacaoPreco.Estoque - quantidadeContagem;

                if (diferencaEstoque > 0)
                {
                    itemMovimentacao.TipoMovimentacao = EnumTipoMovimentacao.SAIDA;
                }
                else
                {
                    itemMovimentacao.TipoMovimentacao = EnumTipoMovimentacao.ENTRADA;
                }

                itemMovimentacao.Produto = item.Produto;
                itemMovimentacao.Quantidade = Math.Abs(diferencaEstoque);

                movimentacao.ListaDeItens.Add(itemMovimentacao);
            }

            _servicoMovimentacao.Cadastre(movimentacao);
        }

        #endregion

        #region " ATUALIZE ITEM "

        public void AtualizeItem(ItemInventario itemInventario)
        {
            ServicoItemInventario servicoItemInventario = new ServicoItemInventario(false, false);
            servicoItemInventario.Atualize(itemInventario);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void InsiraItensInventario(Inventario inventario)
        {
            IRepositorioProduto repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            var listaDeProdutos = repositorioProduto.ConsulteListaProdutosAtivos(inventario.Marca, inventario.Categoria, inventario.Grupo, inventario.SubGrupo, inventario.FiltroSituacaoSaldo);

            switch (inventario.OrdenacaoContagem)
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

            List<ItemInventario> listaItensInventarios = new List<ItemInventario>();

            foreach (var produto in listaDeProdutos)
            {
                ItemInventario itemInventario = new ItemInventario();

                itemInventario.Produto = produto;

                itemInventario.QuantidadeEstoque = produto.FormacaoPreco.Estoque;

                listaItensInventarios.Add(itemInventario);
            }

            inventario.ListaDeItens = listaItensInventarios;
        }

        private void InformeQuantidadeEstoque(Inventario inventario)
        {
            foreach (var item in inventario.ListaDeItens)
            {
                item.QuantidadeEstoque = item.Produto.FormacaoPreco.Estoque;
            }
        }

        private void BloqueieEstoqueOuUtilizeEstoqueNaPrimeiraContagemDosItens(Inventario inventario)
        {
            var repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            var listaProdutos = RetorneDiretoDoRepositorioOsProdutos(inventario);

            foreach (var item in inventario.ListaDeItens)
            {
                var produto = listaProdutos.FirstOrDefault(prod => prod.Id == item.Produto.Id);

                if (inventario.BloquearProdutosMovimentacao)
                {
                    produto.DadosGerais.ProdutoEmInventario = true;
                }

                if (inventario.UtilizarSaldoPrimeiraContagem)
                {
                    item.QuantidadeContagemUm = produto.FormacaoPreco.Estoque;
                }
            }

            if (inventario.BloquearProdutosMovimentacao)
            {
                repositorioProduto.AtualizeLista(listaProdutos);
            }
        }

        private void DesbloqueieEConsolideQuantidadeItens(Inventario inventario)
        {
            var repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            var listaProdutos = RetorneDiretoDoRepositorioOsProdutos(inventario);

            foreach (var produto in listaProdutos)
            {
                produto.DadosGerais.ProdutoEmInventario = false;
                
                //Deve-se atualizar o estoque reservado com zero, para que o estoque disponível fique correto
                produto.FormacaoPreco.EstoqueReservado = 0;
            }

            repositorioProduto.AtualizeLista(listaProdutos);
        }

        private List<Produto> RetorneDiretoDoRepositorioOsProdutos(Inventario inventario)
        {
            var repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            List<int> listaIdsProdutos = new List<int>();

            foreach (var item in inventario.ListaDeItens)
            {
                listaIdsProdutos.Add(item.Produto.Id);
            }

            return repositorioProduto.ConsulteLista(listaIdsProdutos);
        }

        private double RetorneQuantidadeParaConsolidar(ItemInventario itemInventario)
        {
            if (itemInventario.QuantidadeContagemTres != null)
            {
                return itemInventario.QuantidadeContagemTres.GetValueOrDefault();
            }

            if (itemInventario.QuantidadeContagemDois != null)
            {
                return itemInventario.QuantidadeContagemDois.GetValueOrDefault();
            }

            return itemInventario.QuantidadeContagemUm.GetValueOrDefault();
        }

        #endregion
    }
}
