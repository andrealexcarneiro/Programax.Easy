using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Fiscal.NotaFiscalServ
{
    public class NotaFiscalReferenciadaMap : MapeamentoBase<NotaFiscalReferenciada>
    {
        public NotaFiscalReferenciadaMap()
        {
            Table("NOTASFISCAIS_REFERENCIADAS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("NOTA_REF_ID");

            Map(nota => nota.AnoMesEmissao).Column("NOTA_REF_ANO_MES_EMISSAO");
            Map(nota => nota.ChaveDeAcesso).Column("NOTA_REF_CHAVE_ACESSO");
            Map(nota => nota.TipoPessoa).Column("NOTA_REF_TIPO_PESSOA").CustomType<EnumTipoPessoa>();
            Map(nota => nota.CnpjEmitente).Column("NOTA_REF_CNPJ_EMITENTE");
            Map(nota => nota.Coo).Column("NOTA_REF_COO");
            Map(nota => nota.CTe).Column("NOTA_REF_CTE");
            Map(nota => nota.InscricaoEstadual).Column("NOTA_REF_INSCRICAO_ESTADUAL");
            Map(nota => nota.ModeloDocumento).Column("NOTA_REF_MODELO_DOCUMENTO").CustomType<EnumModeloNotaFiscalReferenciada>();

            Map(nota => nota.NumeroEcf).Column("NOTA_REF_NUMERO_ECF");
            Map(nota => nota.NumeroNota).Column("NOTA_REF_NUMERO_NOTA");
            Map(nota => nota.SerieNota).Column("NOTA_REF_SERIE");
            Map(nota => nota.TipoNotaReferenciada).Column("NOTA_REF_TIPO_NOTA").CustomType<EnumTipoNotaReferenciada>();
            Map(nota => nota.CodigoUF).Column("NOTA_REF_CODIGO_UF");

            References(nota => nota.NotaFiscal).Column("NOTA_REF_NOTA_ID");
        }
    }
}
