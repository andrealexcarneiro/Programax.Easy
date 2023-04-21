using System;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio
{
    [Serializable]
    public class PrincipalProduto
    {
        public virtual double? PesoBruto { get; set; }

        public virtual double? PesoLiquido { get; set; }

        public virtual double? QuantidadeMinima { get; set; }

        public virtual double? QuantidadeMaxima { get; set; }

        public virtual Marca Marca { get; set; }

        public virtual Fabricante Fabricante { get; set; }

        public virtual Produto ProdutoSimilar { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual Grupo Grupo { get; set; }

        public virtual SubGrupo SubGrupo { get; set; }

        public virtual string Locacao { get; set; }

        public virtual string Observacao { get; set; }

        public virtual string CodigoFabricante { get; set; }
    }
}
