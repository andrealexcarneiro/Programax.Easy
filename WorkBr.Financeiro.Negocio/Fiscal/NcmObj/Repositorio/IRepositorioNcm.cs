using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System.Collections.Generic;

namespace Programax.Easy.Negocio.Fiscal.NcmObj.Repositorio
{
    public interface IRepositorioNcm : IRepositorioBase<Ncm>
    {
        List<Ncm> ConsulteLista(string codigoNcm, string descricao, string status);

        Ncm ConsultePeloCodigoNcm(string codigoNcm);

        List<Ncm> ConsulteListaDeCodigosNcm(List<string> listaCodigosNcm);

        bool ExisteAlgumNcmForaDoPrazoDeValidade();
    }
}
