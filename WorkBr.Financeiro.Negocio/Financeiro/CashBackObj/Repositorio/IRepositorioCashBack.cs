using Programax.Easy.Negocio.Financeiro.CashBackObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CashBack.CashBackServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System.Collections.Generic;


namespace Programax.Easy.Negocio.Financeiro.CashBackObj.Repositorio
{
    public interface IRepositorioCashBack : IRepositorioBase<Cashback>
    {
        List<Cashback> ConsulteLista(string descricao, string status);

        Cashback ConsultePeloCodigoBanco(string codigoBanco);

        List<Cashback> ConsulteListaAtiva();
    }
}
