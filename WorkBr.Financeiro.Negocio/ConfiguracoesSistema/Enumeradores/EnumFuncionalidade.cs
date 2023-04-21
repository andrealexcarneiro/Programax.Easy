using System.ComponentModel;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores
{
    public enum EnumFuncionalidade
    {
        #region " PARCEIROS "

        [Description("PESSOAS")]
        PESSOAS = 1,

        [Description("COMISSÕES")]
        COMISSOES = 2,

        [Description("CIDADES")]
        CIDADES = 3,

        [Description("ENDEREÇOS")]
        ENDERECOS = 4,

        [Description("RAMO DE ATIVIDADES")]
        RamoAtividade = 5,

        [Description("ORIGEM DO CLIENTE")]
        OrigemCliente = 6,

        [Description("PAUTA ICMS")]
        PAUTAICMS = 7,

        #endregion

        #region " ESTOQUE "

        [Description("ITENS")]
        ITENS = 11,

        [Description("CORES")]
        CORES = 12,

        [Description("TAMANHOS")]
        TAMANHOS = 13,

        [Description("CATEGORIAS")]
        CATEGORIAS = 14,

        [Description("UNIDADE DE MEDIDA")]
        UNIDADESMEDIAS = 15,

        [Description("GRUPO DE PRODUTOS")]
        GRUPODEPRODUTOS = 16,

        [Description("SUBGRUPO DE PRODUTOS")]
        SUBGRUPODEPRODUTOS = 17,

        [Description("MARCAS")]
        MARCAS = 18,

        [Description("FABRICANTES")]
        FABRICANTES = 19,

        [Description("CORREÇÃO DE ESTOQUE")]
        CORRECAOESTOQUE = 20,

        [Description("MOTIVO CORREÇÃO DE ESTOQUE")]
        MOTIVOCORRECAOESTOQUE = 21,

        [Description("TABELA DE PREÇO")]
        TABELAPRECO = 22,

        [Description("ENTRADA DE MERCADORIAS")]
        ENTRADAMERCADORIAS = 23,

        [Description("ENTRADA DE MERCADORIAS MANUAL")]
        ENTRADAMERCADORIASMANUAL = 109,

        [Description("INVENTÁRIO")]
        INVENTARIO = 24,

        [Description("FORMAÇÃO DE PREÇO")]
        FORMACAOPRECO = 72,

        [Description("SAÍDA DE ESTOQUE")]
        SAIDADEESTOQUE = 83,

        [Description("ESTORNAR ESTOQUE")]
        ESTORNARSTOQUE = 101,

        [Description("SUB ESTOQUE")]
        SUBESTOQUE = 112,

        [Description("TRANSFERENCIA SUB ESTOQUE")]
        TRANSFERENCIASUBESTOQUE = 113,
        #endregion

        #region " FINANCEIRO "

        [Description("CADASTRO DE BANCOS")]
        BANCOS = 25,

        [Description("CADASTRO DE AGÊNCIAS")]
        AGENCIAS = 26,

        [Description("CADASTRO DE CONTAS BANCÁRIAS")]
        CONTASBANCARIAS = 27,

        [Description("CADASTRO DE FORMA DE PAGAMENTO")]
        FORMAPAGAMENTO = 28,

        [Description("CADASTRO DE CONDIÇÕES DE PAGAMENTO")]
        CONDICOESPAGAMENTO = 29,

        [Description("PLANO DE CONTAS")]
        PLANODECONTAS = 30,

        [Description("CONTAS À PAGAR")]
        CONTASPAGAR = 31,

        [Description("CONTAS À RECEBER")]
        CONTASRECEBER = 32,

        [Description("MANUTENÇÃO CONTAS À PAGAR")]
        MANUTENCAOCONTASPAGAR = 33,

        [Description("MANUTENÇÃO CONTAS À RECEBER")]
        MANUTENCAOCONTASRECEBER = 34,

        [Description("PRORROGAÇÃO CONTAS À PAGAR")]
        PRORROGACAOCONTASPAGAR = 35,

        [Description("PRORROGAÇÃO CONTAS À RECEBER")]
        PRORROGACAOCONTASRECEBER = 36,

        [Description("CHEQUES")]
        CHEQUES = 37,

        [Description("CADASTRO DE CAIXA")]
        CADASTROCAIXA = 38,

        [Description("MOVIMENTAÇÃO DE CAIXA")]
        MOVIMENTACAOCAIXA = 39,

        [Description("RECEBIMENTO DE VENDAS")]
        RECEBIMENTO = 40,

        [Description("ANÁLISE DE CRÉDITO")]
        CREDIARIO = 80,

        [Description("CONFIGURACAO DE EMISSAO BOLETO")]
        CONFIGURACAOEMISSAOBOLETO = 86,

        [Description("RETORNO DE BOLETOS")]
        RETORNOBOLETOS = 89,

        [Description("RESUMO FINANCEIRO")]
        RESUMOFINANCEIRO = 91,

        [Description("GRUPO CATEGORIA")]
        GRUPOCATEGORIA = 92,

        [Description("CATEGORIA FINANCEIRA")]
        CATEGORIAFINANCEIRA = 93,

        [Description("CATEGORIA FINANCEIRA DRE")]
        CATEGORIAFINANCEIRADRE = 109,

        [Description("OPERADORAS DE CARTAO")]
        OPERADORASCARTAO = 94,

        [Description("BANCO PARA MOVIMENTO")]
        BANCOPARAMOVIMENTO = 95,

        [Description("MOVIMENTAÇÕES BANCO")]
        MOVIMENTACAOBANCO = 96,

        [Description("CONCILIAÇÃO BANCÁRIA")]
        CONCILIACAOBANCARIA = 97,

        [Description("PLANO DE CONTAS D.R.E")]
        PLANODECONTASDRE = 102,

        [Description("D.R.E")]
        DRE = 105,

        [Description("CONFIGURAÇÃO CASHBACK")]
        CONFIGURACAOCASHBACK = 106,

        [Description("CRIAÇÃO CARTEIRA")]
        CRIACAOCARTEIRA = 107,

        [Description("CRIAÇÃO CARTEIRA REFILTEK")]
        CRIACAOCARTEIRAREFILTEK = 114,

        [Description("GRUPO DRE")]
        GRUPODRE = 108,


        #endregion

        #region " FISCAL "

        [Description("CADASTRO DE CFOP")]
        CFOP = 41,

        [Description("CADASTRO DE CNAE")]
        CNAE = 42,

        [Description("CADASTRO DE NCM")]
        NCM = 43,

        [Description("CADASTRO DE CEST")]
        CEST = 87,

        [Description("NATUREZA OPERAÇÃO")]
        NATUREZAOPERACAO = 44,

        [Description("NOTA FISCAL")]
        NOTAFISCAL = 65,

        [Description("OUTRAS SAIDAS")]
        OUTRASSAIDAS = 82,

        [Description("CANCELAMENTO NOTA FISCAL")]
        CANCELAMENTONOTAFISCAL = 67,

        [Description("CONFIGURAÇÃO NFE")]
        CONFIGURACAONFE = 66,

        [Description("INUTILIZAÇÃO NUMERAÇÃO NOTA FISCAL")]
        INUTILIZACAONUMERACAONOTA = 75,

        [Description("GRUPO TRIBUTAÇÃO ICMS")]
        GRUPOTRIBUTACAOICMS = 76,

        [Description("CADASTRO ICMS INTERESTADUAL")]
        ICMSINTERESTADUAL = 78,

        [Description("GRUPO TRIBUTAÇÃO FEDERAL")]
        GRUPOTRIBUTACAOFEDERAL = 84,

        [Description("SPED FISCAL")]
        SPEDFISCAL = 110,

        #endregion

        #region " VENDAS "

        [Description("PEDIDO DE VENDAS")]
        PEDIDODEVENDAS = 45,

        [Description("VENDA RAPIDA")]
        VENDARAPIDA = 46,

        [Description("LIBERAÇÃO DOCUMENTO")]
        LIBERACAODOCUMENTO = 47,

        [Description("CONSULTA PREÇO PRODUTO")]
        CONSULTAPRECOPRODUTO = 56,

        [Description("TROCA PEDIDO DE VENDA")]
        TROCAPEDIDOVENDA = 69,

        [Description("MOTIVO TROCA PEDIDO DE VENDA")]
        MOTIVOTROCAPEDIDOVENDA = 70,

        [Description("ROTEIROS")]
        ROTEIROS = 99,

        [Description("AGENDA")]
        AGENDA = 100,

        [Description("TELEMARKETING")]
        TELEMARKETING = 103,

        [Description("GERENCIARTMK")]
        GERENCIARTMK = 104,


        #endregion

        #region " FRENTE DE LOJA "

        [Description("EXPORTAR VENDAS PDV-ECF")]
        EXPORTARVENDASPDVECF = 74,

        [Description("PDV")]
        PDV = 77,

        #endregion

        #region " ADM SISTEMA "

        [Description("EMPRESA")]
        EMPRESA = 48,

        [Description("USUÁRIO")]
        USUARIO = 49,

        [Description("GRUPO DE ACESSO")]
        GRUPOACESSO = 50,

        [Description("PERMISSÕES")]
        PERMISSOES = 51,

        [Description("PARÂMETROS")]
        PARAMETROS = 52,

        [Description("LICENÇA DE USO")]
        LICENCADEUSO = 53,

        [Description("CONFIGURAÇÕES PDV")]
        CONFIGURACOESPDV = 54,

        [Description("BACKUP")]
        BACKUP = 55,

        [Description("LIBERAÇÃO")]
        LIBERACAOSISTEMA = 73,

        #endregion

        #region " RELATÓRIOS "

        [Description("RELATÓRIO PARCEIROS")]
        RELATORIOPARCEIROS = 57,

        [Description("RELATÓRIO ITENS")]
        RELATORIOITENS = 58,

        [Description("RELATÓRIO CONTAS A PAGAR")]
        RELATORIOCONTASPAGAR = 59,

        [Description("RELATÓRIO CONTAS A RECEBER")]
        RELATORIOCONTASRECEBER = 60,

        [Description("RELATÓRIO VENDAS POR VENDEDOR")]
        RELATORIOVENDASVENDEDOR = 61,

        [Description("RELATÓRIO VENDAS POR VENDEDOR - VISUALIZAR DE TODOS")]
        RELATORIOVENDASVENDEDORVISUALIZARTODOSVENDEDORES = 71,

        [Description("RELATÓRIO VENDAS POR CLIENTE")]
        RELATORIOVENDASCLIENTE = 62,

        [Description("RELATÓRIO ENTRADA")]
        RELATORIOENTRADA = 63,

        [Description("RELATÓRIO MOVIMENTAÇÃO CAIXA")]
        RELATORIOMOVIMENTACAOCAIXA = 64,

        [Description("RELATÓRIO MOVIMENTAÇÃO ITENS")]
        RELATORIOMOVIMENTACAOITENS = 68,

        [Description("RELATÓRIO CLIENTES SEM COMPRAR")]
        RELATORIOCLIENTESSEMCOMPRAR = 79,

        [Description("RELATÓRIO DE TRANSPORTES")]
        RELATORIODETRANSPORTES = 81,

        [Description("RELATÓRIO DE LOCACAO")]
        RELATORIODELOCACAO = 85,

        [Description("RELATÓRIO DE VENDA POR CONDIÇÃO DE PAGAMENTO")]
        RELATORIODEVENDAPORCONDICAODEPAGAMENTO = 88,

        [Description("RELATÓRIO DE ITENS VENDIDOS E BAIXADOS")]
        RELATORIODEITENSVENDIDOSEBAIXADOS = 90,

        [Description("RELATÓRIO DE FLUXO DE CAIXA")]
        RELATORIOFLUXOCAIXA = 98,

        [Description("RELATÓRIO DE CUSTO FINANCEIRO POR VENDEDOR")]
        RELATORIOCUSTOFINANCEIROVENDEDOR = 111,

        #endregion

        SEMVERIFICACAODEPERMISSAO = 0
    }
}
