using FluentValidation;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio
{
    public class ValidacaoMovimentacaoBanco : ValidacaoBase<MovimentacaoBanco>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraCaixaObrigatorio();
            AssineRegraNaoPossuiCaixaAberto();            
        }

        public override void ValideAtualizacao()
        {
            AssineRegraCaixaObrigatorio();           
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraCaixaObrigatorio()
        {
            RuleFor(movimentacaoBanco => movimentacaoBanco.Banco)
                .NotNull()
                .WithMessage("Banco não informado.");
        }

        private void AssineRegraNaoPossuiCaixaAberto()
        {
            RuleFor(movimentacaoBanco => movimentacaoBanco.Banco)
                .Must(banco => !ExisteBancoAberto())
                .WithMessage("Este banco já está aberto.")
                .When(movimentacaoBanco => movimentacaoBanco.Status == EnumStatusMovimentacaoCaixa.ABERTO && movimentacaoBanco.Banco != null);
        }
                

        #endregion

        #region " MÉTODOSO PRIVADOS "

        private bool ExisteBancoAberto()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioMovimentacaoBanco>();

            var bancoAberto = repositorio.ConsulteBancoAberto(ObjetoValidado.Banco);

            return bancoAberto != null;
        }

        #endregion
    }
}
