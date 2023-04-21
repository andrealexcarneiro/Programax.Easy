using System;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio
{
    [Serializable]
    public class HistoricoRoteiro : ObjetoDeNegocioBase
    {
        public virtual Roteirizacao Roteirizacao { get; set; }
       
        public virtual Pessoa Usuario { get; set; }
        
        public virtual string DescricaoHistorico { get; set; }      

        public virtual DateTime DataHistorico { get; set; }
    }
}
