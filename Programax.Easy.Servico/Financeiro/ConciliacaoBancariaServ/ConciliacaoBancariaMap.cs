using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Financeiro.ConciliacaoBancariaServ
{
    public class ConciliacaoBancariaMap : MapeamentoBase<ConciliacaoBancaria>
    {
        public ConciliacaoBancariaMap()
        {
            Table("CONCILIACAOBANCARIA");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CB_ID");

            Map(Conciliacao => Conciliacao.ChaveOrigem1).Column("CB_CHAVE_ORIGEM_1");
            Map(Conciliacao => Conciliacao.Origem1).Column("CB_ORIGEM_1").CustomType<EnumOrigemConciliacaoBancaria>();
            Map(Conciliacao => Conciliacao.NumDoc).Column("CB_NUM_DOC");
            Map(Conciliacao => Conciliacao.DescricaoDoc).Column("CB_DESCRICAO_DOC");
            Map(Conciliacao => Conciliacao.DataVencimento).Column("CB_DATA_VENCIMENTO");
            Map(Conciliacao => Conciliacao.ValorDoc).Column("CB_VALOR_DOC");
            Map(Conciliacao => Conciliacao.Origem2).Column("CB_ORIGEM_2").CustomType<EnumOrigemConciliacaoBancaria>();
            Map(Conciliacao => Conciliacao.NumLancto).Column("CB_NUM_LANCTO");
            Map(Conciliacao => Conciliacao.DescricaoLancto).Column("CB_DESCRICAO_LANCTO");
            Map(Conciliacao => Conciliacao.DataLancto).Column("CB_DATA_LANCTO");
            Map(Conciliacao => Conciliacao.ValorLancto).Column("CB_VALOR_LANCTO");
            Map(Conciliacao => Conciliacao.StatusConciliacao).Column("CB_STATUS_CONCILIACAO").CustomType<EnumOrigemMovimentacaoBanco>();

            References(Conciliacao => Conciliacao.MovimentacaoBanco).Column("CB_MOVIMENTACOES_BANCO_ID");
        }
    }
}
