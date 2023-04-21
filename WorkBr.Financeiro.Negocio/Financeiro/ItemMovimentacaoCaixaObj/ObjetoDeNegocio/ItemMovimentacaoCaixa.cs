using System;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio
{
    [Serializable]
    public class ItemMovimentacaoCaixa : ObjetoDeNegocioBase
    {
        public virtual EnumTipoMovimentacaoCaixa TipoMovimentacao { get; set; }

        public virtual Pessoa Parceiro { get; set; }

        public virtual string HistoricoMovimentacoes { get; set; }

        public virtual FormaPagamento FormaPagamento { get; set; }

        public virtual CategoriaFinanceira CategoriaFinaceira { get; set; }

        public virtual double Valor { get; set; }

        public virtual bool EstahEstornado { get; set; }

        public virtual DateTime DataHora { get; set; }

        public virtual MovimentacaoCaixa MovimentacaoCaixa { get; set; }

        public virtual bool ItemDeEntrada { get; set; }

        public virtual EnumOrigemMovimentacaoCaixa OrigemMovimentacaoCaixa { get; set; }

        public virtual int? NumeroDocumentoOrigem { get; set; }
    }
}
