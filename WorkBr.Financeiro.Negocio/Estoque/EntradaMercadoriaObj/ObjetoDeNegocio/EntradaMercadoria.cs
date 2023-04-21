using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.AjusteFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Estoque.Enumeradores;

namespace Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio
{
    [Serializable]
    public class EntradaMercadoria: ObjetoDeNegocioBase
    {
        public EntradaMercadoria()
        {
            ListaDeItens = new List<ItemEntrada>();
            ListaFinanceiroEntrada = new List<FinanceiroEntrada>();
        }

        public virtual Pessoa Fornecedor { get; set; }

        public virtual Pessoa UsuarioCadastro { get; set; }

        public virtual string NumeroNota { get; set; }

        public virtual string Serie { get; set; }

        public virtual string ChaveDeAcesso { get; set; }

        public virtual DateTime? DataEmissao { get; set; }

        public virtual DateTime? DataMovimentacao { get; set; }

        public virtual EnumCondicaoPagamentoNota CondicaoPagamentoEntrada { get; set; }

        public virtual NaturezaOperacao NaturezaOperacao { get; set; }

        public virtual string NaturezaOperacaoNota { get; set; }

        public virtual AjusteFiscal AjusteFiscal { get; set; }

        public virtual bool AtualizaPrecoCusto { get; set; }

        public virtual EnumModeloDocumentoFiscal? ModeloDocumentoFiscal { get; set; }

        public virtual Pessoa Transportadora { get; set; }

        public virtual string NumeroConhecimentoFrete { get; set; }

        public virtual EnumTipoFrete? TipoFrete { get; set; }

        public virtual double ValorFrete { get; set; }

        public virtual double BaseIcms { get; set; }

        public virtual double AliquotaIcms { get; set; }

        public virtual double ValorIcms { get; set; }

        public virtual double ValorIcmsDesoneracao { get; set; }

        public virtual double BaseIcmsSt { get; set; }

        public virtual double ValorIcmsSt { get; set; }

        public virtual double ValorDesconto { get; set; }

        public virtual double ValorOutrasDespesas { get; set; }

        public virtual double ValorIpi { get; set; }

        public virtual double ValorTotalNota { get; set; }

        public virtual int Tipo { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Observacoes { get; set; }

        public virtual EnumStatusEntrada StatusEntrada { get; set; }

        public virtual IList<ItemEntrada> ListaDeItens { get; set; }

        public virtual IList<FinanceiroEntrada> ListaFinanceiroEntrada { get; set; }
    }
}
