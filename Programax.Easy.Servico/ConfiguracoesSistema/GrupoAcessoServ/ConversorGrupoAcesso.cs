using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.ConfiguracoesSistema.GrupoAcessoServ
{
    public class ConversorGrupoAcesso : ConversorDeObjetoBasico<GrupoAcesso>, IConversorDeObjeto<GrupoAcesso>
    {
        public GrupoAcesso CopieObjetoParaPersistencia(GrupoAcesso objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioGrupoAcesso>();

            var grupoAcessoDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new GrupoAcesso();

            CopieTodasAsPropriedades(objetoDeNegocio, grupoAcessoDaBase);

            return grupoAcessoDaBase;
        }
    }
}
