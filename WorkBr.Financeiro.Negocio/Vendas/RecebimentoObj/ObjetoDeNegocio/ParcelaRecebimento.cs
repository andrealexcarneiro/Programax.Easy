using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Vendas.RecebimentoObj.ObjetoDeNegocio
{
    public class ParcelaRecebimento:ObjetoDeNegocioBase
    {
        public virtual DateTime DataVencimento { get; set; }

        public virtual string NumeroDocumento { get; set; }

        public virtual string NumeroParcela { get; set; }

        public virtual double Valor { get; set; }

        public virtual int CondicaoPagamentoId { get; set; }

        public virtual int FormaPagamentoId { get; set; }

        public virtual EnumTipoFormaPagamento TipoFormaPagamento { get; set; }

        public virtual EnumTipoDocumentoRecebimento TipoDocumentoRecebimento { get; set; }

        public virtual Recebimento Recebimento { get; set; }

        public virtual OperadorasCartao OperadorasCartao { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Recebimento;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.Id == other.Id && this.TipoDocumentoRecebimento == other.TipoDocumento;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                hash = (hash * 31) ^ Id.GetHashCode();
                hash = (hash * 31) ^ TipoDocumentoRecebimento.GetHashCode();

                return hash;
            }
        }
    }
}
