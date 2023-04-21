using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.PessoaServ
{
    public class VWEnderecosPessoasMap : MapeamentoBase<VWEnderecosPessoas>
    {
        public VWEnderecosPessoasMap()
        {
            Table("VW_ENDERECOS_PESSOAS");

            ReadOnly();

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("VWENDPES_ID");

            Map(x => x.PessoaId).Column("VWENDPES_PES_ID");
            Map(x => x.Bairro).Column("VWENDPES_BAIRRO");
            Map(x => x.CidadeId).Column("VWENDPES_CIDADE_ID");
            Map(x => x.CidadeNome).Column("VWENDPES_CIDADE_DESCRICAO");
            Map(x => x.EstadoUF).Column("VWENDPES_ESTADO_UF");
            Map(x => x.EstadoNome).Column("VWENDPES_ESTADO_NOME");
        }
    }
}
