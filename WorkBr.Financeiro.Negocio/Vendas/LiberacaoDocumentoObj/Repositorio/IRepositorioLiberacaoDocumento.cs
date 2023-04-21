using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.LiberacaoDocumentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Vendas.LiberacaoDocumentoObj.Repositorio
{
    public interface IRepositorioLiberacaoDocumento : IRepositorioBase<LiberacaoDocumento>
    {
        LiberacaoDocumento Consulte(int id, Enumeradores.EnumTipoDocumentoLiberacao tipoDocumentoLiberacao);

        List<LiberacaoDocumento> ConsulteLista(DateTime? dataInicial, DateTime? dataFinal, Pessoa atendente, Pessoa vendedor, EnumTipoDocumentoLiberacao? tipoDocumentoLiberacao);
    }
}
