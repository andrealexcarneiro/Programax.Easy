using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.CidadeObj.Repositorio;

namespace Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio
{
    public class ValidacaoCidade : ValidacaoBase<Cidade>
    {
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

        private void AssineRegraNomeCidadeObrigatoria()
        {
            RuleFor(cidade => cidade.Descricao)
                .Must(descricao => !string.IsNullOrWhiteSpace(descricao))
                .WithMessage("Descrição Obrigatória");
        }

        private void AssineRegraEstadoObrigatorio()
        {
            RuleFor(cidade => cidade.Estado)
                .NotNull()
                .WithMessage("Estado Obrigatório");
        }

        private void AssineRegrCodigoIbgeObrigatorio()
        {
            RuleFor(cidade => cidade.CodigoIbge)
                .Must(codigoIbge => !string.IsNullOrWhiteSpace(codigoIbge))
                .WithMessage("Código IBGE Obrigatório");
        }

        private void AssineRegraCodigoIbgeUnico()
        {
            RuleFor(cidade => cidade.CodigoIbge)
                .Must(codigoIbge => CodigoIbgeEhUnico())
                .WithMessage("Este código IBGE já está sendo utilizada por outra cidade")
                .When(cidade => !string.IsNullOrWhiteSpace(cidade.CodigoIbge));
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraNomeCidadeObrigatoria();
            AssineRegraEstadoObrigatorio();
            AssineRegrCodigoIbgeObrigatorio();
            AssineRegraCodigoIbgeUnico();
        }

        private bool CodigoIbgeEhUnico()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCidade>();
            var cidadeBanco = repositorio.ConsultePeloCodigoIbge(ObjetoValidado.CodigoIbge);

            return cidadeBanco == null || cidadeBanco.Id == ObjetoValidado.Id;
        }

        #endregion
    }
}
