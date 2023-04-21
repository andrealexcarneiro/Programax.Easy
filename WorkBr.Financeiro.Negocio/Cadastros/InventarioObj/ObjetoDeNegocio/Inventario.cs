using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio
{
    [Serializable]
    public class Inventario:ObjetoDeNegocioBase
    {
        public Inventario()
        {
            ListaDeItens = new List<ItemInventario>();
        }

        public virtual EnumTipoInventario? TipoInventario { get; set; }

        public virtual EnumModalidadeInventario? Modalidade { get; set; }

        public virtual DateTime DataInicio { get; set; }

        public virtual DateTime DataFinal { get; set; }

        public virtual Marca Marca { get; set; }

        public virtual Grupo Grupo { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual SubGrupo SubGrupo { get; set; }

        public virtual bool BloquearProdutosMovimentacao { get; set; }

        public virtual bool UtilizarSaldoPrimeiraContagem { get; set; }

        public virtual EnumFiltroSituacao? FiltroSituacaoSaldo { get; set; }

        public virtual int ContagemAtual { get; set; }

        public virtual EnumStatusInventario Status { get; set; }

        public virtual EnumFiltroOrdenacaoContagem OrdenacaoContagem { get; set; }

        public virtual IList<ItemInventario> ListaDeItens { get; set; }

        public virtual DateTime? DataInicioPrimeiraContagem { get; set; }

        public virtual DateTime? DataInicioSegundaContagem{ get; set; }

        public virtual DateTime? DataInicioTerceiraContagem { get; set; }
    }
}
