using FluentValidation;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio
{
    public class ValidacaoNaturezaOperacaoCfop:ValidacaoBase<NaturezaOperacaoCfop>
    {
        #region " PROPRIEDADES "

        public NaturezaOperacao NaturezaOperacao { get; set; }

        #endregion

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

        private void AssineRegraCfopCondizComTipoMovimentacaoEOrigemDestino()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("O Cfop {0} não condiz com o tipo movimentação e/ou com a origem/destino.");

            RuleFor(naturezaOperacaoCfop => naturezaOperacaoCfop.Cfop)
                .Must(cfop => CfopCondizComTipoMovimentacaoEOrigemDestino(mensagemComposta))
                .WithMessage(mensagemComposta);

        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraCfopCondizComTipoMovimentacaoEOrigemDestino();
        }

        private bool CfopCondizComTipoMovimentacaoEOrigemDestino(MensagemComposta mensagemComposta)
        {
            mensagemComposta.ListaDeParametros.Clear();
            mensagemComposta.ListaDeParametros.Add(ObjetoValidado.Cfop.Codigo);

            if (NaturezaOperacao == null)
            {
                return false;
            }

            if (NaturezaOperacao.TipoMovimentacao == EnumTipoMovimentacaoNaturezaOperacao.ENTRADA)
            {
                if (NaturezaOperacao.OrigemDestino == EnumOrigemDestino.ESTADUAL)
                {
                    return ObjetoValidado.Cfop.Codigo[0] == '1';
                }
                else if (NaturezaOperacao.OrigemDestino == EnumOrigemDestino.INTERESTADUAL)
                {
                    return ObjetoValidado.Cfop.Codigo[0] == '2';
                }
                else if (NaturezaOperacao.OrigemDestino == EnumOrigemDestino.EXTERIOR)
                {
                    return ObjetoValidado.Cfop.Codigo[0] == '3';
                }
            }
            else
            {
                if (NaturezaOperacao.OrigemDestino == EnumOrigemDestino.ESTADUAL)
                {
                    return ObjetoValidado.Cfop.Codigo[0] == '5';
                }
                else if (NaturezaOperacao.OrigemDestino == EnumOrigemDestino.INTERESTADUAL)
                {
                    return ObjetoValidado.Cfop.Codigo[0] == '6';
                }
                else if (NaturezaOperacao.OrigemDestino == EnumOrigemDestino.EXTERIOR)
                {
                    return ObjetoValidado.Cfop.Codigo[0] == '7';
                }
            }

            return false;
        }

        #endregion
    }
}
