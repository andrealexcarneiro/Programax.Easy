using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubEstoqueObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;

namespace Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio
{
    [Serializable]
    public class ItemTransferencia:ObjetoDeNegocioBase
    {
        public virtual int produto { get; set; }

        public virtual double QuantidadeEstoque { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual int SubEstoque { get; set; }

        public virtual string Descricao { get; set; }
        public virtual string Unidade { get; set; }
    }
}
