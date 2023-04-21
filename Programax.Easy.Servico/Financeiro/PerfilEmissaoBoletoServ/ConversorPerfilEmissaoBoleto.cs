using Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Financeiro.PerfilEmissaoBoletoSev
{
    public class ConversorPerfilEmissaoBoleto : ConversorDeObjetoBasico<PerfilEmissaoBoleto>, IConversorDeObjeto<PerfilEmissaoBoleto>
    {
        public PerfilEmissaoBoleto CopieObjetoParaPersistencia(PerfilEmissaoBoleto objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioPerfilEmissaoBoleto>();

            var perfilDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new PerfilEmissaoBoleto();

            CopieTodasAsPropriedades(objetoDeNegocio, perfilDaBase);

            return perfilDaBase;
        }
    }
}
