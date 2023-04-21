using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio
{
    [Serializable]
    public class Roteiro : ObjetoDeNegocioBase
    {
        public virtual Pessoa PessoaFuncionario { get; set; }

        public virtual PedidoDeVenda PedidoVenda { get; set; }
       
        public virtual EnumPeriodo Periodo { get; set; }

        public virtual EnumStatusRoteiro Status { get; set; }

        public virtual Pessoa Usuario { get; set; }
        
        public virtual string Historico { get; set; }

        public virtual DateTime DataElaboracao { get; set; }

        public virtual DateTime? DataConclusao { get; set; }

        public virtual string DetalheServico { get; set; }

        public virtual string Observacao { get; set; }

        public virtual EnumTipoServico TipoServico { get; set; }

        public virtual EnumTipoEndereco TipoEndereco { get; set; }

        public virtual int? RoteirizacaoId { get; set; }
    }
}
