using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CaixaObj.Repositorio;
using NHibernate;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using System.Linq.Expressions;
using NHibernate.Criterion;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Cadastros.CaixaServ
{
    public class RepositorioCaixa : RepositorioBase<Caixa>, IRepositorioCaixa
    {
        public RepositorioCaixa(ISession sessao)
            : base(sessao)
        {

        }

        public List<Caixa> ConsulteLista(string nomeCaixa, string status, Pessoa pessoa)
        {
            Expression<Func<Caixa, bool>> expressaoParaConsulta = caixa => caixa.NomeCaixa.IsLike("%" + nomeCaixa + "%");

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(caixa => caixa.Status == status);
            }

            if (pessoa != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(caixa => caixa.Funcionario.Id == pessoa.Id);
            }

            return _session.QueryOver<Caixa>().Where(expressaoParaConsulta).List().ToList();
        }

        public Caixa ConsultePeloNomeCaixa(string nomeCaixa)
        {
            return _session.QueryOver<Caixa>().Where(caixa => caixa.NomeCaixa == nomeCaixa).SingleOrDefault();
        }

        public Caixa ConsultePeloFuncionario(Pessoa pessoa)
        {
            return _session.QueryOver<Caixa>().Where(caixa => caixa.Funcionario.Id == pessoa.Id && caixa.Status == "A").SingleOrDefault();
        }
    }
}
