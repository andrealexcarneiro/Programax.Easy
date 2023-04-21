using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.SubGrupoServ
{
    public class SubGrupoMap : MapeamentoBase<SubGrupo>
    {
        public SubGrupoMap()
        {
            Table("PRODUTOSSUBGRUPOS");

            Id(subSubGrupo => subSubGrupo.Id).Column("SUBGRP_ID");

            Map(subSubGrupo => subSubGrupo.Descricao).Column("SUBGRP_DESCRICAO");
            Map(subSubGrupo => subSubGrupo.DataCadastro).Column("SUBGRP_DATA_CADASTRO");
            Map(subSubGrupo => subSubGrupo.Status).Column("SUBGRP_STATUS");

            References(subSubGrupo => subSubGrupo.Grupo).Column("SUBGRP_GRUP_ID").LazyLoad();
        }
    }
}
