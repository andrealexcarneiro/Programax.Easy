using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Integracao.Enumeradores;

namespace Programax.Easy.Negocio.Integracao.TabelasAtualizadasIntegracaoDJObj.ObjetoDeNegocio
{
    public class TabelasAtualizadasIntegracaoDJ:ObjetoDeNegocioBase
    {
        public virtual EnumTabelaAtualizada TabelaAtualizada { get; set; }

        public virtual int IdRegistro { get; set; }
    }
}
