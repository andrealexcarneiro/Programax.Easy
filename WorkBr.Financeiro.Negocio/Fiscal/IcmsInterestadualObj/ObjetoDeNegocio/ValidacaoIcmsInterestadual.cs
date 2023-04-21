using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;

namespace Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.ObjetoDeNegocio
{
    public class ValidacaoIcmsInterestadual : ValidacaoBase<IcmsInterestadual>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            ValideNcmInformado();
            ValideListaIcmsInterestadualEstado();
        }

        public override void ValideAtualizacao()
        {
            ValideNcmInformado();
            ValideListaIcmsInterestadualEstado();
        }

        #endregion

        #region " REGRAS "

        private void ValideNcmInformado()
        {
            RuleFor(icmsInterestadual => icmsInterestadual.Ncm)
                .NotNull()
                .WithMessage("Ncm não informado.");
        }

        private void ValideListaIcmsInterestadualEstado()
        {
            ValidacaoIcmsInterestadualEstado validacaoIcmsInterestadualEstado = new ValidacaoIcmsInterestadualEstado();
            validacaoIcmsInterestadualEstado.ValideInclusao();

            RuleFor(icmsInterestadual => icmsInterestadual.ListaIcmsInterestadualEstado).SetValidator(validacaoIcmsInterestadualEstado);
        }

        #endregion
    }
}
