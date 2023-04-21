using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Financeiro.ContaBancariaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContaBancariaObj.Repositorio;
using NHibernate;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using System.Linq.Expressions;
using NHibernate.Criterion;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Financeiro.ContaBancariaServ
{
    public class RepositorioContaBancaria : RepositorioBase<ContaBancaria>, IRepositorioContaBancaria
    {
        public RepositorioContaBancaria(ISession sessao)
            : base(sessao)
        {

        }

        public List<ContaBancaria> ConsulteLista(Banco banco, Agencia agencia, string numeroConta, string status, Pessoa pessoaTitular)
        {
            Agencia agenciaConsulta = null;

            Expression<Func<ContaBancaria, bool>> expressaoParaConsulta = contaBancaria => contaBancaria.NumeroConta.IsLike("%" + numeroConta + "%");

            if (banco != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaBancaria => agenciaConsulta.Banco.Id == banco.Id);
            }

            if (agencia != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaBancaria => contaBancaria.Agencia.Id == agencia.Id);
            }

            if (pessoaTitular != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaBancaria => contaBancaria.Pessoa.Id == pessoaTitular.Id);
            }

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaBancaria => contaBancaria.Status == status);
            }

            return _session.QueryOver<ContaBancaria>()
                .Left.JoinAlias(pessoa => pessoa.Agencia, () => agenciaConsulta)
                .Where(expressaoParaConsulta).List().ToList();
        }

        public ContaBancaria ConsultePeloNumeroConta(string numeroContaBancaria)
        {
            return _session.QueryOver<ContaBancaria>().Where(contaBancaria => contaBancaria.NumeroConta == numeroContaBancaria).Take(1).SingleOrDefault();
        }
    }
}
