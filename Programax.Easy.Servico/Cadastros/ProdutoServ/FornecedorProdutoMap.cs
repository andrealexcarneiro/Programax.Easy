using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.ProdutoServ
{
    public class FornecedorProdutoMap : MapeamentoBase<FornecedorProduto>
    {
        public FornecedorProdutoMap()
        {
            Table("PRODUTOSFORNECEDORES");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PRODFORN_ID");

            Map(vwProduto => vwProduto.CodigoProduto).Column("PRODFORN_CODIGO_PRODUTO_FORNECEDOR");

            References(vwProduto => vwProduto.Fornecedor).Column("PRODFORN_PESSOA_ID");
            References(vwProduto => vwProduto.Produto).Column("PRODFORN_PRODUTO_ID");
        }
    }
}
