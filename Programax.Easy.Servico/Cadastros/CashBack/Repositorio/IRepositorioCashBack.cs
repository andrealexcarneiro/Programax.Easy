using Programax.Easy.Servico.Cadastros.CashBack.CashBackServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;


using System.Collections.Generic;


namespace Programax.Easy.Servico.Cadastros.CashBack.Repositorio
{
    public interface IRepositorioCashBack : IRepositorioBase<Cashback>
    {
        List<Cashback> ConsulteLista(string descricao, string status);

        Cashback ConsultePeloCodigoBanco(string codigocash);

        List<Cashback> ConsulteListaAtiva();
    }
}
