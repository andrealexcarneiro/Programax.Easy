using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.NcmObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Fiscal.NcmServ
{
    public class ConvercorNcm : ConversorDeObjetoBasico<Ncm>, IConversorDeObjeto<Ncm>
    {
        public Ncm CopieObjetoParaPersistencia(Ncm objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioNcm>();

            var ncmDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Ncm();

            CopieTodasAsPropriedades(objetoDeNegocio, ncmDaBase);

            return ncmDaBase;
        }
    
    }
}
