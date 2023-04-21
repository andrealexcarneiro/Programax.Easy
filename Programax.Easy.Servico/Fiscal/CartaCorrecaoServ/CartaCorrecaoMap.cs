using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Fiscal.CartaCorrecaoObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Fiscal.CartaCorrecaoServ
{
    public class CartaCorrecaoMap : MapeamentoBase<CartaCorrecao>
    {
        public CartaCorrecaoMap()
        {
            Table("CARTASCORRECOES");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CARTA_CORRECAO_ID");

            Map(condicao => condicao.Correcao).Column("CARTA_CORRECAO_CORRECAO");
            Map(condicao => condicao.DataHoraEmissao).Column("CARTA_CORRECAO_DATA_HORA_EMISSAO");
            Map(condicao => condicao.SequenciaEvento).Column("CARTA_CORRECAO_SEQUENCIA_EVENTO");
            Map(condicao => condicao.NumeroProtocolo).Column("CARTA_CORRECAO_NUMERO_PROTOCOLO");

            References(condicao => condicao.NotaFiscal).Column("CARTA_CORRECAO_NOTA_ID");
        }
    }
}
