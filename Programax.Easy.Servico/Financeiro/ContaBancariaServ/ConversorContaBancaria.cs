using Programax.Easy.Negocio.Financeiro.ContaBancariaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContaBancariaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Financeiro.ContaBancariaServ
{
    public class ConversorContaBancaria : ConversorDeObjetoBasico<ContaBancaria>, IConversorDeObjeto<ContaBancaria>
    {
        public ContaBancaria CopieObjetoParaPersistencia(ContaBancaria objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioContaBancaria>();

            var contaBancariaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new ContaBancaria();

            CopieTodasAsPropriedades(objetoDeNegocio, contaBancariaDaBase);

            return contaBancariaDaBase;
        }
    }
}
