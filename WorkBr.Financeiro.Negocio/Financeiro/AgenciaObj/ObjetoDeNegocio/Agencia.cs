using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;

namespace Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio
{
    [Serializable]
    public class Agencia: ObjetoDeNegocioBase
    {
        public virtual string NomeAgencia { get; set; }

        public virtual string NumeroAgencia { get; set; }

        public virtual string DigitoAgencia { get; set; }

        public virtual string Status { get; set; }

        public virtual Banco Banco { get; set; }

        public virtual Endereco Endereco { get; set; }

        public virtual string NumeroEndereco { get; set; }

        public virtual string ComplementoEndereco { get; set; }

        public virtual string Telefone1 { get; set; }

        public virtual string Telefone2 { get; set; }

        public virtual string NomeGerente { get; set; }

        public virtual string CelularGerente { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Observacoes { get; set; }
    }
}
