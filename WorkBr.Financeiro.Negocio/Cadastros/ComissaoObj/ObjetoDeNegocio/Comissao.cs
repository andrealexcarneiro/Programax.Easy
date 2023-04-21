using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio
{
    [Serializable]
    public class Comissao : ObjetoDeNegocioBase
    {
        public virtual EnumFuncaoPessoaComissao? FuncaoPessoaComissao { get; set; }

        public virtual TabelaPreco TabelaPreco { get; set; }

        public virtual bool DescontoMaximoEhPercentual { get; set; }

        public virtual bool ValorComissaoEhPercentual { get; set; }

        public virtual double MetaFaturamento { get; set; }

        public virtual double DescontoMaximo { get; set; }

        public virtual double ValorComissao { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
