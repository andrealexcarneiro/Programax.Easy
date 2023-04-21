using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio
{
    [Serializable]
    public class OperadorasCartao : ObjetoDeNegocioBase
    {  
        public virtual string Descricao {get;set;}

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Status { get; set; }

        public virtual int DiasPrazoParaCreditar {get; set; }

        public virtual bool PermiteParcelamento { get; set; }

        public virtual bool RecebimentoAntecipado { get; set; }

        public virtual BancoParaMovimento BancoParaMovimento { get; set; }

        public virtual CategoriaFinanceira CategoriaDeDespesa { get; set; }

        public virtual int CobrarTaxaApartirDaParcela { get; set; }

        public virtual double TaxaAdministracao { get; set; }
    }
}
