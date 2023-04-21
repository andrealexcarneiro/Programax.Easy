using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class EmitenteNotaFiscal
    {
        public virtual string CNAE { get; set; }

        public virtual string CNPJ { get; set; }

        public virtual EnumCodigoRegimeTributario CRT { get; set; }

        public virtual string Logradouro { get; set; }

        public virtual string Numero { get; set; }

        public virtual string Complemento { get; set; }

        public virtual string Bairro { get; set; }

        public virtual long CodigoMunicipio { get; set; }

        public virtual string NomeMunicipio { get; set; }

        public virtual string Cep { get; set; }

        public virtual string UF { get; set; }

        public int CodigoDoEstado { get; set; }

        public virtual int? CodigoPais { get; set; }

        public virtual string NomePais { get; set; }

        public virtual long? Telefone { get; set; }

        public virtual string InscricaoEstadual { get; set; }

        public virtual string NomeFantasia { get; set; }

        public virtual string RazaoSocial { get; set; }

        public virtual string InscricaoMunicipal { get; set; }
    }
}
