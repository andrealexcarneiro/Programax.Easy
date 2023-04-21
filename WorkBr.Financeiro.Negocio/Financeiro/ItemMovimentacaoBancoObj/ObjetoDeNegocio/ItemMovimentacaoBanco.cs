using System;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio
{
    [Serializable]
    public class ItemMovimentacaoBanco : ObjetoDeNegocioBase
    {
        public virtual EnumTipoMovimentacaoBanco TipoMovimentacao { get; set; }

        public virtual Pessoa Parceiro { get; set; }

        public virtual string DescricaoDaMovimentacao { get; set; }

        public virtual CategoriaFinanceira Categoria { get; set; }

        public virtual double Valor { get; set; }

        public virtual bool EstahEstornado { get; set; }

        public virtual DateTime DataHoraLancamento { get; set; }
        
        public virtual MovimentacaoBanco MovimentacaoBanco { get; set; }

        public virtual bool ItemDeEntrada { get; set; }

        public virtual EnumOrigemMovimentacaoBanco OrigemMovimentacaoBanco { get; set; }

        public virtual string NumeroDocumentoOrigem { get; set; }

        public virtual ContaPagarReceber ContaPagarReceber { get; set; }

        public virtual DateTime DataAtualizacao { get; set; }

        public virtual Pessoa UsuarioAtualizacao {get;set;}

        public virtual int ConciliacaoImportacaoId { get; set; }
    }
}
