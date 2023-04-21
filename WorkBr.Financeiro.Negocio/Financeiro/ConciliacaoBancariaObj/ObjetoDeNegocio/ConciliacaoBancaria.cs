using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.ObjetoDeNegocio
{   
    [Serializable]
    public class ConciliacaoBancaria : ObjetoDeNegocioBase
    {
        public virtual int? ChaveOrigem1 { get; set; } //Pode ser a chave primaria do Contas Pagar ou Receber ou Movimentação Bancária
        public virtual EnumOrigemConciliacaoBancaria? Origem1 { get; set; }
        public virtual string NumDoc { get; set; }
        public virtual string DescricaoDoc { get; set; }
        public virtual DateTime? DataVencimento { get; set; }
        public virtual double? ValorDoc { get; set; }
        public virtual EnumOrigemConciliacaoBancaria? Origem2 { get; set; }
        public virtual string NumLancto { get; set; }
        public virtual string DescricaoLancto { get; set; }
        public virtual DateTime DataLancto { get; set; }
        public virtual double ValorLancto { get; set; }
        public virtual EnumOrigemMovimentacaoBanco StatusConciliacao { get; set; }
        public virtual MovimentacaoBanco MovimentacaoBanco { get; set;}
    }
}
