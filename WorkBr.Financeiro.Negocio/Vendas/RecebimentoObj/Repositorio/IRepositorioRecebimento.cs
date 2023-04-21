using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Vendas.RecebimentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Vendas.RecebimentoObj.Repositorio
{
    public interface IRepositorioRecebimento : IRepositorioBase<Recebimento>
    {
        List<Recebimento> ConsulteListaPorDataElaboracao(DateTime dataInicial, DateTime dataFinal, Enumeradores.EnumTipoDocumentoRecebimento? tipoDocumentoRecebimento);

        List<Recebimento> ConsulteListaPorDataFechamento(DateTime dataInicial, DateTime dataFinal, Enumeradores.EnumTipoDocumentoRecebimento? tipoDocumentoRecebimento);

        List<RecebimentoNf> ConsulteListaPorDataElaboracaoNf(DateTime dataInicial, DateTime dataFinal, Enumeradores.EnumTipoDocumentoRecebimento? tipoDocumentoRecebimento);

        List<RecebimentoNf> ConsulteListaPorDataFechamentoNf(DateTime dataInicial, DateTime dataFinal, Enumeradores.EnumTipoDocumentoRecebimento? tipoDocumentoRecebimento);

        Recebimento Consulte(int id, Enumeradores.EnumTipoDocumentoRecebimento tipoDocumentoRecebimento);

        RecebimentoNf ConsulteNf(int id, Enumeradores.EnumTipoDocumentoRecebimento tipoDocumentoRecebimento);
    }
}
