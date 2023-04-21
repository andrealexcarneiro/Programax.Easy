using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using FluentNHibernate.Mapping.Providers;
using System.Data.SqlClient;
using Programax.Infraestrutura.Negocio.Utils;
using System.Data.Common;

namespace Programax.Easy.Servico.Cadastros.EstadoServ
{
    public class EstadoMap : MapeamentoBase<Estado>
    {
        public EstadoMap()
        {
            Table("ESTADOS");

            Id(estado => estado.UF).Column("ESTA_ID");

            Map(estado => estado.Nome).Column("ESTA_NOME");

            Map(estado => estado.CodigoEstado).Column("ESTA_IBGE");
        }
    }
}
