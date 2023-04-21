using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Financeiro.BancoServ
{
    public class BancoMap : MapeamentoBase<Banco>
    {
        public BancoMap()
        {
            Table("BANCOS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("BANC_ID");

            Map(banco => banco.Codigo).Column("BANC_CODIGO_COMPENSACAO");
            Map(cidade => cidade.DataCadastro).Column("BANC_DATA_REG");
            Map(cidade => cidade.Descricao).Column("BANC_DESCRICAO");
            Map(cidade => cidade.Site).Column("BANC_SITE");
            Map(cidade => cidade.Status).Column("BANC_STATUS");
        }
    }
}
