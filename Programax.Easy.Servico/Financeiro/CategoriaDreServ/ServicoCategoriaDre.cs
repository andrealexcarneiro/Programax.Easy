using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.CategoriaDreObj.ObjetoDeNeocio;
using Programax.Easy.Negocio.Financeiro.CategoriaDreObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.CategoriaDreServ
{
    [Funcionalidade(EnumFuncionalidade.CATEGORIAFINANCEIRADRE)]
    public class ServicoCategoriaDre : ServicoAkilSmallBusiness<CategoriaDre, ValidacaoCategoriaDre, ConversorCategoriaDre>
    {
        IRepositorioCategoriaDre _repositorioCategoriaDre;

        public ServicoCategoriaDre()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<CategoriaDre> RetorneRepositorio()
        {
            if (_repositorioCategoriaDre == null)
            {
                _repositorioCategoriaDre = FabricaDeRepositorios.Crie<IRepositorioCategoriaDre>();
            }

            return _repositorioCategoriaDre;
        }

        public List<CategoriaDre> ConsulteLista()
        {
            return _repositorioCategoriaDre.ConsulteLista();
        }

        public List<CategoriaDre> ConsulteListaAtivos(SubGrupoCategoria grupoCategoria)
        {
            return _repositorioCategoriaDre.ConsulteListaAtivos(grupoCategoria);
        }

        public List<CategoriaDre> ConsulteListaAtivos()
        {
            return _repositorioCategoriaDre.ConsulteListaAtivos();
        }

        public List<CategoriaDre> ConsulteLista(string descricao, SubGrupoCategoria grupoCategoria, string status, EnumTipoCategoria? tipoCategoria = null)
        {
            return _repositorioCategoriaDre.ConsulteLista(descricao, grupoCategoria, status, tipoCategoria);
        }
    }
}
