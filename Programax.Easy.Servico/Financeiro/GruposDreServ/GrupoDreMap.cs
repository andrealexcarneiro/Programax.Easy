using Programax.Easy.Negocio.Financeiro.GruposDreObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.GruposDreServ
{
    public class GrupoDreMap : MapeamentoBase<GrupoDre>
    {
        public GrupoDreMap()
          : base()
        {
            Table("GRUPOSDRE");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("GRUPODRE_ID");

            Map(grupo => grupo.Ativo).Column("GRUPODRE_STATUS");
            Map(grupo => grupo.DataCadastro).Column("GRUPODRE_DATA_CADASTRO");
            Map(grupo => grupo.Descricao).Column("GRUPODRE_DESCRICAO");
        }
    }
}
