using System;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio
{
    [Serializable]
    public class CategoriaFinanceira : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Status { get; set; }
        public virtual Boolean Mostrar { get; set; }
        public virtual Boolean MostrarDRE { get; set; }

        public virtual EnumTipoCategoria TipoCategoria { get; set; }

        public virtual SubGrupoCategoria SubGrupoCategoria { get; set; }

        
    }
}
