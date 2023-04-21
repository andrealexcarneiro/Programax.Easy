using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;

namespace Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio
{
    public class ValidacaoGrupoTributacaoFederal : ValidacaoBase<GrupoTributacaoFederal>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraDescricaoObrigatoria();
            AssineRegraPossuiPeloMenosUmaTributacaoCofins();
            AssineRegraPossuiPeloMenosUmaTributacaoPis();
            AssineRegraPossuiPeloMenosUmaTributacaoIpi();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraDescricaoObrigatoria();
            AssineRegraPossuiPeloMenosUmaTributacaoCofins();
            AssineRegraPossuiPeloMenosUmaTributacaoPis();
            AssineRegraPossuiPeloMenosUmaTributacaoIpi();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(grupoTributacaoIcms => grupoTributacaoIcms.Descricao)
                .Must(descricao => !string.IsNullOrWhiteSpace(descricao))
                .WithMessage("Descrição não informada");
        }

        private void AssineRegraPossuiPeloMenosUmaTributacaoCofins()
        {
            RuleFor(grupoTributacao => grupoTributacao.ListaCofins)
                .Must(listaTributacoes => listaTributacoes.Count > 0)              
                .WithMessage("É necessário ter pelo menos uma tributação de Cofins adicionada");
                
        }

        private void AssineRegraPossuiPeloMenosUmaTributacaoPis()
        {
            RuleFor(grupoTributacao => grupoTributacao.ListaPis)
                .Must(listaTributacoes => listaTributacoes.Count > 0)
                .WithMessage("É necessário ter pelo menos uma tributação de Pis adicionada");
        }

        private void AssineRegraPossuiPeloMenosUmaTributacaoIpi()
        {
            RuleFor(grupoTributacao => grupoTributacao.ListaIpi)
                .Must(listaTributacoes => listaTributacoes.Count > 0)
                .WithMessage("É necessário ter pelo menos uma tributação de Ipi adicionada");
        }

        #endregion
    }
}
