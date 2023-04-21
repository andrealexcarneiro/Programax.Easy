using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CorObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.CorServ
{
    [Funcionalidade(EnumFuncionalidade.CORES)]
    public class ServicoCor : ServicoAkilSmallBusiness<Cor, ValidacaoCor, ConversorCor>
    {
        IRepositorioCor _repositorioCor;

        public ServicoCor()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Cor> RetorneRepositorio()
        {
            if (_repositorioCor == null)
            {
                _repositorioCor = FabricaDeRepositorios.Crie<IRepositorioCor>();
            }

            return _repositorioCor;
        }

        public List<Cor> ConsulteListaAtiva()
        {
            return _repositorioCor.ConsulteListaAtiva();
        }

        public List<Cor> ConsulteLista(string descricao, string status)
        {
            return _repositorioCor.ConsulteLista(descricao, status);
        }
    }
}
