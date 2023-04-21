using System.ComponentModel;

namespace Programax.Easy.Negocio.Cadastros.Enumeradores
{
    public enum EnumOrigem
    {
        [Description("0 - Nacional, exceto as indicadas nos códigos 3 e 5")]
        NACIONALEXCETOASINDICADASNOSCODIGOS3E5 = 0,

        [Description("1 - Estrangeira - Importação direta, exceto a indicada no código 6")]
        ESTRANGEIRAIMPORTACAODIRETA = 1,

        [Description("2 - Estrangeira - Adquirida no mercado interno, exceto a indicada no código 7")]
        ESTRANGEIRAADQUIRIDANOMERCADOINTERNO = 2,

        [Description("3 - Nacional, mercadoria ou bem com conteúdo de importação superior a 40%")]
        NACIONALCONTEUDODEIMPORTACAOSUPERIOR40PORCENTO = 3,

        [Description("4 - Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos...")]
        NACIONALCUJAPRODUCAOEMCONFORMIDADECOMOSPROCESSOSPRODUTIVOSBASICOS = 4,

        [Description("5 - Nacional, mercadoria ou bem com conteúdo de importação inferior a 40%")]
        NACIONALCONTEUDODEIMPORTACAOINFERIOR40PORCENTO = 5,

        [Description("6 - Estrangeira - Importação direta, sem similar nacional, constante em lista de Resolução CAMEX")]
        ESTRANGEIRAIMPORTACAODIRETASEMSIMILARNACIONAL = 6,

        [Description("7 - Estrangeira - Adquirida no mercado interno, sem nacional, constante em lista de Resolução CAMEX")]
        ESTRANGEIRAADQUIRIDANOMERCADOINTERNOSEMSIMILARNACIONAL = 7,

        [Description("8 - Nacional, mercadoria ou bem com conteúdo de importação superior a 70%")]
        NACIONALCONTEUDODEIMPORTACAOSUPERIOR70PORCENTO = 8,
    }
}
