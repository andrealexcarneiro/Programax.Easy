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
    public class VwMovimentacaoSaidaItensMap : MapeamentoBase<VwMovimentacaoSaidaItens>
    {
        public VwMovimentacaoSaidaItensMap()
        {
            Table("vw_movimentacao_saida_itens");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("vwsaida_item_id");

            Map(item => item.DataMovimentacao).Column("vwsaida_data_fechamento");
            Map(item => item.ItemId).Column("vwsaida_item_id");
            Map(item => item.DescricaoItem).Column("vwsaida_item_descricao");
            Map(item => item.QuantidadeVendida).Column("vwsaida_quantidade_vendida");            
            Map(item => item.Saida).Column("vwsaida_saida");
            Map(item => item.Diferenca).Column("vwsaida_diferenca");
            Map(item => item.Estoque).Column("vw_estoque");           
        }
    }
}
