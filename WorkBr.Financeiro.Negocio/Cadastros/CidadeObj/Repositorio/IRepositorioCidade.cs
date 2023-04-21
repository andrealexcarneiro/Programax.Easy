using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.CidadeObj.Repositorio
{
    public interface IRepositorioCidade : IRepositorioBase<Cidade>
    {
        List<Cidade> ConsulteListaAtiva(string uf);

        List<Cidade> ConsulteListaCidades(string descricao, string uf, string status);

        Cidade ConsultePeloCodigoIbge(string codigoIbge);

        Cidade ConsultePeloCodigoIbgeAtivo(string codigoIbge);
    }
}
