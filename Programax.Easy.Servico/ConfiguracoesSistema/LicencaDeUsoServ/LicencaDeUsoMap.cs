using Programax.Easy.Negocio.ConfiguracoesSistema.LicencaDeUsoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.ConfiguracoesSistema.LicencaDeUsoServ
{
    public class LicencaDeUsoMap:MapeamentoBase<LicencaDeUso>
    {
        public LicencaDeUsoMap()
        {
            Table("LICENCADEUSO");

            Id(x => x.Id).GeneratedBy.Assigned().Column("LICENCA_ID");

            Map(usuario => usuario.Contrato).Column("LICENCA_CONTRATO");
            Map(usuario => usuario.IdDatabase).Column("LICENCA_ID_DATABASE");
            Map(usuario => usuario.LiberadoAte).Column("LICENCA_LIBERADO_ATE");
            Map(usuario => usuario.QuantidadeUsuariosContratados).Column("LICENCA_QTD_USERS_CONTRATADOS");
            Map(usuario => usuario.ChaveLiberacao).Column("LICENCA_CHAVE_LIBERACAO");
        }
    }
}
