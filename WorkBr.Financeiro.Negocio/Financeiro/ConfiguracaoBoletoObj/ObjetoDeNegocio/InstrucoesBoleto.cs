using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.ObjetoDeNegocio
{
    [Serializable]
    public class InstrucoesBoleto : ObjetoDeNegocioBase
    {
        public virtual int Item { get; set; }

        public virtual int CodigoInstrucao { get; set; }

        public virtual string DescricaoInstrucao { get; set; }

        public virtual int? Dias { get; set; }

        public virtual double? Valor { get; set; }

        public virtual int? TipoValor { get; set; }

        public virtual ConfiguracaoBoleto ConfiguracaoBoleto { get; set; }
    }
}
