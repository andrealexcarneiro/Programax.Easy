using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using System.Linq;

namespace Programax.Easy.Servico.Financeiro.OperadorasCartaoServ
{
    public class OperadorasCartaoMap : MapeamentoBase<OperadorasCartao>
    {
        public OperadorasCartaoMap()
        {
            Table("OPERADORASCARTAO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("OPER_ID");
            Map(operadora => operadora.Descricao).Column("OPER_DESCRICAO");
            Map(operadora => operadora.DataCadastro).Column("OPER_DATA_CADASTRO");
            Map(operadora => operadora.Status).Column("OPER_STATUS");
            Map(operadora => operadora.DiasPrazoParaCreditar).Column("OPER_DIAS_PRAZO_PARA_CREDITAR");

            Map(operadora => operadora.PermiteParcelamento).Column("OPER_PERMITE_PARCELAMENTO");
            Map(operadora => operadora.RecebimentoAntecipado).Column("OPER_RECEBIMENTO_ANTECIPADO");

            Map(operadora => operadora.CobrarTaxaApartirDaParcela).Column("OPER_COBRAR_TAXA_APARTIR_DA_PARCELA");
            Map(operadora => operadora.TaxaAdministracao).Column("OPER_TAXA_ADMINISTRACAO");

            References(operadora => operadora.BancoParaMovimento).Column("OPER_BANCO_ID").LazyLoad();
            References(operadora => operadora.CategoriaDeDespesa).Column("OPER_CATEGORIA_DESPESA").LazyLoad();
        }
    }
}
