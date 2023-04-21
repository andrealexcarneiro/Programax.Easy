using System;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.VincularChequePedidosObj.ObjetoDeNegocio
{
    [Serializable]
    public class VincularChequePedidos : ObjetoDeNegocioBase
    {
        public Cheque Cheque { get; set; }
        public Pessoa Pessoa { get; set; }
        public int NumeroPedidos { get; set;} 
    }
}
