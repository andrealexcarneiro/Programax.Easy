using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.InventarioObj.Repositorio
{
    public interface IRepositorioInventario : IRepositorioBase<Inventario>
    {
        bool ConsulteProdutoEstahEmInventario(ProdutoObj.ObjetoDeNegocio.Produto produto);

        List<Inventario> ConsulteLista(DateTime? dataInicio, EnumStatusInventario? statusInventario, int? contagem, Marca marca, Categoria categoria, Grupo grupo, SubGrupo subGrupo);
    }
}
