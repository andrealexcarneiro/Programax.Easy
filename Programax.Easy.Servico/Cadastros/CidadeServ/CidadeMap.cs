using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.CidadeServ
{
    public class CidadeMap : MapeamentoBase<Cidade>
    {
        public CidadeMap()
            : base()
        {
            Table("CIDADES");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CIDA_ID");

            Map(cidade => cidade.CodigoIbge).Column("CIDA_CODIBGE");
            Map(cidade => cidade.Descricao).Column("CIDA_DESCRICAO");
            Map(cidade => cidade.Status).Column("CIDA_STATUS");
            Map(cidade => cidade.DataCadastro).Column("CIDA_DATA_CADASTRO");

            References(cidade => cidade.Estado).Column("CIDA_ESTA_ID").Not.LazyLoad().Fetch.Join();
        }
    }
}
