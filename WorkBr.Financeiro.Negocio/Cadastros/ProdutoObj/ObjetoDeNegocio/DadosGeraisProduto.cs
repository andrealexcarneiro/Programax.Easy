using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio;
using System;

namespace Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio
{
    [Serializable]
    public class DadosGeraisProduto
    {
        public virtual byte[] Foto { get; set; }

        public virtual string Descricao { get; set; }

        public virtual string Status { get; set; }

        public virtual string CodigoDeBarras { get; set; }

        public virtual UnidadeMedida Unidade { get; set; }

        public virtual bool ProdutoEmInventario { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual bool PermiteVendaFracionada { get; set; }
    }
}
