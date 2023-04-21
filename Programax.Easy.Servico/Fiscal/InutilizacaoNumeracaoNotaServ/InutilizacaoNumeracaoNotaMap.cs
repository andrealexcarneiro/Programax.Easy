using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Fiscal.InutilizacaoNumeracaoNotaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Estoque.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;

namespace Programax.Easy.Servico.Fiscal.InutilizacaoNumeracaoNotaServ
{
    public class InutilizacaoNumeracaoNotaMap : MapeamentoBase<InutilizacaoNumeracaoNota>
    {
        public InutilizacaoNumeracaoNotaMap()
        {
            Table("INUTILIZACAONUMERACAONOTA");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("INUT_ID");

            Map(inutilizacao => inutilizacao.Ano).Column("INUT_ANO");
            Map(inutilizacao => inutilizacao.Justificativa).Column("INUT_JUSTIFICATIVA");
            Map(inutilizacao => inutilizacao.ModeloNotaFiscal).Column("INUT_MODELO_NOTA_FISCAL").CustomType<EnumModeloNotaFiscal>();
            Map(inutilizacao => inutilizacao.NumeroFinal).Column("INUT_NUMERO_FINAL");
            Map(inutilizacao => inutilizacao.NumeroInicial).Column("INUT_NUMERO_INICIAL");
            Map(inutilizacao => inutilizacao.Serie).Column("INUT_SERIE");
            Map(inutilizacao => inutilizacao.Protocolo).Column("INUT_PROTOCOLO");
        }
    }
}

