using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Cadastros.EmpresaServ
{
    public class ConversorEmpresa : ConversorDeObjetoBasico<Empresa>, IConversorDeObjeto<Empresa>
    {
        public Empresa CopieObjetoParaPersistencia(Empresa objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioEmpresa>();

            var empresaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Empresa();

            CopieTodasAsPropriedades(objetoDeNegocio, empresaDaBase);

            return empresaDaBase;
        }
    }
}
