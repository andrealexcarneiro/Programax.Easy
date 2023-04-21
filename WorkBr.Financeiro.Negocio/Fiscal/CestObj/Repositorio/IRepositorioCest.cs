using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.CestObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.CestObj.Repositorio
{
    public interface IRepositorioCest:IRepositorioBase<Cest>
    {
        List<Cest> ConsulteListaCest(string codigoCest, string descricao, string status);

        Cest ConsultePeloCodigoCest(string codigoCest);

        List<Cest> ConsulteListaDeCodigosCest(List<string> listaCodigosCest);        
    }
}
