using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Fiscal.CfopServ
{
    public class CfopMap : MapeamentoBase<Cfop>
    {
        public CfopMap()
        {
            Table("CFOP");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CFOP_ID");

            Map(condicao => condicao.Codigo).Column("CFOP_CODIGO");
            Map(condicao => condicao.Descricao).Column("CFOP_DESCRICAO");

            Map(condicao => condicao.Status).Column("CFOP_STATUS");
            Map(condicao => condicao.DataCadastro).Column("CFOP_DATA_CADASTRO");

            Map(condicao => condicao.InformacoesComplementaresNFe).Column("CFOP_INFORMACOES_COMPLEMENTARES_NFE");

            References(condicao => condicao.CfopDeConversao).Column("CFOP_CFOP_ID_CONVERSAO");
        }
    }
}
