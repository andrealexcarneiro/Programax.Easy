using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.MarcaServ
{
    [Funcionalidade(EnumFuncionalidade.MARCAS)]
    public class ServicoMarca : ServicoAkilSmallBusiness<Marca, ValidacaoMarca, ConversorMarca>
    {
        IRepositorioMarca _repositorioMarca;

        public ServicoMarca()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Marca> RetorneRepositorio()
        {
            if (_repositorioMarca == null)
            {
                _repositorioMarca = FabricaDeRepositorios.Crie<IRepositorioMarca>();
            }

            return _repositorioMarca;
        }

        public List<Marca> ConsulteListaAtiva()
        {
            return _repositorioMarca.ConsulteListaAtiva();
        }

        public List<Marca> ConsulteLista(int? idMarca, string descricao, string status)
        {
            return _repositorioMarca.ConsulteLista(idMarca, descricao, status);
        }
    }
}
