
using Programax.Easy.Negocio.Financeiro.CashBackObj.Repositorio;

using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;


namespace Programax.Easy.Servico.Cadastros.CashBack.CashBackServ
{
    class conversorCash : ConversorDeObjetoBasico<Cashback>, IConversorDeObjeto<Cashback>
    {
        public Cashback CopieObjetoParaPersistencia(Cashback objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCashBack>();

            var cashDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Cashback();

            CopieTodasAsPropriedades(objetoDeNegocio, cashDaBase);

            return cashDaBase;
        }
    }
}
