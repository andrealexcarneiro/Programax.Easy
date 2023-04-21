using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.ProdutoServ
{
    public class
        ProdutoMap : MapeamentoBase<Produto>
    {
        public ProdutoMap()
            : base()
        {
            Table("PRODUTOS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PROD_ID");

            MapeieDadosGerais();
            MapeiePrincipal();
            MapeieVestuario();
            MapeieContabilFiscal();
            MapeieFormacaoPreco();
            MapeieFornecedores();
        }
        
        private void MapeieDadosGerais()
        {
            Component(produto => produto.DadosGerais, dadosGerais =>
            {
                dadosGerais.Map(produto => produto.Foto).Column("PROD_FOTO").Length(2147483647).LazyLoad();

                dadosGerais.Map(produto => produto.Status).Column("PROD_STATUS");
                dadosGerais.Map(produto => produto.CodigoDeBarras).Column("PROD_CODBARRAS");
                dadosGerais.Map(produto => produto.Descricao).Column("PROD_DESCRICAO");
                dadosGerais.Map(produto => produto.ProdutoEmInventario).Column("PROD_EM_INVENTARIO");
                dadosGerais.Map(produto => produto.DataCadastro).Column("PROD_DATA_CADASTRO");
                dadosGerais.Map(produto => produto.PermiteVendaFracionada).Column("PROD_PERMITE_VENDA_FRACIONADA");

                dadosGerais.References(produto => produto.Unidade).Column("PROD_UND_ID").Not.LazyLoad().Fetch.Join();
            });
        }

        private void MapeiePrincipal()
        {
            Component(produto => produto.Principal, principal =>
            {
                principal.Map(produto => produto.PesoBruto).Column("PROD_PESOBRUTO");
                principal.Map(produto => produto.PesoLiquido).Column("PROD_PESOLIQUIDO");
                principal.Map(produto => produto.QuantidadeMaxima).Column("PROD_QTDEMAXIMA");
                principal.Map(produto => produto.QuantidadeMinima).Column("PROD_QTDEMINIMA");

                principal.Map(produto => produto.Locacao).Column("PROD_LOCACAO");

                principal.Map(produto => produto.Observacao).Column("PROD_OBSERVACAO");

                principal.Map(produto => produto.CodigoFabricante).Column("PROD_CODIGO_FABRICANTE");

                principal.References(produto => produto.ProdutoSimilar).Column("PROD_PRODUTO_SIMILAR_ID");
                principal.References(produto => produto.Marca).Column("PROD_MARC_ID").Not.LazyLoad().Fetch.Join();
                principal.References(produto => produto.Fabricante).Column("PROD_FABRICANTE_ID").Not.LazyLoad().Fetch.Join();
                principal.References(produto => produto.Categoria).Column("PROD_LINHA_ID");
                principal.References(produto => produto.Grupo).Column("PROD_GRUP_ID");
                principal.References(produto => produto.SubGrupo).Column("PROD_SUBGRP_ID");
            });
        }

        private void MapeieContabilFiscal()
        {
            Component(produto => produto.ContabilFiscal, contabilFiscal =>
            {
                contabilFiscal.References(produto => produto.Ncm).Column("PROD_CFISC_ID").Not.LazyLoad().Fetch.Join();
                contabilFiscal.References(produto => produto.GrupoTributacaoIcms).Column("PROD_GRUPO_TRIBUTACAO_ICMS_ID").Not.LazyLoad().Fetch.Join();

                contabilFiscal.References(produto => produto.GrupoTributacaoFederal).Column("PROD_GRUPO_TRIBUTACAO_FEDERAL_ID").Not.LazyLoad().Fetch.Join();

                contabilFiscal.Map(produto => produto.NaturezaProduto).Column("PROD_NATUREZA_PRODUTO").CustomType<EnumNaturezaProduto>();
                contabilFiscal.Map(produto => produto.SituacaoTributariaProduto).Column("PROD_SITUACAO_TRIBUTARIA").CustomType<EnumSituacaoTributariaProduto>();
                contabilFiscal.Map(produto => produto.OrigemProduto).Column("PROD_ORIGEM_PRODUTO").CustomType<EnumOrigem>();
                contabilFiscal.Map(produto => produto.Icms).Column("PROD_ICMS");

                contabilFiscal.Map(produto => produto.CodigoGtin).Column("PROD_CODIGO_GTIN");
            });
        }

        private void MapeieFormacaoPreco()
        {
            Component(produto => produto.FormacaoPreco, formacaoPreco =>
            {
                formacaoPreco.Map(produto => produto.PrecoCompra).Column("PROD_PRECO_COMPRA");
                formacaoPreco.Map(produto => produto.ValorFreteCompra).Column("PROD_VALOR_FRETE_COMPRA");
                formacaoPreco.Map(produto => produto.PercentualIcmsCompra).Column("PROD_PERCENTUAL_ICMS_COMPRA");
                formacaoPreco.Map(produto => produto.PercentualIpiCompra).Column("PROD_PERCENTUAL_IPI_COMPRA");
                formacaoPreco.Map(produto => produto.PercentualIcmsSTCompra).Column("PROD_PERCENTUAL_ICMS_ST_COMPRA");
                formacaoPreco.Map(produto => produto.PercentualReducaoIcmsCompra).Column("PROD_PERCENTUAL_REDUCAO_ICMS_COMPRA");

                formacaoPreco.Map(produto => produto.PercentualDespesasFixasVenda).Column("PROD_PERCENT_DESPESAS_FIXAS_VENDA");
                formacaoPreco.Map(produto => produto.PercentualDespesasVariaveisVenda).Column("PROD_PERCENT_DESP_VARIAVEIS_VENDA");
                formacaoPreco.Map(produto => produto.PercentualIcmsSimplesVenda).Column("PROD_PERCENT_ICMS_SIMPLES_VENDA");
                formacaoPreco.Map(produto => produto.PercentualReducaoIcmsVenda).Column("PROD_PERCENT_RED_ICMS_VENDA");
                formacaoPreco.Map(produto => produto.PercentualOutrasDespesasVenda).Column("PROD_PERCENT_OUTRAS_DESP_VENDA");
                formacaoPreco.Map(produto => produto.PercentualFreteVenda).Column("PROD_PERCENT_FRETE_VENDA");
                formacaoPreco.Map(produto => produto.PercentualComissoesVenda).Column("PROD_PERCENT_COMISSOES_VENDA");

                //VALORES DE SERVIÇOS PARA COMISSOES DE TÉCNICOS.
                formacaoPreco.Map(produto => produto.ValorEntrega).Column("PROD_VALOR_ENTREGA");
                formacaoPreco.Map(produto => produto.ValorEntregaAposHorario).Column("PROD_VALOR_ENTREGA_APOS_HORARIO");
                formacaoPreco.Map(produto => produto.ValorInstalacao).Column("PROD_VALOR_INSTALACAO");
                formacaoPreco.Map(produto => produto.ValorInstalacaoAposHorario).Column("PROD_VALOR_INSTALACAO_APOS_HORARIO");
                formacaoPreco.Map(produto => produto.ValorInstalacaoOutrasCidades).Column("PROD_VALOR_INSTALACAO_OUTRAS_CIDADES");
                formacaoPreco.Map(produto => produto.ValorDeslocamentoEGarantia).Column("PROD_VALOR_DESLOCAMENTO_E_GARANTIA");


                formacaoPreco.Map(produto => produto.PercentualLucro).Column("PROD_PERCENT_LUCRO");
                formacaoPreco.Map(produto => produto.Markup).Column("PROD_MARKUP");
                formacaoPreco.Map(produto => produto.ValorVenda).Column("PROD_VALORVENDA");
                formacaoPreco.Map(produto => produto.EhPromocao).Column("PROD_EH_PROMOCAO");
                formacaoPreco.Map(produto => produto.ValorPromocao).Column("PROD_VALOR_PROMOCAO");
                formacaoPreco.Map(produto => produto.PercentualDescontoMaximo).Column("PROD_PERCENT_DESC_MAXIMO");
                formacaoPreco.Map(produto => produto.Estoque).Column("PROD_ESTOQUE");
                formacaoPreco.Map(produto => produto.EstoqueReservado).Column("PROD_ESTOQUE_RESERVADO");
            });
        }

        private void MapeieVestuario()
        {
            Component(produto => produto.Vestuario, vestuario =>
            {
                vestuario.Map(produto => produto.Colecao).Column("PROD_COLECAO");
                vestuario.Map(produto => produto.Composicao).Column("PROD_COMPOSICAO");
                vestuario.Map(produto => produto.DescricaoDetalhada).Column("PROD_DESCRICAO_DETALHADA");
                vestuario.Map(produto => produto.MaterialTecido).Column("PROD_MATERIAL_TECIDO");
                vestuario.Map(produto => produto.Modelo).Column("PROD_MODELO");
                vestuario.Map(produto => produto.Referencia).Column("PROD_REFERENCIA");
                vestuario.Map(produto => produto.SexoProduto).Column("PROD_SEXO_PRODUTO").CustomType<EnumSexoProduto>();

                vestuario.References(produto => produto.Tamanho).Column("PROD_TAMA_ID").Not.LazyLoad().Fetch.Join();
                vestuario.References(produto => produto.Cor).Column("PROD_COR_ID").Not.LazyLoad().Fetch.Join();
            });
        }

        private void MapeieFornecedores()
        {
            HasMany(produto => produto.ListaFornecedores).KeyColumn("PRODFORN_PRODUTO_ID").Cascade.AllDeleteOrphan().Inverse().AsBag().Table("PRODUTOSFORNECEDORES");
        }
    }
}
