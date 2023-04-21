using FluentValidation;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Validacoes;
using System.Collections.Generic;

namespace Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio
{
    public class ValidacaoTributacaoFederalPis : ValidacaoBase<PisNotaFiscal>
    {
        #region " PROPRIEDADES "

        public List<PisNotaFiscal> ListaTributacoesFederalPis { get; set; }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraEstadoDestinoObrigatorio();            
            AssineRegraCsosnObrigatorio();
            AssineRegraTributacaoIcmsUnica();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraEstadoDestinoObrigatorio();           
            AssineRegraCsosnObrigatorio();
            AssineRegraTributacaoIcmsUnica();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraEstadoDestinoObrigatorio()
        {
            RuleFor(tributacaoFederalPis => tributacaoFederalPis.EstadoDestino)
                .Must(uf => !string.IsNullOrEmpty(uf))
                .WithMessage("Estado de destino obrigatório");
        }
        
        private void AssineRegraCsosnObrigatorio()
        {
            RuleFor(tributacaoFederalPis => tributacaoFederalPis.CstPis)
                .Must(cstCsosn => cstCsosn != null)
                .WithMessage("Cst é obrigatório");
        }

        private void AssineRegraTributacaoIcmsUnica()
        {
            RuleFor(tributacaoFederalPis => tributacaoFederalPis.Id)
                .Must(id => !ListaTributacoesFederalPis.Exists(trib => trib.Id != ObjetoValidado.Id && 
                                                                                      trib.TipoSaida == ObjetoValidado.TipoSaida && 
                                                                                      trib.EstadoDestino == ObjetoValidado.EstadoDestino && 
                                                                                      trib.TipoCliente == ObjetoValidado.TipoCliente &&
                                                                                      trib.CstPis == ObjetoValidado.CstPis))
                .WithMessage("Já existe uma tributação inserida com este tipo de saída, Estado de destino, tipo de cliente e Cst Pis.");
        }

        #endregion
    }
}
