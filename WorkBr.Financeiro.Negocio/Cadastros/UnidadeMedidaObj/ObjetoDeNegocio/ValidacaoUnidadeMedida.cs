using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.Repositorio;

namespace Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio
{
    public class ValidacaoUnidadeMedida : ValidacaoBase<UnidadeMedida>
    {
        public override void ValideInclusao()
        {
            AssineRegraDescricaoObrigatoria();
            AssineRegraUnidadeJaExistente();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraDescricaoObrigatoria();
            AssineRegraUnidadeJaExistente();
        }

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(unidadeMedida => unidadeMedida.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição não informada");
        }

        private void AssineRegraUnidadeJaExistente()
        {
            RuleFor(unidadeMedida => unidadeMedida.Abreviacao)
                .Must(descricao => !ExisteUmaUnidadeComEstaAbreviacao())
                .WithMessage("Já existe uma unidade com esta abreviação");
        }

        private bool ExisteUmaUnidadeComEstaAbreviacao()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioUnidadeMedida>();

            var unidade = repositorio.ConsultePelaAbreviacao(ObjetoValidado.Abreviacao, ObjetoValidado.Id);

            return unidade != null;
        }
    }
}
