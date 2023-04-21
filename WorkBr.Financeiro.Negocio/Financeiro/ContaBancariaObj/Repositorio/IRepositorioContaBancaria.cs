using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContaBancariaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ContaBancariaObj.Repositorio
{
    public interface IRepositorioContaBancaria : IRepositorioBase<ContaBancaria>
    {
        System.Collections.Generic.List<ContaBancaria> ConsulteLista(Banco banco, Agencia agencia, string numeroConta, string status, Pessoa pessoaTitular);

        ContaBancaria ConsultePeloNumeroConta(string numeroContaBancaria);
    }
}
