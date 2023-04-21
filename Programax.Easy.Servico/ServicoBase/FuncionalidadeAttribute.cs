using System;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;

namespace Programax.Easy.Servico.ServicoBase
{
    [AttributeUsage(AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class FuncionalidadeAttribute : Attribute
    {
        public EnumFuncionalidade Funcionaliade { get; set; }

        public FuncionalidadeAttribute(EnumFuncionalidade funcionalidade)
        {
            this.Funcionaliade = funcionalidade;
        }
    }
}
