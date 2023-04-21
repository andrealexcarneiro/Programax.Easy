using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ
{
    public class MovimentacaoCaixaMap:MapeamentoBase<MovimentacaoCaixa>
    {
        public MovimentacaoCaixaMap()
        {
            Table("MOVIMENTACOESCAIXA");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("MOVCAIXA_ID");

            Map(movimentacaoCaixa => movimentacaoCaixa.DataHoraAbertura).Column("MOVCAIXA_DATA_HORA_ABERTURA");
            Map(movimentacaoCaixa => movimentacaoCaixa.DataHoraFechamento).Column("MOVCAIXA_DATA_HORA_FECHAMENTO");
            Map(movimentacaoCaixa => movimentacaoCaixa.ObservacoesAbertura).Column("MOVCAIXA_OBSERVACOES_ABERTURA");
            Map(movimentacaoCaixa => movimentacaoCaixa.ObservacoesFechamento).Column("MOVCAIXA_OBSERVACOES_FECHAMENTO");
            Map(movimentacaoCaixa => movimentacaoCaixa.SaldoInicial).Column("MOVCAIXA_SALDO_INICIAL");

            Map(movimentacaoCaixa => movimentacaoCaixa.SaldoInicialDinheiro).Column("MOVCAIXA_SALDO_INICIAL_DINHEIRO");
            Map(movimentacaoCaixa => movimentacaoCaixa.SaldoInicialCheque).Column("MOVCAIXA_SALDO_INICIAL_CHEQUE");
            Map(movimentacaoCaixa => movimentacaoCaixa.SaldoFinalDinheiro).Column("MOVCAIXA_SALDO_FINAL_DINHEIRO");
            Map(movimentacaoCaixa => movimentacaoCaixa.SaldoFinalCheque).Column("MOVCAIXA_SALDO_FINAL_CHEQUE");

            Map(movimentacaoCaixa => movimentacaoCaixa.ResultadoCaixa).Column("MOVCAIXA_RESULTADO_CAIXA").CustomType<EnumResultadoCaixa>();
            Map(movimentacaoCaixa => movimentacaoCaixa.DiferencaSaldoFinalCaixa).Column("MOVCAIXA_DIFERENCA_SALDO_FINAL");
            Map(movimentacaoCaixa => movimentacaoCaixa.Status).Column("MOVCAIXA_STATUS").CustomType<EnumStatusMovimentacaoCaixa>();

            References(movimentacaoCaixa => movimentacaoCaixa.Caixa).Column("MOVCAIXA_CAIXA").Not.LazyLoad().Fetch.Join();
            References(movimentacaoCaixa => movimentacaoCaixa.UsuarioAbertura).Column("MOVCAIXA_USUARIO_ABERTURA").Not.LazyLoad();
            References(movimentacaoCaixa => movimentacaoCaixa.UsuarioFechamento).Column("MOVCAIXA_USUARIO_FECHAMENTO").Not.LazyLoad();

            HasMany(movimentacaoCaixa => movimentacaoCaixa.ListaItensCaixa).KeyColumn("ITEMCAIXA_MOVIMENTACAO_CAIXA");
        }
    }
}
