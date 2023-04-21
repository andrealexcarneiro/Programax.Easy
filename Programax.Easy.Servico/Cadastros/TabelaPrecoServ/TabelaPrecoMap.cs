using FluentNHibernate.Mapping;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.TabelaPrecoServ
{
    public class TabelaPrecoMap : MapeamentoBase<TabelaPreco>
    {
        public TabelaPrecoMap()
            : base()
        {
            Table("TABELAPRECOS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("TBPRE_ID");

            Map(tabelaPrecos => tabelaPrecos.NomeTabela).Column("TBPRE_DESCRICAO");
            Map(tabelaPrecos => tabelaPrecos.Status).Column("TBPRE_STATUS");
            Map(tabelaPrecos => tabelaPrecos.Acrescimo).Column("TBPRE_ACRESCIMO");
            Map(tabelaPrecos => tabelaPrecos.AcrescimoEhPercentual).Column("TBPRE_ACRESCIMO_EH_PERCENTUAL");
            Map(tabelaPrecos => tabelaPrecos.Decrescimo).Column("TBPRE_DECRESCIMO");
            Map(tabelaPrecos => tabelaPrecos.DecrescimoEhPercentual).Column("TBPRE_DECRESCIMO_EH_PERCENTUAL");
            Map(tabelaPrecos => tabelaPrecos.Frete).Column("TBPRE_FRETE");
            Map(tabelaPrecos => tabelaPrecos.FreteEhPercentual).Column("TBPRE_FRETE_EH_PERCENTUAL");
            Map(tabelaPrecos => tabelaPrecos.DataDeCadastro).Column("TBPRE_DATA_CADASTRO");
            Map(tabelaPrecos => tabelaPrecos.DataDeValidade).Column("TBPRE_DATA_VALIDADE");
        }
    }
}
