using Programax.Easy.Servico.Cadastros.CashBack.CashBackServ;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.CashBack.CashBackServ
{
    class CashMap : MapeamentoBase<Cashback>
    {
        public CashMap()
        {
            Table("CADCASHBACK");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ID");

            Map(cashBack => cashBack.start).Column("START");
            Map(cashBack => cashBack.DataInicio).Column("DATAINICIO");
            Map(cashBack => cashBack.DataFim).Column("DATAFIM");
            Map(cashBack => cashBack.Valor).Column("VALOR");
            Map(cashBack => cashBack.Percentual).Column("PERCENTUAL");
        }
        
    }
}
