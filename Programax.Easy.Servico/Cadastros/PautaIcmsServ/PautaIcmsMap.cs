using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.PautaIcmsObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.PautaIcmsServ
{
    public class PautaIcmsMap : MapeamentoBase<PautaIcms>
    {
        public PautaIcmsMap()
        {
            Table("PAUTASICMS");

            Id(pautaIcms => pautaIcms.Id).Column("PAUTA_ID");

            Map(pautaIcms => pautaIcms.AliquotaSubstituicao).Column("PAUTA_ALIQUOTA_SUBSTITUICAO");
            Map(pautaIcms => pautaIcms.Codigo).Column("PAUTA_CODIGO");
            Map(pautaIcms => pautaIcms.DataInicio).Column("PAUTA_DATA_INICIO");
            Map(pautaIcms => pautaIcms.Instrucao).Column("PAUTA_INSTRUCAO");
            Map(pautaIcms => pautaIcms.PrecoPauta).Column("PAUTA_PRECO_PAUTA");
            Map(pautaIcms => pautaIcms.Status).Column("PAUTA_STATUS");
            Map(pautaIcms => pautaIcms.DataCadastro).Column("PAUTA_DATA_CADASTRO");

            References(pautaIcms => pautaIcms.Cidade).Column("PAUTA_CIDADE_ID");
            References(pautaIcms => pautaIcms.Estado).Column("PAUTA_ESTADO_ID");
            References(pautaIcms => pautaIcms.Produto).Column("PAUTA_PRODUTO_ID");
        }
    }
}
