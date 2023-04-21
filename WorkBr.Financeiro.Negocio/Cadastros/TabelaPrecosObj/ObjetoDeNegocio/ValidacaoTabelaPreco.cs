using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Fabricas;

namespace Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio
{
    public class ValidacaoTabelaPreco : ValidacaoBase<TabelaPreco>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            ValidacoesComunsParaInclusaoEAtualizacao();
        }

        public override void ValideAtualizacao()
        {
            ValidacoesComunsParaInclusaoEAtualizacao();
        }

        public override void ValideExclusao()
        {
            //AssineRegraNaoExistePedidoDeVendaParaEssaTabelaDePreco();
        }

        #endregion

        private void ValidacoesComunsParaInclusaoEAtualizacao()
        {
            AssineRegraDescricaoEhObrigatorio();
            AssineRegraDataDeCadastroEhObrigatoria();
            AssineRegraDataDeValidadeMaiorOuIgualADataDeCadastro();
        }

        #region " REGRAS "

        private void AssineRegraDescricaoEhObrigatorio()
        {
            RuleFor(tabela => tabela.NomeTabela)
                .NotNull()
                .NotEmpty()
                .WithMessage("A descrição é obrigatório ser preenchido.");
        }

        private void AssineRegraDataDeCadastroEhObrigatoria()
        {
            RuleFor(tabela => tabela.DataDeCadastro)
                .Must(dataCadastro => dataCadastro > DateTime.MinValue)
                .WithMessage("A data de cadastro é obrigatória.");
        }

        private void AssineRegraDataDeValidadeMaiorOuIgualADataDeCadastro()
        {
            RuleFor(tabela => tabela.DataDeValidade)
                .Must((tabela, dataDeValidade) => dataDeValidade >= tabela.DataDeCadastro)
                .WithMessage("A data de validade não pode ser anterior a data de cadastro.")
                .When(tabela => tabela.DataDeValidade > DateTime.MinValue && tabela.DataDeCadastro > DateTime.MinValue);
        }

        #endregion
    }
}
