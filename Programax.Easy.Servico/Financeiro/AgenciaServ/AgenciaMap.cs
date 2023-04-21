using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Financeiro.AgenciaServ
{
    public class AgenciaMap : MapeamentoBase<Agencia>
    {
        public AgenciaMap()
        {
            Table("AGENCIAS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("AGE_ID");

            Map(banco => banco.NomeAgencia).Column("AGE_NOME_AGENCIA");
            Map(banco => banco.NumeroAgencia).Column("AGE_NUMERO_AGENCIA");
            Map(banco => banco.DigitoAgencia).Column("AGE_DIGITO_AGENCIA");
            Map(banco => banco.Status).Column("AGE_STATUS");
            Map(banco => banco.NumeroEndereco).Column("AGE_NUMERO_ENDERECO");
            Map(banco => banco.ComplementoEndereco).Column("AGE_COMPLEMENTO_ENDERECO");
            Map(banco => banco.Telefone1).Column("AGE_TELEFONE_1");
            Map(banco => banco.Telefone2).Column("AGE_TELEFONE_2");
            Map(banco => banco.NomeGerente).Column("AGE_NOME_GERENTE");
            Map(banco => banco.CelularGerente).Column("AGE_CELULAR_GERENTE");
            Map(banco => banco.DataCadastro).Column("AGE_DATA_CADASTRO");
            Map(banco => banco.Observacoes).Column("AGE_OBSERVACOES");

            References(banco => banco.Banco).Column("AGE_BANC_ID");
            References(banco => banco.Endereco).Column("AGE_END_ID");
        }
    }
}
