using System.ComponentModel;

namespace Programax.Easy.Negocio.Movimentacao.Enumeradores
{
    public enum EnumModeloDocumentoFiscal
    {
        [Description("01 - Nota Fiscal")]
        NOTAFISCAL = 1,

        [Description("1B - Nota Fiscal Avulsa")]
        NOTAFISCALAVULSA = 0,

        [Description("04 - Nota Fiscal de Produtor")]
        NOTAFISCALDEPRODUTOR = 4,

        [Description("06 - Nota Fiscal / Conta de Energia")]
        NOTAFISCALCONTAENERGIA = 6,

        [Description("07 - Nota Fiscal de Serviço de Transporte")]
        NOTAFISCALDESERVICODETRANSPORTE = 7,

        [Description("08 - Conhecimento de Transporte Rodoviário de Cargas")]
        CONHECIMENTODETRANSPORTERODOVIARIODECARGAS = 8,

        [Description("09 - Conhecimento de Transporte Aquaviário de Cargas")]
        CONHECIMENTODETRANSPORTEAQUAVIARIODECARGAS = 9,

        [Description("10 - Conhecimento Aéreo")]
        CONHECIMENTOAEREO = 10,

        [Description("11 - Conhecimento de Transporte Ferroviário de Cargas")]
        CONHECIMENTODETRANSPORTEFERROVIARIODECARGAS = 11,

        [Description("13 - Bilhete de Passagem Rodoviário")]
        BILHETEDEPASSAGEMRODROVIARIO = 13,

        [Description("14 - Bilhete de Passagem Aquaviário")]
        BIILHETEDEPASSAGEMAQUAVIARIO = 14,

        [Description("15 - Bilhete de Passagem e Nota de Bagagem")]
        BILHETEDEPASSAGEMENOTADEBAGAGEM = 15,

        [Description("16 - Bilhete de Ferroviário")]
        BILHETEDEFERROVIARIO = 16,

        [Description("21 - Nota Fiscal de Serviço de Comunicações")]
        NOTAFISCALDESERVICODECOMUNICACAO = 21,

        [Description("22 - Nota Fiscal de Serviço de Telecomunicações")]
        NOTAFISCALDESERVICODETELECOMUNICACOES = 22,

        [Description("29 - Nota Fiscal / Conta de Fornecimento de Água Canalizada")]
        CONTADEFORNECIMENTODEAGUACANALIZADA = 29,

        [Description("55 - Nota Fiscal Eletrônica")]
        NOTAFISCALELETRONICA = 55
    }
}
