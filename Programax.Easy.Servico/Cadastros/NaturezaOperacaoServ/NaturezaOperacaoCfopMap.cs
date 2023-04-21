using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.NaturezaOperacaoServ
{
    public class NaturezaOperacaoCfopMap : MapeamentoBase<NaturezaOperacaoCfop>
    {
        public NaturezaOperacaoCfopMap()
        {
            Table("NATUREZASOPERACOESCFOP");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("NATUREZACFOP_ID");

            References(motivo => motivo.Cfop).Column("NATUREZACFOP_CFOP_ID").Not.LazyLoad().Fetch.Join();
            References(motivo => motivo.NaturezaOperacao).Column("NATUREZACFOP_NATUREZA_ID");
        }
    }
}
