using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Cadastros.PaisObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.PaisServ
{
    public class ConversorPais : ConversorDeObjetoBasico<Pais>, IConversorDeObjeto<Pais>
    {
        public Pais CopieObjetoParaPersistencia(Pais objetoDeNegocio)
        {
            throw new NotImplementedException();
        }
    }
}
