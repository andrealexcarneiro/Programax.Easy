using Programax.Easy.Negocio.Cadastros.OrigemClienteObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.OrigemClienteServ
{
    public class OrigemClienteMap : MapeamentoBase<OrigemCliente>
    {
        public OrigemClienteMap()
        {
            Table("ORIGENSCLIENTES");

            Id(OrigemCliente => OrigemCliente.Id).Column("ORIGEM_ID");

            Map(OrigemCliente => OrigemCliente.Descricao).Column("ORIGEM_DESCRICAO");
            Map(OrigemCliente => OrigemCliente.Status).Column("ORIGEM_STATUS");
            Map(OrigemCliente => OrigemCliente.DataCadastro).Column("ORIGEM_DATA_CADASTRO");
        }
    }
}
