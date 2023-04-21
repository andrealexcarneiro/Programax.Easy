using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.Repositorio;

namespace Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio
{
    public class ValidacaoEndereco : ValidacaoBase<Endereco>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            ValidacoesComunsAoCadastroEAtualizacao();
        }

        public override void ValideAtualizacao()
        {
            ValidacoesComunsAoCadastroEAtualizacao();
        }

        public override void ValideExclusao()
        {
            
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraCepObrigatorio()
        {
            RuleFor(endereco => endereco.CEP)
                .Must(cep => !string.IsNullOrWhiteSpace(cep))
                .WithMessage("CEP do endereço não foi informado.");
        }

        private void AssineRegraRuaObrigatoria()
        {
            RuleFor(endereco => endereco.Rua)
                .Must(rua => !string.IsNullOrWhiteSpace(rua))
                .WithMessage("Rua do endereço não foi informado.");
        }

        private void AssineRegraBairroObrigatorio()
        {
            RuleFor(endereco => endereco.Bairro)
                .Must(rua => !string.IsNullOrWhiteSpace(rua))
                .WithMessage("Bairro do endereço não foi informado.");
        }

        private void AssineRegraCidadeObrigatoria()
        {
            RuleFor(endereco => endereco.Cidade)
                .Must(cidade => cidade != null && cidade.Id > 0)
                .WithMessage("Cidade do endereço não foi informada.");
        }

        private void AssineRegraCepUnico()
        {
            RuleFor(endereco => endereco.CEP)
                .Must(cep => CepEhUnicoNaBase(cep))
                .WithMessage("O CEP informado já consta na base.")
                .When(endereco => !string.IsNullOrEmpty(endereco.CEP) && endereco.Id == 0);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void ValidacoesComunsAoCadastroEAtualizacao()
        {
            AssineRegraCepObrigatorio();
            AssineRegraRuaObrigatoria();
            AssineRegraBairroObrigatorio();
            AssineRegraCidadeObrigatoria();

            AssineRegraCepUnico();
        }

        private bool CepEhUnicoNaBase(string cep)
        {
            var repositorioEndereco = FabricaDeRepositorios.Crie<IRepositorioEndereco>();

            var endereco = repositorioEndereco.Consulte(cep);

            return endereco == null;
        }

        #endregion
    }
}
