using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.CategoriaServ
{
    [Funcionalidade(EnumFuncionalidade.CATEGORIAS)]
    public class ServicoCategoria : ServicoAkilSmallBusiness<Categoria, ValidacaoCategoria, ConversorCategoria>
    {
        IRepositorioCategoria _repositorioLinha;

        public ServicoCategoria()
        {
            RetorneRepositorio();
        }

        public List<Categoria> ConsulteLista()
        {
            return _repositorioLinha.ConsulteLista();
        }
        public override IRepositorioBase<Categoria> RetorneRepositorio()
        {
            if (_repositorioLinha == null)
            {
                _repositorioLinha = FabricaDeRepositorios.Crie<IRepositorioCategoria>();
            }

            return _repositorioLinha;
        }

        public List<Categoria> ConsulteLista(string descricao, string status, string v)
        {
            return _repositorioLinha.ConsulteLista(descricao, status);
        }

        public List<Categoria> ConsulteListaAtiva()
        {
            return _repositorioLinha.ConsulteListaAtiva();
        }

        
    }
}
