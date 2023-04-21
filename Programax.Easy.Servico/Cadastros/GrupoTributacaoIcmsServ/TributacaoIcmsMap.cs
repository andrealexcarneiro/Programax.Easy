using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.GrupoTributacaoIcmsServ
{
    public class TributacaoIcmsMap : MapeamentoBase<TributacaoIcms>
    {
        public TributacaoIcmsMap()
        {
            Table("TRIBUTACOESICMS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("TRIBICMS_ID");

            Map(grupoTrib => grupoTrib.AliquotaCreditoST).Column("TRIBICMS_ALIQUOTA_CREDITO_ST");
            Map(grupoTrib => grupoTrib.AliquotaDebitoST).Column("TRIBICMS_ALIQUOTA_DEBITO_ST");
            Map(grupoTrib => grupoTrib.MVA).Column("TRIBICMS_MVA");
            Map(grupoTrib => grupoTrib.ReducaoBaseST).Column("TRIBICMS_REDUCAO_BASE_ST");
            Map(grupoTrib => grupoTrib.IcmsBaseCalculo).Column("TRIBICMS_ICMS_BASE_CALCULO");
            Map(grupoTrib => grupoTrib.IcmsReducaoBaseCalculo).Column("TRIBICMS_REDUCAO_BASE_CALCULO");
            Map(grupoTrib => grupoTrib.ModalidadeBaseCalculo).Column("TRIBICMS_MODALIDADE_BASE_CALCULO").CustomType<EnumModBC>();
            Map(grupoTrib => grupoTrib.AliquotaIcmsST).Column("TRIBICMS_ALIQUOTA_ICMS_ST");
            Map(grupoTrib => grupoTrib.PercentualMargemAdicST).Column("TRIBICMS_MARGEM_VALOR_ADIC_ICMS_ST");
            Map(grupoTrib => grupoTrib.ModalidadeIcmsST).Column("TRIBICMS_MODALIDADE_BASE_CALCULO_ST").CustomType<EnumModBCST>();
            Map(grupoTrib => grupoTrib.EstadoDestino).Column("TRIBICMS_ESTADO_DESTINO");
            Map(grupoTrib => grupoTrib.CstCsosn).Column("TRIBICMS_CSTCSOSN").CustomType<EnumCstCsosn>();
            Map(grupoTrib => grupoTrib.TipoCliente).Column("TRIBICMS_TIPO_CLIENTE").CustomType<EnumTipoCliente>();
            Map(grupoTrib => grupoTrib.TipoInscricaoICMS).Column("TRIBICMS_TIPO_INSCRICAO_ICMS").CustomType<EnumTipoInscricaoICMS>();
            Map(grupoTrib => grupoTrib.TipoSaida).Column("TRIBICMS_TIPO_SAIDA").CustomType<EnumTipoSaidaTributacaoIcms>();

            References(grupoTrib => grupoTrib.Cfop).Column("TRIBICMS_CFOP");
            References(grupoTrib => grupoTrib.GrupoTributacaoIcms).Column("TRIBICMS_GRPTRIB_ICMS_ID");
        }
    }
}
