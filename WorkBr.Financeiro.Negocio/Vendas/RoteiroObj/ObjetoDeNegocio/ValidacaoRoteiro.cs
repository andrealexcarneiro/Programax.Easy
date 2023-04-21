using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
namespace Programax.Easy.Negocio.Vendas.RoteiroObj
{
    public class ValidacaoRoteiro : ValidacaoBase<Roteiro>
    {
        #region " VARIÁVEIS PRIVADAS "

        private Roteiro _Roteiro;

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            RegrasComunsAInclusaoEAtualizacao();
        }

        public override void ValideAtualizacao()
        {
            RegrasComunsAInclusaoEAtualizacao();
        }
        #endregion

        #region " REGRAS "

        private void AssineRegraParceiroObrigatorio()
        {
            //RuleFor(rota => rota.PessoaFuncionario)
            //    .NotNull()
            //    .WithMessage("Funcionário ou Técnico não informado.");
        }

        private void AssineRegraPedidoObrigatorio()
        {
            RuleFor(rota => rota.PedidoVenda)
                .NotNull()
                .WithMessage("Número do Pedido não informado.");
        }

        
        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraParceiroObrigatorio();
            AssineRegraPedidoObrigatorio();
        }
        #endregion
    }
}
