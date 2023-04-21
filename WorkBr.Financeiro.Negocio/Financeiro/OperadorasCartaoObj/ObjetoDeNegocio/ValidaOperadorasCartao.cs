using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.Repositorio;

namespace Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio
{
    public class ValidaOperadorasCartao : ValidacaoBase<OperadorasCartao>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            RegrasComunsAInclusaoEAtualizacao();
        }

        public override void ValideAtualizacao()
        {
            RegrasComunsAInclusaoEAtualizacao();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraDescricaoOperadoraObrigatorio()
        {
            RuleFor(operadoras => operadoras.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição da operadora não informado.");
        }

        private void AssineRegraDiasDePrazoParaCreditarObrigatorio()
        {
            RuleFor(operadoras => operadoras.DiasPrazoParaCreditar)
                .Must(dias=> dias !=0)
                .WithMessage("Dias de prazo para creditar não informado.");
        }

        private void AssineBancoObrigatoria()
        {
            RuleFor(operadoras => operadoras.BancoParaMovimento)
                .Must(banco=>banco.Id != 0)               
                .WithMessage("O Banco a ser creditado não foi informado.");
        }

        #endregion

        #region " MÉTODOS AUXILIARES "


        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraDescricaoOperadoraObrigatorio();
            //AssineRegraDiasDePrazoParaCreditarObrigatorio();           
            AssineBancoObrigatoria();
        }

        #endregion

    }
}
