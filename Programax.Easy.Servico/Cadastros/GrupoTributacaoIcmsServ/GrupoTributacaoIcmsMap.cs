using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.TributacaoIpiServ
{
    public class GrupoTributacaoIcmsMap : MapeamentoBase<GrupoTributacaoIcms>
    {
        public GrupoTributacaoIcmsMap()
        {
            Table("GRUPOTRIBUTACOESICMS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("GRPTRIB_ID");

            Map(grupoTrib => grupoTrib.Descricao).Column("GRPTRIB_DESCRICAO");
            Map(grupoTrib => grupoTrib.NaturezaProduto).Column("GRPTRIB_NATUREZA_OPERACAO").CustomType<EnumNaturezaProduto>();
            Map(grupoTrib => grupoTrib.RegimeTributario).Column("GRPTRIB_REGIME_TRIBUTARIO").CustomType<EnumCodigoRegimeTributario>();

            HasMany(grupoTrib => grupoTrib.ListaTributacoesIcms).KeyColumn("TRIBICMS_GRPTRIB_ICMS_ID").Cascade.AllDeleteOrphan().Inverse().AsBag().Table("TRIBUTACOESICMS");
        }
    }
}
