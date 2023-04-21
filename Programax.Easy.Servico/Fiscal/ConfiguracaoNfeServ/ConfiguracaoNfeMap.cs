using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Fiscal.ConfiguracaoNfeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ
{
    public class ConfiguracaoNfeMap:MapeamentoBase<ConfiguracaoNfe>
    {
        public ConfiguracaoNfeMap()
        {
            Table("CONFIGURACOESNFE");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CNFE_ID");
            
            Map(fiscal => fiscal.AliquotaSimplesNacional).Column("CNFE_ALIQUOTA_SIMPLES_NACIONAL");
            Map(fiscal => fiscal.ModeloNF).Column("CNFE_MODELO_ID").CustomType<EnumModeloNotaFiscal>();
            Map(fiscal => fiscal.FormatoImpressaoDanfe).Column("CNFE_FORMATO_IMPRESSAO_DANFE").CustomType<EnumFormatoImpressaoDanfe>();
            Map(fiscal => fiscal.NumeroNota).Column("CNFE_NUMERO_NOTA");
            Map(fiscal => fiscal.Serie).Column("CNFE_SERIE");
            Map(fiscal => fiscal.TipoAmbiente).Column("CNFE_TIPO_AMBIENTE").CustomType<EnumTipoAmbiente>();
            Map(fiscal => fiscal.NumeroSerieCertificado).Column("CNFE_NUMERO_SERIE_CERTIFICADO");
            Map(fiscal => fiscal.PadraoModeloNF).Column("CNFE_MODELO_PADRAO").CustomType<EnumModeloNotaFiscal>();
        }
    }
}
