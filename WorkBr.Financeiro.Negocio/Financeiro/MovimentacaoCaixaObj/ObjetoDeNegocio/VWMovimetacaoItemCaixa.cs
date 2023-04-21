using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio
{
    [Serializable]
    public class VWMovimentacaoItemCaixa : ObjetoDeNegocioBase
    {
        public virtual int caixa { get; set; }

        public virtual int numeroDocumentoOrigem { get; set; }

        public virtual DateTime dataFechamento { get; set; }

        public virtual int id_Movimentacao {get; set;}

        public virtual int formaPagamento { get; set; }

        public virtual double valor { get; set; }
                
    }
}
