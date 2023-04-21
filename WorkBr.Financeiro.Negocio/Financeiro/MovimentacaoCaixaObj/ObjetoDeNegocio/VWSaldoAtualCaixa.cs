using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio
{
    [Serializable]
    public class VWSaldoAtualCaixa : ObjetoDeNegocioBase
    {
        public virtual double SaldoAtualDinheiro { get; set; }                
    }
}
