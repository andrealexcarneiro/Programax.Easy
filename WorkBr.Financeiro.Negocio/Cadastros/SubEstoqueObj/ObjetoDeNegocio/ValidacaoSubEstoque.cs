using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;

namespace Programax.Easy.Negocio.Cadastros.SubEstoqueObj.ObjetoDeNegocio
{
    public class ValidacaoSubEstoque : ValidacaoBase<SubEstoque>
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
            RuleFor(subestoque => subestoque.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição não informada");
        }
    }
}
