using Programax.Easy.Negocio.ConfiguracoesSistema.InformacaoSistemaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.InformacaoSistemaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.ConfiguracoesSistema.InformacaoSistemaServ
{
    public class ConversorInformacaoSistema : ConversorDeObjetoBasico<InformacaoSistema>, IConversorDeObjeto<InformacaoSistema>
    {
        public InformacaoSistema CopieObjetoParaPersistencia(InformacaoSistema objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioInformacaoSistema>();

            var informaSistemaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new InformacaoSistema();

            CopieTodasAsPropriedades(objetoDeNegocio, informaSistemaBase);

            return informaSistemaBase;
        }
    }
}
