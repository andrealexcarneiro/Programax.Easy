using Programax.Easy.Negocio.ConfiguracoesSistema.InformacaoSistemaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.ConfiguracoesSistema.InformacaoSistemaServ
{
    public class InformacaoSistemaMap : MapeamentoBase<InformacaoSistema>
    {
        public InformacaoSistemaMap()
        {
            Table("INFORMACOESSISTEMA");

            Id(x => x.Id).GeneratedBy.Assigned().Column("INFO_ID");

            Map(usuario => usuario.Versao).Column("INFO_VERSAO");
            Map(usuario => usuario.DataVersao).Column("INFO_DATA_VERSAO");
        }
    }
}
