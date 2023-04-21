using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;
using System.Collections.Generic;

namespace Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio
{
    public class ValidacaoTributacaoIcms : ValidacaoBase<TributacaoIcms>
    {
        #region " PROPRIEDADES "

        public List<TributacaoIcms> ListaTributacoesIcms { get; set; }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraEstadoDestinoObrigatorio();
            AssineRegraCfopObrigatorio();
            AssineRegraCsosnObrigatorio();
            AssineRegraTributacaoIcmsUnica();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraEstadoDestinoObrigatorio();
            AssineRegraCfopObrigatorio();
            AssineRegraCsosnObrigatorio();
            AssineRegraTributacaoIcmsUnica();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraEstadoDestinoObrigatorio()
        {
            RuleFor(tributacaoIcms => tributacaoIcms.EstadoDestino)
                .Must(uf => !string.IsNullOrEmpty(uf))
                .WithMessage("Estado de destino obrigatório");
        }

        private void AssineRegraCfopObrigatorio()
        {
            RuleFor(tributacaoIcms => tributacaoIcms.Cfop)
                .Must(cfop => cfop != null)
                .WithMessage("Cfop é obrigatório");
        }

        private void AssineRegraCsosnObrigatorio()
        {
            RuleFor(tributacaoIcms => tributacaoIcms.CstCsosn)
                .Must(cstCsosn => cstCsosn != null)
                .WithMessage("Csosn é obrigatório");
        }

        private void AssineRegraTributacaoIcmsUnica()
        {
            RuleFor(tributacaoIcms => tributacaoIcms.Id)
                .Must(id => !ListaTributacoesIcms.Exists(trib => trib.Id != ObjetoValidado.Id && 
                                                                                      trib.TipoSaida == ObjetoValidado.TipoSaida && 
                                                                                      trib.EstadoDestino == ObjetoValidado.EstadoDestino && 
                                                                                      trib.TipoCliente == ObjetoValidado.TipoCliente &&
                                                                                      trib.TipoInscricaoICMS == ObjetoValidado.TipoInscricaoICMS))
                .WithMessage("Já existe uma tributação inserida com este tipo de saída, Estado de destino, tipo de cliente e tipo de inscrição.");
        }

        #endregion
    }
}
