using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio
{
    [Serializable]
    public class CondicaoPagamento : ObjetoDeNegocioBase
    {
        public CondicaoPagamento()
        {
            ListaDeParcelas = new List<ParcelaCondicaoPagamento>();
        }

        public virtual string Descricao { get; set; }

        public virtual string Status { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual bool EstahDisponivelParaContasAPagar { get; set; }

        public virtual bool EstahDisponivelParaContasAReceber { get; set; }

        public virtual bool EstahDisponivelParaPdv { get; set; }

        public virtual bool PrecisaDaLiberacaoDoGerente { get; set; }

        public virtual bool EstahDisponivelAcimaDeDeterminadoValor { get; set; }

        public virtual double? ValorQueEstaraDisponivel { get; set; }

        public virtual bool CondicaoPadraoAVista { get; set; }

        public virtual IList<ParcelaCondicaoPagamento> ListaDeParcelas { get; set; }
    }
}
