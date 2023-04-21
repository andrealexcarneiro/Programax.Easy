using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio
{
    [Serializable]
    public class Parametros : ObjetoDeNegocioBase
    {
        public Parametros()
        {
            ParametrosCadastros = new ParametrosCadastros();
            ParametrosFinanceiro = new ParametrosFinanceiro();
            ParametrosVenda = new ParametrosVenda();
            ParametrosFiscais = new ParametrosFiscais();
            
        }

        public virtual ParametrosCadastros ParametrosCadastros { get; set; }

        public virtual ParametrosFinanceiro ParametrosFinanceiro { get; set; }

        public virtual ParametrosVenda ParametrosVenda { get; set; }
        

        public virtual ParametrosFiscais ParametrosFiscais { get; set; }
        
        //public virtual bool HabilitarRecursosEspeciais { get; set; }
    }
}
