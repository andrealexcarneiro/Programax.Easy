using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using System;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class DestinatarioNotaFiscal
    {
        public virtual Pessoa Pessoa { get; set; }

        public virtual string CnpjCpf { get; set; }

        public virtual bool EhPessoaFisica { get; set; }

        public virtual string RazaoSocialOuNomeDestinatario { get; set; }

        public virtual EnumIndicadorIEDestinatario IndicadorIEDestinatario { get; set; }

        public virtual string InscricaoEstadual { get; set; }

        public virtual string InscricaoSuframa { get; set; }

        public virtual string Email { get; set; }

        public virtual string Logradouro { get; set; }

        public virtual string Numero { get; set; }

        public virtual string Complemento { get; set; }

        public virtual string Bairro { get; set; }

        public virtual long CodigoMunicipio { get; set; }

        public virtual string NomeMunicipio { get; set; }

        public virtual string Cep { get; set; }

        public virtual string UF { get; set; }

        public virtual int? CodigoPais { get; set; }

        public virtual string NomePais { get; set; }

        public virtual bool ParceiroResideExterior { get; set; }

        public virtual long? Telefone { get; set; }

        public virtual string IdEstrangeiro { get; set; }

        public virtual EnumTipoPessoa TipoPessoa { get; set; }

        public virtual DateTime DataCadastroPessoa { get; set; }

        public virtual string StatusPessoa { get; set; }
    }
}
