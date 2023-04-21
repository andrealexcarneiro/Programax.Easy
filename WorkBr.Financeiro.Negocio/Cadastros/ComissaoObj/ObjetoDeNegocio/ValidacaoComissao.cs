using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio
{
    public class ValidacaoComissao : ValidacaoBase<Comissao>
    {
        #region " PROPRIEDADES "

        public List<Comissao> ListaDeComissoes { get; set; }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            RegrasComunsAAtualizacaoEInclusao();
        }

        public override void ValideAtualizacao()
        {
            RegrasComunsAAtualizacaoEInclusao();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraTabelaDePrecoObrigatoria()
        {
            RuleFor(comissao => comissao.TabelaPreco)
                .NotNull()
                .WithMessage("Tabela de preço não informada.");
        }

        private void AssineRegraFuncaoObrigatoria()
        {
            RuleFor(comissao => comissao.FuncaoPessoaComissao)
                .NotNull()
                .WithMessage("Função não informada.");
        }

        private void AssineRegraPessoaObrigatoria()
        {
            RuleFor(comissao => comissao.Pessoa)
                .NotNull()
                .WithMessage("Parceiro não informado.");
        }

        private void AssineRegraFuncaoUnicaNaComissao()
        {
            RuleFor(comissao => comissao.FuncaoPessoaComissao)
                .Must(funcao => !ListaDeComissoes.Exists(com => com.FuncaoPessoaComissao != funcao))
                    .WithMessage("Já existe uma ou mais comissões com outra função")
                .When(comissao => comissao.FuncaoPessoaComissao != null && comissao.TabelaPreco != null && ListaDeComissoes != null);
        }

        private void AssineRegraComissaoUnicaNaLista()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("Já existe uma comissão com a tabela de preço {0} para a função {1}.");

            RuleFor(comissao => comissao.Id)
                .Must(id => ComissaoUnicaNaLista(mensagemComposta))
                .WithMessage(mensagemComposta)
                .When(comissao => comissao.TabelaPreco != null &&
                                              comissao.FuncaoPessoaComissao != null &&
                                              ListaDeComissoes != null &&
                                              ListaDeComissoes.Count > 0);
        }

        #endregion

        #region " MÉTODOS PRIVADOS "

        private void RegrasComunsAAtualizacaoEInclusao()
        {
            AssineRegraTabelaDePrecoObrigatoria();
            AssineRegraFuncaoObrigatoria();
            AssineRegraPessoaObrigatoria();
            AssineRegraComissaoUnicaNaLista();
            AssineRegraFuncaoUnicaNaComissao();
        }

        private bool ComissaoUnicaNaLista(MensagemComposta mensagemComposta)
        {
            mensagemComposta.ListaDeParametros.Add(ObjetoValidado.TabelaPreco.NomeTabela);
            mensagemComposta.ListaDeParametros.Add(ObjetoValidado.FuncaoPessoaComissao.GetValueOrDefault().Descricao());

            return !ListaDeComissoes.Exists(comissao => comissao.Id != ObjetoValidado.Id &&
                                                                             comissao.FuncaoPessoaComissao == ObjetoValidado.FuncaoPessoaComissao &&
                                                                             comissao.TabelaPreco != null &&
                                                                             comissao.TabelaPreco.Id == ObjetoValidado.TabelaPreco.Id);
        }

        #endregion
    }
}
