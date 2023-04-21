using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.Repositorio;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using NHibernate.Transform;

namespace Programax.Easy.Servico.Fiscal.IcmsInterestadualServ
{
    public class RepositorioIcmsInterestadual : RepositorioBase<IcmsInterestadual>, IRepositorioIcmsInterestadual
    {
        public RepositorioIcmsInterestadual(ISession sessao)
            : base(sessao)
        {
        }

        public IcmsInterestadual ConsultePorNcm(string codigoNcm)
        {
            Ncm ncm = null;

            return _session.QueryOver<IcmsInterestadual>()
                .JoinAlias(icms => icms.Ncm, () => ncm)
                .Where(icms => ncm.CodigoNcm == codigoNcm).SingleOrDefault();
        }

        public IcmsInterestadual ConsultePorNcmEUFDestino(string codigoNcm, string ufDestino)
        {
            Ncm ncm = null;
            IcmsInterestadualEstado icmsEstado = null;

            return _session.QueryOver<IcmsInterestadual>()
                .Left.JoinAlias(icms => icms.Ncm, () => ncm)
                .Left.JoinAlias(icms => icms.ListaIcmsInterestadualEstado, () => icmsEstado)
                .Where(icms => ncm.CodigoNcm == codigoNcm && icmsEstado.UF == ufDestino).Take(1).SingleOrDefault();
        }

        public bool ExisteAliquotaParaONcmEOEstado(int ncmId, string uf)
        {
            IcmsInterestadualEstado icmsEstado = null;

            return _session.QueryOver<IcmsInterestadual>().Left.JoinAlias(x => x.ListaIcmsInterestadualEstado, () => icmsEstado)
                .Where(icms => icms.Ncm.Id == ncmId && icmsEstado.UF == uf).Take(1).SingleOrDefault() != null;
        }

        public List<IcmsInterestadual> ConsulteListaPorNcms(List<Ncm> _listaNcms)
        {
            List<int> listaids = new List<int>();

            foreach (var item in _listaNcms)
            {
                listaids.Add(item.Id);
            }

            IcmsInterestadualEstado icmsEstado = null;

            return _session.QueryOver<IcmsInterestadual>()
                .Left.JoinAlias(x => x.ListaIcmsInterestadualEstado, () => icmsEstado)
                .TransformUsing(Transformers.DistinctRootEntity)
                .Where(icms => icms.Ncm.Id.IsIn(listaids)).List().ToList();
        }

        public List<IcmsInterestadualEstado> ConsulteListaIcmsEstadoPorCodigosNcmsEUF(List<string> listaCodigosNcm, string uf)
        {
            IcmsInterestadual icmsInterestadual = null;
            Ncm ncm = null;

            return _session.QueryOver<IcmsInterestadualEstado>()
                .Left.JoinAlias(icmsEstado => icmsEstado.IcmsInterestadual, () => icmsInterestadual)
                .Left.JoinAlias(icmsEstado => icmsInterestadual.Ncm, () => ncm)
                .Where(icmsEstado => icmsEstado.UF == uf && ncm.CodigoNcm.IsIn(listaCodigosNcm)).List().ToList();
        }
    }
}
