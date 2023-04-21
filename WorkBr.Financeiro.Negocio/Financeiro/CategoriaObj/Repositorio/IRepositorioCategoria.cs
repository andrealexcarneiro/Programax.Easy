using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Negocio.Financeiro.CategoriaObj.Repositorio
{
    public interface IRepositorioCategoria : IRepositorioBase<CategoriaFinanceira>
    {
       List<CategoriaFinanceira> ConsulteLista(string descricao, SubGrupoCategoria categoria, string status, EnumTipoCategoria? tipoCategoria=null);

       List<CategoriaFinanceira> ConsulteListaAtivos(SubGrupoCategoria categoria);

       List<CategoriaFinanceira> ConsulteListaAtivos();
    }
}
