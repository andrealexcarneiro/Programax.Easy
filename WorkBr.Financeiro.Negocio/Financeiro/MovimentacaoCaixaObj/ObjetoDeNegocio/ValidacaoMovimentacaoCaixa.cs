using FluentValidation;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio
{
    public class ValidacaoMovimentacaoCaixa : ValidacaoBase<MovimentacaoCaixa>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraCaixaObrigatorio();
            AssineRegraNaoPossuiCaixaAberto();
            AssineRegraDiferencaCaixaObrigatorio();
            AssineRegraDiferencaCaixaDeveSerZero();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraCaixaObrigatorio();
            AssineRegraDiferencaCaixaObrigatorio();
            AssineRegraDiferencaCaixaDeveSerZero();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraCaixaObrigatorio()
        {
            RuleFor(movimentacaoCaixa => movimentacaoCaixa.Caixa)
                .NotNull()
                .WithMessage("Caixa não informado.");
        }

        private void AssineRegraNaoPossuiCaixaAberto()
        {
            RuleFor(movimentacaoCaixa => movimentacaoCaixa.Caixa)
                .Must(caixa => !ExisteCaixaAberto())
                .WithMessage("Este caixa já está aberto.")
                .When(movimentacaoCaixa => movimentacaoCaixa.Status == EnumStatusMovimentacaoCaixa.ABERTO && movimentacaoCaixa.Caixa != null);
        }

        private void AssineRegraDiferencaCaixaObrigatorio()
        {
            RuleFor(movimentacaoCaixa => movimentacaoCaixa.DiferencaSaldoFinalCaixa)
                .Must(diferenca => diferenca > 0)
                .WithMessage("A diferença do caixa deve ser maior que zero.")
                .When(movimentacaoCaixa => movimentacaoCaixa.Status == EnumStatusMovimentacaoCaixa.FECHADO && 
                                                             movimentacaoCaixa.ResultadoCaixa != EnumResultadoCaixa.SALDOCORRETO);
        }

        private void AssineRegraDiferencaCaixaDeveSerZero()
        {
            RuleFor(movimentacaoCaixa => movimentacaoCaixa.DiferencaSaldoFinalCaixa)
                .Must(diferenca => diferenca == 0)
                .WithMessage("A diferença do caixa deve ser igual a zero.")
                .When(movimentacaoCaixa => movimentacaoCaixa.Status == EnumStatusMovimentacaoCaixa.FECHADO &&
                                                             movimentacaoCaixa.ResultadoCaixa == EnumResultadoCaixa.SALDOCORRETO);
        }

        #endregion

        #region " MÉTODOSO PRIVADOS "

        private bool ExisteCaixaAberto()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioMovimentacaoCaixa>();

            var caixaAberto = repositorio.ConsulteCaixaAberto(ObjetoValidado.Caixa);

            return caixaAberto != null;
        }

        #endregion
    }
}
