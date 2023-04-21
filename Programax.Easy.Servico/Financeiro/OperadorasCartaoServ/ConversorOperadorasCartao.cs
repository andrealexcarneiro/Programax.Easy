using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Financeiro.OperadorasCartaoServ
{
    public class ConversorOperadorasCartao : ConversorDeObjetoBasico<OperadorasCartao>, IConversorDeObjeto<OperadorasCartao>
    {
        public OperadorasCartao CopieObjetoParaPersistencia(OperadorasCartao objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioOperadorasCartao>();

            var planoDeContasBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new OperadorasCartao();

            CopieTodasAsPropriedades(objetoDeNegocio, planoDeContasBase);

            return planoDeContasBase;
        }
    }
}
