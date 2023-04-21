using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.RamoAtividadeServ
{
    public class RamoAtividadeMap : MapeamentoBase<RamoAtividade>
    {
        public RamoAtividadeMap()
        {
            Table("RAMOATIVIDADES");

            Id(ramoAtividade => ramoAtividade.Id).Column("RAMO_ID");

            Map(ramoAtividade => ramoAtividade.Descricao).Column("RAMO_DESCRICAO");
            Map(ramoAtividade => ramoAtividade.Status).Column("RAMO_STATUS");
            Map(ramoAtividade => ramoAtividade.DataCadatro).Column("RAMO_DATA_CADASTRO");
        }
    }
}
