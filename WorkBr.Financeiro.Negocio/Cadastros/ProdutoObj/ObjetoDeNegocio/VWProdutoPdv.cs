using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio
{
    [Serializable]
    public class VWProdutoPdv : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual string CodigoDeBarras { get; set; }

        public virtual double Estoque { get; set; }

        public virtual double EstoqueReservado { get; set; }

        public virtual double ValorVenda { get; set; }

        public virtual double ValorPromocao { get; set; }

        public virtual bool EhPromocao { get; set; }

        public virtual byte[] Foto { get; set; }
    }
}
