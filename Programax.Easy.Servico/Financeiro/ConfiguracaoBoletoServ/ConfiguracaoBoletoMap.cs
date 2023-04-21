using Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Financeiro.ConfiguracaoBoletoSev
{
    public class ConfiguracaoBoletoMap : MapeamentoBase<ConfiguracaoBoleto>
    {
        public ConfiguracaoBoletoMap()
        {
            Table("CONFIGURACAOBOLETO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CFB_ID");
            
            Map(configuracao => configuracao.Padrao).Column("CFB_PADRAO");            
            Map(configuracao => configuracao.Carteira).Column("CFB_CARTEIRA");
            Map(configuracao => configuracao.Variacao).Column("CFB_VARIACAO");
            Map(configuracao => configuracao.CodigoCedente).Column("CFB_CODIGO_CEDENTE");
            Map(configuracao => configuracao.DigitoCedente).Column("CFB_DIGITO_CEDENTE");
            Map(configuracao => configuracao.Modalidade).Column("CFB_MODALIDADE");
            Map(configuracao => configuracao.EspecieDocumento).Column("CFB_ESPECIE_DOCUMENTO");
            Map(configuracao => configuracao.NossoNumero).Column("CFB_NOSSO_NUMERO");
            Map(configuracao => configuracao.LocalPagamento).Column("CFB_LOCAL_PAGAMENTO");
            Map(configuracao => configuracao.CaminhoRemessa).Column("CFB_CAMINHO_REMESSA");
            Map(configuracao => configuracao.TipoDocumentoRemessa).Column("CFB_TIPO_DOCUMENTO_REMESSA");
            Map(configuracao => configuracao.ConvenioRemessa).Column("CFB_CONVENIO_REMESSA");
            Map(configuracao => configuracao.ProximoLote).Column("CFB_PROXIMO_LOTE");

            References(configuracao => configuracao.Perfil).Column("CFB_ID_PERFIL");
            References(configuracao => configuracao.Agencia).Column("CFB_AGENCIA");
            References(configuracao => configuracao.Banco).Column("CFB_BANCO");
            References(configuracao => configuracao.ContaBancaria).Column("CFB_CONTA_BANCARIA");

            HasMany(configuracao => configuracao.ListaInstrucoes).KeyColumn("INSTB_ID_CONFIGURACAO").Cascade.AllDeleteOrphan().Inverse().AsBag();//.Table("INSTRUCOESBOLETO");
           
        }
    }
}
