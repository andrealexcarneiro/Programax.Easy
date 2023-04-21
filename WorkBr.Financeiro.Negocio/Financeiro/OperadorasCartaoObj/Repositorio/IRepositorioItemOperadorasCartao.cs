using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.Repositorio
{
    public interface IRepositorioItemOperadorasCartao:IRepositorioBase<ItemOperadorasCartao>
    {
        List<ItemOperadorasCartao> ConsulteLista(int idOperadora);
    }
}
