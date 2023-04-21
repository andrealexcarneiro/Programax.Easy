using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Infraestrutura.Negocio.Utils;
using FluentValidation;

namespace Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.ObjetoDeNegocio
{
    public class ValidacaoIcmsInterestadualEstado : ValidacaoBase<IcmsInterestadualEstado>
    {
        #region " PROPRIEDADES "

        public List<IcmsInterestadualEstado> ListaIcmsInterestaduais { get; set; }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraAliquotaInternaNaoInformada();
            AssineRegraUFNaoInformada();
            AssineRegraFCPMenorOuIgualADois();
            AssineRegraIcmsUnicoPorNcmEEstado();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraAliquotaInternaNaoInformada();
            AssineRegraUFNaoInformada();
            AssineRegraFCPMenorOuIgualADois();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraAliquotaInternaNaoInformada()
        {
            RuleFor(icmsInterestadual => icmsInterestadual.AliquotaInterna)
                .Must(aliquotaInterna => aliquotaInterna > 0)
                .WithMessage("Alíquota interna não informada.")
                .When(icmsInterestadual => !string.IsNullOrEmpty(icmsInterestadual.UF));
        }

        private void AssineRegraUFNaoInformada()
        {
            RuleFor(icmsInterestadual => icmsInterestadual.UF)
                .Must(uf => !string.IsNullOrEmpty(uf))
                .WithMessage("UF não informada.");
        }

        private void AssineRegraFCPMenorOuIgualADois()
        {
            RuleFor(icmsInterestadual => icmsInterestadual.FCP)
                .Must(fcp => fcp <= 2)
                .WithMessage("FCP tem que ser menor ou igual a 2.");
        }

        private void AssineRegraIcmsUnicoPorNcmEEstado()
        {
            RuleFor(icmsInterestadual => icmsInterestadual.UF)
                .Must(uf => !ListaIcmsInterestaduais.Exists(icms => icms.UF == uf && icms.Id != ObjetoValidado.Id))
                .WithMessage("Este Estado já existe para este NCM.")
                .When(icmsInterestadual => ListaIcmsInterestaduais != null && ListaIcmsInterestaduais.Count > 0);
        }

        #endregion
    }
}
