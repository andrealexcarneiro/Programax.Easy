using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio
{
    [Serializable]
    public class NaturezaOperacao:ObjetoDeNegocioBase
    {
        public NaturezaOperacao()
        {
            ListaCfops = new List<NaturezaOperacaoCfop>();
        }

        public virtual string Descricao { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Status { get; set; }

        public virtual EnumTipoMovimentacaoNaturezaOperacao TipoMovimentacao { get; set; }

        public virtual EnumOrigemDestino OrigemDestino { get; set; }

        public virtual PlanoDeContas PlanoDeContas { get; set; }

        public virtual bool GeraTitulosFinanceiro { get; set; }

        public virtual bool RealizaMovimentacaoEstoque { get; set; }

        public virtual bool ObrigatorioExistirPedidoVenda { get; set; }

        public virtual IList<NaturezaOperacaoCfop> ListaCfops { get; set; }
    }
}
