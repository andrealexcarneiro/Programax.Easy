using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.TeleMarketing.Enumeradores;

namespace Programax.Easy.Servico.Telemarketing.TmkServ
{
    public class HistoricoAtendimentoMap : MapeamentoBase<HistoricoAtendimento>
    {
        public HistoricoAtendimentoMap()
        {
            Table("HISTORICOSATENDIMENTO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("HISAT_ID");
            
            Map(histAtendimento => histAtendimento.DescricaoHistorico).Column("HISAT_DESCRICAO_HISTORICO");
            Map(histAtendimento => histAtendimento.DataHistorico).Column("HISAT_DATA_HISTORICO");
            Map(histAtendimento => histAtendimento.TempoDuracao).Column("HISAT_TEMPO_DURACAO");
           
            Map(histAtendimento => histAtendimento.Status).Column("HISAT_STATUS").CustomType<EnumStatusAtendimento>();
            Map(histAtendimento => histAtendimento.contador).Column("hisat_contador");
            Map(histAtendimento => histAtendimento.codCliente).Column("hisat_cliente");

            References(histAtendimento => histAtendimento.Usuario).Column("HISAT_PES_USUARIO_ID");
                      
            References(histAtendimento => histAtendimento.Pedido).Column("HISAT_PEDIDO_ID");

            References(histAtendimento => histAtendimento.NovoPedido).Column("HISAT_NOVO_PEDIDO_ID");
        }
    }
}
