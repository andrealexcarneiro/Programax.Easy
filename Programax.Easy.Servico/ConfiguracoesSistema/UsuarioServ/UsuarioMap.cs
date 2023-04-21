using Programax.Easy.Negocio.ConfiguracoesSistema.UsuarioObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.ConfiguracoesSistema.UsuarioServ
{
    public class UsuarioMap: MapeamentoBase<Usuario>
    {
        public UsuarioMap()
        {
            Table("USUARIOS");

            Id(x => x.Id).GeneratedBy.Assigned().Column("USER_IDE");

            Map(usuario => usuario.Login).Column("USER_LOGIN");

            Map(usuario => usuario.Senha).Column("USER_SENHA");
            Map(usuario => usuario.Ativo).Column("USER_STATUS");

            Map(usuario => usuario.DataCadastro).Column("USER_DATA_CADASTRO");

            References(usuario => usuario.GrupoAcesso).Column("USER_GRPACESSO_ID");
        }
    }
}
