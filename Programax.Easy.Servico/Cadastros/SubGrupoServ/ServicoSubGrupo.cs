using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.SubGrupoServ
{
    [Funcionalidade(EnumFuncionalidade.SUBGRUPODEPRODUTOS)]
    public class ServicoSubGrupo : ServicoAkilSmallBusiness<SubGrupo, ValidacaoSubGrupo, ConversorSubGrupo>
    {
        IRepositorioSubGrupo _repositorioSubGrupo;

        public ServicoSubGrupo()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<SubGrupo> RetorneRepositorio()
        {
            if (_repositorioSubGrupo == null)
            {
                _repositorioSubGrupo = FabricaDeRepositorios.Crie<IRepositorioSubGrupo>();
            }

            return _repositorioSubGrupo;
        }

        public List<SubGrupo> ConsulteLista()
        {
            return _repositorioSubGrupo.ConsulteLista();
        }

        public List<SubGrupo> ConsulteListaAtiva(Grupo grupo)
        {
            return _repositorioSubGrupo.ConsulteListaAtiva(grupo);
        }
       

        public List<SubGrupo> ConsulteLista(string descricao, Grupo grupo, string status)
        {
            return _repositorioSubGrupo.ConsulteLista(descricao, grupo, status);
        }
    }
}
