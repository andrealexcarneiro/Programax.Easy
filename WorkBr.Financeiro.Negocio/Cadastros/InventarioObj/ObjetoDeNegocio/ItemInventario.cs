using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;

namespace Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio
{
    [Serializable]
    public class ItemInventario:ObjetoDeNegocioBase
    {
        public virtual Produto Produto { get; set; }

        public virtual double QuantidadeEstoque { get; set; }

        public virtual double? QuantidadeContagemUm { get; set; }

        public virtual double? QuantidadeContagemDois { get; set; }

        public virtual double? QuantidadeContagemTres { get; set; }

        public virtual Inventario Inventario { get; set; }
    }
}
