using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class LocalRetiradaNotaFiscal
    {
        public virtual EnumTipoPessoa TipoPessoa { get; set; }

        public virtual string CpfCnpj { get; set; }
                  
        public virtual string Logradouro { get; set; }
                  
        public virtual string Numero { get; set; }
                  
        public virtual string Complemento { get; set; }
                  
        public virtual string Bairro { get; set; }
                  
        public virtual int CodigoMunicipio { get; set; }
                  
        public virtual string NomeMunicipio { get; set; }
                  
        public virtual string UF { get; set; }
    }
}
