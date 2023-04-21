using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.Repositorio;

namespace Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio
{
    public class ValidacaoItemMovimentacaoCaixa : ValidacaoBase<ItemMovimentacaoCaixa>
    {
        public override void ValideInclusao()
        {
            AssineRegraValorMaiorQueZero();
            AssineRegraMovimentacaoObrigratoria();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraValorMaiorQueZero();
            AssineRegraMovimentacaoObrigratoria();
            AssineRegraMovimentacaoEstornadaSomenteUmaVez();
        }

        private void AssineRegraValorMaiorQueZero()
        {
            RuleFor(itemMovimentacaoCaixa => itemMovimentacaoCaixa.Valor)
                .Must(valor => valor > 0)
                .WithMessage("O valor tem que ser maior que zero.")
                .When(itemMovimentacaoCaixa => !itemMovimentacaoCaixa.ItemDeEntrada);
        }

        private void AssineRegraMovimentacaoObrigratoria()
        {
            RuleFor(itemMovimentacaoCaixa => itemMovimentacaoCaixa.MovimentacaoCaixa)
                .NotNull()
                .WithMessage("Movimentação Caixa não informado.");
        }

        private void AssineRegraMovimentacaoEstornadaSomenteUmaVez()
        {
            RuleFor(itemMovimentacaoCaixa => itemMovimentacaoCaixa.EstahEstornado)
                .Must(estahEstornado => !ItemJaFoiEstornado())
                .WithMessage("Este item já foi estornado.");
        }

        private bool ItemJaFoiEstornado()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioItemMovimentacaoCaixa>();

            var itemMovimentacao = repositorio.ConsulteSemSessao(ObjetoValidado.Id);

            return itemMovimentacao.EstahEstornado;
        }
    }
}
