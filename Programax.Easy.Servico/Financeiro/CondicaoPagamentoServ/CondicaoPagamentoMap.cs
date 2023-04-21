using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ
{
    public class CondicaoPagamentoMap : MapeamentoBase<CondicaoPagamento>
    {
        public CondicaoPagamentoMap()
            : base()
        {
            Table("PAGAMENTOCONDICAO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("COPAG_ID");

            Map(condicao => condicao.Status).Column("COPAG_STATUS");
            Map(condicao => condicao.DataCadastro).Column("COPAG_DATA_CADASTRO");
            Map(condicao => condicao.Descricao).Column("COPAG_DESCRICAO");

            Map(condicao => condicao.EstahDisponivelParaContasAPagar).Column("COPAG_DISPONIVEL_CONTAS_PAGAR");
            Map(condicao => condicao.EstahDisponivelParaContasAReceber).Column("COPAG_DISPONIVEL_CONTAS_RECEBER");
            Map(condicao => condicao.EstahDisponivelParaPdv).Column("COPAG_DISPONIVEL_PDV");
            Map(condicao => condicao.PrecisaDaLiberacaoDoGerente).Column("COPAG_PRECISA_LIBERACAO_GERENTE");
            Map(condicao => condicao.EstahDisponivelAcimaDeDeterminadoValor).Column("COPAG_DIPONIVEL_SOMENTE_ACIMA_DO_VALOR");
            Map(condicao => condicao.ValorQueEstaraDisponivel).Column("COPAG_VALOR_QUE_ESTARA_DISPONIVEL");
            Map(condicao => condicao.CondicaoPadraoAVista).Column("COPAG_AVISTA_PADRAO");

            HasMany(condicao => condicao.ListaDeParcelas).KeyColumn("PARC_COPAG_ID").Cascade.AllDeleteOrphan().Inverse().AsBag();
        }
    }
}
