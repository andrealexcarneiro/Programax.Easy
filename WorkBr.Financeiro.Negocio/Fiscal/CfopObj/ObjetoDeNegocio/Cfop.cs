using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;

namespace Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio
{
    [Serializable]
    public class Cfop:ObjetoDeNegocioBase
    {
        public virtual string Codigo { get; set; }

        public virtual string Descricao { get; set; }

        public virtual string Status { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual Cfop CfopDeConversao { get; set; }

        public virtual string InformacoesComplementaresNFe { get; set; }
    }
}
