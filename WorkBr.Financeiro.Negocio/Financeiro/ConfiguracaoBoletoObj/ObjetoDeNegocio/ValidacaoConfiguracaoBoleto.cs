using FluentValidation;
using Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.ObjetoDeNegocio
{
    public class ValidacaoConfiguracaoBoleto : ValidacaoBase<ConfiguracaoBoleto>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraPerfilObrigatorio();
            AssineRegraPerfilAgenciaObrigatorio();
            AssineRegraPerfilContaObrigatorio();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraPerfilObrigatorio();
            AssineRegraPerfilAgenciaObrigatorio();
            AssineRegraPerfilContaObrigatorio();
        }
        
        #endregion

        #region " REGRAS "

        private void AssineRegraPerfilObrigatorio()
        {
            RuleFor(perfilConfiguracao => perfilConfiguracao.Perfil.Id)
                .Must(perfilConfiguracao => perfilConfiguracao !=0)
                .WithMessage("Perfil não informado");
        }

        private void AssineRegraPerfilAgenciaObrigatorio()
        {
            RuleFor(perfilAgencia => perfilAgencia.Agencia)
                .Must(perfilAgencia => perfilAgencia.Id != 0)
                .WithMessage("Agência não informada");
        }

        private void AssineRegraPerfilContaObrigatorio()
        {
            RuleFor(perfilConta => perfilConta.ContaBancaria)
                .Must(perfilConta => perfilConta.Id != 0)
                .WithMessage("Conta não informada");
        }

        #endregion

    }
}
