using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.CaixaServ
{
    public class CaixaMap : MapeamentoBase<Caixa>
    {
        public CaixaMap()
        {
            Table("CAIXAS");

            Id(caixa => caixa.Id).Column("CAIXA_ID");

            Map(caixa => caixa.NomeCaixa).Column("CAIXA_NOME");
            Map(caixa => caixa.DataCadastro).Column("CAIXA_DATA_CADASTRO");
            Map(caixa => caixa.Status).Column("CAIXA_STATUS");
            Map(caixa => caixa.PerfilCaixa).Column("CAIXA_PERFIL").CustomType<EnumPerfilCaixa>();

            References(caixa => caixa.Funcionario).Column("CAIXA_PES_ID");
        }
    }
}
