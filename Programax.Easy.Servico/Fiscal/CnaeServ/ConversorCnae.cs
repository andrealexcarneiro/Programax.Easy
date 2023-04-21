using Programax.Easy.Negocio.Fiscal.CnaeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CnaeObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Fiscal.CnaeServ
{
    public class ConversorCnae : ConversorDeObjetoBasico<Cnae>, IConversorDeObjeto<Cnae>
    {
        public Cnae CopieObjetoParaPersistencia(Cnae objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCnae>();

            var cnaeBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Cnae();

            CopieTodasAsPropriedades(objetoDeNegocio, cnaeBase);

            return cnaeBase;
        }
    }
}
