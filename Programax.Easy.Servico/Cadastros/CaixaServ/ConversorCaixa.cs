using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CaixaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Cadastros.CaixaServ
{
    public class ConversorCaixa : ConversorDeObjetoBasico<Caixa>, IConversorDeObjeto<Caixa>
    {
        public Caixa CopieObjetoParaPersistencia(Caixa objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCaixa>();

            var caixaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Caixa();

            CopieTodasAsPropriedades(objetoDeNegocio, caixaDaBase);

            return caixaDaBase;
        }
    }
}
