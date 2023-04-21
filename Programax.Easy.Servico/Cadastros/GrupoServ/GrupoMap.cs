using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.GrupoServ
{
    public class GrupoMap : MapeamentoBase<Grupo>
    {
        public GrupoMap()
        {
            Table("PRODUTOSGRUPOS");

            Id(grupo => grupo.Id).Column("GRUP_ID");

            Map(grupo => grupo.Descricao).Column("GRUP_DESCRICAO");
            Map(grupo => grupo.DataCadastro).Column("GRUP_DATA_CADASTRO");
            Map(grupo => grupo.Status).Column("GRUP_STATUS");

            References(grupo => grupo.Categoria).Column("GRUP_LINHA_ID").LazyLoad();
        }
    }
}
