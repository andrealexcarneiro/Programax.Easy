using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.Repositorio;

namespace Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio
{
    public class ValidacaoItemMovimentacaoBanco : ValidacaoBase<ItemMovimentacaoBanco>
    {
        public override void ValideInclusao()
        {
            //AssineRegraValorMaiorQueZero();
            AssineRegraMovimentacaoObrigratoria();
        }

        public override void ValideAtualizacao()
        {
            //AssineRegraValorMaiorQueZero();
            AssineRegraMovimentacaoObrigratoria();
            AssineRegraMovimentacaoEstornadaSomenteUmaVez();
        }

        private void AssineRegraValorMaiorQueZero()
        {
            RuleFor(itemMovimentacaoBanco => itemMovimentacaoBanco.Valor)
                .Must(valor => valor > 0)
                .WithMessage("O valor tem que ser maior que zero.");
                //.When(itemMovimentacaoBanco => !itemMovimentacaoBanco.ItemDeEntrada);
        }

        private void AssineRegraMovimentacaoObrigratoria()
        {
            RuleFor(itemMovimentacaoBanco => itemMovimentacaoBanco.MovimentacaoBanco)
                .NotNull()
                .WithMessage("Movimentação Banco não informado.");
        }

        private void AssineRegraMovimentacaoEstornadaSomenteUmaVez()
        {
            RuleFor(itemMovimentacaoBanco => itemMovimentacaoBanco.EstahEstornado)
                .Must(estahEstornado => !ItemJaFoiEstornado())
                .WithMessage("Este item já foi estornado.");
        }

        private bool ItemJaFoiEstornado()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioItemMovimentacaoBanco>();

            var itemMovimentacao = repositorio.ConsulteSemSessao(ObjetoValidado.Id);

            return itemMovimentacao.EstahEstornado;
        }
    }
}
