using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Negocio.Financeiro.CategoriaDreObj.ObjetoDeNeocio
{
    public class ValidacaoCategoriaDre : ValidacaoBase<CategoriaDre>
    {
        public override void ValideInclusao()
        {
            AssineRegraLinhaObrigatoria();
            AssineRegraDescricaoObrigatoria();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraLinhaObrigatoria();
            AssineRegraDescricaoObrigatoria();
        }

        private void AssineRegraLinhaObrigatoria()
        {
            RuleFor(categoria => categoria.SubGrupoCategoria)
                .NotNull()
                .WithMessage("Grupo não informado");
        }

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(grupo => grupo.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição não informada");
        }
    }
}
