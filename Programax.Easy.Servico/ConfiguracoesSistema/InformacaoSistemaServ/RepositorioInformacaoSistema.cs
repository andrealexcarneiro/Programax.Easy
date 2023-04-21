using NHibernate;
using Programax.Easy.Negocio.ConfiguracoesSistema.InformacaoSistemaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.InformacaoSistemaObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.ConfiguracoesSistema.InformacaoSistemaServ
{
    public class RepositorioInformacaoSistema : RepositorioBase<InformacaoSistema>, IRepositorioInformacaoSistema
    {
        public RepositorioInformacaoSistema(ISession sessao)
            : base(sessao)
        {

        }

        public InformacaoSistema ConsulteUltimaInformacaoSistema()
        {
            return _session.QueryOver<InformacaoSistema>().OrderBy(x => x.Id).Desc.Take(1).SingleOrDefault();
        }
    }
}
