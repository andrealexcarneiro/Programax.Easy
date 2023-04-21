using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.MarcaServ
{
    public class MarcaMap : MapeamentoBase<Marca>
    {
        public MarcaMap()
            : base()
        {
            Table("MARCAS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("MARC_ID");

            Map(marca => marca.Ativo).Column("MARC_STATUS");
            Map(marca => marca.DataCadastro).Column("MARC_DATA_CADASTRO");
            Map(marca => marca.Descricao).Column("MARC_DESCRICAO");
        }
    }
}
