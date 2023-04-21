using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.ConfiguracoesSistema.GrupoAcessoServ
{
    public class GrupoAcessoMap: MapeamentoBase<GrupoAcesso>
    {
        public GrupoAcessoMap()
        {
            Table("GRUPOSACESSOS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("GRPACESSO_ID");

            Map(usuario => usuario.Descricao).Column("GRPACESSO_DESCRICAO");
            Map(usuario => usuario.Tesoureiro).Column("GRPACESSO_TESOUREIRO");
        }
    }
}
