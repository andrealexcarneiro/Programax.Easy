using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio
{
    [Serializable]
    public class FornecedorProduto : ObjetoDeNegocioBase
    {
        public virtual Pessoa Fornecedor { get; set; }

        public virtual Produto Produto { get; set; }

        public virtual string CodigoProduto { get; set; }
    }
}
