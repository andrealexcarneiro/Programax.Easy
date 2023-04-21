using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.ProdutoServ
{
    public class RepositorioProduto : RepositorioBase<Produto>, IRepositorioProduto
    {
        #region " VARIÁVEIS PRIVADAS "

        private Marca _marcaDeConsulta;
        private UnidadeMedida _unidadeMedidaDaConsulta;

        #endregion

        #region " CONSTRUTOR "

        public RepositorioProduto(ISession sessao)
            : base(sessao)
        {

        }

        #endregion

        #region " CONSULTAS DE UM ÚNICO REGISTRO "

        public Produto ConsulteProdutoAtivo(int id)
        {
            return _session.QueryOver<Produto>().Where(produto => produto.Id == id && produto.DadosGerais.Status == "A").SingleOrDefault();
        }

        public Produto ConsulteProdutoAtivoPeloCodigoDeBarras(string codigoDeBarras)
        {
            return _session.QueryOver<Produto>().Where(produto => produto.DadosGerais.CodigoDeBarras != null &&
                                                                                                  produto.DadosGerais.CodigoDeBarras == codigoDeBarras &&
                                                                                                  produto.DadosGerais.Status == "A")
                                                                                                  .Take(1).SingleOrDefault();
        }

        public Produto ConsultePeloCodigoDeBarras(string codigoDeBarras, int idDoProdutoEmExcessao)
        {
            return _session.QueryOver<Produto>().Where(produto => produto.DadosGerais.CodigoDeBarras == codigoDeBarras && produto.Id != idDoProdutoEmExcessao)
                                                                    .Take(1).SingleOrDefault();
        }

        public Produto ConsulteProdutoPeloCodigoFornecedor(string codigoFornecedor, Pessoa fornecedor)
        {
            FornecedorProduto fornecedorProduto = null;
            Pessoa fornecedorConsulta = null;

            return _session.QueryOver<Produto>()
                                               .Left.JoinAlias(produto => produto.ListaFornecedores, () => fornecedorProduto)
                                               .Left.JoinAlias(produto => fornecedorProduto.Fornecedor, () => fornecedorConsulta)
                                               .Where(produto => fornecedorProduto.CodigoProduto == codigoFornecedor && fornecedorConsulta.Id == fornecedor.Id).Take(1).SingleOrDefault();
        }

        public Produto ConsulteProdutoPeloCodigoFornecedorExcetoParaOProduto(string codigoFornecedor, Pessoa fornecedor, int produtoId)
        {
            FornecedorProduto fornecedorProduto = null;
            Pessoa fornecedorConsulta = null;

            return _session.QueryOver<Produto>()
                                               .Left.JoinAlias(produto => produto.ListaFornecedores, () => fornecedorProduto)
                                               .Left.JoinAlias(produto => fornecedorProduto.Fornecedor, () => fornecedorConsulta)
                                               .Where(produto => fornecedorProduto.CodigoProduto == codigoFornecedor && fornecedorConsulta.Id == fornecedor.Id && produto.Id != produtoId).Take(1).SingleOrDefault();
        }

        public Produto ConsulteProdutoSerialNumberAtivo(string serialNumber)
        {
            return _session.QueryOver<Produto>().Where(produto => produto.DadosGerais.Status == "A" && produto.Principal.CodigoFabricante == serialNumber).Take(1).SingleOrDefault();
        }

        public Produto ConsulteProdutoPeloCodigoGtin(string codigoGtin)
        {
            return _session.QueryOver<Produto>().Where(produto => produto.ContabilFiscal.CodigoGtin == codigoGtin).Take(1).SingleOrDefault();
        }

        public FornecedorProduto ConsulteProdutoFornecedorPeloCodigo(string codigoProdutoFornecedor)
        {
            return _session.QueryOver<FornecedorProduto>().Where(fornecedorProduto => fornecedorProduto.CodigoProduto == codigoProdutoFornecedor)
                                                                    .Take(1).SingleOrDefault();
        }

        #endregion

        #region " CONSULTA DE UMA LISTA "

        public List<Produto> ConsulteListaPelaDescricao(string descricao, string status, Marca marca, Categoria linha, Grupo grupo, Cor cor, Tamanho tamanho, EnumSexoProduto? sexo)
        {
            Expression<Func<Produto, bool>> expressaoParaConsulta = produto => produto.DadosGerais.Descricao.IsLike("%" + descricao + "%");

            var filtrosAdicionais = FiltrosDeConsultaParaLista(marca, linha, grupo, status, cor, tamanho, sexo);

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(filtrosAdicionais);

            return _session.QueryOver<Produto>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<Produto> ConsulteListaQueContemCodigoBarras(string codigoDeBarras, string status, Marca marca, Categoria categoria, Grupo grupo, Cor cor, Tamanho tamanho, EnumSexoProduto? sexo)
        {
            Expression<Func<Produto, bool>> expressaoParaConsulta = produto => produto.DadosGerais.CodigoDeBarras.IsLike("%" + codigoDeBarras + "%");

            var filtrosAdicionais = FiltrosDeConsultaParaLista(marca, categoria, grupo, status, cor, tamanho, sexo);

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(filtrosAdicionais);

            return _session.QueryOver<Produto>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<Produto> ConsulteListaQueContemSerialNumber(string serialNumber, string status, Marca marca, Categoria categoria, Grupo grupo, Cor cor, Tamanho tamanho, EnumSexoProduto? sexo)
        {
            Expression<Func<Produto, bool>> expressaoParaConsulta = produto => produto.Principal.CodigoFabricante.IsLike("%" + serialNumber + "%");

            var filtrosAdicionais = FiltrosDeConsultaParaLista(marca, categoria, grupo, status, cor, tamanho, sexo);

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(filtrosAdicionais);

            return _session.QueryOver<Produto>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<Produto> ConsulteListaProdutosAtivos(Marca marca, Categoria categoria, Grupo grupo, SubGrupo subGrupo, EnumFiltroSituacao? filtroSituacaoSaldo)
        {
            Expression<Func<Produto, bool>> expressaoParaConsulta = produto => produto.Id != 0;

            var filtrosAdicionais = FiltrosDeConsultaParaLista(null, categoria, grupo, "A", null, null, null);

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(filtrosAdicionais);

            if (marca != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.Principal.Marca.Id == marca.Id);
            }

            if (subGrupo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.Principal.SubGrupo.Id == subGrupo.Id);
            }

            if (filtroSituacaoSaldo != null && filtroSituacaoSaldo != EnumFiltroSituacao.TODOSOSSALDOS)
            {
                if (filtroSituacaoSaldo == EnumFiltroSituacao.SALDONEGATIVO)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.FormacaoPreco.Estoque < 0);
                }
                else if (filtroSituacaoSaldo == EnumFiltroSituacao.SALDOPOSITIVO)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.FormacaoPreco.Estoque > 0);
                }

                else if (filtroSituacaoSaldo == EnumFiltroSituacao.SALDOZERADO)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.FormacaoPreco.Estoque == 0);
                }
            }

            return _session.QueryOver<Produto>()
                                    .Where(expressaoParaConsulta).List().ToList();
        }

        public List<Produto> ConsulteListaProdutosAtivos(string descricao, Marca marca, Fabricante fabricante, Categoria categoria, Grupo grupo, SubGrupo subGrupo)
        {
             Expression<Func<Produto, bool>> expressaoParaConsulta = produto => produto.DadosGerais.Descricao.IsLike( descricao + "%");
            var filtrosAdicionais = FiltrosDeConsultaParaLista(marca, categoria, grupo, "A", null, null, null);

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(filtrosAdicionais);

            if (fabricante != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.Principal.Fabricante.Id == fabricante.Id);
            }

            if (subGrupo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.Principal.SubGrupo.Id == subGrupo.Id);
            }

            return _session.QueryOver<Produto>()
                                    .Where(expressaoParaConsulta).List().ToList();
        }
        public List<Produto> ConsulteListaProdutosAtivosII(int Codigo, Marca marca, Fabricante fabricante, Categoria categoria, Grupo grupo, SubGrupo subGrupo)
        {
             Expression<Func<Produto, bool>> expressaoParaConsulta = produto => produto.Id == Codigo;
            var filtrosAdicionais = FiltrosDeConsultaParaLista(marca, categoria, grupo, "", null, null, null);

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(filtrosAdicionais);

            if (fabricante != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.Principal.Fabricante.Id == fabricante.Id);
            }

            if (subGrupo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.Principal.SubGrupo.Id == subGrupo.Id);
            }

            return _session.QueryOver<Produto>()
                                    .Where(expressaoParaConsulta).List().ToList();
        }

        public List<Produto> ConsulteListaAtivaPelaDescricao(string descricao)
        {
            return _session.QueryOver<Produto>()
                            .Where(produto => produto.DadosGerais.Status == "A" && produto.DadosGerais.Descricao.IsLike("%" + descricao + "%"))
                            .List().ToList();
        }

        public List<Produto> ConsulteListaAtivaQueContemCodigoFabricante(string codigoFabricante)
        {
            return _session.QueryOver<Produto>()
                            .Where(produto => produto.DadosGerais.Status == "A" && produto.Principal.CodigoFabricante.IsLike("%" + codigoFabricante + "%"))
                            .List().ToList();
        }

        public List<Produto> ConsulteListaAtivaQueContemDescricaoMarca(string descricao)
        {
            Marca marcaDaConsulta = null;

            Expression<Func<Produto, bool>> expressaoParaConsulta = produto => produto.DadosGerais.Status == "A";

            if (!string.IsNullOrEmpty(descricao))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => marcaDaConsulta.Descricao.IsLike("%" + descricao + "%"));
            }

            return _session.QueryOver<Produto>()
                            .Left.JoinAlias(produto => produto.Principal.Marca, () => marcaDaConsulta)
                            .Where(expressaoParaConsulta)
                            .List().ToList();
        }

        public List<Produto> ConsulteListaAtivaQueContemDescricaoFabricante(string descricao)
        {
            Fabricante fabricanteDaConsulta = null;

            Expression<Func<Produto, bool>> expressaoParaConsulta = produto => produto.DadosGerais.Status == "A";

            if (!string.IsNullOrEmpty(descricao))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => fabricanteDaConsulta.Descricao.IsLike("%" + descricao + "%"));
            }

            return _session.QueryOver<Produto>()
                            .Left.JoinAlias(produto => produto.Principal.Fabricante, () => fabricanteDaConsulta)
                            .Where(expressaoParaConsulta)
                            .List().ToList();
        }


        public List<VWProduto> ConsulteLista(Categoria categoria,
                                                                Grupo grupo,
                                                                SubGrupo subGrupo,
                                                                Marca marca,
                                                                Fabricante fabricante,
                                                                Tamanho tamanho,
                                                                bool itensComEstoqueMinimo,
                                                                int? itemComDDVAbaixoDe,
                                                                EnumOrdenacaoPesquisaProduto ordenacaoPesquisaProduto, 
                                                                int? estoqueMaiorQue = null)
        {
            Expression<Func<VWProduto, bool>> expressaoParaConsulta = produto => produto.Id > 0;

            if (categoria != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.CategoriaId == categoria.Id);
            }

            if (marca != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.MarcaId == marca.Id);
            }

            if (grupo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.GrupoId == grupo.Id);
            }

            if (subGrupo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.SubGrupoId == subGrupo.Id);
            }

            if (fabricante != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.FabricanteId == fabricante.Id);
            }

            if (tamanho != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.TamanhoId == tamanho.Id);
            }

            if (itemComDDVAbaixoDe != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.DDV < itemComDDVAbaixoDe.Value);
            }

            if (itensComEstoqueMinimo)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.Estoque <= produto.QtdMinima);
            }

            if (estoqueMaiorQue != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.Estoque > estoqueMaiorQue.Value);
            }

            var consulta = _session.QueryOver<VWProduto>().Where(expressaoParaConsulta);

            if (ordenacaoPesquisaProduto == EnumOrdenacaoPesquisaProduto.CODIGO)
            {
                consulta.OrderBy(produto => produto.Id).Asc();
            }
            else if (ordenacaoPesquisaProduto == EnumOrdenacaoPesquisaProduto.DESCRICAO)
            {
                consulta.OrderBy(produto => produto.Descricao).Asc();
            }
            else if (ordenacaoPesquisaProduto == EnumOrdenacaoPesquisaProduto.VALORVENDA)
            {
                consulta.OrderBy(produto => produto.ValorVenda).Asc();
            }

            return consulta.List().ToList();
        }

        public List<VWProdutoPdv> ConsulteListaVwProdutosPdvCodigoBarrasDescricao(string descricaoCodigoBarras)
        {
            return _session.QueryOver<VWProdutoPdv>().Where(produto => produto.Descricao.IsLike("%" + descricaoCodigoBarras + "%") ||
                                                                                                        produto.CodigoDeBarras.IsLike("%" + descricaoCodigoBarras + "%")).Take(200)
                                                                           .List().ToList();
        }

        public List<Produto> ConsulteListasPeloCodigo(int codigo, string status, Marca marca, Categoria linha, Grupo grupo, Cor cor, Tamanho tamanho, EnumSexoProduto? sexo)
        {
            Expression<Func<Produto, bool>> expressaoParaConsulta = produto => produto.Id == codigo;

            var filtrosAdicionais = FiltrosDeConsultaParaLista(marca, linha, grupo, status, cor, tamanho, sexo);

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(filtrosAdicionais);

            return _session.QueryOver<Produto>().Where(expressaoParaConsulta).List().ToList();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private Expression<Func<Produto, bool>> FiltrosDeConsultaParaLista(Marca marca, Categoria linha, Grupo grupo, string status, Cor cor, Tamanho tamanho, EnumSexoProduto? sexo)
        {
            Expression<Func<Produto, bool>> expressaoParaConsulta = null;

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.DadosGerais.Status == status);
            }

            if (marca != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.Principal.Marca.Id == marca.Id);
            }

            if (linha != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.Principal.Categoria.Id == linha.Id);
            }

            if (grupo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.Principal.Grupo.Id == grupo.Id);
            }

            if (cor != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.Vestuario.Cor.Id == cor.Id);
            }

            if (tamanho != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.Vestuario.Tamanho.Id == tamanho.Id);
            }

            if (sexo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(produto => produto.Vestuario.SexoProduto == sexo);
            }

            return expressaoParaConsulta;
        }

        #endregion


        public List<Produto> ConsulteListaPorId(List<int> listaIds)
        {
            UnidadeMedida unidade = null;
            Ncm ncm = null;
            Marca marca = null;
            Grupo grupo = null;

            return _session.QueryOver<Produto>().Where(produto => produto.Id.IsIn(listaIds))
                .Left.JoinAlias(produto => produto.DadosGerais.Unidade, () => unidade)
                .Left.JoinAlias(produto => produto.ContabilFiscal.Ncm, () => ncm)
                .Left.JoinAlias(produto => produto.Principal.Marca, () => marca)
                .Left.JoinAlias(produto => produto.Principal.Grupo, () => grupo).List().ToList();
        }


        public Produto ConsulteComJoinGrupoTributacaoETributacoes(int produtoId)
        {
            GrupoTributacaoIcms grupoTributacaoIcms = null;
            TributacaoIcms tributacoesIcms = null;

            return _session.QueryOver<Produto>()
                                               .Left.JoinAlias(produto => produto.ContabilFiscal.GrupoTributacaoIcms, () => grupoTributacaoIcms)
                                               .Left.JoinAlias(produto => grupoTributacaoIcms.ListaTributacoesIcms, () => tributacoesIcms)
                                               .Where(produto => produto.Id == produtoId).SingleOrDefault();
        }
    }
}
