using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;

namespace Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio
{
    [Serializable]
    public class VwMovimentacaoSaidaItens : ObjetoDeNegocioBase
    {
        public virtual DateTime DataMovimentacao { get; set; }

        public virtual int ItemId { get; set; }

        public virtual string DescricaoItem { get; set; }
        
        public virtual double QuantidadeVendida { get; set; }

        public virtual double Saida { get; set; }

        public virtual double Diferenca { get; set; }

        public virtual double Estoque { get; set; }
    }
}
