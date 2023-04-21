using System;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio
{
    [Serializable]
    public class MovimentacaoCaixa: ObjetoDeNegocioBase
    {
        public virtual Caixa Caixa { get; set; }

        public virtual DateTime? DataHoraAbertura { get; set; }

        public virtual DateTime? DataHoraFechamento { get; set; }

        public virtual Pessoa UsuarioAbertura { get; set; }

        public virtual Pessoa UsuarioFechamento { get; set; }

        public virtual string ObservacoesAbertura { get; set; }

        public virtual string ObservacoesFechamento { get; set; }

        public virtual double SaldoInicial { get; set; }

        public virtual double SaldoInicialDinheiro { get; set; }

        public virtual double SaldoInicialCheque { get; set; }

        public virtual double SaldoFinalDinheiro { get; set; }

        public virtual double SaldoFinalCheque { get; set; }

        public virtual EnumResultadoCaixa ResultadoCaixa { get; set; }

        public virtual double DiferencaSaldoFinalCaixa { get; set; }

        public virtual EnumStatusMovimentacaoCaixa Status { get; set; }

        public virtual IList<ItemMovimentacaoCaixa> ListaItensCaixa { get; set; }

        public virtual CategoriaFinanceira CategoriaFinanceira { get; set; }
    }
}
