using FluentValidation;
using Programax.Easy.Negocio.Financeiro.BancoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.ObjetoDeNegocio
{
    public class ValidacaoPerfilEmissaoBoleto : ValidacaoBase<PerfilEmissaoBoleto>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraDescricaoObrigatoria();           
        }

        public override void ValideAtualizacao()
        {
            AssineRegraDescricaoObrigatoria();           
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(perfil => perfil.Descricao)
                .Must(descricao => !string.IsNullOrWhiteSpace(descricao))
                .WithMessage("Descrição não informada");
        }
        
        #endregion
        
    }
}
