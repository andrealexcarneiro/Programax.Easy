using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Infraestrutura.Servico.Mapeamentos;
using FluentNHibernate.Mapping;

namespace Programax.Easy.Servico.Movimentacao.MovimentacaoServ
{
    public class MovimentacaoBaseMap : MapeamentoBase<MovimentacaoBase>
    {
        public MovimentacaoBaseMap()
        {
            Table("MOVIMENTACOES");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("MOV_ID");

            Map(movimentacao => movimentacao.DataCadastro).Column("MOV_DATA_CADASTRO");
            Map(movimentacao => movimentacao.OrigemMovimentacao).Column("MOV_ORIGEM_MOVIMENTO").CustomType<GenericEnumMapper<EnumOrigemMovimentacao>>();

            Map(movimentacao => movimentacao.TipoMovimentacao).Column("MOV_TIPO").CustomType<GenericEnumMapper<EnumTipoMovimentacao>>();

            Map(movimentacao => movimentacao.Observacoes).Column("MOV_OBSERVACOES");

            Map(movimentacao => movimentacao.DataMovimentacao).Column("MOV_DATA_MOVIMENTACAO");

            References(movimentacao => movimentacao.PessoaCadastro).Column("MOV_PES_ID");
            References(movimentacao => movimentacao.Motivo).Column("MOV_MOTIVO_ID");
            References(entrada => entrada.FornecedorOuCliente).Column("MOV_FORNECEDOR_ID").Not.LazyLoad();

            HasMany(movimentacao => movimentacao.ListaDeItens).KeyColumn("ITEMMOV_MOV_ID").Cascade.AllDeleteOrphan().Inverse().AsBag();
        }
    }
}
