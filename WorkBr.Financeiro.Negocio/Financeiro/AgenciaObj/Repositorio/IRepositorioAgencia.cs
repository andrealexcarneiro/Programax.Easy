using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.AgenciaObj.Repositorio
{
    public interface IRepositorioAgencia : IRepositorioBase<Agencia>
    {
        List<Agencia> ConsulteLista(Banco banco, string nomeAgencia, string status);

        Agencia Consulte(int idBanco, string numeroAgencia);
    }
}
