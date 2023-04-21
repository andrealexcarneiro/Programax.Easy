using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Estoque.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class IdentificacaoNotaFiscal
    {
        public virtual Cidade Cidade { get; set; }

        public virtual long CodigoNumericoNota { get; set; }

        public virtual NaturezaOperacao NaturezaOperacao { get; set; }

        public virtual string DescricaoNaturezaOperacao { get; set; }

        public virtual EnumCondicaoPagamentoNota FormaPagamento { get; set; }

        public virtual int ModeloDocumentoFiscal { get; set; }

        public virtual int Serie { get; set; }

        public virtual int NumeroNota { get; set; }

        public virtual DateTime DataHoraEmissao { get; set; }

        public virtual DateTime? DataHoraSaida { get; set; }

        public virtual bool NotaSaida { get; set; }

        public virtual EnumIdenficacaoOperacaoNotaFiscal IdentificacaoOperacaoNotaFiscal { get; set; }

        public virtual EnumFormatoImpressaoDanfe FormatoImpressaoDanfe { get; set; }

        public virtual EnumTipoEmissaoDanfe TipoEmissaoDanfe { get; set; }

        public virtual int DigitoVerificadorChaveAcesso { get; set; }

        public virtual EnumTipoAmbiente TipoAmbiente { get; set; }

        public virtual EnumFinalidadeEmissaoNfe FinalidadeEmissaoNFe { get; set; }

        public virtual bool ConsumidorFinal { get; set; }

        public virtual EnumIndicacaoPresenca IndicacaoPresenca { get; set; }

        public virtual int ProcessoEmissaoNfe { get { return 0; } }

        public string VersaoAplicativo { get; set; }

        public bool IrProximoNumero { get; set; }
    }
}
