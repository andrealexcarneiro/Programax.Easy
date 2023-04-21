using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio
{
    [Serializable]
    public class TabelaPreco : ObjetoDeNegocioBase
    {
        public virtual string NomeTabela { get; set; }

        public virtual string Status { get; set; }

        public virtual DateTime DataDeCadastro { get; set; }

        public virtual DateTime? DataDeValidade { get; set; }

        public virtual double Acrescimo { get; set; }

        public virtual bool AcrescimoEhPercentual { get; set; }

        public virtual double Decrescimo { get; set; }

        public virtual bool DecrescimoEhPercentual { get; set; }

        public virtual double Frete { get; set; }

        public virtual bool FreteEhPercentual { get; set; }

        #region " PROPRIEDADES NÃO MAPEADAS "

        public virtual string DescricaoStatus
        {
            get
            {
                return Status == "A" ? "Ativo" : "Inativo";
            }
        }

        #endregion
    }
}
