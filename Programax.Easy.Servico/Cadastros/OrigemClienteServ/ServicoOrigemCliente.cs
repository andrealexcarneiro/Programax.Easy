using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.OrigemClienteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.OrigemClienteObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.OrigemClienteServ
{
    [Funcionalidade(EnumFuncionalidade.OrigemCliente)]
    public class ServicoOrigemCliente : ServicoAkilSmallBusiness<OrigemCliente, ValidacaoOrigemCliente, ConversorOrigemCliente>
    {
        IRepositorioOrigemCliente _repositorioOrigemCliente;

        public ServicoOrigemCliente()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<OrigemCliente> RetorneRepositorio()
        {
            if (_repositorioOrigemCliente == null)
            {
                _repositorioOrigemCliente = FabricaDeRepositorios.Crie<IRepositorioOrigemCliente>();
            }

            return _repositorioOrigemCliente;
        }

        public List<OrigemCliente> ConsulteLista()
        {
            return _repositorioOrigemCliente.ConsulteLista();
        }

        public List<OrigemCliente> ConsulteListaAtiva()
        {
            return _repositorioOrigemCliente.ConsulteListaAtiva();
        }

        public List<OrigemCliente> ConsulteLista(string descricao, string status)
        {
            return _repositorioOrigemCliente.ConsulteLista(descricao, status);
        }
    }
}
