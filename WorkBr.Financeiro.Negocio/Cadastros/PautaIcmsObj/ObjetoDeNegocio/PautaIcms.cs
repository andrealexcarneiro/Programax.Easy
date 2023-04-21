using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.PautaIcmsObj.ObjetoDeNegocio
{
    public class PautaIcms : ObjetoDeNegocioBase
    {
        public virtual string Codigo { get; set; }

        public virtual string Instrucao { get; set; }

        public virtual double AliquotaSubstituicao { get; set; }

        public virtual double PrecoPauta { get; set; }

        public virtual DateTime? DataInicio { get; set; }

        public virtual string Status { get; set; }

        public virtual Cidade Cidade { get; set; }

        public virtual Estado Estado { get; set; }

        public virtual Produto Produto { get; set; }

        public virtual DateTime DataCadastro { get; set; }
    }
}
