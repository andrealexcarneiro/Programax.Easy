using System;

namespace Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio
{
    [Serializable]
    public class FormacaoPrecoProduto
    {
        #region " INFORMAÇÕES DE COMPRA "

        public virtual double? PrecoCompra { get; set; }

        public virtual double? ValorFreteCompra { get; set; }

        public virtual double? PercentualIcmsCompra { get; set; }

        public virtual double? PercentualIpiCompra { get; set; }

        public virtual double? PercentualIcmsSTCompra { get; set; }

        public virtual double? PercentualReducaoIcmsCompra { get; set; }

        #endregion

        #region " INFORMAÇÕES DE VENDA "

        public virtual double? PercentualDespesasFixasVenda { get; set; }

        public virtual double? PercentualDespesasVariaveisVenda { get; set; }

        public virtual double? PercentualIcmsSimplesVenda { get; set; }

        public virtual double? PercentualReducaoIcmsVenda { get; set; }

        public virtual double? PercentualOutrasDespesasVenda { get; set; }

        public virtual double? PercentualFreteVenda { get; set; }

        public virtual double? PercentualComissoesVenda { get; set; }
        
        public virtual double Estoque { get; set; }

        public virtual double EstoqueReservado { get; set; }

        #endregion

        #region " DEFINIÇÃO DO PREÇO DE VENDA "

        public virtual double? PercentualLucro { get; set; }

        public virtual double? Markup { get; set; }

        public virtual double? ValorVenda { get; set; }

        public virtual bool? EhPromocao { get; set; }

        public virtual double? ValorPromocao { get; set; }

        public virtual double? PercentualDescontoMaximo { get; set; }

        #endregion

        #region "Comissões de Serviço"

        public virtual double? ValorEntrega { get; set; }

        public virtual double? ValorEntregaAposHorario { get; set; }

        public virtual double? ValorInstalacao { get; set; }

        public virtual double? ValorInstalacaoAposHorario { get; set; }

        public virtual double? ValorInstalacaoOutrasCidades { get; set; }

        public virtual double? ValorDeslocamentoEGarantia { get; set; }

        #endregion
    }
}
