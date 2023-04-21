using System.Collections.Generic;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.ConfiguracoesSistema.GrupoAcessoServ
{
    [Funcionalidade(EnumFuncionalidade.GRUPOACESSO)]
    public class ServicoGrupoAcesso : ServicoAkilSmallBusiness<GrupoAcesso, ValidacaoGrupoAcesso, ConversorGrupoAcesso>
    {
        IRepositorioGrupoAcesso _repositorioGrupoAcesso;

        public ServicoGrupoAcesso()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<GrupoAcesso> RetorneRepositorio()
        {
            if (_repositorioGrupoAcesso == null)
            {
                _repositorioGrupoAcesso = FabricaDeRepositorios.Crie<IRepositorioGrupoAcesso>();
            }

            return _repositorioGrupoAcesso;
        }

        public List<GrupoAcesso> ConsulteLista()
        {
            return _repositorioGrupoAcesso.ConsulteLista();
        }

        public List<GrupoAcesso> ConsulteLista(string descricao)
        {
            return _repositorioGrupoAcesso.ConsulteLista(descricao);
        }
    }
}
