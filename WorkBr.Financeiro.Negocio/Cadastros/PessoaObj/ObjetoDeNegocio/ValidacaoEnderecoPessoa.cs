using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    public class ValidacaoEnderecoPessoa : ValidacaoBase<EnderecoPessoa>
    {
        public List<EnderecoPessoa> ListaEnderecos { get; set; }

        public override void ValideInclusao()
        {
            AssineRegrasComunsAAtulizacaoEInclusao();
        }

        public override void ValideAtualizacao()
        {
            AssineRegrasComunsAAtulizacaoEInclusao();
        }

        private void AssineRegrasComunsAAtulizacaoEInclusao()
        {
            AssineRegraCepObrigatorio();
            AssineRegraCidadeObrigatorio();
            AssineRegraBairroObrigatorio();
            AssineRegraRuaObrigatorio();
            AssineRegraTipoEnderecoObrigatorio();
            AssineRegraTipoEnderecoUnico();
        }

        private void AssineRegraCepObrigatorio()
        {
            RuleFor(endereco => endereco.CEP)
                .Must(cep => !string.IsNullOrWhiteSpace(cep))
                .WithMessage("Cep não informado.");
        }

        private void AssineRegraCidadeObrigatorio()
        {
            RuleFor(endereco => endereco.Cidade)
                .Must(cidade => cidade != null)
                .WithMessage("Cidade não informada.");
        }

        private void AssineRegraBairroObrigatorio()
        {
            RuleFor(endereco => endereco.Bairro)
                .Must(bairro => !string.IsNullOrWhiteSpace(bairro))
                .WithMessage("Bairro não informado.");
        }

        private void AssineRegraRuaObrigatorio()
        {
            RuleFor(endereco => endereco.Rua)
                .Must(rua => !string.IsNullOrWhiteSpace(rua))
                .WithMessage("Rua não informada.");
        }

        private void AssineRegraTipoEnderecoObrigatorio()
        {
            RuleFor(endereco => endereco.TipoEndereco)
                .Must(tipoEndereco => tipoEndereco != null)
                .WithMessage("Tipo endereço não informado.");
        }

        private void AssineRegraTipoEnderecoUnico()
        {
            RuleFor(endereco => endereco.TipoEndereco)
                .Must(tipoEndereco => EnderecoEhUnico())
                .WithMessage("Já existe um endereço com este tipo.");
        }

        private bool EnderecoEhUnico()
        {
            return !ListaEnderecos.Exists(endereco => endereco.TipoEndereco == ObjetoValidado.TipoEndereco && endereco.Id != ObjetoValidado.Id);
        }
    }
}
