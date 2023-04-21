using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.TransferenciaObj.Repositorio
{
    public interface IRepositorioItemTransferencia : IRepositorioBase<ItemTransferencia>
    {
        List<ItemTransferencia> ConsulteLista(int Id);

        List<ItemTransferencia> ConsulteProduto(int Id);
    }
}
