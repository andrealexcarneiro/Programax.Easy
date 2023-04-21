using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.FabricanteServ
{
    [Funcionalidade(EnumFuncionalidade.FABRICANTES)]
    public class ServicoFabricante : ServicoAkilSmallBusiness<Fabricante, ValidacaoFabricante, ConversorFabricante>
    {
        IRepositorioFabricante _repositorioFabricante;

        public ServicoFabricante()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Fabricante> RetorneRepositorio()
        {
            if (_repositorioFabricante == null)
            {
                _repositorioFabricante = FabricaDeRepositorios.Crie<IRepositorioFabricante>();
            }

            return _repositorioFabricante;
        }

        public List<Fabricante> ConsulteListaAtiva()
        {
            return _repositorioFabricante.ConsulteListaAtiva();
        }

        public List<Fabricante> ConsulteLista(int? idFabricante, string descricao, string status)
        {
            return _repositorioFabricante.ConsulteLista(idFabricante, descricao, status);
        }
    }
}
