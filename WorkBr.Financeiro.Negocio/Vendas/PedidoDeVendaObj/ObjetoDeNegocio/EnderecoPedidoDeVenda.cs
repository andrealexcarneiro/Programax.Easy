using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio
{
    [Serializable]
    public class EnderecoPedidoDeVenda
    {
        public virtual EnumTipoEndereco? TipoEndereco { get; set; }

        public virtual string Complemento { get; set; }

        public virtual string Numero { get; set; }

        public virtual string CEP { get; set; }

        public virtual string Rua { get; set; }

        public virtual string Bairro { get; set; }

        public virtual Cidade Cidade { get; set; }

        public bool ClienteResideExterior { get; set; }
    }
}
