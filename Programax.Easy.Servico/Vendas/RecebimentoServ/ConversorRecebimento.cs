using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Vendas.RecebimentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Vendas.RecebimentoServ
{
    public class ConversorRecebimento : ConversorDeObjetoBasico<Recebimento>, IConversorDeObjeto<Recebimento>
    {
        public Recebimento CopieObjetoParaPersistencia(Recebimento objetoDeNegocio)
        {
            throw new NotImplementedException();
        }
    }
}
