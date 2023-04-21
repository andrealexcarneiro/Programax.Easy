using System.Collections.Generic;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using Programax.Easy.Negocio.Fiscal.CartaCorrecaoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class NotaFiscal : ObjetoDeNegocioBase
    {
        public NotaFiscal()
        {
            IdentificacaoNotaFiscal = new IdentificacaoNotaFiscal();

            Emitente = new EmitenteNotaFiscal();
            Destinatario = new DestinatarioNotaFiscal();

            DadosCobranca = new DadosCobrancaNotaFiscal();
            ListaItens = new List<ItemNotaFiscal>();
            ListaNotasReferenciadas = new List<NotaFiscalReferenciada>();

            TotaisNotaFiscal = new TotaisNotaFiscal();

            InformacoesGeraisNotaFiscal = new InformacoesGeraisNotaFiscal();

            InformacoesDocumentoOrigemNotaFiscal = new InformacoesDocumentoOrigemNotaFiscal();

            InformacoesProtocoloAutorizacaoNotaFiscal = new InformacoesProtocoloAutorizacaoNotaFiscal();

            ListaFormasPagamentoNFCe = new List<FormaPagamentoNotaFiscal>();

            ListaCartasCorrecoes = new List<CartaCorrecao>();
        }

        public virtual IdentificacaoNotaFiscal IdentificacaoNotaFiscal { get; set; }

        public virtual EmitenteNotaFiscal Emitente { get; set; }

        public virtual DestinatarioNotaFiscal Destinatario { get; set; }

        public virtual LocalEntregaNotaFiscal LocalEntrega { get; set; }

        public virtual LocalRetiradaNotaFiscal LocalRetirada { get; set; }

        public virtual InformacoesDocumentoOrigemNotaFiscal InformacoesDocumentoOrigemNotaFiscal { get; set; }

        public virtual TotaisNotaFiscal TotaisNotaFiscal { get; set; }

        public virtual InformacoesGeraisNotaFiscal InformacoesGeraisNotaFiscal { get; set; }

        public virtual InformacoesProtocoloAutorizacaoNotaFiscal InformacoesProtocoloAutorizacaoNotaFiscal { get; set; }

        public virtual InformacoesCancelamentoNotaFiscal InformacoesCancelamentoNotaFiscal { get; set; }

        public virtual DadosCobrancaNotaFiscal DadosCobranca { get; set; }

        public virtual InformacoesCompraNotaFiscal InformacoesCompraNotaFiscal { get; set; }

        public virtual InformacoesComercioExteriorNotaFiscal InformacoesComercioExteriorNotaFiscal { get; set; }

        public virtual InformacoesSuplementaresNotaFiscal InformacoesSuplementaresNotaFiscal { get; set; }

        public virtual IList<ItemNotaFiscal> ListaItens { get; set; }

        public virtual IList<NotaFiscalReferenciada> ListaNotasReferenciadas { get; set; }

        public virtual IList<FormaPagamentoNotaFiscal> ListaFormasPagamentoNFCe { get; set; }

        public virtual IList<CartaCorrecao> ListaCartasCorrecoes { get; set; }
    }
}
