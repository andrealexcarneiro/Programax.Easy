using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio
{
    [Serializable]
    public class VWProduto: ObjetoDeNegocioBase
    {
        public virtual string CodigoBarras { get; set; }
                 
        public virtual string Descricao { get; set; }

        public virtual int CodigoFiscal { get; set; }
                 
        public virtual int Estoque { get; set; }

        public virtual int EstoqueReservado { get; set; }
                 
        public virtual int QtdMinima { get; set; }
                 
        public virtual double ValorVenda { get; set; }
                 
        public virtual double ValorCompra { get; set; }

        public virtual double PrecoCompra { get; set; }
                 
        public virtual int CategoriaId { get; set; }
                 
        public virtual string CategoriaDescricao { get; set; }
                 
        public virtual int GrupoId { get; set; }
                 
        public virtual string GrupoDescricao { get; set; }
                 
        public virtual int SubGrupoId { get; set; }
                 
        public virtual string SubGrupoDescricao { get; set; }
                 
        public virtual int MarcaId { get; set; }
                 
        public virtual string MarcaDescricao { get; set; }
                 
        public virtual int FabricanteId { get; set; }
                 
        public virtual string FabricanteDescricao { get; set; }
                 
        public virtual int TamanhoId { get; set; }
                 
        public virtual string TamanhoDescricao { get; set; }
                 
        public virtual int UnidadeId { get; set; }
                 
        public virtual string UnidadeDescricao { get; set; }
                 
        public virtual int DDV { get; set; }
    }
}
