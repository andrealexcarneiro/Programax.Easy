using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.BancoObj.Repositorio
{
    public interface IRepositorioBanco : IRepositorioBase<Banco>
    {
        List<Banco> ConsulteLista(string descricao, string status);

        Banco ConsultePeloCodigoBanco(string codigoBanco);

        List<Banco> ConsulteListaAtiva();
    }
}
