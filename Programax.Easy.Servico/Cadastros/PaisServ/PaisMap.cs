using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.PaisObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.PaisServ
{
    public class PaisMap : MapeamentoBase<Pais>
    {
        public PaisMap()
        {
            Table("PAISES");

            Id(pais => pais.Id).Column("PAIS_ID");

            Map(pais => pais.CodigoPais).Column("PAIS_CODIGO");
            Map(pais => pais.NomePais).Column("PAIS_NOME");
        }
    }
}
