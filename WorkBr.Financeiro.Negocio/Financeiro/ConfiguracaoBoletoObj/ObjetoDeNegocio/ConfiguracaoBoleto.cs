using System;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContaBancariaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.ObjetoDeNegocio
{
    [Serializable]
    public class ConfiguracaoBoleto : ObjetoDeNegocioBase
    {
        public ConfiguracaoBoleto()
        {
            ListaInstrucoes = new List<InstrucoesBoleto>();
        }

        public virtual PerfilEmissaoBoleto Perfil  { get; set; }

        public virtual int Padrao { get; set; }

        public virtual Banco Banco { get; set; }

        public virtual Agencia Agencia { get; set; }

        public virtual ContaBancaria ContaBancaria { get; set; }

        public virtual string Carteira { get; set; }

        public virtual string Variacao { get; set; }

        public virtual string CodigoCedente { get; set; }

        public virtual int? DigitoCedente { get; set; }

        public virtual string Modalidade { get; set; }

        public virtual int? EspecieDocumento { get; set; }

        public virtual string TipoDocumentoRemessa { get; set; }

        public virtual string ConvenioRemessa { get; set; }

        public virtual string NossoNumero { get; set; }

        public virtual int ProximoLote { get; set; }

        public virtual string LocalPagamento { get; set; }

        public virtual string CaminhoRemessa { get; set; }
        
        public virtual IList<InstrucoesBoleto> ListaInstrucoes { get; set; }

    }
}
