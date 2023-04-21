using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.InformacaoSistemaObj.ObjetoDeNegocio
{
    public class InformacaoSistema: ObjetoDeNegocioBase
    {
        public virtual string Versao { get; set; }

        public virtual DateTime DataVersao { get; set; }
    }
}
