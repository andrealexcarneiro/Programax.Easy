using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Cadastros.RamoAtividadeServ
{
    public class ConversorRamoAtividade : ConversorDeObjetoBasico<RamoAtividade>, IConversorDeObjeto<RamoAtividade>
    {
        public RamoAtividade CopieObjetoParaPersistencia(RamoAtividade objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioRamoAtividade>();

            var ramoAtividadeBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new RamoAtividade();

            CopieTodasAsPropriedades(objetoDeNegocio, ramoAtividadeBase);

            return ramoAtividadeBase;
        }
    }
}
