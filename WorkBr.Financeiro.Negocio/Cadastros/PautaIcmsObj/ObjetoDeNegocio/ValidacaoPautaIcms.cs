using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;

namespace Programax.Easy.Negocio.Cadastros.PautaIcmsObj.ObjetoDeNegocio
{
    public class ValidacaoPautaIcms : ValidacaoBase<PautaIcms>
    {
        public override void ValideInclusao()
        {
            AssineRegraEstadoObrigatorio();
            AssineRegraItemObrigatorio();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraEstadoObrigatorio();
            AssineRegraItemObrigatorio();
        }

        private void AssineRegraItemObrigatorio()
        {
            RuleFor(pautaIcms => pautaIcms.Produto)
                .NotNull()
                .WithMessage("Item não informado.");
        }

        private void AssineRegraEstadoObrigatorio()
        {
            RuleFor(pautaIcms => pautaIcms.Estado)
                .NotNull()
                .WithMessage("Estado não informado.");
        }
    }
}
