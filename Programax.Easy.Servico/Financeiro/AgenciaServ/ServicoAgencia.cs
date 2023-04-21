using System.Collections.Generic;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Financeiro.AgenciaServ
{
    [Funcionalidade(EnumFuncionalidade.AGENCIAS)]
    public class ServicoAgencia : ServicoAkilSmallBusiness<Agencia, ValidacaoAgencia, ConversorAgencia>
    {
        IRepositorioAgencia _repositorioBanco;

        public ServicoAgencia()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Agencia> RetorneRepositorio()
        {
            if (_repositorioBanco == null)
            {
                _repositorioBanco = FabricaDeRepositorios.Crie<IRepositorioAgencia>();
            }

            return _repositorioBanco;
        }

        public List<Agencia> ConsulteLista(Banco banco, string nomeAgencia, string status)
        {
            return _repositorioBanco.ConsulteLista(banco, nomeAgencia, status);
        }

        public Agencia Consulte(int idBanco, string numeroAgencia)
        {
            return _repositorioBanco.Consulte(idBanco, numeroAgencia);
        }
    }
}
