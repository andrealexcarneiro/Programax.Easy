using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.NaturezaOperacaoServ
{
    public class NaturezaOperacaoMap:MapeamentoBase<NaturezaOperacao>
    {
        public NaturezaOperacaoMap()
        {
            Table("NATUREZASOPERACOES");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("NATUREZA_ID");

            Map(motivo => motivo.DataCadastro).Column("NATUREZA_DATA_CADASTRO");
            Map(motivo => motivo.Descricao).Column("NATUREZA_DESCRICAO");
            Map(motivo => motivo.GeraTitulosFinanceiro).Column("NATUREZA_GERA_TITULO_FINANCEIRO");
            Map(motivo => motivo.ObrigatorioExistirPedidoVenda).Column("NATUREZA_OBRIGATORIO_EXISTE_PEDIDO");
            Map(motivo => motivo.RealizaMovimentacaoEstoque).Column("NATUREZA_REALIZA_MOVIMENTACAO_ESTOQUE");
            Map(motivo => motivo.OrigemDestino).Column("NATUREZA_ORIGEM_DESTINO").CustomType<EnumOrigemDestino>();
            Map(motivo => motivo.TipoMovimentacao).Column("NATUREZA_TIPO_MOVIMENTACAO").CustomType<EnumTipoMovimentacaoNaturezaOperacao>();
            Map(motivo => motivo.Status).Column("NATUREZA_STATUS");

            References(motivo => motivo.PlanoDeContas).Column("NATUREZA_PLANO_CONTAS_ID");

            HasMany(motivo => motivo.ListaCfops).KeyColumn("NATUREZACFOP_NATUREZA_ID").Cascade.AllDeleteOrphan().Inverse().AsBag();
        }
    }
}
