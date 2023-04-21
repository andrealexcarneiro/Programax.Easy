using Programax.Easy.Negocio.Movimentacao.AjusteFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.AjusteFiscalObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Movimentacao.AjusteFiscalServ
{
    public class ConversorAjusteFiscal : ConversorDeObjetoBasico<AjusteFiscal>, IConversorDeObjeto<AjusteFiscal>
    {
        public AjusteFiscal CopieObjetoParaPersistencia(AjusteFiscal objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioAjusteFiscal>();

            var ajusteFiscalBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new AjusteFiscal();

            CopieTodasAsPropriedades(objetoDeNegocio, ajusteFiscalBase);

            return ajusteFiscalBase;
        }
    }
}
