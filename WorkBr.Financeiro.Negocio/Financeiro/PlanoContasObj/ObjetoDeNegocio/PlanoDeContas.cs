using System;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio
{
    [Serializable]
    public class PlanoDeContas:ObjetoDeNegocioBase
    {
        public virtual string NumeroPlanoDeContas { get; set; }

        public virtual string Status{ get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Descricao { get; set; }

        public virtual EnumNaturezaPlanoContas? NaturezaPlanoContas { get; set; }

        public virtual EnumTipoPlanoContas? TipoPlanoContas { get; set; }

        public virtual bool PlanoDeContasPadrao { get; set; }

        public virtual string NumeroPlanoContasContador { get; set; }
    }
}
