using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Financeiro.BancoParaMovimentoServ
{
    public class BancoParaMovimentoMap : MapeamentoBase<BancoParaMovimento>
    {
        public BancoParaMovimentoMap()
        {
            Table("BANCOPARAMOVIMENTO");

            Id(banco => banco.Id).Column("BPM_ID");

            Map(banco => banco.NomeBanco).Column("BPM_NOME_BANCO");

            Map(banco => banco.DataCadastro).Column("BPM_DATA_CADASTRO");

            References(banco => banco.ContaBancaria).Column("BPM_CONTA_BANCARIA_ID");

            Map(banco => banco.Status).Column("BPM_STATUS");

            Map(banco => banco.TornarPadrao).Column("BPM_BANCO_PADRAO");
        }
    }
}
