using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Negocio.Vendas.LiberacaoDocumentoObj.ObjetoDeNegocio
{
    public class LiberacaoDocumento : ObjetoDeNegocioBase
    {
        public virtual int ClienteId { get; set; }

        public virtual string ClienteNomeFantasia { get; set; }

        public virtual EnumTipoDocumentoLiberacao TipoDocumento { get; set; }

        public virtual string EnderecoCep { get; set; }

        public virtual int CidadeId { get; set; }

        public virtual string CidadeDescricao { get; set; }

        public virtual string EstadoUf { get; set; }

        public virtual string EstadoNome { get; set; }

        public virtual string ClienteInscricaoEstadual { get; set; }

        public virtual string ClienteInscricaoMunicipal { get; set; }

        public virtual EnumTipoPessoa ClienteTipoPessoa { get; set; }

        public virtual DateTime ClienteDataCadastro { get; set; }

        public virtual string ClienteStatus { get; set; }

        public virtual string ClienteCpfCnpj { get; set; }

        public virtual DateTime DataElaboracao { get; set; }

        public virtual DateTime DataFechamento { get; set; }

        public virtual int AtendenteId { get; set; }

        public virtual string AtendenteNomeFantasia { get; set; }

        public virtual int VendedorId { get; set; }

        public virtual string VendedorNomeFantasia { get; set; }

        public virtual double Desconto { get; set; }

        public virtual bool DescontoEhPercentual { get; set; }

        public virtual double ValorTotal { get; set; }

        public virtual int UsuarioId { get; set; }

        public virtual string UsuarioNomeFantasia { get; set; }

        public virtual bool MotivoBloqueioClienteBloqueado { get; set; }

        public virtual bool MotivoBloqueioDescontoAcima { get; set; }

        public virtual bool MotivoBloqueioFinanceiroEmAberto { get; set; }

        public virtual bool MotivoBloqueioItemSemEstoque { get; set; }

        public virtual bool MotivoBloqueioSemSaldoCredito { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as LiberacaoDocumento;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.Id == other.Id && this.TipoDocumento == other.TipoDocumento;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                hash = (hash * 31) ^ Id.GetHashCode();
                hash = (hash * 31) ^ TipoDocumento.GetHashCode();

                return hash;
            }
        }
    }
}
