using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.PessoaServ
{
    public class AtendimentoMap : MapeamentoBase<Atendimento>
    {
        public AtendimentoMap()
        {
            Table("PESSOASATENDIMENTO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PESAT_ID");

           References(x => x.CondicaoDePagamento).Column("PESAT_COPAG_ID").Not.LazyLoad().Fetch.Join();
           References(x => x.FormaPagamento).Column("PESAT_FORPAG_ID").Not.LazyLoad().Fetch.Join();
           References(x => x.TabelaDePreco).Column("PESAT_TBPRE_ID").Not.LazyLoad().Fetch.Join();

           References(x => x.Supervisor).Column("PESAT_SUPERVISOR_ID").LazyLoad();
           References(x => x.Atendente).Column("PESAT_ATENDENTE_ID").Not.LazyLoad();
           References(x => x.Vendedor).Column("PESAT_VENDEDOR_ID").Not.LazyLoad();
           References(x => x.Indicador).Column("PESAT_INDICADOR_ID").LazyLoad();

           References(x => x.OrigemCliente).Column("PESAT_ORIGEM_ID").Not.LazyLoad().Fetch.Join();
           Map(x => x.Observacoes).Column("PESAT_OBS");
        }
    }
}
