using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.PautaIcmsObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.PautaIcmsObj.Repositorio
{
    public interface IRepositorioPautaIcms : IRepositorioBase<PautaIcms>
    {
        PautaIcms Consulte(Produto produto, Estado estado, Cidade cidade);
    }
}
