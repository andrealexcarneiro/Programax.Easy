using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using System.Text.RegularExpressions;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.Repositorio;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    public class ValidacaoPessoa : ValidacaoBase<Pessoa>
    {
        #region " VARIÁVEIS "

        private Parametros _parametros;

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            ValidacoesComunsAoCadastroEAtualizacao();
        }

        public override void ValideAtualizacao()
        {
            ValidacoesComunsAoCadastroEAtualizacao();
        }
        public override FluentValidation.Results.ValidationResult Validate(Pessoa instance)
        {
            PreenchaParametrosSistema();

            return base.Validate(instance);
        }

        #endregion

        #region " DADOS GERAIS "

        private void AssineRegraRazaoSocialObrigatoria()
        {
            RuleFor(pessoa => pessoa.DadosGerais.Razao)
                .Must(razao => !string.IsNullOrEmpty(razao))
                .WithMessage("Razão Social/Nome não foi informado.");
        }

        //private void AssineRegraDataCadastroObrigatoria()
        //{
        //    RuleFor(pessoa => pessoa.DadosGerais.DataCadastro)
        //        .Must(dataCadastro => dataCadastro != null && dataCadastro > DateTime.MinValue)
        //        .WithMessage("Data de cadastro não informada.");
        //}

        private void AssineRegraTipoCadastroObrigatorio()
        {
            RuleFor(pessoa => pessoa.DadosGerais.EhCliente)
                .Must((pessoa, cliente) => pessoa.DadosGerais.EhCliente || pessoa.DadosGerais.EhFornecedor || pessoa.DadosGerais.EhFuncionario || pessoa.DadosGerais.EhTransportadora)
                .WithMessage("É necessário selecionar pelo menos um tipo de cadastro.");
        }

        private void AssineRegraCpfCnpjObrigatorio()
        {
            RuleFor(pessoa => pessoa.DadosGerais.CpfCnpj)
                .Must(cpfCnpj => !string.IsNullOrWhiteSpace(cpfCnpj))
                .WithMessage("Cpf/Cnpj não foi informado.")
                .When(pessoa => CpfCnpjEhObrigatorio());
        }

        private void AssineRegraEnderecoObrigatorio()
        {
            RuleFor(pessoa => pessoa.ListaDeEnderecos)
                .Must(End => End.Count() != 0)
                .WithMessage("Endereço não foi informado.")
                .When(pessoa => EnderecoEhObrigatorio());
        }

        private void AssineRegraPermiteCpfCnpjJaCadastrado()
        {
            RuleFor(pessoa => pessoa.DadosGerais.CpfCnpj)
                .Must(cpfCnpj => CpfCnpjNaoExistenteNoBancoDeDados(cpfCnpj))
                .WithMessage("Cpf/Cnpj já existente.")
                .When(pessoa => NaoPermiteCadastroDeParceiroComMesmoCpfCnpj());
        }

        private void AssineRegraCpfValido()
        {
            RuleFor(pessoa => pessoa.DadosGerais.CpfCnpj)
                .Must(cpfCnpj => ValidacoesGerais.CpfEstahValido(cpfCnpj))
                .WithMessage("CPF Inválido.")
                .When(pessoa => !string.IsNullOrEmpty(pessoa.DadosGerais.CpfCnpj) &&
                          pessoa.DadosGerais.TipoPessoa != null && pessoa.DadosGerais.TipoPessoa == EnumTipoPessoa.PESSOAFISICA);
        }

        private void AssineRegraCnpjValido()
        {
            RuleFor(pessoa => pessoa.DadosGerais.CpfCnpj)
                .Must(cpfCnpj => ValidacoesGerais.CnpjEstahValido(cpfCnpj))
                .WithMessage("CNPJ Inválido.")
                .When(pessoa => !string.IsNullOrEmpty(pessoa.DadosGerais.CpfCnpj) &&
                          pessoa.DadosGerais.TipoPessoa != null && pessoa.DadosGerais.TipoPessoa == EnumTipoPessoa.PESSOAJURIDICA);
        }

        #endregion

        #region " EMPRESA "

        private void AssineRegraEmailPrincipalValido()
        {
            RuleFor(pessoa => pessoa.EmpresaPessoa.EmailPrincipal)
                .EmailAddress()
                .WithMessage("E-mail principal inválido.")
                .When(pessoa => !string.IsNullOrEmpty(pessoa.EmpresaPessoa.EmailPrincipal));
        }

        private void AssineRegraEmailCobrancaValido()
        {
            RuleFor(pessoa => pessoa.EmpresaPessoa.EmailCobranca)
                .EmailAddress()
                .WithMessage("E-mail de cobrança inválido.")
                .When(pessoa => !string.IsNullOrEmpty(pessoa.EmpresaPessoa.EmailCobranca));
        }

        #endregion

        #region " DADOS PESSOAIS "

        private void AssineRegraDataDeNascimentoMenorQueDataCadastro()
        {
            RuleFor(pessoa => pessoa.DadosPessoais.DataDeNascimento)
                .Must((pessoa, dataDeNascimento) => dataDeNascimento.Value < pessoa.DadosGerais.DataCadastro)
                .WithMessage("Data de Nascimento maior que a data de cadastro.")
                .When(pessoa => pessoa.DadosGerais.DataCadastro != null && pessoa.DadosPessoais.DataDeNascimento != null);
        }

        private void AssineRegraDataDeNascimentoMenorQueDataEmissaoIdentidade()
        {
            RuleFor(pessoa => pessoa.DadosPessoais.DataDeNascimento)
                .Must((pessoa, dataDeNascimento) => dataDeNascimento.Value < pessoa.DadosPessoais.DataEmissao)
                .WithMessage("Data de Nascimento maior que a data de emissão da identidade.")
                .When(pessoa => pessoa.DadosPessoais.DataEmissao != null && pessoa.DadosPessoais.DataDeNascimento != null);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void ValidacoesComunsAoCadastroEAtualizacao()
        {
            AssineRegraRazaoSocialObrigatoria();
            AssineRegraTipoCadastroObrigatorio();
            AssineRegraCpfCnpjObrigatorio();

            AssineRegraEnderecoObrigatorio();

            AssineRegraPermiteCpfCnpjJaCadastrado();

            AssineRegraCpfValido();
            AssineRegraCnpjValido();

            AssineRegraEmailPrincipalValido();
            AssineRegraEmailCobrancaValido();

            AssineRegraDataDeNascimentoMenorQueDataCadastro();
            AssineRegraDataDeNascimentoMenorQueDataEmissaoIdentidade();
        }

        private bool CpfCnpjEhObrigatorio()
        {
            return !_parametros.ParametrosCadastros.PermiteCadastroParceiroSemCpfCnpj;
        }

        private bool EnderecoEhObrigatorio()
        {
            return _parametros.ParametrosCadastros.ValidaEndereco;
        }

        private bool NaoPermiteCadastroDeParceiroComMesmoCpfCnpj()
        {
            return !_parametros.ParametrosCadastros.PermiteCadastroParceiroComMesmoCpfCnpj;
        }

        private bool CpfCnpjNaoExistenteNoBancoDeDados(string cpfCnpj)
        {
            var repositorioPessoa = FabricaDeRepositorios.Crie<IRepositorioPessoa>();

            var pessoaConsultada = repositorioPessoa.ConsultePessoaPeloCnpjOuCpf(cpfCnpj);

            return pessoaConsultada == null || pessoaConsultada.Id == ObjetoValidado.Id;
        }

        private void PreenchaParametrosSistema()
        {
            var repositorioParametros = FabricaDeRepositorios.Crie<IRepositorioParametros>();

            _parametros = repositorioParametros.ConsulteParametros();
        }

        #endregion
    }
}
