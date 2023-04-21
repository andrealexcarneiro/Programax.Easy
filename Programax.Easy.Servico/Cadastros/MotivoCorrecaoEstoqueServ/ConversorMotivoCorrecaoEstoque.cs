using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Cadastros.MotivoCorrecaoEstoqueServ
{
    public class ConversorMotivoCorrecaoEstoque : ConversorDeObjetoBasico<MotivoCorrecaoEstoque>, IConversorDeObjeto<MotivoCorrecaoEstoque>
    {
        public MotivoCorrecaoEstoque CopieObjetoParaPersistencia(MotivoCorrecaoEstoque objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioMotivoCorrecaoEstoque>();

            var motivoCorrecaoEstoqueDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new MotivoCorrecaoEstoque();

            CopieTodasAsPropriedades(objetoDeNegocio, motivoCorrecaoEstoqueDaBase);

            return motivoCorrecaoEstoqueDaBase;
        }
    }
}
