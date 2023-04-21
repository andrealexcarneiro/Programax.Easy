using Programax.Easy.Negocio.Financeiro.PlanoContasDreObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasDreObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Financeiro.PlanoContasDreServ
{
    public class ConversorPlanoDeContasDre : ConversorDeObjetoBasico<PlanoContaDre>, IConversorDeObjeto<PlanoContaDre>
    {
        public PlanoContaDre CopieObjetoParaPersistencia(PlanoContaDre objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioPlanoDeContasDre>();

            var planoDeContasBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new PlanoContaDre();

            objetoDeNegocio.PlanoDeContasPadrao = planoDeContasBase.PlanoDeContasPadrao;

            CopieTodasAsPropriedades(objetoDeNegocio, planoDeContasBase);

            return planoDeContasBase;
        }
    }
}
