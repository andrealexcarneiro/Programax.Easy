using Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using System;

namespace Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio
{
    [Serializable]
    public class VestuarioProduto
    {
        public virtual Tamanho Tamanho { get; set; }

        public virtual Cor Cor { get; set; }

        public virtual string MaterialTecido { get; set; }

        public virtual string Composicao { get; set; }

        public virtual string Referencia { get; set; }

        public virtual string Colecao { get; set; }

        public virtual EnumSexoProduto? SexoProduto { get; set; }

        public virtual string Modelo { get; set; }

        public virtual string DescricaoDetalhada { get; set; }
    }
}
