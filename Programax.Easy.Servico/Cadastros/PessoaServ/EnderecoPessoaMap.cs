using FluentNHibernate.Mapping;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.PessoaServ
{
    public class EnderecoPessoaMap : MapeamentoBase<EnderecoPessoa>
    {
        public EnderecoPessoaMap()
        {
            Table("ENDERECOSPESSOAS");

            Id(endereco => endereco.Id).GeneratedBy.Native().UnsavedValue(0).Column("ENDPES_ID");

            Map(endereco => endereco.Complemento).Column("ENDPES_COMPLEMENTO");
            Map(endereco => endereco.Numero).Column("ENDPES_NUMERO");
            Map(endereco => endereco.TipoEndereco).Column("ENDPES_TIPO_ENDERECO").CustomType<EnumTipoEndereco>();

            Map(endereco => endereco.CEP).Column("ENDPES_CEP");
            Map(endereco => endereco.Rua).Column("ENDPES_RUA");
            Map(endereco => endereco.Bairro).Column("ENDPES_BAIRRO");
            References(endereco => endereco.Cidade).Column("ENDPES_CIDADE_ID").Not.LazyLoad().Fetch.Join();

            References(endereco => endereco.Pessoa).Column("ENDPES_PES_ID");
        }
    }
}
