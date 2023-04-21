using NHibernate;
using Programax.Easy.Servico.Cadastros.CashBack.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Cadastros.CashBack.CashBackServ
{
    class RepositorioCash : RepositorioBase<Cashback>, IRepositorioCashBack
    {
        public RepositorioCash(ISession sesao)
          : base(sesao)
        {
        }

        public override List<Cashback> ConsulteLista()
        {
            return _session.QueryOver<Cashback>().OrderBy(Cashback => Cashback.Id).Asc.List().ToList();
            //return _registros.OrderBy(banco => banco.Descricao).ToList();
        }

        public List<Cashback> ConsulteLista(string descricao, string status)
        {
            throw new NotImplementedException();
        }

        //public List<Cashback> ConsulteLista(string descricao, string status)
        //{
        //    Expression<Func<Cashback, bool>> expressaoParaConsulta = Cashback => Cashback.Valor.ToArray("%" + descricao + "%");


        //    return _session.QueryOver<Cashback>().Where(expressaoParaConsulta).List().ToList();
        //}

        public List<Cashback> ConsulteListaAtiva()
        {
            return _session.QueryOver<Cashback>().Where(Cashback => Cashback.start == 0).OrderBy(Cashback => Cashback.Id).Asc.List().ToList();
        }

        public Cashback ConsultePeloCodigoBanco(string codigoBanco)
        {
            return _session.QueryOver<Cashback>().Where(banco => banco.Codigo == codigoBanco).Take(1).SingleOrDefault();
        }
    }
}
