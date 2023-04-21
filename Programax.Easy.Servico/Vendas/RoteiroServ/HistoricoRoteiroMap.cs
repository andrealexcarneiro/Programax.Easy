using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;

namespace Programax.Easy.Servico.Vendas.RoteiroServ
{
    public class HistoricoRoteiroMap : MapeamentoBase<HistoricoRoteiro>
    {
        public HistoricoRoteiroMap()
        {
            Table("HISTORICOSROTEIRO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("HIS_ID");
            
            Map(roteiro => roteiro.DescricaoHistorico).Column("HIS_DESCRICAO_HISTORICO");
            Map(roteiro => roteiro.DataHistorico).Column("HIS_DATA_HISTORICO");
            
            References(roteiro => roteiro.Usuario).Column("HIS_PES_USUARIO_ID");
                      
            References(roteiro => roteiro.Roteirizacao).Column("HIS_ROTEIRO_ID");

        }
    }
}
