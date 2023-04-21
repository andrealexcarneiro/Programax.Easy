using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.CaixaObj.Repositorio
{
    public interface IRepositorioCaixa : IRepositorioBase<Caixa>
    {
        List<Caixa> ConsulteLista(string nomeCaixa, string status, Pessoa pessoa);

        Caixa ConsultePeloNomeCaixa(string nomeCaixa);

        Caixa ConsultePeloFuncionario(Pessoa pessoa);
    }
}
