using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using System.Data.SqlClient;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Infraestrutura.Negocio.Utils;
using System.Data.Common;

namespace Programax.Easy.Servico.Cadastros.EnderecoServ
{
    public class EnderecoMap : MapeamentoBase<Endereco>
    {
        public EnderecoMap()
        {
            Table("ENDERECOS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("END_ID");

            Map(endereco => endereco.Bairro).Column("END_BAIRRO");
            Map(endereco => endereco.CEP).Column("END_CEP");
            Map(endereco => endereco.Rua).Column("END_RUA");
            Map(endereco => endereco.DataCadastro).Column("END_DATA_CADASTRO");
            Map(endereco => endereco.Status).Column("END_STATUS");

            References(cidade => cidade.Cidade).Column("END_CID_ID").Not.LazyLoad().Fetch.Join();
        }
    }
}
