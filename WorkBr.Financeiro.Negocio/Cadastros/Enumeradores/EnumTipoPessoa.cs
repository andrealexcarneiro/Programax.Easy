using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Cadastros.Enumeradores
{
    public enum EnumTipoPessoa
    {
        [Description("Pessoa Jurídica - CNPJ")]
        PESSOAJURIDICA = 0,

        [Description("Pessoa Física - CPF")]
        PESSOAFISICA = 1
    }
}
