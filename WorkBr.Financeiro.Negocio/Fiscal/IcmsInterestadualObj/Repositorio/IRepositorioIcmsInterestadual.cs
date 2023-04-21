using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.Repositorio
{
    public interface IRepositorioIcmsInterestadual : IRepositorioBase<IcmsInterestadual>
    {
        IcmsInterestadual ConsultePorNcm(string codigoNcm);

        IcmsInterestadual ConsultePorNcmEUFDestino(string codigoNcm, string ufDestino);

        bool ExisteAliquotaParaONcmEOEstado(int ncmId, string uf);

        List<IcmsInterestadual> ConsulteListaPorNcms(List<NcmObj.ObjetoDeNegocio.Ncm> _listaNcms);

        List<IcmsInterestadualEstado> ConsulteListaIcmsEstadoPorCodigosNcmsEUF(List<string> listaCodigosNcm, string uf);
    }
}
