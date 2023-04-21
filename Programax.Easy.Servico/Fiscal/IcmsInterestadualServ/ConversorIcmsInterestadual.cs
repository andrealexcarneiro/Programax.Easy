using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.Repositorio;
using System.Collections.Generic;

namespace Programax.Easy.Servico.Fiscal.IcmsInterestadualServ
{
    public class ConversorIcmsInterestadual : ConversorDeObjetoBasico<IcmsInterestadual>, IConversorDeObjeto<IcmsInterestadual>
    {
        public IcmsInterestadual CopieObjetoParaPersistencia(IcmsInterestadual objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioIcmsInterestadual>();

            var icmsInterestadual = repositorio.Consulte(objetoDeNegocio.Id) ?? new IcmsInterestadual();

            var listaIcmsEstadual = CopieListaDeTelefones(objetoDeNegocio, icmsInterestadual);

            CopieTodasAsPropriedades(objetoDeNegocio, icmsInterestadual);

            icmsInterestadual.ListaIcmsInterestadualEstado = listaIcmsEstadual;

            return icmsInterestadual;
        }

        private IList<IcmsInterestadualEstado> CopieListaDeTelefones(IcmsInterestadual objetoDeNegocio, IcmsInterestadual icmsInterEstadualBase)
        {
            var listaIcmsEstado = icmsInterEstadualBase.ListaIcmsInterestadualEstado;

            if (objetoDeNegocio != icmsInterEstadualBase)
            {
                listaIcmsEstado.Clear();
            }

            foreach (var item in objetoDeNegocio.ListaIcmsInterestadualEstado)
            {
                if (objetoDeNegocio != icmsInterEstadualBase)
                {
                    var itemCopiado = new IcmsInterestadualEstado();

                    CopieTodasAsPropriedades(item, itemCopiado);

                    itemCopiado.Id = 0;
                    itemCopiado.IcmsInterestadual = icmsInterEstadualBase;
                    listaIcmsEstado.Add(itemCopiado);
                }
                else
                {
                    item.IcmsInterestadual = icmsInterEstadualBase;
                }
            }

            return listaIcmsEstado;
        }
    }
}
