using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.ComissaoServ
{
    public class ComissaoMap:MapeamentoBase<Comissao>
    {
        public ComissaoMap()
        {
            Table("COMISSOES");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("COMISSAO_ID");

            Map(comissao => comissao.ValorComissao).Column("COMISSAO_VALOR_COMISSAO");
            Map(comissao => comissao.DescontoMaximo).Column("COMISSAO_DESCONTO_MAXIMO");
            Map(comissao => comissao.FuncaoPessoaComissao).Column("COMISSAO_FUNCAO_PESSOA").CustomType<EnumFuncaoPessoaComissao>();
            Map(comissao => comissao.MetaFaturamento).Column("COMISSAO_META_FATURAMENTO");

            Map(comissao => comissao.DescontoMaximoEhPercentual).Column("COMISSAO_DESCONTO_MAXIMO_EH_PERCENTUAL");
            Map(comissao => comissao.ValorComissaoEhPercentual).Column("COMISSAO_VALOR_COMISSAO_EH_PERCENTUAL");

            References(comissao => comissao.Pessoa).Column("COMISSAO_PESSOA_ID");
            References(comissao => comissao.TabelaPreco).Column("COMISSAO_TABELA_PRECO_ID");
        }
    }
}
