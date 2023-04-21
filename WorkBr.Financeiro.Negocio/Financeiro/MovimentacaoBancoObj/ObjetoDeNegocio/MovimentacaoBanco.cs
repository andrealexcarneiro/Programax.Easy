using System;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio
{
    [Serializable]
    public class MovimentacaoBanco: ObjetoDeNegocioBase
    {
        public virtual BancoParaMovimento Banco { get; set; }

        public virtual DateTime? DataHoraAbertura { get; set; }

        public virtual DateTime? DataHoraFechamento { get; set; }

        public virtual Pessoa UsuarioAbertura { get; set; }

        public virtual Pessoa UsuarioFechamento { get; set; }

        public virtual string ObservacoesAbertura { get; set; }

        public virtual string ObservacoesFechamento { get; set; }

        public virtual double SaldoInicial { get; set; }

        public virtual double SaldoFinal { get; set; }

        public virtual EnumStatusMovimentacaoCaixa Status { get; set; }

        public virtual IList<ItemMovimentacaoBanco> ListaItensBanco { get; set; }

        public virtual CategoriaFinanceira Categoria { get; set; }
    }
}
