using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.NcmObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Fiscal.NcmServ
{
    public class RepositorioNcm : RepositorioBase<Ncm>, IRepositorioNcm
    {
        public RepositorioNcm(ISession sessao)
            : base(sessao)
        {

        }

        public Ncm ConsultePeloCodigoNcm(string codigoNcm)
        {
            return _session.QueryOver<Ncm>().Where(ncm => ncm.CodigoNcm == codigoNcm).SingleOrDefault();
        }

        public List<Ncm> ConsulteLista(string codigoNcm, string descricao, string status)
        {
            Expression<Func<Ncm, bool>> expressaoParaConsulta = ncm => ncm.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrEmpty(codigoNcm))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(ncm => ncm.CodigoNcm.IsLike("%" + codigoNcm + "%"));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(ncm => ncm.Status == status);
            }

            return _session.QueryOver<Ncm>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<Ncm> ConsulteListaDeCodigosNcm(List<string> listaCodigosNcm)
        {
            return _session.QueryOver<Ncm>().Where(ncm => ncm.CodigoNcm.IsIn(listaCodigosNcm)).List().ToList();
        }

        public bool ExisteAlgumNcmForaDoPrazoDeValidade()
        {
            return _session.QueryOver<Ncm>().Where(ncm => ncm.DataValidadeIbpt < DateTime.Now.Date && ncm.Status == "A").Select(x => x.Id).RowCount() > 0;
        }
    }
}
