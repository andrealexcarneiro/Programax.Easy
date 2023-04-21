using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.ConfiguracoesSistema.LicencaDeUsoObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.Repositorio;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.LicencaDeUsoObj.ObjetoDeNegocio
{
    public class ValidacaoLicencaDeUso : ValidacaoBase<LicencaDeUso>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideAtualizacao()
        {
            AssineRegraChaveEhValida();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraChaveEhValida()
        {
            RuleFor(licenca => licenca.ChaveLiberacao)
                            .Must(chaveLiberacao => ChaveEhValida())
                            .WithMessage("Chave Inválida.");
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private bool ChaveEhValida()
        {
            return !ChaveEstahVazia() && ChaveEhParaEmpresa() && ChaveEhIgualOuMaiorDataAtual();
        }

        private bool ChaveEhParaEmpresa()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioEmpresa>();

            var empresa = repositorio.ConsulteUltimaEmpresa();

            string chaveDescriptografada = ObjetoValidado.ChaveLiberacao.Descriptografar();

            var parametrosChave = chaveDescriptografada.Split('|');

            return empresa.DadosEmpresa.Cnpj == parametrosChave[0];
        }

        private bool ChaveEstahVazia()
        {
            return string.IsNullOrEmpty(ObjetoValidado.ChaveLiberacao);
        }

        private bool ChaveEhIgualOuMaiorDataAtual()
        {
            string chaveDescriptografada = ObjetoValidado.ChaveLiberacao.Descriptografar();

            var parametrosChave = chaveDescriptografada.Split('|');

            return parametrosChave[1].ToDate() >= DateTime.Now.Date.AddDays(-1);
        }

        #endregion
    }
}
