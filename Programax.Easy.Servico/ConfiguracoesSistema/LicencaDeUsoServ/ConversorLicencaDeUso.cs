using Programax.Easy.Negocio.ConfiguracoesSistema.LicencaDeUsoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.LicencaDeUsoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.ConfiguracoesSistema.LicencaDeUsoServ
{
    public class ConversorLicencaDeUso : ConversorDeObjetoBasico<LicencaDeUso>, IConversorDeObjeto<LicencaDeUso>
    {
        public LicencaDeUso CopieObjetoParaPersistencia(LicencaDeUso objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioLicencaDeUso>();

            var licencaDeUsoBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new LicencaDeUso();

            CopieTodasAsPropriedades(objetoDeNegocio, licencaDeUsoBase);

            return licencaDeUsoBase;
        }
    }
}
