using FluentNHibernate.Mapping;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.PessoaServ
{
    public class TelefoneMap : MapeamentoBase<Telefone>
    {
        public TelefoneMap()
        {
            Table("TELEFONES");

            Id(telefone => telefone.Id).GeneratedBy.Native().UnsavedValue(0).Column("TELE_ID");

            Map(telefone => telefone.Ddd).Column("TELE_DDD");
            Map(telefone => telefone.Numero).Column("TELE_FONE");
            Map(telefone => telefone.Observacao).Column("TELE_OBSERVACAO");
            Map(telefone => telefone.TipoTelefone).Column("TELE_TIPO").CustomType<GenericEnumMapper<EnumTipoTelefone>>();

            References(telefone => telefone.Pessoa).Column("TELE_PES_ID");
        }
    }
}
