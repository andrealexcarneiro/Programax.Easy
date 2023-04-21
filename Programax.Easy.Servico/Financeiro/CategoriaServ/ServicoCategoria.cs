using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.CategoriaServ
{
    [Funcionalidade(EnumFuncionalidade.CATEGORIAFINANCEIRA)]
    public class ServicoCategoria : ServicoAkilSmallBusiness<CategoriaFinanceira, ValidacaoCategoria, ConversorCategoria>
    {
        IRepositorioCategoria _repositorioCategoria;

        public ServicoCategoria()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<CategoriaFinanceira> RetorneRepositorio()
        {
            if (_repositorioCategoria == null)
            {
                _repositorioCategoria = FabricaDeRepositorios.Crie<IRepositorioCategoria>();
            }

            return _repositorioCategoria;
        }

        public List<CategoriaFinanceira> ConsulteLista()
        {
            return _repositorioCategoria.ConsulteLista();
        }

        public List<CategoriaFinanceira> ConsulteListaAtivos(SubGrupoCategoria grupoCategoria)
        {
            return _repositorioCategoria.ConsulteListaAtivos(grupoCategoria);
        }

        public List<CategoriaFinanceira> ConsulteListaAtivos()
        {
            return _repositorioCategoria.ConsulteListaAtivos();
        }

        public List<CategoriaFinanceira> ConsulteLista(string descricao, SubGrupoCategoria grupoCategoria, string status, EnumTipoCategoria? tipoCategoria = null)
        {
            return _repositorioCategoria.ConsulteLista(descricao, grupoCategoria, status, tipoCategoria);
        }
    }
}
