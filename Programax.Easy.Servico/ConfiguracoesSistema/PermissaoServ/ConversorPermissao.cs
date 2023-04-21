using Programax.Easy.Negocio.ConfiguracoesSistema.PermissaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.PermissaoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.ConfiguracoesSistema.PermissaoServ
{
    public class ConversorPermissao : ConversorDeObjetoBasico<Permissao>, IConversorDeObjeto<Permissao>
    {
        public Permissao CopieObjetoParaPersistencia(Permissao objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioPermissao>();

            var permissaoDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Permissao();

            CopieTodasAsPropriedades(objetoDeNegocio, permissaoDaBase);

            return permissaoDaBase;
        }
    }
}
