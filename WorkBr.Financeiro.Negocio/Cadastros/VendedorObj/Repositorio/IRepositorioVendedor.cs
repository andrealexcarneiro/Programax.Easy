using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.VendedorObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.VendedorObj.Repositorio
{
    public interface IRepositorioVendedor : IRepositorioBase<VendedorAntigo>
    {
        List<VendedorAntigo> ConsulteListaDeVendedoresAtivos();
    }
}
