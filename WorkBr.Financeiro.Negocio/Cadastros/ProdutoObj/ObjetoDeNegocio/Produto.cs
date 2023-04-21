using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;

namespace Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio
{
    [Serializable]
    public class Produto : ObjetoDeNegocioBase
    {
        public Produto()
        {
            DadosGerais = new DadosGeraisProduto();
            Principal = new PrincipalProduto();
            ContabilFiscal = new ContabilFiscalProduto();
            FormacaoPreco = new FormacaoPrecoProduto();
            Vestuario = new VestuarioProduto();

            ListaFornecedores = new List<FornecedorProduto>();
        }

        public virtual DadosGeraisProduto DadosGerais { get; set; }

        public virtual PrincipalProduto Principal { get; set; }

        public virtual ContabilFiscalProduto ContabilFiscal { get; set; }

        public virtual FormacaoPrecoProduto FormacaoPreco { get; set; }

        public virtual VestuarioProduto Vestuario { get; set; }

        public virtual IList<FornecedorProduto> ListaFornecedores { get; set; }
    }
}
