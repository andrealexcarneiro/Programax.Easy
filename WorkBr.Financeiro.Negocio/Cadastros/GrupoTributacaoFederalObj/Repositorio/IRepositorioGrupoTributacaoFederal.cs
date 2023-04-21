using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System.Collections.Generic;

namespace Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.Repositorio
{
    public interface IRepositorioGrupoTributacaoFederal : IRepositorioBase<GrupoTributacaoFederal>
    {
        List<GrupoTributacaoFederal> ConsulteLista(string descricao);

        GrupoTributacaoFederal ConsulteTributacaoTerceirosId(int id);

        GrupoTributacaoFederal ConsulteTributacaoProducaoPropriaId(int id);
    }
}
