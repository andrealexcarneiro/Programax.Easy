using Programax.Easy.Negocio.ConfiguracoesSistema.PermissaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;

namespace Programax.Easy.Servico.ConfiguracoesSistema.PermissaoServ
{
    public class PermissaoMap: MapeamentoBase<Permissao>
    {
        public PermissaoMap()
        {
            Table("PERMISSOES");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PERM_ID");

            Map(permissao => permissao.Funcionalidade).Column("PERM_FUNCIONALIDADE").CustomType<EnumFuncionalidade>();
            Map(permissao => permissao.NomeMenu).Column("PERM_NOME_MENU");

            Map(permissao => permissao.Alterar).Column("PERM_ALTERAR");
            Map(permissao => permissao.Acessar).Column("PERM_ACESSAR");

            References(permissao => permissao.GrupoAcesso).Column("PERM_GRPACESSO_ID");
        }
    }
}
