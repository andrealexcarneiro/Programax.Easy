using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;

namespace Programax.Easy.Servico.Cadastros.CashBack.CashBackServ
{
    [Serializable]
    public class Cashback : ObjetoDeNegocioBase
    {
        public virtual string Codigo { get; set; }

        public virtual int start { get; set; }

        public virtual DateTime DataInicio { get; set; }

        public virtual DateTime DataFim { get; set; }

        public virtual string Valor { get; set; }
        public virtual string Percentual { get; set; }
        public virtual string Dias { get; set; }
    }
}
