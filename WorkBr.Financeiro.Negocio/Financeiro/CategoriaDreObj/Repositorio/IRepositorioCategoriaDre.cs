using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Financeiro.CategoriaDreObj.ObjetoDeNeocio;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Negocio.Financeiro.CategoriaDreObj.Repositorio
{
    public interface IRepositorioCategoriaDre : IRepositorioBase<CategoriaDre>
    {
        List<CategoriaDre> ConsulteLista(string descricao, SubGrupoCategoria categoria, string status, EnumTipoCategoria? tipoCategoria = null);

        List<CategoriaDre> ConsulteListaAtivos(SubGrupoCategoria categoria);

        List<CategoriaDre> ConsulteListaAtivos();
       
    }
}
