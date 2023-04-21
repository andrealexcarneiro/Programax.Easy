using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Financeiro.PlanoContasServ
{
    public class ConversorPlanoDeContas : ConversorDeObjetoBasico<PlanoDeContas>, IConversorDeObjeto<PlanoDeContas>
    {
        public PlanoDeContas CopieObjetoParaPersistencia(PlanoDeContas objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioPlanoDeContas>();

            var planoDeContasBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new PlanoDeContas();

            objetoDeNegocio.PlanoDeContasPadrao = planoDeContasBase.PlanoDeContasPadrao;

            CopieTodasAsPropriedades(objetoDeNegocio, planoDeContasBase);

            return planoDeContasBase;
        }
    }
}
