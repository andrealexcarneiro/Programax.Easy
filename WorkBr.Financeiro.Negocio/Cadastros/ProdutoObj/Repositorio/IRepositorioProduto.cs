using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio
{
    public interface IRepositorioProduto : IRepositorioBase<Produto>
    {
        Produto ConsulteProdutoAtivo(int id);

        Produto ConsulteProdutoAtivoPeloCodigoDeBarras(string codigoDeBarras);

        List<Produto> ConsulteListaAtivaPelaDescricao(string descricao);

        List<Produto> ConsulteListaAtivaQueContemCodigoFabricante(string codigoFabricante);

        List<Produto> ConsulteListaPelaDescricao(string descricao, string status, Marca marca, Categoria categoria, Grupo grupo, Cor cor, Tamanho tamanho, EnumSexoProduto? sexo);

        Produto ConsultePeloCodigoDeBarras(string codigoDeBarras, int idDoProdutoEmExcessao);

        FornecedorProduto ConsulteProdutoFornecedorPeloCodigo(string codigoProdutoFornecedor);

        List<Produto> ConsulteListasPeloCodigo(int codigo, string status, Marca marca, Categoria categoria, Grupo grupo, Cor cor, Tamanho tamanho, EnumSexoProduto? sexo);

        Produto ConsulteProdutoPeloCodigoFornecedor(string codigoFornecedor, Pessoa fornecedor);

        List<Produto> ConsulteListaProdutosAtivos(Marca marca, Categoria categoria, Grupo grupo, SubGrupo subGrupo, EnumFiltroSituacao? filtroSituacaoSaldo);

        List<Produto> ConsulteListaQueContemCodigoBarras(string codigoDeBarras, string status, Marca marca, Categoria categoria, Grupo grupo, Cor cor, Tamanho tamanho, EnumSexoProduto? sexo);

        List<Produto> ConsulteListaQueContemSerialNumber(string serialNumber, string status, Marca marca, Categoria categoria, Grupo grupo, Cor cor, Tamanho tamanho, EnumSexoProduto? sexo);

        List<Produto> ConsulteListaAtivaQueContemDescricaoMarca(string descricao);

        List<Produto> ConsulteListaAtivaQueContemDescricaoFabricante(string descricao);

        Produto ConsulteProdutoSerialNumberAtivo(string serialNumber);

        List<VWProduto> ConsulteLista(Categoria categoria, Grupo grupo, SubGrupo subGrupo, Marca marca, FabricanteObj.ObjetoDeNegocio.Fabricante fabricante, TamanhoObj.ObjetoDeNegocio.Tamanho tamanho, bool itensComEstoqueMinimo, int? itemComDDVAbaixoDe, EnumOrdenacaoPesquisaProduto ordenacaoPesquisaProduto, int? estoqueMaiorQue = null);

        List<VWProdutoPdv> ConsulteListaVwProdutosPdvCodigoBarrasDescricao(string descricaoCodigoBarras);

        Produto ConsulteProdutoPeloCodigoFornecedorExcetoParaOProduto(string codigoFornecedor, Pessoa fornecedor, int produtoId);

        Produto ConsulteProdutoPeloCodigoGtin(string codigoGtin);

        List<Produto> ConsulteListaProdutosAtivos(string descricao, Marca marca, FabricanteObj.ObjetoDeNegocio.Fabricante fabricante, Categoria categoria, Grupo grupo, SubGrupo subGrupo);
        List<Produto> ConsulteListaProdutosAtivosII(int codigo, Marca marca, FabricanteObj.ObjetoDeNegocio.Fabricante fabricante, Categoria categoria, Grupo grupo, SubGrupo subGrupo);

        List<Produto> ConsulteListaPorId(List<int> listaIds);

        Produto ConsulteComJoinGrupoTributacaoETributacoes(int p);
    }
}
