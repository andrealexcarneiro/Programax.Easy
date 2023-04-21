using Programax.Easy.Negocio.Financeiro.CashBackObj.Repositorio;
using Programax.Easy.Servico.Cadastros.CashBack.CashBackServ;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Negocio.Financeiro.CashBackObj.ObjetoDeNegocio
{
    public class ValidacaoCash : ValidacaoBase<Cashback>
    {
        #region " MÉTODOS SOBRESCRITOS "

        //public override void ValideInclusao()
        //{
        //    AssineRegraDescricaoObrigatoria();
        //    AssineRegraCodigoDoBancoUnico();
        //}

        //public override void ValideAtualizacao()
        //{
        //    AssineRegraDescricaoObrigatoria();
        //    AssineRegraCodigoDoBancoUnico();
        //}

        #endregion

        #region " REGRAS "

        //private void AssineRegraDescricaoObrigatoria()
        //{
        //    RuleFor(cash => cash.Valor)
        //        .Must(Valor => !string.IsNullOrWhiteSpace(valor))
        //        .WithMessage("Descrição não informada");
        //}

        //private void AssineRegraCodigoDoBancoUnico()
        //{
        //    RuleFor(banco => banco.Codigo)
        //        .Must(descricao => CodigoDoBancoEhUnico())
        //        .WithMessage("Código já utilizado em outro banco.")
        //        .When(banco => !string.IsNullOrWhiteSpace(banco.Codigo));
        //}

        #endregion

        #region " MÉTODOS PRIVADOS "

        private bool CodigoDoBancoEhUnico()
        {
            var repositorioCashBack = FabricaDeRepositorios.Crie<IRepositorioCashBack>();

            var bancoBase = repositorioCashBack.ConsultePeloCodigoBanco(ObjetoValidado.Codigo);

            return bancoBase == null || bancoBase.Id == ObjetoValidado.Id;
        }

        #endregion
    }
}
