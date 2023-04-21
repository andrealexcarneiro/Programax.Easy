using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Integracao.TabelasAtualizadasIntegracaoDJObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Integracao.Enumeradores;

namespace Programax.Easy.Servico.Integracao.TabelasAtualizadasIntegracaoDJServ
{
    public class TabelasAtualizadasIntegracaoDJMap : MapeamentoBase<TabelasAtualizadasIntegracaoDJ>
    {
        public TabelasAtualizadasIntegracaoDJMap()
        {
            Table("TABELASATUALIZADASINTEGRACAODJ");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("TAB_ID");

            Map(ncm => ncm.TabelaAtualizada).Column("TAB_TABELA").CustomType<EnumTabelaAtualizada>();
            Map(ncm => ncm.IdRegistro).Column("TAB_ID_REGISTRO");
        }
    }
}
