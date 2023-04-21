using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;

namespace Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio
{
    [Serializable]
    public class VwMovimentacaoProduto : ObjetoDeNegocioBase
    {
        public virtual DateTime DataMovimentacao { get; set; }

        public virtual int PessoaId { get; set; }

        public virtual string PessoaNome { get; set; }

        public virtual EnumTipoMovimentacao TipoMovimentacao { get; set; }

        public virtual EnumOrigemMovimentacao OrigemMovimentacao { get; set; }

        public virtual string Observacoes { get; set; }

        public virtual int ProdutoId { get; set; }

        public virtual string ProdutoDescricao { get; set; }

        public virtual double Quantidade { get; set; }        
    }
}
