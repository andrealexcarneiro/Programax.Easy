using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System.Collections.Generic;

namespace Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.Repositorio
{
    public interface IRepositorioFormaPagamento : IRepositorioBase<FormaPagamento>
    {
        List<FormaPagamento> ConsulteListaFormasDePagamentoAtivas();

        List<FormaPagamento> ConsulteLista(string descricao, string status);

        List<FormaPagamento> ConsulteListaAtivos();

        FormaPagamento ConsultePeloTipo(Enumeradores.EnumTipoFormaPagamento tipoFormaPagamento);
    }
}
