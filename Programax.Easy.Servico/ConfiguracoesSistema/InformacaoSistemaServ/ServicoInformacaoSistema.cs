using Programax.Easy.Negocio.ConfiguracoesSistema.InformacaoSistemaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.InformacaoSistemaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Servico.Servicos;

namespace Programax.Easy.Servico.ConfiguracoesSistema.InformacaoSistemaServ
{
    public class ServicoInformacaoSistema : ServicoBase<InformacaoSistema, ValidacaoInformacaoSistema, ConversorInformacaoSistema>
    {
        IRepositorioInformacaoSistema _repositorioInformacaoSistema;

        public ServicoInformacaoSistema()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<InformacaoSistema> RetorneRepositorio()
        {
            if (_repositorioInformacaoSistema == null)
            {
                _repositorioInformacaoSistema = FabricaDeRepositorios.Crie<IRepositorioInformacaoSistema>();
            }

            return _repositorioInformacaoSistema;
        }

        public InformacaoSistema ConsulteUltimaInformacaoSistema()
        {
            return _repositorioInformacaoSistema.ConsulteUltimaInformacaoSistema();
        }
    }
}
