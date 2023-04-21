using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;

namespace Programax.Easy.Servico.Vendas.RoteiroServ
{
    public class RoteiroMap : MapeamentoBase<Roteiro>
    {
        public RoteiroMap()
        {
            Table("ROTEIROS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ROT_ID");
                      
            Map(roteiro => roteiro.Status).Column("ROT_STATUS").CustomType<EnumStatusRoteiro>();
            Map(roteiro => roteiro.Periodo).Column("ROT_PERIODO").CustomType<EnumPeriodo>();

            Map(roteiro => roteiro.Historico).Column("ROT_HISTORICO");
            Map(roteiro => roteiro.DataElaboracao).Column("ROT_DATA_ELABORACAO");
            Map(roteiro => roteiro.DataConclusao).Column("ROT_DATA_CONCLUSAO");

            Map(roteiro => roteiro.DetalheServico).Column("ROT_DETALHE_SERVICO");
            Map(roteiro => roteiro.Observacao).Column("ROT_OBSERVACAO");
            Map(roteiro => roteiro.TipoServico).Column("ROT_TIPO_SERVICO").CustomType<EnumTipoServico>();
            Map(roteiro => roteiro.TipoEndereco).Column("ROT_TIPO_ENDERECO").CustomType<EnumTipoServico>();

            Map(roteiro => roteiro.RoteirizacaoId).Column("ROT_ROTEIRIZACAO_ID");

            References(roteiro => roteiro.PessoaFuncionario).Column("ROT_PES_FUNC_ID").Not.LazyLoad().Fetch.Join();

            References(roteiro => roteiro.Usuario).Column("ROT_PES_USUARIO_ID");           
            
            References(roteiro => roteiro.PedidoVenda).Column("ROT_PEDIDO_ID");

        }
    }
}
