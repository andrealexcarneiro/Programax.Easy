using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.MovimentacaoBancoServ
{
    public class MovimentacaoBancoMap:MapeamentoBase<MovimentacaoBanco>
    {
        public MovimentacaoBancoMap()
        {
            Table("MOVIMENTACOESBANCO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("MOVBANCO_ID");

            Map(movimentacaoBanco => movimentacaoBanco.DataHoraAbertura).Column("MOVBANCO_DATA_HORA_ABERTURA");
            Map(movimentacaoBanco => movimentacaoBanco.DataHoraFechamento).Column("MOVBANCO_DATA_HORA_FECHAMENTO");
            Map(movimentacaoBanco => movimentacaoBanco.ObservacoesAbertura).Column("MOVBANCO_OBSERVACOES_ABERTURA");
            Map(movimentacaoBanco => movimentacaoBanco.ObservacoesFechamento).Column("MOVBANCO_OBSERVACOES_FECHAMENTO");
            Map(movimentacaoBanco => movimentacaoBanco.SaldoInicial).Column("MOVBANCO_SALDO_INICIAL");
            Map(movimentacaoBanco => movimentacaoBanco.SaldoFinal).Column("MOVBANCO_SALDO_FINAL");
            Map(movimentacaoBanco => movimentacaoBanco.Status).Column("MOVBANCO_STATUS").CustomType<EnumStatusMovimentacaoCaixa>();

            References(movimentacaoBanco => movimentacaoBanco.Banco).Column("MOVBANCO_BANCO").Not.LazyLoad().Fetch.Join();
            References(movimentacaoBanco => movimentacaoBanco.UsuarioAbertura).Column("MOVBANCO_USUARIO_ABERTURA").Not.LazyLoad();
            References(movimentacaoBanco => movimentacaoBanco.UsuarioFechamento).Column("MOVBANCO_USUARIO_FECHAMENTO").Not.LazyLoad();

            HasMany(movimentacaoBanco => movimentacaoBanco.ListaItensBanco).KeyColumn("ITEMBANCO_MOVIMENTACAO_BANCO");
        }
    }
}
