using System.Collections.Generic;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.ConfiguracoesSistema.PermissaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.PermissaoObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.ConfiguracoesSistema.PermissaoServ
{
    [Funcionalidade(EnumFuncionalidade.PERMISSOES)]
    public class ServicoPermissao : ServicoAkilSmallBusiness<Permissao, ValidacaoPermissao, ConversorPermissao>
    {
        IRepositorioPermissao _repositorioPermissao;

        public ServicoPermissao()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Permissao> RetorneRepositorio()
        {
            if (_repositorioPermissao == null)
            {
                _repositorioPermissao = FabricaDeRepositorios.Crie<IRepositorioPermissao>();
            }

            return _repositorioPermissao;
        }

        //public override void CadastreLista(List<Permissao> listaObjetoDeNegocio)
        //{
        //    _repositorioPermissao.CadastreLista(listaObjetoDeNegocio);
        //}

        public List<Permissao> ConsulteLista()
        {
            return _repositorioPermissao.ConsulteLista();
        }

        public List<Permissao> ConsulteLista(int idGrupoAcesso)
        {
            return _repositorioPermissao.ConsulteLista(idGrupoAcesso);
        }
    }
}
