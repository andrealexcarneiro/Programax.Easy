using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio
{
    [Serializable]
    public class ParametrosFiscais
    {
        public virtual GrupoTributacaoIcms GrupoTributacaoIcmsProducaoPropria { get; set; }

        public virtual GrupoTributacaoIcms GrupoTributacaoIcmsTerceiros { get; set; }

        public virtual GrupoTributacaoFederal GrupoTributacaoFederalProducaoPropria { get; set; }

        public virtual GrupoTributacaoFederal GrupoTributacaoFederalTerceiros { get; set; }

        public virtual string IdCsc { get; set; }

        public virtual string CodigoCsc { get; set; }

        public virtual bool CalcularPartilhaIcms { get; set; }

        public virtual bool CalcularFCP { get; set; }

        public virtual bool AvisarQuandoHouverNcmForaDoPrazoValidade { get; set; }

        public virtual bool EmitirNotaSemReceber { get; set; }

        public virtual string ObservacoesGeraisNotaFiscal { get; set; }        
    }
}
