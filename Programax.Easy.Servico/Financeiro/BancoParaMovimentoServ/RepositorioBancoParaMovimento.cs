using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using System.Linq.Expressions;
using NHibernate.Criterion;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.Repositorio;

namespace Programax.Easy.Servico.Financeiro.BancoParaMovimentoServ
{
    public class RepositorioBancoParaMovimento : RepositorioBase<BancoParaMovimento>, IRepositorioBancoParaMovimento
    {
        public RepositorioBancoParaMovimento(ISession sessao)
            : base(sessao)
        {

        }

        public List<BancoParaMovimento> ConsulteLista(string nomeBanco, string status)
        {
            Expression<Func<BancoParaMovimento, bool>> expressaoParaConsulta = banco =>banco.NomeBanco.IsLike("%" + nomeBanco + "%");

            if (nomeBanco == string.Empty)
                expressaoParaConsulta = banco => banco.NomeBanco != string.Empty;

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(banco => banco.Status == status);
            }

            return _session.QueryOver<BancoParaMovimento>().Where(expressaoParaConsulta).List().ToList();
        }

        public BancoParaMovimento ConsulteBanco()
        {
            return _session.QueryOver<BancoParaMovimento>().Where(banco => banco.Status == "A").Take(1).SingleOrDefault();
        }

        public BancoParaMovimento ConsulteBanco(bool ehPadrao)
        {
            return _session.QueryOver<BancoParaMovimento>().Where(banco => banco.Status == "A" && banco.TornarPadrao == ehPadrao).SingleOrDefault();
        }

        public BancoParaMovimento ConsultePeloNomeBanco(string nomeBanco)
        {
            return _session.QueryOver<BancoParaMovimento>().Where(banco => banco.NomeBanco == nomeBanco).SingleOrDefault();
        }
    }
}
