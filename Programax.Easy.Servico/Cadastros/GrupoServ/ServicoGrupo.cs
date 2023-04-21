using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.GrupoServ
{
    [Funcionalidade(EnumFuncionalidade.GRUPODEPRODUTOS)]
    public class ServicoGrupo : ServicoAkilSmallBusiness<Grupo, ValidacaoGrupo, ConversorGrupo>
    {
        IRepositorioGrupo _repositorioGrupo;

        public ServicoGrupo()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Grupo> RetorneRepositorio()
        {
            if (_repositorioGrupo == null)
            {
                _repositorioGrupo = FabricaDeRepositorios.Crie<IRepositorioGrupo>();
            }

            return _repositorioGrupo;
        }

        public List<Grupo> ConsulteLista()
        {
            return _repositorioGrupo.ConsulteLista();
        }

        public List<Grupo> ConsulteListaAtivos(Categoria categoria)
        {
            return _repositorioGrupo.ConsulteListaAtivos(categoria);
        }

        public List<Grupo> ConsulteListaAtivos()
        {
            return _repositorioGrupo.ConsulteListaAtivos();
        }

        public List<Grupo> ConsulteLista(string descricao, Categoria categoria, string status)
        {
            return _repositorioGrupo.ConsulteLista(descricao, categoria, status);
        }
    }
}
