using Programax.Easy.Negocio.Financeiro.GruposDreObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Negocio.Financeiro.GruposDreObj.Repositorio
{
    public interface IRepositorioGrupoDre : IRepositorioBase<GrupoDre>
    {
        List<GrupoDre> ConsulteLista(int? idGrupo, string descricao, string status);

        List<GrupoDre> ConsulteListaAtiva();
    }
}
