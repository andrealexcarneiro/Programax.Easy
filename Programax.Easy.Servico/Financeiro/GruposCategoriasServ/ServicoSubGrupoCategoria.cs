using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;

namespace Programax.Easy.Servico.Financeiro.GruposCategoriasServ
{
    [Funcionalidade(EnumFuncionalidade.GRUPOCATEGORIA)]
    public class ServicoSubGrupoCategoria : ServicoAkilSmallBusiness<SubGrupoCategoria, ValidaSubcaoGrupoCategoria, ConversorSubGrupoCategoria>
    {
        IRepositorioSubGrupoCategoria _repositorioSubGrupoCategoria;

        public ServicoSubGrupoCategoria()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<SubGrupoCategoria> RetorneRepositorio()
        {
            if (_repositorioSubGrupoCategoria == null)
            {
                _repositorioSubGrupoCategoria = FabricaDeRepositorios.Crie<IRepositorioSubGrupoCategoria>();
            }

            return _repositorioSubGrupoCategoria;
        }

        public List<SubGrupoCategoria> ConsulteListaAtiva()
        {
            return _repositorioSubGrupoCategoria.ConsulteListaAtiva();
        }

        public List<SubGrupoCategoria> ConsulteLista(int? idGrupo, string descricao, string status)
        {
            return _repositorioSubGrupoCategoria.ConsulteLista(idGrupo, descricao, status);
        }
    }
}
