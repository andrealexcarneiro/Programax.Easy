using FluentValidation;
using Programax.Easy.Negocio.Financeiro.BancoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio
{
    public class ValidacaoBanco : ValidacaoBase<Banco>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraDescricaoObrigatoria();
            AssineRegraCodigoDoBancoUnico();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraDescricaoObrigatoria();
            AssineRegraCodigoDoBancoUnico();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(banco => banco.Descricao)
                .Must(descricao => !string.IsNullOrWhiteSpace(descricao))
                .WithMessage("Descrição não informada");
        }

        private void AssineRegraCodigoDoBancoUnico()
        {
            RuleFor(banco => banco.Codigo)
                .Must(descricao => CodigoDoBancoEhUnico())
                .WithMessage("Código já utilizado em outro banco.")
                .When(banco => !string.IsNullOrWhiteSpace(banco.Codigo));
        }

        #endregion

        #region " MÉTODOS PRIVADOS "

        private bool CodigoDoBancoEhUnico()
        {
            var repositorioBanco = FabricaDeRepositorios.Crie<IRepositorioBanco>();

            var bancoBase = repositorioBanco.ConsultePeloCodigoBanco(ObjetoValidado.Codigo);

            return bancoBase == null || bancoBase.Id == ObjetoValidado.Id;
        }

        #endregion
    }
}
