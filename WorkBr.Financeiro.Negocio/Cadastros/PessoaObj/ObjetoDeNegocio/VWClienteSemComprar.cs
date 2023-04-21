using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    [Serializable]
    public class VWClienteSemComprar : ObjetoDeNegocioBase
    {
        public VWClienteSemComprar()
        {
            ListaDeEnderecos = new List<VWEnderecosPessoas>();
        }

        public virtual string CpfCnpj { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Celular { get; set; }

        public virtual string Telefone { get; set; }

        public virtual string Atendente { get; set; }

        public virtual string Vendedor { get; set; }

        public virtual double? ValorUltimoPedido { get; set; }

        public virtual DateTime? DataUltimoPedido { get; set; }

        public virtual int DiasSemComprar { get; set; }

        public virtual int AtendenteId { get; set; }

        public virtual int VendedorId { get; set; }

        public virtual bool JahComprou { get; set; }

        public virtual IList<VWEnderecosPessoas> ListaDeEnderecos { get; set; }
    }
}
