using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Negocio.Financeiro.GruposDreObj.ObjetoDeNegocio
{
    public class ValidacaoGrupoDre : ValidacaoBase<GrupoDre>
    {
        public override void ValideInclusao()
        {
            AssineRegraDescricaoObrigatoria();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraDescricaoObrigatoria();
        }

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(grupo => grupo.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição não informada");
        }
    }
}
