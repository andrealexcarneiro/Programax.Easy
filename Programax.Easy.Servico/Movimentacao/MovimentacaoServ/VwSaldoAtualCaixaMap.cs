using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Movimentacao.MovimentacaoServ
{
    public class VwSaldoAtualCaixaMap : MapeamentoBase<VWSaldoAtualCaixa>
    {
        public VwSaldoAtualCaixaMap()
        {
            Table("VW_SALDO_ATUAL_CAIXA");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ID");

            Map(saldo => saldo.SaldoAtualDinheiro).Column("SALDOATUALDINHEIRO");
        }
    }
}
