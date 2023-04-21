using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.Repositorio
{
    public interface IRepositorioGrupoTributacaoIcms : IRepositorioBase<GrupoTributacaoIcms>
    {
        List<GrupoTributacaoIcms> ConsulteLista(string descricao);

        GrupoTributacaoIcms ConsulteTributacaoTerceirosId(int id);

        GrupoTributacaoIcms ConsulteTributacaoProducaoPropriaId(int id);
    }
}
