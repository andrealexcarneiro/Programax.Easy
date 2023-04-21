using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Fiscal.CfopServ
{
    public class ConversorCfop : ConversorDeObjetoBasico<Cfop>, IConversorDeObjeto<Cfop>
    {
        public Cfop CopieObjetoParaPersistencia(Cfop objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCfop>();

            var cfopBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Cfop();

            CopieTodasAsPropriedades(objetoDeNegocio, cfopBase);

            return cfopBase;
        }
    }
}
