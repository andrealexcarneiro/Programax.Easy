using Programax.Easy.Negocio.Financeiro.ContaBancariaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.ContaBancariaServ
{
    public class ContaBancariaMap : MapeamentoBase<ContaBancaria>
    {
        public ContaBancariaMap()
        {
            Table("CONTASBANCARIAS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CTB_ID");

            Map(contaBancaria => contaBancaria.TipoContaBancaria).Column("CTB_TIPO_CONTA").CustomType<EnumTipoContaBancaria>();
            Map(contaBancaria => contaBancaria.NumeroConta).Column("CTB_NUMERO_CONTA");
            Map(contaBancaria => contaBancaria.Status).Column("CTB_STATUS");
            Map(contaBancaria => contaBancaria.DataCadastro).Column("CTB_DATA_CADASTRO");

            References(contaBancaria => contaBancaria.PlanoDeContas).Column("CTB_PLANO_CONTA_ID");
            References(contaBancaria => contaBancaria.Agencia).Column("CTB_AGE_ID");
            References(contaBancaria => contaBancaria.Pessoa).Column("CTB_PES_ID");
        }
    }
}
