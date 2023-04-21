using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.FabricanteServ
{
    public class FabricanteMap : MapeamentoBase<Fabricante>
    {
        public FabricanteMap()
            : base()
        {
            Table("FABRICANTES");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("FABRICANTE_ID");

            Map(fabricante => fabricante.Ativo).Column("FABRICANTE_STATUS");
            Map(fabricante => fabricante.DataCadastro).Column("FABRICANTE_DATA_CADASTRO");
            Map(fabricante => fabricante.Descricao).Column("FABRICANTE_DESCRICAO");
        }
    }
}
