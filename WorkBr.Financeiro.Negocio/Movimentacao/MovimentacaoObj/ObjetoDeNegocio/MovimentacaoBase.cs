using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio
{
    [Serializable]
    public class MovimentacaoBase: ObjetoDeNegocioBase
    {
        public MovimentacaoBase()
        {
            ListaDeItens = new List<ItemMovimentacao>();
        }

        public virtual EnumTipoMovimentacao TipoMovimentacao { get; set; }

        public virtual EnumOrigemMovimentacao OrigemMovimentacao { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Observacoes { get; set; }

        public virtual MotivoCorrecaoEstoque Motivo { get; set; }

        public virtual Pessoa FornecedorOuCliente { get; set; }

        public virtual Pessoa PessoaCadastro { get; set; }

        public virtual IList<ItemMovimentacao> ListaDeItens { get; set; }

        public virtual DateTime? DataMovimentacao { get; set; }
    }
}
