using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.CidadeServ
{
    [Funcionalidade(EnumFuncionalidade.CIDADES)]
    public class ServicoCidade : ServicoAkilSmallBusiness<Cidade, ValidacaoCidade, ConversorCidade>
    {
        IRepositorioCidade _repositorioCidade;

        public ServicoCidade()
        {
            RetorneRepositorio();
        }

        public ServicoCidade(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {

        }

        public override IRepositorioBase<Cidade> RetorneRepositorio()
        {
            if (_repositorioCidade == null)
            {
                _repositorioCidade = FabricaDeRepositorios.Crie<IRepositorioCidade>();
            }

            return _repositorioCidade;
        }

        public List<Cidade> ConsulteListaCidades()
        {
            return _repositorioCidade.ConsulteLista();
        }

        public List<Cidade> ConsulteListaCidades(string descricao, string uf, string status)
        {
            return _repositorioCidade.ConsulteListaCidades(descricao, uf, status);
        }

        public List<Cidade> ConsulteListaCidadesAtivasPorEstado(string uf)
        {
            return _repositorioCidade.ConsulteListaAtiva(uf);
        }

        public Cidade ConsultePeloCodigoIbge(string codigoIbge)
        {
            return _repositorioCidade.ConsultePeloCodigoIbge(codigoIbge);
        }

        public Cidade ConsultePeloCodigoIbgeAtivo(string codigoIbge)
        {
            return _repositorioCidade.ConsultePeloCodigoIbgeAtivo(codigoIbge);
        }
    }
}
