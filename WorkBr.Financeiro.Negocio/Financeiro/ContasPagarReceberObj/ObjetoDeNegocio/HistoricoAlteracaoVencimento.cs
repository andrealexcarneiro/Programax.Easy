using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio
{
    [Serializable]
    public class HistoricoAlteracaoVencimento : ObjetoDeNegocioBase
    {
        public virtual int NumeroAlteracao { get; set; }

        public virtual Pessoa Usuario { get; set; }

        public virtual DateTime DataAlteracao { get; set; }

        public virtual DateTime DataVencimento { get; set; }

        public virtual double Valor { get; set; }

        public virtual double Multa { get; set; }

        public virtual double Juros { get; set; }

        public virtual double Desconto { get; set; }

        public virtual double ValorTotal { get; set; }

        public virtual string Observacoes { get; set; }

        public virtual ContaPagarReceber ContaPagarReceber { get; set; }
    }
}
