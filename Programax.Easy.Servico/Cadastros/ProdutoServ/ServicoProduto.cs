using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using System.Transactions;
using System;

namespace Programax.Easy.Servico.Cadastros.ProdutoServ
{
    [Funcionalidade(EnumFuncionalidade.ITENS)]
    public class ServicoProduto : ServicoAkilSmallBusiness<Produto, ValidacaoProduto, ConversorProduto>
    {
        IRepositorioProduto _repositorioProduto;

        public ServicoProduto()
        {
            RetorneRepositorio();
        }

        public ServicoProduto(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Produto> RetorneRepositorio()
        {
            if (_repositorioProduto == null)
            {
                _repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();
            }

            return _repositorioProduto;
        }

        public double CalculeFreteProduto(double TotalEntrada, double TotalFreteEntrada, EnumTipoFrete tipoFrete)
        {
            return CalculosFormacaoPrecoProduto.CalculeFreteProduto(TotalEntrada, TotalFreteEntrada, tipoFrete);
        }

        public double CalculePrecoCusto(double precoCompra,
                                                       double valorFreteCompra,
                                                       double percentualIcms,
                                                       double percentualIpi,
                                                       double percentualIcmsSt,
                                                       double percentualReducaoIcms)
        {
            return CalculosFormacaoPrecoProduto.CalculePrecoCusto(precoCompra,
                                                                                              valorFreteCompra,
                                                                                              percentualIcms,
                                                                                              percentualIpi,
                                                                                              percentualIcmsSt,
                                                                                              percentualReducaoIcms);
        }

        public double CalculePrecoVenda(double precoCusto, double markup)
        {
            return CalculosFormacaoPrecoProduto.CalculePrecoVenda(precoCusto, markup);
        }

        public double CalculePrecoVenda(double precoCusto,
                                                        double percentualDespesasFixas,
                                                        double percentualDespesasVariaveis,
                                                        double percentualImpostos,
                                                        double percentualOutrasDespesas,
                                                        double percentualFrete,
                                                        double percentualComissoes,
                                                        double percentualLucro)
        {
            return CalculosFormacaoPrecoProduto.CalculePrecoVenda(precoCusto,
                                                                                               percentualDespesasFixas,
                                                                                               percentualDespesasVariaveis,
                                                                                               percentualImpostos,
                                                                                               percentualOutrasDespesas,
                                                                                               percentualFrete,
                                                                                               percentualComissoes,
                                                                                               percentualLucro);
        }

        public Produto ConsulteProdutoAtivo(int id)
        {
            return _repositorioProduto.ConsulteProdutoAtivo(id);
        }

        public Produto ConsulteProdutoSerialNumberAtivo(string serialNumber)
        {
            return _repositorioProduto.ConsulteProdutoSerialNumberAtivo(serialNumber);
        }

        public Produto ConsulteProdutoPeloCodigoFornecedor(string codigoFornecedor, Pessoa fornecedor)
        {
            return _repositorioProduto.ConsulteProdutoPeloCodigoFornecedor(codigoFornecedor, fornecedor);
        }

        public Produto ConsulteProdutoAtivoPeloCodigoDeBarras(string codigoDeBarras)
        {
            return _repositorioProduto.ConsulteProdutoAtivoPeloCodigoDeBarras(codigoDeBarras);
        }

        public List<Produto> ConsulteListasPelaDescricao(string descricao, string status, Marca marca, Categoria categoria, Grupo grupo, Cor cor, Tamanho tamanho, EnumSexoProduto? sexo)
        {
            return _repositorioProduto.ConsulteListaPelaDescricao(descricao, status, marca, categoria, grupo, cor, tamanho, sexo);
        }

        public List<Produto> ConsulteListaQueContemCodigoBarras(string codigoDeBarras, string status, Marca marca, Categoria categoria, Grupo grupo, Cor cor, Tamanho tamanho, EnumSexoProduto? sexo)
        {
            return _repositorioProduto.ConsulteListaQueContemCodigoBarras(codigoDeBarras, status, marca, categoria, grupo, cor, tamanho, sexo);
        }

        public List<Produto> ConsulteListaQueContemSerialNumber(string serialNumber, string status, Marca marca, Categoria categoria, Grupo grupo, Cor cor, Tamanho tamanho, EnumSexoProduto? sexo)
        {
            return _repositorioProduto.ConsulteListaQueContemSerialNumber(serialNumber, status, marca, categoria, grupo, cor, tamanho, sexo);
        }

        public List<Produto> ConsulteListasProdutosAtivos(Marca marca, Categoria categoria, Grupo grupo, SubGrupo subGrupo, EnumFiltroSituacao? filtroSituacaoSaldo)
        {
            return _repositorioProduto.ConsulteListaProdutosAtivos(marca, categoria, grupo, subGrupo, filtroSituacaoSaldo);
        }

        public List<Produto> ConsulteListasProdutosAtivos(string descricao, Marca marca, Fabricante fabricante, Categoria categoria, Grupo grupo, SubGrupo subGrupo)
        {
            return _repositorioProduto.ConsulteListaProdutosAtivos(descricao, marca, fabricante, categoria, grupo, subGrupo);
        }
        public List<Produto> ConsulteListasProdutosAtivosII(int codigo, Marca marca, Fabricante fabricante, Categoria categoria, Grupo grupo, SubGrupo subGrupo)
        {
            return _repositorioProduto.ConsulteListaProdutosAtivosII(codigo, marca, fabricante, categoria, grupo, subGrupo);
        }

        public List<Produto> ConsulteListaAtivaPelaDescricao(string descricao)
        {
            return _repositorioProduto.ConsulteListaAtivaPelaDescricao(descricao);
        }

        public List<Produto> ConsulteListaAtivaQueContemCodigoFabricante(string codigoFabricante)
        {
            return _repositorioProduto.ConsulteListaAtivaQueContemCodigoFabricante(codigoFabricante);
        }

        public List<Produto> ConsulteListaAtivaQueContemDescricaoMarca(string descricao)
        {
            return _repositorioProduto.ConsulteListaAtivaQueContemDescricaoMarca(descricao);
        }

        public List<Produto> ConsulteListaAtivaQueContemDescricaoFabricante(string descricao)
        {
            return _repositorioProduto.ConsulteListaAtivaQueContemDescricaoFabricante(descricao);
        }

        public List<Produto> ConsulteListaPorId(List<int> listaIds)
        {
            return _repositorioProduto.ConsulteListaPorId(listaIds);
        }

        public List<VWProduto> ConsulteLista(Categoria categoria, Grupo grupo, SubGrupo subGrupo, Marca marca, Fabricante fabricante, Tamanho tamanho, bool itensComEstoqueMinimo, int? itemComDDVAbaixoDe, EnumOrdenacaoPesquisaProduto ordenacaoPesquisaProduto, int? estoqueMaiorQue=null)
        {
            return _repositorioProduto.ConsulteLista(categoria, grupo, subGrupo, marca, fabricante, tamanho, itensComEstoqueMinimo, itemComDDVAbaixoDe, ordenacaoPesquisaProduto, estoqueMaiorQue);
        }

        public List<VWProdutoPdv> ConsulteListaVwProdutosPdvCodigoBarrasDescricao(string descricaoCodigoBarras)
        {
            return _repositorioProduto.ConsulteListaVwProdutosPdvCodigoBarrasDescricao(descricaoCodigoBarras);
        }

        public Produto ConsulteProdutoPeloCodigoGtin(string codigoGtin)
        {
            return _repositorioProduto.ConsulteProdutoPeloCodigoGtin(codigoGtin);
        }

        public FornecedorProduto ConsulteProdutoFornecedorPeloCodigo(string codigoProdutoFornecedor)
        {
            return _repositorioProduto.ConsulteProdutoFornecedorPeloCodigo(codigoProdutoFornecedor);
        }

        public List<Produto> ConsulteListasPeloCodigo(int codigo, string status, Marca marca, Categoria categoria, Grupo grupo, Cor cor, Tamanho tamanho, EnumSexoProduto? sexo)
        {
            return _repositorioProduto.ConsulteListasPeloCodigo(codigo, status, marca, categoria, grupo, cor, tamanho, sexo);
        }

        public void AtualizePrecoVendaProdutos(List<Produto> listaProdutos)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.Serializable, Timeout = new TimeSpan(0, 5, 0) }))
            {
                foreach (var produto in listaProdutos)
                {
                    var produtoBase = Consulte(produto.Id);

                    produtoBase.FormacaoPreco = produtoBase.FormacaoPreco ?? new FormacaoPrecoProduto();
                    produto.FormacaoPreco = produto.FormacaoPreco ?? new FormacaoPrecoProduto();

                    produtoBase.FormacaoPreco.ValorVenda = produto.FormacaoPreco.ValorVenda;
                    produtoBase.FormacaoPreco.ValorPromocao = produto.FormacaoPreco.ValorPromocao;
                    produtoBase.FormacaoPreco.EhPromocao = produto.FormacaoPreco.EhPromocao;

                    _repositorioProduto.Atualize(produtoBase);
                }

                scope.Complete();
            }
        }
    }
}