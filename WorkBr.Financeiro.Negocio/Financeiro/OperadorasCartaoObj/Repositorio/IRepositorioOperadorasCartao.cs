using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.Repositorio
{
    public interface IRepositorioOperadorasCartao:IRepositorioBase<OperadorasCartao>
    {
        List<OperadorasCartao> ConsulteLista(string descricao, string status);

        OperadorasCartao ConsulteOperadorasPeloIdInformado(int idOperadora);
    }
}
