using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;

namespace Programax.Easy.Servico.Financeiro.GruposCategoriasServ
{
    [Funcionalidade(EnumFuncionalidade.GRUPOCATEGORIA)]
    public class ServicoGrupoCategoria : ServicoAkilSmallBusiness<GrupoCategoria, ValidacaoGrupoCategoria, ConversorGrupoCategoria>
    {
        IRepositorioGrupoCategoria _repositorioGrupoCategoria;

        public ServicoGrupoCategoria()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<GrupoCategoria> RetorneRepositorio()
        {
            if (_repositorioGrupoCategoria == null)
            {
                _repositorioGrupoCategoria = FabricaDeRepositorios.Crie<IRepositorioGrupoCategoria>();
            }

            return _repositorioGrupoCategoria;
        }

        public List<GrupoCategoria> ConsulteListaAtiva()
        {
            return _repositorioGrupoCategoria.ConsulteListaAtiva();
        }

        public List<GrupoCategoria> ConsulteLista(int? idGrupo, string descricao, string status)
        {
            return _repositorioGrupoCategoria.ConsulteLista(idGrupo, descricao, status);
        }
    }
}
