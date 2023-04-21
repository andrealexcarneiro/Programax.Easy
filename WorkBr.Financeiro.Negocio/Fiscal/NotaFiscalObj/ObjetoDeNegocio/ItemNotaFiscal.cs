using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class ItemNotaFiscal : ObjetoDeNegocioBase
    {
        public virtual Produto Produto { get; set; }

        public virtual string NomeProduto { get; set; }

        public virtual string UnidadeProduto { get; set; }

        public virtual string CodigoBarrasProduto { get; set; }

        public virtual string CodigoGtinProduto { get; set; }

        public virtual string Ncm { get; set; }

        public virtual string Cest { get; set; }

        public virtual int Cfop { get; set; }

        public virtual double ValorUnitario { get; set; }

        public virtual double Quantidade { get; set; }

        public virtual double? ValorDesconto { get; set; }
        public virtual double? ValorFrete { get; set; }
        public virtual double? Seguro { get; set; }
        public virtual double? OutrasDespesas { get; set;}

        public virtual double ValorBruto
        {
            get
            {
                return Math.Round((Quantidade * ValorUnitario), 2);
            }
        }

        public virtual double ValorTotal { get; set; }

        public virtual ImpostosNotaFiscal Impostos { get; set; }

        public virtual NotaFiscal NotaFiscal { get; set; }
    }
}
