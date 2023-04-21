using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio
{
    [Serializable]
    public class NaturezaOperacaoCfop:ObjetoDeNegocioBase
    {
        public virtual NaturezaOperacao NaturezaOperacao { get; set; }

        public virtual Cfop Cfop { get; set; }
    }
}
