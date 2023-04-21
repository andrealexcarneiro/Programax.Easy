using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PaisObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    [Serializable]
    public class DadosGerais
    {
        public virtual string Razao { get; set; }

        public virtual string NomeFantasia { get; set; }

        public virtual EnumTipoPessoa TipoPessoa { get; set; }

        public virtual bool EhCliente { get; set; }

        public virtual bool EhFornecedor { get; set; }

        public virtual bool EhFuncionario { get; set; }

        public virtual bool EhTransportadora { get; set; }

        public virtual string Status { get; set; }

        public virtual string CpfCnpj { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual byte[] Foto { get; set; }

        public virtual bool PessoaResideExterior { get; set; }

        public virtual Pais Pais { get; set; }

        public virtual EnumTipoCliente TipoCliente { get; set; }
    }
}
