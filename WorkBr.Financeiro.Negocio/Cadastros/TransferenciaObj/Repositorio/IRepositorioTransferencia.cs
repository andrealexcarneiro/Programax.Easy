using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.TransferenciaObj.Repositorio
{
    public interface IRepositorioTransferencia : IRepositorioBase<Transferencia>
    {
        bool ConsulteProdutoEstahEmInventario(ProdutoObj.ObjetoDeNegocio.Produto produto);

        List<Transferencia> ConsulteLista(DateTime? dataInicio, EnumStatusInventario? statusTransferencia);
    }
}
