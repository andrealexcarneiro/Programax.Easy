using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CaixaObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.CaixaServ
{
    [Funcionalidade(EnumFuncionalidade.CADASTROCAIXA)]
    public class ServicoCaixa : ServicoAkilSmallBusiness<Caixa, ValidacaoCaixa, ConversorCaixa>
    {
        private IRepositorioCaixa _repositorioCaixa;

        public ServicoCaixa()
        {
            RetorneRepositorio();
        }

        public ServicoCaixa(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Caixa> RetorneRepositorio()
        {
            if (_repositorioCaixa == null)
            {
                _repositorioCaixa = FabricaDeRepositorios.Crie<IRepositorioCaixa>();
            }

            return _repositorioCaixa;
        }

        public List<Caixa> ConsulteLista(string nomeCaixa, string status, Pessoa pessoa)
        {
            return _repositorioCaixa.ConsulteLista(nomeCaixa, status, pessoa);
        }

        public Caixa ConsultePeloFuncionario(Pessoa pessoa)
        {
            return _repositorioCaixa.ConsultePeloFuncionario(pessoa);
        }

    }
}
