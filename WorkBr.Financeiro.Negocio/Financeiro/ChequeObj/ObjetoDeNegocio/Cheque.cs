using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.VincularChequePedidosObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio
{
    [Serializable]
    public class Cheque:ObjetoDeNegocioBase
    {
        public Cheque()
        {
            ListaVinculosDePedidos = new List<VincularChequePedidos>();
        }

        public virtual Pessoa Pessoa { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string NomeCheque { get; set; }

        public virtual string CpfCnpj { get; set; }

        public virtual DateTime? DataEmissao { get; set; }

        public virtual DateTime? DataVencimento { get; set; }

        public virtual Banco Banco { get; set; }

        public virtual double ValorCheque { get; set; }

        public virtual EnumStatusCheque? StatusCheque { get; set; }

        public virtual string Observacoes { get; set; }

        public virtual bool EhCnpj { get; set; }

        public virtual DateTime? DataRecebimento { get; set; }

        public virtual string Agencia { get; set; }

        public virtual string Conta { get; set; }

        public virtual string Digito { get; set; }

        public virtual string Serie { get; set; }

        public virtual string NumeroCheque { get; set; }

        public virtual int NumeroPedidoVenda { get; set; }

        public virtual string NumeroDocumento { get; set; }

        public virtual IList<VincularChequePedidos> ListaVinculosDePedidos { get; set; }

    }
}
