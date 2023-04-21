using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Fiscal.NotaFiscalServ.ClassesAuxiliares
{
    public class DestinatarioAuxiliarNotaFiscal
    {
        public EnumTipoPessoa TipoPessoa { get; set; }

        public EnumTipoCliente TipoCliente { get; set; }

        public string Email { get; set; }

        public string Nome { get; set; }

        public string CpfCnpj { get; set; }

        public string InscricaoEstadual { get; set; }

        public EnumTipoInscricaoICMS TipoContribuinteICMS { get; set; }

        public string StatusPessoa { get; set; }

        public DateTime DataCadastro { get; set; }

        public int? DddTelefone { get; set; }

        public string NumeroTelefone { get; set; }

        public bool ResideNoExterior { get; set; }

        public string Bairro { get; set; }

        public string Cep { get; set; }

        public string Complemento { get; set; }

        public string Logradouro { get; set; }

        public string NomeCidade { get; set; }

        public long CodigoCidade { get; set; }

        public string UF { get; set; }

        public string Numero { get; set; }

        public string NomePais { get; set; }

        public int CodigoPais { get; set; }

        public string IdEstrangeiro { get; set; }

        public bool IrProximoNumero { get; set; }

        public string DescricaoLocalDespacho { get; set; }

        public string DescricaoLocalEmbarque { get; set; }

        public string UFEmbarque { get; set; }
    }
}
