using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio
{
    [Serializable]
    public class ContaPagarReceberPagamento : ObjetoDeNegocioBase
    {
        public virtual DateTime DataPagamento { get; set; }

        public virtual double Valor { get; set; }

        public virtual FormaPagamento FormaPagamento { get; set; }

        public virtual Pessoa Responsavel { get; set; }

        public virtual string Observacoes { get; set; }

        public virtual ContaPagarReceber ContaPagarReceber { get; set; }

        public virtual bool EstahEstornado { get; set; }
    }
}
