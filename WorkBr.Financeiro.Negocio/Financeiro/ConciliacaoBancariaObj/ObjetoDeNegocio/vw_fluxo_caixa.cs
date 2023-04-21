using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;

namespace Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.ObjetoDeNegocio
{
    [Serializable]
    public class vw_fluxo_caixa : ObjetoDeNegocioBase
    {
        public ulong Id2 { get; set; }
        public virtual int GRUPOID { get; set; }
        public virtual string NOMEGRUPO { get; set; }

        public virtual int CATEGORIAID { get; set; }
        public virtual string NOMECATEGORIA { get; set; }

        public virtual DateTime DATAREALIZADO { get; set; }
        public virtual double VALOR { get; set; }

        public virtual int CAIXABANCOID { get; set; }
        public virtual string NOMECAIXABANCO { get; set; }

        public virtual int TIPOMOVIMENTACAO { get; set; }
        public virtual string ORIGEM { get; set; }        
    }
}
