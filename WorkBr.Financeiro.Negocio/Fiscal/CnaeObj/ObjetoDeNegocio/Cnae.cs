using System;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.CnaeObj.ObjetoDeNegocio
{
    [Serializable]
    public class Cnae : ObjetoDeNegocioBase
    {
        public virtual string Codigo { get; set; }

        public virtual string Descricao { get; set; }

        public virtual EnumAtividadeCnae? Atividade { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Status { get; set; }
    }
}
