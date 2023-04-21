using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.UnidadeMedidaServ
{
    [Funcionalidade(EnumFuncionalidade.UNIDADESMEDIAS)]
    public class ServicoUnidadeMedida : ServicoAkilSmallBusiness<UnidadeMedida, ValidacaoUnidadeMedida, ConversorUnidadeMedida>
    {
        IRepositorioUnidadeMedida _repositorioUnidadeMedida;

        public ServicoUnidadeMedida()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<UnidadeMedida> RetorneRepositorio()
        {
            if (_repositorioUnidadeMedida == null)
            {
                _repositorioUnidadeMedida = FabricaDeRepositorios.Crie<IRepositorioUnidadeMedida>();
            }

            return _repositorioUnidadeMedida;
        }

        public List<UnidadeMedida> ConsulteLista()
        {
            return _repositorioUnidadeMedida.ConsulteLista();
        }

        public List<UnidadeMedida> ConsulteListaAtiva()
        {
            return _repositorioUnidadeMedida.ConsulteListaAtiva();
        }

        public List<UnidadeMedida> ConsulteLista(string descricao, string abreviacao, string status)
        {
            return _repositorioUnidadeMedida.ConsulteLista(descricao, abreviacao,status);
        }
    }
}
