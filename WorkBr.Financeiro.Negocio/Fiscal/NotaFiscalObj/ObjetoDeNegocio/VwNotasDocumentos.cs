using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    public class VwNotasDocumentos : ObjetoDeNegocioBase
    {
        public virtual string NumeroNFe { get; set; }

        public virtual string Serie { get; set; }

        public virtual EnumModeloNotaFiscal Modelo { get; set; }

        public virtual DateTime? DataEmissao { get; set; }

        public virtual int ClienteId { get; set; }

        public virtual string ClienteCpfCnpj { get; set; }

        public virtual string ClienteNomeFantasia { get; set; }

        public virtual string ClienteStatus { get; set; }

        public virtual string ClienteInscricaoEstadual { get; set; }

        public virtual EnumTipoPessoa ClienteTipo { get; set; }

        public virtual DateTime ClienteDataCadastro { get; set; }

        public virtual string Cep { get; set; }

        public virtual string Cidade { get; set; }

        public virtual string Estado { get; set; }

        public virtual string NaturezaOperacao { get; set; }

        public virtual int NumeroDocumento { get; set; }

        public virtual int? AtendenteId { get; set; }

        public virtual string AtendenteNome { get; set; }

        public virtual int? VendedorId { get; set; }

        public virtual string VendedorNome { get; set; }

        public virtual int UsuarioId { get; set; }

        public virtual string UsuarioNome { get; set; }

        public virtual double ValorTotal { get; set; }

        public virtual DateTime DataElaboracao { get; set; }

        public virtual EnumStatusNotaFiscal StatusNotaFiscal { get; set; }

        public virtual string MensagemErroNFe { get; set; }

        public virtual string MensagemDevolvida { get; set; }

        public virtual EnumTipoDocumento TipoDocumento { get; set; }

        public virtual bool JahTemNotaCadastrada { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as VwNotasDocumentos;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.Id == other.Id &&
                     this.NumeroDocumento == other.NumeroDocumento &&
                     this.TipoDocumento == other.TipoDocumento;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                hash = (hash * 31) ^ Id.GetHashCode();
                hash = (hash * 31) ^ NumeroDocumento.GetHashCode();
                hash = (hash * 31) ^ TipoDocumento.GetHashCode();

                return hash;
            }
        }
    }
}
