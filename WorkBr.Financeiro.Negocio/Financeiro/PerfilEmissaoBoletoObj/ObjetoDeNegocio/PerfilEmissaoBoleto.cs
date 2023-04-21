using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.ObjetoDeNegocio
{
    [Serializable]
    public class PerfilEmissaoBoleto : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }
        
        public virtual DateTime DataCadastro { get; set; }

        public virtual int PessoaId { get; set; }

        public virtual bool EhPerfilPadrao { get; set; }
    }
}
