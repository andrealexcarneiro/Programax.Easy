using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Financeiro.BancoParaMovimentoServ
{
    public class ConversorBancoParaMovimento : ConversorDeObjetoBasico<BancoParaMovimento>, IConversorDeObjeto<BancoParaMovimento>
    {
        public BancoParaMovimento CopieObjetoParaPersistencia(BancoParaMovimento objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioBancoParaMovimento>();

            var BancoDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new BancoParaMovimento();

            CopieTodasAsPropriedades(objetoDeNegocio, BancoDaBase);

            return BancoDaBase;
        }
    }
}
