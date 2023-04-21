using Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Financeiro.ConfiguracaoBoletoSev
{
    public class InstrucoesBoletoMap : MapeamentoBase<InstrucoesBoleto>
    {
        public InstrucoesBoletoMap()
        {
            Table("INSTRUCOESBOLETO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("INSTB_ID");

            Map(configuracao => configuracao.Item).Column("INSTB_ITEM");
            Map(configuracao => configuracao.CodigoInstrucao).Column("INSTB_CODIGO_INSTRUCAO");
            Map(configuracao => configuracao.DescricaoInstrucao).Column("INSTB_DESCRICAO_INSTRUCAO");
            Map(configuracao => configuracao.Dias).Column("INSTB_DIAS");
            Map(configuracao => configuracao.Valor).Column("INSTB_VALOR");
            Map(configuracao => configuracao.TipoValor).Column("INSTB_TIPO_VALOR");
            
            References(configuracao => configuracao.ConfiguracaoBoleto).Column("INSTB_ID_CONFIGURACAO");

        }
    }
}
