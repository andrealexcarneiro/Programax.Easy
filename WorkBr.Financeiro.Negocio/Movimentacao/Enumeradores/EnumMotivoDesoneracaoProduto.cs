using System.ComponentModel;

namespace Programax.Easy.Negocio.Movimentacao.Enumeradores
{
    public enum EnumMotivoDesoneracaoProduto
    {
        [Description("TAXI")]
        TAXI = 1,

        [Description("DEFICIENTE FÍSICO")]
        DEFICIENTEFISICO = 2,

        [Description("PRODUTOR AGROPECUÁRIO")]
        PRODUTORAGRUPECUARIO = 3,

        [Description("FROTISTA/LOCADORA")]
        FROTISTALOCADORA = 4,

        [Description("DIPLOMÁTICO/CONSULAR")]
        DIPLOMATICOCONSULTAR = 5,

        [Description("UTILITÁRIOS E MOTOCICLETAS DA AMAZÔNIA OCIDENTAL E ÁREAS DE LIVRE COMÉRCIO (RESOLUÇÃO 714/88 E 790/94 – CONTRAN E SUAS ALTERAÇÕES)")]
        UTILITARIOSEMOTOCICLETASDAAMAZONIAOCIDENTAL = 6,

        [Description("SUFRAMA")]
        SUFRAMA = 7,

        [Description("VENDA A ÓRGÃO PÚBLICO")]
        VENDAAORGAOSPUBLICOS = 8,

        [Description("OUTROS")]
        OUTROS = 9,

        [Description("DEFICIENTE CONDUTOR (CONVÊNIO ICMS 38/12)")]
        DEFICIENTECONDUTOR = 10,

        [Description("DEFICIENTE NÃO CONDUTOR (CONVÊNIO ICMS 38/12)")]
        DEFICIENTENAOCONDUTOR = 11,

        [Description("ÓRGÃO DE FOMENTO E DESENVOLVIMENTO AGROPECUÁRIO.")]
        ORGAOFORMENTODESENVOLVIMENTOAGROPECUARIO = 12
    }
}
