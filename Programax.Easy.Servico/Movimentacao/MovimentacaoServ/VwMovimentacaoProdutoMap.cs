using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using FluentNHibernate.Mapping;

namespace Programax.Easy.Servico.Movimentacao.MovimentacaoServ
{
    public class VwMovimentacaoProdutoMap : MapeamentoBase<VwMovimentacaoProduto>
    {
        public VwMovimentacaoProdutoMap()
        {
            Table("VW_MOVIMENTACOES_PRODUTOS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("VWMOVITEM_ID");

            Map(item => item.DataMovimentacao).Column("VWMOVITEM_DATA");
            Map(item => item.PessoaId).Column("VWMOVITEM_PESSOA_ID");
            Map(item => item.PessoaNome).Column("VWMOVITEM_PESSOA_NOME");
            Map(item => item.TipoMovimentacao).Column("VWMOVITEM_TIPO_MOVIMENTO").CustomType<GenericEnumMapper<EnumTipoMovimentacao>>();
            Map(item => item.OrigemMovimentacao).Column("VWMOVITEM_ORIGEM_MOVIMENTO").CustomType<GenericEnumMapper<EnumOrigemMovimentacao>>();
            Map(item => item.Observacoes).Column("VWMOVITEM_OBSERVACAO");
            Map(item => item.ProdutoId).Column("VWMOVITEM_PRODUTO_ID");
            Map(item => item.ProdutoDescricao).Column("VWMOVITEM_PRODUTO_DESCRICAO");
            Map(item => item.Quantidade).Column("VWMOVITEM_QUANTIDADE");            
        }
    }
}
