using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;


namespace Programax.Easy.Servico.Financeiro.MovimentacaoBancoServ
{
    public class ConversorMovimentacaoBanco : ConversorDeObjetoBasico<MovimentacaoBanco>, IConversorDeObjeto<MovimentacaoBanco>
    {
        public MovimentacaoBanco CopieObjetoParaPersistencia(MovimentacaoBanco objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioMovimentacaoBanco>();

            var movimentacaoBancoBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new MovimentacaoBanco();

            CopieTodasAsPropriedades(objetoDeNegocio, movimentacaoBancoBase);

            return movimentacaoBancoBase;
        }
    }
}
