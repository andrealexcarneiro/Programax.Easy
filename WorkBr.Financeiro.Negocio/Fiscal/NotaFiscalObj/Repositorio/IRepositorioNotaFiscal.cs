using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.Repositorio
{
    public interface IRepositorioNotaFiscal : IRepositorioBase<NotaFiscal>
    {
        List<VwNotasDocumentos> ConsulteListaVwNotasDocumentos(int? numeroDocumento, DateTime? dataInicial, DateTime? dataFinal, Enumeradores.EnumTipoDocumento? tipoDocumento, Enumeradores.EnumStatusNotaFiscal? statusNotaFiscal, EnumModeloNotaFiscal? modelo);

        List<NotaFiscal> ConsulteListaDocumentos(int? numeroDocumento, DateTime? dataInicial, DateTime? dataFinal, Enumeradores.EnumTipoDocumento? tipoDocumento, Enumeradores.EnumStatusNotaFiscal? statusNotaFiscal, EnumModeloNotaFiscal? modelo, EnumTipoDeEmissaoPesquisa? tipoEmissao, int? numeroNF = null);

        NotaFiscal Consulte(int serie, int numero, EnumStatusNotaFiscal? status, EnumModeloNotaFiscal modelo = EnumModeloNotaFiscal.NFE);

        List<NotaFiscal> ConsulteListaComJoinItens(List<int> listaIds);
    }
}
