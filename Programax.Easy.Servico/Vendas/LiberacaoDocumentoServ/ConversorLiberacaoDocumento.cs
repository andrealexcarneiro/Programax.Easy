using System;
using Programax.Easy.Negocio.Vendas.LiberacaoDocumentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Vendas.LiberacaoDocumentoServ
{
    public class ConversorLiberacaoDocumento : ConversorDeObjetoBasico<LiberacaoDocumento>, IConversorDeObjeto<LiberacaoDocumento>
    {
        public LiberacaoDocumento CopieObjetoParaPersistencia(LiberacaoDocumento objetoDeNegocio)
        {
            throw new NotImplementedException();
        }
    }
}
