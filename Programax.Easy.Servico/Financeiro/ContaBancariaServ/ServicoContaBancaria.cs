using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.Financeiro.ContaBancariaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContaBancariaObj.Repositorio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Financeiro.ContaBancariaServ
{
    [Funcionalidade(EnumFuncionalidade.CONTASBANCARIAS)]
    public class ServicoContaBancaria : ServicoAkilSmallBusiness<ContaBancaria, ValidacaoContaBancaria, ConversorContaBancaria>
    {
        IRepositorioContaBancaria _repositorioBanco;

        public ServicoContaBancaria()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<ContaBancaria> RetorneRepositorio()
        {
            if (_repositorioBanco == null)
            {
                _repositorioBanco = FabricaDeRepositorios.Crie<IRepositorioContaBancaria>();
            }

            return _repositorioBanco;
        }

        public List<ContaBancaria> ConsulteLista(Banco banco, Agencia agencia, string numeroConta, string status, Pessoa pessoaTitular)
        {
            return _repositorioBanco.ConsulteLista(banco, agencia, numeroConta, status, pessoaTitular);
        }

        public ContaBancaria ConsultePeloNumeroConta(string numeroContaBancaria)
        {
            return _repositorioBanco.ConsultePeloNumeroConta(numeroContaBancaria);
        }
    }
}
