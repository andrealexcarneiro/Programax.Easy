using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.ConfiguracoesSistema.ConfiguracoesPdvObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;

namespace Programax.Easy.Servico.ConfiguracoesSistema.ConfiguracoesPdvServ
{
    public class ConfiguracoesPdvMap : MapeamentoBase<ConfiguracoesPdv>
    {
        public ConfiguracoesPdvMap()
        {
            Table("CONFIGURACOESPDV");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CONFIG_ID");

            Map(config => config.TipoCartaoCreditoDebito).Column("CONFIG_TIPO_CARTAO").CustomType<EnumTipoCartaoCreditoEDebito>();

            Map(config => config.GereVendaAPartirDoPdv).Column("CONFIG_GERE_VENDA_PDV");

            Map(config => config.DescontoMaximoCaixa).Column("CONFIG_DESCONTO_MAXIMO_CAIXA");

            Map(config => config.FormaPagamentoEntradaIgualPedidoVenda).Column("CONFIG_FORMA_PAGAMENTO_ENTRADA_IGUAL_PEDIDO_VENDA");

            Map(config => config.EmitirNotaFiscalDiretamenteNoPDV).Column("CONFIG_EMITIR_NOTA_DIRETAMENTE_PDV");

            Map(config => config.PermitirFormaECondicaoPagamentoNoPDV).Column("CONFIG_PERMITIR_FORMA_E_CONDICAO_PDV");

            References(config => config.Cliente).Column("CONFIG_CLIENTE_PADRAO_ID");

            References(config => config.ClienteTemporario).Column("CONFIG_CLIENTE_TEMPORARIO_ID");
        }
    }
}
