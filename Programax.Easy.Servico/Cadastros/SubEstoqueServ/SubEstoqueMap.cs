using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubEstoqueObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.MarcaServ
{
    public class SubEstoqueMap : MapeamentoBase<SubEstoque>
    {
        public SubEstoqueMap()
            : base()
        {
            Table("SUBESTOQUE");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("SUBESTOQUE_ID");

            Map(subestoque => subestoque.Ativo).Column("SUBESTOQUE_STATUS");
            Map(subestoque => subestoque.DataCadastro).Column("SUBESTOQUE_DATA");
            Map(subestoque => subestoque.Descricao).Column("SUBESTOQUE_DESCRICAO");
        }
    }
}
