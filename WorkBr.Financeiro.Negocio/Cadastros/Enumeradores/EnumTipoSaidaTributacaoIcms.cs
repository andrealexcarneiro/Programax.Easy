using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Cadastros.Enumeradores
{
    public enum EnumTipoSaidaTributacaoIcms
    {
        [Description("SAÍDA DE VENDAS")]
        SAIDAVENDA,

        [Description("ENTRADA DEVOLUÇÃO VENDAS")]
        ENTRADADEVOLUCAOVENDA
    }
}
