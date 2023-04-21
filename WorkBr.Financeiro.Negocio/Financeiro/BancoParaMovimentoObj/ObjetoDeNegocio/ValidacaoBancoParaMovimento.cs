using FluentValidation;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio
{
    public class ValidacaoBancoParaMovimento: ValidacaoBase<BancoParaMovimento>
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

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAtualizacao()
        {   
            AssineRegraContaBancariaObrigatorio();
            AssineRegraNomeBancoObrigatorio();            
            AssineRegraNomeUnico();           
        }

        private bool NomeEhUnico()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioBancoParaMovimento>();

            var banco = repositorio.ConsultePeloNomeBanco(ObjetoValidado.NomeBanco);

            return banco == null || banco.Id == ObjetoValidado.Id;
        }
        
        #endregion

        #endregion

        #region " REGRAS "
        
        private void AssineRegraContaBancariaObrigatorio()
        {
            RuleFor(banco => banco.ContaBancaria)
                .NotNull()
                .WithMessage("Conta Bancária não foi informado.");
        }

        private void AssineRegraNomeBancoObrigatorio()
        {
            RuleFor(banco => banco.NomeBanco)
                .Must(nomeCaixa => !string.IsNullOrEmpty(nomeCaixa))
                .WithMessage("Nome Banco não foi informado.");
        }

        private void AssineRegraNomeUnico()
        {
            RuleFor(banco => banco.NomeBanco)
                .Must(banco => NomeEhUnico())
                .WithMessage("Nome Caixa já existente.")
                .When(banco => !string.IsNullOrEmpty(banco.NomeBanco));
        }
        
        #endregion

    }
}
