using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio
{
    public class ValidacaoEmpresa : ValidacaoBase<Empresa>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            MetodosComunsAoCadastroEAtualizacao();
        }

        public override void ValideAtualizacao()
        {
            MetodosComunsAoCadastroEAtualizacao();
        }

        #endregion

        #region " REGRAS "

        #region " DADOS DA EMPRESA "

        private void AssineRegraRazaoSocialObrigatoria()
        {
            RuleFor(empresa => empresa.DadosEmpresa.RazaoSocial)
                .Must(razaoSocial => !string.IsNullOrEmpty(razaoSocial))
                .WithMessage("Razão Social não informada.");
        }

        private void AssineRegraNomeFantasiaObrigatoria()
        {
            RuleFor(empresa => empresa.DadosEmpresa.NomeFantasia)
                .Must(nomeFantasia => !string.IsNullOrEmpty(nomeFantasia))
                .WithMessage("Nome Fantasia não informada.");
        }

        private void AssineRegraCnpjObrigatorio()
        {
            RuleFor(empresa => empresa.DadosEmpresa.Cnpj)
                .Must(cnpj => !string.IsNullOrEmpty(cnpj))
                .WithMessage("Cnpj não informado.");
        }

        private void AssineRegraCnpjValido()
        {
            RuleFor(empresa => empresa.DadosEmpresa.Cnpj)
                .Must(cnpj => ValidacoesGerais.CnpjEstahValido(cnpj))
                .WithMessage("CNPJ Inválido.")
                .When(empresa => !string.IsNullOrEmpty(empresa.DadosEmpresa.Cnpj));
        }

        private void AssineRegraInscricaoEstadualObrigatorio()
        {
            RuleFor(empresa => empresa.DadosEmpresa.InscricaoEstadual)
                .Must(inscricaoEstadual => !string.IsNullOrEmpty(inscricaoEstadual))
                .WithMessage("Inscrição Estadual não informado.");
        }

        private void AssineRegraInscricaoMunicipalObrigatorio()
        {
            RuleFor(empresa => empresa.DadosEmpresa.InscricaoMunicipal)
                .Must(inscricaoMunicipal => !string.IsNullOrEmpty(inscricaoMunicipal))
                .WithMessage("Inscrição Municipal não informado.");
        }

        private void AssineRegraCepInformado()
        {
            RuleFor(empresa => empresa.DadosEmpresa.Endereco.CEP)
                .Must(cep => !string.IsNullOrWhiteSpace(cep))
                .WithMessage("CEP não informado.");
        }

        private void AssineRegraBairroInformado()
        {
            RuleFor(empresa => empresa.DadosEmpresa.Endereco.Bairro)
                .Must(bairro => !string.IsNullOrWhiteSpace(bairro))
                .WithMessage("Bairro não informado.");
        }

        private void AssineRegraCidadeInformado()
        {
            RuleFor(empresa => empresa.DadosEmpresa.Endereco.Cidade)
                .Must(cidade => cidade != null)
                .WithMessage("Cidade não informada.");
        }

        private void AssineRegraRuaInformado()
        {
            RuleFor(empresa => empresa.DadosEmpresa.Endereco.Rua)
                .Must(rua => !string.IsNullOrWhiteSpace(rua))
                .WithMessage("Rua não informado.");
        }

        private void AssineRegraNumeroInformado()
        {
            RuleFor(empresa => empresa.DadosEmpresa.Endereco.Numero)
                .Must(numero => !string.IsNullOrEmpty(numero))
                .WithMessage("Número não informado.");
        }

        private void AssineRegraEmailInformado()
        {
            RuleFor(empresa => empresa.DadosEmpresa.Endereco.Email)
                .Must(email => !string.IsNullOrEmpty(email))
                .WithMessage("E-mail não informado.");
        }

        private void AssineRegraEmailValido()
        {
            RuleFor(empresa => empresa.DadosEmpresa.Endereco.Email)
                .EmailAddress()
                .WithMessage("E-mail inválido.")
                .When(empresa => !string.IsNullOrEmpty(empresa.DadosEmpresa.Endereco.Email));
        }

        private void AssineRegraRegimeTributarioInformado()
        {
            RuleFor(empresa => empresa.DadosEmpresa.CodigoRegimeTributario)
                .Must(codigoRegimeTributario => codigoRegimeTributario != null)
                .WithMessage("Regime Tributário não informado.");
        }

        #endregion

        #region " DADOS DO CONTADOR "

        private void AssineRegraCnpjContadorValido()
        {
            RuleFor(empresa => empresa.DadosContador.Cnpj)
                .Must(cnpj => ValidacoesGerais.CnpjEstahValido(cnpj))
                .WithMessage("CNPJ do Contador Inválido.")
                .When(empresa => !string.IsNullOrEmpty(empresa.DadosContador.Cnpj));
        }

        private void AssineRegraCpfContadorValido()
        {
            RuleFor(empresa => empresa.DadosContador.CpfContador)
                .Must(cpf => ValidacoesGerais.CpfEstahValido(cpf))
                .WithMessage("CPF do Contador Inválido.")
                .When(empresa => !string.IsNullOrEmpty(empresa.DadosContador.CpfContador));
        }

        #endregion

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void MetodosComunsAoCadastroEAtualizacao()
        {
            AssineRegraRazaoSocialObrigatoria();
            AssineRegraNomeFantasiaObrigatoria();
            AssineRegraCnpjObrigatorio();
            AssineRegraCnpjValido();
            AssineRegraInscricaoEstadualObrigatorio();
            AssineRegraInscricaoMunicipalObrigatorio();
            AssineRegraCepInformado();
            AssineRegraBairroInformado();
            AssineRegraCidadeInformado();
            AssineRegraRuaInformado();
            AssineRegraNumeroInformado();
            AssineRegraEmailInformado();
            AssineRegraEmailValido();
            AssineRegraRegimeTributarioInformado();

            AssineRegraCnpjContadorValido();
            AssineRegraCpfContadorValido();
        }

        #endregion
    }
}
