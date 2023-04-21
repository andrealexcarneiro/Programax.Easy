using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using System;

namespace Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio
{
    [Serializable]
    public class BaixaItens : ObjetoDeNegocioBase
    {
        public virtual Produto Produto { get; set; }

        public virtual EnumTipoMovimentacao TipoMovimentacao { get; set; }

        public virtual double Quantidade { get; set; }

        public virtual MovimentacaoBase MovimentacaoBase { get; set; }

        public virtual int? PedidoVenda_Id { get; set; }

        public virtual int EstahEstornado { get; set; }

        public virtual int IdProduto { get; set; }

        public virtual string Nome { get; set; }
        public virtual string DataBaixa { get; set; }

        public virtual string Serie { get; set; }
        public virtual string Movimentacao { get; set; }

    }
}
