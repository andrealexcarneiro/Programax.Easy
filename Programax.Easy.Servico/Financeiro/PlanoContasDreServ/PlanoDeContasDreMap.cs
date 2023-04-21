using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.PlanoContasDreObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.PlanoContasDreServ
{
    public class PlanoDeContasDreMap : MapeamentoBase<PlanoContaDre>
    {
        public PlanoDeContasDreMap()
        {
            Table("PLANOCONTASDRE");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PLC_ID");

            Map(planoContas => planoContas.Descricao).Column("PLC_DESCRICAO");
            Map(planoContas => planoContas.Status).Column("PLC_STATUS");
            Map(planoContas => planoContas.DataCadastro).Column("PLC_DATA_CADASTRO");
            Map(planoContas => planoContas.NumeroPlanoDeContas).Column("PLC_NUMERO_PLANO_CONTAS");
            Map(planoContas => planoContas.PlanoDeContasPadrao).Column("PLC_PLANO_CONTAS_PADRAO");
            Map(planoContas => planoContas.NumeroPlanoContasContador).Column("PLC_NUMERO_PLANO_CONTAS_CONTADOR");
            Map(planoContas => planoContas.NaturezaPlanoContas).Column("PLC_NATUREZA_PLANO_CONTAS").CustomType<EnumNaturezaPlanoContas>();
            Map(planoContas => planoContas.TipoPlanoContas).Column("PLC_TIPO_PLANO_CONTAS").CustomType<EnumTipoPlanoContas>();
            Map(planoContas => planoContas.Grau).Column("PLC_GRAU");
        }
    }
}
