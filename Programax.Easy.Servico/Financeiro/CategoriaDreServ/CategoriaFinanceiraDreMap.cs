using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Financeiro.CategoriaDreObj.ObjetoDeNeocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.CategoriaDreServ
{
    public class CategoriaFinanceiraDreMap : MapeamentoBase<CategoriaDre>
    {
        public CategoriaFinanceiraDreMap()
        {
            Table("CATEGORIASDRE");

            Id(categoria => categoria.Id).Column("CATDRE_ID");

            Map(categoria => categoria.Descricao).Column("CATDRE_DESCRICAO");
            Map(categoria => categoria.DataCadastro).Column("CATDRE_DATA_CADASTRO");
            Map(categoria => categoria.Status).Column("CATDRE_STATUS");

            Map(categoria => categoria.TipoCategoria).Column("CATDRE_TIPO_CATEGORIA").CustomType<EnumTipoCategoria>();

            References(categoria => categoria.SubGrupoCategoria).Column("CATDRE_SUBGRUPO_ID").LazyLoad();
        }
    }
}
